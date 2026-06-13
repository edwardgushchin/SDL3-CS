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

namespace Demo_Snake;

internal static class Program
{
    private const int StepRateMs = 125;
    private const int BlockSize = 24;
    private const int GameWidth = 24;
    private const int GameHeight = 18;
    private const int WindowWidth = BlockSize * GameWidth;
    private const int WindowHeight = BlockSize * GameHeight;
    private static readonly SnakeContext Snake = new();
    private static IntPtr _joystick;
    private static ulong _lastStep;

    [STAThread]
    private static int Main()
    {
        return RendererExampleRunner.Run(
            "Example Snake game",
            "com.example.snake",
            "examples/demo/snake",
            WindowWidth,
            WindowHeight,
            RenderFrame,
            Configure,
            Cleanup,
            HandleEvent,
            SDL.InitFlags.Video | SDL.InitFlags.Joystick);
    }

    private static void Configure(RendererExampleContext context)
    {
        Snake.Initialize();
        _lastStep = SDL.GetTicks();
    }

    private static bool HandleEvent(SDL.Event sdlEvent)
    {
        switch ((SDL.EventType)sdlEvent.Type)
        {
            case SDL.EventType.JoystickAdded when _joystick == IntPtr.Zero:
                _joystick = SDL.OpenJoystick(sdlEvent.JDevice.Which);
                if (_joystick == IntPtr.Zero)
                {
                    SDL.Log($"Failed to open joystick ID {sdlEvent.JDevice.Which}: {SDL.GetError()}");
                }
                break;

            case SDL.EventType.JoystickRemoved when _joystick != IntPtr.Zero && SDL.GetJoystickID(_joystick) == sdlEvent.JDevice.Which:
                SDL.CloseJoystick(_joystick);
                _joystick = IntPtr.Zero;
                break;

            case SDL.EventType.JoystickHatMotion:
                HandleHat((SDL.JoystickHat)sdlEvent.JHat.Value);
                break;

            case SDL.EventType.KeyDown:
                return HandleKey(sdlEvent.Key.Scancode);
        }

        return true;
    }

    private static bool HandleKey(SDL.Scancode scancode)
    {
        switch (scancode)
        {
            case SDL.Scancode.Escape:
            case SDL.Scancode.Q:
                return false;

            case SDL.Scancode.R:
                Snake.Initialize();
                break;

            case SDL.Scancode.Right:
                Snake.Redirect(SnakeDirection.Right);
                break;

            case SDL.Scancode.Up:
                Snake.Redirect(SnakeDirection.Up);
                break;

            case SDL.Scancode.Left:
                Snake.Redirect(SnakeDirection.Left);
                break;

            case SDL.Scancode.Down:
                Snake.Redirect(SnakeDirection.Down);
                break;
        }

        return true;
    }

    private static void HandleHat(SDL.JoystickHat hat)
    {
        if ((hat & SDL.JoystickHat.Right) != 0)
        {
            Snake.Redirect(SnakeDirection.Right);
        }
        else if ((hat & SDL.JoystickHat.Up) != 0)
        {
            Snake.Redirect(SnakeDirection.Up);
        }
        else if ((hat & SDL.JoystickHat.Left) != 0)
        {
            Snake.Redirect(SnakeDirection.Left);
        }
        else if ((hat & SDL.JoystickHat.Down) != 0)
        {
            Snake.Redirect(SnakeDirection.Down);
        }
    }

    private static void RenderFrame(RendererExampleContext context, double now)
    {
        var ticks = SDL.GetTicks();
        while (ticks - _lastStep >= StepRateMs)
        {
            Snake.Step();
            _lastStep += StepRateMs;
        }

        SDL.SetRenderDrawColor(context.Renderer, 0, 0, 0, 255);
        SDL.RenderClear(context.Renderer);

        var rect = new SDL.FRect { W = BlockSize, H = BlockSize };
        for (var y = 0; y < GameHeight; y++)
        {
            for (var x = 0; x < GameWidth; x++)
            {
                var cell = Snake.CellAt(x, y);
                if (cell == SnakeCell.Nothing)
                {
                    continue;
                }

                rect.X = x * BlockSize;
                rect.Y = y * BlockSize;
                if (cell == SnakeCell.Food)
                {
                    SDL.SetRenderDrawColor(context.Renderer, 80, 80, 255, 255);
                }
                else
                {
                    SDL.SetRenderDrawColor(context.Renderer, 0, 128, 0, 255);
                }

                SDL.RenderFillRect(context.Renderer, in rect);
            }
        }

        SDL.SetRenderDrawColor(context.Renderer, 255, 255, 0, 255);
        rect.X = Snake.HeadX * BlockSize;
        rect.Y = Snake.HeadY * BlockSize;
        SDL.RenderFillRect(context.Renderer, in rect);
        SDL.RenderPresent(context.Renderer);
    }

    private static void Cleanup(RendererExampleContext context)
    {
        if (_joystick != IntPtr.Zero)
        {
            SDL.CloseJoystick(_joystick);
            _joystick = IntPtr.Zero;
        }
    }

    private sealed class SnakeContext
    {
        private readonly SnakeCell[,] _cells = new SnakeCell[GameWidth, GameHeight];
        private int _headX;
        private int _headY;
        private int _tailX;
        private int _tailY;
        private SnakeDirection _nextDirection;
        private int _inhibitTailStep;
        private int _occupiedCells;

        public int HeadX => _headX;

        public int HeadY => _headY;

        public SnakeCell CellAt(int x, int y) => _cells[x, y];

        public void Initialize()
        {
            Array.Clear(_cells);
            _headX = _tailX = GameWidth / 2;
            _headY = _tailY = GameHeight / 2;
            _nextDirection = SnakeDirection.Right;
            _inhibitTailStep = _occupiedCells = 4;
            _occupiedCells--;
            _cells[_tailX, _tailY] = SnakeCell.SRight;

            for (var i = 0; i < 4; i++)
            {
                NewFoodPosition();
                _occupiedCells++;
            }
        }

        public void Redirect(SnakeDirection direction)
        {
            var cell = _cells[_headX, _headY];
            if ((direction == SnakeDirection.Right && cell != SnakeCell.SLeft) ||
                (direction == SnakeDirection.Up && cell != SnakeCell.SDown) ||
                (direction == SnakeDirection.Left && cell != SnakeCell.SRight) ||
                (direction == SnakeDirection.Down && cell != SnakeCell.SUp))
            {
                _nextDirection = direction;
            }
        }

        public void Step()
        {
            var directionAsCell = (SnakeCell)((int)_nextDirection + 1);

            if (--_inhibitTailStep == 0)
            {
                _inhibitTailStep++;
                var tailCell = _cells[_tailX, _tailY];
                _cells[_tailX, _tailY] = SnakeCell.Nothing;

                switch (tailCell)
                {
                    case SnakeCell.SRight:
                        _tailX++;
                        break;
                    case SnakeCell.SUp:
                        _tailY--;
                        break;
                    case SnakeCell.SLeft:
                        _tailX--;
                        break;
                    case SnakeCell.SDown:
                        _tailY++;
                        break;
                }

                WrapAround(ref _tailX, GameWidth);
                WrapAround(ref _tailY, GameHeight);
            }

            var previousX = _headX;
            var previousY = _headY;
            switch (_nextDirection)
            {
                case SnakeDirection.Right:
                    _headX++;
                    break;
                case SnakeDirection.Up:
                    _headY--;
                    break;
                case SnakeDirection.Left:
                    _headX--;
                    break;
                case SnakeDirection.Down:
                    _headY++;
                    break;
            }

            WrapAround(ref _headX, GameWidth);
            WrapAround(ref _headY, GameHeight);

            var targetCell = _cells[_headX, _headY];
            if (targetCell != SnakeCell.Nothing && targetCell != SnakeCell.Food)
            {
                Initialize();
                return;
            }

            _cells[previousX, previousY] = directionAsCell;
            _cells[_headX, _headY] = directionAsCell;
            if (targetCell == SnakeCell.Food)
            {
                if (_occupiedCells == GameWidth * GameHeight)
                {
                    Initialize();
                    return;
                }

                NewFoodPosition();
                _inhibitTailStep++;
                _occupiedCells++;
            }
        }

        private void NewFoodPosition()
        {
            while (true)
            {
                var x = Random.Shared.Next(GameWidth);
                var y = Random.Shared.Next(GameHeight);
                if (_cells[x, y] == SnakeCell.Nothing)
                {
                    _cells[x, y] = SnakeCell.Food;
                    return;
                }
            }
        }

        private static void WrapAround(ref int value, int max)
        {
            if (value < 0)
            {
                value = max - 1;
            }
            else if (value > max - 1)
            {
                value = 0;
            }
        }
    }

    private enum SnakeCell
    {
        Nothing,
        SRight,
        SUp,
        SLeft,
        SDown,
        Food
    }

    private enum SnakeDirection
    {
        Right,
        Up,
        Left,
        Down
    }
}
