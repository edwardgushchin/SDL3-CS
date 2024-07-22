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

/// <summary>
/// <para>This file contains functions to set and get configuration hints, as well as
/// listing each of them alphabetically.</para>
/// <para>The convention for naming hints is SDL_HINT_X, where "Hints.X" is the
/// environment variable that can be used to override the default.</para>
/// <para>In general these hints are just that - they may or may not be supported or
/// applicable on any given platform, but they provide a way for an application
/// or user to give the library a hint as to how they would like the library to
/// work.</para>
/// </summary>
public static class Hints
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
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AllowAltTabWhileGrabbed = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
    
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
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AndroidAllowRecreateActivity = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";
    
    /// <summary>
    /// <para>A variable to control whether the event loop will block itself when the app
    /// is paused.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Non blocking.</item>
    /// <item>"1": Blocking. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AndroidBlockOnPause = "SDL_ANDROID_BLOCK_ON_PAUSE";
    
    /// <summary>
    /// <para>A variable to control whether SDL will pause audio in background.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Not paused, requires that <see cref="AndroidBlockOnPause"/> be set to
    /// "0"</item>
    /// <item>"1": Paused. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AndroidBlockOnPausePauseaudio = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";
    
    /// <summary>
    /// <para>A variable to control whether we trap the Android back button to handle it
    /// manually.</para>
    /// <para>This is necessary for the right mouse button to work on some Android
    /// devices, or to be able to trap the back button for use in your code
    /// reliably. If this hint is true, the back button will show up as an
    /// <see cref="SDL.EventType.KeyDown"/> / <see cref="SDL.EventType.KeyUp"/> pair with a keycode of
    /// <see cref="SDL.Scancode.AcBack"/>.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Back button will be handled as usual for system. (default)</item>
    /// <item>"1": Back button will be trapped, allowing you to handle the key press
    /// manually. (This will also let right mouse click work on systems where the
    /// right mouse button functions as back.)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AndroidTrapBackButton = "SDL_ANDROID_TRAP_BACK_BUTTON";
    
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
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AppId = "SDL_APP_ID";
    
    /// <summary>
    /// <para>Specify an application name.</para>
    /// <para>This hint lets you specify the application name sent to the OS when
    /// required. For example, this will often appear in volume control applets for
    /// audio streams, and in lists of applications which are inhibiting the
    /// screensaver. You should use a string that describes your program ("My Game
    /// 2: The Revenge")</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable
    /// default: probably the application's name or "SDL Application" if SDL
    /// doesn't have any better information.</para>
    /// <para>Note that, for audio streams, this can be overridden with
    /// <see cref="AudioDeviceAppName"/></para>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AppName = "SDL_APP_NAME";
    
    /// <summary>
    /// <para>A variable controlling whether controllers used with the Apple TV generate
    /// UI events.</para>
    /// <para>When UI events are generated by controller input, the app will be
    /// backgrounded when the Apple TV remote's menu button is pressed, and when
    /// the pause or B buttons on gamepads are pressed.</para>
    /// <para>More information about properly making use of controllers for the Apple TV
    /// can be found here:
    /// https://developer.apple.com/tvos/human-interface-guidelines/remote-and-controllers/</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Controller input does not generate UI events. (default)</item>
    /// <item>"1": Controller input generates UI events.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AppleTVControllerUIEvents = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";
    
    /// <summary>
    /// <para>A variable controlling whether the Apple TV remote's joystick axes will
    /// automatically match the rotation of the remote.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Remote orientation does not affect joystick axes. (default)</item>
    /// <item>"1": Joystick axes are based on the orientation of the remote.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AppleTVRemoteAllowRotation = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
    
    /// <summary>
    /// <para>A variable controlling the audio category on iOS and macOS.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"ambient": Use the AVAudioSessionCategoryAmbient audio category, will be
    /// muted by the phone mute switch (default)</item>
    /// <item>"playback": Use the AVAudioSessionCategoryPlayback category.</item>
    /// </list>
    /// <para>For more information, see Apple's documentation:
    /// https://developer.apple.com/library/content/documentation/Audio/Conceptual/AudioSessionProgrammingGuide/AudioSessionCategoriesandModes/AudioSessionCategoriesandModes.html</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioCategory = "SDL_AUDIO_CATEGORY";

    /// <summary>
    /// <para>Specify an application name for an audio device.</para>
    /// <para>Some audio backends (such as PulseAudio) allow you to describe your audio
    /// stream. Among other things, this description might show up in a system
    /// control panel that lets the user adjust the volume on specific audio
    /// streams instead of using one giant master volume slider.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of
    /// this hint are used while opening an audio device. You should use a string
    /// that describes your program ("My Game 2: The Revenge")</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable
    /// default: this will be the name set with <see cref="AppName"/>, if that hint is
    /// set. Otherwise, it'll probably the application's name or "SDL Application"
    /// if SDL doesn't have any better information.</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioDeviceAppName = "SDL_AUDIO_DEVICE_APP_NAME"; 
    
    /// <summary>
    /// <para>Specify an application icon name for an audio device.</para>
    /// <para>Some audio backends (such as Pulseaudio and Pipewire) allow you to set an
    /// XDG icon name for your application. Among other things, this icon might
    /// show up in a system control panel that lets the user adjust the volume on
    /// specific audio streams instead of using one giant master volume slider.
    /// Note that this is unrelated to the icon used by the windowing system, which
    /// may be set with SDL_SetWindowIcon (or via desktop file on Wayland).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable
    /// default, "applications-games", which is likely to be installed. See
    /// https://specifications.freedesktop.org/icon-theme-spec/icon-theme-spec-latest.html
    /// and
    /// https://specifications.freedesktop.org/icon-naming-spec/icon-naming-spec-latest.html
    /// for the relevant XDG icon specs.</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioDeviceAppIconName = "SDL_AUDIO_DEVICE_APP_ICON_NAME";
    
    /// <summary>
    /// <para>A variable controlling device buffer size.</para>
    /// <para>This hint is an integer > 0, that represents the size of the device's
    /// buffer in sample frames (stereo audio data in 16-bit format is 4 bytes per
    /// sample frame, for example).</para>
    /// <para>SDL3 generally decides this value on behalf of the app, but if for some
    /// reason the app needs to dictate this (because they want either lower
    /// latency or higher throughput AND ARE WILLING TO DEAL WITH what that might
    /// require of the app), they can specify it.</para>
    /// <para>SDL will try to accommodate this value, but there is no promise you'll get
    /// the buffer size requested. Many platforms won't honor this request at all,
    /// or might adjust it.</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioDeviceSampleFrames = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";
    
    /// <summary>
    /// <para>Specify an audio stream name for an audio device.</para>
    /// <para>Some audio backends (such as PulseAudio) allow you to describe your audio
    /// stream. Among other things, this description might show up in a system
    /// control panel that lets the user adjust the volume on specific audio
    /// streams instead of using one giant master volume slider.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of
    /// this hint are used while opening an audio device. You should use a string
    /// that describes your what your program is playing ("audio stream" is
    /// probably sufficient in many cases, but this could be useful for something
    /// like "team chat" if you have a headset playing VoIP audio separately).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable
    /// default: "audio stream" or something similar.</para>
    /// <para>Note that while this talks about audio streams, this is an OS-level
    /// concept, so it applies to a physical audio device in this case, and not an
    /// <see cref="SDL.AudioStream"/>, nor an SDL logical audio device.</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioDeviceStreamName = "SDL_AUDIO_DEVICE_STREAM_NAME";
    
    /// <summary>
    /// <para>Specify an application role for an audio device.</para>
    /// <para>Some audio backends (such as Pipewire) allow you to describe the role of
    /// your audio stream. Among other things, this description might show up in a
    /// system control panel or software for displaying and manipulating media
    /// playback/recording graphs.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of
    /// this hint are used while opening an audio device. You should use a string
    /// that describes your what your program is playing (Game, Music, Movie,
    /// etc...).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable
    /// default: "Game" or something similar.</para>
    /// <para>Note that while this talks about audio streams, this is an OS-level
    /// concept, so it applies to a physical audio device in this case, and not an
    /// <see cref="SDL.AudioStream"/>, nor an SDL logical audio device.</para>
    /// </summary>
    /// <remarks>This hint should be set before an audio device is opened.</remarks>
    public const string AudioDeviceStreamRole = "SDL_AUDIO_DEVICE_STREAM_ROLE";
    
    /// <summary>
    /// <para>A variable that specifies an audio backend to use.</para>
    /// <para>By default, SDL will try all available audio backends in a reasonable order
    /// until it finds one that can work, but this hint allows the app or user to
    /// force a specific driver, such as "pipewire" if, say, you are on PulseAudio
    /// but want to try talking to the lower level instead.</para>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AudioDriver = "SDL_AUDIO_DRIVER";
    
    /// <summary>
    /// <para>A variable that causes SDL to not ignore audio "monitors".</para>
    /// <para>This is currently only used by the PulseAudio driver.</para>
    /// <para>By default, SDL ignores audio devices that aren't associated with physical
    /// hardware. Changing this hint to "1" will expose anything SDL sees that
    /// appears to be an audio source or sink. This will add "devices" to the list
    /// that the user probably doesn't want or need, but it can be useful in
    /// scenarios where you want to hook up SDL to some sort of virtual device,
    /// etc.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Audio monitor devices will be ignored. (default)</item>
    /// <item>"1": Audio monitor devices will show up in the device list.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string AudioIncludeMonitors = "SDL_AUDIO_INCLUDE_MONITORS";
    
    /// <summary>
    /// <para>A variable controlling whether SDL updates joystick state when getting
    /// input events.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": You'll call <see cref="SDL.UpdateJoysticks"/> manually.</item>
    /// <item>"1": SDL will automatically call <see cref="SDL.UpdateJoysticks"/>. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AutoUpdateJoysticks = "SDL_AUTO_UPDATE_JOYSTICKS";
    
    /// <summary>
    /// <para>A variable controlling whether SDL updates sensor state when getting input
    /// events.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": You'll call <see cref="SDL.UpdateSensors"/> manually.</item>
    /// <item>"1": SDL will automatically call <see cref="SDL.UpdateSensors"/>. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string AutoUpdateSensors = "SDL_AUTO_UPDATE_SENSORS";
    
    /// <summary>
    /// <para>Prevent SDL from using version 4 of the bitmap header when saving BMPs.</para>
    /// <para>The bitmap header version 4 is required for proper alpha channel support
    /// and SDL will use it when required. Should this not be desired, this hint
    /// can force the use of the 40 byte header version which is supported
    /// everywhere.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Surfaces with a colorkey or an alpha channel are saved to a 32-bit
    /// BMP file with an alpha mask. SDL will use the bitmap header version 4 and
    /// set the alpha mask accordingly. (default)</item>
    /// <item>"1": Surfaces with a colorkey or an alpha channel are saved to a 32-bit
    /// BMP file without an alpha mask. The alpha channel data will be in the
    /// file, but applications are going to ignore it.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string BmpSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";
    
    /// <summary>
    /// <para>A variable that decides what camera backend to use.</para>
    /// <para>By default, SDL will try all available camera backends in a reasonable
    /// order until it finds one that can work, but this hint allows the app or
    /// user to force a specific target, such as "directshow" if, say, you are on
    /// Windows Media Foundations but want to try DirectShow instead.</para>
    /// <para>The default value is unset, in which case SDL will try to figure out the
    /// best camera backend on your behalf. This hint needs to be set before
    /// <see cref="SDL.Init"/> is called to be useful.</para>
    /// </summary>
    public const string CameraDriver = "SDL_CAMERA_DRIVER";
    
    /// <summary>
    /// <para>A variable that limits what CPU features are available.</para>
    /// <para>By default, SDL marks all features the current CPU supports as available.
    /// This hint allows to limit these to a subset.</para>
    /// <para>When the hint is unset, or empty, SDL will enable all detected CPU
    /// features.</para>
    /// <para>The variable can be set to a comma separated list containing the following
    /// items:</para>
    /// <list type="bullet">
    /// <item>"all"</item>
    /// <item>"altivec"</item>
    /// <item>"sse"</item>
    /// <item>"sse2"</item>
    /// <item>"sse3"</item>
    /// <item>"sse41"</item>
    /// <item>"sse42"</item>
    /// <item>"avx"</item>
    /// <item>"avx2"</item>
    /// <item>"avx512f"</item>
    /// <item>"arm-simd"</item>
    /// <item>"neon"</item>
    /// <item>"lsx"</item>
    /// <item>"lasx"</item>
    /// </list>
    /// <para>The items can be prefixed by '+'/'-' to add/remove features.</para>
    /// </summary>
    public const string CPUFeatureMask = "SDL_CPU_FEATURE_MASK";
    
    /// <summary>
    /// <para>A variable controlling whether DirectInput should be used for controllers.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable DirectInput detection.</item>
    /// <item>"1": Enable DirectInput detection. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string JoystickDirectinput = "SDL_JOYSTICK_DIRECTINPUT";

    /// <summary>
    /// <para>A variable that specifies a dialog backend to use.</para>
    /// <para>By default, SDL will try all available dialog backends in a reasonable
    /// order until it finds one that can work, but this hint allows the app or
    /// user to force a specific target.</para>
    /// <para>If the specified target does not exist or is not available, the
    /// dialog-related function calls will fail.</para>
    /// <para>This hint currently only applies to platforms using the generic "Unix"
    /// dialog implementation, but may be extended to more platforms in the future.
    /// Note that some Unix and Unix-like platforms have their own implementation,
    /// such as macOS and Haiku.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>NULL: Select automatically (default, all platforms)</item>
    /// <item>"portal": Use XDG Portals through DBus (Unix only)</item>
    /// <item>"zenity": Use the Zenity program (Unix only)</item>
    /// </list>
    /// <para>More options may be added in the future.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string FileDialogDriver = "SDL_FILE_DIALOG_DRIVER";
    
    /// <summary>
    /// <para>Override for <see cref="SDL.GetDisplayUsableBounds"/>.</para>
    /// <para>If set, this hint will override the expected results for
    /// <see cref="SDL.GetDisplayUsableBounds"/> for display index 0. Generally you don't want
    /// to do this, but this allows an embedded system to request that some of the
    /// screen be reserved for other uses when paired with a well-behaved
    /// application.</para>
    /// <para>The contents of this hint must be 4 comma-separated integers, the first is
    /// the bounds x, then y, width and height, in that order.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string DisplayUsableBounds = "SDL_DISPLAY_USABLE_BOUNDS";
    
    /// <summary>
    /// <para>Disable giving back control to the browser automatically when running with
    /// asyncify.</para>
    /// <para>With -s ASYNCIFY, SDL calls emscripten_sleep during operations such as
    /// refreshing the screen or polling events.</para>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable emscripten_sleep calls (if you give back browser control
    /// manually or use asyncify for other purposes).</item>
    /// <item>"1": Enable emscripten_sleep calls. (default)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string EmscriptenAsyncify = "SDL_EMSCRIPTEN_ASYNCIFY";
    
    /// <summary>
    /// <para>Specify the CSS selector used for the "default" window/canvas.</para>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The default value is "#canvas"</para>
    /// </summary>
    /// <remarks>This hint should be set before creating a window.</remarks>
    public const string EmscriptenCanvasSelector = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
    
    /// <summary>
    /// <para>Override the binding element for keyboard inputs for Emscripten builds.</para>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The variable can be one of:</para>
    /// <list type="bullet">
    /// <item>"#window": the javascript window object (default)</item>
    /// <item>"#document": the javascript document object</item>
    /// <item>"#screen": the javascript window.screen object</item>
    /// <item>"#canvas": the WebGL canvas element</item>
    /// <item>any other string without a leading # sign applies to the element on the
    /// page with that ID.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before creating a window.</remarks>
    public const string EmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
    
    /// <summary>
    /// <para>A variable that controls whether the on-screen keyboard should be shown
    /// when text input is active.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"auto": The on-screen keyboard will be shown if there is no physical
    /// keyboard attached. (default)</item>
    /// <item>"0": Do not show the on-screen keyboard.</item>
    /// <item>"1": Show the on-screen keyboard, if available.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint must be set before <see cref="SDL.StartTextInput"/> is called</remarks>
    public const string EnableScreenKeyboard = "SDL_ENABLE_SCREEN_KEYBOARD";
    
    /// <summary>
    /// <para>A variable controlling verbosity of the logging of SDL events pushed onto
    /// the internal queue.</para>
    /// <para>The variable can be set to the following values, from least to most
    /// verbose:</para>
    /// <list type="bullet">
    /// <item>"0": Don't log any events. (default)</item>
    /// <item>"1": Log most events (other than the really spammy ones).</item>
    /// <item>"2": Include mouse and finger motion events.</item>
    /// </list>
    /// <para>This is generally meant to be used to debug SDL itself, but can be useful
    /// for application developers that need better visibility into what is going
    /// on in the event queue. Logged events are sent through <see cref="SDL.Log"/>, which
    /// means by default they appear on stdout on most platforms or maybe
    /// <see cref="SDL.OutputDebugString"/> on Windows, and can be funneled by the app with
    /// <see cref="SDL.SetLogOutputFunction"/>, etc.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string EventLogging = "SDL_EVENT_LOGGING";
    
    /// <summary>
    /// <para>A variable controlling whether raising the window should be done more
    /// forcefully.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Honor the OS policy for raising windows. (default)</item>
    /// <item>"1": Force the window to be raised, overriding any OS policy.</item>
    /// </list>
    /// </summary>
    /// <para>At present, this is only an issue under MS Windows, which makes it nearly
    /// impossible to programmatically move a window to the foreground, for
    /// "security" reasons. See http://stackoverflow.com/a/34414846 for a
    /// discussion.</para>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string ForceRaiseWindow = "SDL_FORCE_RAISEWINDOW";
    
    /// <summary>
    /// <para> A variable controlling how 3D acceleration is used to accelerate the SDL
    /// screen surface.</para>
    /// <para>SDL can try to accelerate the SDL screen surface by using streaming
    /// textures with a 3D rendering engine. This variable controls whether and how
    /// this is done.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable 3D acceleration</item>
    /// <item>"1": Enable 3D acceleration, using the default renderer. (default)</item>
    /// <item>"X": Enable 3D acceleration, using X where X is one of the valid
    /// rendering drivers. (e.g. "direct3d", "opengl", etc.)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.GetWindowSurface"/></remarks>
    public const string FrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";

    /// <summary>
    /// <para>A variable that lets you manually hint extra gamecontroller db entries.</para>
    /// <para>The variable should be newline delimited rows of gamecontroller config
    /// data, see SDL_gamepad.h</para>
    /// <para>You can update mappings after SDL is initialized with
    /// <see cref="SDL.GetGamepadMappingForGUID"/> and <see cref="SDL.AddGamepadMapping"/></para>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string GameControllerConfig = "SDL_GAMECONTROLLERCONFIG";
    
    /// <summary>
    /// <para>A variable that lets you provide a file with extra gamecontroller db
    /// entries.</para>
    /// <para>The file should contain lines of gamecontroller config data, see
    /// SDL_gamepad.h</para>
    /// <para>You can update mappings after SDL is initialized with
    /// <see cref="SDL.GetGamepadMappingForGUID"/> and <see cref="SDL.AddGamepadMapping"/></para>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string GameControllerConfigFile = "SDL_GAMECONTROLLERCONFIG_FILE";
    
    /// <summary>
    /// <para>A variable that overrides the automatic controller type detection.</para>
    /// <para>The variable should be comma separated entries, in the form: VID/PID=type</para>
    /// <para>The VID and PID should be hexadecimal with exactly 4 digits, e.g. 0x00fd</para>
    /// <para>This hint affects what low level protocol is used with the HIDAPI driver.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"Xbox360"</item>
    /// <item>"XboxOne"</item>
    /// <item>"PS3"</item>
    /// <item>"PS4"</item>
    /// <item>"PS5"</item>
    /// <item>"SwitchPro"</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string GameControllerType = "SDL_GAMECONTROLLERTYPE";
    
    /// <summary>
    /// <para>A variable containing a list of devices to skip when scanning for game
    /// controllers.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
    /// hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named
    /// file will be loaded and interpreted as the value of the variable.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string GameControllerIgnoreDevices = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
    
    /// <summary>
    /// <para>If set, all devices will be skipped when scanning for game controllers
    /// except for the ones listed in this variable.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
    /// hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named
    /// file will be loaded and interpreted as the value of the variable.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string GameControllerIgnoreDevicesExcept = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
    
    /// <summary>
    /// <para> A variable that controls whether the device's built-in accelerometer and
    /// gyro should be used as sensors for gamepads.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Sensor fusion is disabled</item>
    /// <item>"1": Sensor fusion is enabled for all controllers that lack sensors</item>
    /// </list>
    /// <para>Or the variable can be a comma separated list of USB VID/PID pairs in
    /// hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named
    /// file will be loaded and interpreted as the value of the variable.</para>
    /// </summary>
    /// <remarks>This hint should be set before a gamepad is opened.</remarks>
    public const string GameControllerSensorFusion = "SDL_GAMECONTROLLER_SENSOR_FUSION";
    
    /// <summary>
    /// <para>This variable sets the default text of the TextInput window on GDK
    /// platforms.</para>
    /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.StartTextInput"/></remarks>
    public const string GDKTextInputDefaultText = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
    
    /// <summary>
    /// <para>This variable sets the description of the TextInput window on GDK
    /// platforms.</para>
    /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.StartTextInput"/></remarks>
    public const string GDKTextInputDescription = "SDL_GDK_TEXTINPUT_DESCRIPTION";
    
    /// <summary>
    /// <para>This variable sets the maximum input length of the TextInput window on GDK
    /// platforms.</para>
    /// <para>The value must be a stringified integer, for example "10" to allow for up
    /// to 10 characters of text input.</para>
    /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.StartTextInput"/></remarks>
    public const string GDKTextInputMaxLength = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
    
    /// <summary>
    /// <para>This variable sets the input scope of the TextInput window on GDK
    /// platforms.</para>
    /// <para>Set this hint to change the XGameUiTextEntryInputScope value that will be
    /// passed to the window creation function. The value must be a stringified
    /// integer, for example "0" for XGameUiTextEntryInputScope::Default.</para>
    /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.StartTextInput"/></remarks>
    public const string GDKTextInputScope = "SDL_GDK_TEXTINPUT_SCOPE";
    
    /// <summary>
    /// <para>This variable sets the title of the TextInput window on GDK platforms.</para>
    /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
    /// </summary>
    /// <remarks>This hint should be set before calling <see cref="SDL.StartTextInput"/></remarks>
    public const string GDKTextInputTitle = "SDL_GDK_TEXTINPUT_TITLE";
    
    /// <summary>
    /// <para>A variable to control whether <see cref="SDL.HIDEnumerate"/> enumerates all HID
    /// devices or only controllers.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": <see cref="SDL.HIDEnumerate"/>  will enumerate all HID devices.</item>
    /// <item>"1": <see cref="SDL.HIDEnumerate"/>  will only enumerate controllers. (default)</item>
    /// </list>
    /// <para>By default SDL will only enumerate controllers, to reduce risk of hanging
    /// or crashing on devices with bad drivers and avoiding macOS keyboard capture
    /// permission prompts.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string HIDAPIEnumerateOnlyControllers = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
    
    /// <summary>
    /// <para>A variable containing a list of devices to ignore in <see cref="SDL.HIDEnumerate"/>.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
    /// hexadecimal form, e.g.</para>
    /// <para>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</para>
    /// <para>For example, to ignore the Shanwan DS3 controller and any Valve controller,
    /// you might use the string "0x2563/0x0523,0x28de/0x0000"</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string HIDAPIIgnoreDevices = "SDL_HIDAPI_IGNORE_DEVICES";
    
    /// <summary>
    /// <para>A variable describing what IME UI elements the application can display.</para>
    /// <para>By default IME UI is handled using native components by the OS where
    /// possible, however this can interfere with or not be visible when exclusive
    /// fullscreen mode is used.</para>
    /// <para>The variable can be set to a comma separated list containing the following
    /// items:</para>
    /// <list type="bullet">
    /// <item>"none" or "0": The application can't render any IME elements, and native
    /// UI should be used. (default)</item>
    /// <item>"composition": The application handles <see cref="SDL.EventType.TextEditing"/> events and
    /// can render the composition text.</item>
    /// <item>candidates": The application handles <see cref="SDL.EventType.TextEditingCandidates"/>
    /// and can render the candidate list.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint should be set before SDL is initialized.</remarks>
    public const string IMEImplementedUI = "SDL_IME_IMPLEMENTED_UI";
    
    /// <summary>
    /// <para>A variable controlling whether the home indicator bar on iPhone X should be
    /// hidden.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The indicator bar is not hidden. (default for windowed applications)</item>
    /// <item>"1": The indicator bar is hidden and is shown when the screen is touched
    /// (useful for movie playback applications).</item>
    /// <item>"2": The indicator bar is dim and the first swipe makes it visible and
    /// the second swipe performs the "home" action. (default for fullscreen
    /// applications)</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string IOSHideHomeIndicator = "SDL_IOS_HIDE_HOME_INDICATOR";
    
    /// <summary>
    /// <para>A variable that lets you enable joystick (and gamecontroller) events even
    /// when your app is in the background.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable joystick and gamecontroller input events when the application
    /// is in the background. (default)</item>
    /// <item>"1": Enable joystick and gamecontroller input events when the application
    /// is in the background.</item>
    /// </list>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string JoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    
    /// <summary>
    /// <para>A variable containing a list of arcade stick style controllers.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
    /// hexadecimal form, e.g.</para>
    /// <para>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</para>
    /// <para>The variable can also take the form of "@file", in which case the named
    /// file will be loaded and interpreted as the value of the variable.</para>
    /// </summary>
    /// <remarks>This hint can be set anytime.</remarks>
    public const string JoystickArcadeStickDevices = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickArcadeStickDevicesExcluded = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string IMEInternalEditing = "SDL_IME_INTERNAL_EDITING";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string IMENativeUI = "SDL_IME_NATIVE_UI";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string IMEShowUI = "SDL_IME_SHOW_UI";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickBlacklistDevices = "SDL_JOYSTICK_BLACKLIST_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickBlacklistDevicesExcluded = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickDevice = "SDL_JOYSTICK_DEVICE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickDirectInput = "SDL_JOYSTICK_DIRECTINPUT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickFlightstickDevices = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickFlightstickDevicesExcluded = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickGamecubeDevices = "SDL_JOYSTICK_GAMECUBE_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickGamecubeDevicesExcluded = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPI = "SDL_JOYSTICK_HIDAPI";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPICombineJoyCons = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIGamecube = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIGamecubeRumbleBrake = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIJoyCons = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIJoyconHomeLED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPILuna = "SDL_JOYSTICK_HIDAPI_LUNA";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPINintendoClassic = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS3 = "SDL_JOYSTICK_HIDAPI_PS3";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS3SixAxisDriver = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS4 = "SDL_JOYSTICK_HIDAPI_PS4";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS4ReportInterval = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS4Rumble = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS5 = "SDL_JOYSTICK_HIDAPI_PS5";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS5PlayerLED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIPS5Rumble = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIShield = "SDL_JOYSTICK_HIDAPI_SHIELD";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIStadia = "SDL_JOYSTICK_HIDAPI_STADIA";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPISteam = "SDL_JOYSTICK_HIDAPI_STEAM";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPISteamdeck = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPISwitch = "SDL_JOYSTICK_HIDAPI_SWITCH";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPISwitchHomeLED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPISwitchPlayerLED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIVerticalJoyCons = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIWii = "SDL_JOYSTICK_HIDAPI_WII";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIWiiPlayerLED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXbox = "SDL_JOYSTICK_HIDAPI_XBOX";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXbox360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXbox360PlayerLED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXbox360Wireless = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXboxOne = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickHIDAPIXboxOneHomeLED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickIOKit = "SDL_JOYSTICK_IOKIT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickLinuxClassic = "SDL_JOYSTICK_LINUX_CLASSIC";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickLinuxDeadzones = "SDL_JOYSTICK_LINUX_DEADZONES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickLinuxDigitalHats = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickLinuxHatDeadzones = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickMFI = "SDL_JOYSTICK_MFI";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickRawInput = "SDL_JOYSTICK_RAWINPUT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickRawInputCorrelateXInput = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickROGChakram = "SDL_JOYSTICK_ROG_CHAKRAM";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickThread = "SDL_JOYSTICK_THREAD";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickThrottleDevices = "SDL_JOYSTICK_THROTTLE_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickThrottleDevicesExcluded = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickWGI = "SDL_JOYSTICK_WGI";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickWheelDevices = "SDL_JOYSTICK_WHEEL_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickWheelDevicesExcluded = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string JoystickZeroCenteredDevices = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string KeycodeOptions = "SDL_KEYCODE_OPTIONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string KMSDRMDeviceIndex = "SDL_KMSDRM_DEVICE_INDEX";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string KMSDRMRequireDRMMaster = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string Logging = "SDL_LOGGING";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MacCtrlClickEmulateRightClick = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MacOpenGLAsyncDispatch = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MainCallbackRate = "SDL_MAIN_CALLBACK_RATE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseAutoCapture = "SDL_MOUSE_AUTO_CAPTURE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseDoubleClickRadius = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseDoubleClickTime = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseFocusClickThrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseNormalSpeedScale = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeClipInterval = "SDL_MOUSE_RELATIVE_CLIP_INTERVAL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeCursorVisible = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeModeCenter = "SDL_MOUSE_RELATIVE_MODE_CENTER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeModeWarp = "SDL_MOUSE_RELATIVE_MODE_WARP";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeSpeedScale = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeSystemScale = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseRelativeWarpMotion = "SDL_MOUSE_RELATIVE_WARP_MOTION";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string MouseTouchEvents = "SDL_MOUSE_TOUCH_EVENTS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string NoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string OpenGLESDriver = "SDL_OPENGL_ES_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string Orientations = "SDL_ORIENTATIONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string PenDelayMouseButton = "SDL_PEN_DELAY_MOUSE_BUTTON";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string PenNotMouse = "SDL_PEN_NOT_MOUSE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string PollSentinel = "SDL_POLL_SENTINEL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string PreferredLocales = "SDL_PREFERRED_LOCALES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string QuitOnLastWindowClose = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderDirect3DThreadSafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderDriver = "SDL_RENDER_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderLineMethod = "SDL_RENDER_LINE_METHOD";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderMetalPreferLowPowerDevice = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderVsync = "SDL_RENDER_VSYNC";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RenderVulkanDebug = "SDL_RENDER_VULKAN_DEBUG";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ReturnKeyHidesIME = "SDL_RETURN_KEY_HIDES_IME";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ROGGamepadMice = "SDL_ROG_GAMEPAD_MICE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ROGGamepadMiceExcluded = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string RPIVideoLayer = "SDL_RPI_VIDEO_LAYER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ScreensaverInhibitActivityName = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ShutdownDBusOnQuit = "SDL_SHUTDOWN_DBUS_ON_QUIT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string StorageTitleDriver = "SDL_STORAGE_TITLE_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string StorageUserDriver = "SDL_STORAGE_USER_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ThreadForceRealtimeTimeCritical = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string ThreadPriorityPolicy = "SDL_THREAD_PRIORITY_POLICY";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string TimerResolution = "SDL_TIMER_RESOLUTION";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string TouchMouseEvents = "SDL_TOUCH_MOUSE_EVENTS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string TrackpadIsTouchOnly = "SDL_TRACKPAD_IS_TOUCH_ONLY";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string TVRemoteAsJoystick = "SDL_TV_REMOTE_AS_JOYSTICK";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoAllowScreensaver = "SDL_VIDEO_ALLOW_SCREENSAVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoDoubleBuffer = "SDL_VIDEO_DOUBLE_BUFFER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoDriver = "SDL_VIDEO_DRIVER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoEGLAllowGetDisplayFallback = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoForceEGL = "SDL_VIDEO_FORCE_EGL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoSyncWindowOperations = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandAllowLibdecor = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandEmulateMouseWarp = "SDL_VIDEO_WAYLAND_EMULATE_MOUSE_WARP";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandModeEmulation = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandModeScaling = "SDL_VIDEO_WAYLAND_MODE_SCALING";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandPreferLibdecor = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWaylandScaleToDisplay = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoWinD3DCompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoX11NetWmBypassCompositor = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoX11NetWmPing = "SDL_VIDEO_X11_NET_WM_PING";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoX11ScalingFactor = "SDL_VIDEO_X11_SCALING_FACTOR";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoX11WindowVisualId = "SDL_VIDEO_X11_WINDOW_VISUALID";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VideoX11Xrandr = "SDL_VIDEO_X11_XRANDR";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string VitaTouchMouseDevice = "SDL_VITA_TOUCH_MOUSE_DEVICE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WaveFactChunk = "SDL_WAVE_FACT_CHUNK";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WaveRiffChunkSize = "SDL_WAVE_RIFF_CHUNK_SIZE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WaveTruncation = "SDL_WAVE_TRUNCATION";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowActivateWhenRaised = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowActivateWhenShown = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowAllowTopmost = "SDL_WINDOW_ALLOW_TOPMOST";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowFrameUsableWhileCursorHidden = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsCloseOnAltF4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsEnableMenuMnemonics = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsEnableMessageloop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsEraseBackgroundMode = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsForceMutexCriticalSections = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsForceSemaphoreKernel = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsIntResourceIcon = "SDL_WINDOWS_INTRESOURCE_ICON";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsRawKeyboard = "SDL_WINDOWS_RAW_KEYBOARD";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WindowsUseD3D9Ex = "SDL_WINDOWS_USE_D3D9EX";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WinRTHandleBackButton = "SDL_WINRT_HANDLE_BACK_BUTTON";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WinRTPrivacyPolicyLabel = "SDL_WINRT_PRIVACY_POLICY_LABEL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string WinRTPrivacyPolicyUrl = "SDL_WINRT_PRIVACY_POLICY_URL";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string X11ForceOverrideRedirect = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string X11WindowType = "SDL_X11_WINDOW_TYPE";
    
    /// <summary>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// </list>
    /// </summary>
    /// <remarks></remarks>
    public const string XInputEnabled = "SDL_XINPUT_ENABLED";
}