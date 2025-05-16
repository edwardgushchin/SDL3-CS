#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    /// <code>extern SDL_DECLSPEC SDL_Process *SDLCALL SDL_CreateProcess(const char * const *args, bool pipe_stdio);</code>
    /// <summary>
    /// <para>Create a new process.</para>
    /// <para>The path to the executable is supplied in args[0]. args[1..N] are
    /// additional arguments passed on the command line of the new process, and the
    /// argument list should be terminated with a <c>null</c>, e.g.:</para>
    /// <code>const char *args[] = { "myprogram", "argument", <c>null</c>};</code>
    /// <para>Setting pipe_stdio to true is equivalent to setting
    /// <see cref="Props.ProcessCreateSTDInNumber"/> and
    /// <see cref="Props.ProcessCreateSTDOutNumber"/> to <see cref="ProcessIO.App"/>, and
    /// will allow the use of <see cref="ReadProcess"/> or <see cref="GetProcessInput"/> and
    /// <see cref="GetProcessOutput"/>.</para>
    /// <para>See <see cref="CreateProcessWithProperties"/> for more details.</para>
    /// </summary>
    /// <param name="args">the path and arguments for the new process.</param>
    /// <param name="pipeStdio"><c>true</c> to create pipes to the process's standard input and
    /// from the process's standard output, <c>false</c> for the process
    /// to have no input and inherit the application's standard
    /// output.</param>
    /// <returns>the newly created and running process, or <c>null</c> if the process
    /// couldn't be created.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessProperties"/>
    /// <seealso cref="ReadProcess"/>
    /// <seealso cref="GetProcessInput"/>
    /// <seealso cref="GetProcessOutput"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateProcess"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateProcess([MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str)] string[] args, [MarshalAs(UnmanagedType.I1)] bool pipeStdio);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Process *SDLCALL SDL_CreateProcessWithProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Create a new process with the specified properties.</para>
    /// <para>These are the supported properties:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.ProcessCreateArgsPointer"/>: an array of strings containing
    /// the program to run, any arguments, and a <c>null</c> pointer, e.g. const char
    /// *args[] = { "myprogram", "argument", <c>null</c>}. This is a required property.</item>
    /// <item><see cref="Props.ProcessCreateEnvironmentPointer"/>: an SDL_Environment
    /// pointer. If this property is set, it will be the entire environment for
    /// the process, otherwise the current environment is used.</item>
    /// <item><see cref="Props.ProcessCreateWorkingDirectoryString"/>: a UTF-8 encoded
    /// string representing the working directory for the process, defaults to
    /// the current working directory.</item>
    /// <item><see cref="Props.ProcessCreateSTDInNumber"/>: an SDL_ProcessIO value describing
    /// where standard input for the process comes from, defaults to
    /// <see cref="ProcessIO.Null"/>.</item>
    /// <item><see cref="Props.ProcessCreateSTDInPointer"/>: an SDL_IOStream pointer used for
    /// standard input when <see cref="Props.ProcessCreateSTDInNumber"/> is set to
    /// <see cref="ProcessIO.Redirect"/>.</item>
    /// <item><see cref="Props.ProcessCreateSTDOutNumber"/>: an SDL_ProcessIO value
    /// describing where standard output for the process goes to, defaults to
    /// <see cref="ProcessIO.Inherited"/>.</item>
    /// <item><see cref="Props.ProcessCreateSTDOutPointer"/>: an SDL_IOStream pointer used
    /// for standard output when <see cref="Props.ProcessCreateSTDOutNumber"/> is set
    /// to <see cref="ProcessIO.Redirect"/>..</item>
    /// <item><see cref="Props.ProcessCreateSTDErrNumber"/>: an SDL_ProcessIO value
    /// describing where standard error for the process goes to, defaults to
    /// <see cref="ProcessIO.Inherited"/>.</item>
    /// <item><see cref="Props.ProcessCreateSTDErrPointer"/>: an SDL_IOStream pointer used
    /// for standard error when <see cref="Props.ProcessCreateSTDErrNumber"/> is set to
    /// <see cref="ProcessIO.Redirect"/>.</item>
    /// <item><see cref="Props.ProcessCreateSTDErrToSTDOutBoolean"/>: true if the error
    /// output of the process should be redirected into the standard output of
    /// the process. This property has no effect if
    /// <see cref="Props.ProcessCreateSTDErrNumber"/> is set.</item>
    /// <item><see cref="Props.ProcessCreateBackgroundBoolean"/>: true if the process should
    /// run in the background. In this case the default input and output is
    /// <see cref="ProcessIO.Null"/> and the exitcode of the process is not
    /// available, and will always be 0.</item>
    /// <item><see cref="Props.ProcessCreateCMDLineString"/>: a string containing the program
    /// to run and any parameters. This string is passed directly to
    /// <c>CreateProcess</c> on Windows, and does nothing on other platforms. This
    /// property is only important if you want to start programs that does
    /// non-standard command-line processing, and in most cases using
    /// <see cref="Props.ProcessCreateArgsPointer"/> is sufficient.</item>
    /// </list>
    /// <para>On POSIX platforms, wait() and waitpid(-1, ...) should not be called, and
    /// SIGCHLD should not be ignored or handled because those would prevent SDL
    /// from properly tracking the lifetime of the underlying process. You should
    /// use <see cref="WaitProcess"/> instead.</para>
    /// </summary>
    /// <param name="props">the properties to use.</param>
    /// <returns>the newly created and running process, or <c>null</c> if the process
    /// couldn't be created.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="GetProcessProperties"/>
    /// <seealso cref="ReadProcess"/>
    /// <seealso cref="GetProcessInput"/>
    /// <seealso cref="GetProcessOutput"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateProcessWithProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateProcessWithProperties(uint props);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetProcessProperties(SDL_Process *process);</code>
    /// <summary>
    /// <para>Get the properties associated with a process.</para>
    /// <para>The following read-only properties are provided by SDL:</para>
    /// <list type="bullet">
    /// <item><see cref="Props.ProcessPIDNumber"/>: the process ID of the process.</item>
    /// <item><see cref="Props.ProcessSTDInPointer"/>: an SDL_IOStream that can be used to
    /// write input to the process, if it was created with
    /// <see cref="Props.ProcessCreateSTDInNumber"/> set to <see cref="ProcessIO.App"/>.</item>
    /// <item><see cref="Props.ProcessSTDOutPointer"/>: a non-blocking SDL_IOStream that can
    /// be used to read output from the process, if it was created with
    /// <see cref="Props.ProcessCreateSTDOutNumber"/> set to <see cref="ProcessIO.App"/>.</item>
    /// <item><see cref="Props.ProcessSTDErrPointer"/>: a non-blocking SDL_IOStream that can
    /// be used to read error output from the process, if it was created with
    /// <see cref="Props.ProcessCreateSTDErrNumber"/> set to <see cref="ProcessIO.App"/>.</item>
    /// <item><see cref="Props.ProcessBackgroundBoolean"/>: true if the process is running in
    /// the background.</item>
    /// </list>
    /// </summary>
    /// <param name="process">the process to query.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetProcessProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetProcessProperties(IntPtr process);
    
    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_ReadProcess(SDL_Process *process, size_t *datasize, int *exitcode);</code>
    /// <summary>
    /// <para>Read all the output from a process.</para>
    /// <para>If a process was created with I/O enabled, you can use this function to
    /// read the output. This function blocks until the process is complete,
    /// capturing all output, and providing the process exit code.</para>
    /// <para>If a process was created with I/O enabled, you can use this function to
    /// read the output. This function blocks until the process is complete,
    /// capturing all output, and providing the process exit code.</para>
    /// <para>The data should be freed with <see cref="Free"/>.</para>
    /// </summary>
    /// <param name="process">The process to read.</param>
    /// <param name="datasize">a pointer filled in with the number of bytes read, may be
    /// <c>null</c>.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the
    /// process has exited, may be <c>null</c>.</param>
    /// <returns>the data or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="DestroyProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReadProcess"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr ReadProcess(IntPtr process, out UIntPtr datasize, out int exitcode);
    
    
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_GetProcessInput(SDL_Process *process);</code>
    /// <summary>
    /// <para>Get the SDL_IOStream associated with process standard input.</para>
    /// <para>The process must have been created with <see cref="CreateProcess"/> and pipe_stdio
    /// set to true, or with <see cref="CreateProcessWithProperties"/> and
    /// <see cref="Props.ProcessCreateSTDInNumber"/> set to <see cref="ProcessIO.App"/>.</para>
    /// <para>Writing to this stream can return less data than expected if the process
    /// hasn't read its input. It may be blocked waiting for its output to be read,
    /// if so you may need to call <see cref="GetProcessOutput"/> and read the output in
    /// parallel with writing input.</para>
    /// </summary>
    /// <param name="process">The process to get the input stream for.</param>
    /// <returns>the input stream or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessOutput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetProcessInput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetProcessInput(IntPtr process);
    
    
    /// <code>extern SDL_DECLSPEC SDL_IOStream *SDLCALL SDL_GetProcessOutput(SDL_Process *process);</code>
    /// <summary>
    /// <para>Get the SDL_IOStream associated with process standard output.</para>
    /// <para>The process must have been created with <see cref="CreateProcess"/> and pipe_stdio
    /// set to true, or with <see cref="CreateProcessWithProperties"/> and
    /// <see cref="Props.ProcessCreateSTDOutNumber"/> set to <see cref="ProcessIO.App"/>.</para>
    /// <para>Reading from this stream can return 0 with <see cref="GetIOStatus"/> returning
    /// SDL_IO_STATUS_NOT_READY if no output is available yet.</para>
    /// </summary>
    /// <param name="process">The process to get the output stream for.</param>
    /// <returns>the output stream or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="GetProcessInput"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetProcessOutput"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetProcessOutput(IntPtr process);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_KillProcess(SDL_Process *process, bool force);</code>
    /// <summary>
    /// Stop a process.
    /// </summary>
    /// <param name="process">The process to stop.</param>
    /// <param name="force">true to terminate the process immediately, false to try to
    /// stop the process gracefully. In general you should try to stop
    /// the process gracefully first as terminating a process may
    /// leave it with half-written data or in some other unstable
    /// state.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="WaitProcess"/>
    /// <seealso cref="DestroyProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_KillProcess"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool KillProcess(IntPtr process, [MarshalAs(UnmanagedType.I1)] bool force);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_WaitProcess(SDL_Process *process, bool block, int *exitcode);</code>
    /// <summary>
    /// <para>Wait for a process to finish.</para>
    /// <para>This can be called multiple times to get the status of a process.</para>
    /// <para>The exit code will be the exit code of the process if it terminates
    /// normally, a negative signal if it terminated due to a signal, or -255
    /// otherwise. It will not be changed if the process is still running.</para>
    /// <para>If you create a process with standard output piped to the application
    /// (`pipe_stdio` being true) then you should read all of the process output
    /// before calling <see cref="WaitProcess"/>. If you don't do this the process might be
    /// blocked indefinitely waiting for output to be read and <see cref="WaitProcess"/>
    /// will never return true;</para>
    /// </summary>
    /// <param name="process">The process to wait for.</param>
    /// <param name="block">If <c>true</c>, block until the process finishes; otherwise, report
    /// on the process' status.</param>
    /// <param name="exitcode">a pointer filled in with the process exit code if the
    /// process has exited, may be <c>null</c>.</param>
    /// <returns><c>true</c> if the process exited, <c>false</c> otherwise.</returns>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="KillProcess"/>
    /// <seealso cref="DestroyProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_WaitProcess"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool WaitProcess(IntPtr process, [MarshalAs(UnmanagedType.I1)] bool block, out int exitcode);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyProcess(SDL_Process *process);</code>
    /// <summary>
    /// <para>Destroy a previously created process object.</para>
    /// <para>Note that this does not stop the process, just destroys the SDL object used
    /// to track it. If you want to stop the process you should use
    /// <see cref="KillProcess"/>.</para>
    /// </summary>
    /// <param name="process">The process object to destroy.</param>
    /// <threadsafety>This function is not thread safe.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateProcess"/>
    /// <seealso cref="CreateProcessWithProperties"/>
    /// <seealso cref="KillProcess"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyProcess"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyProcess(IntPtr process);
}