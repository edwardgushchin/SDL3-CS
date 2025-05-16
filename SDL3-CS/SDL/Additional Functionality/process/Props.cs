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

namespace SDL3;

public static partial class SDL
{
    public static partial class Props
    {
        public const string ProcessCreateArgsPointer = "SDL.process.create.args";
        public const string ProcessCreateEnvironmentPointer = "SDL.process.create.environment";
        public const string ProcessCreateWorkingDirectoryString = "SDL.process.create.working_directory";
        public const string ProcessCreateSTDInNumber = "SDL.process.create.stdin_option";
        public const string ProcessCreateSTDInPointer = "SDL.process.create.stdin_source";
        public const string ProcessCreateSTDOutNumber = "SDL.process.create.stdout_option";
        public const string ProcessCreateSTDOutPointer = "SDL.process.create.stdout_source";
        public const string ProcessCreateSTDErrNumber = "SDL.process.create.stderr_option";
        public const string ProcessCreateSTDErrPointer = "SDL.process.create.stderr_source";
        public const string ProcessCreateSTDErrToSTDOutBoolean = "SDL.process.create.stderr_to_stdout";
        public const string ProcessCreateBackgroundBoolean = "SDL.process.create.background";
        public const string ProcessCreateCMDLineString = "SDL.process.create.cmdline";

        public const string ProcessPIDNumber = "SDL.process.pid";
        public const string ProcessSTDInPointer = "SDL.process.stdin";
        public const string ProcessSTDOutPointer = "SDL.process.stdout";
        public const string ProcessSTDErrPointer = "SDL.process.stderr";
        public const string ProcessBackgroundBoolean = "SDL.process.background";
    }
}