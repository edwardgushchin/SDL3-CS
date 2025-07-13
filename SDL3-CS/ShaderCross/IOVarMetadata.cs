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

using System.Runtime.InteropServices;

namespace SDL3;

public partial class ShaderCross
{
    [StructLayout(LayoutKind.Sequential)]
    public struct IOVarMetadata
    {
        /// <summary>
        /// The UTF-8 name of the variable.
        /// </summary>
        public string Name
        {
            get => Marshal.PtrToStringUTF8(name)!;
            set => name = SDL.StringToPointer(value);
        }
        
        private IntPtr name;
        
        /// <summary>
        /// The location of the variable.
        /// </summary>
        public UInt32 Location;
        
        /// <summary>
        /// The vector type of the variable.
        /// </summary>
        public IOVarType VectorType;
        
        /// <summary>
        /// The number of components in the vector type of the variable.
        /// </summary>
        public UInt32 VectorSize;
    }
}