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
        public const string IOStreamWindowsHandlePointer = "SDL.iostream.windows.handle";
        public const string IOStreamSTDIOFilePointer = "SDL.iostream.stdio.file";
        public const string IOStreamFileDescriptorNumber = "SDL.iostream.filede_scriptor";
        public const string IOStreamAndroidAAssetPointer = "SDL.iostream.android.aasset";

        public const string IOStreamMemoryPointer = "SDL.iostream.memory.base";
        public const string IOStreamMemorySizeNumber = "SDL.iostream.memory.size";

        public const string IOStreamDynamicMemoryPointer = "SDL.iostream.dynamic.memory";
        public const string IOStreamDynamicChunkSizeNumber = "SDL.iostream.dynamic.chunksize";

        public const string IOStreamMemoryFreeFuncPointer = "SDL.iostream.memory.free";
    }
}