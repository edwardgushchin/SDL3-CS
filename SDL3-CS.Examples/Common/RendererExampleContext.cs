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

using SDL3;

namespace SDL3.Examples.Common;

internal sealed class RendererExampleContext : IDisposable
{
    private bool _disposed;

    private RendererExampleContext(IntPtr window, IntPtr renderer, int width, int height)
    {
        Window = window;
        Renderer = renderer;
        Width = width;
        Height = height;
    }

    public IntPtr Window { get; }

    public IntPtr Renderer { get; }

    public int Width { get; }

    public int Height { get; }

    public static RendererExampleContext Create(
        string appName,
        string appIdentifier,
        string windowTitle,
        int width,
        int height,
        SDL.InitFlags initFlags = SDL.InitFlags.Video,
        SDL.RendererLogicalPresentation presentation = SDL.RendererLogicalPresentation.Letterbox)
    {
        SDL.SetAppMetadata(appName, "1.0", appIdentifier);

        if (!SDL.Init(initFlags))
        {
            throw new InvalidOperationException($"Couldn't initialize SDL: {SDL.GetError()}");
        }

        if (!SDL.CreateWindowAndRenderer(windowTitle, width, height, SDL.WindowFlags.Resizable, out var window, out var renderer))
        {
            SDL.Quit();
            throw new InvalidOperationException($"Couldn't create window/renderer: {SDL.GetError()}");
        }

        SDL.SetRenderLogicalPresentation(renderer, width, height, presentation);

        return new RendererExampleContext(window, renderer, width, height);
    }

    public bool PollEvents(Func<SDL.Event, bool>? handleEvent = null)
    {
        while (SDL.PollEvent(out var sdlEvent))
        {
            if (sdlEvent.Type == (uint)SDL.EventType.Quit)
            {
                return false;
            }

            if (handleEvent?.Invoke(sdlEvent) == false)
            {
                return false;
            }
        }

        return true;
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        if (Renderer != IntPtr.Zero)
        {
            SDL.DestroyRenderer(Renderer);
        }

        if (Window != IntPtr.Zero)
        {
            SDL.DestroyWindow(Window);
        }

        SDL.Quit();
        _disposed = true;
    }
}
