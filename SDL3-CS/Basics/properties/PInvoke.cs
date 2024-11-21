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

public static partial class SDL
{
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetGlobalProperties();
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetGlobalProperties(void);</code>
    /// <summary>
    /// Get the global SDL properties.
    /// </summary>
    /// <returns>
    /// a valid property ID on success or 0 on failure; call <see cref="GetError"/> for more information
    /// </returns>
    public static uint GetGlobalProperties() => SDL_GetGlobalProperties();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_CreateProperties();
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_CreateProperties(void);</code>
    /// <summary>
    /// <para>Create a group of properties.</para>
    /// <para>All properties are automatically destroyed when <see cref="Quit"/> is called.</para>
    /// </summary>
    /// <returns>
    /// an ID for a new group of properties, or 0 on failure; call
    ///          <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// It is safe to call this function from any thread.
    /// </remarks>
    public static uint CreateProperties() => SDL_CreateProperties();
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_CopyProperties(uint src, uint dst);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CopyProperties(SDL_PropertiesID src, SDL_PropertiesID dst);</code>
    /// <summary>
    /// <para>Copy a group of properties.</para>
    /// <para>Copy all the properties from one group of properties to another, with the
    /// exception of properties requiring cleanup (set using
    /// <see cref="SetPointerPropertyWithCleanup"/>), which will not be copied. Any
    /// property that already exists on `dst` will be overwritten.</para>
    /// </summary>
    /// <param name="src">the properties to copy.</param>
    /// <param name="dst">the destination properties.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information</returns>
    /// <remarks>
    /// It is safe to call this function from any thread.
    /// </remarks>
    public static int CopyProperties(uint src, uint dst) => SDL_CopyProperties(src, dst);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_LockProperties(uint props);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_LockProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Lock a group of properties.</para>
    /// <para>Obtain a multi-threaded lock for these properties. Other threads will wait
    /// while trying to lock these properties until they are unlocked. Properties
    /// must be unlocked before they are destroyed.</para>
    /// <para>The lock is automatically taken when setting individual properties, this
    /// function is only needed when you want to set several properties atomically
    /// or want to guarantee that properties being queried aren't freed in another
    /// thread.</para>
    /// </summary>
    /// <param name="props">props the properties to lock.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="UnlockProperties"/>
    public static int LockProperties(uint props) => SDL_LockProperties(props);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UnlockProperties(uint props);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UnlockProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// Unlock a group of properties.
    /// </summary>
    /// <param name="props">props the properties to unlock</param>
    /// <remarks>It is safe to call this function from any thread</remarks>
    /// <seealso cref="LockProperties"/>
    public static void UnlockProperties(uint props) => SDL_UnlockProperties(props);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetPointerPropertyWithCleanup(uint props, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name, IntPtr value, CleanupPropertyCallback cleanup, 
        IntPtr userdata);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetPointerPropertyWithCleanup(SDL_PropertiesID props, const char *name, void *value, SDL_CleanupPropertyCallback cleanup, void *userdata);</code>
    /// <summary>
    /// <para>Set a pointer property in a group of properties with a cleanup function
    /// that is called when the property is deleted.</para>
    /// <para>The cleanup function is also called if setting the property fails for any
    /// reason.</para>
    /// <para>For simply setting basic data types, like numbers, bools, or strings, use
    /// SDL_SetNumberProperty, SDL_SetBooleanProperty, or SDL_SetStringProperty
    /// instead, as those functions will handle cleanup on your behalf. This
    /// function is only for more complex, custom data.</para>
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <param name="cleanup">the function to call when this property is deleted, or NULL
    ///                       if no cleanup is necessary.</param>
    /// <param name="userdata">a pointer that is passed to the cleanup function.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPointerProperty"/>
    /// <seealso cref="SetPointerProperty"/>
    /// <seealso cref="CleanupPropertyCallback"/>
    public static int SetPointerPropertyWithCleanup(uint props, string name, IntPtr value, 
        CleanupPropertyCallback cleanup, IntPtr userdata) => 
        SDL_SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetPointerProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        IntPtr value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetPointerProperty(SDL_PropertiesID props, const char *name, void *value);</code>
    /// <summary>
    /// Set a pointer property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPointerProperty"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetBooleanProperty"/>
    /// <seealso cref="SetFloatProperty"/>
    /// <seealso cref="SetNumberProperty"/>
    /// <seealso cref="SetPointerPropertyWithCleanup"/>
    /// <seealso cref="SetStringProperty"/>
    public static int SetPointerProperty(uint props, string name, IntPtr value) => 
        SDL_SetPointerProperty(props, name, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetStringProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetStringProperty(SDL_PropertiesID props, const char *name, const char *value);</code>
    /// <summary>
    /// <para>Set a string property in a group of properties.</para>
    /// <para>This function makes a copy of the string; the caller does not have to
    /// preserve the data after this call completes.</para>
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property, or NULL to delete the property.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetStringProperty"/>
    public static int SetStringProperty(uint props, string name, string? value) => 
        SDL_SetStringProperty(props, name, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetNumberProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        long value
    );
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetNumberProperty(SDL_PropertiesID props, const char *name, Sint64 value);</code>
    /// <summary>
    /// Set an integer property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetNumberProperty"/>
    public static int SetNumberProperty(uint props, string name, long value) => 
        SDL_SetNumberProperty(props, name, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetFloatProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        float value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetFloatProperty(SDL_PropertiesID props, const char *name, float value);</code>
    /// <summary>
    /// Set a floating point property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetFloatProperty"/>
    public static int SetFloatProperty(uint props, string name, float value) => 
        SDL_SetFloatProperty(props, name, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_SetBooleanProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.I1)] bool value);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_SetBooleanProperty(SDL_PropertiesID props, const char *name, SDL_bool value);</code>
    /// <summary>
    /// Set a boolean property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to modify.</param>
    /// <param name="value">the new value of the property.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetBooleanProperty"/>
    public static int SetBooleanProperty(uint props, string name, bool value) => 
        SDL_SetBooleanProperty(props, name, value);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_HasProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasProperty(SDL_PropertiesID props, const char *name);</code>
    /// <summary>
    /// Return whether a property exists in a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <returns>true if the property exists, or false if it doesn't.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPropertyType"/>
    public static bool HasProperty(uint props, string name) => SDL_HasProperty(props, name);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertyType SDL_GetPropertyType(uint props, 
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC SDL_PropertyType SDLCALL SDL_GetPropertyType(SDL_PropertiesID props, const char *name);</code>
    /// <summary>
    /// Get the type of a property in a group of properties.
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <returns>the type of the property, or <see cref="PropertyType.Invalid"/> if it is
    ///          not set.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    public static PropertyType GetPropertyType(uint props, string name) => SDL_GetPropertyType(props, name);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetPointerProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        IntPtr defaultValue
    );
    /// <code>extern SDL_DECLSPEC void *SDLCALL SDL_GetPointerProperty(SDL_PropertiesID props, const char *name, void *default_value);</code>
    /// <summary>
    /// <para>Get a pointer property from a group of properties.</para>
    /// <para>By convention, the names of properties that SDL exposes on objects will
    /// start with "SDL.", and properties that SDL uses internally will start with
    /// "SDL.internal.". These should be considered read-only and should not be
    /// modified by applications.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <returns>the value of the property, or defaultValue if it is not set or
    ///          not a pointer property.</returns>
    /// <remarks>It is safe to call this function from any thread, although
    ///               the data returned is not protected and could potentially be
    ///               freed if you call <see cref="SetPointerProperty"/> or
    ///               <see cref="ClearProperty"/> on these properties from another thread.
    ///               If you need to avoid this, use <see cref="LockProperties"/> and
    ///               <see cref="UnlockProperties"/>.</remarks>
    /// <seealso cref="GetBooleanProperty"/>
    /// <seealso cref="GetFloatProperty"/>
    /// <seealso cref="GetNumberProperty"/>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="GetStringProperty"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetPointerProperty"/>
    public static IntPtr GetPointerProperty(uint props, string name, IntPtr defaultValue) => 
        SDL_GetPointerProperty(props, name, defaultValue);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetStringProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string defaultValue
    );
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetStringProperty(SDL_PropertiesID props, const char *name, const char *default_value);</code>
    /// <summary>
    /// <para>Get a string property from a group of properties.</para>
    /// <para>The returned string follows the <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <returns>the value of the property, or defaultValue if it is not set or
    ///          not a string property.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetStringProperty"/>
    public static string GetStringProperty(uint props, string name, string defaultValue)
    {
        var result = SDL_GetStringProperty(props, name, defaultValue);
        return Marshal.PtrToStringUTF8(result) ?? defaultValue;
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long SDL_GetNumberProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        long defaultValue);
    /// <code>extern SDL_DECLSPEC Sint64 SDLCALL SDL_GetNumberProperty(SDL_PropertiesID props, const char *name, Sint64 default_value);</code>
    /// <summary>
    /// <para>Get a number property from a group of properties.</para>
    /// <para>You can use <see cref="GetPropertyType"/> to query whether the property exists and
    /// is a number property.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <returns>the value of the property, or `defaultValue` if it is not set or
    ///          not a number property.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetNumberProperty"/>
    public static long GetNumberProperty(uint props, string name, long defaultValue) => 
        SDL_GetNumberProperty(props, name, defaultValue);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float SDL_GetFloatProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        float defaultValue);
    /// <code>extern SDL_DECLSPEC float SDLCALL SDL_GetFloatProperty(SDL_PropertiesID props, const char *name, float default_value);</code>
    /// <summary>
    /// <para>Get a floating point property from a group of properties.</para>
    /// <para>You can use <see cref="GetPropertyType"/> to query whether the property exists and
    /// is a floating point property.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <returns>the value of the property, or `default_value` if it is not set or
    ///          not a float property.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    /// <seealso cref="GetPropertyType"/>
    /// <seealso cref="HasProperty"/>
    /// <seealso cref="SetFloatProperty"/>
    public static float GetFloatProperty(uint props, string name, float defaultValue) => 
        SDL_GetFloatProperty(props, name, defaultValue);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetBooleanProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name, 
        [MarshalAs(UnmanagedType.I1)] bool defaultValue);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetBooleanProperty(SDL_PropertiesID props, const char *name, SDL_bool default_value);</code>
    /// <summary>
    /// <para>Get a boolean property from a group of properties.</para>
    /// <para>You can use <see cref="GetPropertyType"/> to query whether the property exists and
    /// is a boolean property.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="name">the name of the property to query.</param>
    /// <param name="defaultValue">the default value of the property.</param>
    /// <returns>the value of the property, or `defaultValue` if it is not set or
    ///          not a float property.</returns>
    public static bool GetBooleanProperty(uint props, string name, bool defaultValue) => 
        SDL_GetBooleanProperty(props, name, defaultValue);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_ClearProperty(uint props, [MarshalAs(UnmanagedType.LPUTF8Str)] string name);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_ClearProperty(SDL_PropertiesID props, const char *name);</code>
    /// <summary>
    /// Clear a property from a group of properties.
    /// per property in the set.
    /// </summary>
    /// <param name="props">the properties to modify.</param>
    /// <param name="name">the name of the property to clear.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    public static int ClearProperty(uint props, string name) => SDL_ClearProperty(props, name);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_EnumerateProperties(uint props, EnumeratePropertiesCallback callback, 
        IntPtr userdata);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_EnumerateProperties(SDL_PropertiesID props, SDL_EnumeratePropertiesCallback callback, void *userdata);</code>
    /// <summary>
    /// <para>Enumerate the properties contained in a group of properties.</para>
    /// <para>The callback function is called for each property in the group of
    /// properties. The properties are locked during enumeration.</para>
    /// </summary>
    /// <param name="props">the properties to query.</param>
    /// <param name="callback">the function to call for each property.</param>
    /// <param name="userdata">a pointer that is passed to `callback`.</param>
    /// <returns>0 on success or a negative error code on failure; call
    ///          <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is safe to call this function from any thread.</remarks>
    public static int EnumerateProperties(uint props, EnumeratePropertiesCallback callback, IntPtr userdata) =>
        SDL_EnumerateProperties(props, callback, userdata);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_DestroyProperties(uint props);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyProperties(SDL_PropertiesID props);</code>
    /// <summary>
    /// <para>Destroy a group of properties.</para>
    /// <para>All properties are deleted and their cleanup functions will be called, if
    /// any.</para>
    /// </summary>
    /// <param name="props">the properties to destroy.</param>
    /// <remarks>This function should not be called while these properties are
    ///               locked or other threads might be setting or getting values
    ///               from these properties.</remarks>
    /// <seealso cref="CreateProperties"/>
    public static void DestroyProperties(uint props) => SDL_DestroyProperties(props);
}