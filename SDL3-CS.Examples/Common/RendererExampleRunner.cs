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

internal static class RendererExampleRunner
{
    public static int Run(
        string appName,
        string appIdentifier,
        string windowTitle,
        int width,
        int height,
        Action<RendererExampleContext, double> renderFrame,
        Action<RendererExampleContext>? configure = null,
        Action<RendererExampleContext>? cleanup = null,
        Func<SDL.Event, bool>? handleEvent = null,
        SDL.InitFlags initFlags = SDL.InitFlags.Video,
        SDL.RendererLogicalPresentation presentation = SDL.RendererLogicalPresentation.Letterbox)
    {
        RendererExampleContext? context = null;

        try
        {
            context = RendererExampleContext.Create(appName, appIdentifier, windowTitle, width, height, initFlags, presentation);
            configure?.Invoke(context);

            while (context.PollEvents(handleEvent))
            {
                renderFrame(context, SDL.GetTicks() / 1000.0);
                SDL.Delay(1);
            }

            return 0;
        }
        catch (Exception ex)
        {
            SDL.LogError(SDL.LogCategory.Application, ex.Message);
            return 1;
        }
        finally
        {
            if (context is not null)
            {
                cleanup?.Invoke(context);
                context.Dispose();
            }
        }
    }
}
