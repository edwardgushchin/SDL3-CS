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
 * product, an acknowLEDgment in the product documentation would be
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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    #region Hints
    /// <summary>
    /// Specify the behavior of Alt+Tab while the keyboard is grabbed.
    /// </summary>
    /// <remarks>
    /// By default, SDL emulates Alt+Tab functionality while the keyboard is grabbed and your window is full-screen.
    /// This prevents the user from getting stuck in your application if you've enabLED keyboard grab.
    /// <para>
    /// The variable can be set to the following values:
    /// </para>
    /// <list type="bullet">
    /// <item>
    /// "0": SDL will not handle Alt+Tab. Your application is responsible for handling Alt+Tab
    /// while the keyboard is grabbed.
    /// </item>
    /// <item>
    /// "1": SDL will minimize your window when Alt+Tab is pressed (default)
    /// This hint can be set anytime.
    /// </item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAllowAltTabWhileGrabbed = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";

    /// <summary>
    /// A variable to control whether the SDL activity is allowed to be re-created.
    /// </summary>
    /// <remarks>
    /// If this hint is true, the activity can be recreated on demand by the OS, and Java static data and
    /// C++ static data remain with their current values. If this hint is false, then SDL will call exit() when you
    /// return from your main function and the application will be terminated and then started fresh each time.
    /// <para>
    /// The variable can be set to the following values:
    /// </para>
    /// <list type="bullet">
    /// <item>"0": The application starts fresh at each launch. (default)</item>
    /// <item>"1": The application activity can be recreated by the OS.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAndroidAllowRecreateActivity = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";

    /// <summary>
    /// A variable to control whether the event loop will block itself when the app is paused.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Non blocking.</item>
    /// <item>"1": Blocking. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAndroidBlockOnPause = "SDL_ANDROID_BLOCK_ON_PAUSE";

    /// <summary>
    /// A variable to control whether SDL will pause audio in background.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Not paused, requires that <see cref="HintAndroidBlockOnPause"/> be set to "0"</item>
    /// <item>"1": Paused. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAndroidBlockOnPausePauseaudio = "SDL_ANDROID_BLOCK_ON_PAUSE_PAUSEAUDIO";

    /// <summary>
    /// A variable to control whether we trap the Android back button to handle it manually.
    /// </summary>
    /// <remarks>
    /// This is necessary for the right mouse button to work on some Android devices,
    /// or to be able to trap the back button for use in your code reliably.
    /// If this hint is true, the back button will show up as an 
    /// <see cref="EventType.KeyDown"/> / <see cref="EventType.KeyUp"/> pair with a keycode of
    /// <see cref="Scancode.ScancodeAcBack"/>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Back button will be handLED as usual for system. (default)</item>
    /// <item>"1": Back button will be trapped, allowing you to handle the key press manually.
    /// (This will also let right mouse click work on systems where the right mouse button functions as back.)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAndroidTrapBackButton = "SDL_ANDROID_TRAP_BACK_BUTTON";

    /// <summary>
    /// A variable setting the app ID string.
    /// </summary>
    /// <remarks>
    /// <para>This string is used by desktop compositors to identify and group windows together,
    /// as well as match applications with associated desktop settings and icons.</para>
    /// <para>On Wayland this corresponds to the "app ID" window property and on X11 this corresponds to the
    /// WM_CLASS property. Windows inherit the value of this hint at creation time. Changing this hint after a
    /// window has been created will not change the app ID or class of existing windows.</para>
    /// <para>For *nix platforms, this string should be formatted in reverse-DNS notation and follow some basic
    /// rules to be valid:</para>
    /// <list type="bullet">
    /// <item>The application ID must be composed of two or more elements separated by a period (.) character.</item>
    /// <item>Each element must contain one or more of the alphanumeric characters (A-Z, a-z, 0-9) plus
    /// underscore (_) and hyphen (-) and must not start with a digit. Note that hyphens, while technically
    /// allowed, should not be used if possible, as they are not supported by all components that use the ID,
    /// such as D-Bus. For maximum compatibility, replace hyphens with an underscore.</item>
    /// <item>The empty string is not a valid element (ie: your application ID may not start or end with a
    /// period and it is not valid to have two periods in a row).</item>
    /// <item>The entire ID must be less than 255 characters in length.</item>
    /// </list>
    /// <para>Examples of valid app ID strings:</para>
    /// <list type="bullet">
    /// <item>org.MyOrg.MyApp</item>
    /// <item>com.your_company.your_app</item>
    /// </list>
    /// <para>Desktops such as GNOME and KDE require that the app ID string matches your application's .desktop file
    /// name (e.g. if the app ID string is 'org.MyOrg.MyApp', your application's .desktop file should be named
    /// 'org.MyOrg.MyApp.desktop').</para>
    /// <para>If you plan to package your application in a container such as Flatpak, the app ID should match the
    /// name of your Flatpak container as well.</para>
    /// <para>If not set, SDL will attempt to use the application executable name. If the executable name cannot be
    /// retrieved, the generic string "SDL_App" will be used.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAppId = "SDL_APP_ID";

    /// <summary>
    /// Specify an application name.
    /// </summary>
    /// <remarks>
    /// <para>This hint lets you specify the application name sent to the OS when required. For example, this will
    /// often appear in volume control applets for audio streams, and in lists of applications which are inhibiting
    /// the screensaver. You should use a string that describes your program ("My Game 2: The Revenge")</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default: probably the
    /// application's name or "SDL Application" if SDL doesn't have any better information.</para>
    /// <para>Note that, for audio streams, this can be overridden with <see cref="HintAudioDeviceAppName"/>.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAppName = "SDL_APP_NAME";

    /// <summary>
    /// A variable controlling whether controllers used with the Apple TV generate UI events.
    /// </summary>
    /// <remarks>
    /// <para>When UI events are generated by controller input, the app will be backgrounded when the Apple TV remote's
    /// menu button is pressed, and when the pause or B buttons on gamepads are pressed.</para>
    /// <para>More information about properly making use of controllers for the Apple TV can be found here:
    /// https://developer.apple.com/tvos/human-interface-guidelines/remote-and-controllers/</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Controller input does not generate UI events. (default)</item>
    /// <item>"1": Controller input generates UI events.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAppleTVControllerUIEvents = "SDL_HINT_APPLE_TV_CONTROLLER_UI_EVENTS";

    /// <summary>
    /// A variable controlling whether the Apple TV remote's joystick axes will automatically match the rotation
    /// of the remote.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Remote orientation does not affect joystick axes. (default)</item>
    /// <item>"1": Joystick axes are based on the orientation of the remote.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAppleTVRemoteAllowRotation = "SDL_HINT_APPLE_TV_REMOTE_ALLOW_ROTATION";

    /// <summary>
    /// A variable controlling the audio category on iOS and macOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"ambient": Use the AVAudioSessionCategoryAmbient audio category, will be muted by the phone mute
    /// switch (default)</item>
    /// <item>"playback": Use the AVAudioSessionCategoryPlayback category.</item>
    /// </list>
    /// <para>For more information, see Apple's documentation:
    /// https://developer.apple.com/library/content/documentation/Audio/Conceptual/AudioSessionProgrammingGuide/AudioSessionCategoriesandModes/AudioSessionCategoriesandModes.html</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioCategory = "SDL_HINT_AUDIO_CATEGORY";

    /// <summary>
    /// Specify an application icon name for an audio device.
    /// </summary>
    /// <remarks>
    /// <para>Some audio backends (such as Pulseaudio and Pipewire) allow you to set an XDG icon name for your
    /// application. Among other things, this icon might show up in a system control panel that lets the user
    /// adjust the volume on specific audio streams instead of using one giant master volume slider. Note that
    /// this is unrelated to the icon used by the windowing system, which may be set with <see cref="SetWindowIcon"/>
    /// (or via desktop file on Wayland).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default, "applications-games",
    /// which is likely to be instalLED.
    /// See https://specifications.freedesktop.org/icon-theme-spec/icon-theme-spec-latest.html and
    /// https://specifications.freedesktop.org/icon-naming-spec/icon-naming-spec-latest.html for the relevant
    /// XDG icon specs.</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioDeviceAppIconName = "SDL_AUDIO_DEVICE_APP_ICON_NAME";

    /// <summary>
    /// Specify an application name for an audio device.
    /// </summary>
    /// <remarks>
    /// <para>Some audio backends (such as PulseAudio) allow you to describe your audio stream. Among other things,
    /// this description might show up in a system control panel that lets the user adjust the volume on specific
    /// audio streams instead of using one giant master volume slider.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of this hint are used while
    /// opening an audio device. You should use a string that describes your program ("My Game 2: The Revenge")</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default: this will be the
    /// name set with <see cref="HintAppName"/>, if that hint is set. Otherwise, it'll probably the application's name
    /// or "SDL Application" if SDL doesn't have any better information.</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioDeviceAppName = "SDL_AUDIO_DEVICE_APP_NAME";

    /// <summary>
    /// A variable controlling device buffer size.
    /// </summary>
    /// <remarks>
    /// <para>This hint is an integer > 0, that represents the size of the device's buffer in sample frames
    /// (stereo audio data in 16-bit format is 4 bytes per sample frame, for example).</para>
    /// <para>SDL3 generally decides this value on behalf of the app, but if for some reason the app needs to
    /// dictate this (because they want either lower latency or higher throughput AND ARE WILLING TO DEAL WITH
    /// what that might require of the app), they can specify it.</para>
    /// <para>SDL will try to accommodate this value, but there is no promise you'll get the buffer size requested.
    /// Many platforms won't honor this request at all, or might adjust it.</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioDeviceSampleFrames = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";

    /// <summary>
    /// Specify an audio stream name for an audio device.
    /// </summary>
    /// <remarks>
    /// <para>Some audio backends (such as PulseAudio) allow you to describe your audio stream. Among other things,
    /// this description might show up in a system control panel that lets the user adjust the volume on specific
    /// audio streams instead of using one giant master volume slider.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of this hint are used while
    /// opening an audio device. You should use a string that describes your what your program is playing
    /// ("audio stream" is probably sufficient in many cases, but this could be useful for something like
    /// "team chat" if you have a headset playing VoIP audio separately).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default: "audio stream" or
    /// something similar.</para>
    /// <para>Note that while this talks about audio streams, this is an OS-level concept, so it applies to a
    /// physical audio device in this case, and not an <see cref="AudioStream"/>, nor an SDL logical audio
    /// device.</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioDeviceStreamName = "SDL_AUDIO_DEVICE_STREAM_NAME";

    /// <summary>
    /// Specify an application role for an audio device.
    /// </summary>
    /// <remarks>
    /// <para>Some audio backends (such as Pipewire) allow you to describe the role of your audio stream. Among other
    /// things, this description might show up in a system control panel or software for displaying and manipulating
    /// media playback/recording graphs.</para>
    /// <para>This hints lets you transmit that information to the OS. The contents of this hint are used while opening
    /// an audio device. You should use a string that describes your what your program is playing (Game, Music, Movie,
    /// etc...).</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default: "Game" or something
    /// similar.</para>
    /// <para>Note that while this talks about audio streams, this is an OS-level concept, so it applies to a physical
    /// audio device in this case, and not an <see cref="AudioStream"/>, nor an SDL logical audio device.</para>
    /// <para>This hint should be set before an audio device is opened.</para>
    /// </remarks>
    public const string HintAudioDeviceStreamRole = "SDL_AUDIO_DEVICE_STREAM_ROLE";

    /// <summary>
    /// A variable that specifies an audio backend to use.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available audio backends in a reasonable order until it finds one that
    /// can work, but this hint allows the app or user to force a specific driver, such as "pipewire" if, say, you
    /// are on PulseAudio but want to try talking to the lower level instead.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAudioDriver = "SDL_AUDIO_DRIVER";

    /// <summary>
    /// A variable that causes SDL to not ignore audio "monitors".
    /// </summary>
    /// <remarks>
    /// <para>This is currently only used by the PulseAudio driver.</para>
    /// <para>By default, SDL ignores audio devices that aren't associated with physical hardware. Changing this
    /// hint to "1" will expose anything SDL sees that appears to be an audio source or sink. This will add "devices"
    /// to the list that the user probably doesn't want or need, but it can be useful in scenarios where you want to
    /// hook up SDL to some sort of virtual device, etc.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Audio monitor devices will be ignored. (default)</item>
    /// <item>"1": Audio monitor devices will show up in the device list.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintAudioIncludeMonitors = "SDL_AUDIO_INCLUDE_MONITORS";

    /// <summary>
    /// A variable controlling whether SDL updates joystick state when getting input events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": You'll call <see cref="UpdateJoysticks"/> manually.</item>
    /// <item>"1": SDL will automatically call <see cref="UpdateJoysticks"/>. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAutoUpdateJoysticks = "SDL_AUTO_UPDATE_JOYSTICKS";

    /// <summary>
    /// A variable controlling whether SDL updates sensor state when getting input events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": You'll call <see cref="UpdateSensors"/> manually.</item>
    /// <item>"1": SDL will automatically call <see cref="UpdateSensors"/>. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintAutoUpdateSensors = "SDL_AUTO_UPDATE_SENSORS";

    /// <summary>
    /// Prevent SDL from using version 4 of the bitmap header when saving BMPs.
    /// </summary>
    /// <remarks>
    /// <para>The bitmap header version 4 is required for proper alpha channel support and SDL will use it when
    /// required. Should this not be desired, this hint can force the use of the 40 byte header version which is
    /// supported everywhere.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Surfaces with a colorkey or an alpha channel are saved to a 32-bit BMP file with an alpha mask.
    /// SDL will use the bitmap header version 4 and set the alpha mask accordingly. (default)</item>
    /// <item>"1": Surfaces with a colorkey or an alpha channel are saved to a 32-bit BMP file without an alpha mask.
    /// The alpha channel data will be in the file, but applications are going to ignore it.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintBmpSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";

    /// <summary>
    /// A variable that decides what camera backend to use.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available camera backends in a reasonable order until it finds one that
    /// can work, but this hint allows the app or user to force a specific target, such as "directshow" if, say,
    /// you are on Windows Media Foundations but want to try DirectShow instead.</para>
    /// <para>The default value is unset, in which case SDL will try to figure out the best camera backend on your
    /// behalf. This hint needs to be set before <see cref="Init"/> is calLED to be useful.</para>
    /// </remarks>
    public const string HintCameraDriver = "SDL_CAMERA_DRIVER";

    /// <summary>
    /// A variable that limits what CPU features are available.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL marks all features the current CPU supports as available. This hint allows to limit these to a subset.</para>
    /// <para>When the hint is unset, or empty, SDL will enable all detected CPU features.</para>
    /// <para>The variable can be set to a comma separated list containing the following items:</para>
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
    /// </remarks>
    public const string HintCpuFeatureMask = "SDL_CPU_FEATURE_MASK";

    /// <summary>
    /// Override for <see cref="GetDisplayUsableBounds"/>.
    /// </summary>
    /// <remarks>
    /// <para>If set, this hint will override the expected results for <see cref="GetDisplayUsableBounds"/> for
    /// display index 0. Generally you don't want to do this, but this allows an embedded system to request that some
    /// of the screen be reserved for other uses when paired with a well-behaved application.</para>
    /// <para>The contents of this hint must be 4 comma-separated integers, the first is the bounds x, then y,
    /// width and height, in that order.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintDisplayUsableBounds = "SDL_HINT_DISPLAY_USABLE_BOUNDS";

    /// <summary>
    /// Disable giving back control to the browser automatically when running with asyncify.
    /// </summary>
    /// <remarks>
    /// <para>With -s ASYNCIFY, SDL calls emscripten_sleep during operations such as refreshing the screen or polling events.</para>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable emscripten_sleep calls (if you give back browser control manually or use asyncify for other purposes).</item>
    /// <item>"1": Enable emscripten_sleep calls. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintEmscriptenAsyncify = "SDL_EMSCRIPTEN_ASYNCIFY";
    
    /// <summary>
    /// Specify the CSS selector used for the "default" window/canvas.
    /// </summary>
    /// <remarks>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The default value is "#canvas"</para>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintEmscriptenCanvasSelector = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
    
    /// <summary>
    /// Override the binding element for keyboard inputs for Emscripten builds.
    /// </summary>
    /// <remarks>
    /// <para>This hint only applies to the emscripten platform.</para>
    /// <para>The variable can be one of:</para>
    /// <list type="bullet">
    /// <item>"#window": the javascript window object (default)</item>
    /// <item>"#document": the javascript document object</item>
    /// <item>"#screen": the javascript window.screen object</item>
    /// <item>"#canvas": the WebGL canvas element</item>
    /// <item>any other string without a leading # sign applies to the element on the page with that ID.</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintEmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
    
    /// <summary>
    /// A variable that controls whether the on-screen keyboard should be shown when text input is active.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"auto": The on-screen keyboard will be shown if there is no physical keyboard attached. (default)</item>
    /// <item>"0": Do not show the on-screen keyboard.</item>
    /// <item>"1": Show the on-screen keyboard, if available.</item>
    /// </list>
    /// <para>This hint must be set before <see cref="StartTextInput"/> is calLED</para>
    /// </remarks>
    public const string HintEnableScreenKeyboard = "SDL_ENABLE_SCREEN_KEYBOARD";
    
    /// <summary>
    /// A variable controlling verbosity of the logging of SDL events pushed onto the internal queue.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values, from least to most verbose:</para>
    /// <list type="bullet">
    /// <item>"0": Don't log any events. (default)</item>
    /// <item>"1": Log most events (other than the really spammy ones).</item>
    /// <item>"2": Include mouse and finger motion events.</item>
    /// </list>
    /// <para>This is generally meant to be used to debug SDL itself, but can be useful for application
    /// developers that need better visibility into what is going on in the event queue. Logged events are sent
    /// through <see cref="Log"/>, which means by default they appear on stdout on most platforms or maybe
    /// OutputDebugString() on Windows, and can be funneLED by the app with <see cref="SetLogOutputFunction"/>,
    /// etc.</para>
    /// </remarks>
    public const string HintEventLogging = "SDL_HINT_EVENT_LOGGING";
    
    /// <summary>
    /// A variable that specifies a dialog backend to use.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available dialog backends in a reasonable order until it finds one that
    /// can work, but this hint allows the app or user to force a specific target.</para>
    /// <para>If the specified target does not exist or is not available, the dialog-related function calls
    /// will fail.</para>
    /// <para>This hint currently only applies to platforms using the generic "Unix" dialog implementation,
    /// but may be extended to more platforms in the future. Note that some Unix and Unix-like platforms have
    /// their own implementation, such as macOS and Haiku.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>NULL: Select automatically (default, all platforms)</item>
    /// <item>"portal": Use XDG Portals through DBus (Unix only)</item>
    /// <item>"zenity": Use the Zenity program (Unix only)</item>
    /// </list>
    /// <para>More options may be added in the future.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintFiLEDialogDriver = "SDL_FILE_DIALOG_DRIVER";
    
    /// <summary>
    /// A variable controlling whether raising the window should be done more forcefully.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Honor the OS policy for raising windows. (default)</item>
    /// <item>"1": Force the window to be raised, overriding any OS policy.</item>
    /// </list>
    /// <para>At present, this is only an issue under MS Windows, which makes it nearly impossible to
    /// programmatically move a window to the foreground, for "security" reasons.
    /// See http://stackoverflow.com/a/34414846 for a discussion.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintForceRaiseWindow = "SDL_FORCE_RAISEWINDOW";
    
    /// <summary>
    /// A variable controlling how 3D acceleration is used to accelerate the SDL screen surface.
    /// </summary>
    /// <remarks>
    /// <para>SDL can try to accelerate the SDL screen surface by using streaming textures with a 3D rendering engine. This variable controls whether and how this is done.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable 3D acceleration</item>
    /// <item>"1": Enable 3D acceleration, using the default renderer. (default)</item>
    /// <item>"X": Enable 3D acceleration, using X where X is one of the valid rendering drivers.
    /// (e.g. "direct3d", "opengl", etc.)</item>
    /// </list>
    /// <para>This hint should be set before calling <see cref="GetWindowSurface"/></para>
    /// </remarks>
    public const string HintFrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
    
    /// <summary>
    /// A variable containing a list of devices to skip when scanning for game controllers.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintGameControllerIgnoreDevices = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
    
    /// <summary>
    /// If set, all devices will be skipped when scanning for game controllers except for the ones
    /// listed in this variable.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintGameControllerIgnoreDevicesExcept = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
    
    /// <summary>
    /// A variable that controls whether the device's built-in accelerometer and gyro should be used as
    /// sensors for gamepads.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Sensor fusion is disabLED</item>
    /// <item>"1": Sensor fusion is enabLED for all controllers that lack sensors</item>
    /// </list>
    /// <para>Or the variable can be a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint should be set before a gamepad is opened.</para>
    /// </remarks>
    public const string HintGameControllerSensorFusion = "SDL_GAMECONTROLLER_SENSOR_FUSION";
    
    /// <summary>
    /// A variable that lets you manually hint extra gamecontroller db entries.
    /// </summary>
    /// <remarks>
    /// <para>The variable should be newline delimited rows of gamecontroller config data, see SDL_gamepad.h</para>
    /// <para>You can update mappings after SDL is initialized with
    /// <see cref="GetGamepadMappingForGUID"/> and <see cref="AddGamepadMapping"/></para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintGameControllerConfig = "SDL_GAMECONTROLLERCONFIG";
    
    /// <summary>
    /// A variable that lets you provide a file with extra gamecontroller db entries.
    /// </summary>
    /// <remarks>
    /// <para>The file should contain lines of gamecontroller config data, see SDL_gamepad.h</para>
    /// <para>You can update mappings after SDL is initialized with
    /// <see cref="GetGamepadMappingForGUID"/> and <see cref="AddGamepadMapping"/>()</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintGameControllerConfigFile = "SDL_GAMECONTROLLERCONFIG_FILE";
    
    /// <summary>
    /// A variable that overrides the automatic controller type detection.
    /// </summary>
    /// <remarks>
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
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintGameControllerType = "SDL_GAMECONTROLLERTYPE";
    
    /// <summary>
    /// This variable sets the default text of the TextInput window on GDK platforms.
    /// </summary>
    /// <remarks>
    /// <para>This hint is available only if <see cref="GDK_TEXTINPUT"/> defined.</para>
    /// <para>This hint should be set before calling <see cref="StartTextInput"/></para>
    /// </remarks>
    public const string HintGDKTextInputDefaultText = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
    
    /// <summary>
    /// This variable sets the description of the TextInput window on GDK platforms.
    /// </summary>
    /// <remarks>
    /// <para>This hint is available only if <see cref="GDK_TEXTINPUT"/> defined.</para>
    /// <para>This hint should be set before calling <see cref="StartTextInput"/></para>
    /// </remarks>
    public const string HintGDKTextInputDescription = "SDL_GDK_TEXTINPUT_DESCRIPTION";
    
    
    /// <summary>
    /// This variable sets the maximum input length of the TextInput window on GDK platforms.
    /// </summary>
    /// <remarks>
    /// <para>The value must be a stringified integer, for example "10" to allow for up to 10 characters of text
    /// input.</para>
    /// <para>This hint is available only if <see cref="GDK_TEXTINPUT"/> defined.</para>
    /// <para>This hint should be set before calling <see cref="StartTextInput"/></para>
    /// </remarks>
    public const string HintGDKTextInputMaxLength = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
    
    /// <summary>
    /// This variable sets the input scope of the TextInput window on GDK platforms.
    /// </summary>
    /// <remarks>
    /// <para>Set this hint to change the XGameUiTextEntryInputScope value that will be passed to the window
    /// creation function. The value must be a stringified integer, for example "0" for
    /// XGameUiTextEntryInputScope::Default.</para>
    /// <para>This hint is available only if <see cref="GDK_TEXTINPUT"/> defined.</para>
    /// <para>This hint should be set before calling <see cref="StartTextInput"/></para>
    /// </remarks>
    public const string HintGDKTextInputScope = "SDL_GDK_TEXTINPUT_SCOPE";
    
    /// <summary>
    /// This variable sets the title of the TextInput window on GDK platforms.
    /// </summary>
    /// <remarks>
    /// <para>This hint is available only if <see cref="GDK_TEXTINPUT"/> defined.</para>
    /// <para>This hint should be set before calling <see cref="StartTextInput"/></para>
    /// </remarks>
    public const string HintGDKTextInputTitle = "SDL_GDK_TEXTINPUT_TITLE";
    
    /// <summary>
    /// A variable to control whether <see cref="HIDEnumerate"/> enumerates all HID devices or only controllers.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": <see cref="HIDEnumerate"/> will enumerate all HID devices.</item>
    /// <item>"1": <see cref="HIDEnumerate"/> will only enumerate controllers. (default)</item>
    /// </list>
    /// <para>By default SDL will only enumerate controllers, to reduce risk of hanging or crashing on devices
    /// with bad drivers and avoiding macOS keyboard capture permission prompts.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintHIDAPIEnumerateOnlyControllers = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
    
    /// <summary>
    /// A variable containing a list of devices to ignore in <see cref="HIDEnumerate"/>.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>For example, to ignore the Shanwan DS3 controller and any Valve controller, you might use the string
    /// "0x2563/0x0523,0x28de/0x0000"</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintHIDAPIIgnoreDevices = "SDL_HIDAPI_IGNORE_DEVICES";
    
    /// <summary>
    /// A variable describing what IME UI elements the application can display.
    /// </summary>
    /// <remarks>
    /// <para>By default IME UI is handLED using native components by the OS where possible,
    /// however this can interfere with or not be visible when exclusive fullscreen mode is used.</para>
    /// <para>The variable can be set to a comma separated list containing the following items:</para>
    /// <list type="bullet">
    /// <item>"none" or "0": The application can't render any IME elements, and native UI should be used. (default)</item>
    /// <item>"composition": The application handles <see cref="EventType.TextEditing"/>
    /// events and can render the composition text.</item>
    /// <item>"candidates": The application handles <see cref="EventType.TextEditingCandidates"/>
    /// and can render the candidate list.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintIMEImplementedUI = "SDL_IME_IMPLEMENTED_UI";
    
    /// <summary>
    /// A variable to control whether certain IMEs should handle text editing internally instead of sending
    /// <see cref="EventType.TextEditing"/> events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": <see cref="EventType.TextEditing"/> events are sent, and it is the application's responsibility
    /// to render the text from these events and differentiate it somehow from committed text. (default)</item>
    /// <item>"1": If supported by the IME then <see cref="EventType.TextEditing"/> events are not sent, and text
    /// that is being composed will be rendered in its own UI.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintIMEInternaLEDiting = "SDL_IME_INTERNAL_EDITING";
    
    /// <summary>
    /// A variable describing what IME elements the OS should render natively over the game.
    /// </summary>
    /// <remarks>
    /// <para>By default IME UI is handLED using native components by the OS, however this interferes with
    /// fullscreen games in some cases.</para>
    /// <para>The variable can be set to a comma separated list containing the following items:</para>
    /// <list type="bullet">
    /// <item>"none" or "0": Native UI elements will not be displayed.</item>
    /// <item>"composition": Native UI elements will be used for the IME composition string.</item>
    /// <item>"candidates": Native UI elements will be used for the IME candidate list.</item>
    /// <item>"all" or "1": Native UI elements will be used for all IME UI. (default)</item>
    /// </list>
    /// <para>If native UI is used for the composition string, then <see cref="EventType.TextEditing"/>
    /// will not be sent.</para>
    /// <para>If native UI is used for the candidates list, then <see cref="EventType.TextEditingCandidates"/>
    /// will not be sent.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintIMENativeUI = "SDL_IME_NATIVE_UI";
    
    /// <summary>
    /// A variable to control whether certain IMEs should show native UI components (such as the Candidate List)
    /// instead of suppressing them.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Native UI components are not display.</item>
    /// <item>"1": Native UI components are displayed. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintIMEShowUI = "SDL_IME_SHOW_UI";
    
    /// <summary>
    /// A variable controlling whether the home indicator bar on iPhone X should be hidden.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The indicator bar is not hidden. (default for windowed applications)</item>
    /// <item>"1": The indicator bar is hidden and is shown when the screen is touched
    /// (useful for movie playback applications).</item>
    /// <item>"2": The indicator bar is dim and the first swipe makes it visible and the second swipe
    /// performs the "home" action. (default for fullscreen applications)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintIOSHideHomeIndicator = "SDL_IOS_HIDE_HOME_INDICATOR";
    
    /// <summary>
    /// A variable that lets you enable joystick (and gamecontroller) events even when your app is in the background.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable joystick & gamecontroller input events when the application is in the background.
    /// (default)</item>
    /// <item>"1": Enable joystick & gamecontroller input events when the application is in the background.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    
    /// <summary>
    /// A variable containing a list of arcade stick style controllers.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickArcadeStickDevices = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices that are not arcade stick style controllers.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickArcadeStickDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickArcadeStickDevicesExcluded = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable containing a list of devices that should not be considered joysticks.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickBlacklistDevices = "SDL_JOYSTICK_BLACKLIST_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices that should be considered joysticks.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickBlacklistDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickBlacklistDevicesExcluded = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable containing a comma separated list of devices to open as joysticks.
    /// </summary>
    /// <remarks>
    /// This variable is currently only used by the Linux joystick driver.
    /// </remarks>
    public const string HintJoystickDevice = "SDL_JOYSTICK_DEVICE";
    
    /// <summary>
    /// A variable controlling whether DirectInput should be used for controllers.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable DirectInput detection.</item>
    /// <item>"1": Enable DirectInput detection. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickDirectInput = "SDL_JOYSTICK_DIRECTINPUT";
    
    /// <summary>
    /// A variable containing a list of flightstick style controllers.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of @file, in which case the named file will be loaded and interpreted
    /// as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickFlightstickDevices = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices that are not flightstick style controllers.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickFlightstickDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickFlightstickDevicesExcluded = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable containing a list of devices known to have a GameCube form factor.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickGamecubeDevices = "SDL_JOYSTICK_GAMECUBE_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices known not to have a GameCube form factor.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickGamecubeDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickGamecubeDevicesExcluded = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI joystick drivers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI drivers are not used.</item>
    /// <item>"1": HIDAPI drivers are used. (default)</item>
    /// </list>
    /// <para>This variable is the default for all drivers, but can be overridden by the hints for specific
    /// drivers below.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPI = "SDL_JOYSTICK_HIDAPI";
    
    /// <summary>
    /// A variable controlling whether Nintendo Switch Joy-Con controllers will be combined into a single Pro-like
    /// controller when using the HIDAPI driver.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Left and right Joy-Con controllers will not be combined and each will be a mini-gamepad.</item>
    /// <item>"1": Left and right Joy-Con controllers will be combined into a single controller. (default)</item>
    /// </list>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPICombineJoyCons = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo GameCube controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/></para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIGamecube = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    
    /// <summary>
    /// A variable controlling whether rumble is used to implement the GameCube controller's 3 rumble modes,
    /// Stop(0), Rumble(1), and StopHard(2).
    /// </summary>
    /// <remarks>
    /// <para>This is useful for applications that need full compatibility for things like ADSR envelopes.
    /// - Stop is implemented by setting low_frequency_rumble to 0 and high_frequency_rumble >0
    /// - Rumble is both at any arbitrary value - StopHard is implemented by setting both
    /// low_frequency_rumble and high_frequency_rumble to 0</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Normal rumble behavior is behavior is used. (default)</item>
    /// <item>"1": Proper GameCube controller rumble behavior is used.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIGamecubeRumbleBrake = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Switch Joy-Cons should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIJoyCons = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    
    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when a Nintendo
    /// Switch Joy-Con controller is opened.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": home button LED is turned off</item>
    /// <item>"1": home button LED is turned on</item>
    /// </list>
    /// <para>By default the Home button LED state is not changed. This hint can also be set to a floating point value
    /// between 0.0 and 1.0 which controls the brightness of the Home button LED.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIJoyconHomeLED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Amazon Luna controllers connected via Bluetooth
    /// should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPILuna = "SDL_JOYSTICK_HIDAPI_LUNA";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Online classic controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPINintendoClassic = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS3 controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/> on macOS, and "0" on other platforms.</para>
    /// <para>For official Sony driver (sixaxis.sys) use <see cref="HintJoystickHIDAPIPS3SixAxisDriver"/>.
    /// See https://github.com/ViGEm/DsHidMini for an alternative driver on Windows.
    /// This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS3 = "SDL_JOYSTICK_HIDAPI_PS3";
    
    /// <summary>
    /// A variable controlling whether the Sony driver (sixaxis.sys) for PS3 controllers (Sixaxis/DualShock 3)
    /// should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Sony driver (sixaxis.sys) is not used.</item>
    /// <item>"1": Sony driver (sixaxis.sys) is used.</item>
    /// </list>
    /// <para>The default value is 0.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS3SixAxisDriver = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS4 controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS4 = "SDL_JOYSTICK_HIDAPI_PS4";
    
    /// <summary>
    /// A variable controlling the update rate of the PS4 controller over Bluetooth when using the HIDAPI driver.
    /// </summary>
    /// <remarks>
    /// <para>This defaults to 4 ms, to match the behavior over USB, and to be more friendly to other Bluetooth
    /// devices and older Bluetooth hardware on the computer. It can be set to "1" (1000Hz), "2" (500Hz) and
    /// "4" (250Hz)</para>
    /// <para>This hint can be set anytime, but only takes effect when extended input reports are enabLED.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS4ReportInterval = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
    
    /// <summary>
    /// A variable controlling whether extended input reports should be used for PS4 controllers when using
    /// the HIDAPI driver.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": extended reports are not enabLED. (default)</item>
    /// <item>"1": extended reports are enabLED.</item>
    /// </list>
    /// <para>Extended input reports allow rumble on Bluetooth PS4 controllers, but break DirectInput handling for
    /// applications that don't use SDL.</para>
    /// <para>Once extended reports are enabLED, they can not be disabLED without power cycling the controller.</para>
    /// <para>For compatibility with applications written for versions of SDL prior to the introduction of PS5
    /// controller support, this value will also control the state of extended reports on PS5 controllers when
    /// the <see cref="HintJoystickHIDAPIPS5Rumble"/> hint is not explicitly set.</para>
    /// <para>This hint can be enabLED anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS4Rumble = "SDL_JOYSTICK_HIDAPI_PS4_RUMBLE";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for PS5 controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS5 = "SDL_JOYSTICK_HIDAPI_PS5";
    
    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a
    /// PS5 controller.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": player LEDs are not enabLED.</item>
    /// <item>"1": player LEDs are enabLED. (default)</item>
    /// </list>
    /// </remarks>
    public const string HintJoystickHIDAPIPS5PlayerLED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    
    /// <summary>
    /// A variable controlling whether extended input reports should be used for PS5 controllers when using
    /// the HIDAPI driver.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": extended reports are not enabLED. (default)</item>
    /// <item>"1": extended reports.</item>
    /// </list>
    /// <para>Extended input reports allow rumble on Bluetooth PS5 controllers, but break DirectInput handling for
    /// applications that don't use SDL.</para>
    /// <para>Once extended reports are enabLED, they can not be disabLED without power cycling the controller.</para>
    /// <para>For compatibility with applications written for versions of SDL prior to the introduction of PS5
    /// controller support, this value defaults to the value of <see cref="HintJoystickHIDAPIPS5Rumble"/>.</para>
    /// <para>This hint can be enabLED anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIPS5Rumble = "SDL_JOYSTICK_HIDAPI_PS5_RUMBLE";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for NVIDIA SHIELD controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIShield = "SDL_JOYSTICK_HIDAPI_SHIELD";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Google Stadia controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIStadia = "SDL_JOYSTICK_HIDAPI_STADIA";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Bluetooth Steam Controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used. (default)</item>
    /// <item>"1": HIDAPI driver is used for Steam Controllers, which requires Bluetooth access and may prompt the
    /// user for permission on iOS and Android.</item>
    /// </list>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPISteam = "SDL_JOYSTICK_HIDAPI_STEAM";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for the Steam Deck builtin controller should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPISteamdeck = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Switch controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPISwitch = "SDL_JOYSTICK_HIDAPI_SWITCH";
    
    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when a Nintendo Switch Pro
    /// controller is opened.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Home button LED is turned off.</item>
    /// <item>"1": Home button LED is turned on.</item>
    /// </list>
    /// <para>By default the Home button LED state is not changed. This hint can also be set to a floating point value
    /// between 0.0 and 1.0 which controls the brightness of the Home button LED.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPISwitchHomeLED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    
    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with a
    /// Nintendo Switch controller.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Player LEDs are not enabled.</item>
    /// <item>"1": Player LEDs are enabled. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPISwitchPlayerLED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    
    /// <summary>
    /// A variable controlling whether Nintendo Switch Joy-Con controllers will be in vertical mode when using
    /// the HIDAPI driver.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Left and right Joy-Con controllers will not be in vertical mode. (default)</item>
    /// <item>"1": Left and right Joy-Con controllers will be in vertical mode.</item>
    /// </list>
    /// <para>This hint should be set before opening a Joy-Con controller.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIVerticalJoyCons = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for Nintendo Wii and Wii U controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>This driver doesn't work with the dolphinbar, so the default is SDL_FALSE for now.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIWii = "SDL_JOYSTICK_HIDAPI_WII";
    
    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated
    /// with a Wii controller.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Player LEDs are not enabled.</item>
    /// <item>"1": Player LEDs are enabled. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIWiiPlayerLED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is "0" on Windows, otherwise the value of <see cref="HintJoystickHIDAPI"/>.</para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIXbox = "SDL_JOYSTICK_HIDAPI_XBOX";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox 360 controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPIXbox"/></para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIXbox360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
    
    /// <summary>
    /// A variable controlling whether the player LEDs should be lit to indicate which player is associated with an Xbox 360 controller.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Player LEDs are not enabled.</item>
    /// <item>"1": Player LEDs are enabled. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIXbox360PlayerLED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox 360 wireless controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPIXbox360"/></para>
    /// <para>This hint should be set before enumerating controllers.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIXbox360Wireless = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    
    /// <summary>
    /// A variable controlling whether the HIDAPI driver for XBox One controllers should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": HIDAPI driver is not used.</item>
    /// <item>"1": HIDAPI driver is used.</item>
    /// </list>
    /// <para>The default is the value of <see cref="HintJoystickHIDAPIXbox"/>.</para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHIDAPIXboxOne = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
    
    /// <summary>
    /// A variable controlling whether the Home button LED should be turned on when an Xbox One controller is opened.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Home button LED is turned off.</item>
    /// <item>"1": Home button LED is turned on.</item>
    /// </list>
    /// <para>By default the Home button LED state is not changed.
    /// This hint can also be set to a floating point value between 0.0 and 1.0 which controls the brightness of the
    /// Home button LED. The default brightness is 0.4.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickHIDAPIXboxOneHomeLED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    
    /// <summary>
    /// A variable controlling whether IOKit should be used for controller handling.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": IOKit is not used.</item>
    /// <item>"1": IOKit is used. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickIOKit = "SDL_JOYSTICK_IOKIT";
    
    /// <summary>
    /// A variable controlling whether to use the classic /dev/input/js* joystick interface or the newer
    /// /dev/input/event* joystick interface on Linux.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use /dev/input/event* (default)</item>
    /// <item>"1": Use /dev/input/js*</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickLinuxClassic = "SDL_JOYSTICK_LINUX_CLASSIC";
    
    /// <summary>
    /// A variable controlling whether joysticks on Linux adhere to their HID-defined deadzones or
    /// return unfiltered values.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Return unfiltered joystick axis values. (default)</item>
    /// <item>"1": Return axis values with deadzones taken into account.</item>
    /// </list>
    /// <para>This hint should be set before a controller is opened.</para>
    /// </remarks>
    public const string HintJoystickLinuxDeadzones = "SDL_JOYSTICK_LINUX_DEADZONES";
    
    /// <summary>
    /// A variable controlling whether joysticks on Linux will always treat 'hat' axis inputs
    /// (ABS_HAT0X - ABS_HAT3Y) as 8-way digital hats without checking whether they may be analog.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Only map hat axis inputs to digital hat outputs if the input axes appear to
    /// actually be digital. (default)</item>
    /// <item>"1": Always handle the input axes numbered ABS_HAT0X to ABS_HAT3Y as digital hats.</item>
    /// </list>
    /// <para>This hint should be set before a controller is opened.</para>
    /// </remarks>
    public const string HintJoystickLinuxDigitalHats = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
    
    /// <summary>
    /// A variable controlling whether digital hats on Linux will apply deadzones to their underlying input axes or
    /// use unfiltered values.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Return digital hat values based on unfiltered input axis values.</item>
    /// <item>"1": Return digital hat values with deadzones on the input axes taken into account. (default)</item>
    /// </list>
    /// <para>This hint should be set before a controller is opened.</para>
    /// </remarks>
    public const string HintJoystickLinuxHatDeadzones = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
    
    /// <summary>
    /// A variable controlling whether GCController should be used for controller handling.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": GCController is not used.</item>
    /// <item>"1": GCController is used. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickMFI = "SDL_JOYSTICK_MFI";
    
    /// <summary>
    /// A variable controlling whether the RAWINPUT joystick drivers should be used for better handling
    /// XInput-capable devices.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": RAWINPUT drivers are not used.</item>
    /// <item>"1": RAWINPUT drivers are used. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickRawInput = "SDL_JOYSTICK_RAWINPUT";
    
    /// <summary>
    /// A variable controlling whether the RAWINPUT driver should pull correlated data from XInput.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": RAWINPUT driver will only use data from raw input APIs.</item>
    /// <item>"1": RAWINPUT driver will also pull data from XInput and Windows.Gaming.Input, providing better trigger
    /// axes, guide button presses, and rumble support for Xbox controllers. (default)</item>
    /// </list>
    /// <para>This hint should be set before a gamepad is opened.</para>
    /// </remarks>
    public const string HintJoystickRawInputCorrelateXInput = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
    
    /// <summary>
    /// A variable controlling whether the ROG Chakram mice should show up as joysticks.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": ROG Chakram mice do not show up as joysticks. (default)</item>
    /// <item>"1": ROG Chakram mice show up as joysticks.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickROGChakram = "SDL_JOYSTICK_ROG_CHAKRAM";
    
    /// <summary>
    /// A variable controlling whether a separate thread should be used for handling joystick detection
    /// and raw input messages on Windows.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": A separate thread is not used. (default)</item>
    /// <item>"1": A separate thread is used for handling raw input messages.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickThread = "SDL_JOYSTICK_THREAD";
    
    /// <summary>
    /// A variable containing a list of throttle style controllers.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickThrottleDevices = "SDL_JOYSTICK_THROTTLE_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices that are not throttle style controllers.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickThrottleDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickThrottleDevicesExcluded = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable controlling whether Windows.Gaming.Input should be used for controller handling.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": WGI is not used.</item>
    /// <item>"1": WGI is used. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintJoystickWGI = "SDL_JOYSTICK_WGI";
    
    /// <summary>
    /// A variable containing a list of wheel style controllers.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickWheelDevices = "SDL_JOYSTICK_WHEEL_DEVICES";
    
    /// <summary>
    /// A variable containing a list of devices that are not wheel style controllers.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintJoystickWheelDevices"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintJoystickWheelDevicesExcluded = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    
    /// <summary>
    /// A variable containing a list of devices known to have all axes centered at zero.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint should be set before a controller is opened.</para>
    /// </remarks>
    public const string HintJoystickZeroCenteredDevices = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
    
    /// <summary>
    /// A variable that controls keycode representation in keyboard events.
    /// </summary>
    /// <remarks>
    /// <para>This variable is a comma separated set of options for translating keycodes in events:</para>
    /// <list type="bullet">
    /// <item>"none": Keycode options are cleared, this overrides other options.</item>
    /// <item>"hide_numpad": The numpad keysyms will be translated into their non-numpad versions based on the current
    /// NumLock state. For example, SDLK_KP_4 would become SDLK_4 if SDL_KMOD_NUM is set in the event modifiers,
    /// and SDLK_LEFT if it is unset.</item>
    /// <item>"french_numbers": The number row on French keyboards is inverted, so pressing the 1 key would yield
    /// the keycode SDLK_1, or '1', instead of SDLK_AMPERSAND, or '&'</item>
    /// <item>"latin_letters": For keyboards using non-Latin letters, such as Russian or Thai, the letter
    /// keys generate keycodes as though it had an en_US layout. e.g. pressing the key associated with
    /// SDL_SCANCODE_A on a Russian keyboard would yield 'a' instead of 'ф'.</item>
    /// </list>
    /// <para>The default value for this hint is "french_numbers"</para>
    /// <para>Some platforms like Emscripten only provide modified keycodes and the options are not used.</para>
    /// <para>These options do not affect the return value of <see cref="GetKeyFromScancode"/> or
    /// <see cref="GetScancodeFromKey"/>, they just apply to the keycode included in key events.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintKeycodeOptions = "SDL_KEYCODE_OPTIONS";
    
    /// <summary>
    /// A variable that controls what KMSDRM device to use.
    /// </summary>
    /// <remarks>
    /// <para>SDL might open something like "/dev/dri/cardNN" to access KMSDRM functionality, where "NN" is a
    /// device index number. SDL makes a guess at the best index to use (usually zero), but the app or user can set
    /// this hint to a number between 0 and 99 to force selection.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintKMSDRMDeviceIndex = "SDL_KMSDRM_DEVICE_INDEX";
    
    /// <summary>
    /// A variable that controls whether SDL requires DRM master access in order to initialize the KMSDRM video backend.
    /// </summary>
    /// <remarks>
    /// <para>The DRM subsystem has a concept of a "DRM master" which is a DRM client that has the ability to set
    /// planes, set cursor, etc. When SDL is DRM master, it can draw to the screen using the SDL rendering APIs.
    /// Without DRM master, SDL is still able to process input and query attributes of attached displays, but it
    /// cannot change display state or draw to the screen directly.</para>
    /// <para>In some cases, it can be useful to have the KMSDRM backend even if it cannot be used for rendering.
    /// An app may want to use SDL for input processing while using another rendering API (such as an MMAL overlay
    /// on Raspberry Pi) or using its own code to render to DRM overlays that SDL doesn't support.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will allow usage of the KMSDRM backend without DRM master.</item>
    /// <item>"1": SDL Will require DRM master to use the KMSDRM backend. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// <para></para>
    /// </remarks>
    public const string HintKMSDRMRequireDRMMaster = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    
    /// <summary>
    /// A variable controlling the default SDL log levels.
    /// </summary>
    /// <remarks>
    /// <para>This variable is a comma separated set of category=level tokens that define the default logging
    /// levels for SDL applications.</para>
    /// <para>The category can be a numeric category, one of "app", "error", "assert", "system", "audio", "video",
    /// "render", "input", "test", or * for any unspecified category.</para>
    /// <para>The level can be a numeric level, one of "verbose", "debug", "info", "warn", "error", "critical", or
    /// "quiet" to disable that category.</para>
    /// <para>You can omit the category if you want to set the logging level for all categories.</para>
    /// <para>If this hint isn't set, the default log levels are equivalent to:</para>
    /// <para>app=info,assert=warn,test=verbose,*=error</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintLogging = "SDL_LOGGING";
    
    /// <summary>
    /// A variable controlling whether to force the application to become the foreground process when launched on macOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The application is brought to the foreground when launched. (default)</item>
    /// <item>"1": The application may remain in the background when launched.</item>
    /// </list>
    /// <para>This hint should be set before applicationDidFinishLaunching() is called.</para>
    /// </remarks>
    public const string HintMacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
    
    /// <summary>
    /// A variable that determines whether Ctrl+Click should generate a right-click event on macOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Ctrl+Click does not generate a right mouse button click event. (default)</item>
    /// <item>"1": Ctrl+Click generated a right mouse button click event.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMacCtrlClickEmulateRightClick = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";
    
    /// <summary>
    /// A variable controlling whether dispatching OpenGL context updates should block the dispatching
    /// thread until the main thread finishes processing on macOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Dispatching OpenGL context updates will block the dispatching thread until the main thread
    /// finishes processing. (default)</item>
    /// <item>"1": Dispatching OpenGL context updates will allow the dispatching thread to continue execution.</item>
    /// </list>
    /// <para>Generally you want the default, but if you have OpenGL code in a background thread on a Mac,
    /// and the main thread hangs because it's waiting for that background thread, but that background thread
    /// is also hanging because it's waiting for the main thread to do an update, this might fix your issue.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMacOpenGLAsyncDispatch = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
    
    /// <summary>
    /// Request <see cref="AppIterate"/> be called at a specific rate.
    /// </summary>
    /// <remarks>
    /// <para>This number is in Hz, so "60" means try to iterate 60 times per second.</para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para>On some platforms, or if you are using SDL_main instead of <see cref="AppIterate"/>, this hint is ignored.
    /// When the hint can be used, it is allowed to be changed at any time.</para>
    /// <para>This defaults to 60, and specifying NULL for the hint's value will restore the default.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMainCallbackRate = "SDL_MAIN_CALLBACK_RATE";
    
    /// <summary>
    /// A variable controlling whether the mouse is captured while mouse buttons are pressed.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The mouse is not captured while mouse buttons are pressed.</item>
    /// <item>"1": The mouse is captured while mouse buttons are pressed.</item>
    /// </list>
    /// <para>By default the mouse is captured while mouse buttons are pressed so if the mouse is dragged outside
    /// the window, the application continues to receive mouse events until the button is released.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseAutoCapture = "SDL_MOUSE_AUTO_CAPTURE";
    
    /// <summary>
    /// A variable setting the double click radius, in pixels.
    /// </summary>
    /// <remarks>
    /// This hint can be set anytime.
    /// </remarks>
    public const string HintMouseDoubleClickRadius = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    
    /// <summary>
    /// A variable setting the double click time, in milliseconds.
    /// </summary>
    /// <remarks>
    /// This hint can be set anytime.
    /// </remarks>
    public const string HintMouseDoubleClickTime = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    
    /// <summary>
    /// Allow mouse click events when clicking to focus an SDL window.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Ignore mouse clicks that activate a window. (default)</item>
    /// <item>"1": Generate events for mouse clicks that activate a window.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseFocusClickThrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
    
    /// <summary>
    /// A variable setting the speed scale for mouse motion, in floating point, when the mouse is not in relative mode.
    /// </summary>
    /// <remarks>
    /// This hint can be set anytime.
    /// </remarks>
    public const string HintMouseNormalSpeedScale = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    
    /// <summary>
    /// Controls how often SDL issues cursor confinement commands to the operating system while
    /// relative mode is active, in case the desired confinement state became out-of-sync due to interference
    /// from other running programs.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be integers representing miliseconds between each refresh.
    /// A value of zero means SDL will not automatically refresh the confinement. The default
    /// value varies depending on the operating system, this variable might not have any effects on
    /// inapplicable platforms such as those without a cursor.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseRelativeClipInterval = "SDL_MOUSE_RELATIVE_CLIP_INTERVAL";
    
    /// <summary>
    /// A variable controlling whether the hardware cursor stays visible when relative mode is active.
    /// </summary>
    /// <remarks>
    /// <para>This variable can be set to the following values: "0" - The cursor will be hidden while
    /// relative mode is active (default) "1" - The cursor will remain visible while relative mode is active</para>
    /// <para>Note that for systems without raw hardware inputs, relative mode is implemented
    /// using warping, so the hardware cursor will visibly warp between frames
    /// if this is enabled on those systems.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseRelativeCursorVisible = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
    
    /// <summary>
    /// A variable controlling whether relative mouse mode constrains the mouse to the center of the window.
    /// </summary>
    /// <remarks>
    /// <para>Constraining to the center of the window works better for FPS games and when the application
    /// is running over RDP. Constraining to the whole window works better for 2D games and increases the
    /// chance that the mouse will be in the correct position when using high DPI mice.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Relative mouse mode constrains the mouse to the window.</item>
    /// <item>"1": Relative mouse mode constrains the mouse to the center of the window. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseRelativeModeCenter = "SDL_MOUSE_RELATIVE_MODE_CENTER";
    
    /// <summary>
    /// A variable controlling whether relative mouse mode is implemented using mouse warping.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Relative mouse mode uses raw input. (default)</item>
    /// <item>"1": Relative mouse mode uses mouse warping.</item>
    /// </list>
    /// <para>This hint can be set anytime relative mode is not currently enabled.</para>
    /// </remarks>
    public const string HintMouseRelativeModeWarp = "SDL_MOUSE_RELATIVE_MODE_WARP";
    
    /// <summary>
    /// A variable setting the scale for mouse motion, in floating point, when the mouse is in relative mode.
    /// </summary>
    /// <remarks>
    /// This hint can be set anytime.
    /// </remarks>
    public const string HintMouseRelativeSpeedScale = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    
    /// <summary>
    /// A variable controlling whether the system mouse acceleration curve is used for relative mouse motion.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Relative mouse motion will be unscaled. (default)</item>
    /// <item>"1": Relative mouse motion will be scaled using the system mouse acceleration curve.</item>
    /// </list>
    /// <para>If <see cref="HintMouseRelativeSpeedScale"/> is set, that will override the system speed scale.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseRelativeSystemScale = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
    
    /// <summary>
    /// A variable controlling whether a motion event should be generated for mouse warping in relative mode.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Warping the mouse will not generate a motion event in relative mode</item>
    /// </list>
    /// <para>By default warping the mouse will not generate motion events in relative mode.
    /// This avoids the application having to filter out large relative motion due to warping.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseRelativeWarpMotion = "SDL_MOUSE_RELATIVE_WARP_MOTION";
    
    /// <summary>
    /// A variable controlling whether mouse events should generate synthetic touch events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Mouse events will not generate touch events. (default for desktop platforms)</item>
    /// <item>"1": Mouse events will generate touch events.
    /// (default for mobile platforms, such as Android and iOS)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintMouseTouchEvents = "SDL_MOUSE_TOUCH_EVENTS";
    
    /// <summary>
    /// Tell SDL not to catch the SIGINT or SIGTERM signals on POSIX platforms.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will install a SIGINT and SIGTERM handler, and when it catches a signal,
    /// convert it into an <see cref="EventType.Quit"/> event. (default)</item>
    /// <item>"1": SDL will not install a signal handler at all.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintNoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
    
    /// <summary>
    /// A variable controlling what driver to use for OpenGL ES contexts.
    /// </summary>
    /// <remarks>
    /// <para>On some platforms, currently Windows and X11, OpenGL drivers may support creating contexts with an
    /// OpenGL ES profile. By default SDL uses these profiles, when available, otherwise it attempts to load an
    /// OpenGL ES library, e.g. that provided by the ANGLE project. This variable controls whether SDL follows this
    /// default behaviour or will always load an OpenGL ES library.</para>
    /// <para>Circumstances where this is useful include - Testing an app with a particular OpenGL ES implementation,
    /// e.g ANGLE, or emulator, e.g. those from ARM, Imagination or Qualcomm. - Resolving OpenGL ES function addresses
    /// at link time by linking with the OpenGL ES library instead of querying them at run time with
    /// <see cref="GLGetProcAddress"/>.</para>
    /// <para>Caution: for an application to work with the default behaviour across different OpenGL drivers
    /// it must query the OpenGL ES function addresses at run time using <see cref="GLGetProcAddress"/>.</para>
    /// <para>This variable is ignored on most platforms because OpenGL ES is native or not supported.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use ES profile of OpenGL, if available. (default)</item>
    /// <item>"1": Load OpenGL ES library using the default library names.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintOpenGLESDriver = "SDL_OPENGL_ES_DRIVER";
    
    /// <summary>
    /// A variable controlling which orientations are allowed on iOS/Android.
    /// </summary>
    /// <remarks>
    /// <para>In some circumstances it is necessary to be able to explicitly control which UI orientations
    /// are allowed.</para>
    /// <para>This variable is a space delimited list of the following values:</para>
    /// <list type="bullet">
    /// <item>"LandscapeLeft"</item>
    /// <item>"LandscapeRight"</item>
    /// <item>"Portrait"</item>
    /// <item>"PortraitUpsideDown"</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintOrientations = "SDL_ORIENTATIONS";
    
    /// <summary>
    /// A variable controlling whether pen mouse button emulation triggers only when the pen touches the tablet surface.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The pen reports mouse button press/release immediately when the pen button is pressed/released,
    /// and the pen tip touching the surface counts as left mouse button press.</item>
    /// <item>"1": Mouse button presses are sent when the pen first touches the tablet (analogously for releases).
    /// Not pressing a pen button simulates mouse button 1, pressing the first pen button simulates mouse button
    /// 2 etc.; it is not possible to report multiple buttons as pressed at the same time. (default)</item>
    /// <item></item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintPenDelayMouseButton = "SDL_PEN_DELAY_MOUSE_BUTTON";
    
    /// <summary>
    /// A variable controlling whether to treat pen movement as separate from mouse movement.
    /// </summary>
    /// <remarks>
    /// <para>By default, pens report both <see cref="EventType.MouseMotion"/> and <see cref="EventType.PenMotion"/>
    /// updates (analogously for button presses). This hint allows decoupling mouse and pen updates.</para>
    /// <para>This variable toggles between the following behaviour:</para>
    /// <list type="bullet">
    /// <item>"0": Pen acts as a mouse with mouse ID SDL_PEN_MOUSEID.
    /// (default) Use case: client application is not pen aware, user wants to use pen instead
    /// of mouse to interact.</item>
    /// <item>"1": Pen reports mouse clicks and movement events but does not update SDL-internal mouse state
    /// (buttons pressed, current mouse location). Use case: client application is not pen aware, user
    /// frequently alternates between pen and "real" mouse.</item>
    /// <item>"2": Pen reports no mouse events. Use case: pen-aware client application uses this hint to
    /// allow user to toggle between pen+mouse mode ("2") and pen-only mode ("1" or "0").</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintPenNotMouse = "SDL_PEN_NOT_MOUSE";
    
    /// <summary>
    /// A variable controlling the use of a sentinel event when polling the event queue.
    /// </summary>
    /// <remarks>
    /// <para>When polling for events, <see cref="PumpEvents"/> is used to gather new events from devices.
    /// If a device keeps producing new events between calls to <see cref="PumpEvents"/>, a poll loop will
    /// become stuck until the new events stop. This is most noticeable when moving a high frequency mouse.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable poll sentinels.</item>
    /// <item>"1": Enable poll sentinels. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintPollSentinel = "SDL_POLL_SENTINEL";
    
    /// <summary>
    /// Override for <see cref="GetPreferredLocales"/>.
    /// </summary>
    /// <remarks>
    /// <para>If set, this will be favored over anything the OS might report for the user's preferred locales.
    /// Changing this hint at runtime will not generate an <see cref="EventType.LocaleChanged"/> event
    /// (but if you can change the hint, you can push your own event, if you want).</para>
    /// <para>The format of this hint is a comma-separated list of language and locale, combined with an underscore,
    /// as is a common format: "en_GB". Locale is optional: "en". So you might have a list like this:
    /// "en_GB,jp,es_PT"</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintPreferredLocales = "SDL_PREFERRED_LOCALES";
    
    /// <summary>
    /// A variable that decides whether to send <see cref="EventType.Quit"/> when closing the last window.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will not send an <see cref="EventType.Quit"/> event when the last window is requesting to close.
    /// Note that in this case, there are still other legitimate reasons one might get an
    /// <see cref="EventType.Quit"/> event: choosing "Quit" from the macOS menu bar, sending a
    /// SIGINT (ctrl-c) on Unix, etc.</item>
    /// <item>"1": SDL will send a quit event when the last window is requesting to close. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintQuitOnLastWindowClose = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
    
    /// <summary>
    /// A variable controlling whether to enable Direct3D 11+'s Debug Layer.
    /// </summary>
    /// <remarks>
    /// <para>This variable does not have any effect on the Direct3D 9 based renderer.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable Debug Layer use. (default)</item>
    /// <item>"1": Enable Debug Layer use.</item>
    /// </list>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
    
    /// <summary>
    /// A variable controlling whether the Direct3D device is initialized for thread-safe operations.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Thread-safety is not enabled. (default)</item>
    /// <item>"1": Thread-safety is enabled.</item>
    /// </list>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderDirect3DThreadSafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
    
    /// <summary>
    /// A variable specifying which render driver to use.
    /// </summary>
    /// <remarks>
    /// <para>If the application doesn't pick a specific renderer to use, this variable specifies the name
    /// of the preferred renderer. If the preferred renderer can't be initialized, the normal default
    /// renderer is used.</para>
    /// <para>This variable is case insensitive and can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"direct3d"</item>
    /// <item>"direct3d11"</item>
    /// <item>"direct3d12"</item>
    /// <item>"opengl"</item>
    /// <item>"opengles2"</item>
    /// <item>"opengles"</item>
    /// <item>"metal"</item>
    /// <item>"vulkan"</item>
    /// <item>"software"</item>
    /// </list>
    /// <para>The default varies by platform, but it's the first one in the list that is
    /// available on the current platform.</para>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderDriver = "SDL_RENDER_DRIVER";
    
    /// <summary>
    /// A variable controlling how the 2D render API renders lines.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use the default line drawing method (Bresenham's line algorithm)</item>
    /// <item>"1": Use the driver point API using Bresenham's line algorithm (correct, draws many points)</item>
    /// <item>"2": Use the driver line API (occasionally misses line endpoints based on hardware driver quirks</item>
    /// <item>"3": Use the driver geometry API (correct, draws thicker diagonal lines)</item>
    /// </list>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderLineMethod = "SDL_RENDER_LINE_METHOD";
    
    /// <summary>
    /// A variable controlling whether the Metal render driver select low power device over default one.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use the preferred OS device. (default)</item>
    /// <item>"1": Select a low power device.</item>
    /// </list>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderMetalPreferLowPowerDevice = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";
    
    /// <summary>
    /// A variable controlling whether updates to the SDL screen surface should be synchronized with the
    /// vertical refresh, to avoid tearing.
    /// </summary>
    /// <remarks>
    /// <para>This hint overrides the application preference when creating a renderer.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable vsync. (default)</item>
    /// <item>"1": Enable vsync.</item>
    /// </list>
    /// <para>This hint should be set before creating a renderer.</para>
    /// </remarks>
    public const string HintRenderVsync = "SDL_RENDER_VSYNC";
    
    /// <summary>
    /// A variable controlling whether to enable Vulkan Validation Layers.
    /// </summary>
    /// <remarks>
    /// <para>This variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable Validation Layer use</item>
    /// <item>"1": Enable Validation Layer use</item>
    /// </list>
    /// <para>By default, SDL does not use Vulkan Validation Layers.</para>
    /// </remarks>
    public const string HintRenderVulkanDebug = "SDL_RENDER_VULKAN_DEBUG";
    
    /// <summary>
    /// A variable to control whether the return key on the soft keyboard should hide the soft keyboard on Android and iOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The return key will be handled as a key event. (default)</item>
    /// <item>"1": The return key will hide the keyboard.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintReturnKeyHidesIME = "SDL_RETURN_KEY_HIDES_IME";
    
    /// <summary>
    /// A variable containing a list of ROG gamepad capable mice.
    /// </summary>
    /// <remarks>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintROGGamepadMice = "SDL_ROG_GAMEPAD_MICE";
    
    /// <summary>
    /// A variable containing a list of devices that are not ROG gamepad capable mice.
    /// </summary>
    /// <remarks>
    /// <para>This will override <see cref="HintROGGamepadMice"/> and the built in device list.</para>
    /// <para>The format of the string is a comma separated list of USB VID/PID pairs in hexadecimal form, e.g.</para>
    /// <para>0xAAAA/0xBBBB,0xCCCC/0xDDDD</para>
    /// <para>The variable can also take the form of "@file", in which case the named file will be loaded and
    /// interpreted as the value of the variable.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintROGGamepadMiceExcluded = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
    
    /// <summary>
    /// A variable controlling which Dispmanx layer to use on a Raspberry PI.
    /// </summary>
    /// <remarks>
    /// <para>Also known as Z-order. The variable can take a negative or positive value. The default is 10000.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintRPIVideoLayer = "SDL_RPI_VIDEO_LAYER";
    
    /// <summary>
    /// Specify an "activity name" for screensaver inhibition.
    /// </summary>
    /// <remarks>
    /// <para>Some platforms, notably Linux desktops, list the applications which are inhibiting the
    /// screensaver or other power-saving features.</para>
    /// <para>This hint lets you specify the "activity name" sent to the OS when <see cref="DisableScreenSaver"/>
    /// is used (or the screensaver is automatically disabled). The contents of this hint are used when the
    /// screensaver is disabled. You should use a string that describes what your program is doing (and, therefore,
    /// why the screensaver is disabled). For example, "Playing a game" or "Watching a video".</para>
    /// <para>Setting this to "" or leaving it unset will have SDL use a reasonable default:
    /// "Playing a game" or something similar.</para>
    /// <para>This hint should be set before calling <see cref="DisableScreenSaver"/></para>
    /// </remarks>
    public const string HintScreensaverInhibitActivityName = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
    
    /// <summary>
    /// A variable controlling whether SDL calls dbus_shutdown() on quit.
    /// </summary>
    /// <remarks>
    /// <para>This is useful as a debug tool to validate memory leaks, but shouldn't ever be set in production
    /// applications, as other libraries used by the application might use dbus under the hood and this cause
    /// crashes if they continue after <see cref="Quit"/>.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will not call dbus_shutdown() on quit. (default)</item>
    /// <item>"1": SDL will call dbus_shutdown() on quit.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintShutdownDBusOnQuit = "SDL_SHUTDOWN_DBUS_ON_QUIT";
    
    /// <summary>
    /// A variable that specifies a backend to use for title storage.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available storage backends in a reasonable order until it
    /// finds one that can work, but this hint allows the app or user to force a specific target, such as
    /// "pc" if, say, you are on Steam but want to avoid SteamRemoteStorage for title data.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintStorageTitleDriver = "SDL_STORAGE_TITLE_DRIVER";
    
    /// <summary>
    /// A variable that specifies a backend to use for user storage.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available storage backends in a reasonable order until it
    /// finds one that can work, but this hint allows the app or user to force a specific target, such as
    /// "pc" if, say, you are on Steam but want to avoid SteamRemoteStorage for user data.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintStorageUserDriver = "SDL_STORAGE_USER_DRIVER";
    
    /// <summary>
    /// Specifies whether <see cref="ThreadPriority.TimeCritical"/> should be treated as realtime.
    /// </summary>
    /// <remarks>
    /// <para>On some platforms, like Linux, a realtime priority thread may be subject to restrictions
    /// that require special handling by the application. This hint exists to let SDL know that the app
    /// is prepared to handle said restrictions.</para>
    /// <para>On Linux, SDL will apply the following configuration to any thread that becomes realtime:</para>
    /// <list type="bullet">
    /// <item>The SCHED_RESET_ON_FORK bit will be set on the scheduling policy,</item>
    /// <item>An RLIMIT_RTTIME budget will be configured to the rtkit specified limit.</item>
    /// <item>Exceeding this limit will result in the kernel sending SIGKILL to the app,
    /// refer to the man pages for more information.</item>
    /// </list>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": default platform specific behaviour</item>
    /// <item>"1": Force <see cref="ThreadPriority.TimeCritical"/> to a realtime scheduling policy</item>
    /// </list>
    /// <para>This hint should be set before calling <see cref="SetThreadPriority"/></para>
    /// </remarks>
    public const string HintThreadForceRealtimeTimeCritical = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
    
    /// <summary>
    /// A string specifying additional information to use with <see cref="SetThreadPriority"/>.
    /// </summary>
    /// <remarks>
    /// <para>By default, <see cref="SetThreadPriority"/> will make appropriate system changes in order to apply a
    /// thread priority. For example on systems using pthreads the scheduler policy is changed
    /// automatically to a policy that works well with a given priority. Code which has specific
    /// requirements can override SDL's default behavior with this hint.</para>
    /// <para>pthread hint values are "current", "other", "fifo" and "rr". Currently no other
    /// platform hint values are defined but may be in the future.</para>
    /// <para>On Linux, the kernel may send SIGKILL to realtime tasks which exceed the distro configured execution
    /// budget for rtkit. This budget can be queried through RLIMIT_RTTIME after calling
    /// <see cref="SetThreadPriority"/>.</para>
    /// <para>This hint should be set before calling <see cref="SetThreadPriority"/></para>
    /// </remarks>
    public const string HintThreadPriorityPolicy = "SDL_THREAD_PRIORITY_POLICY";
    
    /// <summary>
    /// A variable that controls the timer resolution, in milliseconds.
    /// </summary>
    /// <remarks>
    /// <para>The higher resolution the timer, the more frequently the CPU services timer interrupts,
    /// and the more precise delays are, but this takes up power and CPU time. This hint is only used on Windows.</para>
    /// <para>See this blog post for more information:
    /// http://randomascii.wordpress.com/2013/07/08/windows-timer-resolution-megawatts-wasted/</para>
    /// <para>The default value is "1".</para>
    /// <para>If this variable is set to "0", the system timer resolution is not set.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintTimerResolution = "SDL_TIMER_RESOLUTION";
    
    /// <summary>
    /// A variable controlling whether touch events should generate synthetic mouse events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Touch events will not generate mouse events.</item>
    /// <item>"1": Touch events will generate mouse events. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintTouchMouseEvents = "SDL_TOUCH_MOUSE_EVENTS";
    
    /// <summary>
    /// A variable controlling whether trackpads should be treated as touch devices.
    /// </summary>
    /// <remarks>
    /// <para>On macOS (and possibly other platforms in the future), SDL will report touches on a
    /// trackpad as mouse input, which is generally what users expect from this device; however,
    /// these are often actually full multitouch-capable touch devices, so it might be preferable to
    /// some apps to treat them as such.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Trackpad will send mouse events. (default)</item>
    /// <item>"1": Trackpad will send touch events.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintTrackpadIsTouchOnly = "SDL_TRACKPAD_IS_TOUCH_ONLY";
    
    /// <summary>
    /// A variable controlling whether the Android / tvOS remotes should be listed as joystick
    /// devices, instead of sending keyboard events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Remotes send enter/escape/arrow key events.</item>
    /// <item>"1": Remotes are available as 2 axis, 2 button joysticks. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintTVRemoteAsJoystick = "SDL_TV_REMOTE_AS_JOYSTICK";
    
    /// <summary>
    /// A variable controlling whether the screensaver is enabled.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable screensaver. (default)</item>
    /// <item>"1": Enable screensaver.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoAllowScreensaver = "SDL_VIDEO_ALLOW_SCREENSAVER";
    
    /// <summary>
    /// Tell the video driver that we only want a double buffer.
    /// </summary>
    /// <remarks>
    /// <para>By default, most lowlevel 2D APIs will use a triple buffer scheme that wastes no CPU time on
    /// waiting for vsync after issuing a flip, but introduces a frame of latency. On the other hand,
    /// using a double buffer scheme instead is recommended for cases where low latency is an important
    /// factor because we save a whole frame of latency.</para>
    /// <para>We do so by waiting for vsync immediately after issuing a flip, usually just after
    /// eglSwapBuffers call in the backend's *_SwapWindow function.</para>
    /// <para>This hint is currently supported on the following drivers:</para>
    /// <list type="bullet">
    /// <item>Raspberry Pi (raspberrypi)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoDoubleBuffer = "SDL_VIDEO_DOUBLE_BUFFER";
    
    /// <summary>
    /// A variable that specifies a video backend to use.
    /// </summary>
    /// <remarks>
    /// <para>By default, SDL will try all available video backends in a reasonable order until it
    /// finds one that can work, but this hint allows the app or user to force a specific target, such as
    /// "x11" if, say, you are on Wayland but want to try talking to the X server instead.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoDriver = "SDL_VIDEO_DRIVER";
    
    /// <summary>
    /// If eglGetPlatformDisplay fails, fall back to calling eglGetDisplay.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to one of the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Do not fall back to eglGetDisplay.</item>
    /// <item>"1": Fall back to eglGetDisplay if eglGetPlatformDisplay fails. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoEGLAllowGetDisplayFallback = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
    
    /// <summary>
    /// A variable controlling whether the OpenGL context should be created with EGL.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use platform-specific GL context creation API (GLX, WGL, CGL, etc). (default)</item>
    /// <item>"1": Use EGL</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoForceEGL = "SDL_VIDEO_FORCE_EGL";
    
    /// <summary>
    /// A variable that specifies the policy for fullscreen Spaces on macOS.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable Spaces support (FULLSCREEN_DESKTOP won't use them and <see cref="WindowFlags.Resizable"/>
    /// windows won't offer the "fullscreen" button on their titlebars).</item>
    /// <item>"1": Enable Spaces support (FULLSCREEN_DESKTOP will use them and <see cref="WindowFlags.Resizable"/>
    /// windows will offer the "fullscreen" button on their titlebars). (default)</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintVideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
    
    /// <summary>
    /// A variable controlling whether fullscreen windows are minimized when they lose focus.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Fullscreen windows will not be minimized when they lose focus. (default)</item>
    /// <item>"1": Fullscreen windows are minimized when they lose focus.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintVideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    
    /// <summary>
    /// A variable controlling whether all window operations will block until complete.
    /// </summary>
    /// <remarks>
    /// <para>Window systems that run asynchronously may not have the results of window operations that
    /// resize or move the window applied immediately upon the return of the requesting function. Setting
    /// this hint will cause such operations to block after every call until the pending operation has completed.
    /// Setting this to '1' is the equivalent of calling <see cref="SyncWindow"/> after every function call.</para>
    /// <para>Be aware that amount of time spent blocking while waiting for window operations to complete can
    /// be quite lengthy, as animations may have to complete, which can take upwards of multiple seconds in
    /// some cases.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Window operations are non-blocking. (default)</item>
    /// <item>"1": Window operations will block until completed.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintVideoSyncWindowOperations = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
    
    /// <summary>
    /// A variable controlling whether the libdecor Wayland backend is allowed to be used.
    /// </summary>
    /// <remarks>
    /// <para>libdecor is used over xdg-shell when xdg-decoration protocol is unavailable.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": libdecor use is disabled.</item>
    /// <item>"1": libdecor use is enabled. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoWaylandAllowLibdecor = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
    
    /// <summary>
    /// Enable or disable hidden mouse pointer warp emulation, needed by some older games.
    /// </summary>
    /// <remarks>
    /// <para>Wayland requires the pointer confinement protocol to warp the mouse, but that is just a hint
    /// that the compositor is free to ignore, and warping the the pointer to or from regions outside of the
    /// focused window is prohibited. When this hint is set and the pointer is hidden, SDL will emulate mouse
    /// warps using relative mouse mode. This is required for some older games (such as Source engine games),
    /// which warp the mouse to the centre of the screen rather than using relative mouse motion. Note that
    /// relative mouse mode may have different mouse acceleration behaviour than pointer warps.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Attempts to warp the mouse will be made, if the appropriate protocol is available.</item>
    /// <item>"1": Some mouse warps will be emulated by forcing relative mouse mode.</item>
    /// </list>
    /// <para>If not set, this is automatically enabled unless an application uses relative mouse mode directly.</para>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintVideoWaylandEmulateMouseWarp = "SDL_VIDEO_WAYLAND_EMULATE_MOUSE_WARP";
    
    /// <summary>
    /// A variable controlling whether video mode emulation is enabled under Wayland.
    /// </summary>
    /// <remarks>
    /// <para>When this hint is set, a standard set of emulated CVT video modes will be exposed for use by the
    /// application. If it is disabled, the only modes exposed will be the logical desktop size and, in the
    /// case of a scaled desktop, the native display resolution.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Video mode emulation is disabled.</item>
    /// <item>"1": Video mode emulation is enabled. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoWaylandModeEmulation = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
    
    /// <summary>
    /// A variable controlling how modes with a non-native aspect ratio are displayed under Wayland.
    /// </summary>
    /// <remarks>
    /// <para>When this hint is set, the requested scaling will be used when displaying fullscreen video modes
    /// that don't match the display's native aspect ratio. This is contingent on compositor viewport support.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"aspect" - Video modes will be displayed scaled, in their proper aspect ratio, with black bars.</item>
    /// <item>"stretch" - Video modes will be scaled to fill the entire display. (default)</item>
    /// <item>"none" - Video modes will be displayed as 1:1 with no scaling.</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintVideoWaylandModeScaling = "SDL_VIDEO_WAYLAND_MODE_SCALING";
    
    /// <summary>
    /// A variable controlling whether the libdecor Wayland backend is preferred over native decorations.
    /// </summary>
    /// <remarks>
    /// <para>When this hint is set, libdecor will be used to provide window decorations,
    /// even if xdg-decoration is available. (Note that, by default, libdecor will use xdg-decoration
    /// itself if available).</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": libdecor is enabled only if server-side decorations are unavailable. (default)</item>
    /// <item>"1": libdecor is always enabled if available.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoWaylandPreferLibdecor = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
    
    /// <summary>
    /// A variable forcing non-DPI-aware Wayland windows to output at 1:1 scaling.
    /// </summary>
    /// <remarks>
    /// <para>This must be set before initializing the video subsystem.</para>
    /// <para>When this hint is set, Wayland windows that are not flagged as being DPI-aware will be output with
    /// scaling designed to force 1:1 pixel mapping.</para>
    /// <para>This is intended to allow legacy applications to be displayed without desktop scaling being applied,
    /// and has issues with certain display configurations, as this forces the window to behave in a way that Wayland
    /// desktops were not designed to accommodate:</para>
    /// <list type="bullet">
    /// <item>Rounding errors can result with odd window sizes and/or desktop scales, which can cause the window
    /// contents to appear slightly blurry.</item>
    /// <item>The window may be unusably small on scaled desktops.</item>
    /// <item>The window may jump in size when moving between displays of different scale factors.</item>
    /// <item>Displays may appear to overlap when using a multi-monitor setup with scaling enabled.</item>
    /// <item>Possible loss of cursor precision due to the logical size of the window being reduced.</item>
    /// </list>
    /// <para>New applications should be designed with proper DPI awareness handling instead of enabling this.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Windows will be scaled normally.</item>
    /// <item>"1": Windows will be forced to scale to achieve 1:1 output.</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintVideoWaylandScaleToDisplay = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    
    /// <summary>
    /// A variable specifying which shader compiler to preload when using the Chrome ANGLE binaries.
    /// </summary>
    /// <remarks>
    /// <para>SDL has EGL and OpenGL ES2 support on Windows via the ANGLE project. It can use two different
    /// sets of binaries, those compiled by the user from source or those provided by the Chrome browser.
    /// In the later case, these binaries require that SDL loads a DLL providing the shader compiler.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"d3dcompiler_46.dll" - best for Vista or later. (default)</item>
    /// <item>"d3dcompiler_43.dll" - for XP support.</item>
    /// <item>"none" - do not load any library, useful if you compiled ANGLE from source and included the
    /// compiler in your binaries.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoWinD3DCompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
    
    /// <summary>
    /// A variable controlling whether the X11 _NET_WM_BYPASS_COMPOSITOR hint should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable _NET_WM_BYPASS_COMPOSITOR.</item>
    /// <item>"1": Enable _NET_WM_BYPASS_COMPOSITOR. (default)</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintVideoX11NetWmBypassCompositor = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
    
    /// <summary>
    /// A variable controlling whether the X11 _NET_WM_PING protocol should be supported.
    /// </summary>
    /// <remarks>
    /// <para>By default SDL will use _NET_WM_PING, but for applications that know they will not always be able
    /// to respond to ping requests in a timely manner they can turn it off to avoid the window manager thinking
    /// the app is hung.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable _NET_WM_PING.</item>
    /// <item>"1": Enable _NET_WM_PING. (default)</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintVideoX11NetWmPing = "SDL_VIDEO_X11_NET_WM_PING";
    
    /// <summary>
    /// A variable forcing the content scaling factor for X11 displays.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to a floating point value in the range 1.0-10.0f</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoX11ScalingFactor = "SDL_VIDEO_X11_SCALING_FACTOR";
    
    /// <summary>
    /// A variable forcing the visual ID chosen for new X11 windows.
    /// </summary>
    /// <remarks>
    /// This hint should be set before creating a window.
    /// </remarks>
    public const string HintVideoX11WindowVisualId = "SDL_VIDEO_X11_WINDOW_VISUALID";
    
    /// <summary>
    /// A variable controlling whether the X11 XRandR extension should be used.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Disable XRandR.</item>
    /// <item>"1": Enable XRandR. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintVideoX11Xrandr = "SDL_VIDEO_X11_XRANDR";
    
    /// <summary>
    /// A variable controlling which touchpad should generate synthetic mouse events.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Only front touchpad should generate mouse events. (default)</item>
    /// <item>"1": Only back touchpad should generate mouse events.</item>
    /// <item>"2": Both touchpads should generate mouse events.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintVitaTouchMouseDevice = "SDL_VITA_TOUCH_MOUSE_DEVICE";
    
    /// <summary>
    /// A variable controlling how the fact chunk affects the loading of a WAVE file.
    /// </summary>
    /// <remarks>
    /// <para>The fact chunk stores information about the number of samples of a WAVE file.
    /// The Standards Update from Microsoft notes that this value can be used to 'determine the length
    /// of the data in seconds'. This is especially useful for compressed formats (for which this is a
    /// mandatory chunk) if they produce multiple sample frames per block and truncating the block is not
    /// allowed. The fact chunk can exactly specify how many sample frames there should be in this case.</para>
    /// <para>Unfortunately, most application seem to ignore the fact chunk and so SDL ignores it
    /// by default as well.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"truncate" - Use the number of samples to truncate the wave data if the fact chunk is
    /// present and valid.</item>
    /// <item>"strict" - Like "truncate", but raise an error if the fact chunk is invalid,
    /// not present for non-PCM formats, or if the data chunk doesn't have that many samples.</item>
    /// <item>"ignorezero" - Like "truncate", but ignore fact chunk if the number of samples is zero.</item>
    /// <item>"ignore" - Ignore fact chunk entirely. (default)</item>
    /// </list>
    /// <para>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAV_IO"/></para>
    /// </remarks>
    public const string HintWaveFactChunk = "SDL_WAVE_FACT_CHUNK";
    
    /// <summary>
    /// A variable controlling how the size of the RIFF chunk affects the loading of a WAVE file.
    /// </summary>
    /// <remarks>
    /// <para>The size of the RIFF chunk (which includes all the sub-chunks of the WAVE file)
    /// is not always reliable. In case the size is wrong, it's possible to just ignore it and
    /// step through the chunks until a fixed limit is reached.</para>
    /// <para>Note that files that have trailing data unrelated to the WAVE file or corrupt files may
    /// slow down the loading process without a reliable boundary. By default, SDL stops after 10000 chunks
    /// to prevent wasting time. Use the environment variable SDL_WAVE_CHUNK_LIMIT to adjust this value.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"force" - Always use the RIFF chunk size as a boundary for the chunk search.</item>
    /// <item>"ignorezero" - Like "force", but a zero size searches up to 4 GiB. (default)</item>
    /// <item>"ignore" - Ignore the RIFF chunk size and always search up to 4 GiB.</item>
    /// <item>"maximum" - Search for chunks until the end of file. (not recommended)</item>
    /// </list>
    /// <para>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAV_IO"/></para>
    /// <para></para>
    /// </remarks>
    public const string HintWaveRiffChunkSize = "SDL_WAVE_RIFF_CHUNK_SIZE";
    
    /// <summary>
    /// A variable controlling how a truncated WAVE file is handled.
    /// </summary>
    /// <remarks>
    /// <para>A WAVE file is considered truncated if any of the chunks are incomplete or the data chunk size
    /// is not a multiple of the block size. By default, SDL decodes until the first incomplete block, as most
    /// applications seem to do.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"verystrict" - Raise an error if the file is truncated.</item>
    /// <item>"strict" - Like "verystrict", but the size of the RIFF chunk is ignored.</item>
    /// <item>"dropframe" - Decode until the first incomplete sample frame.</item>
    /// <item>"dropframe" - Decode until the first incomplete sample frame.</item>
    /// </list>
    /// <para>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAV_IO"/></para>
    /// </remarks>
    public const string HintWaveTruncation = "SDL_WAVE_TRUNCATION";
    
    /// <summary>
    /// A variable controlling whether the window is activated when the <see cref="RaiseWindow"/> function is called.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The window is not activated when the <see cref="RaiseWindow"/> function is called.</item>
    /// <item>"1": The window is activated when the <see cref="RaiseWindow"/> function is called. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowActivateWhenRaised = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
    
    /// <summary>
    /// A variable controlling whether the window is activated when the <see cref="ShowWindow"/> function is called.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The window is not activated when the <see cref="ShowWindow"/> function is called.</item>
    /// <item>"1": The window is activated when the <see cref="ShowWindow"/> function is called. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowActivateWhenShown = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
    
    /// <summary>
    /// If set to "0" then never set the top-most flag on an SDL Window even if the application requests it.
    /// </summary>
    /// <remarks>
    /// <para>This is a debugging aid for developers and not expected to be used by end users.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": don't allow topmost</item>
    /// <item>"1": allow topmost (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowAllowTopmost = "SDL_WINDOW_ALLOW_TOPMOST";
    
    /// <summary>
    /// A variable controlling whether the window frame and title bar are interactive when the cursor is hidden.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The window frame is not interactive when the cursor is hidden (no move, resize, etc).</item>
    /// <item>"1": The window frame is interactive when the cursor is hidden. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowFrameUsableWhileCursorHidden = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
    
    /// <summary>
    /// A variable controlling whether SDL generates window-close events for Alt+F4 on Windows.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": SDL will only do normal key handling for Alt+F4.</item>
    /// <item>"1": SDL will generate a window-close event when it sees Alt+F4. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowsCloseOnAltF4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
    
    /// <summary>
    /// A variable controlling whether menus can be opened with their keyboard shortcut (Alt+mnemonic).
    /// </summary>
    /// <remarks>
    /// <para>If the mnemonics are enabled, then menus can be opened by pressing the Alt key and the
    /// corresponding mnemonic (for example, Alt+F opens the File menu). However, in case an invalid mnemonic
    /// is pressed, Windows makes an audible beep to convey that nothing happened. This is true even if the
    /// window has no menu at all!</para>
    /// <para>Because most SDL applications don't have menus, and some want to use the Alt key for other purposes,
    /// SDL disables mnemonics (and the beeping) by default.</para>
    /// <para>Note: This also affects keyboard events: with mnemonics enabled, when a menu is opened from the keyboard,
    /// you will not receive a KEYUP event for the mnemonic key, and might not receive one for Alt.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Alt+mnemonic does nothing, no beeping. (default)</item>
    /// <item>"1": Alt+mnemonic opens menus, invalid mnemonics produce a beep.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowsEnableMenuMnemonics = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
    
    /// <summary>
    /// A variable controlling whether the windows message loop is processed by SDL.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The window message loop is not run.</item>
    /// <item>"1": The window message loop is processed in <see cref="PumpEvents"/>. (default)</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowsEnableMessageloop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
    
    /// <summary>
    /// A variable controlling whether SDL will clear the window contents when the WM_ERASEBKGND message is received.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0"/"never": Never clear the window.</item>
    /// <item>"1"/"initial": Clear the window when the first WM_ERASEBKGND event fires. (default)</item>
    /// <item>"2"/"always": Clear the window on every WM_ERASEBKGND event.</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintWindowsEraseBackgroundMode = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
    
    /// <summary>
    /// A variable controlling whether SDL uses Critical Sections for mutexes on Windows.
    /// </summary>
    /// <remarks>
    /// <para>On Windows 7 and newer, Slim Reader/Writer Locks are available. They offer better performance,
    /// allocate no kernel resources and use less memory. SDL will fall back to Critical Sections on older
    /// OS versions or if forced to by this hint.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use SRW Locks when available, otherwise fall back to Critical Sections. (default)</item>
    /// <item>"1": Force the use of Critical Sections in all cases.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintWindowsForceMutexCriticalSections = "SDL_WINDOWS_FORCE_MUTEX_CRITICAL_SECTIONS";
    
    /// <summary>
    /// A variable controlling whether SDL uses Kernel Semaphores on Windows.
    /// </summary>
    /// <remarks>
    /// <para>Kernel Semaphores are inter-process and require a context switch on every interaction.
    /// On Windows 8 and newer, the WaitOnAddress API is available. Using that and atomics to implement semaphores
    /// increases performance. SDL will fall back to Kernel Objects on older
    /// OS versions or if forced to by this hint.</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use Atomics and WaitOnAddress API when available, otherwise fall
    /// back to Kernel Objects. (default)</item>
    /// <item>"1": Force the use of Kernel Objects in all cases.</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintWindowsForceSemaphoreKernel = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    
    /// <summary>
    /// A variable to specify custom icon resource id from RC file on Windows platform.
    /// </summary>
    /// <remarks>
    /// This hint should be set before SDL is initialized.
    /// </remarks>
    public const string HintWindowsIntResourceIcon = "SDL_WINDOWS_INTRESOURCE_ICON";
    
    /// <summary>
    /// A variable controlling whether raw keyboard events are used on Windows.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": The Windows message loop is used for keyboard events. (default)</item>
    /// <item>"1": Low latency raw keyboard events are used.</item>
    /// </list>
    /// <para>This hint can be set anytime.</para>
    /// </remarks>
    public const string HintWindowsRawKeyboard = "SDL_WINDOWS_RAW_KEYBOARD";
    
    /// <summary>
    /// A variable controlling whether SDL uses the D3D9Ex API introduced in Windows Vista, instead of normal D3D9.
    /// </summary>
    /// <remarks>
    /// <para>Direct3D 9Ex contains changes to state management that can eliminate device loss errors during
    /// scenarios like Alt+Tab or UAC prompts. D3D9Ex may require some changes to your application to cope with
    /// the new behavior, so this is disabled by default.</para>
    /// <para>For more information on Direct3D 9Ex, see:</para>
    /// <list type="bullet">
    /// <item>https://docs.microsoft.com/en-us/windows/win32/direct3darticles/graphics-apis-in-windows-vista#direct3d-9ex</item>
    /// <item>https://docs.microsoft.com/en-us/windows/win32/direct3darticles/direct3d-9ex-improvements</item>
    /// </list>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Use the original Direct3D 9 API. (default)</item>
    /// <item>"1": Use the Direct3D 9Ex API on Vista and later (and fall back if D3D9Ex is unavailable)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintWindowsUseD3D9Ex = "SDL_WINDOWS_USE_D3D9EX";
    
    /// <summary>
    /// A variable controlling whether back-button-press events on Windows Phone to be marked as handled.
    /// </summary>
    /// <remarks>
    /// <para>Windows Phone devices typically feature a Back button. When pressed, the OS will emit back-button-press
    /// events, which apps are expected to handle in an appropriate manner. If apps do not explicitly mark these events
    /// as 'Handled', then the OS will invoke its default behavior for unhandled back-button-press events, which on
    /// [Windows Phone 8 and 8.1 is to terminate the app (and attempt to switch to the previous app, or to the device's
    /// home screen).</para>
    /// <para>Setting the <see cref="HintWinRTHandleBackButton"/> hint to "1" will cause SDL to mark
    /// back-button-press events as Handled, if and when one is sent to the app.</para>
    /// <para>Internally, Windows Phone sends back button events as parameters to special back-button-press callback
    /// functions. Apps that need to respond to back-button-press events are expected to register one or more
    /// callback functions for such, shortly after being launched (during the app's initialization phase).
    /// After the back button is pressed, the OS will invoke these callbacks. If the app's callback(s) do not
    /// explicitly mark the event as handled by the time they return, or if the app never registers one of
    /// these callback, the OS will consider the event un-handled, and it will apply its default back button
    /// behavior (terminate the app).</para>
    /// <para>SDL registers its own back-button-press callback with the Windows Phone OS. This callback will
    /// emit a pair of SDL key-press events (<see cref="EventType.KeyDown"/> and <see cref="EventType.KeyUp"/>),
    /// each with a scancode of SDL_SCANCODE_AC_BACK, after which it will check
    /// the contents of the hint, <see cref="HintWinRTHandleBackButton"/>.
    /// If the hint's value is set to "1", the back button event's Handled property will get set to 'true'.
    /// If the hint's value is set to something else, or if it is unset, SDL will leave the event's Handled
    /// property alone. (By default, the OS sets this property to 'false', to note.)</para>
    /// <para>SDL apps can either set <see cref="HintWinRTHandleBackButton"/> well before a back button is
    /// pressed, or can set it in direct-response to a back button being pressed.</para>
    /// <para>In order to get notified when a back button is pressed, SDL apps should register a
    /// callback function with <see cref="AddEventWatch"/>, and have it listen for <see cref="EventType.KeyDown"/>
    /// events that have a scancode of SDL_SCANCODE_AC_BACK. (Alternatively, <see cref="EventType.KeyUp"/> events can
    /// be listened-for. Listening for either event type is suitable.) Any value of
    /// <see cref="HintWinRTHandleBackButton"/> set by such a callback, will be applied to the OS' current
    /// back-button-press event.</para>
    /// <para>More details on back button behavior in Windows Phone apps can be found at the following page, on
    /// Microsoft's developer site:
    /// http://msdn.microsoft.com/en-us/library/windowsphone/develop/jj247550(v=vs.105).aspx</para>
    /// </remarks>
    public const string HintWinRTHandleBackButton = "SDL_WINRT_HANDLE_BACK_BUTTON";
    
    /// <summary>
    /// A variable specifying the label text for a WinRT app's privacy policy link.
    /// </summary>
    /// <remarks>
    /// <para>Network-enabled WinRT apps must include a privacy policy. On Windows 8, 8.1, and RT,
    /// Microsoft mandates that this policy be available via the Windows Settings charm. SDL provides code
    /// to add a link there, with its label text being set via the optional hint,
    /// <see cref="HintWinRTPrivacyPolicyLabel"/>.</para>
    /// <para>Please note that a privacy policy's contents are not set via this hint.
    /// A separate hint, <see cref="HintWinRTPrivacyPolicyUrl"/>, is used to link to the actual text
    /// of the policy.</para>
    /// <para>The contents of this hint should be encoded as a UTF8 string.</para>
    /// <para>The default value is "Privacy Policy".</para>
    /// <para>For additional information on linking to a privacy policy, see the documentation
    /// for <see cref="HintWinRTPrivacyPolicyUrl"/>.</para>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintWinRTPrivacyPolicyLabel = "SDL_WINRT_PRIVACY_POLICY_LABEL";
    
    /// <summary>
    /// A variable specifying the URL to a WinRT app's privacy policy.
    /// </summary>
    /// <remarks>
    /// <para>All network-enabled WinRT apps must make a privacy policy available to its users.
    /// On Windows 8, 8.1, and RT, Microsoft mandates that this policy be available in the Windows
    /// Settings charm, as accessed from within the app. SDL provides code to add a URL-based link there,
    /// which can point to the app's privacy policy.</para>
    /// <para>To setup a URL to an app's privacy policy, set <see cref="HintWinRTPrivacyPolicyUrl"/> before
    /// calling any <see cref="Init"/> functions. The contents of the hint should be a
    /// valid URL. For example, "http://www.example.com".</para>
    /// <para>The default value is "", which will prevent SDL from adding a privacy policy link to the
    /// Settings charm. This hint should only be set during app init.</para>
    /// <para>The label text of an app's "Privacy Policy" link may be customized via another hint,
    /// <see cref="HintWinRTPrivacyPolicyLabel"/>.</para>
    /// <para>Please note that on Windows Phone, Microsoft does not provide standard UI for displaying a privacy
    /// policy link, and as such, <see cref="HintWinRTPrivacyPolicyUrl"/> will not get used on that platform.
    /// Network-enabled phone apps should display their privacy policy through some other, in-app means.</para>
    /// </remarks>
    public const string HintWinRTPrivacyPolicyUrl = "SDL_WINRT_PRIVACY_POLICY_URL";
    
    /// <summary>
    /// A variable controlling whether X11 windows are marked as override-redirect.
    /// </summary>
    /// <remarks>
    /// <para>If set, this might increase framerate at the expense of the desktop not working as expected.
    /// Override-redirect windows aren't noticed by the window manager at all.</para>
    /// <para>You should probably only use this for fullscreen windows, and you probably shouldn't even use it for
    /// that. But it's here if you want to try!</para>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": Do not mark the window as override-redirect. (default)</item>
    /// <item>"1": Mark the window as override-redirect.</item>
    /// </list>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintX11ForceOverrideRedirect = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    
    /// <summary>
    /// A variable specifying the type of an X11 window.
    /// </summary>
    /// <remarks>
    /// <para>During SDL_CreateWindow, SDL uses the _NET_WM_WINDOW_TYPE X11 property to report to the window manager
    /// the type of window it wants to create. This might be set to various things if <see cref="WindowFlags.Tooltip"/>
    /// or <see cref="WindowFlags.PopupMenu"/>, etc, were specified. For "normal" windows that haven't set
    /// a specific type, this hint can be used to specify a custom type. For example, a dock window might
    /// set this to "_NET_WM_WINDOW_TYPE_DOCK".</para>
    /// <para>This hint should be set before creating a window.</para>
    /// </remarks>
    public const string HintX11WindowType = "SDL_X11_WINDOW_TYPE";
    
    /// <summary>
    /// A variable controlling whether XInput should be used for controller handling.
    /// </summary>
    /// <remarks>
    /// <para>The variable can be set to the following values:</para>
    /// <list type="bullet">
    /// <item>"0": XInput is not enabled.</item>
    /// <item>"1": XInput is enabled. (default)</item>
    /// </list>
    /// <para>This hint should be set before SDL is initialized.</para>
    /// </remarks>
    public const string HintXInputEnabled = "SDL_XINPUT_ENABLED";
    #endregion
    
    
    /// <summary>
    /// An enumeration of hint priorities.
    /// </summary>
    public enum HintPriority
    {
        Default,
        Normal,
        Override
    }
    
    
    /// <summary>
    /// Type definition of the hint callback function.
    /// </summary>
    /// <param name="userdata">what was passed as userdata to <see cref="AddHintCallback"/>.</param>
    /// <param name="name">what was passed as name to <see cref="AddHintCallback"/>.</param>
    /// <param name="oldValue">the previous hint value.</param>
    /// <param name="newValue">the new value hint is to be set to.</param>
    public delegate void HintCallback(IntPtr userdata, string name, string oldValue, string newValue);

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_AddHintCallback(byte* name, HintCallback callback, IntPtr userdata);
    /// <summary>
    /// Add a function to watch a particular hint.
    /// </summary>
    /// <param name="name">the hint to watch.</param>
    /// <param name="callback">an <see cref="HintCallback"/> function that will be called when the
    /// hint value changes.</param>
    /// <returns>Returns 0 on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <remarks>It is NOT safe to call this function from two threads at once.</remarks>
    public static unsafe int AddHintCallback(string name, HintCallback callback)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_AddHintCallback(Utf8Encode(name, utf8Name, utf8NameBufSize), callback, IntPtr.Zero);
    }

    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_DelHintCallback(byte* name, HintCallback callback, IntPtr userdata);
    /// <summary>
    /// Remove a function watching a particular hint.
    /// </summary>
    /// <param name="name">the hint being watched.</param>
    /// <param name="callback">	an <see cref="HintCallback"/> function that will be called when the hint
    /// value changes.</param>
    public static unsafe void DelHintCallback(string name, HintCallback callback)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        SDL_DelHintCallback(Utf8Encode(name, utf8Name, utf8NameBufSize), callback, IntPtr.Zero);
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_GetHint(byte* name);
    /// <summary>
    /// Get the value of a hint.
    /// </summary>
    /// <param name="name">the hint to query.</param>
    /// <returns>Returns the string value of a hint or NULL if the hint isn't set.</returns>
    /// <remarks>The returned string follows the <see cref="GetStringRule"/>.</remarks>
    public static unsafe string? GetHint(string name)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return UTF8_ToManaged(SDL_GetHint(Utf8Encode(name, utf8Name, utf8NameBufSize)));
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_GetHintBoolean(byte* name, int defaultValue);
    /// <summary>
    /// Get the boolean value of a hint variable.
    /// </summary>
    /// <param name="name">the name of the hint to get the boolean value from.</param>
    /// <param name="defaultValue">the value to return if the hint does not exist.</param>
    /// <returns>Returns the boolean value of a hint or the provided default value if the hint does not exist.</returns>
    public static unsafe bool GetHintBoolean(string name, bool defaultValue)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_GetHintBoolean(Utf8Encode(name, utf8Name, utf8NameBufSize), defaultValue ? 1 : 0) != 0;
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_ResetHint(byte* name);
    /// <summary>
    /// Reset a hint to the default value.
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <returns>Returns True if the hint was set, False otherwise.</returns>
    /// <remarks>
    /// This will reset a hint to the value of the environment variable,
    /// or NULL if the environment isn't set. Callbacks will be called normally with this change.
    /// </remarks>
    public static unsafe bool ResetHint(string name)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];
        return SDL_ResetHint(Utf8Encode(name, utf8Name, utf8NameBufSize)) != 0;
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_ResetHints();
    /// <summary>
    /// Reset all hints to the default values.
    /// </summary>
    /// <remarks>
    /// This will reset all hints to the value of the associated environment variable,
    /// or NULL if the environment isn't set. Callbacks will be called normally with this change.
    /// </remarks>
    public static void ResetHints() => SDL_ResetHints();
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_SetHint(byte* name, byte* value);
    /// <summary>
    /// Set a hint with normal priority.
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <returns>Returns True if the hint was set, False otherwise.</returns>
    /// <remarks>
    /// Hints will not be set if there is an existing override hint or environment variable that takes precedence.
    /// You can use <see cref="SetHintWithPriority"/> to set the hint with override priority instead.
    /// </remarks>
    public static unsafe bool SetHint(string name, string value)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];

        var utf8ValueBufSize = Utf8Size(value);
        var utf8Value = stackalloc byte[utf8ValueBufSize];

        return SDL_SetHint(
            Utf8Encode(name, utf8Name, utf8NameBufSize),
            Utf8Encode(value, utf8Value, utf8ValueBufSize)) != 0;
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_SetHintWithPriority(byte* name, byte* value, HintPriority priority);
    /// <summary>
    /// Set a hint with a specific priority.
    /// </summary>
    /// <param name="name">the hint to set.</param>
    /// <param name="value">the value of the hint variable.</param>
    /// <param name="priority">the <see cref="HintPriority"/> level for the hint.</param>
    /// <returns>Returns True if the hint was set, False otherwise.</returns>
    /// <remarks>The priority controls the behavior when setting a hint that already has a value.
    /// Hints will replace existing hints of their priority and lower.
    /// Environment variables are considered to have override priority.</remarks>
    public static unsafe bool SetHintWithPriority(string name, string value, HintPriority priority)
    {
        var utf8NameBufSize = Utf8Size(name);
        var utf8Name = stackalloc byte[utf8NameBufSize];

        var utf8ValueBufSize = Utf8Size(value);
        var utf8Value = stackalloc byte[utf8ValueBufSize];

        return SDL_SetHintWithPriority(
            Utf8Encode(name, utf8Name, utf8NameBufSize),
            Utf8Encode(value, utf8Value, utf8ValueBufSize), priority) != 0;
    }
}