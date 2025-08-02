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
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetClipboardText(const char *text);</code>
    /// <summary>
    /// Put UTF-8 text into the clipboard.
    /// </summary>
    /// <param name="text">the text to store in the clipboard.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="HasClipboardText"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetClipboardText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetClipboardText([MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetClipboardText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClipboardText();
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetClipboardText(void);</code>
    /// <summary>
    /// <para>Get UTF-8 text from the clipboard.</para>
    /// <para>This function returns an empty string if there is not enough memory left
    /// for a copy of the clipboard's content.</para>
    /// </summary>
    /// <returns>the clipboard text on success or an empty string on failure; call
    /// <see cref="GetError"/> for more information. This should be freed with
    /// <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    public static string GetClipboardText()
    {
        var value = SDL_GetClipboardText(); 
        try
        {
            return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasClipboardText(void);</code>
    /// <summary>
    /// Query whether the clipboard exists and contains a non-empty text string.
    /// </summary>
    /// <returns><c>true</c> if the clipboard has text, or <c>false</c> if it does not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasClipboardText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasClipboardText();

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetPrimarySelectionText(const char *text);</code>
    /// <summary>
    /// Put UTF-8 text into the primary selection.
    /// </summary>
    /// <param name="text">the text to store in the primary selection.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="HasPrimarySelectionText"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetPrimarySelectionText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPrimarySelectionText([MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetPrimarySelectionText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPrimarySelectionText();
    /// <code>extern SDL_DECLSPEC char * SDLCALL SDL_GetPrimarySelectionText(void);</code>
    /// <summary>
    /// <para>Get UTF-8 text from the primary selection.</para>
    /// <para>This function returns an empty string if there is not enough memory left
    /// for a copy of the primary selection's content.</para>
    /// </summary>
    /// <returns>the primary selection text on success or an empty string on
    /// failure; call <see cref="GetError"/> for more information. This should be
    /// freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    public static string GetPrimarySelectionText()
    {
        var value = SDL_GetPrimarySelectionText(); 
        try
        {
            return value == IntPtr.Zero ? "" : Marshal.PtrToStringUTF8(value)!;
        }
        finally
        {
            if(value != IntPtr.Zero) Free(value);
        }
    }


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasPrimarySelectionText(void);</code>
    /// <summary>
    /// Query whether the primary selection exists and contains a non-empty text
    /// string.
    /// </summary>
    /// <returns><c>true</c> if the primary selection has text, or <c>false</c> if it does not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasPrimarySelectionText"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasPrimarySelectionText();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetClipboardData(SDL_ClipboardDataCallback callback, SDL_ClipboardCleanupCallback cleanup, void *userdata, const char **mime_types, size_t num_mime_types);</code>
    /// <summary>
    /// <para>Offer clipboard data to the OS.</para>
    /// <para>Tell the operating system that the application is offering clipboard data
    /// for each of the proivded mime-types. Once another application requests the
    /// data the callback function will be called allowing it to generate and
    /// respond with the data for the requested mime-type.</para>
    /// <para>The size of text data does not include any terminator, and the text does
    /// not need to be null-terminated (e.g., you can directly copy a portion of a
    /// document)</para>
    /// </summary>
    /// <param name="callback">a function pointer to the function that provides the
    /// clipboard data.</param>
    /// <param name="cleanup">a function pointer to the function that cleans up the
    /// clipboard data.</param>
    /// <param name="userdata">an opaque pointer that will be forwarded to the callbacks.</param>
    /// <param name="mimeTypes">a list of mime-types that are being offered. SDL copies
    /// the given list.</param>
    /// <param name="numMimeTypes">the number of mime-types in the mime_types list.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ClearClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    /// <seealso cref="HasClipboardData"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetClipboardData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetClipboardData(ClipboardDataCallback callback, ClipboardCleanupCallback cleanup, 
        IntPtr userdata, [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.LPUTF8Str, SizeParamIndex = 4)] string[] mimeTypes, UIntPtr numMimeTypes);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ClearClipboardData(void);</code>
    /// <summary>
    /// Clear the clipboard data.
    /// </summary>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetClipboardData"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ClearClipboardData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearClipboardData();

    
    /// <code>extern SDL_DECLSPEC void * SDLCALL SDL_GetClipboardData(const char *mime_type, size_t *size);</code>
    /// <summary>
    /// <para>Get the data from the clipboard for a given mime type.</para>
    /// <para>The size of text data does not include the terminator, but the text is
    /// guaranteed to be null-terminated.</para>
    /// </summary>
    /// <param name="mimeType">the mime type to read from the clipboard.</param>
    /// <param name="size">a pointer filled in with the length of the returned data.</param>
    /// <returns>the retrieved data buffer or <c>null</c> on failure; call <see cref="GetError"/>
    /// for more information. This should be freed with <see cref="Free"/> when it
    /// is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasClipboardData"/>
    /// <seealso cref="SetClipboardData"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetClipboardData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetClipboardData([MarshalAs(UnmanagedType.LPUTF8Str)] string mimeType, out UIntPtr size);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasClipboardData(const char *mime_type);</code>
    /// <summary>
    /// Query whether there is data in the clipboard for the provided mime type.
    /// </summary>
    /// <param name="mimeType">the mime type to check for data.</param>
    /// <returns><c>true</c> if data exists in the clipboard for the provided mime type,
    /// <c>false</c> if it does not.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasClipboardData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasClipboardData([MarshalAs(UnmanagedType.LPUTF8Str)] string mimeType);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetClipboardMimeTypes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClipboardMimeTypes(out UIntPtr numMimeTypes);
    /// <code>extern SDL_DECLSPEC char ** SDLCALL SDL_GetClipboardMimeTypes(size_t *num_mime_types);</code>
    /// <summary>
    /// Retrieve the list of mime types available in the clipboard.
    /// </summary>
    /// <param name="numMimeTypes">a pointer filled with the number of mime types, may
    /// be <c>null</c>.</param>
    /// <returns>a null-terminated array of strings with mime types, or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This should be
    /// freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>This function should only be called on the main thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="SetClipboardData"/>
    public static string[]? GetClipboardMimeTypes(out UIntPtr numMimeTypes)
    {
        var ptr = SDL_GetClipboardMimeTypes(out numMimeTypes);

        try
        {
            return PointerToStringArray(ptr);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }
}