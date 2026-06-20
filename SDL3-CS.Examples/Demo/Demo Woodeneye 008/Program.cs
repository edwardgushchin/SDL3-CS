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
using SDL3.Examples.Common;

namespace Demo_Woodeneye_008;

internal static class Program
{
    private const int MapBoxScale = 16;
    private const int MapBoxEdgesLength = 12 + (MapBoxScale * 2);
    private const int MaxPlayerCount = 4;
    private const int CircleSides = 32;
    private static readonly Player[] Players = Enumerable.Range(0, MaxPlayerCount).Select(_ => new Player()).ToArray();
    private static readonly float[,] Edges = new float[MapBoxEdgesLength, 6];
    private static int _playerCount = 1;
    private static ulong _past;
    private static ulong _lastFpsTick;
    private static ulong _frames;
    private static string _debugText = "";

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example splitscreen shooter game",
            "com.example.woodeneye-008",
            "examples/demo/woodeneye-008",
            640,
            480,
            RenderFrame,
            Configure,
            cleanup: null,
            HandleEvent,
            presentation: SDL.RendererLogicalPresentation.Disabled);
    }

    private static void Configure(RendererExampleContext context)
    {
        InitPlayers();
        InitEdges();
        SDL.SetRenderVSync(context.Renderer, 0);
        SDL.SetWindowRelativeMouseMode(context.Window, true);
        _past = SDL.GetTicksNS();
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.MouseRemoved:
                for (var i = 0; i < _playerCount; i++)
                {
                    if (Players[i].Mouse == sdlEvent.MDevice.Which)
                    {
                        Players[i].Mouse = 0;
                    }
                }
                break;

            case SDL.EventType.KeyboardRemoved:
                for (var i = 0; i < _playerCount; i++)
                {
                    if (Players[i].Keyboard == sdlEvent.KDevice.Which)
                    {
                        Players[i].Keyboard = 0;
                    }
                }
                break;

            case SDL.EventType.MouseMotion:
                HandleMouseMotion(sdlEvent.Motion);
                break;

            case SDL.EventType.MouseButtonDown:
                Shoot(PlayerIndexForMouse(sdlEvent.Button.Which));
                break;

            case SDL.EventType.KeyDown:
                if (sdlEvent.Key.Key == SDL.Keycode.Escape)
                {
                    return false;
                }

                HandleKey(sdlEvent.Key, true);
                break;

            case SDL.EventType.KeyUp:
                HandleKey(sdlEvent.Key, false);
                break;
        }

        return true;
    }

    private static void HandleMouseMotion(SDL.MouseMotionEvent motion)
    {
        var index = PlayerIndexForMouse(motion.Which);
        var player = Players[index];
        player.Yaw -= (long)(motion.XRel * 0x00080000);
        player.Pitch = Math.Clamp(player.Pitch - (int)(motion.YRel * 0x00080000), -0x40000000, 0x40000000);
    }

    private static void HandleKey(SDL.KeyboardEvent key, bool down)
    {
        var index = PlayerIndexForKeyboard(key.Which);
        var player = Players[index];

        switch (key.Key)
        {
            case SDL.Keycode.W:
                SetMovementBit(player, 1, down);
                break;
            case SDL.Keycode.A:
                SetMovementBit(player, 2, down);
                break;
            case SDL.Keycode.S:
                SetMovementBit(player, 4, down);
                break;
            case SDL.Keycode.D:
                SetMovementBit(player, 8, down);
                break;
            case SDL.Keycode.Space:
                SetMovementBit(player, 16, down);
                break;
        }
    }

    private static void SetMovementBit(Player player, byte bit, bool down)
    {
        if (down)
        {
            player.Wasd |= bit;
        }
        else
        {
            player.Wasd = (byte)(player.Wasd & ~bit);
        }
    }

    private static int PlayerIndexForMouse(uint mouse)
    {
        for (var i = 0; i < _playerCount; i++)
        {
            if (Players[i].Mouse == mouse)
            {
                return i;
            }
        }

        return AssignPlayer(mouse, isMouse: true);
    }

    private static int PlayerIndexForKeyboard(uint keyboard)
    {
        for (var i = 0; i < _playerCount; i++)
        {
            if (Players[i].Keyboard == keyboard)
            {
                return i;
            }
        }

        return AssignPlayer(keyboard, isMouse: false);
    }

    private static int AssignPlayer(uint device, bool isMouse)
    {
        if (device == 0)
        {
            return 0;
        }

        for (var i = 0; i < MaxPlayerCount; i++)
        {
            if ((isMouse && Players[i].Mouse == 0) || (!isMouse && Players[i].Keyboard == 0))
            {
                if (isMouse)
                {
                    Players[i].Mouse = device;
                }
                else
                {
                    Players[i].Keyboard = device;
                }

                _playerCount = Math.Max(_playerCount, i + 1);
                return i;
            }
        }

        return 0;
    }

    private static void Shoot(int shooter)
    {
        if (shooter < 0 || shooter >= _playerCount)
        {
            return;
        }

        var origin = Players[shooter];
        var yawRad = origin.Yaw * Math.PI / 2147483648.0;
        var pitchRad = origin.Pitch * Math.PI / 2147483648.0;
        var vx = -Math.Sin(yawRad) * Math.Cos(pitchRad);
        var vy = Math.Sin(pitchRad);
        var vz = -Math.Cos(yawRad) * Math.Cos(pitchRad);

        for (var i = 0; i < _playerCount; i++)
        {
            if (i == shooter)
            {
                continue;
            }

            var target = Players[i];
            var hit = 0;
            for (var j = 0; j < 2; j++)
            {
                var dx = target.X - origin.X;
                var dy = target.Y - origin.Y + (j == 0 ? 0.0 : target.Radius - target.Height);
                var dz = target.Z - origin.Z;
                var vd = (vx * dx) + (vy * dy) + (vz * dz);
                var dd = (dx * dx) + (dy * dy) + (dz * dz);
                var vv = (vx * vx) + (vy * vy) + (vz * vz);
                var rr = target.Radius * target.Radius;
                if (vd >= 0.0 && (vd * vd) >= vv * (dd - rr))
                {
                    hit++;
                }
            }

            if (hit > 0)
            {
                target.X = MapBoxScale * ((Random.Shared.Next(256) - 128) / 256.0);
                target.Y = MapBoxScale * ((Random.Shared.Next(256) - 128) / 256.0);
                target.Z = MapBoxScale * ((Random.Shared.Next(256) - 128) / 256.0);
            }
        }
    }

    private static void RenderFrame(RendererExampleContext context, double nowSeconds)
    {
        var now = SDL.GetTicksNS();
        var delta = _past == 0 ? 0 : now - _past;
        UpdatePlayers(delta);
        Draw(context.Renderer);

        if (now - _lastFpsTick > 999_999_999UL)
        {
            _lastFpsTick = now;
            _debugText = $"{_frames} fps";
            _frames = 0;
        }

        _past = now;
        _frames++;
    }

    private static void UpdatePlayers(ulong deltaNs)
    {
        foreach (var player in Players.Take(_playerCount))
        {
            const double rate = 6.0;
            var time = deltaNs * 1e-9;
            var drag = Math.Exp(-time * rate);
            var diff = 1.0 - drag;
            var yaw = player.Yaw * Math.PI / 2147483648.0;
            var cos = Math.Cos(yaw);
            var sin = Math.Sin(yaw);
            var dirX = ((player.Wasd & 8) != 0 ? 1.0 : 0.0) - ((player.Wasd & 2) != 0 ? 1.0 : 0.0);
            var dirZ = ((player.Wasd & 4) != 0 ? 1.0 : 0.0) - ((player.Wasd & 1) != 0 ? 1.0 : 0.0);
            var norm = (dirX * dirX) + (dirZ * dirZ);
            var accX = norm == 0.0 ? 0.0 : 60.0 * ((cos * dirX) + (sin * dirZ)) / Math.Sqrt(norm);
            var accZ = norm == 0.0 ? 0.0 : 60.0 * ((-sin * dirX) + (cos * dirZ)) / Math.Sqrt(norm);

            player.VelX -= player.VelX * diff;
            player.VelY -= 25.0 * time;
            player.VelZ -= player.VelZ * diff;
            player.VelX += diff * accX / rate;
            player.VelZ += diff * accZ / rate;
            player.X += ((time - (diff / rate)) * accX / rate) + (diff * player.VelX / rate);
            player.Y += (-0.5 * 25.0 * time * time) + (player.VelY * time);
            player.Z += ((time - (diff / rate)) * accZ / rate) + (diff * player.VelZ / rate);

            var bound = MapBoxScale - player.Radius;
            var x = Math.Clamp(player.X, -bound, bound);
            var y = Math.Clamp(player.Y, player.Height - MapBoxScale, bound);
            var z = Math.Clamp(player.Z, -bound, bound);

            if (player.X != x)
            {
                player.VelX = 0.0;
            }

            if (player.Y != y)
            {
                player.VelY = (player.Wasd & 16) != 0 ? 8.4375 : 0.0;
            }

            if (player.Z != z)
            {
                player.VelZ = 0.0;
            }

            player.X = x;
            player.Y = y;
            player.Z = z;
        }
    }

    private static void Draw(IntPtr renderer)
    {
        if (!SDL.GetRenderOutputSize(renderer, out var w, out var h))
        {
            return;
        }

        SDL.SetRenderDrawColor(renderer, 0, 0, 0, 255);
        SDL.RenderClear(renderer);

        var horizontalParts = _playerCount > 2 ? 2 : 1;
        var verticalParts = _playerCount > 1 ? 2 : 1;
        var partWidth = w / (float)horizontalParts;
        var partHeight = h / (float)verticalParts;

        for (var i = 0; i < _playerCount; i++)
        {
            var player = Players[i];
            var column = i % horizontalParts;
            var row = i / horizontalParts;
            var originX = (column + 0.5f) * partWidth;
            var originY = (row + 0.5f) * partHeight;
            var cameraOrigin = 0.5f * MathF.Sqrt((partWidth * partWidth) + (partHeight * partHeight));
            var clip = new SDL.Rect
            {
                X = (int)(column * partWidth),
                Y = (int)(row * partHeight),
                W = (int)partWidth,
                H = (int)partHeight
            };
            SDL.SetRenderClipRect(renderer, in clip);
            DrawPlayerView(renderer, player, originX, originY, cameraOrigin);
        }

        SDL.SetRenderClipRect(renderer, IntPtr.Zero);
        SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);
        SDL.RenderDebugText(renderer, 0.0f, 0.0f, _debugText);
        SDL.RenderPresent(renderer);
    }

    private static void DrawPlayerView(IntPtr renderer, Player player, float originX, float originY, float cameraOrigin)
    {
        var yawRad = player.Yaw * Math.PI / 2147483648.0;
        var pitchRad = player.Pitch * Math.PI / 2147483648.0;
        var cosYaw = Math.Cos(yawRad);
        var sinYaw = Math.Sin(yawRad);
        var cosPitch = Math.Cos(pitchRad);
        var sinPitch = Math.Sin(pitchRad);
        var mat = new[]
        {
            cosYaw, 0.0, -sinYaw,
            sinYaw * sinPitch, cosPitch, cosYaw * sinPitch,
            sinYaw * cosPitch, -sinPitch, cosYaw * cosPitch
        };

        SDL.SetRenderDrawColor(renderer, 64, 64, 64, 255);
        for (var i = 0; i < MapBoxEdgesLength; i++)
        {
            TransformLine(player, mat, i, out var ax, out var ay, out var az, out var bx, out var by, out var bz);
            DrawClippedSegment(renderer, ax, ay, az, bx, by, bz, originX, originY, cameraOrigin, 1.0f);
        }

        for (var j = 0; j < _playerCount; j++)
        {
            var target = Players[j];
            if (ReferenceEquals(player, target))
            {
                continue;
            }

            SDL.SetRenderDrawColor(renderer, target.Red, target.Green, target.Blue, 255);
            for (var k = 0; k < 2; k++)
            {
                var rx = target.X - player.X;
                var ry = target.Y - player.Y + ((target.Radius - target.Height) * k);
                var rz = target.Z - player.Z;
                var dx = (mat[0] * rx) + (mat[1] * ry) + (mat[2] * rz);
                var dy = (mat[3] * rx) + (mat[4] * ry) + (mat[5] * rz);
                var dz = (mat[6] * rx) + (mat[7] * ry) + (mat[8] * rz);
                if (dz >= 0.0)
                {
                    continue;
                }

                var radius = target.Radius * cameraOrigin / dz;
                DrawCircle(renderer, MathF.Abs((float)radius), (float)(originX - (cameraOrigin * dx / dz)), (float)(originY + (cameraOrigin * dy / dz)));
            }
        }

        SDL.SetRenderDrawColor(renderer, 255, 255, 255, 255);
        SDL.RenderLine(renderer, originX, originY - 10.0f, originX, originY + 10.0f);
        SDL.RenderLine(renderer, originX - 10.0f, originY, originX + 10.0f, originY);
    }

    private static void TransformLine(Player player, double[] mat, int line, out float ax, out float ay, out float az, out float bx, out float by, out float bz)
    {
        ax = (float)((mat[0] * (Edges[line, 0] - player.X)) + (mat[1] * (Edges[line, 1] - player.Y)) + (mat[2] * (Edges[line, 2] - player.Z)));
        ay = (float)((mat[3] * (Edges[line, 0] - player.X)) + (mat[4] * (Edges[line, 1] - player.Y)) + (mat[5] * (Edges[line, 2] - player.Z)));
        az = (float)((mat[6] * (Edges[line, 0] - player.X)) + (mat[7] * (Edges[line, 1] - player.Y)) + (mat[8] * (Edges[line, 2] - player.Z)));
        bx = (float)((mat[0] * (Edges[line, 3] - player.X)) + (mat[1] * (Edges[line, 4] - player.Y)) + (mat[2] * (Edges[line, 5] - player.Z)));
        by = (float)((mat[3] * (Edges[line, 3] - player.X)) + (mat[4] * (Edges[line, 4] - player.Y)) + (mat[5] * (Edges[line, 5] - player.Z)));
        bz = (float)((mat[6] * (Edges[line, 3] - player.X)) + (mat[7] * (Edges[line, 4] - player.Y)) + (mat[8] * (Edges[line, 5] - player.Z)));
    }

    private static void DrawClippedSegment(IntPtr renderer, float ax, float ay, float az, float bx, float by, float bz, float x, float y, float z, float w)
    {
        if (az >= -w && bz >= -w)
        {
            return;
        }

        var dx = ax - bx;
        var dy = ay - by;
        if (az > -w)
        {
            var t = (-w - bz) / (az - bz);
            ax = bx + (dx * t);
            ay = by + (dy * t);
            az = -w;
        }
        else if (bz > -w)
        {
            var t = (-w - az) / (bz - az);
            bx = ax - (dx * t);
            by = ay - (dy * t);
            bz = -w;
        }

        ax = -z * ax / az;
        ay = -z * ay / az;
        bx = -z * bx / bz;
        by = -z * by / bz;
        SDL.RenderLine(renderer, x + ax, y - ay, x + bx, y - by);
    }

    private static void DrawCircle(IntPtr renderer, float radius, float x, float y)
    {
        var points = new SDL.FPoint[CircleSides + 1];
        for (var i = 0; i < points.Length; i++)
        {
            var angle = 2.0f * MathF.PI * i / CircleSides;
            points[i] = new SDL.FPoint { X = x + (radius * MathF.Cos(angle)), Y = y + (radius * MathF.Sin(angle)) };
        }

        SDL.RenderLines(renderer, points, points.Length);
    }

    private static void InitPlayers()
    {
        for (var i = 0; i < MaxPlayerCount; i++)
        {
            var player = Players[i];
            player.X = 8.0 * ((i & 1) != 0 ? -1.0 : 1.0);
            player.Y = 0.0;
            player.Z = 8.0 * ((i & 1) != 0 ? -1.0 : 1.0) * ((i & 2) != 0 ? -1.0 : 1.0);
            player.VelX = player.VelY = player.VelZ = 0.0;
            player.Yaw = 0x20000000L + ((i & 1) != 0 ? 0x80000000L : 0L) + ((i & 2) != 0 ? 0x40000000L : 0L);
            player.Pitch = -0x08000000;
            player.Radius = 0.5;
            player.Height = 1.5;
            player.Wasd = 0;
            player.Mouse = 0;
            player.Keyboard = 0;
            player.Red = (byte)(((1 << (i / 2)) & 2) != 0 ? 0 : 0xFF);
            player.Green = (byte)(((1 << (i / 2)) & 1) != 0 ? 0 : 0xFF);
            player.Blue = (byte)(((1 << (i / 2)) & 4) != 0 ? 0 : 0xFF);

            if ((i & 1) == 0)
            {
                player.Red = (byte)~player.Red;
                player.Green = (byte)~player.Green;
                player.Blue = (byte)~player.Blue;
            }
        }
    }

    private static void InitEdges()
    {
        var map = new[] { 0, 1, 1, 3, 3, 2, 2, 0, 7, 6, 6, 4, 4, 5, 5, 7, 6, 2, 3, 7, 0, 4, 5, 1 };
        for (var i = 0; i < 12; i++)
        {
            for (var j = 0; j < 3; j++)
            {
                Edges[i, j] = (map[(i * 2) + 0] & (1 << j)) != 0 ? MapBoxScale : -MapBoxScale;
                Edges[i, j + 3] = (map[(i * 2) + 1] & (1 << j)) != 0 ? MapBoxScale : -MapBoxScale;
            }
        }

        for (var i = 0; i < MapBoxScale; i++)
        {
            var d = i * 2.0f;
            for (var j = 0; j < 2; j++)
            {
                Edges[i + 12, (3 * j) + 0] = j != 0 ? MapBoxScale : -MapBoxScale;
                Edges[i + 12, (3 * j) + 1] = -MapBoxScale;
                Edges[i + 12, (3 * j) + 2] = d - MapBoxScale;
                Edges[i + 12 + MapBoxScale, (3 * j) + 0] = d - MapBoxScale;
                Edges[i + 12 + MapBoxScale, (3 * j) + 1] = -MapBoxScale;
                Edges[i + 12 + MapBoxScale, (3 * j) + 2] = j != 0 ? MapBoxScale : -MapBoxScale;
            }
        }
    }

    private sealed class Player
    {
        public uint Mouse { get; set; }
        public uint Keyboard { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }
        public double VelX { get; set; }
        public double VelY { get; set; }
        public double VelZ { get; set; }
        public long Yaw { get; set; }
        public int Pitch { get; set; }
        public double Radius { get; set; }
        public double Height { get; set; }
        public byte Red { get; set; }
        public byte Green { get; set; }
        public byte Blue { get; set; }
        public byte Wasd { get; set; }
    }
}
