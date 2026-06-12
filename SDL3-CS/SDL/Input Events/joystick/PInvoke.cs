#region License
/* Copyright (c) 2024-2026 Eduard Gushchin.
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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_LockJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockJoysticks();
	private delegate void LockJoysticksNativeDelegate();
	private static LockJoysticksNativeDelegate LockJoysticksNativeFunction = SDL_LockJoysticks;

	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_LockJoysticks(void) SDL_ACQUIRE(SDL_event_lock);</code>
	/// <summary>
	/// <para>Locking for atomic access to the joystick API.</para>
	/// <para>The SDL joystick functions are thread-safe, however you can lock the
	/// joysticks while processing to guarantee that the joystick list won't change
	/// and joystick and gamepad events will not be delivered.</para>
	/// </summary>
	/// <threadsafety>This should be called from the same thread that called
	/// <see cref="LockJoysticks"/>.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static void LockJoysticks()
	{
		LockJoysticksNativeFunction();
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_UnlockJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockJoysticks();
	private delegate void UnlockJoysticksNativeDelegate();
	private static UnlockJoysticksNativeDelegate UnlockJoysticksNativeFunction = SDL_UnlockJoysticks;

	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockJoysticks(void) SDL_RELEASE(SDL_event_lock);</code>
	/// <summary>
	/// Unlocking for atomic access to the joystick API.
	/// </summary>
	/// <since>This function is available since SDL 3.2.0</since>
	public static void UnlockJoysticks()
	{
		UnlockJoysticksNativeFunction();
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_HasJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_HasJoystick();
	private delegate bool HasJoystickNativeDelegate();
	private static HasJoystickNativeDelegate HasJoystickNativeFunction = SDL_HasJoystick;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasJoystick(void);</code>
	/// <summary>
	/// Return whether a joystick is currently connected.
	/// </summary>
	/// <returns><c>true</c> if a joystick is connected, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoysticks"/>
	public static bool HasJoystick()
	{
		return HasJoystickNativeFunction();
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoysticks(out int count);
	private delegate IntPtr GetJoysticksNativeDelegate(out int count);
	private static GetJoysticksNativeDelegate GetJoysticksNativeFunction = SDL_GetJoysticks;

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
		var ptr = GetJoysticksNativeFunction(out count);

		try
		{
			return PointerToStructureArray<uint>(ptr, count);
		}
		finally
		{
			Free(ptr);
		}
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickNameForID(uint instanceId);
	private delegate IntPtr GetJoystickNameForIDNativeDelegate(uint instanceId);
	private static GetJoystickNameForIDNativeDelegate GetJoystickNameForIDNativeFunction = SDL_GetJoystickNameForID;

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
        var value = GetJoystickNameForIDNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPathForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPathForID(uint instanceId);
	private delegate IntPtr GetJoystickPathForIDNativeDelegate(uint instanceId);
	private static GetJoystickPathForIDNativeDelegate GetJoystickPathForIDNativeFunction = SDL_GetJoystickPathForID;

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
		var value = GetJoystickPathForIDNativeFunction(instanceId);
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPlayerIndexForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndexForID(uint instanceId);
	private delegate int GetJoystickPlayerIndexForIDNativeDelegate(uint instanceId);
	private static GetJoystickPlayerIndexForIDNativeDelegate GetJoystickPlayerIndexForIDNativeFunction = SDL_GetJoystickPlayerIndexForID;

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
	public static int GetJoystickPlayerIndexForID(uint instanceId)
	{
		return GetJoystickPlayerIndexForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUIDForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUIDForID(uint instanceId);
	private delegate GUID GetJoystickGUIDForIDNativeDelegate(uint instanceId);
	private static GetJoystickGUIDForIDNativeDelegate GetJoystickGUIDForIDNativeFunction = SDL_GetJoystickGUIDForID;

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
	/// <seealso cref="GUIDToString(GUID, byte[], int)"/>
	public static GUID GetJoystickGUIDForID(uint instanceId)
	{
		return GetJoystickGUIDForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickVendorForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendorForID(uint instanceId);
	private delegate ushort GetJoystickVendorForIDNativeDelegate(uint instanceId);
	private static GetJoystickVendorForIDNativeDelegate GetJoystickVendorForIDNativeFunction = SDL_GetJoystickVendorForID;

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
	public static ushort GetJoystickVendorForID(uint instanceId)
	{
		return GetJoystickVendorForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductForID(uint instanceId);
	private delegate ushort GetJoystickProductForIDNativeDelegate(uint instanceId);
	private static GetJoystickProductForIDNativeDelegate GetJoystickProductForIDNativeFunction = SDL_GetJoystickProductForID;

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
	public static ushort GetJoystickProductForID(uint instanceId)
	{
		return GetJoystickProductForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductVersionForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersionForID(uint instanceId);
	private delegate ushort GetJoystickProductVersionForIDNativeDelegate(uint instanceId);
	private static GetJoystickProductVersionForIDNativeDelegate GetJoystickProductVersionForIDNativeFunction = SDL_GetJoystickProductVersionForID;

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
	public static ushort GetJoystickProductVersionForID(uint instanceId)
	{
		return GetJoystickProductVersionForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickTypeForID(uint instanceId);
	private delegate JoystickType GetJoystickTypeForIDNativeDelegate(uint instanceId);
	private static GetJoystickTypeForIDNativeDelegate GetJoystickTypeForIDNativeFunction = SDL_GetJoystickTypeForID;

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
	public static JoystickType GetJoystickTypeForID(uint instanceId)
	{
		return GetJoystickTypeForIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_OpenJoystick(uint instanceId);
	private delegate IntPtr OpenJoystickNativeDelegate(uint instanceId);
	private static OpenJoystickNativeDelegate OpenJoystickNativeFunction = SDL_OpenJoystick;

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
	public static IntPtr OpenJoystick(uint instanceId)
	{
		return OpenJoystickNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromID(uint instanceId);
	private delegate IntPtr GetJoystickFromIDNativeDelegate(uint instanceId);
	private static GetJoystickFromIDNativeDelegate GetJoystickFromIDNativeFunction = SDL_GetJoystickFromID;

	/// <code>extern SDL_DECLSPEC SDL_Joystick * SDLCALL SDL_GetJoystickFromID(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// <para>Get the SDL_Joystick associated with an instance ID, if it has been opened.</para>
	/// </summary>
	/// <param name="instanceId">the instance ID to get the SDL_Joystick for.</param>
	/// <returns>an SDL_Joystick on success or <c>null</c> on failure or if it hasn't been
	/// opened yet; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static IntPtr GetJoystickFromID(uint instanceId)
	{
		return GetJoystickFromIDNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFromPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromPlayerIndex(int playerIndex);
	private delegate IntPtr GetJoystickFromPlayerIndexNativeDelegate(int playerIndex);
	private static GetJoystickFromPlayerIndexNativeDelegate GetJoystickFromPlayerIndexNativeFunction = SDL_GetJoystickFromPlayerIndex;

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
	public static IntPtr GetJoystickFromPlayerIndex(int playerIndex)
	{
		return GetJoystickFromPlayerIndexNativeFunction(playerIndex);
	}


	[ExcludeFromCodeCoverage]
	[DllImport(SDLLibrary, EntryPoint = "SDL_AttachVirtualJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static extern uint SDL_AttachVirtualJoystick(in VirtualJoystickDesc desc);
	private delegate uint AttachVirtualJoystickNativeDelegate(in VirtualJoystickDesc desc);
	private static AttachVirtualJoystickNativeDelegate AttachVirtualJoystickNativeFunction = SDL_AttachVirtualJoystick;

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
	/// <seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static uint AttachVirtualJoystick(in VirtualJoystickDesc desc)
	{
		return AttachVirtualJoystickNativeFunction(in desc);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_DetachVirtualJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_DetachVirtualJoystick(uint instanceId);
	private delegate bool DetachVirtualJoystickNativeDelegate(uint instanceId);
	private static DetachVirtualJoystickNativeDelegate DetachVirtualJoystickNativeFunction = SDL_DetachVirtualJoystick;

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
	public static bool DetachVirtualJoystick(uint instanceId)
	{
		return DetachVirtualJoystickNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_IsJoystickVirtual"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_IsJoystickVirtual(uint instanceId);
	private delegate bool IsJoystickVirtualNativeDelegate(uint instanceId);
	private static IsJoystickVirtualNativeDelegate IsJoystickVirtualNativeFunction = SDL_IsJoystickVirtual;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsJoystickVirtual(SDL_JoystickID instance_id);</code>
	/// <summary>
	/// Query whether or not a joystick is virtual.
	/// </summary>
	/// <param name="instanceId">the joystick instance ID.</param>
	/// <returns><c>true</c> if the joystick is virtual, <c>false</c> otherwise.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static bool IsJoystickVirtual(uint instanceId)
	{
		return IsJoystickVirtualNativeFunction(instanceId);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);
	private delegate bool SetJoystickVirtualAxisNativeDelegate(IntPtr joystick, int axis, short value);
	private static SetJoystickVirtualAxisNativeDelegate SetJoystickVirtualAxisNativeFunction = SDL_SetJoystickVirtualAxis;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualAxis(SDL_Joystick *joystick, int axis, Sint16 value);</code>
	/// <summary>
	/// <para>Set the state of an axis on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	/// <seealso cref="SetJoystickVirtualHat"/>
	/// <seealso cref="SetJoystickVirtualTouchpad"/>
	/// <seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static bool SetJoystickVirtualAxis(IntPtr joystick, int axis, short value)
	{
		return SetJoystickVirtualAxisNativeFunction(joystick, axis, value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualBall"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);
	private delegate bool SetJoystickVirtualBallNativeDelegate(IntPtr joystick, int ball, short xrel, short yrel);
	private static SetJoystickVirtualBallNativeDelegate SetJoystickVirtualBallNativeFunction = SDL_SetJoystickVirtualBall;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualBall(SDL_Joystick *joystick, int ball, Sint16 xrel, Sint16 yrel);</code>
	/// <summary>
	/// <para>Generate ball motion on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	/// <seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static bool SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel)
	{
		return SetJoystickVirtualBallNativeFunction(joystick, ball, xrel, yrel);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickVirtualButton(IntPtr joystick, int button, [MarshalAs(UnmanagedType.I1)] bool down);
	private delegate bool SetJoystickVirtualButtonNativeDelegate(IntPtr joystick, int button, bool down);
	private static SetJoystickVirtualButtonNativeDelegate SetJoystickVirtualButtonNativeFunction = SDL_SetJoystickVirtualButton;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualButton(SDL_Joystick *joystick, int button, bool down);</code>
	/// <summary>
	/// <para>Set the state of a button on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	///	<seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static bool SetJoystickVirtualButton(IntPtr joystick, int button, bool down)
	{
		return SetJoystickVirtualButtonNativeFunction(joystick, button, down);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualHat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, JoystickHat value);
	private delegate bool SetJoystickVirtualHatNativeDelegate(IntPtr joystick, int hat, JoystickHat value);
	private static SetJoystickVirtualHatNativeDelegate SetJoystickVirtualHatNativeFunction = SDL_SetJoystickVirtualHat;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualHat(SDL_Joystick *joystick, int hat, Uint8 value);</code>
	/// <summary>
	/// <para>Set the state of a hat on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	/// <seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static bool SetJoystickVirtualHat(IntPtr joystick, int hat, JoystickHat value)
	{
		return SetJoystickVirtualHatNativeFunction(joystick, hat, value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickVirtualTouchpad"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, [MarshalAs(UnmanagedType.I1)] bool down, float x, float y, float pressure);
	private delegate bool SetJoystickVirtualTouchpadNativeDelegate(IntPtr joystick, int touchpad, int finger, bool down, float x, float y, float pressure);
	private static SetJoystickVirtualTouchpadNativeDelegate SetJoystickVirtualTouchpadNativeFunction = SDL_SetJoystickVirtualTouchpad;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetJoystickVirtualTouchpad(SDL_Joystick *joystick, int touchpad, int finger, bool down, float x, float y, float pressure);</code>
	/// <summary>
	/// <para>Set touchpad finger state on an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	/// <seealso cref="SetJoystickVirtualHat"/>
	/// <seealso cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static bool SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, bool down, float x, float y, float pressure)
	{
		return SetJoystickVirtualTouchpadNativeFunction(joystick, touchpad, finger, down, x, y, pressure);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickVirtualSensorData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] float[] data, int numValues);
	private delegate bool SendJoystickVirtualSensorDataNativeDelegate(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, float[] data, int numValues);
	private static SendJoystickVirtualSensorDataNativeDelegate SendJoystickVirtualSensorDataNativeFunction = SDL_SendJoystickVirtualSensorData;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SendJoystickVirtualSensorData(SDL_Joystick *joystick, SDL_SensorType type, Uint64 sensor_timestamp, const float *data, int num_values);</code>
	/// <summary>
	/// <para>Send a sensor update for an opened virtual joystick.</para>
	/// <para>Please note that values set here will not be applied until the next call to
	/// <see cref="UpdateJoysticks"/>, which can either be called directly, or can be called
	/// indirectly through various other SDL APIs, including, but not limited to
	/// the following: <see cref="PollEvent(out Event)"/>, <see cref="PumpEvents"/>, <see cref="WaitEventTimeout"/>,
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
	/// <seealso cref="SetJoystickVirtualAxis"/>
	/// <seealso cref="SetJoystickVirtualButton"/>
	/// <seealso cref="SetJoystickVirtualBall"/>
	/// <seealso cref="SetJoystickVirtualHat"/>
	/// <seealso cref="SetJoystickVirtualTouchpad"/>
	public static bool SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 4)] float[] data, int numValues)
	{
		return SendJoystickVirtualSensorDataNativeFunction(joystick, type, sensorTimestamp, data, numValues);
	}

	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickVirtualSensorData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, IntPtr data, int numValues);
	private delegate bool SendJoystickVirtualSensorDataPointerNativeDelegate(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, IntPtr data, int numValues);
	private static SendJoystickVirtualSensorDataPointerNativeDelegate SendJoystickVirtualSensorDataPointerNativeFunction = SDL_SendJoystickVirtualSensorData;

	/// <inheritdoc cref="SendJoystickVirtualSensorData(nint, SensorType, UInt64, float[], int)"/>
	public static unsafe bool SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, UInt64 sensorTimestamp, ReadOnlySpan<float> data, int numValues)
	{
		fixed (float* pData = data)
		{
			return SendJoystickVirtualSensorDataPointerNativeFunction(joystick, type, sensorTimestamp, (IntPtr)pData, numValues);
		}
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickProperties(IntPtr joystick);
	private delegate uint GetJoystickPropertiesNativeDelegate(IntPtr joystick);
	private static GetJoystickPropertiesNativeDelegate GetJoystickPropertiesNativeFunction = SDL_GetJoystickProperties;

	/// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetJoystickProperties(SDL_Joystick *joystick);</code>
	/// <summary>
	/// <para>Get the properties associated with a joystick.</para>
	/// <para>The following read-only properties are provided by SDL:</para>
	/// <list type="bullet">
	///	<item><see cref="Props.JoystickCapMonoLedBoolean"/>: <c>true</c> if this joystick has an
	/// LED that has adjustable brightness</item>
	/// <item><see cref="Props.JoystickCapRGBLedBoolean"/>: <c>true</c> if this joystick has an LED
	/// that has adjustable color</item>
	/// <item><see cref="Props.JoystickCapPlayerLedBoolean"/>: <c>true</c> if this joystick has a
	/// player LED</item>
	/// <item><see cref="Props.JoystickCapRumbleBoolean"/>: <c>true</c> if this joystick has
	/// left/right rumble</item>
	/// <item><see cref="Props.JoystickCapTriggerRumbleBoolean"/>: <c>true</c> if this joystick has
	/// simple trigger rumble</item>
	/// </list>
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>a valid property ID on success or 0 on failure; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static uint GetJoystickProperties(IntPtr joystick)
	{
		return GetJoystickPropertiesNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickName(IntPtr joystick);
	private delegate IntPtr GetJoystickNameNativeDelegate(IntPtr joystick);
	private static GetJoystickNameNativeDelegate GetJoystickNameNativeFunction = SDL_GetJoystickName;
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
		var value = GetJoystickNameNativeFunction(joystick);
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPath"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickPath(IntPtr joystick);
	private delegate IntPtr GetJoystickPathNativeDelegate(IntPtr joystick);
	private static GetJoystickPathNativeDelegate GetJoystickPathNativeFunction = SDL_GetJoystickPath;
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
		var value = GetJoystickPathNativeFunction(joystick);
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndex(IntPtr joystick);
	private delegate int GetJoystickPlayerIndexNativeDelegate(IntPtr joystick);
	private static GetJoystickPlayerIndexNativeDelegate GetJoystickPlayerIndexNativeFunction = SDL_GetJoystickPlayerIndex;

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
	public static int GetJoystickPlayerIndex(IntPtr joystick)
	{
		return GetJoystickPlayerIndexNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickPlayerIndex"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickPlayerIndex(IntPtr joystick, int playerIndex);
	private delegate bool SetJoystickPlayerIndexNativeDelegate(IntPtr joystick, int playerIndex);
	private static SetJoystickPlayerIndexNativeDelegate SetJoystickPlayerIndexNativeFunction = SDL_SetJoystickPlayerIndex;

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
	public static bool SetJoystickPlayerIndex(IntPtr joystick, int playerIndex)
	{
		return SetJoystickPlayerIndexNativeFunction(joystick, playerIndex);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUID(IntPtr joystick);
	private delegate GUID GetJoystickGUIDNativeDelegate(IntPtr joystick);
	private static GetJoystickGUIDNativeDelegate GetJoystickGUIDNativeFunction = SDL_GetJoystickGUID;

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
	/// <seealso cref="GUIDToString(GUID, byte[], int)"/>
	public static GUID GetJoystickGUID(IntPtr joystick)
	{
		return GetJoystickGUIDNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickVendor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendor(IntPtr joystick);
	private delegate ushort GetJoystickVendorNativeDelegate(IntPtr joystick);
	private static GetJoystickVendorNativeDelegate GetJoystickVendorNativeFunction = SDL_GetJoystickVendor;

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
	public static ushort GetJoystickVendor(IntPtr joystick)
	{
		return GetJoystickVendorNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProduct"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProduct(IntPtr joystick);
	private delegate ushort GetJoystickProductNativeDelegate(IntPtr joystick);
	private static GetJoystickProductNativeDelegate GetJoystickProductNativeFunction = SDL_GetJoystickProduct;

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
	public static ushort GetJoystickProduct(IntPtr joystick)
	{
		return GetJoystickProductNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickProductVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersion(IntPtr joystick);
	private delegate ushort GetJoystickProductVersionNativeDelegate(IntPtr joystick);
	private static GetJoystickProductVersionNativeDelegate GetJoystickProductVersionNativeFunction = SDL_GetJoystickProductVersion;

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
	public static ushort GetJoystickProductVersion(IntPtr joystick)
	{
		return GetJoystickProductVersionNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickFirmwareVersion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);
	private delegate ushort GetJoystickFirmwareVersionNativeDelegate(IntPtr joystick);
	private static GetJoystickFirmwareVersionNativeDelegate GetJoystickFirmwareVersionNativeFunction = SDL_GetJoystickFirmwareVersion;

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
	public static ushort GetJoystickFirmwareVersion(IntPtr joystick)
	{
		return GetJoystickFirmwareVersionNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickSerial"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickSerial(IntPtr joystick);
	private delegate IntPtr GetJoystickSerialNativeDelegate(IntPtr joystick);
	private static GetJoystickSerialNativeDelegate GetJoystickSerialNativeFunction = SDL_GetJoystickSerial;
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
		var value = GetJoystickSerialNativeFunction(joystick);
		return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickType(IntPtr joystick);
	private delegate JoystickType GetJoystickTypeNativeDelegate(IntPtr joystick);
	private static GetJoystickTypeNativeDelegate GetJoystickTypeNativeFunction = SDL_GetJoystickType;

	/// <code>extern SDL_DECLSPEC SDL_JoystickType SDLCALL SDL_GetJoystickType(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the type of an opened joystick.
	/// </summary>
	/// <param name="joystick">the SDL_Joystick obtained from <see cref="OpenJoystick"/>.</param>
	/// <returns>the <see cref="JoystickType"/> of the selected joystick.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="GetJoystickTypeForID"/>
	public static JoystickType GetJoystickType(IntPtr joystick)
	{
		return GetJoystickTypeNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickGUIDInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_GetJoystickGUIDInfo(GUID guid, out short vendor, out short product, out short version, out short crc16);
	private delegate void GetJoystickGUIDInfoNativeDelegate(GUID guid, out short vendor, out short product, out short version, out short crc16);
	private static GetJoystickGUIDInfoNativeDelegate GetJoystickGUIDInfoNativeFunction = SDL_GetJoystickGUIDInfo;

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
	public static void GetJoystickGUIDInfo(GUID guid, out short vendor, out short product, out short version, out short crc16)
	{
		GetJoystickGUIDInfoNativeFunction(guid, out vendor, out product, out version, out crc16);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_JoystickConnected"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_JoystickConnected(IntPtr joystick);
	private delegate bool JoystickConnectedNativeDelegate(IntPtr joystick);
	private static JoystickConnectedNativeDelegate JoystickConnectedNativeFunction = SDL_JoystickConnected;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_JoystickConnected(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the status of a specified joystick.
	/// </summary>
	/// <param name="joystick">the joystick to query.</param>
	/// <returns><c>true</c> if the joystick has been opened, <c>false</c> if it has not; call
	/// <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static bool JoystickConnected(IntPtr joystick)
	{
		return JoystickConnectedNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickID(IntPtr joystick);
	private delegate uint GetJoystickIDNativeDelegate(IntPtr joystick);
	private static GetJoystickIDNativeDelegate GetJoystickIDNativeFunction = SDL_GetJoystickID;

	/// <code>extern SDL_DECLSPEC SDL_JoystickID SDLCALL SDL_GetJoystickID(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Get the instance ID of an opened joystick.
	/// </summary>
	/// <param name="joystick">an SDL_Joystick structure containing joystick information.</param>
	/// <returns>the instance ID of the specified joystick on success or 0 on
	/// failure; call <see cref="GetError"/> for more information.</returns>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static uint GetJoystickID(IntPtr joystick)
	{
		return GetJoystickIDNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickAxes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickAxes(IntPtr joystick);
	private delegate int GetNumJoystickAxesNativeDelegate(IntPtr joystick);
	private static GetNumJoystickAxesNativeDelegate GetNumJoystickAxesNativeFunction = SDL_GetNumJoystickAxes;

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
	public static int GetNumJoystickAxes(IntPtr joystick)
	{
		return GetNumJoystickAxesNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickBalls"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickBalls(IntPtr joystick);
	private delegate int GetNumJoystickBallsNativeDelegate(IntPtr joystick);
	private static GetNumJoystickBallsNativeDelegate GetNumJoystickBallsNativeFunction = SDL_GetNumJoystickBalls;

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
	public static int GetNumJoystickBalls(IntPtr joystick)
	{
		return GetNumJoystickBallsNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickHats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickHats(IntPtr joystick);
	private delegate int GetNumJoystickHatsNativeDelegate(IntPtr joystick);
	private static GetNumJoystickHatsNativeDelegate GetNumJoystickHatsNativeFunction = SDL_GetNumJoystickHats;

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
	public static int GetNumJoystickHats(IntPtr joystick)
	{
		return GetNumJoystickHatsNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumJoystickButtons"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetNumJoystickButtons(IntPtr joystick);
	private delegate int GetNumJoystickButtonsNativeDelegate(IntPtr joystick);
	private static GetNumJoystickButtonsNativeDelegate GetNumJoystickButtonsNativeFunction = SDL_GetNumJoystickButtons;

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
	public static int GetNumJoystickButtons(IntPtr joystick)
	{
		return GetNumJoystickButtonsNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_SetJoystickEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled);
	private delegate void SetJoystickEventsEnabledNativeDelegate(bool enabled);
	private static SetJoystickEventsEnabledNativeDelegate SetJoystickEventsEnabledNativeFunction = SDL_SetJoystickEventsEnabled;

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
	public static void SetJoystickEventsEnabled([MarshalAs(UnmanagedType.I1)] bool enabled)
	{
		SetJoystickEventsEnabledNativeFunction(enabled);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_JoystickEventsEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_JoystickEventsEnabled();
	private delegate bool JoystickEventsEnabledNativeDelegate();
	private static JoystickEventsEnabledNativeDelegate JoystickEventsEnabledNativeFunction = SDL_JoystickEventsEnabled;

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
	public static bool JoystickEventsEnabled()
	{
		return JoystickEventsEnabledNativeFunction();
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateJoysticks"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UpdateJoysticks();
	private delegate void UpdateJoysticksNativeDelegate();
	private static UpdateJoysticksNativeDelegate UpdateJoysticksNativeFunction = SDL_UpdateJoysticks;

	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateJoysticks(void);</code>
	/// <summary>
	/// <para>Update the current state of the open joysticks.</para>
	/// <para>This is called automatically by the event loop if any joystick events are
	/// enabled and <see cref="Hints.AutoUpdateJoysticks"/> hasn't been set to "0".</para>
	/// </summary>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	public static void UpdateJoysticks()
	{
		UpdateJoysticksNativeFunction();
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickAxis"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial short SDL_GetJoystickAxis(IntPtr joystick, int axis);
	private delegate short GetJoystickAxisNativeDelegate(IntPtr joystick, int axis);
	private static GetJoystickAxisNativeDelegate GetJoystickAxisNativeFunction = SDL_GetJoystickAxis;

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
	public static short GetJoystickAxis(IntPtr joystick, int axis)
	{
		return GetJoystickAxisNativeFunction(joystick, axis);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickAxisInitialState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state);
	private delegate bool GetJoystickAxisInitialStateNativeDelegate(IntPtr joystick, int axis, out short state);
	private static GetJoystickAxisInitialStateNativeDelegate GetJoystickAxisInitialStateNativeFunction = SDL_GetJoystickAxisInitialState;

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
	public static bool GetJoystickAxisInitialState(IntPtr joystick, int axis, out short state)
	{
		return GetJoystickAxisInitialStateNativeFunction(joystick, axis, out state);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickBall"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy);
	private delegate bool GetJoystickBallNativeDelegate(IntPtr joystick, int ball, out int dx, out int dy);
	private static GetJoystickBallNativeDelegate GetJoystickBallNativeFunction = SDL_GetJoystickBall;

	/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetJoystickBall(SDL_Joystick *joystick, int ball, int *dx, int *dy);</code>
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
	public static bool GetJoystickBall(IntPtr joystick, int ball, out int dx, out int dy)
	{
		return GetJoystickBallNativeFunction(joystick, ball, out dx, out dy);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickHat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickHat SDL_GetJoystickHat(IntPtr joystick, int hat);
	private delegate JoystickHat GetJoystickHatNativeDelegate(IntPtr joystick, int hat);
	private static GetJoystickHatNativeDelegate GetJoystickHatNativeFunction = SDL_GetJoystickHat;

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
	public static JoystickHat GetJoystickHat(IntPtr joystick, int hat)
	{
		return GetJoystickHatNativeFunction(joystick, hat);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickButton"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_GetJoystickButton(IntPtr joystick, int button);
	private delegate bool GetJoystickButtonNativeDelegate(IntPtr joystick, int button);
	private static GetJoystickButtonNativeDelegate GetJoystickButtonNativeFunction = SDL_GetJoystickButton;

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
	public static bool GetJoystickButton(IntPtr joystick, int button)
	{
		return GetJoystickButtonNativeFunction(joystick, button);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_RumbleJoystick(IntPtr joystick, short lowFrequencyRumble, short highFrequencyRumble, int durationMs);
	private delegate bool RumbleJoystickNativeDelegate(IntPtr joystick, short lowFrequencyRumble, short highFrequencyRumble, int durationMs);
	private static RumbleJoystickNativeDelegate RumbleJoystickNativeFunction = SDL_RumbleJoystick;

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
	public static bool RumbleJoystick(IntPtr joystick, short lowFrequencyRumble, short highFrequencyRumble, int durationMs)
	{
		return RumbleJoystickNativeFunction(joystick, lowFrequencyRumble, highFrequencyRumble, durationMs);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_RumbleJoystickTriggers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_RumbleJoystickTriggers(IntPtr joystick, short leftRumble, short rightRumble, int durationMs);
	private delegate bool RumbleJoystickTriggersNativeDelegate(IntPtr joystick, short leftRumble, short rightRumble, int durationMs);
	private static RumbleJoystickTriggersNativeDelegate RumbleJoystickTriggersNativeFunction = SDL_RumbleJoystickTriggers;

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
	public static bool RumbleJoystickTriggers(IntPtr joystick, short leftRumble, short rightRumble, int durationMs)
	{
		return RumbleJoystickTriggersNativeFunction(joystick, leftRumble, rightRumble, durationMs);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SetJoystickLED"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue);
	private delegate bool SetJoystickLEDNativeDelegate(IntPtr joystick, byte red, byte green, byte blue);
	private static SetJoystickLEDNativeDelegate SetJoystickLEDNativeFunction = SDL_SetJoystickLED;

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
	public static bool SetJoystickLED(IntPtr joystick, byte red, byte green, byte blue)
	{
		return SetJoystickLEDNativeFunction(joystick, red, green, blue);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SendJoystickEffect(IntPtr joystick, IntPtr data, int size);
	private delegate bool SendJoystickEffectPointerNativeDelegate(IntPtr joystick, IntPtr data, int size);
	private static SendJoystickEffectPointerNativeDelegate SendJoystickEffectPointerNativeFunction = SDL_SendJoystickEffect;

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
	public static bool SendJoystickEffect(IntPtr joystick, IntPtr data, int size)
	{
		return SendJoystickEffectPointerNativeFunction(joystick, data, size);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_SendJoystickEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_SendJoystickEffect(IntPtr joystick, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int size);
	private delegate bool SendJoystickEffectArrayNativeDelegate(IntPtr joystick, byte[] data, int size);
	private static SendJoystickEffectArrayNativeDelegate SendJoystickEffectArrayNativeFunction = SDL_SendJoystickEffect;

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
	public static bool SendJoystickEffect(IntPtr joystick, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] data, int size)
	{
		return SendJoystickEffectArrayNativeFunction(joystick, data, size);
	}

	/// <inheritdoc cref="SendJoystickEffect(nint, byte[], int)"/>
	public static unsafe bool SendJoystickEffect(IntPtr joystick, ReadOnlySpan<byte> data, int size)
	{
		fixed (byte* pData = data)
		{
			return SendJoystickEffectPointerNativeFunction(joystick, (IntPtr)pData, size);
		}
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_CloseJoystick(IntPtr joystick);
	private delegate void CloseJoystickNativeDelegate(IntPtr joystick);
	private static CloseJoystickNativeDelegate CloseJoystickNativeFunction = SDL_CloseJoystick;

	/// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseJoystick(SDL_Joystick *joystick);</code>
	/// <summary>
	/// Close a joystick previously opened with <see cref="OpenJoystick"/>.
	/// </summary>
	/// <param name="joystick">the joystick device to close.</param>
	/// <threadsafety>It is safe to call this function from any thread.</threadsafety>
	/// <since>This function is available since SDL 3.2.0</since>
	/// <seealso cref="OpenJoystick"/>
	public static void CloseJoystick(IntPtr joystick)
	{
		CloseJoystickNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickConnectionState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickConnectionState SDL_GetJoystickConnectionState(IntPtr joystick);
	private delegate JoystickConnectionState GetJoystickConnectionStateNativeDelegate(IntPtr joystick);
	private static GetJoystickConnectionStateNativeDelegate GetJoystickConnectionStateNativeFunction = SDL_GetJoystickConnectionState;

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
	public static JoystickConnectionState GetJoystickConnectionState(IntPtr joystick)
	{
		return GetJoystickConnectionStateNativeFunction(joystick);
	}


	[ExcludeFromCodeCoverage]
	[LibraryImport(SDLLibrary, EntryPoint = "SDL_GetJoystickPowerInfo"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial PowerState SDL_GetJoystickPowerInfo(IntPtr joystick, out int percent);
	private delegate PowerState GetJoystickPowerInfoNativeDelegate(IntPtr joystick, out int percent);
	private static GetJoystickPowerInfoNativeDelegate GetJoystickPowerInfoNativeFunction = SDL_GetJoystickPowerInfo;

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
	public static PowerState GetJoystickPowerInfo(IntPtr joystick, out int percent)
	{
		return GetJoystickPowerInfoNativeFunction(joystick, out percent);
	}
}
