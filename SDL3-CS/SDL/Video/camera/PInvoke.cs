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
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetNumCameraDrivers"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetNumCameraDrivers();
    private delegate int GetNumCameraDriversNativeDelegate();
    private static GetNumCameraDriversNativeDelegate GetNumCameraDriversNativeFunction = SDL_GetNumCameraDrivers;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetNumCameraDrivers(void);</code>
    /// <summary>
    /// <para>Use this function to get the number of built-in camera drivers.</para>
    /// <para>This function returns a hardcoded number. This never returns a negative
    /// value; if there are no drivers compiled into this build of SDL, this
    /// function returns zero. The presence of a driver in this list does not mean
    /// it will function, it just means SDL is capable of interacting with that
    /// interface. For example, a build of SDL might have v4l2 support, but if
    /// there's no kernel support available, SDL's v4l2 driver would fail if used.</para>
    /// <para>By default, SDL tries all drivers, in its preferred order, until one is
    /// found to be usable.</para>
    /// </summary>
    /// <returns>the number of built-in camera drivers.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameraDriver"/>
    public static int GetNumCameraDrivers()
    {
        return GetNumCameraDriversNativeFunction();
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCameraDriver(int index);
    private delegate IntPtr GetCameraDriverNativeDelegate(int index);
    private static GetCameraDriverNativeDelegate GetCameraDriverNativeFunction = SDL_GetCameraDriver;
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetCameraDriver(int index);</code>
    /// <summary>
    /// <para>Use this function to get the name of a built in camera driver.</para>
    /// <para>The list of camera drivers is given in the order that they are normally
    /// initialized by default; the drivers that seem more reasonable to choose
    /// first (as far as the SDL developers believe) are earlier in the list.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "v4l2",
    /// "coremedia" or "android". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <param name="index">the index of the camera driver; the value ranges from 0 to
    /// <see cref="GetNumCameraDrivers"/> - 1.</param>
    /// <returns>the name of the camera driver at the requested index, or <c>null</c> if
    /// an invalid index was specified.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetNumCameraDrivers"/>
    public static string? GetCameraDriver(int index)
    {
        var value = GetCameraDriverNativeFunction(index);

        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCurrentCameraDriver"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCurrentCameraDriver();
    private delegate IntPtr GetCurrentCameraDriverNativeDelegate();
    private static GetCurrentCameraDriverNativeDelegate GetCurrentCameraDriverNativeFunction = SDL_GetCurrentCameraDriver;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetCurrentCameraDriver(void);</code>
    /// <summary>
    /// <para>Get the name of the current camera driver.</para>
    /// <para>The names of drivers are all simple, low-ASCII identifiers, like "v4l2",
    /// "coremedia" or "android". These never have Unicode characters, and are not
    /// meant to be proper names.</para>
    /// </summary>
    /// <returns>the name of the current camera driver or <c>null</c> if no driver has
    /// been initialized.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetCurrentCameraDriver()
    {
        var value = GetCurrentCameraDriverNativeFunction();
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameras"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCameras(out int count);
    private delegate IntPtr GetCamerasNativeDelegate(out int count);
    private static GetCamerasNativeDelegate GetCamerasNativeFunction = SDL_GetCameras;
    /// <code>extern SDL_DECLSPEC SDL_CameraID * SDLCALL SDL_GetCameras(int *count);</code>
    /// <summary>
    /// Get a list of currently connected camera devices.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of cameras returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of camera instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    public static uint[]? GetCameras(out int count)
    {
        var ptr = GetCamerasNativeFunction(out count);

        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraSupportedFormats"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCameraSupportedFormats(uint instanceId, out int count);
    private delegate IntPtr GetCameraSupportedFormatsNativeDelegate(uint instanceId, out int count);
    private static GetCameraSupportedFormatsNativeDelegate GetCameraSupportedFormatsNativeFunction = SDL_GetCameraSupportedFormats;
    /// <code>extern SDL_DECLSPEC SDL_CameraSpec *SDLCALL SDL_GetCameraSupportedFormats(SDL_CameraID devid, int *count);</code>
    /// <summary>
    /// <para>Get the list of native formats/sizes a camera supports.</para>
    /// <para>This returns a list of all formats and frame sizes that a specific camera
    /// can offer. This is useful if your app can accept a variety of image formats
    /// and sizes and so want to find the optimal spec that doesn't require
    /// conversion.</para>
    /// <para>This function isn't strictly required; if you call <see cref="OpenCamera(uint, nint)"/> with a
    /// <c>null</c> spec, SDL will choose a native format for you, and if you instead
    /// specify a desired format, it will transparently convert to the requested
    /// format on your behalf.</para>
    /// <para>If <c>count</c> is not <c>null</c>, it will be filled with the number of elements in
    /// the returned array.</para>
    /// <para>Note that it's legal for a camera to supply an empty list. This is what
    /// will happen on Emscripten builds, since that platform won't tell _anything_
    /// about available cameras until you've opened one, and won't even tell if
    /// there _is_ a camera until the user has given you permission to check
    /// through a scary warning popup.</para>
    /// </summary>
    /// <param name="instanceId">the camera device instance ID.</param>
    /// <param name="count">a pointer filled in with the number of elements in the list,
    /// may be <c>null</c>.</param>
    /// <returns>a <c>null</c> terminated array of pointers to <see cref="CameraSpec"/> or <c>null</c> on
    /// failure; call <see cref="GetError"/> for more information. This is a
    /// single allocation that should be freed with <see cref="Free"/> when it is
    /// no longer needed.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameras"/>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    public static CameraSpec[]? GetCameraSupportedFormats(uint instanceId, out int count)
    {
        var ptr = GetCameraSupportedFormatsNativeFunction(instanceId, out count);

        try
        {
            return PointerToStructureArray<CameraSpec>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetCameraName(uint instanceId);
    private delegate IntPtr GetCameraNameNativeDelegate(uint instanceId);
    private static GetCameraNameNativeDelegate GetCameraNameNativeFunction = SDL_GetCameraName;
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetCameraName(SDL_CameraID instance_id);</code>
    /// <summary>
    /// <para>Get the human-readable device name for a camera.</para>
    /// </summary>
    /// <param name="instanceId">the camera device instance ID.</param>
    /// <returns>a human-readable device name or <c>null</c> on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameras"/>
    public static string? GetCameraName(uint instanceId)
    {
        var value = GetCameraNameNativeFunction(instanceId);
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraPosition"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraPosition SDL_GetCameraPosition(uint instanceId);
    private delegate CameraPosition GetCameraPositionNativeDelegate(uint instanceId);
    private static GetCameraPositionNativeDelegate GetCameraPositionNativeFunction = SDL_GetCameraPosition;

    /// <code>extern SDL_DECLSPEC SDL_CameraPosition SDLCALL SDL_GetCameraPosition(SDL_CameraID instance_id);</code>
    /// <summary>
    /// <para>Get the position of the camera in relation to the system.</para>
    /// <para>Most platforms will report <see cref="CameraPosition.Unknown"/>, but mobile devices, like phones, can
    /// often make a distinction between cameras on the front of the device (that
    /// points towards the user, for taking "selfies") and cameras on the back (for
    /// filming in the direction the user is facing).</para>
    /// </summary>
    /// <param name="instanceId">the camera device instance ID.</param>
    /// <returns>the position of the camera on the system hardware.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameras"/>
    public static CameraPosition GetCameraPosition(uint instanceId)
    {
        return GetCameraPositionNativeFunction(instanceId);
    }



    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenCamera"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenCamera(uint instanceId, IntPtr spec);
    private delegate IntPtr OpenCameraWithPointerNativeDelegate(uint instanceId, IntPtr spec);
    private static OpenCameraWithPointerNativeDelegate OpenCameraWithPointerNativeFunction = SDL_OpenCamera;

    /// <code>extern SDL_DECLSPEC SDL_Camera * SDLCALL SDL_OpenCamera(SDL_CameraID instance_id, const SDL_CameraSpec *spec);</code>
    /// <summary>
    /// <para>Open a video recording device (a "camera").</para>
    /// <para>You can open the device with any reasonable spec, and if the hardware can't
    /// directly support it, it will convert data seamlessly to the requested
    /// format. This might incur overhead, including scaling of image data.</para>
    /// <para>If you would rather accept whatever format the device offers, you can pass
    /// a <c>null</c> spec here and it will choose one for you (and you can use
    /// <see cref="Surface"/>'s conversion/scaling functions directly if necessary).</para>
    /// <para>You can call <see cref="GetCameraFormat"/> to get the actual data format if passing
    /// a <c>null</c> spec here. You can see the exact specs a device can support without
    /// conversion with <see cref="GetCameraSupportedFormats"/>.</para>
    /// <para>SDL will not attempt to emulate framerate; it will try to set the hardware
    /// to the rate closest to the requested speed, but it won't attempt to limit
    /// or duplicate frames artificially; call <see cref="GetCameraFormat"/> to see the
    /// actual framerate of the opened the device, and check your timestamps if
    /// this is crucial to your app!</para>
    /// <para>Note that the camera is not usable until the user approves its use! On some
    /// platforms, the operating system will prompt the user to permit access to
    /// the camera, and they can choose Yes or No at that point. Until they do, the
    /// camera will not be usable. The app should either wait for an
    /// <see cref="EventType.CameraDeviceApproved"/> (or <see cref="EventType.CameraDeviceDenied"/>) event,
    /// or poll <see cref="GetCameraPermissionState"/> occasionally until it returns
    /// non-zero. On platforms that don't require explicit user approval (and
    /// perhaps in places where the user previously permitted access), the approval
    /// event might come immediately, but it might come seconds, minutes, or hours
    /// later!</para>
    /// </summary>
    /// <param name="instanceId">the camera device instance ID.</param>
    /// <param name="spec">the desired format for data the device will provide. Can be
    /// <c>null</c>.</param>
    /// <returns>an SDL_Camera object or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameras"/>
    /// <seealso cref="GetCameraFormat"/>
    public static IntPtr OpenCamera(uint instanceId, IntPtr spec)
    {
        return OpenCameraWithPointerNativeFunction(instanceId, spec);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenCamera"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenCamera(uint instanceId, in CameraSpec spec);
    private delegate IntPtr OpenCameraWithSpecNativeDelegate(uint instanceId, in CameraSpec spec);
    private static OpenCameraWithSpecNativeDelegate OpenCameraWithSpecNativeFunction = SDL_OpenCamera;

    /// <code>extern SDL_DECLSPEC SDL_Camera * SDLCALL SDL_OpenCamera(SDL_CameraID instance_id, const SDL_CameraSpec *spec);</code>
    /// <summary>
    /// <para>Open a video recording device (a "camera").</para>
    /// <para>You can open the device with any reasonable spec, and if the hardware can't
    /// directly support it, it will convert data seamlessly to the requested
    /// format. This might incur overhead, including scaling of image data.</para>
    /// <para>If you would rather accept whatever format the device offers, you can pass
    /// a <c>null</c> spec here and it will choose one for you (and you can use
    /// <see cref="Surface"/>'s conversion/scaling functions directly if necessary).</para>
    /// <para>You can call <see cref="GetCameraFormat"/> to get the actual data format if passing
    /// a <c>null</c> spec here. You can see the exact specs a device can support without
    /// conversion with <see cref="GetCameraSupportedFormats"/>.</para>
    /// <para>SDL will not attempt to emulate framerate; it will try to set the hardware
    /// to the rate closest to the requested speed, but it won't attempt to limit
    /// or duplicate frames artificially; call <see cref="GetCameraFormat"/> to see the
    /// actual framerate of the opened the device, and check your timestamps if
    /// this is crucial to your app!</para>
    /// <para>Note that the camera is not usable until the user approves its use! On some
    /// platforms, the operating system will prompt the user to permit access to
    /// the camera, and they can choose Yes or No at that point. Until they do, the
    /// camera will not be usable. The app should either wait for an
    /// <see cref="EventType.CameraDeviceApproved"/> (or <see cref="EventType.CameraDeviceDenied"/>) event,
    /// or poll <see cref="GetCameraPermissionState"/> occasionally until it returns
    /// non-zero. On platforms that don't require explicit user approval (and
    /// perhaps in places where the user previously permitted access), the approval
    /// event might come immediately, but it might come seconds, minutes, or hours
    /// later!</para>
    /// </summary>
    /// <param name="instanceId">the camera device instance ID.</param>
    /// <param name="spec">the desired format for data the device will provide. Can be
    /// <c>null</c>.</param>
    /// <returns>an SDL_Camera object or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetCameras"/>
    /// <seealso cref="GetCameraFormat"/>
    public static IntPtr OpenCamera(uint instanceId, in CameraSpec spec)
    {
        return OpenCameraWithSpecNativeFunction(instanceId, in spec);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraPermissionState"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraPermissionState SDL_GetCameraPermissionState(IntPtr camera);
    private delegate CameraPermissionState GetCameraPermissionStateNativeDelegate(IntPtr camera);
    private static GetCameraPermissionStateNativeDelegate GetCameraPermissionStateNativeFunction = SDL_GetCameraPermissionState;

    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetCameraPermissionState(SDL_Camera *camera);</code>
    /// <summary>
    /// <para>Query if camera access has been approved by the user.</para>
    /// <para>Cameras will not function between when the device is opened by the app and
    /// when the user permits access to the hardware. On some platforms, this
    /// presents as a popup dialog where the user has to explicitly approve access;
    /// on others the approval might be implicit and not alert the user at all.</para>
    /// <para>This function can be used to check the status of that approval. It will
    /// return <see cref="CameraPermissionState.Pending"/> if waiting for user response,
    /// <see cref="CameraPermissionState.Approved"/> if the camera is approved for use, and
    /// <see cref="CameraPermissionState.Denied"/> if the user denied access.</para>
    /// <para>Instead of polling with this function, you can wait for a
    /// <see cref="EventType.CameraDeviceApproved"/> (or <see cref="EventType.CameraDeviceDenied"/>) event
    /// in the standard SDL event loop, which is guaranteed to be sent once when
    /// permission to use the camera is decided.</para>
    /// <para>If a camera is declined, there's nothing to be done but call
    /// <see cref="CloseCamera"/> to dispose of it.</para>
    /// </summary>
    /// <param name="camera">the opened camera device to query.</param>
    /// <returns>a <see cref="CameraPermissionState"/> value indicating if access is
    /// granted, or <see cref="CameraPermissionState.Pending"/> if the decision
    /// is still pending.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    /// <seealso cref="CloseCamera"/>
    public static CameraPermissionState GetCameraPermissionState(IntPtr camera)
    {
        return GetCameraPermissionStateNativeFunction(camera);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetCameraID(IntPtr camera);
    private delegate uint GetCameraIDNativeDelegate(IntPtr camera);
    private static GetCameraIDNativeDelegate GetCameraIDNativeFunction = SDL_GetCameraID;

    /// <code>extern SDL_DECLSPEC SDL_CameraID SDLCALL SDL_GetCameraID(SDL_Camera *camera);</code>
    /// <summary>
    /// Get the instance ID of an opened camera.
    /// </summary>
    /// <param name="camera">an SDL_Camera to query.</param>
    /// <returns>the instance ID of the specified camera on success or 0 on
    /// failure; call <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    public static uint GetCameraID(IntPtr camera)
    {
        return GetCameraIDNativeFunction(camera);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetCameraProperties(IntPtr camera);
    private delegate uint GetCameraPropertiesNativeDelegate(IntPtr camera);
    private static GetCameraPropertiesNativeDelegate GetCameraPropertiesNativeFunction = SDL_GetCameraProperties;

    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetCameraProperties(SDL_Camera *camera);</code>
    /// <summary>
    /// Get the properties associated with an opened camera.
    /// </summary>
    /// <param name="camera">the SDL_Camera obtained from <see cref="OpenCamera(uint, nint)"/>.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint GetCameraProperties(IntPtr camera)
    {
        return GetCameraPropertiesNativeFunction(camera);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetCameraFormat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetCameraFormat(IntPtr camera, out CameraSpec spec);
    private delegate int GetCameraFormatNativeDelegate(IntPtr camera, out CameraSpec spec);
    private static GetCameraFormatNativeDelegate GetCameraFormatNativeFunction = SDL_GetCameraFormat;

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetCameraFormat(SDL_Camera *camera, SDL_CameraSpec *spec);</code>
    /// <summary>
    /// <para>Get the spec that a camera is using when generating images.</para>
    /// <para>Note that this might not be the native format of the hardware, as SDL might
    /// be converting to this format behind the scenes.</para>
    /// <para>If the system is waiting for the user to approve access to the camera, as
    /// some platforms require, this will return <c>false</c>, but this isn't necessarily
    /// a fatal error; you should either wait for an
    /// <see cref="EventType.CameraDeviceApproved"/> (or <see cref="EventType.CameraDeviceDenied"/>) event,
    /// or poll <see cref="GetCameraPermissionState"/> occasionally until it returns
    /// non-zero.</para>
    /// </summary>
    /// <param name="camera">opened camera device.</param>
    /// <param name="spec">the <see cref="CameraSpec"/> to be initialized by this function.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    public static int GetCameraFormat(IntPtr camera, out CameraSpec spec)
    {
        return GetCameraFormatNativeFunction(camera, out spec);
    }



    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_AcquireCameraFrame"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_AcquireCameraFrame(IntPtr camera, out ulong timestampNS);
    private delegate IntPtr AcquireCameraFrameNativeDelegate(IntPtr camera, out ulong timestampNS);
    private static AcquireCameraFrameNativeDelegate AcquireCameraFrameNativeFunction = SDL_AcquireCameraFrame;

    /// <code>extern SDL_DECLSPEC SDL_Surface * SDLCALL SDL_AcquireCameraFrame(SDL_Camera *camera, Uint64 *timestampNS);</code>
    /// <summary>
    /// <para>Acquire a frame.</para>
    /// <para>The frame is a memory pointer to the image data, whose size and format are
    /// given by the spec requested when opening the device.</para>
    /// <para>This is a non blocking API. If there is a frame available, a non-<c>null</c>
    /// surface is returned, and timestampNS will be filled with a non-zero value.</para>
    /// <para>Note that an error case can also return <c>null</c>, but a <c>null</c> by itself is
    /// normal and just signifies that a new frame is not yet available. Note that
    /// even if a camera device fails outright (a USB camera is unplugged while in
    /// use, etc), SDL will send an event separately to notify the app, but
    /// continue to provide blank frames at ongoing intervals until
    /// <see cref="CloseCamera"/> is called, so real failure here is almost always an out
    /// of memory condition.</para>
    /// <para>After use, the frame should be released with <see cref="ReleaseCameraFrame"/>. If
    /// you don't do this, the system may stop providing more video!</para>
    /// <para>Do not call <see cref="DestroySurface"/> on the returned surface! It must be given
    /// back to the camera subsystem with <see cref="ReleaseCameraFrame"/>!</para>
    /// <para>If the system is waiting for the user to approve access to the camera, as
    /// some platforms require, this will return <c>null</c> (no frames available); you
    /// should either wait for an <see cref="EventType.CameraDeviceApproved"/> (or
    /// <see cref="EventType.CameraDeviceDenied"/>) event, or poll
    /// <see cref="GetCameraPermissionState"/> occasionally until it returns non-zero.</para>
    /// </summary>
    /// <param name="camera">opened camera device.</param>
    /// <param name="timestampNS">a pointer filled in with the frame's timestamp, or 0 on
    /// error. Can be <c>null</c>.</param>
    /// <returns>a new frame of video on success, <c>null</c> if none is currently
    /// available.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="ReleaseCameraFrame"/>
    public static IntPtr AcquireCameraFrame(IntPtr camera, out ulong timestampNS)
    {
        return AcquireCameraFrameNativeFunction(camera, out timestampNS);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_ReleaseCameraFrame"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_ReleaseCameraFrame(IntPtr camera, IntPtr frame);
    private delegate void ReleaseCameraFrameNativeDelegate(IntPtr camera, IntPtr frame);
    private static ReleaseCameraFrameNativeDelegate ReleaseCameraFrameNativeFunction = SDL_ReleaseCameraFrame;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_ReleaseCameraFrame(SDL_Camera *camera, SDL_Surface *frame);</code>
    /// <summary>
    /// <para>Release a frame of video acquired from a camera.</para>
    /// <para>Let the back-end reuse the internal buffer for camera.</para>
    /// <para>This function _must_ be called only on surface objects returned by
    /// <see cref="AcquireCameraFrame"/>. This function should be called as quickly as
    /// possible after acquisition, as SDL keeps a small FIFO queue of surfaces for
    /// video frames; if surfaces aren't released in a timely manner, SDL may drop
    /// upcoming video frames from the camera.</para>
    /// <para>If the app needs to keep the surface for a significant time, they should
    /// make a copy of it and release the original.</para>
    /// <para>The app should not use the surface again after calling this function;
    /// assume the surface is freed and the pointer is invalid.</para>
    /// </summary>
    /// <param name="camera">opened camera device.</param>
    /// <param name="frame">the video frame surface to release.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="AcquireCameraFrame"/>
    public static void ReleaseCameraFrame(IntPtr camera, IntPtr frame)
    {
        ReleaseCameraFrameNativeFunction(camera, frame);
    }


    [ExcludeFromCodeCoverage]
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseCamera"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseCamera(IntPtr camera);
    private delegate void CloseCameraNativeDelegate(IntPtr camera);
    private static CloseCameraNativeDelegate CloseCameraNativeFunction = SDL_CloseCamera;

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseCamera(SDL_Camera *camera);</code>
    /// <summary>
    /// Use this function to shut down camera processing and close the camera
    /// device.
    /// </summary>
    /// <param name="camera">opened camera device.</param>
    /// <threadsafety>It is safe to call this function from any thread, but no
    /// thread may reference <c>device</c> once this function is called.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="OpenCamera(uint, nint)"/>
    public static void CloseCamera(IntPtr camera)
    {
        CloseCameraNativeFunction(camera);
    }

}
