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
    [Macro]
	public static uint FourCC(char a, char b, char c, char d)
	{
		return (uint)(a | (b << 8) | (c << 16) | (d << 24));
	}

	
	/// <summary>
	/// <para>A macro to initialize an SDL interface.</para>
	/// <para>This macro will initialize an SDL interface structure and should be called
	/// before you fill out the fields with your implementation.</para>
	/// <para>You can use it like this:</para>
	/// <code>
	///	SDL_IOStreamInterface iface;
	///
	/// SDL_INIT_INTERFACE(&amp;iface);
	///
	/// // Fill in the interface function pointers with your implementation
	/// iface.seek = ...
	///
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// <para>If you are using designated initializers, you can use the size of the
	/// interface as the version, e.g.</para>
	/// <code>
	/// SDL_IOStreamInterface iface = {
	/// .version = sizeof(iface),
	/// .seek = ...
	/// };
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// </summary>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	/// <seealso cref="IOStreamInterface"/>
	/// <seealso cref="StorageInterface"/>
	/// <seealso cref="VirtualJoystickDesc"/>
	[Macro]
	public static void InitInterface(ref IOStreamInterface iface)
	{
		var ptr = StructureToPointer<IOStreamInterface>(iface);

		try
		{
			var size = (uint)Marshal.SizeOf(iface);
		
			Memset(ptr, 0, size);
			iface.Version = size;
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
	}
	
	
	/// <summary>
	/// <para>A macro to initialize an SDL interface.</para>
	/// <para>This macro will initialize an SDL interface structure and should be called
	/// before you fill out the fields with your implementation.</para>
	/// <para>You can use it like this:</para>
	/// <code>
	///	SDL_IOStreamInterface iface;
	///
	/// SDL_INIT_INTERFACE(&amp;iface);
	///
	/// // Fill in the interface function pointers with your implementation
	/// iface.seek = ...
	///
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// <para>If you are using designated initializers, you can use the size of the
	/// interface as the version, e.g.</para>
	/// <code>
	/// SDL_IOStreamInterface iface = {
	/// .version = sizeof(iface),
	/// .seek = ...
	/// };
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// </summary>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	/// <seealso cref="IOStreamInterface"/>
	/// <seealso cref="StorageInterface"/>
	/// <seealso cref="VirtualJoystickDesc"/>
	[Macro]
	public static void InitInterface(ref StorageInterface iface)
	{
		var ptr = StructureToPointer<StorageInterface>(iface);

		try
		{
			var size = (uint)Marshal.SizeOf(iface);
		
			Memset(ptr, 0, size);
			iface.Version = size;
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
	}
	
	
	/// <summary>
	/// <para>A macro to initialize an SDL interface.</para>
	/// <para>This macro will initialize an SDL interface structure and should be called
	/// before you fill out the fields with your implementation.</para>
	/// <para>You can use it like this:</para>
	/// <code>
	///	SDL_IOStreamInterface iface;
	///
	/// SDL_INIT_INTERFACE(&amp;iface);
	///
	/// // Fill in the interface function pointers with your implementation
	/// iface.seek = ...
	///
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// <para>If you are using designated initializers, you can use the size of the
	/// interface as the version, e.g.</para>
	/// <code>
	/// SDL_IOStreamInterface iface = {
	/// .version = sizeof(iface),
	/// .seek = ...
	/// };
	/// stream = SDL_OpenIO(&amp;iface, NULL);
	/// </code>
	/// </summary>
	/// <threadsafety>It is safe to call this macro from any thread.</threadsafety>
	/// <since>This macro is available since SDL 3.2.0</since>
	/// <seealso cref="IOStreamInterface"/>
	/// <seealso cref="StorageInterface"/>
	/// <seealso cref="VirtualJoystickDesc"/>
	[Macro]
	public static void InitInterface(ref VirtualJoystickDesc iface)
	{
		var ptr = StructureToPointer<VirtualJoystickDesc>(iface);

		try
		{
			var size = (uint)Marshal.SizeOf(iface);
		
			Memset(ptr, 0, size);
			iface.Version = size;
		}
		finally
		{
			Marshal.FreeHGlobal(ptr);
		}
	}
}