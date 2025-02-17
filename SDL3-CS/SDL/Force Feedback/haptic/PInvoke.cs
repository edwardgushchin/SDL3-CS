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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHaptics"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHaptics(out int count);
    /// <code>extern SDL_DECLSPEC SDL_HapticID * SDLCALL SDL_GetHaptics(int *count);</code>
    /// <summary>
    /// Get a list of currently connected haptic devices.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of haptic devices
    /// returned, may be <c>null</c>.</param>
    /// <returns>a 0 terminated array of haptic device instance IDs or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This should be
    /// freed with <see cref="Free"/> when it is no longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenHaptic"/>
    public static int[]? GetHaptics(out int count)
    {
        var ptr = SDL_GetHaptics(out count);
        
        try
        {
            return PointerToStructureArray<int>(ptr, count);
        }
        finally
        {
            Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHapticNameForID(int instanceId);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetHapticNameForID(SDL_HapticID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a haptic device.</para>
    /// <para>This can be called before any haptic devices are opened.</para>
    /// </summary>
    /// <param name="instanceId">the haptic device instance ID.</param>
    /// <returns>the name of the selected haptic device. If no name can be found,
    /// this function returns <c>null</c>; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticName"/>
    /// <seealso cref="OpenHaptic"/>
    public static string? GetHapticNameForID(int instanceId)
    {
        var value = SDL_GetHapticNameForID(instanceId); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_Haptic * SDLCALL SDL_OpenHaptic(SDL_HapticID instance_id);</code>
    /// <summary>
    /// <para>Open a haptic device for use.</para>
    /// <para>The index passed as an argument refers to the N'th haptic device on this
    /// system.</para>
    /// <para>When opening a haptic device, its gain will be set to maximum and
    /// autocenter will be disabled. To modify these values use <see cref="SetHapticGain"/>
    /// and <see cref="SetHapticAutocenter"/>.</para>
    /// </summary>
    /// <param name="instanceId">the haptic device instance ID.</param>
    /// <returns>the device identifier or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseHaptic"/>
    /// <seealso cref="GetHaptics"/>
    /// <seealso cref="OpenHapticFromJoystick"/>
    /// <seealso cref="OpenHapticFromMouse"/>
    /// <seealso cref="SetHapticAutocenter"/>
    /// <seealso cref="SetHapticGain"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenHaptic(int instanceId);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Haptic * SDLCALL SDL_GetHapticFromID(SDL_HapticID instance_id);</code>
    /// <summary>
    /// Get the SDL_Haptic associated with an instance ID, if it has been opened.
    /// </summary>
    /// <param name="instanceId">the instance ID to get the SDL_Haptic for.</param>
    /// <returns>an SDL_Haptic on success or <c>null</c> on failure or if it hasn't been
    /// opened yet; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetHapticFromID(int instanceId);
    
    
    /// <code>extern SDL_DECLSPEC SDL_HapticID SDLCALL SDL_GetHapticID(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Get the instance ID of an opened haptic device.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <returns>the instance ID of the specified haptic device on success or 0 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetHapticID(IntPtr haptic);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetHapticName(IntPtr haptic);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetHapticName(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Get the implementation dependent name of a haptic device.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic obtained from <see cref="OpenJoystick"/>.</param>
    /// <returns>the name of the selected haptic device. If no name can be found,
    /// this function returns <c>null</c>; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticNameForID"/>
    public static string? GetHapticName(IntPtr haptic)
    {
        var value = SDL_GetHapticName(haptic); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsMouseHaptic(void);</code>
    /// <summary>
    /// Query whether or not the current mouse has haptic capabilities.
    /// </summary>
    /// <returns><c>true</c> if the mouse is haptic or <c>false</c> if it isn't.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenHapticFromMouse"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsMouseHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMouseHaptic();
    
    
    /// <code>extern SDL_DECLSPEC SDL_Haptic * SDLCALL SDL_OpenHapticFromMouse(void);</code>
    /// <summary>
    /// Try to open a haptic device from the current mouse.
    /// </summary>
    /// <returns>the haptic device identifier or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseHaptic"/>
    /// <seealso cref="IsMouseHaptic"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenHapticFromMouse"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenHapticFromMouse();
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_IsJoystickHaptic(SDL_Joystick *joystick);</code>
    /// <summary>
    /// Query if a joystick has haptic features.
    /// </summary>
    /// <param name="joystick">the SDL_Joystick to test for haptic capabilities.</param>
    /// <returns><c>true</c> if the joystick is haptic or <c>false</c> if it isn't.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenHapticFromJoystick"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_IsJoystickHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsJoystickHaptic(IntPtr joystick);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Haptic * SDLCALL SDL_OpenHapticFromJoystick(SDL_Joystick *joystick);</code>
    /// <summary>
    /// <para>Open a haptic device for use from a joystick device.</para>
    /// <para>You must still close the haptic device separately. It will not be closed
    /// with the joystick.</para>
    /// <para>When opened from a joystick you should first close the haptic device before
    /// closing the joystick device. If not, on some implementations the haptic
    /// device will also get unallocated and you'll be unable to use force feedback
    /// on that device.</para>
    /// </summary>
    /// <param name="joystick">the SDL_Joystick to create a haptic device from.</param>
    /// <returns>a valid haptic device identifier on success or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CloseHaptic"/>
    /// <seealso cref="IsJoystickHaptic"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenHapticFromJoystick"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenHapticFromJoystick(IntPtr joystick);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseHaptic(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Close a haptic device previously opened with <see cref="OpenHaptic"/>.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to close.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenHaptic"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseHaptic(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetMaxHapticEffects(SDL_Haptic *haptic);</code>
    /// <summary>
    /// <para>Get the number of effects a haptic device can store.</para>
    /// <para>On some platforms this isn't fully supported, and therefore is an
    /// approximation. Always check to see if your created effect was actually
    /// created and do not rely solely on <see cref="GetMaxHapticEffects"/>.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <returns>the number of effects the haptic device can store or a negative
    /// error code on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMaxHapticEffectsPlaying"/>
    /// <seealso cref="GetHapticFeatures"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMaxHapticEffects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMaxHapticEffects(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetMaxHapticEffectsPlaying(SDL_Haptic *haptic);</code>
    /// <summary>
    /// <para>Get the number of effects a haptic device can play at the same time.</para>
    /// <para>This is not supported on all platforms, but will always return a value.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query maximum playing effects.</param>
    /// <returns>the number of effects the haptic device can play at the same time
    /// or -1 on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetMaxHapticEffects"/>
    /// <seealso cref="GetHapticFeatures"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetMaxHapticEffectsPlaying"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMaxHapticEffectsPlaying(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC Uint32 SDLCALL SDL_GetHapticFeatures(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Get the haptic device's supported features in bitwise manner.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <returns>a list of supported haptic features in bitwise manner (OR'd), or 0
    /// on failure; call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HapticEffectSupported"/>
    /// <seealso cref="GetMaxHapticEffects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticFeatures"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetHapticFeatures(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumHapticAxes(SDL_Haptic *haptic);</code>
    /// <summary>
    /// <para>Get the number of haptic axes the device has.</para>
    /// <para>The number of haptic axes might be useful if working with the
    /// <see cref="HapticDirection"/> effect.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <returns>the number of axes on success or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumHapticAxes"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumHapticAxes(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HapticEffectSupported(SDL_Haptic *haptic, const SDL_HapticEffect *effect);</code>
    /// <summary>
    /// Check to see if an effect is supported by a haptic device.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query.</param>
    /// <param name="effect">the desired effect to query.</param>
    /// <returns><c>true</c> if the effect is supported or <c>false</c> if it isn't.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateHapticEffect"/>
    /// <seealso cref="GetHapticFeatures"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_HapticEffectSupported"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool HapticEffectSupported(IntPtr haptic, in HapticEffect effect);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_CreateHapticEffect(SDL_Haptic *haptic, const SDL_HapticEffect *effect);</code>
    /// <summary>
    /// Create a new haptic effect on a specified device.
    /// </summary>
    /// <param name="haptic">an SDL_Haptic device to create the effect on.</param>
    /// <param name="effect">an <see cref="HapticEffect"/> structure containing the properties of
    /// the effect to create.</param>
    /// <returns>the ID of the effect on success or -1 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="DestroyHapticEffect"/>
    /// <seealso cref="RunHapticEffect"/>
    /// <seealso cref="UpdateHapticEffect"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_CreateHapticEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static extern int CreateHapticEffect(IntPtr haptic, in HapticEffect effect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_UpdateHapticEffect(SDL_Haptic *haptic, int effect, const SDL_HapticEffect *data);</code>
    /// <summary>
    /// <para>Update the properties of an effect.</para>
    /// <para>Can be used dynamically, although behavior when dynamically changing
    /// direction may be strange. Specifically the effect may re-upload itself and
    /// start playing from the start. You also cannot change the type either when
    /// running <see cref="UpdateHapticEffect"/>.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device that has the effect.</param>
    /// <param name="effect">the identifier of the effect to update.</param>
    /// <param name="data"></param>
    /// <returns>an <see cref="HapticEffect"/> structure containing the new effect
    /// properties to use.</returns>
    /// <since><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</since>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateHapticEffect"/>
    /// <seealso cref="RunHapticEffect"/>
    [DllImport(SDLLibrary, EntryPoint = "SDL_UpdateHapticEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static extern bool UpdateHapticEffect(IntPtr haptic, int effect, in HapticEffect data);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_RunHapticEffect(SDL_Haptic *haptic, int effect, Uint32 iterations);</code>
    /// <summary>
    /// <para>Run the haptic effect on its associated haptic device.</para>
    /// <para>To repeat the effect over and over indefinitely, set <c>iterations</c> to
    /// <c>HAPTIC_INFINITY</c>. (Repeats the envelope - attack and fade.) To make
    /// one instance of the effect last indefinitely (so the effect does not fade),
    /// set the effect's <c>length</c> in its structure/union to <c>HAPTIC_INFINITY</c>
    /// instead.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to run the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to run.</param>
    /// <param name="iterations">the number of iterations to run the effect; use
    /// <c>HAPTIC_INFINITY</c> to repeat forever.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticEffectStatus"/>
    /// <seealso cref="StopHapticEffect"/>
    /// <seealso cref="StopHapticEffects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_RunHapticEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool RunHapticEffect(IntPtr haptic, int effect, uint iterations);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StopHapticEffect(SDL_Haptic *haptic, int effect);</code>
    /// <summary>
    /// Stop the haptic effect on its associated haptic device.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to stop the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to stop.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RunHapticEffect"/>
    /// <seealso cref="StopHapticEffects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopHapticEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopHapticEffect(IntPtr haptic, int effect);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_DestroyHapticEffect(SDL_Haptic *haptic, int effect);</code>
    /// <summary>
    /// <para>Destroy a haptic effect on the device.</para>
    /// <para>This will stop the effect if it's running. Effects are automatically
    /// destroyed when the device is closed.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to destroy the effect on.</param>
    /// <param name="effect">the ID of the haptic effect to destroy.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="CreateHapticEffect"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_DestroyHapticEffect"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyHapticEffect(IntPtr haptic, int effect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetHapticEffectStatus(SDL_Haptic *haptic, int effect);</code>
    /// <summary>
    /// <para>Get the status of the current effect on the specified haptic device.</para>
    /// <para>Device must support the <see cref="HAPTIC_STATUS"/> feature.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to query for the effect status on.</param>
    /// <param name="effect">the ID of the haptic effect to query its status.</param>
    /// <returns><c>true</c> if it is playing, <c>false</c> if it isn't playing or haptic status
    /// isn't supported.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticFeatures"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetHapticEffectStatus"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetHapticEffectStatus(IntPtr haptic, int effect);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetHapticGain(SDL_Haptic *haptic, int gain);</code>
    /// <summary>
    /// <para>Set the global gain of the specified haptic device.</para>
    /// <para>Device must support the <see cref="HAPTIC_GAIN"/> feature.</para>
    /// <para>The user may specify the maximum gain by setting the environment variable
    /// <c>HAPTIC_GAIN_MAX</c> which should be between 0 and 100. All calls to
    /// <see cref="SetHapticGain"/> will scale linearly using <c>SDL_HAPTIC_GAIN_MAX</c> as the
    /// maximum.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to set the gain on.</param>
    /// <param name="gain">value to set the gain to, should be between 0 and 100 (0 -
    /// 100).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticFeatures"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetHapticGain"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHapticGain(IntPtr haptic, int gain);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_SetHapticAutocenter(SDL_Haptic *haptic, int autocenter);</code>
    /// <summary>
    /// <para>Set the global autocenter of the device.</para>
    /// <para>Autocenter should be between 0 and 100. Setting it to 0 will disable
    /// autocentering.</para>
    /// <para>Device must support the <see cref="HAPTIC_AUTOCENTER"/> feature.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to set autocentering on.</param>
    /// <param name="autocenter">value to set autocenter to (0-100).</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetHapticFeatures"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_SetHapticAutocenter"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHapticAutocenter(IntPtr haptic, int autocenter);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PauseHaptic(SDL_Haptic *haptic);</code>
    /// <summary>
    /// <para>Pause a haptic device.</para>
    /// <para>Device must support the <c>HAPTIC_PAUSE</c> feature. Call <see cref="ResumeHaptic"/>
    /// to resume playback.</para>
    /// <para>Do not modify the effects nor add new ones while the device is paused. That
    /// can cause all sorts of weird errors.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to pause.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ResumeHaptic"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PauseHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PauseHaptic(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_ResumeHaptic(SDL_Haptic *haptic);</code>
    /// <summary>
    /// <para>Resume a haptic device.</para>
    /// <para>Call to unpause after <see cref="PauseHaptic"/>.</para>
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to unpause.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PauseHaptic"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ResumeHaptic"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResumeHaptic(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StopHapticEffects(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Stop all the currently playing effects on a haptic device.
    /// </summary>
    /// <param name="haptic">the SDL_Haptic device to stop.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RunHapticEffect"/>
    /// <seealso cref="StopHapticEffects"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopHapticEffects"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopHapticEffects(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HapticRumbleSupported(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Check whether rumble is supported on a haptic device.
    /// </summary>
    /// <param name="haptic">haptic device to check for rumble support.</param>
    /// <returns><c>true</c> if the effect is supported or <c>false</c> if it isn't.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="InitHapticRumble"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HapticRumbleSupported"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HapticRumbleSupported(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_InitHapticRumble(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Initialize a haptic device for simple rumble playback.
    /// </summary>
    /// <param name="haptic">the haptic device to initialize for simple rumble playback.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PlayHapticRumble"/>
    /// <seealso cref="StopHapticRumble"/>
    /// <seealso cref="HapticRumbleSupported"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_InitHapticRumble"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool InitHapticRumble(IntPtr haptic);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_PlayHapticRumble(SDL_Haptic *haptic, float strength, Uint32 length);</code>
    /// <summary>
    /// Run a simple rumble effect on a haptic device.
    /// </summary>
    /// <param name="haptic">the haptic device to play the rumble effect on.</param>
    /// <param name="strength">strength of the rumble to play as a 0-1 float value.</param>
    /// <param name="length">length of the rumble to play in milliseconds.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="InitHapticRumble"/>
    /// <seealso cref="StopHapticRumble"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_PlayHapticRumble"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PlayHapticRumble(IntPtr haptic, float strength, uint length);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_StopHapticRumble(SDL_Haptic *haptic);</code>
    /// <summary>
    /// Stop the simple rumble on a haptic device.
    /// </summary>
    /// <param name="haptic">the haptic device to stop the rumble effect on.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="PlayHapticRumble"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_StopHapticRumble"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool StopHapticRumble(IntPtr haptic);
}