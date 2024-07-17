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
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
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
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_LockJoysticks();
	public static void LockJoysticks() => SDL_LockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial void SDL_UnlockJoysticks();
	public static void UnlockJoysticks() => SDL_UnlockJoysticks();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_HasJoystick();
	public static bool HasJoystick() => SDL_HasJoystick();
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoysticks(out int count);
	public static uint[] GetJoysticks(out int cout)
	{
		var pArray = SDL_GetJoysticks(out cout);
		var joystickArray = new int[cout];
		Marshal.Copy(pArray, joystickArray, 0, cout);
		Free(pArray);
		return Array.ConvertAll(joystickArray, item => (uint)item);
	}
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickInstanceName(uint instanceId);
	public static string? GetJoystickInstanceName(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickInstanceName(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickInstancePath(uint instanceId);
	public static string? GetJoystickInstancePath(uint instanceId) =>
		Marshal.PtrToStringUTF8(SDL_GetJoystickInstancePath(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickInstancePlayerIndex(uint instanceId);
	public static int GetJoystickInstancePlayerIndex(uint instanceId) => SDL_GetJoystickInstancePlayerIndex(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickInstanceGUID(uint instanceId);
	public static GUID GetJoystickInstanceGUID(uint instanceId) => SDL_GetJoystickInstanceGUID(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceVendor(uint instanceId);
	public static ushort GetJoystickInstanceVendor(uint instanceId) => SDL_GetJoystickInstanceVendor(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceProduct(uint instanceId);
	public static ushort GetJoystickInstanceProduct(uint instanceId) => SDL_GetJoystickInstanceProduct(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickInstanceProductVersion(uint instanceId);
	public static ushort GetJoystickInstanceProductVersion(uint instanceId) => 
		SDL_GetJoystickInstanceProductVersion(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial JoystickType SDL_GetJoystickInstanceType(uint instanceId);
	public static JoystickType GetJoystickInstanceType(uint instanceId) => 
		SDL_GetJoystickInstanceType(instanceId);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_OpenJoystick(uint instanceId);
	public static Joystick OpenJoystick(uint instanceId) => new(SDL_OpenJoystick(instanceId));
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromInstanceID(uint instanceId);
	public static Joystick GetJoystickFromInstanceID(uint instanceId) => 
		new(SDL_GetJoystickFromInstanceID(instanceId));

	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial IntPtr SDL_GetJoystickFromPlayerIndex(int playerIndex);
	public static Joystick GetJoystickFromPlayerIndex(int playerIndex) =>
		new(SDL_GetJoystickFromPlayerIndex(playerIndex));


	[DllImport(SDLLibrary, CallingConvention = CallingConvention.Cdecl, ExactSpelling = true)]
	private static extern uint SDL_AttachVirtualJoystick([In] in VirtualJoystickDesc desc);
	public static uint AttachVirtualJoystick(VirtualJoystickDesc desc) => SDL_AttachVirtualJoystick(desc);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_DetachVirtualJoystick(uint instance_id);
	public static int DetachVirtualJoystick(uint instance_id) => SDL_DetachVirtualJoystick(instance_id);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.I1)]
	private static partial bool SDL_IsJoystickVirtual(uint instance_id);
	public static bool IsJoystickVirtual(uint instance_id) => SDL_IsJoystickVirtual(instance_id);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualAxis(IntPtr joystick, int axis, short value);
	public static int SetJoystickVirtualAxis(Joystick joystick, int axis, short value) => 
		SDL_SetJoystickVirtualAxis(joystick.Handle, axis, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualBall(IntPtr joystick, int ball, short xrel, short yrel);
	public static int SetJoystickVirtualBall(Joystick joystick, int ball, short xrel, short yrel) => 
		SDL_SetJoystickVirtualBall(joystick.Handle, ball, xrel, yrel);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualButton(IntPtr joystick, int button, byte value);
	public static int SetJoystickVirtualButton(Joystick joystick, int button, byte value) => 
		SDL_SetJoystickVirtualButton(joystick.Handle, button, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualHat(IntPtr joystick, int hat, byte value);
	public static int SetJoystickVirtualHat(Joystick joystick, int hat, byte value) => 
		SDL_SetJoystickVirtualHat(joystick.Handle, hat, value);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickVirtualTouchpad(IntPtr joystick, int touchpad, int finger, Keystate state, float x, float y, float pressure);
	public static int SetJoystickVirtualTouchpad(Joystick joystick, int touchpad, int finger, 
		Keystate state, float x, float y, float pressure) => 
		SDL_SetJoystickVirtualTouchpad(joystick.Handle, touchpad, finger, state, x, y, pressure);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SendJoystickVirtualSensorData(IntPtr joystick, SensorType type, 
		ulong sensor_timestamp, [In] float[] data, int num_values);
	public static int SendJoystickVirtualSensorData(Joystick joystick, SensorType type, 
		ulong sensor_timestamp, float[] data, int num_values) => 
		SDL_SendJoystickVirtualSensorData(joystick.Handle, type, sensor_timestamp, data, num_values);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial uint SDL_GetJoystickProperties(IntPtr joystick);
	public static uint GetJoystickProperties(Joystick joystick) => SDL_GetJoystickProperties(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.LPUTF8Str)]
	private static partial string SDL_GetJoystickName(IntPtr joystick);
	public static string GetJoystickName(Joystick joystick) => SDL_GetJoystickName(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.LPUTF8Str)]
	private static partial string SDL_GetJoystickPath(IntPtr joystick);
	public static string GetJoystickPath(Joystick joystick) => SDL_GetJoystickPath(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_GetJoystickPlayerIndex(IntPtr joystick);
	public static int GetJoystickPlayerIndex(Joystick joystick) => SDL_GetJoystickPlayerIndex(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial int SDL_SetJoystickPlayerIndex(IntPtr joystick, int player_index);
	public static int SetJoystickPlayerIndex(Joystick joystick, int player_index) => 
		SDL_SetJoystickPlayerIndex(joystick.Handle, player_index);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial GUID SDL_GetJoystickGUID(IntPtr joystick);
	public static GUID GetJoystickGUID(Joystick joystick) => SDL_GetJoystickGUID(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickVendor(IntPtr joystick);
	public static ushort GetJoystickVendor(Joystick joystick) => SDL_GetJoystickVendor(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProduct(IntPtr joystick);
	public static ushort GetJoystickProduct(Joystick joystick) => SDL_GetJoystickProduct(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickProductVersion(IntPtr joystick);
	public static ushort GetJoystickProductVersion(Joystick joystick) => SDL_GetJoystickProductVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	private static partial ushort SDL_GetJoystickFirmwareVersion(IntPtr joystick);
	public static ushort GetJoystickFirmwareVersion(Joystick joystick) => 
		SDL_GetJoystickFirmwareVersion(joystick.Handle);
	
	
	[LibraryImport(SDLLibrary)]
	[UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
	[return: MarshalAs(UnmanagedType.LPUTF8Str)]
	private static partial string SDL_GetJoystickSerial(IntPtr joystick);
	public static string GetJoystickSerial(Joystick joystick) => SDL_GetJoystickSerial(joystick.Handle);
}