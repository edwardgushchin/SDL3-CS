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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

/**
 * # CategoryJoystick
 *
 * SDL joystick support.
 *
 * This is the lower-level joystick handling. If you want the simpler option,
 * where what buttons does what is well-defined, you should use the gamepad
 * API instead.
 *
 * The term "instance_id" is the current instantiation of a joystick device in
 * the system, if the joystick is removed and then re-inserted then it will
 * get a new instance_id, instance_id's are monotonically increasing
 * identifiers of a joystick plugged in.
 *
 * The term "player_index" is the number assigned to a player on a specific
 * controller. For XInput controllers this returns the XInput user index. Many
 * joysticks will not be able to supply this information.
 *
 * The term SDL_JoystickGUID is a stable 128-bit identifier for a joystick
 * device that does not change over time, it identifies class of the device (a
 * X360 wired controller for example). This identifier is platform dependent.
 *
 * In order to use these functions, SDL_Init() must have been called with the
 * SDL_INIT_JOYSTICK flag. This causes SDL to scan the system for joysticks,
 * and load appropriate drivers.
 *
 * If you would like to receive joystick updates while the application is in
 * the background, you should set the following hint before calling
 * SDL_Init(): SDL_HINT_JOYSTICK_ALLOW_BACKGROUND_EVENTS
 */

public static partial class SDL
{
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockJoysticks();
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockJoysticks(void) SDL_ACQUIRE(SDL_joystick_lock);</code>
	/// <summary>
	/// <para>Locking for atomic access to the joystick API.</para>
	/// <para>The SDL joystick functions are thread-safe, however you can lock the
	/// joysticks while processing to guarantee that the joystick list won't change
	/// and joystick and gamepad events will not be delivered.</para>
	/// </summary>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static void LockJoysticks() => SDL_LockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockJoysticks();
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockJoysticks(void) SDL_RELEASE(SDL_joystick_lock);</code>
	/// <summary>
	/// <para>Unlocking for atomic access to the joystick API.</para>
	/// </summary>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static void UnlockJoysticks() => SDL_UnlockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(SDLBool)]
	private static partial bool SDL_HasJoystick();
	/// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasJoystick(void);</code>
	/// <summary>
	/// <para>Return whether a joystick is currently connected.</para>
	/// </summary>
	/// <returns><c>true</c> if a joystick is connected, <c>false</c> otherwise.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoysticks"/>
	public static bool HasJoystick() => SDL_HasJoystick();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoysticks(out int count);
	/// <code>extern SDL_DECLSPEC SDL_JoystickID *SDLCALL SDL_GetJoysticks(int *count);</code>
	/// <summary>
	/// <para>Get a list of currently connected joysticks.</para>
	/// <param name="count">a pointer filled in with the number of joysticks returned.</param>
	/// </summary>
	/// <returns>a 0 terminated array of joystick instance IDs which should be
	/// freed with <see cref="Free"/>, or <c>null</c> on error; call <see cref="GetError"/> for
	/// more details.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="HasJoystick"/>
	/// <seealso cref="OpenJoystick"/>
	public static uint[]? GetJoysticks(out int count)
	{
		var pArray = SDL_GetJoysticks(out count);

		if (pArray == IntPtr.Zero) return null;

		if (count == 0) return [];
		
		try
		{
			var joystickArray = new int[count];
			Marshal.Copy(pArray, joystickArray, 0, count);
			return Array.ConvertAll(joystickArray, item => (uint)item);
		}
		finally
		{
			Free(pArray);
		}
	}
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickNameForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetJoystickNameForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation dependent name of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// <para>The returned string follows the
	/// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the name of the selected joystick. If no name can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickName"/>
	/// <seealso cref="GetJoysticks"/>
	public static string? GetJoystickNameForID(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickNameForID(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPathForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetJoystickPathForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation dependent path of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// <para>The returned string follows the
	/// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the path of the selected joystick. If no path can be found, this
	/// function returns <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickPath"/>
	/// <seealso cref="GetJoysticks"/>
	public static string? GetJoystickPathForID(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickPathForID(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndexForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickPlayerIndexForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the player index of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the player index of a joystick, or <c>-1</c> if it's not available.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	/// <seealso cref="GetJoysticks"/>
	public static int GetJoystickPlayerIndexForID(uint instanceId) => SDL_GetJoystickPlayerIndexForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUIDForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC SDL_JoystickGUID SDLCALL SDL_GetJoystickGUIDForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the implementation-dependent GUID of a joystick.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the GUID of the selected joystick. If called with an invalid
	/// <paramref name="instanceId"/>, this function returns a zero GUID.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickGUID"/>
	/// <seealso cref="GetJoystickGUIDString"/>
	public static GUID GetJoystickGUIDForID(uint instanceId) => SDL_GetJoystickGUIDForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendorForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickVendorForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the USB vendor ID of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the vendor ID isn't
	/// available this function returns <c>0</c>.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the USB vendor ID of the selected joystick. If called with an
	/// invalid <paramref name="instanceId"/>, this function returns <c>0</c>.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickVendor"/>
	/// <seealso cref="GetJoysticks"/>
	public static ushort GetJoystickVendorForID(uint instanceId) => SDL_GetJoystickVendorForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the USB product ID of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the product ID isn't
	/// available this function returns <c>0</c>.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the USB product ID of the selected joystick. If called with an
	/// invalid <paramref name="instanceId"/>, this function returns <c>0</c>.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickProduct"/>
	/// <seealso cref="GetJoysticks"/>
	public static ushort GetJoystickProductForID(uint instanceId) => SDL_GetJoystickProductForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersionForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductVersionForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the product version of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened. If the product version
	/// isn't available this function returns <c>0</c>.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the product version of the selected joystick. If called with an
	/// invalid <paramref name="instanceId"/>, this function returns <c>0</c>.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickProductVersion"/>
	/// <seealso cref="GetJoysticks"/>
	public static ushort GetJoystickProductVersionForID(uint instanceId) => 
		SDL_GetJoystickProductVersionForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickTypeForID(uint instanceId);
	/// <code>extern SDL_DECLSPEC SDL_JoystickType SDLCALL SDL_GetJoystickTypeForID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the type of a joystick, if available.</para>
	/// <para>This can be called before any joysticks are opened.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>the <see cref="JoystickType"/> of the selected joystick. If called with an
	/// invalid <paramref name="instanceId"/>, this function returns
	/// <see cref="JoystickType.Unknown"/>.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickType"/>
	/// <seealso cref="GetJoysticks"/>
	public static JoystickType GetJoystickTypeForID(uint instanceId) => 
		SDL_GetJoystickTypeForID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_OpenJoystick(uint instanceId);
	/// <code>extern SDL_DECLSPEC SDL_Joystick *SDLCALL SDL_OpenJoystick(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Open a joystick for use.</para>
	/// <para>The joystick subsystem must be initialized before a joystick can be opened
	/// for use.</para>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// </summary>
	/// <returns>a joystick identifier or <c>null</c> if an error occurred; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="CloseJoystick"/>
	public static Joystick? OpenJoystick(uint instanceId)
	{
		var joystickPtr = SDL_OpenJoystick(instanceId);

		return joystickPtr == IntPtr.Zero ? null : new Joystick(joystickPtr);
	}


	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromID(uint instanceId);
	/// <code>extern SDL_DECLSPEC SDL_Joystick *SDLCALL SDL_GetJoystickFromID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the <see cref="Joystick"/> associated with an instance ID, if it has been opened.</para>
	/// <param name="instanceId">the instance ID to get the <see cref="Joystick"/> for.</param>
	/// </summary>
	/// <returns>an <see cref="Joystick"/> on success or <c>null</c> on failure or if it hasn't been
	/// opened yet; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static Joystick? GetJoystickFromID(uint instanceId)
	{
		var joystickPtr = SDL_GetJoystickFromID(instanceId);

		return joystickPtr == IntPtr.Zero ? null : new Joystick(joystickPtr);
	}


	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromPlayerIndex(int playerIndex);
	/// <code>extern SDL_DECLSPEC SDL_Joystick *SDLCALL SDL_GetJoystickFromPlayerIndex(int player_index);</code>
	/// <summary>
	/// <para>Get the SDL_Joystick associated with a player index.</para>
	/// </summary>
	/// <param name="playerIndex">the player index to get the SDL_Joystick for.</param>
	/// <returns>an <see cref="Joystick"/> on success or <c>NULL</c> on failure; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	/// <seealso cref="SetJoystickPlayerIndex"/>
	public static Joystick? GetJoystickFromPlayerIndex(int playerIndex)
	{
		var joystickPtr = SDL_GetJoystickFromPlayerIndex(playerIndex);

		return joystickPtr == IntPtr.Zero ? null : new Joystick(joystickPtr);
	}


	[DllImport(SDLLibrary, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
	private static extern uint SDL_AttachVirtualJoystick([In] in VirtualJoystickDesc desc);
	/// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_AttachVirtualJoystick(const SDL_VirtualJoystickDesc *desc);</code>
	/// <summary>
	/// <para>Attach a new virtual joystick.</para>
	/// </summary>
	/// <param name="desc">joystick description.</param>
	/// <returns>the joystick instance ID, or <c>0</c> if an error occurred; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="DetachVirtualJoystick"/>
	public static uint AttachVirtualJoystick(VirtualJoystickDesc desc) => SDL_AttachVirtualJoystick(desc);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_DetachVirtualJoystick(uint instanceID);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_DetachVirtualJoystick(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Detach a virtual joystick.</para>
	/// </summary>
	/// <param name="instanceID">the joystick instance ID, previously returned from
	/// <see cref="AttachVirtualJoystick"/>.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="AttachVirtualJoystick"/>
	public static int DetachVirtualJoystick(uint instanceID) => SDL_DetachVirtualJoystick(instanceID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(SDLBool)]
	private static partial bool SDL_IsJoystickVirtual(uint instanceID);
	/// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_IsJoystickVirtual(SDL_JoystickID instance_id);</code>
	/// <summary>Query whether or not a joystick is virtual.</summary>
	/// <param name="instanceID">the joystick instance ID.</param>
	/// <returns><c>true</c> if the joystick is virtual, <c>false</c> otherwise.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static bool IsJoystickVirtual(uint instanceID) => SDL_IsJoystickVirtual(instanceID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickVirtualAxis(SDL_Joystick *joystick, int axis, Sint16 value);</code>
	/// <summary>
	/// <para>Set the state of an axis on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// SDL_UpdateJoysticks, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// <para>Note that when sending trigger axes, you should scale the value to the full
	/// range of <c>Sint16</c>. For example, a trigger at rest would have the value of
	/// <see cref="JoystickAxisMin"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="axis">the index of the axis on the virtual joystick to update.</param>
	/// <param name="value">the new value for the specified axis.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickVirtualAxis(Joystick joystick, int axis, short value) => 
		SDL_SetJoystickVirtualAxis(joystick.Handle, axis, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickVirtualBall(SDL_Joystick *joystick, int ball, Sint16 xrel, Sint16 yrel);</code>
	/// <summary>
	/// <para>Generate ball motion on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="ball">the index of the ball on the virtual joystick to update.</param>
	/// <param name="xrel">the relative motion on the X axis.</param>
	/// <param name="yrel">the relative motion on the Y axis.</param>
	/// <returns><c>0</c> on success or a negative error code on failure;
	/// call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickVirtualBall(Joystick joystick, int ball, short xrel, short yrel) => 
		SDL_SetJoystickVirtualBall(joystick.Handle, ball, xrel, yrel);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualButton(IntPtr joystick, int button, byte value);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickVirtualButton(SDL_Joystick *joystick, int button, Uint8 value);</code>
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
	/// <param name="value">the new value for the specified button.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickVirtualButton(Joystick joystick, int button, byte value) => 
		SDL_SetJoystickVirtualButton(joystick.Handle, button, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, JoystickHat value);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickVirtualHat(SDL_Joystick *joystick, int hat, Uint8 value);</code>
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
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickVirtualHat(Joystick joystick, int hat, JoystickHat value) => 
		SDL_SetJoystickVirtualHat(joystick.Handle, hat, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, 
		Keystate state, float x, float y, float pressure);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickVirtualTouchpad(SDL_Joystick *joystick, int touchpad, int finger, Uint8 state, float x, float y, float pressure);</code>
	/// <summary>
	/// <para>Set touchpad finger state on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
	/// <see cref="WaitEvent"/>.</para>
	/// </summary>
	/// <param name="joystick">the virtual joystick on which to set state.</param>
	/// <param name="touchpad">the index of the touchpad on the virtual joystick to update.</param>
	/// <param name="finger">the index of the finger on the touchpad to set.</param>
	/// <param name="state"><see cref="Keystate.Pressed"/> if the finger is pressed,
	/// <see cref="Keystate.Released"/> if the finger is released.</param>
	/// <param name="x">the x coordinate of the finger on the touchpad, normalized 0 to 1,
	/// with the origin in the upper left.</param>
	/// <param name="y">the y coordinate of the finger on the touchpad, normalized 0 to 1,
	/// with the origin in the upper left.</param>
	/// <param name="pressure">the pressure of the finger.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickVirtualTouchpad(Joystick joystick, int touchpad, int finger, 
		Keystate state, float x, float y, float pressure) => 
		SDL_SetJoystickVirtualTouchpad(joystick.Handle, touchpad, finger, state, x, y, pressure);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, 
		ulong sensorTimestamp, [In] float[] data, int numValues);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SendJoystickVirtualSensorData(SDL_Joystick *joystick, SDL_SensorType type, Uint64 sensor_timestamp, const float *data, int num_values);</code>
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
	/// <param name="sensorTimestamp">a 64-bit timestamp in nanoseconds associated with the sensor reading.</param>
	/// <param name="data">the data associated with the sensor reading.</param>
	/// <param name="numValues">the number of values pointed to by <c>data</c>.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SendJoystickVirtualSensorData(Joystick joystick, SensorType type, 
		ulong sensorTimestamp, float[] data, int numValues) => 
		SDL_SendJoystickVirtualSensorData(joystick.Handle, type, sensorTimestamp, data, numValues);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickProperties(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetJoystickProperties(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the properties associated with a joystick.</para>
	/// <para>The following read-only properties are provided by SDL:</para>
	/// <list type="bullet">
	/// <item><see cref="PropJoystickCapMonoLedBoolean"/>:
	/// true if this joystick has an LED that has adjustable brightness</item>
	/// <item><see cref="PropJoystickCapRGBLedBoolean"/>:
	/// true if this joystick has an LED that has adjustable color</item>
	/// <item><see cref="PropJoystickCapPlayerLedBoolean"/>:
	/// true if this joystick has a player LED</item>
	/// <item><see cref="PropJoystickCapRumbleBoolean"/>:
	/// true if this joystick has left/right rumble</item>
	/// <item><see cref="PropJoystickCapTriggerRumbleBoolean"/>: true if this joystick has simple trigger rumble</item>
	/// </list>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>a valid property ID on success or <c>0</c> on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static uint GetJoystickProperties(Joystick joystick) => SDL_GetJoystickProperties(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickName(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetJoystickName(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the implementation dependent name of a joystick.</para>
	/// <para>The returned string follows the
	/// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the name of the selected joystick. If no name can be found, this function returns
	/// <c>null</c>; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickNameForID"/>
	public static string? GetJoystickName(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickName(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPath(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetJoystickPath(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the implementation dependent path of a joystick.</para>
	/// <para>The returned string follows the
	/// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the path of the selected joystick. If no path can be found, this function returns <c>null</c>; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickPathForID"/>
	public static string? GetJoystickPath(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickPath(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndex(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickPlayerIndex(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the player index of an opened joystick.</para>
	/// <para>For XInput controllers this returns the XInput user index. Many joysticks
	/// will not be able to supply this information.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the player index, or <c>-1</c> if it's not available.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="SetJoystickPlayerIndex"/>
	public static int GetJoystickPlayerIndex(Joystick joystick) => SDL_GetJoystickPlayerIndex(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickPlayerIndex(IntPtr joystick, int playerIndex);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickPlayerIndex(SDL_Joystick *joystick, int player_index);</code>
	/// <summary>
	/// <para>Set the player index of an opened joystick.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <param name="playerIndex">player index to assign to this joystick, or <c>-1</c> to
	/// clear the player index and turn off player LEDs.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickPlayerIndex"/>
	public static int SetJoystickPlayerIndex(Joystick joystick, int playerIndex) => 
		SDL_SetJoystickPlayerIndex(joystick.Handle, playerIndex);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUID(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_JoystickGUID SDLCALL SDL_GetJoystickGUID(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the implementation-dependent GUID for the joystick.</para>
	/// <para>This function requires an open joystick.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the GUID of the given joystick. If called on an invalid index, this function returns a zero GUID;
	/// call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickGUIDForID"/>
	/// <seealso cref="GetJoystickGUIDString"/>
	public static GUID GetJoystickGUID(Joystick joystick) => SDL_GetJoystickGUID(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendor(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickVendor(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the USB vendor ID of an opened joystick, if available.</para>
	/// <para>If the vendor ID isn't available this function returns <c>0</c>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the USB vendor ID of the selected joystick, or <c>0</c> if unavailable.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickVendorForID"/>
	public static ushort GetJoystickVendor(Joystick joystick) => SDL_GetJoystickVendor(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProduct(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProduct(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the USB product ID of an opened joystick, if available.</para>
	/// <para>If the product ID isn't available this function returns <c>0</c>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the USB product ID of the selected joystick, or <c>0</c> if unavailable.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickProductForID"/>
	public static ushort GetJoystickProduct(Joystick joystick) => SDL_GetJoystickProduct(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersion(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickProductVersion(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the product version of an opened joystick, if available.</para>
	/// <para>If the product version isn't available this function returns <c>0</c>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the product version of the selected joystick, or <c>0</c> if unavailable.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickProductVersionForID"/>
	public static ushort GetJoystickProductVersion(Joystick joystick) => SDL_GetJoystickProductVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC Uint16 SDLCALL SDL_GetJoystickFirmwareVersion(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the firmware version of an opened joystick, if available.</para>
	/// <para>If the firmware version isn't available this function returns <c>0</c>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the firmware version of the selected joystick, or <c>0</c> if unavailable.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static ushort GetJoystickFirmwareVersion(Joystick joystick) => 
		SDL_GetJoystickFirmwareVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickSerial(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetJoystickSerial(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the serial number of an opened joystick, if available.</para>
	/// <para>Returns the serial number of the joystick, or <c>NULL</c> if it is not available.</para>
	/// <para>The returned string follows the
	/// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the serial number of the selected joystick, or <c>NULL</c> if unavailable.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static string? GetJoystickSerial(Joystick joystick) => 
		Marshal.PtrToStringUTF8(SDL_GetJoystickSerial(joystick.Handle));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickType(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_JoystickType SDLCALL SDL_GetJoystickType(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the type of an opened joystick.</para>
	/// </summary>
	/// <param name="joystick">the <see cref="Joystick"/> obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the <see cref="JoystickType"/> of the selected joystick.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickTypeForID"/>
	public static JoystickType GetJoystickType(Joystick joystick) => SDL_GetJoystickType(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickGUIDString(GUID guid, [MarshalAs(UnmanagedType.LPUTF8Str)]out string pszGUID, int cbGUID);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickGUIDString(SDL_JoystickGUID guid, char *pszGUID, int cbGUID);</code>
	/// <summary>
	/// <para>Get an ASCII string representation for a given <see cref="GUID"/>.</para>
	/// <para>You should supply at least 33 bytes for <c>pszGUID</c>.</para>
	/// </summary>
	/// <param name="guid">the <see cref="GUID"/> you wish to convert to string.</param>
	/// <param name="pszGUID">buffer in which to write the ASCII string.</param>
	/// <param name="cbGUID">the size of <c>pszGUID</c>.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickGUIDForID"/>
	/// <seealso cref="GetJoystickGUID"/>
	/// <seealso cref="GetJoystickGUIDFromString"/>
	public static int GetJoystickGUIDString(GUID guid, out string pszGUID, int cbGUID = 33) =>
		SDL_GetJoystickGUIDString(guid, out pszGUID, cbGUID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUIDFromString([MarshalAs(UnmanagedType.LPUTF8Str)] string pchGUID);
	/// <code>extern SDL_DECLSPEC SDL_JoystickGUID SDLCALL SDL_GetJoystickGUIDFromString(const char *pchGUID);</code>
	/// <summary>
	/// <para>Convert a GUID string into a <see cref="GUID"/> structure.</para>
	/// <para>Performs no error checking. If this function is given a string containing
	/// an invalid GUID, the function will silently succeed, but the GUID generated
	/// will not be useful.</para>
	/// </summary>
	/// <param name="pchGUID">string containing an ASCII representation of a GUID.</param>
	/// <returns>a <see cref="GUID"/> structure.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickGUIDString"/>
	public static GUID GetJoystickGUIDFromString(string pchGUID) => SDL_GetJoystickGUIDFromString(pchGUID);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetJoystickGUIDInfo(GUID guid, out ushort vendor, out ushort product, 
		out ushort version, out ushort crc16);
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_GetJoystickGUIDInfo(SDL_JoystickGUID guid, Uint16 *vendor, Uint16 *product, Uint16 *version, Uint16 *crc16);</code>
	/// <summary>
	/// <para>Get the device information encoded in a <see cref="GUID"/> structure.</para>
	/// </summary>
	/// <param name="guid">the <see cref="GUID"/> you wish to get info about.</param>
	/// <param name="vendor">a pointer filled in with the device VID, or <c>0</c> if not available.</param>
	/// <param name="product">a pointer filled in with the device PID, or <c>0</c> if not available.</param>
	/// <param name="version">a pointer filled in with the device version, or <c>0</c> if not available.</param>
	/// <param name="crc16">a pointer filled in with a CRC used to distinguish different products
	/// with the same VID/PID, or <c>0</c> if not available.</param>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickGUIDForID"/>
	public static void GetJoystickGUIDInfo(GUID guid, out ushort vendor, out ushort product,
		out ushort version, out ushort crc16) =>
		SDL_GetJoystickGUIDInfo(guid, out vendor, out product, out version, out crc16);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(SDLBool)]
	private static partial bool SDL_JoystickConnected(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_JoystickConnected(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the status of a specified joystick.</para>
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <returns><c>true</c> if the joystick has been opened, <c>false</c> if it has not; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static bool JoystickConnected(Joystick joystick) => SDL_JoystickConnected(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickID(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_GetJoystickID(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the instance ID of an opened joystick.</para>
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <returns>the instance ID of the specified joystick on success or <c>0</c> on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static uint GetJoystickID(Joystick joystick) => SDL_GetJoystickID(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickAxes(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickAxes(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the number of general axis controls on a joystick.</para>
	/// <para>Often, the directional pad on a game controller will either look like 4
	/// separate buttons or a POV hat, and not axes, but all of this is up to the
	/// device and platform.</para>
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <returns>the number of axis controls/number of axes on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickAxis"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	/// <seealso cref="GetNumJoystickHats"/>
	public static int GetNumJoystickAxes(Joystick joystick) => SDL_GetNumJoystickAxes(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickBalls(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickBalls(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the number of trackballs on a joystick.</para>
	/// <para>Joystick trackballs have only relative motion events associated with them
	/// and their state cannot be polled.</para>
	/// <para>Most joysticks do not have trackballs.</para>
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <returns>the number of trackballs on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickBall"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	/// <seealso cref="GetNumJoystickHats"/>
	public static int GetNumJoystickBalls(Joystick joystick) => SDL_GetNumJoystickBalls(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickHats(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickHats(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the number of POV hats on a joystick.</para>
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <returns>the number of POV hats on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickHat"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickButtons"/>
	public static int GetNumJoystickHats(Joystick joystick) => SDL_GetNumJoystickHats(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickButtons(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumJoystickButtons(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the number of buttons on a joystick.
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <returns>the number of buttons on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetJoystickButton"/>
	/// <seealso cref="GetNumJoystickAxes"/>
	/// <seealso cref="GetNumJoystickBalls"/>
	/// <seealso cref="GetNumJoystickHats"/>
	public static int GetNumJoystickButtons(Joystick joystick) => SDL_GetNumJoystickButtons(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetJoystickEventsEnabled([MarshalAs(SDLBool)] bool enabled);
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetJoystickEventsEnabled(SDL_bool enabled);</code>
	/// <summary>
	/// Set the state of joystick event processing.
	/// </summary>
	/// <param name="enabled">whether to process joystick events or not.</param>
	/// <remarks>
	/// If joystick events are disabled, you must call <see cref="UpdateJoysticks"/> yourself and
	/// check the state of the joystick when you want joystick information.
	/// </remarks>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="JoystickEventsEnabled"/>
	/// <seealso cref="UpdateJoysticks"/>
	public static void SetJoystickEventsEnabled(bool enabled) => SDL_SetJoystickEventsEnabled(enabled);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(SDLBool)]
	private static partial bool SDL_JoystickEventsEnabled();
	/// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_JoystickEventsEnabled(void);</code>
	/// <summary>
	/// Query the state of joystick event processing.
	/// </summary>
	/// <returns><c>true</c> if joystick events are being processed, <c>false</c> otherwise.</returns>
	/// <remarks>
	/// If joystick events are disabled, you must call <see cref="UpdateJoysticks"/> yourself and check the
	/// state of the joystick when you want joystick information.
	/// </remarks>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="SetJoystickEventsEnabled"/>
	public static bool JoystickEventsEnabled() => SDL_JoystickEventsEnabled();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateJoysticks();
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateJoysticks(void);</code>
	/// <summary>
	/// Update the current state of the open joysticks.
	/// </summary>
	/// <remarks>
	/// This function is called automatically by the event loop if any joystick events are enabled.
	/// </remarks>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static void UpdateJoysticks() => SDL_UpdateJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial short SDL_GetJoystickAxis(IntPtr joystick, int axis);
	/// <code>extern SDL_DECLSPEC Sint16 SDLCALL SDL_GetJoystickAxis(SDL_Joystick *joystick, int axis);</code>
	/// <summary>
	/// Get the current state of an axis control on a joystick.
	/// </summary>
	/// <remarks>
	/// SDL makes no promises about what part of the joystick any given axis refers to. Your game should have
	/// some sort of configuration UI to let users specify what each axis should be bound to. Alternately,
	/// SDL's higher-level Game Controller API makes a great effort to apply order to this lower-level interface,
	/// so you know that a specific axis is the "left thumb stick," etc.
	/// 
	/// The value returned by <see cref="GetJoystickAxis"/> is a signed integer (-32768 to 32767) representing
	/// the current position of the axis. It may be necessary to impose certain tolerances on these values to
	/// account for jitter.
	/// </remarks>
	/// <param name="joystick">An <see cref="Joystick"/> structure containing joystick information.</param>
	/// <param name="axis">The axis to query; the axis indices start at index 0.</param>
	/// <returns>A 16-bit signed integer representing the current position of the axis or 0 on failure; call
	/// <c>GetError()</c> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetNumJoystickAxes"/>
	public static short GetJoystickAxis(Joystick joystick, int axis) => SDL_GetJoystickAxis(joystick.Handle, axis);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(SDLBool)]
	private static partial bool SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);
	/// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetJoystickAxisInitialState(SDL_Joystick *joystick, int axis, Sint16 *state);</code>
	/// <summary>
	/// <para>Get the initial state of an axis control on a joystick.</para>
	/// <para>The state is a value ranging from -32768 to 32767.</para>
	/// <para>The axis indices start at index 0.</para>
	/// </summary>
	/// <param name="joystick">An <c>SDL_Joystick</c> structure containing joystick information.</param>
	/// <param name="axis">The axis to query; the axis indices start at index 0.</param>
	/// <param name="state">Upon return, the initial value is supplied here.</param>
	/// <returns><c>SDL_TRUE</c> if this axis has any initial value, or <c>SDL_FALSE</c> if not.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static bool GetJoystickAxisInitialState(Joystick joystick, int axis, out short state) =>
		SDL_GetJoystickAxisInitialState(joystick.Handle, axis, out state);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetJoystickBall(SDL_Joystick *joystick, int ball, int *dx, int *dy);</code>
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
	/// <returns><c>0</c> on success or a negative error code on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetNumJoystickBalls"/>
	public static int GetJoystickBall(Joystick joystick, int ball, out int dx, out int dy) =>
		SDL_GetJoystickBall(joystick.Handle, ball, out dx, out dy);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickHat SDL_GetJoystickHat(IntPtr joystick, int hat);
	/// <code>extern SDL_DECLSPEC Uint8 SDLCALL SDL_GetJoystickHat(SDL_Joystick *joystick, int hat);</code>
	/// <summary>
	/// <para>Get the current state of a POV hat on a joystick.</para>
	/// <para>The returned value will be one of the <c>SDL_HAT_*</c> values.</para>
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <param name="hat">the hat index to get the state from; indices start at index 0.</param>
	/// <returns>the current hat position.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetNumJoystickHats"/>
	public static JoystickHat GetJoystickHat(Joystick joystick, int hat) => SDL_GetJoystickHat(joystick.Handle, hat);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial byte SDL_GetJoystickButton(IntPtr joystick, int button);
	/// <code>extern SDL_DECLSPEC Uint8 SDLCALL SDL_GetJoystickButton(SDL_Joystick *joystick, int button);</code>
	/// <summary>
	/// <para>Get the current state of a button on a joystick.</para>
	/// </summary>
	/// <param name="joystick">an <see cref="Joystick"/> structure containing joystick information.</param>
	/// <param name="button">the button index to get the state from; indices start at index 0.</param>
	/// <returns><c>1</c> if the specified button is pressed, <c>0</c> otherwise.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="GetNumJoystickButtons"/>
	public static byte GetJoystickButton(Joystick joystick, int button) => 
		SDL_GetJoystickButton(joystick.Handle, button);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_RumbleJoystick(IntPtr joystick, ushort lowFrequencyRumble, 
		ushort highFrequencyRumble, uint durationMs);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_RumbleJoystick(SDL_Joystick *joystick, Uint16 low_frequency_rumble, Uint16 high_frequency_rumble, Uint32 duration_ms);</code>
	/// <summary>
	/// <para>Start a rumble effect.</para>
	/// <para>Each call to this function cancels any previous rumble effect, and calling
	/// it with <c>0</c> intensity stops any rumbling.</para>
	/// <para>This function requires you to process SDL events or call
	/// <see cref="UpdateJoysticks"/> to update rumble state.</para>
	/// </summary>
	/// <param name="joystick">the joystick to vibrate.</param>
	/// <param name="lowFrequencyRumble">the intensity of the low frequency (left)
	///                             rumble motor, from <c>0</c> to <c>0xFFFF</c>.</param>
	/// <param name="highFrequencyRumble">the intensity of the high frequency (right)
	///                              rumble motor, from <c>0</c> to <c>0xFFFF</c>.</param>
	/// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
	/// <returns><c>0</c>, or <c>-1</c> if rumble isn't supported on this joystick.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int RumbleJoystick(Joystick joystick, ushort lowFrequencyRumble, ushort highFrequencyRumble,
		uint durationMs) =>
		SDL_RumbleJoystick(joystick.Handle, lowFrequencyRumble, highFrequencyRumble, durationMs);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_RumbleJoystickTriggers(IntPtr joystick, ushort leftRumble, ushort rightRumble, 
		uint durationMs);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_RumbleJoystickTriggers(SDL_Joystick *joystick, Uint16 left_rumble, Uint16 right_rumble, Uint32 duration_ms);</code>
	/// <summary>
	/// <para>Start a rumble effect in the joystick's triggers.</para>
	/// <para>Each call to this function cancels any previous trigger rumble effect, and
	/// calling it with <c>0</c> intensity stops any rumbling.</para>
	/// <para>Note that this is rumbling of the <c>triggers</c> and not the game controller as
	/// a whole. This is currently only supported on Xbox One controllers. If you
	/// want the (more common) whole-controller rumble, use <see cref="RumbleJoystick"/>
	/// instead.</para>
	/// <para>This function requires you to process SDL events or call
	/// <see cref="UpdateJoysticks"/> to update rumble state.</para>
	/// </summary>
	/// <param name="joystick">the joystick to vibrate.</param>
	/// <param name="leftRumble">the intensity of the left trigger rumble motor, from <c>0</c>
	///                    to <c>0xFFFF</c>.</param>
	/// <param name="rightRumble">the intensity of the right trigger rumble motor, from <c>0</c>
	///                     to <c>0xFFFF</c>.</param>
	/// <param name="durationMs">the duration of the rumble effect, in milliseconds.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/>
	/// for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="RumbleJoystick"/>
	public static int RumbleJoystickTriggers(Joystick joystick, ushort leftRumble, ushort rightRumble,
		uint durationMs) =>
		SDL_RumbleJoystickTriggers(joystick.Handle, leftRumble, rightRumble, durationMs);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetJoystickLED(SDL_Joystick *joystick, Uint8 red, Uint8 green, Uint8 blue);</code>
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
	/// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/>
	/// for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SetJoystickLED(Joystick joystick, byte red, byte green, byte blue) =>
		SDL_SetJoystickLED(joystick.Handle, red, green, blue);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);
	/// <code>extern SDL_DECLSPEC int SDLCALL SDL_SendJoystickEffect(SDL_Joystick *joystick, const void *data, int size);</code>
	/// <summary>
	/// <para>Send a joystick specific effect packet.</para>
	/// </summary>
	/// <param name="joystick">the joystick to affect.</param>
	/// <param name="data">the data to send to the joystick.</param>
	/// <param name="size">the size of the data to send to the joystick.</param>
	/// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/>
	/// for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static int SendJoystickEffect(Joystick joystick, IntPtr data, int size) =>
		SDL_SendJoystickEffect(joystick.Handle, data, size);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseJoystick(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseJoystick(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Close a joystick previously opened with <see cref="OpenJoystick"/>.</para>
	/// </summary>
	/// <param name="joystick">the joystick device to close.</param>
	/// <since>This function is available since SDL 3.0.0.</since>
	/// <seealso cref="OpenJoystick"/>
	public static void CloseJoystick(Joystick joystick) => SDL_CloseJoystick(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);
	/// <code>extern SDL_DECLSPEC SDL_JoystickConnectionState SDLCALL SDL_GetJoystickConnectionState(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the connection state of a joystick.</para>
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <returns>the connection state on success or
	///          <see cref="JoystickConnectionState.Invalid"/> on failure; call <see cref="GetError"/>
	///          for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>

	public static JoystickConnectionState GetJoystickConnectionState(Joystick joystick) => 
		SDL_GetJoystickConnectionState(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, out int percent);
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
	///                left, between <c>0</c> and <c>100</c>, or <c>NULL</c> to ignore. This will be
	///                filled in with <c>-1</c> we can't determine a value or there is no
	///                battery.</param>
	/// <returns>the current battery state or <see cref="PowerState.Error"/> on failure;
	///          call <see cref="GetError"/> for more information.</returns>
	/// <since>This function is available since SDL 3.0.0.</since>
	public static PowerState GetJoystickPowerInfo(Joystick joystick, out int percent) =>
		SDL_GetJoystickPowerInfo(joystick.Handle, out percent);
}