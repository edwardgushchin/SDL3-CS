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
 * # CategoryClipboard
 *
 * SDL provides access to the system clipboard, both for reading information
 * from other processes and publishing information of its own.
 *
 * This is not just text! SDL apps can access and publish data by mimetype.
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetClipboardText([MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetClipboardText(const char *text);</code>
    /// <summary>
    /// <para>Put UTF-8 text into the clipboard.</para>
    /// </summary>
    /// <param name="text">the text to store in the clipboard.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="HasClipboardText"/>
    public static int SetClipboardText(string text) => SDL_SetClipboardText(text);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClipboardText();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetClipboardText(void);</code>
    /// <summary>
    /// <para>Get UTF-8 text from the clipboard.</para>
    /// <para>This functions returns empty string if there was not enough memory left for
    /// a copy of the clipboard's content.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <returns>the clipboard text on success or an empty string on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    public static string GetClipboardText()
    {
        var ptr = SDL_GetClipboardText();
        
        if (ptr == IntPtr.Zero) return string.Empty;
        
        return Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
    }

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasClipboardText();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasClipboardText(void);</code>
    /// <summary>
    /// <para>Query whether the clipboard exists and contains a non-empty text string.</para>
    /// </summary>
    /// <returns><c>true</c> if the clipboard has text, or <c>false</c> if it does not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetClipboardText"/>
    /// <seealso cref="SetClipboardText"/>
    public static bool HasClipboardText() => SDL_HasClipboardText();

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetPrimarySelectionText([MarshalAs(UnmanagedType.LPUTF8Str)] string text);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetPrimarySelectionText(const char *text);</code>
    /// <summary>
    /// <para>Put UTF-8 text into the primary selection.</para>
    /// </summary>
    /// <param name="text">the text to store in the primary selection.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="HasPrimarySelectionText"/>
    public static int SetPrimarySelectionText(string text) => SDL_SetPrimarySelectionText(text);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPrimarySelectionText();
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetPrimarySelectionText(void);</code>
    /// <summary>
    /// <para>Get UTF-8 text from the primary selection.</para>
    /// <para>This functions returns empty string if there was not enough memory left for
    /// a copy of the primary selection's content.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <returns>the primary selection text on success or an empty string on
    ///          failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    public static string GetPrimarySelectionText()
    {
        var ptr = SDL_GetPrimarySelectionText();
        
        if (ptr == IntPtr.Zero) return string.Empty;
        
        return Marshal.PtrToStringUTF8(ptr) ?? string.Empty;
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasPrimarySelectionText();
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasPrimarySelectionText(void);</code>
    /// <summary>
    /// <para>Query whether the primary selection exists and contains a non-empty text
    /// string.</para>
    /// </summary>
    /// <returns><c>true</c> if the primary selection has text, or <c>false</c> if it
    ///          does not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetPrimarySelectionText"/>
    /// <seealso cref="SetPrimarySelectionText"/>
    public static bool HasPrimarySelectionText() => SDL_HasPrimarySelectionText();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetClipboardData(ClipboardDataCallback callback, ClipboardCleanupCallback cleanup, 
        IntPtr userdata, IntPtr mimeTypes, IntPtr numMimeTypes
    );
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetClipboardData(SDL_ClipboardDataCallback callback, SDL_ClipboardCleanupCallback cleanup, void *userdata, const char **mime_types, size_t num_mime_types);</code>
    /// <summary>
    /// <para>Offer clipboard data to the OS.</para>
    /// <para>Tell the operating system that the application is offering clipboard data
    /// for each of the provided mime-types. Once another application requests the
    /// data the callback function will be called allowing it to generate and
    /// respond with the data for the requested mime-type.</para>
    /// <para>The size of text data does not include any terminator, and the text does
    /// not need to be null terminated (e.g. you can directly copy a portion of a
    /// document)</para>
    /// <param name="callback">a function pointer to the function that provides the
    /// clipboard data.</param>
    /// <param name="cleanup">a function pointer to the function that cleans up the
    /// clipboard data.</param>
    /// <param name="userdata">an opaque pointer that will be forwarded to the callbacks.</param>
    /// <param name="mimeTypes">a list of mime-types that are being offered.</param>
    /// <param name="numMimeTypes">the number of mime-types in the mime_types list.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="ClearClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    /// <seealso cref="HasClipboardData"/>
    /// </summary>
    public static int SetClipboardData(ClipboardDataCallback callback, ClipboardCleanupCallback cleanup, 
        object? userdata, string[] mimeTypes, int numMimeTypes)
    {
        var mimeTypesPtr = Marshal.AllocHGlobal(numMimeTypes * IntPtr.Size);
        try
        {
            for (var i = 0; i < numMimeTypes; i++)
            {
                var stringPtr = Marshal.StringToHGlobalUni(mimeTypes[i]);
                Marshal.WriteIntPtr(mimeTypesPtr, i * IntPtr.Size, stringPtr);
            }
            
            var userdataPtr = userdata != null ? GCHandle.ToIntPtr(GCHandle.Alloc(userdata)) : IntPtr.Zero;

            return SDL_SetClipboardData(callback, cleanup, userdataPtr, mimeTypesPtr, numMimeTypes);
        }
        finally
        {
            for (var i = 0; i < numMimeTypes; i++)
            {
                var stringPtr = Marshal.ReadIntPtr(mimeTypesPtr, i * IntPtr.Size);
                Marshal.FreeHGlobal(stringPtr);
            }
            Marshal.FreeHGlobal(mimeTypesPtr);
        }
    }
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ClearClipboardData();
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ClearClipboardData(void);</code>
    /// <summary>
    /// Clear the clipboard data.
    /// </summary>
    /// <returns><c>0</c> on success or a negative error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetClipboardData"/>
    public static int ClearClipboardData() => SDL_ClearClipboardData();

    
    // Определяем функцию для получения данных из буфера обмена
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetClipboardData(
        [MarshalAs(UnmanagedType.LPUTF8Str)] string mimeType, out nuint size);
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_GetClipboardData(const char *mime_type, size_t *size);</code>
    /// <summary>
    /// Get the data from clipboard for a given mime type.
    /// </summary>
    /// <para>The size of text data does not include the terminator, but the text is
    /// guaranteed to be null terminated.</para>
    /// <param name="mimeType">the mime type to read from the clipboard.</param>
    /// <returns>the retrieved data buffer or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information. Caller must call <see cref="Free"/>
    /// on the returned pointer when done with it.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasClipboardData"/>
    /// <seealso cref="SetClipboardData"/>
    public static byte[]? GetClipboardData(string mimeType)
    {
        var dataPtr = SDL_GetClipboardData(mimeType, out var size);
        
        if (dataPtr == IntPtr.Zero) return null;
    
        try
        {
            var data = new byte[size];
            
            Marshal.Copy(dataPtr, data, 0, (int)size);
        
            return data;
        }
        finally
        {
            Marshal.FreeHGlobal(dataPtr);
        }
    }

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(SDLBool)]
    private static partial bool SDL_HasClipboardData([MarshalAs(UnmanagedType.LPUTF8Str)] string mimeType);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasClipboardData(const char *mime_type);</code>
    /// <summary>
    /// Query whether there is data in the clipboard for the provided mime type.
    /// </summary>
    /// <param name="mimeType">the mime type to check for data for.</param>
    /// <returns><c>true</c> if there exists data in clipboard for the provided mime type, <c>false</c> if it does not.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="SetClipboardData"/>
    /// <seealso cref="GetClipboardData"/>
    public static bool HasClipboardData(string mimeType) => SDL_HasClipboardData(mimeType);

}