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

public partial class SDL
{
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsTraySupported(void);</code>
    /// <summary>
    /// <para>Check whether or not tray icons can be created.</para>
    /// <para>Note that this function does not guarantee that <see cref="CreateTray"/> will or
    /// will not work; you should still check <see cref="CreateTray"/> for errors.</para>
    /// <para>Using tray icons require the video subsystem.</para>
    /// </summary>
    /// <returns>true if trays are available, false otherwise.</returns>
    /// <threadsafety>This function should only be called on the main thread. It
    /// will return false if not called on the main thread.</threadsafety>
    /// <returns>This function is available since SDL 3.4.0.</returns>
    /// <seealso cref="CreateTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsTraySupported"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsTraySupported();
    
    
    /// <code>extern SDL_DECLSPEC SDL_Tray *SDLCALL SDL_CreateTray(SDL_Surface *icon, const char *tooltip);</code>
    /// <summary>
    /// <para>Create an icon to be placed in the operating system's tray, or equivalent.</para>
    /// <para>Many platforms advise not using a system tray unless persistence is a
    /// necessary feature. Avoid needlessly creating a tray icon, as the user may
    /// feel like it clutters their interface.</para>
    /// <para>Using tray icons require the video subsystem.</para>
    /// </summary>
    /// <param name="icon">a surface to be used as icon. May be <c>null</c>.</param>
    /// <param name="tooltip">a tooltip to be displayed when the mouse hovers the icon in
    /// UTF-8 encoding. Not supported on all platforms. May be <c>null</c>.</param>
    /// <returns>The newly created system tray icon.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTrayMenu"/>
    /// <seealso cref="GetTrayMenu"/>
    /// <seealso cref="DestroyTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTray"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTray(IntPtr icon, [MarshalAs(UnmanagedType.LPUTF8Str)] string? tooltip);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayIcon(SDL_Tray *tray, SDL_Surface *icon);</code>
    /// <summary>
    /// Updates the system tray icon's icon.
    /// </summary>
    /// <param name="tray">the tray icon to be updated.</param>
    /// <param name="icon">the new icon. May be <c>null</c>.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTrayIcon"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayIcon(IntPtr tray, IntPtr icon);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayTooltip(SDL_Tray *tray, const char *tooltip);</code>
    /// <summary>
    /// Updates the system tray icon's tooltip.
    /// </summary>
    /// <param name="tray">the tray icon to be updated.</param>
    /// <param name="tooltip">the new tooltip in UTF-8 encoding. May be <c>null</c>.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTrayTooltip"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayTooltip(IntPtr tray, [MarshalAs(UnmanagedType.LPUTF8Str)] string? tooltip);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayMenu *SDLCALL SDL_CreateTrayMenu(SDL_Tray *tray);</code>
    /// <summary>
    /// <para>Create a menu for a system tray.</para>
    /// <para>This should be called at most once per tray icon.</para>
    /// <para>This function does the same thing as <see cref="CreateTraySubmenu"/>, except that
    /// it takes a SDL_Tray instead of a SDL_TrayEntry.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </summary>
    /// <param name="tray">the tray to bind the menu to.</param>
    /// <returns>the newly created menu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTray"/>
    /// <seealso cref="GetTrayMenu"/>
    /// <seealso cref="GetTrayMenuParentTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTrayMenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTrayMenu(IntPtr tray);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayMenu *SDLCALL SDL_CreateTraySubmenu(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// <para>Create a submenu for a system tray entry.</para>
    /// <para>This should be called at most once per tray entry.</para>
    /// <para>This function does the same thing as <see cref="CreateTrayMenu"/>, except that it
    /// takes a SDL_TrayEntry instead of a SDL_Tray.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </summary>
    /// <param name="entry">the tray entry to bind the menu to.</param>
    /// <returns>the newly created menu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTraySubmenu"/>
    /// <seealso cref="GetTrayMenuParentEntry"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CreateTraySubmenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr CreateTraySubmenu(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayMenu *SDLCALL SDL_GetTrayMenu(SDL_Tray *tray);</code>
    /// <summary>
    /// <para>Gets a previously created tray menu.</para>
    /// <para>You should have called <see cref="CreateTrayMenu"/> on the tray object. This
    /// function allows you to fetch it again later.</para>
    /// <para>This function does the same thing as <see cref="GetTraySubmenu"/>, except that it
    /// takes a SDL_Tray instead of a SDL_TrayEntry.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </summary>
    /// <param name="tray">the tray entry to bind the menu to.</param>
    /// <returns>the newly created menu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTray"/>
    /// <seealso cref="CreateTrayMenu"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayMenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrayMenu(IntPtr tray);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayMenu *SDLCALL SDL_GetTraySubmenu(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// <para>Gets a previously created tray entry submenu.</para>
    /// <para>You should have called <see cref="CreateTraySubmenu"/> on the entry object. This
    /// function allows you to fetch it again later.</para>
    /// <para>This function does the same thing as <see cref="GetTrayMenu"/>, except that it
    /// takes a SDL_TrayEntry instead of a SDL_Tray.</para>
    /// <para>A menu does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </summary>
    /// <param name="entry">the tray entry to bind the menu to.</param>
    /// <returns>the newly created menu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="CreateTraySubmenu"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTraySubmenu"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTraySubmenu(IntPtr entry);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayEntries"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTrayEntries(IntPtr menu, out int count);
    /// <code>extern SDL_DECLSPEC const SDL_TrayEntry **SDLCALL SDL_GetTrayEntries(SDL_TrayMenu *menu, int *count);</code>
    /// <summary>
    /// Returns a list of entries in the menu, in order.
    /// </summary>
    /// <param name="menu">The menu to get entries from.</param>
    /// <param name="size">An optional pointer to obtain the number of entries in the
    /// menu.</param>
    /// <returns>a NULL-terminated list of entries within the given menu. The
    /// pointer becomes invalid when any function that inserts or deletes
    /// entries in the menu is called.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="RemoveTrayEntry"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    public static IntPtr[]? GetTrayEntries(IntPtr menu, out int size) 
    {
        var ptr = SDL_GetTrayEntries(menu, out size);
            
        try
        {
            return PointerToPointerArray(ptr, size);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_RemoveTrayEntry(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// Removes a tray entry.
    /// </summary>
    /// <param name="entry">The entry to be deleted.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RemoveTrayEntry"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveTrayEntry(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayEntry *SDLCALL SDL_InsertTrayEntryAt(SDL_TrayMenu *menu, int pos, const char *label, SDL_TrayEntryFlags flags);</code>
    /// <summary>
    /// <para>Insert a tray entry at a given position.</para>
    /// <para>If label is <c>null</c>, the entry will be a separator. Many functions won't work
    /// for an entry that is a separator.</para>
    /// <para>An entry does not need to be destroyed; it will be destroyed with the tray.</para>
    /// </summary>
    /// <param name="menu">the menu to append the entry to.</param>
    /// <param name="pos">the desired position for the new entry. Entries at or following
    /// this place will be moved. If pos is -1, the entry is appended.</param>
    /// <param name="label">the text to be displayed on the entry, in UTF-8 encoding, or
    /// <c>null</c> for a separator.</param>
    /// <param name="flags">a combination of flags, some of which are mandatory.</param>
    /// <returns>the newly created entry, or <c>null</c> if pos is out of bounds.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="TrayEntryFlags"/>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="RemoveTrayEntry"/>
    /// <seealso cref="GetTrayEntryParent"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InsertTrayEntryAt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr InsertTrayEntryAt(IntPtr menu, int pos, [MarshalAs(UnmanagedType.LPUTF8Str)] string? label, TrayEntryFlags flags);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayEntryLabel(SDL_TrayEntry *entry, const char *label);</code>
    /// <summary>
    /// <para>Sets the label of an entry.</para>
    /// <para>An entry cannot change between a separator and an ordinary entry; that is,
    /// it is not possible to set a non-NULL label on an entry that has a <c>null</c>
    /// label (separators), or to set a <c>null</c> label to an entry that has a non-NULL
    /// label. The function will silently fail if that happens.</para>
    /// </summary>
    /// <param name="entry">the entry to be updated.</param>
    /// <param name="label">the new label for the entry in UTF-8 encoding.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryLabel"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InsertTrayEntryAt"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayEntryLabel(IntPtr entry, [MarshalAs(UnmanagedType.LPUTF8Str)] string label);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayEntryLabel"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetTrayEntryLabel(IntPtr entry);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetTrayEntryLabel(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// <para>Gets the label of an entry.</para>
    /// <para>If the returned value is <c>null</c>, the entry is a separator.</para>
    /// </summary>
    /// <param name="entry">the entry to be read.</param>
    /// <returns>the label of the entry in UTF-8 encoding.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryLabel"/>
    public static string? GetTrayEntryLabel(IntPtr entry)
    {
    	var value = SDL_GetTrayEntryLabel(entry); 
    	return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayEntryChecked(SDL_TrayEntry *entry, bool checked);</code>
    /// <summary>
    /// <para>Sets whether or not an entry is checked.</para>
    /// <para>The entry must have been created with the <see cref="TrayEntryFlags.CheckBox"/> flag.</para>
    /// </summary>
    /// <param name="entry">the entry to be updated.</param>
    /// <param name="checked"><c>true</c> if the entry should be checked; <c>false</c>
    /// otherwise.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryChecked"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTrayEntryChecked"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayEntryChecked(IntPtr entry, [MarshalAs(UnmanagedType.I1)] bool @checked);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTrayEntryChecked(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// <para>Gets whether or not an entry is checked.</para>
    /// <para>The entry must have been created with the <see cref="TrayEntryFlags.CheckBox"/> flag.</para>
    /// </summary>
    /// <param name="entry">the entry to be read.</param>
    /// <returns><c>true</c> if the entry is checked; <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryChecked"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayEntryChecked"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTrayEntryChecked(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayEntryEnabled(SDL_TrayEntry *entry, bool enabled);</code>
    /// <summary>
    /// Sets whether or not an entry is enabled.
    /// </summary>
    /// <param name="entry">the entry to be updated.</param>
    /// <param name="enabled"><c>true</c> if the entry should be enabled; <c>false</c>
    /// otherwise.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="GetTrayEntryEnabled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTrayEntryEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayEntryEnabled(IntPtr entry, [MarshalAs(UnmanagedType.I1)] bool enabled);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetTrayEntryEnabled(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// Gets whether or not an entry is enabled.
    /// </summary>
    /// <param name="entry">the entry to be read.</param>
    /// <returns><c>true</c> if the entry is enabled; <c>false</c> otherwise.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    /// <seealso cref="SetTrayEntryEnabled"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayEntryEnabled"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetTrayEntryEnabled(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_SetTrayEntryCallback(SDL_TrayEntry *entry, SDL_TrayCallback callback, void *userdata);</code>
    /// <summary>
    /// Sets a callback to be invoked when the entry is selected.
    /// </summary>
    /// <param name="entry">the entry to be updated.</param>
    /// <param name="callback">a callback to be invoked when the entry is selected.</param>
    /// <param name="userdata">an optional pointer to pass extra data to the callback when
    /// it will be invoked.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="GetTrayEntries"/>
    /// <seealso cref="InsertTrayEntryAt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetTrayEntryCallback"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTrayEntryCallback(IntPtr entry, TrayCallback callback, IntPtr userdata);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ClickTrayEntry(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// Simulate a click on a tray entry.
    /// </summary>
    /// <param name="entry">The entry to activate.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.10.</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClickTrayEntry"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ClickTrayEntry(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyTray(SDL_Tray *tray);</code>
    /// <summary>
    /// <para>Destroys a tray object.</para>
    /// <para>This also destroys all associated menus and entries.</para>
    /// </summary>
    /// <param name="tray">the tray icon to be destroyed.</param>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyTray"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyTray(IntPtr tray);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayMenu *SDLCALL SDL_GetTrayEntryParent(SDL_TrayEntry *entry);</code>
    /// <summary>
    /// Gets the menu contianing a certain tray entry.
    /// </summary>
    /// <param name="entry">the entry for which to get the parent menu.</param>
    /// <returns>the parent menu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="InsertTrayEntryAt"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayEntryParent"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrayEntryParent(IntPtr entry);
    
    
    /// <code>extern SDL_DECLSPEC SDL_TrayEntry *SDLCALL SDL_GetTrayMenuParentEntry(SDL_TrayMenu *menu);</code>
    /// <summary>
    /// <para>Gets the entry for which the menu is a submenu, if the current menu is a
    /// submenu.</para>
    /// <para>Either this function or <see cref="GetTrayMenuParentTray"/> will return non-NULL
    /// for any given menu.</para>
    /// </summary>
    /// <param name="menu">the menu for which to get the parent entry.</param>
    /// <returns>the parent entry, or <c>null</c> if this menu is not a submenu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTraySubmenu"/>
    /// <seealso cref="GetTrayMenuParentTray"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayMenuParentEntry"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrayMenuParentEntry(IntPtr menu);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Tray *SDLCALL SDL_GetTrayMenuParentTray(SDL_TrayMenu *menu);</code>
    /// <summary>
    /// <para>Gets the tray for which this menu is the first-level menu, if the current
    /// menu isn't a submenu.</para>
    /// <para>Either this function or <see cref="GetTrayMenuParentEntry"/> will return non-NULL
    /// for any given menu.</para>
    /// </summary>
    /// <param name="menu">the menu for which to get the parent enttrayry.</param>
    /// <returns>the parent tray, or <c>null</c> if this menu is a submenu.</returns>
    /// <threadsafety>This function should be called on the thread that created the
    /// tray.</threadsafety>
    /// <since>This function is available since SDL 3.1.8.</since>
    /// <seealso cref="CreateTrayMenu"/>
    /// <seealso cref="GetTrayMenuParentEntry"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetTrayMenuParentTray"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetTrayMenuParentTray(IntPtr menu);
    
    
    /// extern SDL_DECLSPEC void SDLCALL SDL_UpdateTrays(void);
    /// <summary>
    /// <para>Update the trays.</para>
    /// <para>This is called automatically by the event loop and is only needed if you're
    /// using trays but aren't handling SDL events.</para>
    /// </summary>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateTrays"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UpdateTrays();
}