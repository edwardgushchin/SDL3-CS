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

using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    #region hints
    /// <summary>
    /// Specify the behavior of Alt+Tab while the keyboard is grabbed.
    /// </summary>
    /// <remarks>
    /// By default, SDL emulates Alt+Tab functionality while the keyboard is grabbed and your window is full-screen.
    /// This prevents the user from getting stuck in your application if you've enabled keyboard grab.
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
    /// <item>"0": Back button will be handled as usual for system. (default)</item>
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
    /// which is likely to be installed.
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
    /// behalf. This hint needs to be set before <see cref="Init"/> is called to be useful.</para>
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
    /// <para>This hint must be set before <see cref="StartTextInput"/> is called</para>
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
    /// OutputDebugString() on Windows, and can be funneled by the app with <see cref="SetLogOutputFunction"/>,
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
    public const string HintFileDialogDriver = "SDL_FILE_DIALOG_DRIVER";
    
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
    /// <item>"0": Sensor fusion is disabled</item>
    /// <item>"1": Sensor fusion is enabled for all controllers that lack sensors</item>
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
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintGdkTextinputMaxLength = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintGdkTextinputScope = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintGdkTextinputTitle = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintHidapiEnumerateOnlyControllers = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintHidapiIgnoreDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintImeImplementedUI = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintImeInternalEditing = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintImeNativeUI = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintImeShowUI = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintIosHideHomeIndicator = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickAllowBackgroundEvents = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickArcadestickDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickArcadestickDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickBlacklistDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickBlacklistDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickDevice = "";
    
    /// <summary>
    /// 
    /// </summary>
    public const string HintJoystickDirectInput = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickFlightstickDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickFlightstickDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickGamecubeDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickGamecubeDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapi = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiCombineJoyCons = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiGamecube = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiGamecubeRumbleBrake = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiJoyCons = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiJoyconHomeLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiLuna = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiNintendoClassic = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS3 = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS3SixaxisDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS4 = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS4ReportInterval = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS4Rumble = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS5 = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS5PlayerLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiPS5Rumble = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiShield = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiStadia = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiSteam = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiSteamdeck = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiSwitch = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiSwitchHomeLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiSwitchPlayerLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiVerticalJoyCons = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiWii = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiWiiPlayerLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXbox = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXbox360 = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXbox360PlayerLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXbox360Wireless = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXboxOne = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickHidapiXboxOneHomeLed = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickIokit = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickLinuxClassic = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickLinuxDeadzones = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickLinuxDigitalHats = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickLinuxHatDeadzones = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickMfi = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickRawinput = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickRawinputCorrelateXinput = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickRogChakram = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickThread = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickThrottleDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickThrottleDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickWgi = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickWheelDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickWheelDevicesExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintJoystickZeroCenteredDevices = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintKeycodeOptions = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintKmsdrmDeviceIndex = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintKmsdrmRequireDrmMaster = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintLogging = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMacBackgroundApp = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMacCtrlClickEmulateRightClick = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMacOpenglAsyncDispatch = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMainCallbackRate = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseAutoCapture = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseDoubleClickRadius = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseDoubleClickTime = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseFocusClickthrough = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseNormalSpeedScale = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeClipInterval = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeCursorVisible = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeModeCenter = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeModeWarp = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeSpeedScale = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeSystemScale = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseRelativeWarpMotion = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintMouseTouchEvents = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintNoSignalHandlers = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintOpenglEsDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintOrientations = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintPenDelayMouseButton = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintPenNotMouse = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintPollSentinel = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintPreferredLocales = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintQuitOnLastWindowClose = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderDirect3D11Debug = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderDirect3DThreadsafe = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderLineMethod = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderMetalPreferLowPowerDevice = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderVsync = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRenderVulkanDebug = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintReturnKeyHidesIme = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRogGamepadMice = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRogGamepadMiceExcluded = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintRpiVideoLayer = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintScreensaverInhibitActivityName = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintShutdownDbusOnQuit = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintStorageTitleDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintStorageUserDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintThreadForceRealtimeTimeCritical = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintThreadPriorityPolicy = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintTimerResolution = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintTouchMouseEvents = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintTrackpadIsTouchOnly = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintTVRemoteAsJoystick = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoAllowScreensaver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoDoubleBuffer = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoDriver = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoEglAllowGetdisplayFallback = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoForceEgl = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoMacFullscreenSpaces = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoMinimizeOnFocusLoss = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoSyncWindowOperations = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandAllowLibdecor = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandEmulateMouseWarp = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandModeEmulation = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandModeScaling = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandPreferLibdecor = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWaylandScaleToDisplay = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoWinD3Dcompiler = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoX11NetWmBypassCompositor = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoX11NetWmPing = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoX11ScalingFactor = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoX11WindowVisualid = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVideoX11Xrandr = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintVitaTouchMouseDevice = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWaveFactChunk = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWaveRiffChunkSize = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWaveTruncation = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowActivateWhenRaised = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowActivateWhenShown = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowAllowTopmost = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowFrameUsableWhileCursorHidden = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsCloseOnAltF4 = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsEnableMenuMnemonics = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsEnableMessageloop = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsEraseBackgroundMode = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsForceMutexCriticalSections = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsForceSemaphoreKernel = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsIntresourceIcon = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsRawKeyboard = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWindowsUseD3D9Ex = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWinrtHandleBackButton = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWinrtPrivacyPolicyLabel = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintWinrtPrivacyPolicyUrl = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintX11ForceOverrideRedirect = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintX11WindowType = "";
    
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <para></para>
    /// <list type="bullet">
    /// <item></item>
    /// <item></item>
    /// <item></item>
    /// </list>
    /// <para></para>
    /// <para></para>
    /// </remarks>
    public const string HintXinputEnabled = "";
    #endregion
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_SetHint(byte* name, byte* value);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    /// <returns></returns>
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
}