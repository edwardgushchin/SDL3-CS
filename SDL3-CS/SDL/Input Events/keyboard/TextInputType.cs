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
    /// <summary>
    /// <para>Text input type.</para>
    /// <para>These are the valid values for <see cref="Props.TextInputTypeNumber"/>. Not every
    /// value is valid on every platform, but where a value isn't supported, a
    /// reasonable fallback will be used.</para>
    /// </summary>
    /// <since>This enum is available since SDL 3.2.0</since>
    /// <seealso cref="StartTextInputWithProperties"/>
    public enum TextInputType
    {
        /// <summary>
        /// The input is text
        /// </summary>
        Text,
        
        /// <summary>
        /// The input is a person's name
        /// </summary>
        TextName,
        
        /// <summary>
        /// The input is an e-mail address
        /// </summary>
        TextEmail,
        
        /// <summary>
        /// The input is a username
        /// </summary>
        TextUsername,
        
        /// <summary>
        /// The input is a secure password that is hidden
        /// </summary>
        TextPasswordHidden,
        
        /// <summary>
        /// The input is a secure password that is visible
        /// </summary>
        TextPasswordVisible,
        
        /// <summary>
        /// The input is a number
        /// </summary>
        Number,
        
        /// <summary>
        /// The input is a secure PIN that is hidden
        /// </summary>
        NumberPasswordHidden,
        
        /// <summary>
        /// The input is a secure PIN that is visible
        /// </summary>
        NumberPasswordVisible 
    }
}