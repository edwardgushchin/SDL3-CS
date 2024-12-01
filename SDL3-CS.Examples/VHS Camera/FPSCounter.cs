#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

namespace VHS_Camera;

public class FPSCounter
{
    private ulong _lastTime = SDL.GetPerformanceCounter();
    private int _frameCount;
    private double _fps;

    public void Update()
    {
        _frameCount++;
        var currentTime = SDL.GetPerformanceCounter();
        var elapsedTime = (currentTime - _lastTime) / (double)SDL.GetPerformanceFrequency();

        if (!(elapsedTime >= 0.1)) return;
        
        _fps = _frameCount / elapsedTime;
        _frameCount = 0;
        _lastTime = currentTime;
    }
    
    public double FPS => _fps;
}