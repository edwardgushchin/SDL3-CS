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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>Specify the behavior of Alt+Tab while the keyboard is grabbed.</para>
    /// <para>By default, SDL emulates Alt+Tab functionality while the keyboard is
    /// grabbed and your window is full-screen. This prevents the user from getting
    /// stuck in your application if you've enabled keyboard grab.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will not handle Alt+Tab. Your application is responsible for
    /// handling Alt+Tab while the keyboard is grabbed.</item>
    /// <item>"1": SDL will minimize your window when Alt+Tab is pressed (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </summary>
    public const string HintAllowAltTabWhileGrabbed = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
    
    /// <summary>
    /// <para>A variable to control whether the SDL activity is allowed to be re-created.</para>
    /// <para>If this hint is true, the activity can be recreated on demand by the OS,
    /// and Java static data and C++ static data remain with their current values.
    /// If this hint is false, then SDL will call exit() when you return from your
    /// main function and the application will be terminated and then started fresh
    /// each time.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The application starts fresh at each launch. (default)</item>
    /// <item>"1": The application activity can be recreated by the OS.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </summary>
    public const string HintAndroidAllowRecreateActivity = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";
    
    /// <summary>
    /// <para>A variable to control whether the event loop will block itself when the app
    /// is paused.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Non blocking.</item>
    /// <item>"1": Blocking. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </summary>
    public const string HintAndroidBlockOnPause = "SDL_ANDROID_BLOCK_ON_PAUSE";
    
    /// <summary>
    /// <para>A variable to control whether SDL will pause audio in background.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Not paused, requires that SDL_HINT_ANDROID_BLOCK_ON_PAUSE be set to
    /// "0"</item>
    /// <item>"1": Paused. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </summary>
    public const string HintAndroidBlockOnPausePauseaudio = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";
    
    /// <summary>
    /// <para>A variable to control whether we trap the Android back button to handle it
    /// manually.</para>
    /// <para>This is necessary for the right mouse button to work on some Android
    /// devices, or to be able to trap the back button for use in your code
    /// reliably. If this hint is true, the back button will show up as an
    /// <see cref="EventType.KeyDown"/> / <see cref="EventType.KeyUp"/> pair with a keycode of
    /// <see cref="Scancode.AcBack"/>.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Back button will be handled as usual for system. (default)</item>
    /// <item>"1": Back button will be trapped, allowing you to handle the key press
    /// manually. (This will also let right mouse click work on systems where the
    /// right mouse button functions as back.)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </summary>
    public const string HintAndroidTrapBackButton = "SDL_ANDROID_TRAP_BACK_BUTTON";
    
    /// <summary>
    /// <para>A variable setting the app ID string.</para>
    /// <para>This string is used by desktop compositors to identify and group windows
    /// together, as well as match applications with associated desktop settings
    /// and icons.</para>
    /// <para>On Wayland this corresponds to the "app ID" window property and on X11 this
    /// corresponds to the WM_CLASS property. Windows inherit the value of this
    /// hint at creation time. Changing this hint after a window has been created
    /// will not change the app ID or class of existing windows.</para>
    /// <para>For *nix platforms, this string should be formatted in reverse-DNS notation
    /// and follow some basic rules to be valid:</para>
    /// <list type="table">
    /// <item>The application ID must be composed of two or more elements separated by
    /// a period (.) character.</item>
    /// <item>Each element must contain one or more of the alphanumeric characters
    /// (A-Z, a-z, 0-9) plus underscore (_) and hyphen (-) and must not start
    /// with a digit. Note that hyphens, while technically allowed, should not be
    /// used if possible, as they are not supported by all components that use
    /// the ID, such as D-Bus. For maximum compatibility, replace hyphens with an
    /// underscore.</item>
    /// <item>The empty string is not a valid element (ie: your application ID may not
    /// start or end with a period and it is not valid to have two periods in a
    /// row).</item>
    /// <item>The entire ID must be less than 255 characters in length.</item>
    /// </list>
    /// <para>Examples of valid app ID strings:</para>
    /// <list type="bullet">
    /// <item>org.MyOrg.MyApp</item>
    /// <item>com.your_company.your_app</item>
    /// </list>
    /// <para>Desktops such as GNOME and KDE require that the app ID string matches your
    /// application's .desktop file name (e.g. if the app ID string is
    /// 'org.MyOrg.MyApp', your application's .desktop file should be named
    /// 'org.MyOrg.MyApp.desktop').</para>
    /// <para>If you plan to package your application in a container such as Flatpak, the
    /// app ID should match the name of your Flatpak container as well.</para>
    /// <para>If not set, SDL will attempt to use the application executable name. If the
    /// executable name cannot be retrieved, the generic string "SDL_App" will be
    /// used.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </summary>
    public const string HintAppId = "SDL_APP_ID";
    
    
    public const string HintAppName = "SDL_APP_NAME";
    public const string HintAppleTVControllerUIEvents = "SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS";
    public const string HintAppleTVRemoteAllowRotation = "SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION";
    public const string HintAudioCategory = "SDL_HINT_AUDIO_CATEGORY";
    public const string HintAudioDeviceAppIconName = "SDL_AUDIO_DEVICE_APP_ICON_NAME";
    public const string HintAudioDeviceAppName = "SDL_AUDIO_DEVICE_APP_NAME";
    public const string HintAudioDeviceSampleFrames = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";
    public const string HintAudioDeviceStreamName = "SDL_AUDIO_DEVICE_STREAM_NAME";
    public const string HintAudioDeviceStreamRole = "SDL_AUDIO_DEVICE_STREAM_ROLE";
    public const string HintAudioDriver = "SDL_AUDIO_DRIVER";
    public const string HintAudioIncludeMonitors = "SDL_AUDIO_INCLUDE_MONITORS";
    public const string HintAutoUpdateJoysticks = "SDL_AUTO_UPDATE_JOYSTICKS";
    public const string HintAutoUpdateSensors = "SDL_AUTO_UPDATE_SENSORS";
    public const string HintBmpSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";
    public const string HintCameraDriver = "SDL_CAMERA_DRIVER";
    public const string HintCpuFeatureMask = "SDL_CPU_FEATURE_MASK";
    public const string HintDisplayUsableBounds = "SDL_HINT_DISPLAY_USABLE_BOUNDS";
    public const string HintEmscriptenAsyncify = "SDL_EMSCRIPTEN_ASYNCIFY";
    public const string HintEmscriptenCanvasSelector = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
    public const string HintEmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
    public const string HintEnableScreenKeyboard = "SDL_ENABLE_SCREEN_KEYBOARD";
    public const string HintEventLogging = "SDL_HINT_EVENT_LOGGING";
    public const string HintFiLeDialogDriver = "SDL_FILE_DIALOG_DRIVER";
    public const string HintForceRaiseWindow = "SDL_FORCE_RAISEWINDOW";
    public const string HintFrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
    public const string HintGameControllerIgnoreDevices = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
    public const string HintGameControllerIgnoreDevicesExcept = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
    public const string HintGameControllerSensorFusion = "SDL_GAMECONTROLLER_SENSOR_FUSION";
    public const string HintGameControllerConfig = "SDL_GAMECONTROLLERCONFIG";
    public const string HintGameControllerConfigFile = "SDL_GAMECONTROLLERCONFIG_FILE";
    public const string HintGameControllerType = "SDL_GAMECONTROLLERTYPE";
    public const string HintGDKTextInputDefaultText = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
    public const string HintGDKTextInputDescription = "SDL_GDK_TEXTINPUT_DESCRIPTION";
    public const string HintGDKTextInputMaxLength = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
    public const string HintGDKTextInputScope = "SDL_GDK_TEXTINPUT_SCOPE";
    public const string HintGDKTextInputTitle = "SDL_GDK_TEXTINPUT_TITLE";
    public const string HintHIDAPIEnumerateOnlyControllers = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
    public const string HintHIDAPIIgnoreDevices = "SDL_HIDAPI_IGNORE_DEVICES";
    public const string HintIMEImplementedUI = "SDL_IME_IMPLEMENTED_UI";
    public const string HintIMEInternalEditing = "SDL_IME_INTERNAL_EDITING";
    public const string HintIMENativeUI = "SDL_IME_NATIVE_UI";
    public const string HintIMEShowUI = "SDL_IME_SHOW_UI";
    public const string HintIOSHideHomeIndicator = "SDL_IOS_HIDE_HOME_INDICATOR";
    public const string HintJoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    public const string HintJoystickArcadeStickDevices = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
    public const string HintJoystickArcadeStickDevicesExcluded = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
    public const string HintJoystickBlacklistDevices = "SDL_JOYSTICK_BLACKLIST_DEVICES";
    public const string HintJoystickBlacklistDevicesExcluded = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
    public const string HintJoystickDevice = "SDL_JOYSTICK_DEVICE";
    public const string HintJoystickDirectInput = "SDL_JOYSTICK_DIRECTINPUT";
    public const string HintJoystickFlightstickDevices = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
    public const string HintJoystickFlightstickDevicesExcluded = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
    public const string HintJoystickGamecubeDevices = "SDL_JOYSTICK_GAMECUBE_DEVICES";
    public const string HintJoystickGamecubeDevicesExcluded = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
    public const string HintJoystickHIDAPI = "SDL_JOYSTICK_HIDAPI";
    public const string HintJoystickHIDAPICombineJoyCons = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    public const string HintJoystickHIDAPIGamecube = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    public const string HintJoystickHIDAPIGamecubeRumbleBrake = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
    public const string HintJoystickHIDAPIJoyCons = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    public const string HintJoystickHIDAPIJoyconHomeLED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    public const string HintJoystickHIDAPILuna = "SDL_JOYSTICK_HIDAPI_LUNA";
    public const string HintJoystickHIDAPINintendoClassic = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    public const string HintJoystickHIDAPIPS3 = "SDL_JOYSTICK_HIDAPI_PS3";
    public const string HintJoystickHIDAPIPS3SixAxisDriver = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
    public const string HintJoystickHIDAPIPS4 = "SDL_JOYSTICK_HIDAPI_PS4";
    public const string HintJoystickHIDAPIPS4ReportInterval = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
    public const string HintJoystickHIDAPIPS4Rumble = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
    public const string HintJoystickHIDAPIPS5 = "SDL_JOYSTICK_HIDAPI_PS5";
    public const string HintJoystickHIDAPIPS5PlayerLED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    public const string HintJoystickHIDAPIPS5Rumble = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";
    public const string HintJoystickHIDAPIShield = "SDL_JOYSTICK_HIDAPI_SHIELD";
    public const string HintJoystickHIDAPIStadia = "SDL_JOYSTICK_HIDAPI_STADIA";
    public const string HintJoystickHIDAPISteam = "SDL_JOYSTICK_HIDAPI_STEAM";
    public const string HintJoystickHIDAPISteamdeck = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
    public const string HintJoystickHIDAPISwitch = "SDL_JOYSTICK_HIDAPI_SWITCH";
    public const string HintJoystickHIDAPISwitchHomeLED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    public const string HintJoystickHIDAPISwitchPlayerLED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    public const string HintJoystickHIDAPIVerticalJoyCons = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    public const string HintJoystickHIDAPIWii = "SDL_JOYSTICK_HIDAPI_WII";
    public const string HintJoystickHIDAPIWiiPlayerLED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
    public const string HintJoystickHIDAPIXbox = "SDL_JOYSTICK_HIDAPI_XBOX";
    public const string HintJoystickHIDAPIXbox360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
    public const string HintJoystickHIDAPIXbox360PlayerLED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
    public const string HintJoystickHIDAPIXbox360Wireless = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    public const string HintJoystickHIDAPIXboxOne = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
    public const string HintJoystickHIDAPIXboxOneHomeLED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    public const string HintJoystickIOKit = "SDL_JOYSTICK_IOKIT";
    public const string HintJoystickLinuxClassic = "SDL_JOYSTICK_LINUX_CLASSIC";
    public const string HintJoystickLinuxDeadzones = "SDL_JOYSTICK_LINUX_DEADZONES";
    public const string HintJoystickLinuxDigitalHats = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
    public const string HintJoystickLinuxHatDeadzones = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
    public const string HintJoystickMFI = "SDL_JOYSTICK_MFI";
    public const string HintJoystickRawInput = "SDL_JOYSTICK_RAWINPUT";
    public const string HintJoystickRawInputCorrelateXInput = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    public const string HintJoystickROGChakram = "SDL_JOYSTICK_ROG_CHAKRAM";
    public const string HintJoystickThread = "SDL_JOYSTICK_THREAD";
    public const string HintJoystickThrottleDevices = "SDL_JOYSTICK_THROTTLE_DEVICES";
    public const string HintJoystickThrottleDevicesExcluded = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
    public const string HintJoystickWGI = "SDL_JOYSTICK_WGI";
    public const string HintJoystickWheelDevices = "SDL_JOYSTICK_WHEEL_DEVICES";
    public const string HintJoystickWheelDevicesExcluded = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    public const string HintJoystickZeroCenteredDevices = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
    public const string HintKeycodeOptions = "SDL_KEYCODE_OPTIONS";
    public const string HintKMSDRMDeviceIndex = "SDL_KMSDRM_DEVICE_INDEX";
    public const string HintKMSDRMRequireDRMMaster = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    public const string HintLogging = "SDL_LOGGING";
    public const string HintMacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
    public const string HintMacCtrlClickEmulateRightClick = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    public const string HintMacOpenGLAsyncDispatch = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
    public const string HintMainCallbackRate = "SDL_MAIN_CALLBACK_RATE";
    public const string HintMouseAutoCapture = "SDL_MOUSE_AUTO_CAPTURE";
    public const string HintMouseDoubleClickRadius = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    public const string HintMouseDoubleClickTime = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    public const string HintMouseFocusClickThrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
    public const string HintMouseNormalSpeedScale = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    public const string HintMouseRelativeClipInterval = "SDL_MOUSE_RELATIVE_CLIP_INTERVAL";
    public const string HintMouseRelativeCursorVisible = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
    public const string HintMouseRelativeModeCenter = "SDL_MOUSE_RELATIVE_MODE_CENTER";
    public const string HintMouseRelativeModeWarp = "SDL_MOUSE_RELATIVE_MODE_WARP";
    public const string HintMouseRelativeSpeedScale = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    public const string HintMouseRelativeSystemScale = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
    public const string HintMouseRelativeWarpMotion = "SDL_MOUSE_RELATIVE_WARP_MOTION";
    public const string HintMouseTouchEvents = "SDL_MOUSE_TOUCH_EVENTS";
    public const string HintNoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
    public const string HintOpenGLESDriver = "SDL_OPENGL_ES_DRIVER";
    public const string HintOrientations = "SDL_ORIENTATIONS";
    public const string HintPenDelayMouseButton = "SDL_PEN_DELAY_MOUSE_BUTTON";
    public const string HintPenNotMouse = "SDL_PEN_NOT_MOUSE";
    public const string HintPollSentinel = "SDL_POLL_SENTINEL";
    public const string HintPreferredLocales = "SDL_PREFERRED_LOCALES";
    public const string HintQuitOnLastWindowClose = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
    public const string HintRenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
    public const string HintRenderDirect3DThreadSafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
    public const string HintRenderDriver = "SDL_RENDER_DRIVER";
    public const string HintRenderLineMethod = "SDL_RENDER_LINE_METHOD";
    public const string HintRenderMetalPreferLowPowerDevice = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";
    public const string HintRenderVsync = "SDL_RENDER_VSYNC";
    public const string HintRenderVulkanDebug = "SDL_RENDER_VULKAN_DEBUG";
    public const string HintReturnKeyHidesIME = "SDL_RETURN_KEY_HIDES_IME";
    public const string HintROGGamepadMice = "SDL_ROG_GAMEPAD_MICE";
    public const string HintROGGamepadMiceExcluded = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
    public const string HintRPIVideoLayer = "SDL_RPI_VIDEO_LAYER";
    public const string HintScreensaverInhibitActivityName = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    public const string HintShutdownDBusOnQuit = "SDL_SHUTDOWN_DBUS_ON_QUIT";
    public const string HintStorageTitleDriver = "SDL_STORAGE_TITLE_DRIVER";
    public const string HintStorageUserDriver = "SDL_STORAGE_USER_DRIVER";
    public const string HintThreadForceRealtimeTimeCritical = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
    public const string HintThreadPriorityPolicy = "SDL_THREAD_PRIORITY_POLICY";
    public const string HintTimerResolution = "SDL_TIMER_RESOLUTION";
    public const string HintTouchMouseEvents = "SDL_TOUCH_MOUSE_EVENTS";
    public const string HintTrackpadIsTouchOnly = "SDL_TRACKPAD_IS_TOUCH_ONLY";
    public const string HintTVRemoteAsJoystick = "SDL_TV_REMOTE_AS_JOYSTICK";
    public const string HintVideoAllowScreensaver = "SDL_VIDEO_ALLOW_SCREENSAVER";
    public const string HintVideoDoubleBuffer = "SDL_VIDEO_DOUBLE_BUFFER";
    public const string HintVideoDriver = "SDL_VIDEO_DRIVER";
    public const string HintVideoEGLAllowGetDisplayFallback = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
    public const string HintVideoForceEGL = "SDL_VIDEO_FORCE_EGL";
    public const string HintVideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
    public const string HintVideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    public const string HintVideoSyncWindowOperations = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
    public const string HintVideoWaylandAllowLibdecor = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
    public const string HintVideoWaylandEmulateMouseWarp = "SDL_VIDEO_WAYLAND_EMULATE_MOUSE_WARP";
    public const string HintVideoWaylandModeEmulation = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
    public const string HintVideoWaylandModeScaling = "SDL_VIDEO_WAYLAND_MODE_SCALING";
    public const string HintVideoWaylandPreferLibdecor = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
    public const string HintVideoWaylandScaleToDisplay = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    public const string HintVideoWinD3DCompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
    public const string HintVideoX11NetWmBypassCompositor = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
    public const string HintVideoX11NetWmPing = "SDL_VIDEO_X11_NET_WM_PING";
    public const string HintVideoX11ScalingFactor = "SDL_VIDEO_X11_SCALING_FACTOR";
    public const string HintVideoX11WindowVisualId = "SDL_VIDEO_X11_WINDOW_VISUALID";
    public const string HintVideoX11Xrandr = "SDL_VIDEO_X11_XRANDR";
    public const string HintVitaTouchMouseDevice = "SDL_VITA_TOUCH_MOUSE_DEVICE";
    public const string HintWaveFactChunk = "SDL_WAVE_FACT_CHUNK";
    public const string HintWaveRiffChunkSize = "SDL_WAVE_RIFF_CHUNK_SIZE";
    public const string HintWaveTruncation = "SDL_WAVE_TRUNCATION";
    public const string HintWindowActivateWhenRaised = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
    public const string HintWindowActivateWhenShown = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
    public const string HintWindowAllowTopmost = "SDL_WINDOW_ALLOW_TOPMOST";
    public const string HintWindowFrameUsableWhileCursorHidden = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
    public const string HintWindowsCloseOnAltF4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
    public const string HintWindowsEnableMenuMnemonics = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
    public const string HintWindowsEnableMessageloop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
    public const string HintWindowsEraseBackgroundMode = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
    public const string HintWindowsForceMutexCriticalSections = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";
    public const string HintWindowsForceSemaphoreKernel = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    public const string HintWindowsIntResourceIcon = "SDL_WINDOWS_INTRESOURCE_ICON";
    public const string HintWindowsRawKeyboard = "SDL_WINDOWS_RAW_KEYBOARD";
    public const string HintWindowsUseD3D9Ex = "SDL_WINDOWS_USE_D3D9EX";
    public const string HintWinRTHandleBackButton = "SDL_WINRT_HANDLE_BACK_BUTTON";
    public const string HintWinRTPrivacyPolicyLabel = "SDL_WINRT_PRIVACY_POLICY_LABEL";
    public const string HintWinRTPrivacyPolicyUrl = "SDL_WINRT_PRIVACY_POLICY_URL";
    public const string HintX11ForceOverrideRedirect = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    public const string HintX11WindowType = "SDL_X11_WINDOW_TYPE";
    public const string HintXInputEnabled = "SDL_XINPUT_ENABLED";
}