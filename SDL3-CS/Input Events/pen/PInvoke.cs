#region License
/* SDL3# - C# Wrapper for SDL3
 *
 * Copyright (c) 2024 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you, must not
 * claim that you, wrote the original software. If you, use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Eduard "edwardgushchin" Gushchin <eduardgushchin@yandex.ru>
 *
 */
#endregion

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPens(out int count);
    /// <code>extern SDL_DECLSPEC SDL_PenID *SDLCALL SDL_GetPens(int *count);</code>
    /// <summary>
    /// <para>Retrieves all pens that are connected to the system.</para>
    /// <para>Yields an array of <c>SDL_PenID</c> values. These identify and track pens throughout a session.
    /// To track pens across sessions (program restart), use <see cref="GUID"/>.</para>
    /// </summary>
    /// <param name="count">the number of pens in the array (number of array elements minus 1, i.e.,
    /// not counting the terminator <c>0</c>).</param>
    /// <returns>a <c>0</c> terminated array of <c>SDL_PenID</c> values, or
    /// <c>NULL</c> on error. The array must be freed with <see cref="Free"/>.
    /// On a <c>NULL</c> return, <see cref="GetError"/> is set.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint[]? GetPens(out int count)
    {
        var pArray = SDL_GetPens(out count);

        if (pArray == IntPtr.Zero) return null;
        
        if (count == 0) return [];
        
        try
        {
            var penArray = new int[count];
            Marshal.Copy(pArray, penArray, 0, count);
            return Array.ConvertAll(penArray, item => (uint)item);
        }
        finally
        {
            Free(pArray);
        }
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPenStatus(uint instanceID, out float x, out float y, out float axes,
        ulong numAxes);
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GetPenStatus(SDL_PenID instance_id, float *x, float *y, float *axes, size_t num_axes);</code>
    /// <summary>
    /// <para>Retrieves the pen's current status.</para>
    /// <para>If the pen is detached (cf. <see cref="PenConnected"/>), this operation may return default values.</para>
    /// </summary>
    /// <param name="instanceID">the pen to query.</param>
    /// <param name="x">out-mode parameter for pen x coordinate. May be <c>NULL</c>.</param>
    /// <param name="y">out-mode parameter for pen y coordinate. May be <c>NULL</c>.</param>
    /// <param name="axes">out-mode parameter for axis information. May be <c>null</c>.
    /// The axes are in the same order as <see cref="PenAxis"/>.</param>
    /// <param name="numAxes">maximum number of axes to write to <c>axes</c>.</param>
    /// <returns>a bit mask with the current pen button states (<see cref="ButtonLMask"/> etc.), possibly
    /// <see cref="PenCapabilityFlags.Down"/>, and exactly one of <see cref="PenCapabilityFlags.Ink"/>
    /// or <see cref="PenCapabilityFlags.Eraser"/>, or <c>0</c> on error (see <see cref="GetError"/>).</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetPenStatus(uint instanceID, out float x, out float y, out float axes, ulong numAxes) =>
        SDL_GetPenStatus(instanceID, out x, out y, out axes, numAxes);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetPenFromGUID(GUID guid);
    /// <code>extern SDL_DECLSPEC SDL_PenID SDLCALL SDL_GetPenFromGUID(SDL_GUID guid);</code>
    /// <summary>
    /// <para>Retrieves an <c>SDL_PenID</c> for the given <see cref="GUID"/>.</para>
    /// </summary>
    /// <param name="guid">a pen GUID.</param>
    /// <returns>a valid <c>SDL_PenID</c>, or <see cref="PenInvalid"/> if there is no matching <c>SDL_PenID</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetPenFromGUID(GUID guid) => SDL_GetPenFromGUID(guid);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GUID SDL_GetPenGUID(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_GetPenGUID(SDL_PenID instance_id);</code>
    /// <summary>
    /// <para>Retrieves the <see cref="GUID"/> for a given <c>SDL_PenID</c>.</para>
    /// <para>If <c>instanceID</c> is <see cref="PenInvalid"/>, returns an all-zeroes GUID.</para>
    /// </summary>
    /// <param name="instanceID">the pen to query.</param>
    /// <returns>the instanceID pen <see cref="GUID"/>; persistent across multiple sessions.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static GUID GetPenGUID(uint instanceID) => SDL_GetPenGUID(instanceID);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_PenConnected(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_PenConnected(SDL_PenID instance_id);</code>
    /// <summary>
    /// <para>Checks whether a pen is still attached.</para>
    /// <para>If a pen is detached, it will not show up for <see cref="GetPens"/>. Other operations will still be available but may return default values.</para>
    /// </summary>
    /// <param name="instanceID">a pen ID.</param>
    /// <returns><c>true</c> if <c>instanceID</c> is valid and the corresponding pen is attached, or <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool PenConnected(uint instanceID) => SDL_PenConnected(instanceID);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPenName(uint instanceID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetPenName(SDL_PenID instance_id);</code>
    /// <summary>
    /// <para>Retrieves a human-readable description for a <c>SDL_PenID</c>.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// <para>The string might or might not be localised, depending on platform settings.
    /// It is not guaranteed to be unique; use <see cref="GetPenGUID"/> for (best-effort) unique identifiers.
    /// The pointer is managed by the SDL pen subsystem and must not be deallocated. The pointer remains
    /// valid until SDL is shut down. Returns <c>NULL</c> on error (cf. <see cref="GetError"/>).</para>
    /// </summary>
    /// <param name="instanceID">the pen to query.</param>
    /// <returns>a string that contains the name of the pen, intended for human consumption.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetPenName(uint instanceID) => Marshal.PtrToStringAnsi(SDL_GetPenName(instanceID));
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PenCapabilityFlags SDL_GetPenCapabilities(uint instanceID,
        out PenCapabilityInfo capabilities);
    /// <code>extern SDL_DECLSPEC SDL_PenCapabilityFlags SDLCALL SDL_GetPenCapabilities(SDL_PenID instance_id, SDL_PenCapabilityInfo *capabilities);</code>
    /// <summary>
    /// <para>Retrieves capability flags for a given <c>SDL_PenID</c>.</para>
    /// <para><c>capabilities</c> detail information about pen capabilities, such as the number of buttons.</para>
    /// </summary>
    /// <param name="instanceID">the pen to query.</param>
    /// <param name="capabilities">detail information about pen capabilities, such as the number of buttons.</param>
    /// <returns>a set of capability flags, cf. <see cref="PenCapabilityFlags"/>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static PenCapabilityFlags GetPenCapabilities(uint instanceID, out PenCapabilityInfo capabilities) =>
        SDL_GetPenCapabilities(instanceID, out capabilities);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PenSubtype SDL_GetPenType(uint instanceID);
    /// <code>extern SDL_DECLSPEC SDL_PenSubtype SDLCALL SDL_GetPenType(SDL_PenID instance_id);</code>
    /// <summary>
    /// <para>Retrieves the pen type for a given <c>SDL_PenID</c>.</para>
    /// <para>Note that the pen type does not dictate whether the pen tip is <see cref="PenTips.Ink"/> or
    /// <see cref="PenCapabilityFlags.Eraser"/>; to determine whether a pen is being used for drawing or in eraser mode,
    /// check either the pen tip on <see cref="EventType.PenDown"/>, or the flag <see cref="PenCapabilityFlags.Eraser"/>
    /// in the pen state.</para>
    /// </summary>
    /// <param name="instanceID">the pen to query.</param>
    /// <returns>the corresponding pen type (cf. <see cref="PenSubtype"/>), or <c>0</c> on error.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static PenSubtype GetPenType(uint instanceID) => SDL_GetPenType(instanceID);
}