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
    public struct SPIRVInfo : IDisposable
    {
        /// <summary>
        /// The SPIRV bytecode.
        /// </summary>
        public IntPtr ByteCode;
        
        /// <summary>
        /// The length of the SPIRV bytecode.
        /// </summary>
        public UIntPtr ByteCodeSize;

        private IntPtr entrypoint;
        
        /// <summary>
        /// The entry point function name for the shader in UTF-8.
        /// </summary>
        public string Entrypoint
        {
            get => Marshal.PtrToStringUTF8(entrypoint)!;
            set => entrypoint = SDL.StringToPointer(value);
        }

        /// <summary>
        /// The shader stage to transpile the shader with.
        /// </summary>
        public ShaderStage ShaderStage;

        private byte enableDebug;

        /// <summary>
        /// Allows debug info to be emitted when relevant. Can be useful for graphics debuggers like RenderDoc.
        /// </summary>
        public bool EnableDebug => Convert.ToBoolean(enableDebug);

        private IntPtr name;
        
        /// <summary>
        /// A UTF-8 name to associate with the shader. Optional, can be NULL.
        /// </summary>
        public string? Name
        {
            get => Marshal.PtrToStringUTF8(name);
            set => name = SDL.StringToPointer(value);
        }

        /// <summary>
        /// A properties ID for extensions. Should be 0 if no extensions are needed.
        /// </summary>
        public uint Props;


        public void Dispose()
        {
            Marshal.FreeHGlobal(entrypoint);
            Marshal.FreeHGlobal(name);
        }
    }
}