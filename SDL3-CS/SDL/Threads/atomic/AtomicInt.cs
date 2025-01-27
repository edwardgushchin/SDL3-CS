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

public static partial class SDL
{
    /// <summary>
    /// <para>A type representing an atomic integer value.</para>
    /// <para>This can be used to manage a value that is synchronized across multiple
    /// CPUs without a race condition; when an app sets a value with
    /// <see cref="SetAtomicInt"/> all other threads, regardless of the CPU it is running on,
    /// will see that value when retrieved with <see cref="GetAtomicInt"/>, regardless of CPU
    /// caches, etc.</para>
    /// <para>This is also useful for atomic compare-and-swap operations: a thread can
    /// change the value as long as its current value matches expectations. When
    /// done in a loop, one can guarantee data consistency across threads without a
    /// lock (but the usual warnings apply: if you don't know what you're doing, or
    /// you don't do it carefully, you can confidently cause any number of
    /// disasters with this, so in most cases, you _should_ use a mutex instead of
    /// this!).</para>
    /// <para>This is a struct so people don't accidentally use numeric operations on it
    /// directly. You have to use SDL atomic functions.</para>
    /// </summary>
    /// <since>This struct is available since SDL 3.2.0</since>
    /// <seealso cref="CompareAndSwapAtomicInt"/>
    /// <seealso cref="GetAtomicInt"/>
    /// <seealso cref="SetAtomicInt"/>
    /// <seealso cref="AddAtomicInt"/>
    [StructLayout(LayoutKind.Sequential)]
    public struct AtomicInt
    {
        public int Value;
    }
}