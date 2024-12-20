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

namespace SDL3;

public static partial class SDL
{
    /// <code>#define SDL_AtomicIncRef(a)    SDL_AddAtomicInt(a, 1)</code>
    /// <summary>
    /// <para>Increment an atomic variable used as a reference count.</para>
    /// <para><b>Note: If you don't know what this macro is for, you shouldn't use it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an SDL_AtomicInt to increment.</param>
    /// <returns>the previous value of the atomic variable.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.1.3.</since>
    /// <seealso cref="AtomicDecRef"/>
    public static int AtomicIncRef(ref AtomicInt a) => AddAtomicInt(ref a, 1);
    
    
    /// <code>#define SDL_AtomicDecRef(a)    (SDL_AddAtomicInt(a, -1) == 1)</code>
    /// <summary>
    /// <para>Decrement an atomic variable used as a reference count.</para>
    /// <para><b>Note: If you don't know what this macro is for, you shouldn't use it!</b></para>
    /// </summary>
    /// <param name="a">a pointer to an <see cref="AtomicInt"/> to increment.</param>
    /// <returns><c>true</c> if the variable reached zero after decrementing, <c>false</c>
    /// otherwise.</returns>
    /// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
    /// <since>This macro is available since SDL 3.1.3.</since>
    /// <seealso cref="AtomicIncRef"/>
    [Macro]
    public static bool AtomicDecRef(ref AtomicInt a) => AddAtomicInt(ref a, -1) == 1;
}