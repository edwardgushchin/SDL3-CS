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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockJoysticks(void) SDL_ACQUIRE(SDL_joystick_lock);</code>
	/// <summary>
	/// <para>Locking for atomic access to the joystick API.</para>
	/// <para>The SDL joystick functions are thread-safe, however you can lock the
	/// joysticks while processing to guarantee that the joystick list won't change
	/// and joystick and gamepad events will not be delivered.</para>
	/// </summary>
	/// <threadsafety>This should be called from the same thread that called
	/// <see cref="LockJoysticks"/>.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_LockJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void LockJoysticks();
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockJoysticks(void) SDL_RELEASE(SDL_joystick_lock);</code>
	/// <summary>
	/// Unlocking for atomic access to the joystick API.
	/// </summary>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void UnlockJoysticks();
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasJoystick(void);</code>
	/// <summary>
	/// Return whether a joystick is currently connected.
	/// </summary>
	/// <returns><c>true</c> if a joystick is connected, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_HasJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool HasJoystick();
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoysticks(out int count);
	/// <code>extern SDL_DECLSPEC SDL_JoystickID * SDLCALL SDL_GetJoysticks(int *count);</code>
	/// <summary>
	/// Get a list of currently connected joysticks.
	/// </summary>
	/// <param name="count">a pointer filled in with the number of joysticks returned, may
	/// be <c>null</c>.</param>
	/// <returns>a 0 terminated array of joystick instance IDs or <c>null</c> on failure;
	/// call <see cref="GetError"/> for more information. This should be freed
	/// with <see cref="Free"/> when it is no longer needed.</returns>
	/// 
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="HasJoystick"/>
	/// <seealso cref="OpenJoystick"/>
	public static uint[]? GetJoysticks(out int count)
	{
		var ptr = SDL_GetJoysticks(out count);

		try
		{
			return PointerToStructureArray<uint>(ptr, count);
		}
		finally
		{
			Free(ptr);
		}
	}
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickNameForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickNameForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation dependent name of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the name of the selected joystick. If no name can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickName"/>
	/// <seealso cref="GetJoysticks"/>
	public static string? GetJoystickNameForID(uint instanceId)
    {
        var value = SDL_GetJoystickNameForID(instanceId); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPathForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPathForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickPathForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation dependent path of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the path of the selected joystick. If no path can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickPath"/>
	/// <seealso cref="GetJoysticks"/>
	public static string? GetJoystickPathForID(uint instanceId)
	{
		var value = SDL_GetJoystickPathForID(instanceId); 
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickPlayerIndexForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the player index of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the player index of a joystick, or -1 if it's not available.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPlayerIndexForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetJoystickPlayerIndexForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_GetJoystickGUIDForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation-dependent GUID of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the GUID of the selected joystick. If called with an invalid
	/// <c>instanceId</c>, this function returns a zero GUID.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickGUID"/>
	/// <seealso cref="GUIDToString"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUIDForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial GUID GetJoystickGUIDForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickVendorForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the USB vendor ID of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the vendor ID isn't
	/// available this function returns 0.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the USB vendor ID of the selected joystick. If called with an
	/// invalid instance_id, this function returns 0.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickVendor"/>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickVendorForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickVendorForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the USB product ID of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the product ID isn't
	/// available this function returns 0.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the USB product ID of the selected joystick. If called with an
	/// invalid <c>instanceId</c>, this function returns 0.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickProduct"/>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickProductForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductVersionForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the product version of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the product version
	/// isn't available this function returns 0.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the product version of the selected joystick. If called with an
	/// invalid <c>instanceId</c>, this function returns 0.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickProductVersion"/>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductVersionForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickProductVersionForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC SDL_JoystickType SDLCALL SDL_GetJoystickTypeForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the type of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>the <see cref="JoystickType"/> of the selected joystick. If called with an
	/// invalid <c>instanceId</c>, this function returns
	/// <see cref="JoystickType.Unknown"/>.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickType"/>
	/// <seealso cref="GetJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial JoystickType GetJoystickTypeForID(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC SDL_Joystick * SDLCALL SDL_OpenJoystick(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Open a joystick for use.</para>
	/// <para>The joystick subsystem must be initialized before a joystick can be opened
	/// for use.</para>
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns>a joystick identifier or <c>null</c> on failure; call <see cref="GetError"/> for
	/// more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="CloseJoystick"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr OpenJoystick(uint instanceId);


	/// <code>extern SDL_DECLSPEC SDL_Joystick * SDLCALL SDL_GetJoystickFromID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the SDL_Joystick associated with an instance ID, if it has been opened.</para>
	/// </summary>
	/// <param name="instanceId">the instance ID to get the SDL_Joystick for.</param>
	/// <returns>an SDL_Joystick on success or <c>null</c> on failure or if it hasn't been
	/// opened yet; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr GetJoystickFromID(uint instanceId);


	/// <code>extern SDL_DECLSPEC SDL_Joystick * SDLCALL SDL_GetJoystickFromPlayerIndex(int player_index);</code>
	/// <summary>
	/// Get the SDL_Joystick associated with a player index.
	/// </summary>
	/// <param name="playerIndex">the player index to get the SDL_Joystick for.</param>
	/// <returns>an SDL_Joystick on success or <c>null</c> on failure; call <see cref="GetError"/>
	/// for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	/// <seealso cref="SetJoystickPlayerIndex"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFromPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial IntPtr GetJoystickFromPlayerIndex(int playerIndex);


	/// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_AttachVirtualJoystick(const SDL_VirtualJoystickDesc *desc);</code>
	/// <summary>
	/// Attach a new virtual joystick.
	/// <para>Apps can create virtual joysticks, that exist without hardware directly
	/// backing them, and have program-supplied inputs. Once attached, a virtual
	/// joystick looks like any other joystick that SDL can access. These can be
	/// used to make other things look like joysticks, or provide pre-recorded
	/// input, etc.</para>
	/// <para>Once attached, the app can send joystick inputs to the new virtual joystick
	/// using <see cref="SetJoystickVirtualAxis"/>, etc.</para>
	/// <para>When no longer needed, the virtual joystick can be removed by calling
	/// <see cref="DetachVirtualJoystick"/>.</para>
	/// </summary>
	/// <param name="desc">joystick description, initialized using <see cref="InitInterface(ref VirtualJoystickDesc)"/>.</param>
	/// <returns>the joystick instance ID, or 0 on failure; call <see cref="GetError"/> for
	/// more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="DetachVirtualJoystick"/>
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	/// <seealso cref="SetJoystickVirtualHat"/>
	/// <seealso cref="SetJoystickVirtualTouchpad"/>
	/// <seealso cref="SetJoystickVirtualSensorData"/>
	[DllImport(SDLLibrary, EntryPoint = "SDL_AttachVirtualJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static extern uint AttachVirtualJoystick(in VirtualJoystickDesc desc);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_DetachVirtualJoystick(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// Detach a virtual joystick.
	/// </summary>
	/// <param name="instanceId">the joystick instance ID, previously returned from
	/// <see cref="AttachVirtualJoystick"/>.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="AttachVirtualJoystick"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_DetachVirtualJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool DetachVirtualJoystick(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsJoystickVirtual(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// Query whether or not a joystick is virtual.
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns><c>true</c> if the joystick is virtual, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_IsJoystickVirtual"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool IsJoystickVirtual(uint instanceId);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualAxis(SDL_Joystick *joystick, int axis, Sint16 value);</code>
	/// <summary>
	/// <para>Set the state of an axis on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// <para>Note that when sending trigger axes, you should scale the value to the full
	/// range of Sint16. For example, a trigger at rest would have the value of
	/// <see cref="JoystickAxisMin"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="axis">the index of the axis on the virtual joystick to update.</param>
	/// <param name="value">the new value for the specified axis.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <sealso cref="SetJoystickVirtualButton"/>
	/// <sealso cref="SetJoystickVirtualBall"/>
	/// <sealso cref="SetJoystickVirtualHat"/>
	/// <sealso cref="SetJoystickVirtualTouchpad"/>
	/// <sealso cref="SetJoystickVirtualSensorData"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualBall(SDL_Joystick *joystick, int ball, Sint16 xrel, Sint16 yrel);</code>
	/// <summary>
	/// <para>Generate ball motion on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// SDL_WaitEvent.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="ball">the index of the ball on the virtual joystick to update.</param>
	/// <param name="xrel">the relative motion on the X axis.</param>
	/// <param name="yrel">the relative motion on the Y axis.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualHat"/>
	/// <seealso cref="SetJoystickVirtualTouchpad"/>
	/// <seealso cref="SetJoystickVirtualSensorData"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualBall"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualButton(SDL_Joystick *joystick, int button, bool down);</code>
	/// <summary>
	/// <para>Set the state of a button on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="button">the index of the button on the virtual joystick to update.</param>
	/// <param name="down"><c>true</c> if the button is pressed, <c>false</c> otherwise.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	///	<seealso cref="SetJoystickVirtualHat"/>
	///	<seealso cref="SetJoystickVirtualTouchpad"/>
	///	<seealso cref="SetJoystickVirtualSensorData"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickVirtualButton(IntPtr joystick, int button, [MarshalAs(UnmanagedType.I1)] bool down);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualHat(SDL_Joystick *joystick, int hat, Uint8 value);</code>
	/// <summary>
	/// <para>Set the state of a hat on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="hat">the index of the hat on the virtual joystick to update.</param>
	/// <param name="value">the new value for the specified hat.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	/// <seealso cref="SetJoystickVirtualTouchpad"/>
	/// <seealso cref="SetJoystickVirtualSensorData"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualHat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickVirtualHat(IntPtr joystick, int hat, JoystickHat value);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualTouchpad(SDL_Joystick *joystick, int touchpad, int finger, bool down, float x, float y, float pressure);</code>
	/// <summary>
	/// <para>Set touchpad finger state on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="touchpad">the index of the touchpad on the virtual joystick to
	/// update.</param>
	/// <param name="finger">the index of the finger on the touchpad to set.</param>
	/// <param name="down"><c>true</c> if the finger is pressed, <c>false</c> if the finger is released.</param>
	/// <param name="x">the x coordinate of the finger on the touchpad, normalized 0 to 1,
	/// with the origin in the upper left.</param>
	/// <param name="y">the y coordinate of the finger on the touchpad, normalized 0 to 1,
	/// with the origin in the upper left.</param>
	/// <param name="pressure">the pressure of the finger.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <sealso cref="SetJoystickVirtualAxis"/>
	/// <sealso cref="SetJoystickVirtualButton"/>
	/// <sealso cref="SetJoystickVirtualBall"/>
	/// <sealso cref="SetJoystickVirtualHat"/>
	/// <sealso cref="SetJoystickVirtualSensorData"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualTouchpad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, [MarshalAs(UnmanagedType.I1)] bool down, float x, float y, float pressure);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendJoystickVirtualSensorData(SDL_Joystick *joystick, SDL_SensorType type, Uint64 sensor_timestamp, const float *data, int num_values);</code>
	/// <summary>
	/// <para>Send a sensor update for an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="type">the type of the sensor on the virtual joystick to update.</param>
	/// <param name="sensorTimestamp">a 64-bit timestamp in nanoseconds associated with
	/// the sensor reading.</param>
	/// <param name="data">the data associated with the sensor reading.</param>
	/// <param name="numValues">the number of values pointed to by <c>data</c>.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <sealso cref="SetJoystickVirtualAxis"/>
	/// <sealso cref="SetJoystickVirtualButton"/>
	/// <sealso cref="SetJoystickVirtualBall"/>
	/// <sealso cref="SetJoystickVirtualHat"/>
	/// <sealso cref="SetJoystickVirtualTouchpad"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickVirtualSensorData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] float[] data, int numValues);
	
	
	/// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetJoystickProperties(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the properties associated with a joystick.</para>
	/// <para>The following read-only properties are provided by SDL:</para>
	/// <list type="bullet">
	///	<item><see cref="Props.JoystickCapMonoLedBoolean"/>: true if this joystick has an
	/// LED that has adjustable brightness</item>
	/// <item><see cref="Props.JoystickCapRGBLedBoolean"/>: true if this joystick has an LED
	/// that has adjustable color</item>
	/// <item><see cref="Props.JoystickCapPlayerLedBoolean"/>: true if this joystick has a
	/// player LED</item>
	/// <item><see cref="Props.JoystickCapRumbleBoolean"/>: true if this joystick has
	/// left/right rumble</item>
	/// <item><see cref="Props.JoystickCapTriggerRumbleBoolean"/>: true if this joystick has
	/// simple trigger rumble</item>
	/// </list>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>a valid property ID on success or 0 on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial uint GetJoystickProperties(IntPtr joystick);
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickName(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickName(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the implementation dependent name of a joystick.
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the name of the selected joystick. If no name can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickNameForID"/>
	public static string? GetJoystickName(IntPtr joystick)
	{
		var value = SDL_GetJoystickName(joystick); 
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPath(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickPath(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the implementation dependent path of a joystick.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the path of the selected joystick. If no path can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickPathForID"/>
	public static string? GetJoystickPath(IntPtr joystick)
	{
		var value = SDL_GetJoystickPath(joystick); 
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickPlayerIndex(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the player index of an opened joystick.</para>
	/// <para>For XInput controllers this returns the XInput user index. Many joysticks
	/// will not be able to supply this information.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the player index, or -1 if it's not available.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="SetJoystickPlayerIndex"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetJoystickPlayerIndex(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickPlayerIndex(SDL_Joystick *joystick, int player_index);</code>
	/// <summary>
	/// Set the player index of an opened joystick.
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <param name="playerIndex">player index to assign to this joystick, or -1 to clear
	/// the player index and turn off player LEDs.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickPlayerIndex(IntPtr joystick, int playerIndex);
	
	
	/// <code>extern SDL_DECLSPEC SDL_GUID SDLCALL SDL_GetJoystickGUID(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the implementation-dependent GUID for the joystick.</para>
	/// <para>This function requires an open joystick.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the <see cref="GUID"/> of the given joystick. If called on an invalid index,
	/// this function returns a zero <see cref="GUID"/>; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickGUIDForID"/>
	/// <seealso cref="GUIDToString"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial GUID GetJoystickGUID(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickVendor(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the USB vendor ID of an opened joystick, if available.</para>
	/// <para>If the vendor ID isn't available this function returns 0.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the USB vendor ID of the selected joystick, or 0 if unavailable.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickVendorForID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickVendor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickVendor(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProduct(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the USB product ID of an opened joystick, if available.</para>
	/// <para>If the product ID isn't available this function returns 0.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the USB product ID of the selected joystick, or 0 if unavailable.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickProductForID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProduct"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickProduct(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductVersion(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the product version of an opened joystick, if available.</para>
	/// <para>If the product version isn't available this function returns 0.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the product version of the selected joystick, or 0 if unavailable.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickProductVersionForID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickProductVersion(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickFirmwareVersion(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the firmware version of an opened joystick, if available.</para>
	/// <para>If the firmware version isn't available this function returns 0.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the firmware version of the selected joystick, or 0 if
	/// unavailable.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFirmwareVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial ushort GetJoystickFirmwareVersion(IntPtr joystick);
	
	
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickSerial"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickSerial(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickSerial(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the serial number of an opened joystick, if available.</para>
	/// <para>Returns the serial number of the joystick, or <c>null</c> if it is not available.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the serial number of the selected joystick, or <c>null</c> if
	/// unavailable.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static string? GetJoystickSerial(IntPtr joystick)
	{
		var value = SDL_GetJoystickSerial(joystick); 
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}
	
	
	/// <code>extern SDL_DECLSPEC SDL_JoystickType SDLCALL SDL_GetJoystickType(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the type of an opened joystick.
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the <see cref="JoystickType"/> of the selected joystick.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickTypeForID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial JoystickType GetJoystickType(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetJoystickGUIDInfo(SDL_GUID guid, Uint16 *vendor, Uint16 *product, Uint16 *version, Uint16 *crc16);</code>
	/// <summary>
	/// Get the device information encoded in a <see cref="GUID"/> structure.
	/// </summary>
	/// <param name="guid">the <see cref="GUID"/> you wish to get info about.</param>
	/// <param name="vendor">a pointer filled in with the device VID, or 0 if not
	/// available.</param>
	/// <param name="product">a pointer filled in with the device PID, or 0 if not
	/// available.</param>
	/// <param name="version">a pointer filled in with the device version, or 0 if not
	/// available.</param>
	/// <param name="crc16">a pointer filled in with a CRC used to distinguish different
	/// products with the same VID/PID, or 0 if not available.</param>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickGUIDForID"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUIDInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void GetJoystickGUIDInfo(GUID guid, out short vendor, out short product, out short version, out short crc16);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_JoystickConnected(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the status of a specified joystick.
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <returns><c>true</c> if the joystick has been opened, <c>false</c> if it has not; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_JoystickConnected"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool JoystickConnected(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_GetJoystickID(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the instance ID of an opened joystick.
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the instance ID of the specified joystick on success or 0 on
	/// failure; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial uint GetJoystickID(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickAxes(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the number of general axis controls on a joystick.</para>
	/// <para>Often, the directional pad on a game controller will either look like 4
	/// separate buttons or a POV hat, and not axes, but all of this is up to the
	/// device and platform.</para>
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the number of axis controls/number of axes on success or -1 on
	/// failure; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickAxis"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	/// <seealso cref="GetNumJoystickHats"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickAxes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetNumJoystickAxes(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickBalls(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the number of trackballs on a joystick.</para>
	/// <para>Joystick trackballs have only relative motion events associated with them
	/// and their state cannot be polled.</para>
	/// <para>Most joysticks do not have trackballs.</para>
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the number of trackballs on success or -1 on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickBall"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	/// <seealso cref="GetNumJoystickHats"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickBalls"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetNumJoystickBalls(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickHats(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the number of POV hats on a joystick.
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the number of POV hats on success or -1 on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickHat"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickHats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetNumJoystickHats(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickButtons(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the number of buttons on a joystick.
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the number of buttons on success or -1 on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickButton"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickHats"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickButtons"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial int GetNumJoystickButtons(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetJoystickEventsEnabled(bool enabled);</code>
	/// <summary>
	/// <para>Set the state of joystick event processing.</para>
	/// <para>If joystick events are disabled, you must call <see cref="UpdateJoysticks"/>
	/// yourself and check the state of the joystick when you want joystick
	/// information.</para>
	/// </summary>
	/// <param name="enabled">whether to process joystick events or not.</param>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="JoystickEventsEnabled"/>
	/// <seealso cref="UpdateJoysticks"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void SetJoystickEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_JoystickEventsEnabled(void);</code>
	/// <summary>
	/// <para>Query the state of joystick event processing.</para>
	/// <para>If joystick events are disabled, you must call <see cref="UpdateJoysticks"/>
	/// yourself and check the state of the joystick when you want joystick
	/// information.</para>
	/// </summary>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <returns><c>true</c> if joystick events are being processed, <c>false</c> otherwise.</returns>
	/// <seealso cref="SetJoystickEventsEnabled"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_JoystickEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool JoystickEventsEnabled();
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateJoysticks(void);</code>
	/// <summary>
	/// <para>Update the current state of the open joysticks.</para>
	/// <para>This is called automatically by the event loop if any joystick events are
	/// enabled.</para>
	/// </summary>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void UpdateJoysticks();
	
	
	/// <code>extern SDL_DECLSPEC Sint16 SDLCALL SDL_GetJoystickAxis(SDL_Joystick *joystick, int axis);</code>
	/// <summary>
	/// <para>Get the current state of an axis control on a joystick.</para>
	/// <para>SDL makes no promises about what part of the joystick any given axis refers
	/// to. Your game should have some sort of configuration UI to let users
	/// specify what each axis should be bound to. Alternately, SDL's higher-level
	/// Game Controller API makes a great effort to apply order to this lower-level
	/// interface, so you know that a specific axis is the "left thumb stick," etc.</para>
	/// <para>The value returned by <see cref="GetJoystickAxis"/> is a signed integer (-32768 to
	/// 32767) representing the current position of the axis. It may be necessary
	/// to impose certain tolerances on these values to account for jitter.</para>
	/// </summary>
	/// <param name="joystick"> an SDL_Joystick structure containing joystick information.</param>
	/// <param name="axis">the axis to query; the axis indices start at index 0.</param>
	/// <returns>a 16-bit signed integer representing the current position of the
	/// axis or 0 on failure; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetNumJoystickAxes"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial short GetJoystickAxis(IntPtr joystick, int axis);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetJoystickAxisInitialState(SDL_Joystick *joystick, int axis, Sint16 *state);</code>
	/// <summary>
	/// <para>Get the initial state of an axis control on a joystick.</para>
	/// <para>The state is a value ranging from -32768 to 32767.</para>
	/// <para>The axis indices start at index 0.</para>
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <param name="axis">the axis to query; the axis indices start at index 0.</param>
	/// <param name="state">upon return, the initial value is supplied here.</param>
	/// <returns><c>true</c> if this axis has any initial value, or <c>false</c> if not.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickAxisInitialState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);
	
	
	//extern SDL_DECLSPEC bool SDLCALL SDL_GetJoystickBall(SDL_Joystick *joystick, int ball, int *dx, int *dy);
	/// <summary>
	/// <para>Get the ball axis change since the last poll.</para>
	/// <para>Trackballs can only return relative motion since the last call to
	/// <see cref="GetJoystickBall"/>, these motion deltas are placed into <c>dx</c> and <c>dy</c>.</para>
	/// <para>Most joysticks do not have trackballs.</para>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick to query.</param>
	/// <param name="ball">the ball index to query; ball indices start at index 0.</param>
	/// <param name="dx">stores the difference in the x axis position since the last poll.</param>
	/// <param name="dy">stores the difference in the y axis position since the last poll.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetNumJoystickBalls"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickBall"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);
	
	
	/// <code>extern SDL_DECLSPEC Uint8 SDLCALL SDL_GetJoystickHat(SDL_Joystick *joystick, int hat);</code>
	/// <summary>
	/// <para>Get the current state of a POV hat on a joystick.</para>
	/// <para>The returned value will be one of the <c>SDL_HAT_*</c> values.</para>
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <param name="hat">the hat index to get the state from; indices start at index 0.</param>
	/// <returns>the current hat position.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetNumJoystickHats"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickHat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial JoystickHat GetJoystickHat(IntPtr joystick, int hat);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetJoystickButton(SDL_Joystick *joystick, int button);</code>
	/// <summary>
	/// Get the current state of a button on a joystick.
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <param name="button">the button index to get the state from; indices start at
	/// index 0.</param>
	/// <returns><c>true</c> if the button is pressed, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetNumJoystickButtons"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool GetJoystickButton(IntPtr joystick, int button);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RumbleJoystick(SDL_Joystick *joystick, Uint16 low_frequency_rumble, Uint16 high_frequency_rumble, Uint32 duration_ms);</code>
	/// <summary>
	/// <para>Start a rumble effect.</para>
	/// <para>Each call to this function cancels any previous rumble effect, and calling
	/// it with 0 intensity stops any rumbling.</para>
	/// <para>This function requires you to process SDL events or call
	/// <see cref="UpdateJoysticks"/> to update rumble state.</para>
	/// </summary>
	/// <param name="joystick">the joystick to vibrate.</param>
	/// <param name="lowFrequencyRumble">the intensity of the low frequency (left)
	/// rumble motor, from 0 to 0xFFFF.</param>
	/// <param name="highFrequencyRumble">the intensity of the high frequency (right)
	/// rumble motor, from 0 to 0xFFFF.</param>
	/// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
	/// <returns><c>true</c>, or <c>false</c> if rumble isn't supported on this joystick.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool RumbleJoystick(IntPtr joystick, short lowFrequencyRumble, short highFrequencyRumble, int durationMs);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RumbleJoystickTriggers(SDL_Joystick *joystick, Uint16 left_rumble, Uint16 right_rumble, Uint32 duration_ms);</code>
	/// <summary>
	/// <para>Start a rumble effect in the joystick's triggers.</para>
	/// <para>Each call to this function cancels any previous trigger rumble effect, and
	/// calling it with 0 intensity stops any rumbling.</para>
	/// <para>Note that this is rumbling of the _triggers_ and not the game controller as
	/// a whole. This is currently only supported on Xbox One controllers. If you
	/// want the (more common) whole-controller rumble, use <see cref="RumbleJoystick"/>
	/// instead.</para>
	/// <para>This function requires you to process SDL events or call
	/// <see cref="UpdateJoysticks"/> to update rumble state.</para>
	/// </summary>
	/// <param name="joystick">the joystick to vibrate.</param>
	/// <param name="leftRumble">the intensity of the left trigger rumble motor, from 0
	/// to 0xFFFF.</param>
	/// <param name="rightRumble">the intensity of the right trigger rumble motor, from 0
	/// to 0xFFFF.</param>
	/// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
	/// <returns><c>true on</c> success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="RumbleJoystick"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleJoystickTriggers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool RumbleJoystickTriggers(IntPtr joystick, short leftRumble, short rightRumble, int durationMs);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickLED(SDL_Joystick *joystick, Uint8 red, Uint8 green, Uint8 blue);</code>
	/// <summary>
	/// <para>Update a joystick's LED color.</para>
	/// <para>An example of a joystick LED is the light on the back of a PlayStation 4's
	/// DualShock 4 controller.</para>
	/// <para>For joysticks with a single color LED, the maximum of the RGB values will
	/// be used as the LED brightness.</para>
	/// </summary>
	/// <param name="joystick">the joystick to update.</param>
	/// <param name="red">the intensity of the red LED.</param>
	/// <param name="green">the intensity of the green LED.</param>
	/// <param name="blue">the intensity of the blue LED.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickLED"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendJoystickEffect(SDL_Joystick *joystick, const void *data, int size);</code>
	/// <summary>
	/// Send a joystick specific effect packet.
	/// </summary>
	/// <param name="joystick">the joystick to affect.</param>
	/// <param name="data">the data to send to the joystick.</param>
	/// <param name="size">the size of the data to send to the joystick.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SendJoystickEffect(IntPtr joystick, IntPtr data, int size);
	
	
	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendJoystickEffect(SDL_Joystick *joystick, const void *data, int size);</code>
	/// <summary>
	/// Send a joystick specific effect packet.
	/// </summary>
	/// <param name="joystick">the joystick to affect.</param>
	/// <param name="data">the data to send to the joystick.</param>
	/// <param name="size">the size of the data to send to the joystick.</param>
	/// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
	/// information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	public static partial bool SendJoystickEffect(IntPtr joystick, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int size);
	
	
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseJoystick(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Close a joystick previously opened with <see cref="OpenJoystick"/>.
	/// </summary>
	/// <param name="joystick">the joystick device to close.</param>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="OpenJoystick"/>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial void CloseJoystick(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC SDL_JoystickConnectionState SDLCALL SDL_GetJoystickConnectionState(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the connection state of a joystick.
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <returns>the connection state on success or
	/// <see cref="JoystickConnectionState"/> on failure; call <see cref="GetError"/>
	/// for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickConnectionState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial JoystickConnectionState GetJoystickConnectionState(IntPtr joystick);
	
	
	/// <code>extern SDL_DECLSPEC SDL_PowerState SDLCALL SDL_GetJoystickPowerInfo(SDL_Joystick *joystick, int *percent);</code>
	/// <summary>
	/// <para>Get the battery state of a joystick.</para>
	/// <para>You should never take a battery status as absolute truth. Batteries
	/// (especially failing batteries) are delicate hardware, and the values
	/// reported here are best estimates based on what that hardware reports. It's
	/// not uncommon for older batteries to lose stored power much faster than it
	/// reports, or completely drain when reporting it has 20 percent left, etc.</para>
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <param name="percent">a pointer filled in with the percentage of battery life
	/// left, between 0 and 100, or <c>null</c> to ignore. This will be
	/// filled in with -1 we can't determine a value or there is no
	/// battery.</param>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <returns>the current battery state or <see cref="PowerState.Error"/> on failure;
	/// call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.2.0</since>
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPowerInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	public static partial PowerState GetJoystickPowerInfo(IntPtr joystick, out int percent);
}