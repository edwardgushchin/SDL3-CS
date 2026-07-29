#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

namespace SDL3;

public partial class SDL
{
    /// <summary>
    /// Defines a managed SDL application that uses the main-callback lifecycle.
    /// </summary>
    /// <typeparam name="TSelf">The managed application state type.</typeparam>
    /// <remarks>
    /// Use <see cref="RunMainCallbacks{TApp}"/> to run an implementation directly,
    /// or apply <see cref="GenerateMainAttribute"/> to let SDL3-CS generate the entry point.
    /// </remarks>
    public interface IMainCallbacks<TSelf>
        where TSelf : class, IMainCallbacks<TSelf>
    {
        /// <summary>
        /// Creates and initializes the managed application state.
        /// </summary>
        /// <param name="appState">The managed application state, or <c>null</c> when initialization terminates before state is needed.</param>
        /// <param name="args">The application arguments, excluding the executable path.</param>
        /// <returns><see cref="AppResult.Continue"/> to start the callback loop, <see cref="AppResult.Success"/> to finish successfully, or <see cref="AppResult.Failure"/> to finish with an error.</returns>
        static abstract AppResult AppInit(out TSelf? appState, string[] args);

        /// <summary>
        /// Runs one managed application iteration.
        /// </summary>
        /// <returns><see cref="AppResult.Continue"/> to keep running, <see cref="AppResult.Success"/> to finish successfully, or <see cref="AppResult.Failure"/> to finish with an error.</returns>
        AppResult AppIterate();

        /// <summary>
        /// Handles an SDL event for the managed application.
        /// </summary>
        /// <param name="event">The event to examine or update.</param>
        /// <returns><see cref="AppResult.Continue"/> to keep running, <see cref="AppResult.Success"/> to finish successfully, or <see cref="AppResult.Failure"/> to finish with an error.</returns>
        AppResult AppEvent(ref Event @event);

        /// <summary>
        /// Releases resources owned by the managed application.
        /// </summary>
        /// <param name="result">The result that terminated the application.</param>
        void AppQuit(AppResult result);
    }

    /// <summary>
    /// Requests generation of a managed <c>Main(string[])</c> entry point for an
    /// <see cref="IMainCallbacks{TSelf}"/> implementation.
    /// </summary>
    /// <remarks>
    /// Apply this attribute to exactly one top-level, non-abstract, non-generic partial class
    /// in an executable project. The source generator calls <see cref="RunMainCallbacks{TApp}"/>.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public sealed class GenerateMainAttribute : Attribute
    {
    }

    /// <summary>
    /// Runs a managed SDL application through the SDL main-callback lifecycle.
    /// </summary>
    /// <typeparam name="TApp">The managed application state type.</typeparam>
    /// <param name="args">The application arguments from a C# <c>Main(string[])</c>, excluding the executable path, or <c>null</c> for no arguments.</param>
    /// <returns>The platform main return code from <see cref="EnterAppMainCallbacks"/>.</returns>
    /// <exception cref="InvalidOperationException"><typeparamref name="TApp"/> returns <see cref="AppResult.Continue"/> without creating managed state.</exception>
    /// <remarks>
    /// Managed callback exceptions are contained before crossing the native boundary. The first
    /// exception is rethrown on the calling thread after SDL finishes the callback lifecycle and
    /// managed state has been released.
    /// </remarks>
    public static int RunMainCallbacks<TApp>(string[]? args)
        where TApp : class, IMainCallbacks<TApp>
    {
        string[] managedArgs = args is null ? [] : (string[])args.Clone();
        string[] nativeArgv = CreateNativeMainArguments(managedArgs);
        MainCallbacksContext<TApp> context = new(managedArgs);
        ExceptionDispatchInfo? nativeFailure = null;
        int result = 0;

        try
        {
            result = EnterAppMainCallbacks(
                nativeArgv.Length,
                nativeArgv,
                context.AppInit,
                context.AppIterate,
                context.AppEvent,
                context.AppQuit);
        }
        catch (Exception exception)
        {
            nativeFailure = ExceptionDispatchInfo.Capture(exception);
        }
        finally
        {
            context.EnsureQuit();
        }

        nativeFailure?.Throw();
        context.ThrowIfFaulted();
        GC.KeepAlive(context);
        return result;
    }

    private static string[] CreateNativeMainArguments(string[] args)
    {
        string? executablePath = Environment.ProcessPath;
        if (string.IsNullOrWhiteSpace(executablePath))
        {
            executablePath = AppDomain.CurrentDomain.FriendlyName;
        }

        if (string.IsNullOrWhiteSpace(executablePath))
        {
            executablePath = "SDL3-CS";
        }

        string[] nativeArguments = new string[args.Length + 1];
        nativeArguments[0] = executablePath;
        Array.Copy(args, 0, nativeArguments, 1, args.Length);
        return nativeArguments;
    }

    private sealed class MainCallbacksContext<TApp>
        where TApp : class, IMainCallbacks<TApp>
    {
        private readonly object sync = new();
        private readonly string[] arguments;
        private readonly AppInitFunc appInitCallback;
        private readonly AppIterateFunc appIterateCallback;
        private readonly AppEventFunc appEventCallback;
        private readonly AppQuitFunc appQuitCallback;
        private TApp? application;
        private GCHandle stateHandle;
        private ExceptionDispatchInfo? callbackFailure;
        private AppResult terminalResult = AppResult.Failure;
        private int activeCallbacks;
        private bool stateHandleAllocated;
        private bool quitStarted;
        private bool quitCompleted;

        internal MainCallbacksContext(string[] arguments)
        {
            this.arguments = arguments;
            appInitCallback = AppInitCore;
            appIterateCallback = AppIterateCore;
            appEventCallback = AppEventCore;
            appQuitCallback = AppQuitCore;
        }

        internal AppInitFunc AppInit => appInitCallback;
        internal AppIterateFunc AppIterate => appIterateCallback;
        internal AppEventFunc AppEvent => appEventCallback;
        internal AppQuitFunc AppQuit => appQuitCallback;

        internal void EnsureQuit()
        {
            Complete(AppResult.Failure);
        }

        internal void ThrowIfFaulted()
        {
            callbackFailure?.Throw();
        }

        private AppResult AppInitCore(ref IntPtr appstate, int argc, string[]? argv)
        {
            try
            {
                AppResult result = TApp.AppInit(out TApp? createdApplication, arguments);
                if (createdApplication is null)
                {
                    if (result == AppResult.Continue)
                    {
                        throw new InvalidOperationException($"{typeof(TApp).FullName}.AppInit returned Continue without creating managed state.");
                    }

                    SetTerminalResultIfRunning(result);
                    return result;
                }

                lock (sync)
                {
                    application = createdApplication;
                    stateHandle = GCHandle.Alloc(this, GCHandleType.Normal);
                    stateHandleAllocated = true;
                    appstate = GCHandle.ToIntPtr(stateHandle);
                    terminalResult = result;
                }

                return result;
            }
            catch (Exception exception)
            {
                CaptureFailure(exception);
                SetTerminalResultIfRunning(AppResult.Failure);
                return AppResult.Failure;
            }
        }

        private AppResult AppIterateCore(IntPtr appstate)
        {
            if (!TryEnterCallback(out TApp? currentApplication))
            {
                return GetTerminalResult();
            }

            try
            {
                AppResult result = currentApplication!.AppIterate();
                SetTerminalResultIfRunning(result);
                return result;
            }
            catch (Exception exception)
            {
                CaptureFailure(exception);
                SetTerminalResultIfRunning(AppResult.Failure);
                return AppResult.Failure;
            }
            finally
            {
                ExitCallback();
            }
        }

        private AppResult AppEventCore(IntPtr appstate, ref Event @event)
        {
            if (!TryEnterCallback(out TApp? currentApplication))
            {
                return GetTerminalResult();
            }

            try
            {
                AppResult result = currentApplication!.AppEvent(ref @event);
                SetTerminalResultIfRunning(result);
                return result;
            }
            catch (Exception exception)
            {
                CaptureFailure(exception);
                SetTerminalResultIfRunning(AppResult.Failure);
                return AppResult.Failure;
            }
            finally
            {
                ExitCallback();
            }
        }

        private void AppQuitCore(IntPtr appstate, AppResult result)
        {
            Complete(result);
        }

        private bool TryEnterCallback(out TApp? currentApplication)
        {
            lock (sync)
            {
                if (quitStarted || application is null)
                {
                    currentApplication = null;
                    return false;
                }

                activeCallbacks++;
                currentApplication = application;
                return true;
            }
        }

        private void ExitCallback()
        {
            lock (sync)
            {
                activeCallbacks--;
                if (activeCallbacks == 0)
                {
                    Monitor.PulseAll(sync);
                }
            }
        }

        private AppResult GetTerminalResult()
        {
            lock (sync)
            {
                return terminalResult;
            }
        }

        private void SetTerminalResultIfRunning(AppResult result)
        {
            lock (sync)
            {
                if (!quitStarted)
                {
                    terminalResult = result;
                }
            }
        }

        private void Complete(AppResult result)
        {
            TApp? applicationToQuit;
            GCHandle handleToFree = default;
            bool freeHandle;

            lock (sync)
            {
                if (quitStarted)
                {
                    while (!quitCompleted)
                    {
                        Monitor.Wait(sync);
                    }

                    return;
                }

                quitStarted = true;
                terminalResult = result;
                while (activeCallbacks != 0)
                {
                    Monitor.Wait(sync);
                }

                applicationToQuit = application;
                application = null;
                freeHandle = stateHandleAllocated;
                if (freeHandle)
                {
                    handleToFree = stateHandle;
                    stateHandleAllocated = false;
                }
            }

            try
            {
                applicationToQuit?.AppQuit(result);
            }
            catch (Exception exception)
            {
                CaptureFailure(exception);
            }
            finally
            {
                if (freeHandle)
                {
                    handleToFree.Free();
                }

                lock (sync)
                {
                    quitCompleted = true;
                    Monitor.PulseAll(sync);
                }
            }
        }

        private void CaptureFailure(Exception exception)
        {
            lock (sync)
            {
                callbackFailure ??= ExceptionDispatchInfo.Capture(exception);
            }
        }
    }
}
