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

internal sealed class EventMessageLog
{
    private const float MessageLifetimeMs = 3500.0f;
    private readonly SDL.Color[] _colors;
    private readonly List<EventMessage> _messages = [];

    public EventMessageLog(SDL.Color[] colors)
    {
        _colors = colors;
    }

    public void Add(uint deviceId, string text)
    {
        var color = _colors.Length == 0
            ? new SDL.Color { R = 255, G = 255, B = 255, A = 255 }
            : _colors[deviceId % _colors.Length];

        _messages.Add(new EventMessage(deviceId, text, color, SDL.GetTicks()));
    }

    public void Render(IntPtr renderer, int width, int height)
    {
        var now = SDL.GetTicks();
        var previousY = 0.0f;

        for (var i = 0; i < _messages.Count;)
        {
            var message = _messages[i];
            var lifePercent = (float)(now - message.StartTicks) / MessageLifetimeMs;

            if (lifePercent >= 1.0f)
            {
                _messages.RemoveAt(i);
                continue;
            }

            var y = height * lifePercent;
            if (previousY != 0.0f && previousY - y < SDL.DebugTextFontCharacterSize)
            {
                message.StartTicks = now;
                break;
            }

            var x = Math.Max(0.0f, (width - (message.Text.Length * SDL.DebugTextFontCharacterSize)) / 2.0f);
            var alpha = (byte)Math.Clamp(message.Color.A * (1.0f - lifePercent), 0.0f, 255.0f);

            SDL.SetRenderDrawColor(renderer, message.Color.R, message.Color.G, message.Color.B, alpha);
            SDL.RenderDebugText(renderer, x, y, message.Text);

            previousY = y;
            i++;
        }
    }

    private sealed class EventMessage
    {
        public EventMessage(uint deviceId, string text, SDL.Color color, ulong startTicks)
        {
            DeviceId = deviceId;
            Text = text;
            Color = color;
            StartTicks = startTicks;
        }

        public uint DeviceId { get; }

        public string Text { get; }

        public SDL.Color Color { get; }

        public ulong StartTicks { get; set; }
    }
}
