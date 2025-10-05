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

namespace SDL3;

public static partial class SDL
{
    /// <summary>
    /// <para>This file contains functions to set and get configuration hints, as well as
    /// listing each of them alphabetically.</para>
    /// <para>The convention for naming hints is X, where "SDL_X" is the
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
        /// <item><c>"0"</c>: SDL will not handle Alt+Tab. Your application is responsible for
        /// handling Alt+Tab while the keyboard is grabbed.</item>
        /// <item><c>"1"</c>: SDL will minimize your window when Alt+Tab is pressed (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <item><c>"0"</c>: The application starts fresh at each launch. (default)</item>
        /// <item><c>"1"</c>: The application activity can be recreated by the OS.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AndroidAllowRecreateActivity = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";
        
        /// <summary>
        /// <para>A variable to control whether the event loop will block itself when the app
        /// is paused.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Non blocking.</item>
        /// <item><c>"1"</c>: Blocking. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AndroidBlockOnPause = "SDL_ANDROID_BLOCK_ON_PAUSE";
        
        /// <summary>
        /// <para>A variable to control whether low latency audio should be enabled.</para>
        /// <para>Some devices have poor quality output when this is enabled, but this is
        /// usually an improvement in audio latency.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Low latency audio is not enabled.</item>
        /// <item><c>"1"</c>: Low latency audio is enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL audio is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AndroidLowLatencyAudio = "SDL_ANDROID_LOW_LATENCY_AUDIO";
        
        /// <summary>
        /// <para>A variable to control whether we trap the Android back button to handle it
        /// manually.</para>
        /// <para>This is necessary for the right mouse button to work on some Android
        /// devices, or to be able to trap the back button for use in your code
        /// reliably. If this hint is true, the back button will show up as an
        /// <see cref="EventType.KeyDown"/> / <see cref="EventType.KeyUp"/> pair with a keycode of
        /// <see cref="Scancode.ACBack"/>.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item>"0": Back button will be handled as usual for system. (default)</item>
        /// <item>"1": Back button will be trapped, allowing you to handle the key press
        /// manually. (This will also let right mouse click work on systems where the
        /// right mouse button functions as back.)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AndroidTrapBackButton = "SDL_ANDROID_TRAP_BACK_BUTTON";
        
        /// <summary>
        /// <para>A variable setting the app ID string.</para>
        /// <para>This string is used by desktop compositors to identify and group windows
        /// together, as well as match applications with associated desktop settings
        /// and icons.</para>
        /// <para>This will override <see cref="Props.AppMetadataIdentifierString"/>, if set by the
        /// application.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AppID = "SDL_APP_ID";
        
        /// <summary>
        /// <para>A variable setting the application name.</para>
        /// <para>This hint lets you specify the application name sent to the OS when
        /// required. For example, this will often appear in volume control applets for
        /// audio streams, and in lists of applications which are inhibiting the
        /// screensaver. You should use a string that describes your program (<c>"My Game
        /// 2: The Revenge"</c>)</para>
        /// <para>This will override <see cref="Props.AppMetadataNameString"/>, if set by the
        /// application.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <item><c>"0"</c>: Controller input does not generate UI events. (default)</item>
        /// <item><c>"1"</c>: Controller input generates UI events.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AppleTVRemoteAllowRotation = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
        
        /// <summary>
        /// <para>Specify the default ALSA audio device name.</para>
        /// <para>This variable is a specific audio device to open when the "default" audio
        /// device is used.</para>
        /// <para>This hint will be ignored when opening the default playback device if
        /// <see cref="AudioALSADefaultPlaybackDevice"/> is set, or when opening the
        /// default recording device if <see cref="AudioALSADefaultRecordingDevice"/>
        /// is set.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        /// <seealso cref="AudioALSADefaultPlaybackDevice"/>
        /// <seealso cref="AudioALSADefaultRecordingDevice"/>
        public const string AudioALSADefaultDevice = "SDL_AUDIO_ALSA_DEFAULT_DEVICE";

        /// <summary>
        /// <para>Specify the default ALSA audio playback device name.</para>
        /// <para>This variable is a specific audio device to open for playback, when the
        /// "default" audio device is used.</para>
        /// <para>If this hint isn't set, SDL will check <see cref="AudioALSADefaultDevice"/>
        /// before choosing a reasonable default.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.1.7.</since>
        /// <seealso cref="AudioALSADefaultRecordingDevice"/>
        /// <seealso cref="AudioALSADefaultDevice"/>
        public const string AudioALSADefaultPlaybackDevice = "SDL_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE";

        /// <summary>
        /// <para>Specify the default ALSA audio recording device name.</para>
        /// <para>This variable is a specific audio device to open for recording, when the
        /// "default" audio device is used.</para>
        /// <para>If this hint isn't set, SDL will check <see cref="AudioALSADefaultDevice"/>
        /// before choosing a reasonable default.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.1.7.</since>
        /// <seealso cref="AudioALSADefaultPlaybackDevice"/>
        /// <seealso cref="AudioALSADefaultDevice"/>
        public const string AudioALSADefaultRecordingDevice = "SDL_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE";
        
        /// <summary>
        /// <para>A variable controlling the audio category on iOS and macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"ambient"</c>: Use the AVAudioSessionCategoryAmbient audio category, will be
        /// muted by the phone mute switch (default)</item>
        /// <item><c>"playback"</c>: Use the AVAudioSessionCategoryPlayback category.</item>
        /// </list>
        /// <para>For more information, see Apple's documentation:
        /// https://developer.apple.com/library/content/documentation/Audio/Conceptual/AudioSessionProgrammingGuide/AudioSessionCategoriesandModes/AudioSessionCategoriesandModes.html</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioCategory = "SDL_AUDIO_CATEGORY";

        /// <summary>
        /// <para>A variable controlling the default audio channel count.</para>
        /// <para>If the application doesn't specify the audio channel count when opening the
        /// device, this hint can be used to specify a default channel count that will
        /// be used. This defaults to <c>"1"</c> for recording and <c>"2"</c> for playback devices.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioChannels = "SDL_AUDIO_CHANNELS";
        
        /// <summary>
        /// <para>Specify an application icon name for an audio device.</para>
        /// <para>Some audio backends (such as Pulseaudio and Pipewire) allow you to set an
        /// XDG icon name for your application. Among other things, this icon might
        /// show up in a system control panel that lets the user adjust the volume on
        /// specific audio streams instead of using one giant master volume slider.
        /// Note that this is unrelated to the icon used by the windowing system, which
        /// may be set with <see cref="SetWindowIcon"/> (or via desktop file on Wayland).</para>
        /// <para>Setting this to <c>""</c> or leaving it unset will have SDL use a reasonable
        /// default, <c>"applications-games"</c>, which is likely to be installed. See
        /// https://specifications.freedesktop.org/icon-theme-spec/icon-theme-spec-latest.html
        /// and
        /// https://specifications.freedesktop.org/icon-naming-spec/icon-naming-spec-latest.html
        /// for the relevant XDG icon specs.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDeviceSampleFrames = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";
        
        /// <summary>
        /// <para>Specify an audio stream name for an audio device.</para>
        /// <para>Some audio backends (such as PulseAudio) allow you to describe your audio
        /// stream. Among other things, this description might show up in a system
        /// control panel that lets the user adjust the volume on specific audio
        /// streams instead of using one giant master volume slider.</para>
        /// <para>This hints lets you transmit that information to the OS. The contents of
        /// this hint are used while opening an audio device. You should use a string
        /// that describes your what your program is playing (<c>"audio stream"</c> is
        /// probably sufficient in many cases, but this could be useful for something
        /// like "team chat" if you have a headset playing VoIP audio separately).</para>
        /// <para>Setting this to <c>""</c> or leaving it unset will have SDL use a reasonable
        /// default: <c>"audio stream"</c> or something similar.</para>
        /// <para>Note that while this talks about audio streams, this is an OS-level
        /// concept, so it applies to a physical audio device in this case, and not an
        /// SDL_AudioStream, nor an SDL logical audio device.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <para>Setting this to <c>""</c> or leaving it unset will have SDL use a reasonable
        /// default: <c>"Game"</c> or something similar.</para>
        /// <para>Note that while this talks about audio streams, this is an OS-level
        /// concept, so it applies to a physical audio device in this case, and not an
        /// SDL_AudioStream, nor an SDL logical audio device.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDeviceStreamRole = "SDL_AUDIO_DEVICE_STREAM_ROLE";
        
        /// <summary>
        /// <para>Specify the input file when recording audio using the disk audio driver.</para>
        /// <para>This defaults to <c>"sdlaudio-in.raw"</c>;</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDiskInputFile = "SDL_AUDIO_DISK_INPUT_FILE";
        
        /// <summary>
        /// <para>Specify the output file when playing audio using the disk audio driver.</para>
        /// <para>This defaults to <c>"sdlaudio.raw"</c>;</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDiskOutputFile = "SDL_AUDIO_DISK_OUTPUT_FILE";
        
        /// <summary>
        /// <para>A variable controlling the audio rate when using the disk audio driver.</para>
        /// <para>The disk audio driver normally simulates real-time for the audio rate that
        /// was specified, but you can use this variable to adjust this rate higher or
        /// lower down to 0. The default value is <c>"1.0"</c>.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDiskTimescale = "SDL_AUDIO_DISK_TIMESCALE";
        
        /// <summary>
        /// <para>A variable that specifies an audio backend to use.</para>
        /// <para>By default, SDL will try all available audio backends in a reasonable order
        /// until it finds one that can work, but this hint allows the app or user to
        /// force a specific driver, such as <c>"pipewire"</c> if, say, you are on PulseAudio
        /// but want to try talking to the lower level instead.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDriver = "SDL_AUDIO_DRIVER";
        
        /// <summary>
        /// <para>A variable controlling the audio rate when using the dummy audio driver.</para>
        /// <para>The dummy audio driver normally simulates real-time for the audio rate that
        /// was specified, but you can use this variable to adjust this rate higher or
        /// lower down to 0. The default value is <c>"1.0"</c>.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioDummyTimescale = "SDL_AUDIO_DUMMY_TIMESCALE";
        
        /// <summary>
        /// <para>A variable controlling the default audio format.</para>
        /// <para>If the application doesn't specify the audio format when opening the
        /// device, this hint can be used to specify a default format that will be
        /// used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"U8"</c>: Unsigned 8-bit audio</item>
        /// <item><c>"S8"</c>: Signed 8-bit audio</item>
        /// <item><c>"S16LE"</c>: Signed 16-bit little-endian audio</item>
        /// <item><c>"S16BE"</c>: Signed 16-bit big-endian audio</item>
        /// <item><c>"S16"</c>: Signed 16-bit native-endian audio (default)</item>
        /// <item><c>"S32LE"</c>: Signed 32-bit little-endian audio</item>
        /// <item><c>"S32BE"</c>: Signed 32-bit big-endian audio</item>
        /// <item><c>"S32"</c>: Signed 32-bit native-endian audio</item>
        /// <item><c>"F32LE"</c>: Floating point little-endian audio</item>
        /// <item><c>"F32BE"</c>: Floating point big-endian audio</item>
        /// <item><c>"F32"</c>: Floating point native-endian audio</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioFormat = "SDL_AUDIO_FORMAT";
        
        /// <summary>
        /// <para>A variable controlling the default audio frequency.</para>
        /// <para>If the application doesn't specify the audio frequency when opening the
        /// device, this hint can be used to specify a default frequency that will be
        /// used. This defaults to <c>"44100"</c>.</para>
        /// </summary>
        /// <remarks>This hint should be set before an audio device is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioFrequency = "SDL_AUDIO_FREQUENCY";
        
        /// <summary>
        /// <para>A variable that causes SDL to not ignore audio <c>"monitors"</c>.</para>
        /// <para>This is currently only used by the PulseAudio driver.</para>
        /// <para>By default, SDL ignores audio devices that aren't associated with physical
        /// hardware. Changing this hint to <c>"1"</c> will expose anything SDL sees that
        /// appears to be an audio source or sink. This will add <c>"devices"</c> to the list
        /// that the user probably doesn't want or need, but it can be useful in
        /// scenarios where you want to hook up SDL to some sort of virtual device,
        /// etc.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Audio monitor devices will be ignored. (default)</item>
        /// <item><c>"1"</c>: Audio monitor devices will show up in the device list.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AudioIncludeMonitors = "SDL_AUDIO_INCLUDE_MONITORS";
        
        /// <summary>
        /// <para>A variable controlling whether SDL updates joystick state when getting
        /// input events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: You'll call <see cref="UpdateJoysticks"/> manually.</item>
        /// <item><c>"1"</c>: SDL will automatically call <see cref="UpdateJoysticks"/>. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AutoUpdateJoysticks = "SDL_AUTO_UPDATE_JOYSTICKS";
        
        /// <summary>
        /// <para>A variable controlling whether SDL updates sensor state when getting input
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: You'll call <see cref="UpdateSensors"/> manually.</item>
        /// <item><c>"1"</c>: SDL will automatically call <see cref="UpdateSensors"/>. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string AutoUpdateSensors = "SDL_AUTO_UPDATE_SENSORS";
        
        /// <summary>
        /// <para>Prevent SDL from using version 4 of the bitmap header when saving BMPs.</para>
        /// <para>The bitmap header version 4 is required for proper alpha channel support
        /// and SDL will use it when required. Should this not be desired, this hint
        /// can force the use of the 40 byte header version which is supported
        /// everywhere.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Surfaces with a colorkey or an alpha channel are saved to a 32-bit
        /// BMP file with an alpha mask. SDL will use the bitmap header version 4 and
        /// set the alpha mask accordingly. (default)</item>
        /// <item><c>"1"</c>: Surfaces with a colorkey or an alpha channel are saved to a 32-bit
        /// BMP file without an alpha mask. The alpha channel data will be in the
        /// file, but applications are going to ignore it.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string BMPSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";
        
        /// <summary>
        /// <para>A variable that decides what camera backend to use.</para>
        /// <para>By default, SDL will try all available camera backends in a reasonable
        /// order until it finds one that can work, but this hint allows the app or
        /// user to force a specific target, such as <c>"directshow"</c> if, say, you are on
        /// Windows Media Foundations but want to try DirectShow instead.</para>
        /// <para>The default value is unset, in which case SDL will try to figure out the
        /// best camera backend on your behalf. This hint needs to be set before
        /// <see cref="SDL.Init(SDL.InitFlags)"/> is called to be useful.</para>
        /// </summary>
        /// <remarks></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
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
        /// <item><c>"all";</c></item>
        /// <item><c>"altivec";</c></item>
        /// <item><c>"sse";</c></item>
        /// <item><c>"sse2";</c></item>
        /// <item><c>"sse3";</c></item>
        /// <item><c>"sse41";</c></item>
        /// <item><c>"sse42";</c></item>
        /// <item><c>"avx";</c></item>
        /// <item><c>"avx2";</c></item>
        /// <item><c>"avx512f";</c></item>
        /// <item><c>"arm-simd";</c></item>
        /// <item><c>"neon";</c></item>
        /// <item><c>"lsx";</c></item>
        /// <item><c>"lasx";</c></item>
        /// </list>
        /// </summary>
        /// <remarks>The items can be prefixed by <c>'+'/'-'</c> to add/remove features.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string CPUFeatureMask = "SDL_CPU_FEATURE_MASK";
        
        /// <summary>
        /// <para>A variable controlling whether DirectInput should be used for controllers.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable DirectInput detection.</item>
        /// <item><c>"1"</c>: Enable DirectInput detection. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickDirectinput = "SDL_JOYSTICK_DIRECTINPUT";
        
        /// <summary>
        /// <para>A variable that specifies a dialog backend to use.</para>
        /// <para>By default, SDL will try all available dialog backends in a reasonable
        /// order until it finds one that can work, but this hint allows the app or
        /// user to force a specific target.</para>
        /// <para>If the specified target does not exist or is not available, the
        /// dialog-related function calls will fail.</para>
        /// <para>This hint currently only applies to platforms using the generic <c>"Unix"</c>;
        /// dialog implementation, but may be extended to more platforms in the future.
        /// Note that some Unix and Unix-like platforms have their own implementation,
        /// such as macOS and Haiku.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>NULL</c>: Select automatically (default, all platforms)</item>
        /// <item><c>"portal"</c>: Use XDG Portals through DBus (Unix only)</item>
        /// <item><c>"zenity"</c>: Use the Zenity program (Unix only)</item>
        /// </list>
        /// <para>More options may be added in the future.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string FileDialogDriver = "SDL_FILE_DIALOG_DRIVER";
        
        /// <summary>
        /// <para>Override for <see cref="GetDisplayUsableBounds"/>.</para>
        /// <para>If set, this hint will override the expected results for
        /// <see cref="GetDisplayUsableBounds"/> for display index 0. Generally you don't want
        /// to do this, but this allows an embedded system to request that some of the
        /// screen be reserved for other uses when paired with a well-behaved
        /// application.</para>
        /// <para>The contents of this hint must be 4 comma-separated integers, the first is
        /// the bounds x, then y, width and height, in that order.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string DisplayUsableBounds = "SDL_DISPLAY_USABLE_BOUNDS";

        /// <summary>
        /// <para>Set the level of checking for invalid parameters passed to SDL functions.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"1"</c>: Enable fast parameter error checking, e.g. quick <c>null</c> checks, etc.
        /// (default)</item>
        /// <item><c>"2"</c>: Enable full parameter error checking, e.g. validating objects are
        /// the correct type, etc.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string InvalidParamChecks = "SDL_INVALID_PARAM_CHECKS";
        
        /// <summary>
        /// <para>Disable giving back control to the browser automatically when running with
        /// asyncify.</para>
        /// <para>With -s ASYNCIFY, SDL calls emscripten_sleep during operations such as
        /// refreshing the screen or polling events.</para>
        /// <para>This hint only applies to the emscripten platform.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable emscripten_sleep calls (if you give back browser control
        /// manually or use asyncify for other purposes).</item>
        /// <item><c>"1"</c>: Enable emscripten_sleep calls. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EmscriptenAsyncify = "SDL_EMSCRIPTEN_ASYNCIFY";
        
        /// <summary>
        /// <para>Specify the CSS selector used for the <c>"default"</c> window/canvas.</para>
        /// <para>This hint only applies to the emscripten platform.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EmscriptenCanvasSelector = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
        
        /// <summary>
        /// <para>Override the binding element for keyboard inputs for Emscripten builds.</para>
        /// <para>This hint only applies to the emscripten platform.</para>
        /// <para>The variable can be one of:</para>
        /// <list type="bullet">
        /// <item><c>"#window"</c>: the javascript window object</item>
        /// <item><c>"#document"</c>: the javascript document object</item>
        /// <item><c>"#screen"</c>: the javascript window.screen object</item>
        /// <item><c>"#canvas"</c>: the WebGL canvas element</item>
        /// <item><c>"none"</c>: Don't bind anything at all</item>
        /// <item>any other string without a leading <c>#</c> sign applies to the element on the
        /// page with that ID.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";


        /// <summary>
        /// Dictate that newly-created windows will fill the whole browser window.
        /// <para>The canvas element fills the entire document. Resize events will be
        /// generated as the browser window is resized, as that will adjust the canvas
        /// size as well. The canvas will cover anything else on the page, including
        /// any controls provided by Emscripten in its generated HTML file. Often times
        /// this is desirable for a browser-based game, but it means several things
        /// that we expect of an SDL window on other platforms might not work as
        /// expected, such as minimum window sizes and aspect ratios.</para>
        /// <para>This hint overrides <see cref="Props.WindowCreateEMScriptenFillDocumentBoolean"/>
        /// properties when creating an SDL window.</para>
        /// </summary>
        public const string EmscriptenFillDocument = "SDL_EMSCRIPTEN_FILL_DOCUMENT";
        
        /// <summary>
        /// <para>A variable that controls whether the on-screen keyboard should be shown
        /// when text input is active.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"auto"</c>: The on-screen keyboard will be shown if there is no physical
        /// keyboard attached. (default)</item>
        /// <item><c>"0"</c>: Do not show the on-screen keyboard.</item>
        /// <item><c>"1"</c>: Show the on-screen keyboard, if available.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint must be set before <see cref="StartTextInput"/> is called</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EnableScreenKeyboard = "SDL_ENABLE_SCREEN_KEYBOARD";
        
        /// <summary>
        /// <para>A variable containing a list of evdev devices to use if udev is not
        /// available.</para>
        /// <para>The list of devices is in the form:</para>
        /// <para>deviceclass:path[,deviceclass:path[,...]]</para>
        /// <para>where device class is an integer representing the SDL_UDEV_deviceclass and
        /// path is the full path to the event device.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EvdevDevices = "SDL_EVDEV_DEVICES";
        
        /// <summary>
        /// <para>A variable controlling verbosity of the logging of SDL events pushed onto
        /// the internal queue.</para>
        /// <para>The variable can be set to the following values, from least to most
        /// verbose:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Don't log any events. (default)</item>
        /// <item><c>"1"</c>: Log most events (other than the really spammy ones).</item>
        /// <item><c>"2"</c>: Include mouse and finger motion events.</item>
        /// <para>This is generally meant to be used to debug SDL itself, but can be useful
        /// for application developers that need better visibility into what is going
        /// on in the event queue. Logged events are sent through <see cref="Log"/>, which
        /// means by default they appear on stdout on most platforms or <c>maybeOutputDebugString()</c> on Windows, and can be funneled by the app with
        /// <see cref="SetLogOutputFunction"/>, etc.</para>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EventLogging = "SDL_EVENT_LOGGING";
        
        /// <summary>
        /// <para>A variable controlling whether raising the window should be done more
        /// forcefully.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Honor the OS policy for raising windows. (default)</item>
        /// <item><c>"1"</c>: Force the window to be raised, overriding any OS policy.</item>
        /// </list>
        /// <para>At present, this is only an issue under MS Windows, which makes it nearly
        /// impossible to programmatically move a window to the foreground, for
        /// <c>"security"</c> reasons. See http://stackoverflow.com/a/34414846 for a
        /// discussion.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ForceRaiseWindow = "SDL_FORCE_RAISEWINDOW";
        
        /// <summary>
        /// <para>A variable controlling how 3D acceleration is used to accelerate the SDL
        /// screen surface.</para>
        /// <para>SDL can try to accelerate the SDL screen surface by using streaming
        /// textures with a 3D rendering engine. This variable controls whether and how
        /// this is done.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable 3D acceleration</item>
        /// <item><c>"1"</c>: Enable 3D acceleration, using the default renderer. (default)</item>
        /// <item><c>"X"</c>: Enable 3D acceleration, using X where X is one of the valid
        /// rendering drivers. (e.g. <c>"direct3d"</c>, <c>"opengl"</c>, etc.)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="GetWindowSurface"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string FramebufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
        
        /// <summary>
        /// <para>A variable that lets you manually hint extra gamecontroller db entries.</para>
        /// <para>The variable should be newline delimited rows of gamecontroller config
        /// data, see SDL_gamepad.h</para>
        /// <para>You can update mappings after SDL is initialized with
        /// <see cref="GetGamepadMappingForGUID"/> and <see cref="AddGamepadMapping"/></para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerConfig = "SDL_GAMECONTROLLERCONFIG";
        
        /// <summary>
        /// <para>A variable that lets you provide a file with extra gamecontroller db
        /// entries.</para>
        /// <para>The file should contain lines of gamecontroller config data, see
        /// SDL_gamepad.h</para>
        /// <para>You can update mappings after SDL is initialized with
        /// <see cref="GetGamepadMappingForGUID"/> and <see cref="AddGamepadMapping"/></para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerConfigFile = "SDL_GAMECONTROLLERCONFIG_FILE";
        
        /// <summary>
        /// <para>A variable that overrides the automatic controller type detection.</para>
        /// <para>The variable should be comma separated entries, in the form: <c>VID/PID=type</c></para>
        /// <para>The VID and PID should be hexadecimal with exactly 4 digits, e.g. <c>0x00fd</c></para>
        /// <para>This hint affects what low level protocol is used with the HIDAPI driver.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"Xbox360";</c></item>
        /// <item><c>"XboxOne";</c></item>
        /// <item><c>"PS3";</c></item>
        /// <item><c>"PS4";</c></item>
        /// <item><c>"PS5";</c></item>
        /// <item><c>"SwitchPro";</c></item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerType = "SDL_GAMECONTROLLERTYPE";
        
        /// <summary>
        /// <para>A variable containing a list of devices to skip when scanning for game
        /// controllers.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerIgnoreDevices = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
        
        /// <summary>
        /// <para>If set, all devices will be skipped when scanning for game controllers
        /// except for the ones listed in this variable.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerIgnoreDevicesExcept = "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";
        
        /// <summary>
        /// <para>A variable that controls whether the device's built-in accelerometer and
        /// gyro should be used as sensors for gamepads.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Sensor fusion is disabled</item>
        /// <item><c>"1"</c>: Sensor fusion is enabled for all controllers that lack sensors</item>
        /// </list>
        /// <para>Or the variable can be a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint should be set before a gamepad is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GameControllerSensorFusion = "SDL_GAMECONTROLLER_SENSOR_FUSION";
        
        /// <summary>
        /// <para>This variable sets the default text of the TextInput window on GDK
        /// platforms.</para>
        /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="StartTextInput"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GDKTextInputDefaultText = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
        
        /// <summary>
        /// <para>This variable sets the description of the TextInput window on GDK
        /// platforms.</para>
        /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="StartTextInput"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GDKTextInputDescription = "SDL_GDK_TEXTINPUT_DESCRIPTION";
        
        /// <summary>
        /// <para>This variable sets the maximum input length of the TextInput window on GDK
        /// platforms.</para>
        /// <para>The value must be a stringified integer, for example <c>"10"</c> to allow for up
        /// to 10 characters of text input.</para>
        /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="StartTextInput"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GDKTextInputMaxLength = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
        
        /// <summary>
        /// <para>This variable sets the input scope of the TextInput window on GDK
        /// platforms.</para>
        /// <para>Set this hint to change the XGameUiTextEntryInputScope value that will be
        /// passed to the window creation function. The value must be a stringified
        /// integer, for example <c>"0"</c> for XGameUiTextEntryInputScope::Default.</para>
        /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="StartTextInput"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GDKTextInputScope = "SDL_GDK_TEXTINPUT_SCOPE";
        
        /// <summary>
        /// <para>This variable sets the title of the TextInput window on GDK platforms.</para>
        /// <para>This hint is available only if SDL_GDK_TEXTINPUT defined.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="StartTextInput"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GDKTextInputTitle = "SDL_GDK_TEXTINPUT_TITLE";
        
        /// <summary>
        /// <para>A variable to control whether HIDAPI uses libusb for device access.</para>
        /// <para>By default libusb will only be used for a few devices that require direct
        /// USB access, and this can be controlled with
        /// HIDAPI_LIBUSB_WHITELIST.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI will not use libusb for device access.</item>
        /// <item><c>"1"</c>: HIDAPI will use libusb for device access if available. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string HIDAPILibusb = "SDL_HIDAPI_LIBUSB";
        
        /// <summary>
        /// <para>A variable to control whether HIDAPI uses libusb only for whitelisted
        /// devices.</para>
        /// <para>By default libusb will only be used for a few devices that require direct
        /// USB access.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI will use libusb for all device access.</item>
        /// <item><c>"1"</c>: HIDAPI will use libusb only for whitelisted devices. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string HIDAPILibusbWhitelist = "SDL_HIDAPI_LIBUSB_WHITELIST";
        
        /// <summary>
        /// <para>A variable to control whether HIDAPI uses udev for device detection.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI will poll for device changes.</item>
        /// <item><c>"1"</c>: HIDAPI will use udev for device detection. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string HIDAPIUdev = "SDL_HIDAPI_UDEV";
        
        /// <summary>
        /// <para>A variable that specifies a GPU backend to use.</para>
        /// <para>By default, SDL will try all available GPU backends in a reasonable order
        /// until it finds one that can work, but this hint allows the app or user to
        /// force a specific target, such as <c>direct3d12</c> if, say, your hardware
        /// supports Vulkan but you want to try using D3D12 instead.</para>
        /// </summary>
        /// <remarks>This hint should be set before any GPU functions are called.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string GPUDriver = "SDL_GPU_DRIVER";
        
        /// <summary>
        /// <para>A variable to control whether SDL_hid_enumerate() enumerates all HID
        /// devices or only controllers.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL_hid_enumerate() will enumerate all HID devices.</item>
        /// <item><c>"1"</c>: SDL_hid_enumerate() will only enumerate controllers. (default)</item>
        /// </list>
        /// <para>By default SDL will only enumerate controllers, to reduce risk of hanging
        /// or crashing on devices with bad drivers and avoiding macOS keyboard capture
        /// permission prompts.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string HIDAPIEnumerateOnlyControllers = "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";
        
        /// <summary>
        /// <para>A variable containing a list of devices to ignore in SDL_hid_enumerate().</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>For example, to ignore the Shanwan DS3 controller and any Valve controller,
        /// you might use the string <c>0x2563/0x0523,0x28de/0x0000</c>;</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string HIDAPIIgnoreDevices = "SDL_HIDAPI_IGNORE_DEVICES";
        
        /// <summary>
        /// <para>A variable describing what IME UI elements the application can display.</para>
        /// <para>By default IME UI is handled using native components by the OS where
        /// possible, however this can interfere with or not be visible when exclusive
        /// fullscreen mode is used.</para>
        /// <para>The variable can be set to a comma separated list containing the following
        /// items:</para>
        /// <list type="bullet">
        /// <item><c>"none"</c> or "0": The application can't render any IME elements, and native
        /// UI should be used. (default)</item>
        /// <item><c>"composition"</c>: The application handles <see cref="EventType.TextEditing"/> events and
        /// can render the composition text.</item>
        /// <item><c>"candidates"</c>: The application handles <see cref="EventType.TextEditingCandidates"/>
        /// and can render the candidate list.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string IMEImplementedUI = "SDL_IME_IMPLEMENTED_UI";
        
        /// <summary>
        /// <para>A variable controlling whether the home indicator bar on iPhone X should be
        /// hidden.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The indicator bar is not hidden. (default for windowed applications)</item>
        /// <item><c>"1"</c>: The indicator bar is hidden and is shown when the screen is touched
        /// (useful for movie playback applications).</item>
        /// <item><c>"2"</c>: The indicator bar is dim and the first swipe makes it visible and
        /// the second swipe performs the "home" action. (default for fullscreen
        /// applications)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string IOSHideHomeIndicator = "SDL_IOS_HIDE_HOME_INDICATOR";
        
        /// <summary>
        /// <para>A variable that lets you enable joystick (and gamecontroller) events even
        /// when your app is in the background.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item>"0": Disable joystick &amp; gamecontroller input events when the application
        /// is in the background. (default)</item>
        /// <item>"1": Enable joystick &amp; gamecontroller input events when the application
        /// is in the background.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
        
        /// <summary>
        /// <para>A variable containing a list of arcade stick style controllers.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickArcadestickDevices = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices that are not arcade stick style
        /// controllers.</para>
        /// <para>This will override <see cref="JoystickArcadestickDevices"/> and the built in
        /// device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickArcadestickDevicesExcluded = "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable containing a list of devices that should not be considered
        /// joysticks.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickBlacklistDevices = "SDL_JOYSTICK_BLACKLIST_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices that should be considered
        /// joysticks.</para>
        /// <para>This will override <see cref="JoystickBlacklistDevices"/> and the built in
        /// device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickBlacklistDevicesExcluded = "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable containing a comma separated list of devices to open as
        /// joysticks.</para>
        /// </summary>
        /// <remarks>This variable is currently only used by the Linux joystick driver.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickDevice = "SDL_JOYSTICK_DEVICE";
        
        
        /// <summary>
        /// <para>A variable controlling whether enhanced reports should be used for
        /// controllers when using the HIDAPI driver.</para>
        /// <para>Enhanced reports allow rumble and effects on Bluetooth PlayStation
        /// controllers and gyro on Nintendo Switch controllers, but break Windows
        /// DirectInput for other applications that don't use SDL.</para>
        /// <para>Once enhanced reports are enabled, they can't be disabled on PlayStation
        /// controllers without power cycling the controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: enhanced reports are not enabled.</item>
        /// <item><c>"1"</c>: enhanced reports are enabled. (default)</item>
        /// <item><c>"auto"</c>: enhanced features are advertised to the application, but SDL
        /// doesn't change the controller report mode unless the application uses
        /// them.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be enabled anytime.</remarks>
        /// <since>This hint is available since SDL 3.1.8.</since>
        public const string JoystickEnhancedReports = "SDL_JOYSTICK_ENHANCED_REPORTS";
        
        
        /// <summary>
        /// <para>A variable containing a list of flightstick style controllers.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>@file</c>, in which case the named file
        /// will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickFlightstickDevices = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices that are not flightstick style
        /// controllers.</para>
        /// <para>This will override <see cref="JoystickFlightstickDevices"/> and the built in
        /// device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickFlightstickDevicesExcluded = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable controlling whether GameInput should be used for controller
        /// handling on Windows.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: GameInput is not used.</item>
        /// <item><c>"1"</c>: GameInput is used.</item>
        /// </list>
        /// <para>The default is <c>"1"</c> on GDK platforms, and <c>"0"</c> otherwise.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickGameInput = "SDL_JOYSTICK_GAMEINPUT";
        
        /// <summary>
        /// <para>A variable containing a list of devices known to have a GameCube form
        /// factor.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickGameCubeDevices = "SDL_JOYSTICK_GAMECUBE_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices known not to have a GameCube form
        /// factor.</para>
        /// <para>This will override <see cref="JoystickGameCubeDevices"/> and the built in
        /// device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickGameCubeDevicesExcluded = "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI joystick drivers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item>"0": HIDAPI drivers are not used.</item>
        /// <item>"1": HIDAPI drivers are used. (default)</item>
        /// </list>
        /// <para>This variable is the default for all drivers, but can be overridden by the
        /// hints for specific drivers below.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPI = "SDL_JOYSTICK_HIDAPI";
        
        /// <summary>
        /// <para>A variable controlling whether Nintendo Switch Joy-Con controllers will be
        /// combined into a single Pro-like controller when using the HIDAPI driver.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Left and right Joy-Con controllers will not be combined and eachwill be a mini-gamepad.</item>
        /// <item><c>"1"</c>: Left and right Joy-Con controllers will be combined into a singlecontroller. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPICombineJoyCons = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Nintendo GameCube
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/></para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIGameCube = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
        
        /// <summary>
        /// <para>A variable controlling whether rumble is used to implement the GameCube
        /// controller's 3 rumble modes, Stop(0), Rumble(1), and StopHard(2).</para>
        /// <para>This is useful for applications that need full compatibility for things
        /// like ADSR envelopes. - Stop is implemented by setting low_frequency_rumble
        /// to 0 and high_frequency_rumble >0 - Rumble is both at any arbitrary value -
        /// StopHard is implemented by setting both low_frequency_rumble and
        /// high_frequency_rumble to 0</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Normal rumble behavior is behavior is used. (default)</item>
        /// <item><c>"1"</c>: Proper GameCube controller rumble behavior is used.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIGameCubeRumbleBrake = "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Nintendo Switch
        /// Joy-Cons should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIJoyCons = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
        
        /// <summary>
        /// <para>A variable controlling whether the Home button LED should be turned on when
        /// a Nintendo Switch Joy-Con controller is opened.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: home button LED is turned off</item>
        /// <item><c>"1"</c>: home button LED is turned on</item>
        /// </list>
        /// <para>By default the Home button LED state is not changed. This hint can also be
        /// set to a floating point value between 0.0 and 1.0 which controls the
        /// brightness of the Home button LED.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIJoyconHomeLED = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Amazon Luna
        /// controllers connected via Bluetooth should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPILuna = "SDL_JOYSTICK_HIDAPI_LUNA";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Nintendo Online
        /// classic controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item>0: HIDAPI driver is not used.</item>
        /// <item>1: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPINintendoClassic = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for PS3 controllers should
        /// be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/> on macOS, and <c>"0"</c> on
        /// other platforms.</para>
        /// <para>For official Sony driver (sixaxis.sys) use
        /// <see cref="JoystickHIDAPIPS3SixAxisDriver"/>. See
        /// https://github.com/ViGEm/DsHidMini for an alternative driver on Windows.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS3 = "SDL_JOYSTICK_HIDAPI_PS3";
        
        /// <summary>
        /// <para>A variable controlling whether the Sony driver (sixaxis.sys) for PS3
        /// controllers (Sixaxis/DualShock 3) should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Sony driver (sixaxis.sys) is not used.</item>
        /// <item><c>"1"</c>: Sony driver (sixaxis.sys) is used.</item>
        /// </list>
        /// <para>The default value is 0.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS3SixAxisDriver = "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for PS4 controllers should
        /// be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS4 = "SDL_JOYSTICK_HIDAPI_PS4";
        
        /// <summary>
        /// <para>A variable controlling the update rate of the PS4 controller over Bluetooth
        /// when using the HIDAPI driver.</para>
        /// <para>This defaults to 4 ms, to match the behavior over USB, and to be more
        /// friendly to other Bluetooth devices and older Bluetooth hardware on the
        /// computer. It can be set to <c>"1"</c> (1000Hz), <c>"2"</c> (500Hz) and <c>"4"</c> (250Hz)</para>
        /// </summary>
        /// <remarks>This hint can be set anytime, but only takes effect when extended input
        /// reports are enabled.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS4ReportInterval = "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";
        
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for PS5 controllers should
        /// be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS5 = "SDL_JOYSTICK_HIDAPI_PS5";
        
        /// <summary>
        /// <para>A variable controlling whether the player LEDs should be lit to indicate
        /// which player is associated with a PS5 controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: player LEDs are not enabled.</item>
        /// <item><c>"1"</c>: player LEDs are enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIPS5PlayerLED = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
        
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for NVIDIA SHIELD
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIShield = "SDL_JOYSTICK_HIDAPI_SHIELD";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Google Stadia
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIStadia = "SDL_JOYSTICK_HIDAPI_STADIA";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Bluetooth Steam
        /// Controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used. (default)</item>
        /// <item><c>"1"</c>: HIDAPI driver is used for Steam Controllers, which requires
        /// Bluetooth access and may prompt the user for permission on iOS and
        /// Android.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISteam = "SDL_JOYSTICK_HIDAPI_STEAM";
        
        /// <summary>
        /// <para>A variable controlling whether the Steam button LED should be turned on
        /// when a Steam controller is opened.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Steam button LED is turned off.</item>
        /// <item><c>"1"</c>: Steam button LED is turned on.</item>
        /// </list>
        /// <para>By default the Steam button LED state is not changed. This hint can also be
        /// set to a floating point value between 0.0 and 1.0 which controls the
        /// brightness of the Steam button LED.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.1.8.</since>
        public const string JoystickHIDAPISteamHomeLED = "SDL_JOYSTICK_HIDAPI_STEAM_HOME_LED";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for the Steam Deck builtin
        /// controller should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISteamdeck = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for HORI licensed Steam
        /// controllers should be used.</para>
        /// <para>This variable can be set to the following values: <c>"0"</c> - HIDAPI driver is
        /// not used <c>"1"</c> - HIDAPI driver is used</para>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/></para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISteamHORI = "SDL_JOYSTICK_HIDAPI_STEAM_HORI";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for some Logitech wheels
        /// should be used.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used</item>
        /// <item><c>"1"</c>: HIDAPI driver is used</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/></para>
        /// </summary>
        public const string JoystickHIDAPILG4FF = "SDL_JOYSTICK_HIDAPI_LG4FF";
        
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for 8BitDo controllers
        /// should be used.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c> - HIDAPI driver is not used.</item>
        /// <item><c>"1"</c> - HIDAPI driver is used.</item>
        /// </list>
        /// </summary>
        public const string JoystickHIDAPI8BITDO = "SDL_JOYSTICK_HIDAPI_8BITDO";


        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for SInput controllers
        /// should be used.</para>
        /// <para>More info - https://github.com/HandHeldLegend/SInput-HID</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c> - HIDAPI driver is not used.</item>
        /// <item><c>"1"</c> - HIDAPI driver is used.</item>
        /// </list>
        /// </summary>
        /// <remarks>The default is the value of <see cref="JoystickHIDAPI"/></remarks>
        public const string JoystickHIDAPISInput = "SDL_JOYSTICK_HIDAPI_SINPUT";

        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for ZUIKI controllers
        /// should be used.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c> - HIDAPI driver is not used.</item>
        /// <item><c>"1"</c> - HIDAPI driver is used.</item>
        /// </list>
        /// </summary>
        /// <remarks>The default is the value of <see cref="JoystickHIDAPI"/></remarks>
        public const string JoystickHIDAPIZUIKI = "SDL_JOYSTICK_HIDAPI_ZUIKI";
        
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Flydigi controller
        /// should be used.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><b>"0"</b> - HIDAPI driver is not used.</item>
        /// <item><b>"1"</b> - HIDAPI driver is used.</item>
        /// </list>
        /// </summary>
        public const string JoystickHIDAPIFLYDIGI  = "SDL_JOYSTICK_HIDAPI_FLYDIGI";
        
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Nintendo Switch
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISwitch = "SDL_JOYSTICK_HIDAPI_SWITCH";
        
        /// <summary>
        /// <para>A variable controlling whether the Home button LED should be turned on when
        /// a Nintendo Switch Pro controller is opened.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Home button LED is turned off.</item>
        /// <item><c>"1"</c>: Home button LED is turned on.</item>
        /// </list>
        /// <para>By default the Home button LED state is not changed. This hint can also be
        /// set to a floating point value between 0.0 and 1.0 which controls the
        /// brightness of the Home button LED.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISwitchHomeLED = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
        
        /// <summary>
        /// <para>A variable controlling whether the player LEDs should be lit to indicate
        /// which player is associated with a Nintendo Switch controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Player LEDs are not enabled.</item>
        /// <item><c>"1"</c>: Player LEDs are enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPISwitchPlayerLED = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";

        /// <summary>
        /// <para> A variable controlling whether the HIDAPI driver for Nintendo Switch 2
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPI"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string JoystickHIDAPISwitch2 = "SDL_JOYSTICK_HIDAPI_SWITCH2";
        
        /// <summary>
        /// <para>A variable controlling whether Nintendo Switch Joy-Con controllers will be
        /// in vertical mode when using the HIDAPI driver.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Left and right Joy-Con controllers will not be in vertical mode.(default)</item>
        /// <item><c>"1"</c>: Left and right Joy-Con controllers will be in vertical mode.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before opening a Joy-Con controller.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIVerticalJoyCons = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for Nintendo Wii and Wii U
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>This driver doesn't work with the dolphinbar, so the default is false for
        /// now.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIWii = "SDL_JOYSTICK_HIDAPI_WII";
        
        /// <summary>
        /// <para>A variable controlling whether the player LEDs should be lit to indicate
        /// which player is associated with a Wii controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Player LEDs are not enabled.</item>
        /// <item><c>"1"</c>: Player LEDs are enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIWiiPlayerLED = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for XBox controllers
        /// should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is <c>"0"</c> on Windows, otherwise the value of
        /// <see cref="JoystickHIDAPI"/></para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXbox = "SDL_JOYSTICK_HIDAPI_XBOX";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for XBox 360 controllers
        /// should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPIXbox"/></para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXbox360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
        
        /// <summary>
        /// <para>A variable controlling whether the player LEDs should be lit to indicate
        /// which player is associated with an Xbox 360 controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Player LEDs are not enabled.</item>
        /// <item><c>"1"</c>: Player LEDs are enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXbox360PlayerLED = "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for XBox 360 wireless
        /// controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPIXbox360"/></para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXbox360Wireless = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
        
        /// <summary>
        /// <para>A variable controlling whether the HIDAPI driver for XBox One controllers
        /// should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// </list>
        /// <para>The default is the value of <see cref="JoystickHIDAPIXbox"/>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXboxOne = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
        
        /// <summary>
        /// <para>A variable controlling whether the Home button LED should be turned on when
        /// an Xbox One controller is opened.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Home button LED is turned off.</item>
        /// <item><c>"1"</c>: Home button LED is turned on.</item>
        /// </list>
        /// <para>By default the Home button LED state is not changed. This hint can also be
        /// set to a floating point value between 0.0 and 1.0 which controls the
        /// brightness of the Home button LED. The default brightness is 0.4.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickHIDAPIXboxOneHomeLED = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";


        /// <summary>
        /// <para> A variable controlling whether the new HIDAPI driver for wired Xbox One
        /// (GIP) controllers should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: HIDAPI driver is not used.</item>
        /// <item><c>"1"</c>: HIDAPI driver is used.</item>
        /// <para>The default is the value of <see cref="JoystickHIDAPIXboxOne"/>.</para>
        /// <para>This hint should be set before initializing joysticks and gamepads.</para>
        /// </list>
        /// </summary>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string JoystickHIDAPIGIP = "SDL_JOYSTICK_HIDAPI_GIP";
        
        
        /// <summary>
        /// <para>A variable controlling whether the new HIDAPI driver for wired Xbox One
        /// (GIP) controllers should reset the controller if it can't get the metadata
        /// from the controller.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Assume this is a generic controller.</item>
        /// <item><c>"1"</c>: Reset the controller to get metadata.</item>
        /// </list>
        /// <para>By default the controller is not reset.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing joysticks and gamepads.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string JoystickHIDAPIGIPResetForMetadata = "SDL_JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA";
        
        /// <summary>
        /// <para>A variable controlling whether IOKit should be used for controller
        /// handling.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: IOKit is not used.</item>
        /// <item><c>"1"</c>: IOKit is used. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickIOKit = "SDL_JOYSTICK_IOKIT";
        
        /// <summary>
        /// <para>A variable controlling whether to use the classic /dev/input/js* joystick
        /// interface or the newer /dev/input/event* joystick interface on Linux.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use /dev/input/event* (default)</item>
        /// <item><c>"1"</c>: Use /dev/input/js*</item>
        /// </list>
        /// <para></para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickLinuxClassic = "SDL_JOYSTICK_LINUX_CLASSIC";
        
        /// <summary>
        /// <para>A variable controlling whether joysticks on Linux adhere to their
        /// HID-defined deadzones or return unfiltered values.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Return unfiltered joystick axis values. (default)</item>
        /// <item><c>"1"</c>: Return axis values with deadzones taken into account.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before a controller is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickLinuxDeadzones = "SDL_JOYSTICK_LINUX_DEADZONES";
        
        /// <summary>
        /// <para>A variable controlling whether joysticks on Linux will always treat <c>'hat'</c>
        /// axis inputs (ABS_HAT0X - ABS_HAT3Y) as 8-way digital hats without checking
        /// whether they may be analog.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Only map hat axis inputs to digital hat outputs if the input axesappear to actually be digital. (default)</item>
        /// <item><c>"1"</c>: Always handle the input axes numbered ABS_HAT0X to ABS_HAT3Y asdigital hats.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before a controller is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickLinuxDigitalHats = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
        
        /// <summary>
        /// <para>A variable controlling whether digital hats on Linux will apply deadzones
        /// to their underlying input axes or use unfiltered values.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Return digital hat values based on unfiltered input axis values.</item>
        /// <item><c>"1"</c>: Return digital hat values with deadzones on the input axes takeninto account. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before a controller is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickLinuxHatDeadzones = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
        
        /// <summary>
        /// <para>A variable controlling whether GCController should be used for controller
        /// handling.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: GCController is not used.</item>
        /// <item><c>"1"</c>: GCController is used. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickMFI = "SDL_JOYSTICK_MFI";
        
        /// <summary>
        /// <para>A variable controlling whether the RAWINPUT joystick drivers should be used
        /// for better handling XInput-capable devices.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: RAWINPUT drivers are not used. (default)</item>
        /// <item><c>"1"</c>: RAWINPUT drivers are used.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickRawinput = "SDL_JOYSTICK_RAWINPUT";
        
        /// <summary>
        /// <para>A variable controlling whether the RAWINPUT driver should pull correlated
        /// data from XInput.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: RAWINPUT driver will only use data from raw input APIs.</item>
        /// <item><c>"1"</c>: RAWINPUT driver will also pull data from XInput and
        /// Windows.Gaming.Input, providing better trigger axes, guide button
        /// presses, and rumble support for Xbox controllers. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before a gamepad is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickRawinputCorrelateXInput = "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";
        
        /// <summary>
        /// <para>A variable controlling whether the ROG Chakram mice should show up as
        /// joysticks.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: ROG Chakram mice do not show up as joysticks. (default)</item>
        /// <item><c>"1"</c>: ROG Chakram mice show up as joysticks.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickROGChakram = "SDL_JOYSTICK_ROG_CHAKRAM";
        
        /// <summary>
        /// <para>A variable controlling whether a separate thread should be used for
        /// handling joystick detection and raw input messages on Windows.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: A separate thread is not used.</item>
        /// <item><c>"1"</c>: A separate thread is used for handling raw input messages. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickThread = "SDL_JOYSTICK_THREAD";
        
        /// <summary>
        /// <para>A variable containing a list of throttle style controllers.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickThrottleDevices = "SDL_JOYSTICK_THROTTLE_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices that are not throttle style
        /// controllers.</para>
        /// <para>This will override <see cref="JoystickThrottleDevices"/> and the built in
        /// device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickThrottleDevicesExcluded = "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable controlling whether Windows.Gaming.Input should be used for
        /// controller handling.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: WGI is not used.(default)</item>
        /// <item><c>"1"</c>: WGI is used.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickWGI = "SDL_JOYSTICK_WGI";
        
        /// <summary>
        /// <para>A variable containing a list of wheel style controllers.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickWheelDevices = "SDL_JOYSTICK_WHEEL_DEVICES";
        
        /// <summary>
        /// <para>A variable containing a list of devices that are not wheel style
        /// controllers.</para>
        /// <para>This will override <see cref="JoystickWheelDevices"/> and the built in device
        /// list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickWheelDevicesExcluded = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
        
        /// <summary>
        /// <para>A variable containing a list of devices known to have all axes centered at
        /// zero.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>`0xAAAA/0xBBBB,0xCCCC/0xDDDD`</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint should be set before a controller is opened.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string JoystickZeroCenteredDevices = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
        
        //#define SDL_HINT_JOYSTICK_HAPTIC_AXES "SDL_JOYSTICK_HAPTIC_AXES"
        /// <summary>
        /// <para>A variable containing a list of devices and their desired number of haptic
        /// (force feedback) enabled axis.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form plus the number of desired axes, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB/1,0xCCCC/0xDDDD/3</c></para>
        /// <para>This hint supports a "wildcard" device that will set the number of haptic
        /// axes on all initialized haptic devices which were not defined explicitly in
        /// this hint.</para>
        /// <para><c>0xFFFF/0xFFFF/1</c></para>
        /// <para>This hint should be set before a controller is opened. The number of haptic
        /// axes won't exceed the number of real axes found on the device.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.5.</since>
        public const string JoystickHapticAxes = "SDL_JOYSTICK_HAPTIC_AXES";
        
        
        /// <summary>
        /// <para>A variable that controls keycode representation in keyboard events.</para>
        /// <para>This variable is a comma separated set of options for translating keycodes
        /// in events:</para>
        /// <list type="bullet">
        /// <item><c>"none"</c>: Keycode options are cleared, this overrides other options.</item>
        /// <item><c>"hide_numpad"</c>: The numpad keysyms will be translated into their
        /// non-numpad versions based on the current NumLock state. For example,
        /// <see cref="Keycode.Kp4"/> would become <see cref="Keycode.Alpha4"/> if <see cref="Keymod.Num"/> is set in the event
        /// modifiers, and <see cref="Keycode.Left"/> if it is unset.</item>
        /// <item><c>"french_numbers"</c>: The number row on French keyboards is inverted, so
        /// pressing the 1 key would yield the keycode <see cref="Keycode.Alpha1"/>, or <c>'1'</c>, instead of
        /// <see cref="Keycode.Ampersand"/>, or <c>'&amp;'</c></item>
        /// <item><c>"latin_letters"</c>: For keyboards using non-Latin letters, such as Russian
        /// or Thai, the letter keys generate keycodes as though it had an en_US
        /// layout. e.g. pressing the key associated with <see cref="Scancode.A"/> on a Russian
        /// keyboard would yield <c>'a'</c> instead of a Cyrillic letter.</item>
        /// </list>
        /// <para>The default value for this hint is <c>"french_numbers,latin_letters"</c>;</para>
        /// <para>Some platforms like Emscripten only provide modified keycodes and the
        /// options are not used.</para>
        /// <para>These options do not affect the return value of <see cref="GetKeyFromScancode"/> or
        /// <see cref="GetScancodeFromKey"/>, they just apply to the keycode included in key
        /// events.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string KeycodeOptions = "SDL_KEYCODE_OPTIONS";
        
        /// <summary>
        /// <para>A variable that controls what KMSDRM device to use.</para>
        /// <para>SDL might open something like <c>"/dev/dri/cardNN"</c> to access KMSDRM
        /// functionality, where <c>"NN"</c> is a device index number. SDL makes a guess at
        /// the best index to use (usually zero), but the app or user can set this hint
        /// to a number between 0 and 99 to force selection.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string KMSDRMDeviceIndex = "SDL_KMSDRM_DEVICE_INDEX";
        
        /// <summary>
        /// <para>A variable that controls whether SDL requires DRM master access in order to
        /// initialize the KMSDRM video backend.</para>
        /// <para>The DRM subsystem has a concept of a <c>"DRM master"</c> which is a DRM client
        /// that has the ability to set planes, set cursor, etc. When SDL is DRM
        /// master, it can draw to the screen using the SDL rendering APIs. Without DRM
        /// master, SDL is still able to process input and query attributes of attached
        /// displays, but it cannot change display state or draw to the screen
        /// directly.</para>
        /// <para>In some cases, it can be useful to have the KMSDRM backend even if it
        /// cannot be used for rendering. An app may want to use SDL for input
        /// processing while using another rendering API (such as an MMAL overlay on
        /// Raspberry Pi) or using its own code to render to DRM overlays that SDL
        /// doesn't support.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL will allow usage of the KMSDRM backend without DRM master.</item>
        /// <item><c>"1"</c>: SDL Will require DRM master to use the KMSDRM backend. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string KMSDRMRequireDRMMaster = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
        
        /// <summary>
        /// <para>A variable controlling the default SDL log levels.</para>
        /// <para>This variable is a comma separated set of category=level tokens that define
        /// the default logging levels for SDL applications.</para>
        /// <para>The category can be a numeric category, one of <c>"app"</c>, <c>"error"</c>, <c>"assert"</c>,
        /// <c>"system"</c>, <c>"audio"</c>, <c>"video"</c>, <c>"render"</c>, <c>"input"</c>, <c>"test"</c>, or <c>`*`</c> for any
        /// unspecified category.</para>
        /// <para>The level can be a numeric level, one of <c>"verbose"</c>, <c>"debug"</c>, <c>"info"</c>,
        /// <c>"warn"</c>, <c>"error"</c>, <c>"critical"</c>, or <c>"quiet"</c> to disable that category.</para>
        /// <para>You can omit the category if you want to set the logging level for all
        /// categories.</para>
        /// <para>If this hint isn't set, the default log levels are equivalent to:</para>
        /// <para><c>`app=info,assert=warn,test=verbose,*=error`</c></para>
        /// <para>If the <c>`DEBUG_INVOCATION`</c> environment variable is set to <c>"1"</c>, the default
        /// log levels are equivalent to:</para>
        /// <para><c>`assert=warn,test=verbose,*=debug`</c></para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string Logging = "SDL_LOGGING";
        
        /// <summary>
        /// <para>A variable controlling whether to force the application to become the
        /// foreground process when launched on macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The application is brought to the foreground when launched.
        /// (default)</item>
        /// <item><c>"1"</c>: The application may remain in the background when launched.</item>
        /// </list>
        /// <para>This hint needs to be set before <see cref="Init"/>.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
        
        /// <summary>
        /// <para>A variable that determines whether Ctrl+Click should generate a right-click
        /// event on macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Ctrl+Click does not generate a right mouse button click event.
        /// (default)</item>
        /// <item><c>"1"</c>: Ctrl+Click generated a right mouse button click event.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MacCtrlClickEmulateRightClick = "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";

        /// <summary>
        /// <para>A variable controlling whether dispatching OpenGL context updates should
        /// block the dispatching thread until the main thread finishes processing on
        /// macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Dispatching OpenGL context updates will block the dispatching thread
        /// until the main thread finishes processing. (default)</item>
        /// <item><c>"1"</c>: Dispatching OpenGL context updates will allow the dispatching thread
        /// to continue execution.</item>
        /// </list>
        /// <para>Generally you want the default, but if you have OpenGL code in a background
        /// thread on a Mac, and the main thread hangs because it's waiting for that
        /// background thread, but that background thread is also hanging because it's
        /// waiting for the main thread to do an update, this might fix your issue.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MacOpenGLAsyncDispatch = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
        
        /// <summary>
        /// <para>AA variable controlling whether the Option key on macOS should be remapped
        /// to act as the Alt key.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"none</c>: The Option key is not remapped to Alt. (default)</item>
        /// <item><c>"only_left"</c>: Only the left Option key is remapped to Alt.</item>
        /// <item><c>"only_right"</c>: Only the right Option key is remapped to Alt.</item>
        /// <item><c>"both"</c>: Both Option keys are remapped to Alt.</item>
        /// </list>
        /// <para>This will prevent the triggering of key compositions that rely on the
        /// Option key, but will still send the Alt modifier for keyboard events. In
        /// the case that both Alt and Option are pressed, the Option key will be
        /// ignored. This is particularly useful for applications like terminal
        /// emulators and graphical user interfaces (GUIs) that rely on Alt key
        /// functionality for shortcuts or navigation. This does not apply to
        /// <see cref="GetKeyFromScancode"/> and only has an effect if IME is enabled.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since 3.2.0</since>
        public const string MacOptionAsAlt = "SDL_MAC_OPTION_AS_ALT";
        
        /// <summary>
        /// <para>A variable controlling whether SDL_EVENT_MOUSE_WHEEL event values will have
        /// momentum on macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The mouse wheel events will have no momentum. (default)</item>
        /// <item><c>"1"</c>: The mouse wheel events will have momentum.</item>
        /// </list>
        /// <para>This hint needs to be set before <see cref="Init"/>.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MacScrollMomentum = "SDL_MAC_SCROLL_MOMENTUM";
        
        /// <summary>
        /// <para>Request <see cref="AppIterate"/> be called at a specific rate.</para>
        /// <para>If this is set to a number, it represents Hz, so "60" means try to iterate
        /// 60 times per second. "0" means to iterate as fast as possible. Negative
        /// values are illegal, but reserved, in case they are useful in a future
        /// revision of SDL.</para>
        /// <para>There are other strings that have special meaning. If set to "waitevent",
        /// <see cref="AppIterate"/> will not be called until new event(s) have arrived (and been
        /// processed by <see cref="AppEvent"/>). This can be useful for apps that are completely
        /// idle except in response to input.</para>
        /// <para>On some platforms, or if you are using SDL_main instead of <see cref="AppIterate"/>,
        /// this hint is ignored. When the hint can be used, it is allowed to be
        /// changed at any time.</para>
        /// <para>This defaults to 0, and specifying <c>null</c> for the hint's value will restore
        /// the default.</para>
        /// <para>This doesn't have to be an integer value. For example, "59.94" won't be
        /// rounded to an integer rate; the digits after the decimal are actually
        /// respected.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MainCallbackRate = "SDL_MAIN_CALLBACK_RATE";
        
        /// <summary>
        /// <para>A variable controlling whether the mouse is captured while mouse buttons
        /// are pressed.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The mouse is not captured while mouse buttons are pressed.</item>
        /// <item><c>"1"</c>: The mouse is captured while mouse buttons are pressed.</item>
        /// </list>
        /// <para>By default the mouse is captured while mouse buttons are pressed so if the
        /// mouse is dragged outside the window, the application continues to receive
        /// mouse events until the button is released.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseAutoCapture = "SDL_MOUSE_AUTO_CAPTURE";
        
        /// <summary>
        /// <para>A variable setting the double click radius, in pixels.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseDoubleClickRadius = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
        
        /// <summary>
        /// <para>A variable setting the double click time, in milliseconds.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseDoubleClickTime = "SDL_MOUSE_DOUBLE_CLICK_TIME";
        
        
        /// <summary>
        /// <para>A variable setting which system cursor to use as the default cursor.</para>
        /// <para>This should be an integer corresponding to the SDL_SystemCursor enum.
        /// The default value is zero (<see cref="SystemCursor.Default"/>).</para>
        /// </summary>
        /// <remarks>This hint needs to be set before <see cref="Init"/>.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseDefaultSystemCursor = "SDL_MOUSE_DEFAULT_SYSTEM_CURSOR";
        
        
        /// <summary>
        /// <para>A variable controlling whether warping a hidden mouse cursor will activate
        /// relative mouse mode.</para>
        /// <para>When this hint is set, the mouse cursor is hidden, and multiple warps to
        /// the window center occur within a short time period, SDL will emulate mouse
        /// warps using relative mouse mode. This can provide smoother and more
        /// reliable mouse motion for some older games, which continuously calculate
        /// the distance traveled by the mouse pointer and warp it back to the center
        /// of the window, rather than using relative mouse motion.</para>
        /// <para>Note that relative mouse mode may have different mouse acceleration
        /// behavior than pointer warps.</para>
        /// <para>If your application needs to repeatedly warp the hidden mouse cursor at a
        /// high-frequency for other purposes, it should disable this hint.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Attempts to warp the mouse will always be made.</item>
        /// <item><c>"1"</c>: Some mouse warps will be emulated by forcing relative mouse mode.
        /// (default)</item>
        /// </list>
        /// <para>If not set, this is automatically enabled unless an application uses
        /// relative mouse mode directly.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseEmulateWarpWithRelative = "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE";
        
        /// <summary>
        /// <para>Allow mouse click events when clicking to focus an SDL window.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Ignore mouse clicks that activate a window. (default)</item>
        /// <item><c>"1"</c>: Generate events for mouse clicks that activate a window.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseFocusClickthrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
        
        /// <summary>
        /// <para>A variable setting the speed scale for mouse motion, in floating point,
        /// when the mouse is not in relative mode.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseNormalSpeedScale = "SDL_MOUSE_NORMAL_SPEED_SCALE";
        
        /// <summary>
        /// <para>A variable controlling whether relative mouse mode constrains the mouse to
        /// the center of the window.</para>
        /// <para>Constraining to the center of the window works better for FPS games and
        /// when the application is running over RDP. Constraining to the whole window
        /// works better for 2D games and increases the chance that the mouse will be
        /// in the correct position when using high DPI mice.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Relative mouse mode constrains the mouse to the window.</item>
        /// <item><c>"1"</c>: Relative mouse mode constrains the mouse to the center of the
        /// window. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseRelativeModeCenter = "SDL_MOUSE_RELATIVE_MODE_CENTER";
        
        /// <summary>
        /// <para>A variable setting the scale for mouse motion, in floating point, when the
        /// mouse is in relative mode.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseRelativeSpeedScale = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
        
        /// <summary>
        /// <para>A variable controlling whether the system mouse acceleration curve is used
        /// for relative mouse motion.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Relative mouse motion will be unscaled. (default)</item>
        /// <item><c>"1"</c>: Relative mouse motion will be scaled using the system mouse
        /// acceleration curve.</item>
        /// </list>
        /// <para>If <see cref="MouseRelativeSpeedScale"/> is set, that will be applied after
        /// system speed scale.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseRelativeSystemScale = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
        
        /// <summary>
        /// <para>A variable controlling whether a motion event should be generated for mouse
        /// warping in relative mode.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Warping the mouse will not generate a motion event in relative mode</item>
        /// <item><c>"1"</c>: Warping the mouse will generate a motion event in relative mode</item>
        /// </list>
        /// <para>By default warping the mouse will not generate motion events in relative
        /// mode. This avoids the application having to filter out large relative
        /// motion due to warping.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseRelativeWarpMotion = "SDL_MOUSE_RELATIVE_WARP_MOTION";
        
        /// <summary>
        /// <para>A variable controlling whether the hardware cursor stays visible when
        /// relative mode is active.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The cursor will be hidden while relative mode is active (default)</item>
        /// <item><c>"1"</c>: The cursor will remain visible while relative mode is active</item>
        /// </list>
        /// <para>Note that for systems without raw hardware inputs, relative mode is
        /// implemented using warping, so the hardware cursor will visibly warp between
        /// frames if this is enabled on those systems.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseRelativeCursorVisible = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
        
        /// <summary>
        /// <para>A variable controlling whether mouse events should generate synthetic touch
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Mouse events will not generate touch events. (default for desktop
        /// platforms)</item>
        /// <item><c>"1"</c>: Mouse events will generate touch events. (default for mobile
        /// platforms, such as Android and iOS)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MouseTouchEvents = "SDL_MOUSE_TOUCH_EVENTS";
        
        /// <summary>
        /// <para>A variable controlling whether the keyboard should be muted on the console.</para>
        /// <para>Normally the keyboard is muted while SDL applications are running so that
        /// keyboard input doesn't show up as key strokes on the console. This hint
        /// allows you to turn that off for debugging purposes.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Allow keystrokes to go through to the console.</item>
        /// <item><c>"1"</c>: Mute keyboard input so it doesn't show up on the console. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string MuteConsoleKeyboard = "SDL_MUTE_CONSOLE_KEYBOARD";
        
        /// <summary>
        /// <para>Tell SDL not to catch the SIGINT or SIGTERM signals on POSIX platforms.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL will install a SIGINT and SIGTERM handler, and when it catches a
        /// signal, convert it into an <see cref="EventType.Quit"/> event. (default)</item>
        /// <item><c>"1"</c>: SDL will not install a signal handler at all.</item>
        /// </list>
        /// <para>This hint should be set before SDL is initialized.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string NoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
        
        /// <summary>
        /// <para>Specify the OpenGL library to load.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating an OpenGL window or creating an
        /// OpenGL context. If this hint isn't set, SDL will choose a reasonable
        /// default.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string OpenGLLibrary = "SDL_OPENGL_LIBRARY";
        
        /// <summary>
        /// <para>Specify the EGL library to load.</para>
        /// <para>This hint should be set before creating an OpenGL window or creating an
        /// OpenGL context. This hint is only considered if SDL is using EGL to
        /// manage OpenGL contexts. If this hint isn't set, SDL will choose a
        /// reasonable default.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string EGLLibrary = "SDL_EGL_LIBRARY";
        
        /// <summary>
        /// <para>A variable controlling what driver to use for OpenGL ES contexts.</para>
        /// <para>On some platforms, currently Windows and X11, OpenGL drivers may support
        /// creating contexts with an OpenGL ES profile. By default SDL uses these
        /// profiles, when available, otherwise it attempts to load an OpenGL ES
        /// library, e.g. that provided by the ANGLE project. This variable controls
        /// whether SDL follows this default behaviour or will always load an OpenGL ES
        /// library.</para>
        /// <para>Circumstances where this is useful include - Testing an app with a
        /// particular OpenGL ES implementation, e.g ANGLE, or emulator, e.g. those
        /// from ARM, Imagination or Qualcomm. - Resolving OpenGL ES function addresses
        /// at link time by linking with the OpenGL ES library instead of querying them
        /// at run time with <see cref="GLGetProcAddress"/>.</para>
        /// <para>Caution: for an application to work with the default behaviour across
        /// different OpenGL drivers it must query the OpenGL ES function addresses at
        /// run time using <see cref="GLGetProcAddress"/>.</para>
        /// <para>This variable is ignored on most platforms because OpenGL ES is native or
        /// not supported.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use ES profile of OpenGL, if available. (default)</item>
        /// <item><c>"1"</c>: Load OpenGL ES library using the default library names.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string OpenGLESDriver = "SDL_OPENGL_ES_DRIVER";
        
        /// <summary>
        /// <para>Mechanism to specify openvr_api library location</para>
        /// <para>By default, when using the OpenVR driver, it will search for the API
        /// library in the current folder. But, if you wish to use a system API you can
        /// specify that by using this hint. This should be the full or relative path
        /// to a .dll on Windows or .so on Linux.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string OpenVRLibrary = "SDL_OPENVR_LIBRARY";
        
        /// <summary>
        /// <para>A variable controlling which orientations are allowed on iOS/Android.</para>
        /// <para>In some circumstances it is necessary to be able to explicitly control
        /// which UI orientations are allowed.</para>
        /// <para>This variable is a space delimited list of the following values:</para>
        /// <list type="bullet">
        /// <item><c>"LandscapeLeft"</c>;</item>
        /// <item><c>"LandscapeRight"</c>;</item>
        /// <item><c>"Portrait"</c>;</item>
        /// <item><c>"PortraitUpsideDown"</c>;</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string Orientations = "SDL_ORIENTATIONS";
        
        /// <summary>
        /// <para>A variable controlling the use of a sentinel event when polling the event
        /// queue.</para>
        /// <para>When polling for events, <see cref="SDL.PumpEvents"/> is used to gather new events from
        /// devices. If a device keeps producing new events between calls to
        /// <see cref="SDL.PumpEvents"/>, a poll loop will become stuck until the new events stop.
        /// This is most noticeable when moving a high frequency mouse.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable poll sentinels.</item>
        /// <item><c>"1"</c>: Enable poll sentinels. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string PollSentinel = "SDL_POLL_SENTINEL";
        
        /// <summary>
        /// <para>Override for <see cref="GetPreferredLocales"/>.</para>
        /// <para>If set, this will be favored over anything the OS might report for the
        /// user's preferred locales. Changing this hint at runtime will not generate a
        /// <see cref="EventType.LocaleChanged"/> event (but if you can change the hint, you can
        /// push your own event, if you want).</para>
        /// <para>The format of this hint is a comma-separated list of language and locale,
        /// combined with an underscore, as is a common format: <c>"en_GB"</c>. Locale is
        /// optional: <c>"en"</c>. So you might have a list like this: <c>"en_GB,jp,es_PT"</c>;</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string PreferredLocales = "SDL_PREFERRED_LOCALES";
        
        /// <summary>
        /// <para>A variable that decides whether to send <see cref="EventType.Quit"/> when closing the
        /// last window.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL will not send an <see cref="EventType.Quit"/> event when the last window is
        /// requesting to close. Note that in this case, there are still other
        /// legitimate reasons one might get an <see cref="EventType.Quit"/> event: choosing <c>"Quit"</c>;
        /// from the macOS menu bar, sending a SIGINT (ctrl-c) on Unix, etc.</item>
        /// <item><c>"1"</c>: SDL will send a quit event when the last window is requesting to
        /// close. (default)</item>
        /// </list>
        /// <para>If there is at least one active system tray icon, <see cref="EventType.Quit"/> will
        /// instead be sent when both the last window will be closed and the last tray
        /// icon will be destroyed.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string QuitOnLastWindowClose = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
        
        /// <summary>
        /// <para>A variable controlling whether the Direct3D device is initialized for
        /// thread-safe operations.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Thread-safety is not enabled. (default)</item>
        /// <item><c>"1"</c>: Thread-safety is enabled.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderDirect3DThreadsafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
        
        /// <summary>
        /// <para>A variable controlling whether to enable Direct3D 11+'s Debug Layer.</para>
        /// <para>This variable does not have any effect on the Direct3D 9 based renderer.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable Debug Layer use. (default)</item>
        /// <item><c>"1"</c>: Enable Debug Layer use.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
        
        /// <summary>
        /// <para>A variable controlling whether to use the Direct3D 11 WARP software 
        /// rasterizer.</para>
        /// <para>For more information, see:
        /// https://learn.microsoft.com/en-us/windows/win32/direct3darticles/directx-warp</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable WARP rasterizer. (default)</item>
        /// <item><c>"1"</c>: Enable WARP rasterizer.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string RenderDirect3D11Warp = "SDL_RENDER_DIRECT3D11_WARP";
        
        /// <summary>
        /// <para>A variable controlling whether to enable Vulkan Validation Layers.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable Validation Layer use</item>
        /// <item><c>"1"</c>: Enable Validation Layer use</item>
        /// </list>
        /// <para>By default, SDL does not use Vulkan Validation Layers.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderVulkanDebug = "SDL_RENDER_VULKAN_DEBUG";
        
        /// <summary>
        /// <para>A variable controlling whether to create the GPU device in debug mode.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable debug mode use (default)</item>
        /// <item><c>"1"</c>: Enable debug mode use</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderGPUDebug = "SDL_RENDER_GPU_DEBUG";
        
        /// <summary>
        /// <para>A variable controlling whether to prefer a low-power GPU on multi-GPU
        /// systems.</para>
        /// <para>This variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Prefer high-performance GPU (default)</item>
        /// <item><c>"1"</c>: Prefer low-power GPU</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderGPULowPower = "SDL_RENDER_GPU_LOW_POWER";
        
        /// <summary>
        /// <para>A variable specifying which render driver to use.</para>
        /// <para>If the application doesn't pick a specific renderer to use, this variable
        /// specifies the name of the preferred renderer. If the preferred renderer
        /// can't be initialized, creating a renderer will fail.</para>
        /// <para>This variable is case insensitive and can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"direct3d"</c>;</item>
        /// <item><c>"direct3d11"</c>;</item>
        /// <item><c>"direct3d12"</c>;</item>
        /// <item><c>"opengl"</c>;</item>
        /// <item><c>"opengles2"</c>;</item>
        /// <item><c>"opengles"</c>;</item>
        /// <item><c>"metal"</c>;</item>
        /// <item><c>"vulkan"</c>;</item>
        /// <item><c>"software"</c>;</item>
        /// </list>
        /// <para>This hint accepts a comma-separated list of driver names, and each will be
        /// tried in the order listed when creating a renderer until one succeeds or
        /// all of them fail.</para>
        /// <para>The default varies by platform, but it's the first one in the list that is
        /// available on the current platform.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderDriver = "SDL_RENDER_DRIVER";
        
        /// <summary>
        /// <para>A variable controlling how the 2D render API renders lines.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use the default line drawing method (Bresenham's line algorithm)</item>
        /// <item><c>"1"</c>: Use the driver point API using Bresenham's line algorithm (correct,
        /// draws many points)</item>
        /// <item><c>"2"</c>: Use the driver line API (occasionally misses line endpoints based on
        /// hardware driver quirks</item>
        /// <item><c>"3"</c>: Use the driver geometry API (correct, draws thicker diagonal lines)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderLineMethod = "SDL_RENDER_LINE_METHOD";
        
        /// <summary>
        /// <para>A variable controlling whether the Metal render driver select low power
        /// device over default one.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use the preferred OS device. (default)</item>
        /// <item><c>"1"</c>: Select a low power device.</item>
        /// </list>
        /// <para>This hint should be set before creating a renderer.</para>
        /// </summary>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderMetalPreferLowPowerDevice = "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";
        
        /// <summary>
        /// <para>A variable controlling whether updates to the SDL screen surface should be
        /// synchronized with the vertical refresh, to avoid tearing.</para>
        /// <para>This hint overrides the application preference when creating a renderer.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable vsync. (default)</item>
        /// <item><c>"1"</c>: Enable vsync.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a renderer.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RenderVsync = "SDL_RENDER_VSYNC";
        
        /// <summary>
        /// <para>A variable to control whether the return key on the soft keyboard should
        /// hide the soft keyboard on Android and iOS.</para>
        /// <para>This hint sets the default value of <see cref="Props.TextInputMultilineBoolean"/></para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The return key will be handled as a key event. (default)</item>
        /// <item><c>"1"</c>: The return key will hide the keyboard.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ReturnKeyHidesIME = "SDL_RETURN_KEY_HIDES_IME";
        
        /// <summary>
        /// <para>A variable containing a list of ROG gamepad capable mice.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        /// <seealso cref="ROGGamepadMiceExcluded"/>
        public const string ROGGamepadMice = "SDL_ROG_GAMEPAD_MICE";
        
        /// <summary>
        /// <para>A variable containing a list of devices that are not ROG gamepad capable
        /// mice.</para>
        /// <para>This will override <see cref="ROGGamepadMice"/> and the built in device list.</para>
        /// <para>The format of the string is a comma separated list of USB VID/PID pairs in
        /// hexadecimal form, e.g.</para>
        /// <para><c>0xAAAA/0xBBBB,0xCCCC/0xDDDD</c></para>
        /// <para>The variable can also take the form of <c>"@file"</c>, in which case the named
        /// file will be loaded and interpreted as the value of the variable.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ROGGamepadMiceExcluded = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
        
        /// <summary>
        /// <para>Variable controlling the width of the PS2's framebuffer in pixels</para>
        /// <para>By default, this variable is <c>"640"</c></para>
        /// </summary>
        public const string PS2GSWidth = "SDL_PS2_GS_WIDTH";
        
        /// <summary>
        /// <para>Variable controlling the height of the PS2's framebuffer in pixels</para>
        /// <para>By default, this variable is "448"</para>
        /// </summary>
        public const string PS2GSHeight = "SDL_PS2_GS_HEIGHT";

        /// <summary>
        /// <para>Variable controlling whether the signal is interlaced or progressive</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Image is interlaced. (default)</item>
        /// <item><c>"1"</c>: Image is progressive</item>
        /// </list>
        /// </summary>
        public const string PS2GSProgressive = "SDL_PS2_GS_PROGRESSIVE";
        
        /// <summary>
        /// <para>Variable controlling the video mode of the console</para>
        /// <list type="bullet">
        /// <item><c>""</c>: Console-native. (default)</item>
        /// <item><c>"NTSC"</c>: 60hz region</item>
        /// <item><c>"PAL"</c>: 50hz region</item>
        /// </list>
        /// </summary>
        public const string PS2GSMode = "SDL_PS2_GS_MODE";
        
        /// <summary>
        /// <para>A variable controlling which Dispmanx layer to use on a Raspberry PI.</para>
        /// <para>Also known as Z-order. The variable can take a negative or positive value.
        /// The default is 10000.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string RPIVideoLayer = "SDL_RPI_VIDEO_LAYER";
        
        /// <summary>
        /// <para>Specify an <c>"activity name"</c> for screensaver inhibition.</para>
        /// <para>Some platforms, notably Linux desktops, list the applications which are
        /// inhibiting the screensaver or other power-saving features.</para>
        /// <para>This hint lets you specify the <c>"activity name"</c> sent to the OS when
        /// <see cref="DisableScreenSaver"/> is used (or the screensaver is automatically
        /// disabled). The contents of this hint are used when the screensaver is
        /// disabled. You should use a string that describes what your program is doing
        /// (and, therefore, why the screensaver is disabled). For example, <c>"Playing a
        /// game"</c> or <c>"Watching a video"</c>.</para>
        /// <para>Setting this toor leaving it unset will have SDL use a reasonable
        /// default: <c>"Playing a game"</c> or something similar.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="DisableScreenSaver"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ScreensaverInhibitActivityName = "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";
        
        /// <summary>
        /// <para>A variable controlling whether SDL calls dbus_shutdown() on quit.</para>
        /// <para>This is useful as a debug tool to validate memory leaks, but shouldn't ever
        /// be set in production applications, as other libraries used by the
        /// application might use dbus under the hood and this can cause crashes if
        /// they continue after <see cref="Quit"/>.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item>0: SDL will not call dbus_shutdown() on quit. (default)</item>
        /// <item>1: SDL will call dbus_shutdown() on quit.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ShutdownDbusOnQuit = "SDL_SHUTDOWN_DBUS_ON_QUIT";
        
        /// <summary>
        /// <para>A variable that specifies a backend to use for title storage.</para>
        /// <para>By default, SDL will try all available storage backends in a reasonable
        /// order until it finds one that can work, but this hint allows the app or
        /// user to force a specific target, such as <c>"pc"</c> if, say, you are on Steam but
        /// want to avoid SteamRemoteStorage for title data.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string StorageTitleDriver = "SDL_STORAGE_TITLE_DRIVER";
        
        /// <summary>
        /// <para>A variable that specifies a backend to use for user storage.</para>
        /// <para>By default, SDL will try all available storage backends in a reasonable
        /// order until it finds one that can work, but this hint allows the app or
        /// user to force a specific target, such as pc if, say, you are on Steam but
        /// want to avoid SteamRemoteStorage for user data.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string StorageUserDriver = "SDL_STORAGE_USER_DRIVER";
        
        /// <summary>
        /// <para>Specifies whether SDL_ThreadPriority should be treated as
        /// realtime.</para>
        /// <para>On some platforms, like Linux, a realtime priority thread may be subject to
        /// restrictions that require special handling by the application. This hint
        /// exists to let SDL know that the app is prepared to handle said
        /// restrictions.</para>
        /// <para>On Linux, SDL will apply the following configuration to any thread that
        /// becomes realtime:</para>
        /// <list type="bullet">
        /// <item>The SCHED_RESET_ON_FORK bit will be set on the scheduling policy,</item>
        /// <item>An RLIMIT_RTTIME budget will be configured to the rtkit specified limit.</item>
        /// <item>Exceeding this limit will result in the kernel sending SIGKILL to the
        /// app, refer to the man pages for more information.</item>
        /// </list>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: default platform specific behaviour</item>
        /// <item><c>"1"</c>: Force SDL_ThreadPriority to a realtime scheduling
        /// policy</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before calling SDL_SetCurrentThreadPriority</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ThreadForceRealtimeTimeCritical = "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";
        
        /// <summary>
        /// <para>A string specifying additional information to use with
        /// SDL_SetCurrentThreadPriority.</para>
        /// <para>By default SDL_SetCurrentThreadPriority will make appropriate system
        /// changes in order to apply a thread priority. For example on systems using
        /// pthreads the scheduler policy is changed automatically to a policy that
        /// works well with a given priority. Code which has specific requirements can
        /// override SDL's default behavior with this hint.</para>
        /// <para>pthread hint values are <c>"current"</c>, <c>"other"</c>, <c>"fifo"</c> and <c>"rr"</c>. Currently no
        /// other platform hint values are defined but may be in the future.</para>
        /// <para>On Linux, the kernel may send SIGKILL to realtime tasks which exceed the
        /// distro configured execution budget for rtkit. This budget can be queried
        /// through RLIMIT_RTTIME after calling SDL_SetCurrentThreadPriority.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling SDL_SetCurrentThreadPriority</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string ThreadPriorityPolicy = "SDL_THREAD_PRIORITY_POLICY";
        
        /// <summary>
        /// <para>A variable that controls the timer resolution, in milliseconds.</para>
        /// <para>The higher resolution the timer, the more frequently the CPU services timer
        /// interrupts, and the more precise delays are, but this takes up power and
        /// CPU time. This hint is only used on Windows.</para>
        /// <para>See this blog post for more information:
        /// http://randomascii.wordpress.com/2013/07/08/windows-timer-resolution-megawatts-wasted/</para>
        /// <para>The default value is <c>"1"</c>.</para>
        /// <para>If this variable is set to <c>"0"</c>, the system timer resolution is not set.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string TimerResolution = "SDL_TIMER_RESOLUTION";
        
        /// <summary>
        /// <para>A variable controlling whether touch events should generate synthetic mouse
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Touch events will not generate mouse events.</item>
        /// <item><c>"1"</c>: Touch events will generate mouse events. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string TouchMouseEvents = "SDL_TOUCH_MOUSE_EVENTS";
        
        /// <summary>
        /// <para>A variable controlling whether trackpads should be treated as touch
        /// devices.</para>
        /// <para>On macOS (and possibly other platforms in the future), SDL will report
        /// touches on a trackpad as mouse input, which is generally what users expect
        /// from this device; however, these are often actually full multitouch-capable
        /// touch devices, so it might be preferable to some apps to treat them as
        /// such.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Trackpad will send mouse events. (default)</item>
        /// <item><c>"1"</c>: Trackpad will send touch events.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string TrackpadIsTouchOnly = "SDL_TRACKPAD_IS_TOUCH_ONLY";
        
        /// <summary>
        /// <para>A variable controlling whether the Android / tvOS remotes should be listed
        /// as joystick devices, instead of sending keyboard events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Remotes send enter/escape/arrow key events.</item>
        /// <item><c>"1"</c>: Remotes are available as 2 axis, 2 button joysticks. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string TVRemoteAsJoystick = "SDL_TV_REMOTE_AS_JOYSTICK";
        
        /// <summary>
        /// <para>A variable controlling whether the screensaver is enabled.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable screensaver. (default)</item>
        /// <item><c>"1"</c>: Enable screensaver.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoAllowScreensaver = "SDL_VIDEO_ALLOW_SCREENSAVER";
        
        /// <summary>
        /// <para>A comma separated list containing the names of the displays that SDL should
        /// sort to the front of the display list.</para>
        /// <para>When this hint is set, displays with matching name strings will be
        /// prioritized in the list of displays, as exposed by calling
        /// <see cref="GetDisplays"/>, with the first listed becoming the primary display. The
        /// naming convention can vary depending on the environment, but it is usually
        /// a connector name (e.g. <c>'DP-1'</c>, <c>'DP-2'</c>, <c>'HDMI-A-1'</c>,etc...).</para>
        /// <para>On Wayland and X11 desktops, the connector names associated with displays
        /// can typically be found by using the <c>`xrandr`</c> utility.</para>
        /// <para>This hint is currently supported on the following drivers:</para>
        /// <list type="bullet">
        /// <item>KMSDRM (kmsdrm)</item>
        /// <item>Wayland (wayland)</item>
        /// <item>X11 (x11)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoDisplayPriority = "SDL_VIDEO_DISPLAY_PRIORITY";
        
        /// <summary>
        /// <para>Tell the video driver that we only want a double buffer.</para>
        /// <para>By default, most lowlevel 2D APIs will use a triple buffer scheme that
        /// wastes no CPU time on waiting for vsync after issuing a flip, but
        /// introduces a frame of latency. On the other hand, using a double buffer
        /// scheme instead is recommended for cases where low latency is an important
        /// factor because we save a whole frame of latency.</para>
        /// <para>We do so by waiting for vsync immediately after issuing a flip, usually
        /// just after eglSwapBuffers call in the backend's *_SwapWindow function.</para>
        /// <para>This hint is currently supported on the following drivers:</para>
        /// <list type="bullet">
        /// <item>Raspberry Pi (raspberrypi)</item>
        /// <item>Wayland (wayland)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoDoubleBuffer = "SDL_VIDEO_DOUBLE_BUFFER";
        
        /// <summary>
        /// <para>A variable that specifies a video backend to use.</para>
        /// <para>By default, SDL will try all available video backends in a reasonable order
        /// until it finds one that can work, but this hint allows the app or user to
        /// force a specific target, such as <c>"x11"</c> if, say, you are on Wayland but want
        /// to try talking to the X server instead.</para>
        /// <para>This hint accepts a comma-separated list of driver names, and each will be
        /// tried in the order listed during init, until one succeeds or all of them
        /// fail.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoDriver = "SDL_VIDEO_DRIVER";
        
        /// <summary>
        /// <para>A variable controlling whether the dummy video driver saves output frames.</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Video frames are not saved to disk. (default)</item>
        /// <item><c>"1"</c>: Video frames are saved to files in the format SDL_windowX-Y.bmp,
        /// where X is the window ID, and Y is the frame number.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoDummySaveFrames = "SDL_VIDEO_DUMMY_SAVE_FRAMES";
        
        /// <summary>
        /// <para>If eglGetPlatformDisplay fails, fall back to calling eglGetDisplay.</para>
        /// <para>The variable can be set to one of the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Do not fall back to eglGetDisplay.</item>
        /// <item><c>"1"</c>: Fall back to eglGetDisplay if eglGetPlatformDisplay fails. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoEGLAllowGetDisplayFallback = "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";
        
        /// <summary>
        /// <para>A variable controlling whether the OpenGL context should be created with
        /// EGL.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use platform-specific GL context creation API (GLX, WGL, CGL, etc).
        /// (default)</item>
        /// <item><c>"1"</c>: Use EGL</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoForceEGL = "SDL_VIDEO_FORCE_EGL";
        
        /// <summary>
        /// <para>A variable that specifies the policy for fullscreen Spaces on macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable Spaces support (FULLSCREEN_DESKTOP won't use them and
        /// <see cref="WindowFlags.Resizable"/> windows won't offer the fullscreen button on their
        /// titlebars).</item>
        /// <item><c>"1"</c>: Enable Spaces support (FULLSCREEN_DESKTOP will use them and
        /// <see cref="WindowFlags.Resizable"/> windows will offer the fullscreen button on their
        /// titlebars). (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
        
        /// <summary>
        /// <para>A variable that specifies the menu visibility when a window is fullscreen
        /// in Spaces on macOS.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The menu will be hidden when the window is in a fullscreen space,
        /// and not accessible by moving the mouse to the top of the screen.</item>
        /// <item><c>"1"</c>: The menu will be accessible when the window is in a fullscreen
        /// space.</item>
        /// <item>auto": The menu will be hidden if fullscreen mode was toggled on
        /// programmatically via <see cref="SetWindowFullscreen"/>, and accessible if
        /// fullscreen was entered via the "fullscreen" button on the window title
        /// bar. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.1.9.</since>
        public const string VideoMacFullScreenMenuVisibility = "SDL_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY";
        
        /// <summary>
        /// <para>A variable controlling whether SDL will attempt to automatically set the
        /// destination display to a mode most closely matching that of the previous
        /// display if an exclusive fullscreen window is moved onto it.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL will not attempt to automatically set a matching mode on the
        /// destination display. If an exclusive fullscreen window is moved to a new
        /// display, the window will become fullscreen desktop.</item>
        /// <item><c>"1"</c>: SDL will attempt to automatically set a mode on the destination
        /// display that most closely matches the mode of the display that the 
        /// exclusive fullscreen window was previously on. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string VideoMatchExclusiveModeOnMove = "SDL_VIDEO_MATCH_EXCLUSIVE_MODE_ON";
        
        /// <summary>
        /// <para>A variable controlling whether fullscreen windows are minimized when they
        /// lose focus.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Fullscreen windows will not be minimized when they lose focus.
        /// (default)</item>
        /// <item><c>"1"</c>: Fullscreen windows are minimized when they lose focus.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
        
        /// <summary>
        /// <para>A variable controlling whether the offscreen video driver saves output
        /// frames.</para>
        /// <para>This only saves frames that are generated using software rendering, not
        /// accelerated OpenGL rendering.</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Video frames are not saved to disk. (default)</item>
        /// <item><c>"1"</c>: Video frames are saved to files in the format SDL_windowX-Y.bmp,
        /// where X is the window ID, and Y is the frame number.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoOffscreenSaveFrames = "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES";
        
        /// <summary>
        /// <para>A variable controlling whether all window operations will block until
        /// complete.</para>
        /// <para>Window systems that run asynchronously may not have the results of window
        /// operations that resize or move the window applied immediately upon the
        /// return of the requesting function. Setting this hint will cause such
        /// operations to block after every call until the pending operation has
        /// completed. Setting this to <c>'1'</c> is the equivalent of calling
        /// <see cref="SDL.SyncWindow(nint)"/> after every function call.</para>
        /// <para>Be aware that amount of time spent blocking while waiting for window
        /// operations to complete can be quite lengthy, as animations may have to
        /// complete, which can take upwards of multiple seconds in some cases.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Window operations are non-blocking. (default)</item>
        /// <item><c>"1"</c>: Window operations will block until completed.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoSyncWindowOperations = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
        
        /// <summary>
        /// <para>A variable controlling whether the libdecor Wayland backend is allowed to
        /// be used.</para>
        /// <para>libdecor is used over xdg-shell when xdg-decoration protocol is
        /// unavailable.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: libdecor use is disabled.</item>
        /// <item><c>"1"</c>: libdecor use is enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWaylandAllowLibdecor = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
        
        /// <summary>
        /// <para>A variable controlling whether video mode emulation is enabled under
        /// Wayland.</para>
        /// <para>When this hint is set, a standard set of emulated CVT video modes will be
        /// exposed for use by the application. If it is disabled, the only modes
        /// exposed will be the logical desktop size and, in the case of a scaled
        /// desktop, the native display resolution.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Video mode emulation is disabled.</item>
        /// <item><c>"1"</c>: Video mode emulation is enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWaylandModeEmulation = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
        
        /// <summary>
        /// <para>A variable controlling how modes with a non-native aspect ratio are
        /// displayed under Wayland.</para>
        /// <para>When this hint is set, the requested scaling will be used when displaying
        /// fullscreen video modes that don't match the display's native aspect ratio.
        /// This is contingent on compositor viewport support.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"aspect"</c> - Video modes will be displayed scaled, in their proper aspect
        /// ratio, with black bars.</item>
        /// <item><c>"stretch"</c> - Video modes will be scaled to fill the entire display.
        /// (default)</item>
        /// <item><c>"none"</c> - Video modes will be displayed as 1:1 with no scaling.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWaylandModeScaling = "SDL_VIDEO_WAYLAND_MODE_SCALING";
        
        /// <summary>
        /// <para>A variable controlling whether the libdecor Wayland backend is preferred
        /// over native decorations.</para>
        /// <para>When this hint is set, libdecor will be used to provide window decorations,
        /// even if xdg-decoration is available. (Note that, by default, libdecor will
        /// use xdg-decoration itself if available).</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: libdecor is enabled only if server-side decorations are unavailable.
        /// (default)</item>
        /// <item><c>"1"</c>: libdecor is always enabled if available.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWaylandPreferLibdecor = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
        
        /// <summary>
        /// <para>A variable forcing non-DPI-aware Wayland windows to output at 1:1 scaling.</para>
        /// <para>This must be set before initializing the video subsystem.</para>
        /// <para>When this hint is set, Wayland windows that are not flagged as being
        /// DPI-aware will be output with scaling designed to force 1:1 pixel mapping.</para>
        /// <para>This is intended to allow legacy applications to be displayed without
        /// desktop scaling being applied, and has issues with certain display
        /// configurations, as this forces the window to behave in a way that Wayland
        /// desktops were not designed to accommodate:</para>
        /// <list type="bullet">
        /// <item>Rounding errors can result with odd window sizes and/or desktop scales,
        /// which can cause the window contents to appear slightly blurry.</item>
        /// <item>Positioning the window may be imprecise due to unit conversions and
        /// rounding.</item>
        /// <item>The window may be unusably small on scaled desktops.</item>
        /// <item>The window may jump in size when moving between displays of different
        /// scale factors.</item>
        /// <item>Displays may appear to overlap when using a multi-monitor setup with
        /// scaling enabled.</item>
        /// <item>Possible loss of cursor precision due to the logical size of the window
        /// being reduced.</item>
        /// </list>
        /// <para>New applications should be designed with proper DPI awareness handling
        /// instead of enabling this.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Windows will be scaled normally.</item>
        /// <item><c>"1"</c>: Windows will be forced to scale to achieve 1:1 output.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWaylandScaleToDisplay = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
        
        /// <summary>
        /// <para>A variable specifying which shader compiler to preload when using the
        /// Chrome ANGLE binaries.</para>
        /// <para>SDL has EGL and OpenGL ES2 support on Windows via the ANGLE project. It can
        /// use two different sets of binaries, those compiled by the user from source
        /// or those provided by the Chrome browser. In the later case, these binaries
        /// require that SDL loads a DLL providing the shader compiler.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"d3dcompiler_46.dll"</c> - best for Vista or later. (default)</item>
        /// <item><c>"d3dcompiler_43.dll"</c> - for XP support.</item>
        /// <item><c>"none"</c> - do not load any library, useful if you compiled ANGLE from
        /// source and included the compiler in your binaries.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoWinD3Dcompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
        
        /// <summary>
        /// A variable controlling whether SDL should call XSelectInput() to enable
        /// input events on X11 windows wrapped by SDL windows.
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Don't call XSelectInput(), assuming the native window code has done
        /// it already.</item>
        /// <item><c>"1"</c>: Call XSelectInput() to enable input events. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.10.</since>
        public const string VideoX11ExternalWindowInput = "SDL_VIDEO_X11_EXTERNAL_WINDOW_INPUT";
        
        /// <summary>
        /// <para>A variable controlling whether the X11 _NET_WM_BYPASS_COMPOSITOR hint
        /// should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable _NET_WM_BYPASS_COMPOSITOR.</item>
        /// <item><c>"1"</c>: Enable _NET_WM_BYPASS_COMPOSITOR. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11NetWMBypassCompositor = "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";
        
        /// <summary>
        /// <para>A variable controlling whether the X11 _NET_WM_PING protocol should be
        /// supported.</para>
        /// <para>By default SDL will use _NET_WM_PING, but for applications that know they
        /// will not always be able to respond to ping requests in a timely manner they
        /// can turn it off to avoid the window manager thinking the app is hung.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable _NET_WM_PING.</item>
        /// <item><c>"1"</c>: Enable _NET_WM_PING. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11NetWMPing = "SDL_VIDEO_X11_NET_WM_PING";
        
        /// <summary>
        /// <para>A variable controlling whether SDL uses DirectColor visuals.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable DirectColor visuals.</item>
        /// <item><c>"1"</c>: Enable DirectColor visuals. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before initializing the video subsystem.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11NoDirectColor = "SDL_VIDEO_X11_NODIRECTCOLOR";
        
        /// <summary>
        /// <para>A variable forcing the content scaling factor for X11 displays.</para>
        /// <para>The variable can be set to a floating point value in the range 1.0-10.0f</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11ScalingFactor = "SDL_VIDEO_X11_SCALING_FACTOR";
        
        /// <summary>
        /// <para>A variable forcing the visual ID used for X11 display modes.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing the video subsystem.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11VisualID = "SDL_VIDEO_X11_VISUALID";
        
        /// <summary>
        /// <para>A variable forcing the visual ID chosen for new X11 windows.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11WindowVisualID = "SDL_VIDEO_X11_WINDOW_VISUALID";
        
        /// <summary>
        /// <para>A variable controlling whether the X11 XRandR extension should be used.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable XRandR.</item>
        /// <item><c>"1"</c>: Enable XRandR. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VideoX11XRandR = "SDL_VIDEO_X11_XRANDR";
        
        /// <summary>
        /// <para>A variable controlling whether touch should be enabled on the back panel of
        /// the PlayStation Vita.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable touch on the back panel.</item>
        /// <item><c>"1"</c>: Enable touch on the back panel. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaEnableBackTouch = "SDL_VITA_ENABLE_BACK_TOUCH";
        
        /// <summary>
        /// <para>A variable controlling whether touch should be enabled on the front panel
        /// of the PlayStation Vita.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Disable touch on the front panel.</item>
        /// <item><c>"1"</c>: Enable touch on the front panel. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaEnableFrontTouch = "SDL_VITA_ENABLE_FRONT_TOUCH";
        
        /// <summary>
        /// <para>A variable controlling the module path on the PlayStation Vita.</para>
        /// <para>This hint defaults to <c>"app0:module"</c>;</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaModulePath = "SDL_VITA_MODULE_PATH";
        
        /// <summary>
        /// <para>A variable controlling whether to perform PVR initialization on the
        /// PlayStation Vita.</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Skip PVR initialization.</item>
        /// <item><c>"1"</c>: Perform the normal PVR initialization. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaPvrInit = "SDL_VITA_PVR_INIT";
        
        /// <summary>
        /// <para>A variable overriding the resolution reported on the PlayStation Vita.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"544"</c>: 544p (default)</item>
        /// <item><c>"720"</c>: 725p for PSTV</item>
        /// <item><c>"1080"</c>: 1088i for PSTV</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaResolution = "SDL_VITA_RESOLUTION";
        
        /// <summary>
        /// <para>A variable controlling whether OpenGL should be used instead of OpenGL ES
        /// on the PlayStation Vita.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use OpenGL ES. (default)</item>
        /// <item><c>"1"</c>: Use OpenGL.</item>
        /// </list>>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaPvrOpenGL = "SDL_VITA_PVR_OPENGL";
        
        /// <summary>
        /// <para>A variable controlling which touchpad should generate synthetic mouse
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Only front touchpad should generate mouse events. (default)</item>
        /// <item><c>"1"</c>: Only back touchpad should generate mouse events.</item>
        /// <item><c>"2"</c>: Both touchpads should generate mouse events.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VitaTouchMouseDevice = "SDL_VITA_TOUCH_MOUSE_DEVICE";
        
        /// <summary>
        /// <para>A variable overriding the display index used in <see cref="SDL.VulkanCreateSurface"/></para>
        /// <para>The display index starts at 0, which is the default.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="SDL.VulkanCreateSurface"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VulkanDisplay = "SDL_VULKAN_DISPLAY";
        
        /// <summary>
        /// <para>Specify the Vulkan library to load.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating a Vulkan window or calling
        /// <see cref="SDL.VulkanLoadLibrary(string)"/>.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string VulkanLibrary = "SDL_VULKAN_LIBRARY";
        
        /// <summary>
        /// <para>A variable controlling how the fact chunk affects the loading of a WAVE
        /// file.</para>
        /// <para>The fact chunk stores information about the number of samples of a WAVE
        /// file. The Standards Update from Microsoft notes that this value can be used
        /// to 'determine the length of the data in seconds'. This is especially useful
        /// for compressed formats (for which this is a mandatory chunk) if they
        /// produce multiple sample frames per block and truncating the block is not
        /// allowed. The fact chunk can exactly specify how many sample frames there
        /// should be in this case.</para>
        /// <para>Unfortunately, most application seem to ignore the fact chunk and so SDL
        /// ignores it by default as well.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"truncate"</c> - Use the number of samples to truncate the wave data if the
        /// fact chunk is present and valid.</item>
        /// <item><c>"strict"</c> - Like <c>"truncate"</c>, but raise an error if the fact chunk is
        /// invalid, not present for non-PCM formats, or if the data chunk doesn't
        /// have that many samples.</item>
        /// <item><c>"ignorezero"</c> - Like <c>"truncate"</c>, but ignore fact chunk if the number of
        /// samples is zero.</item>
        /// <item><c>"ignore"</c> - Ignore fact chunk entirely. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAVIO"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WaveFactChunk = "SDL_WAVE_FACT_CHUNK";
        
        /// <summary>
        /// <para>A variable controlling the maximum number of chunks in a WAVE file.</para>
        /// <para>This sets an upper bound on the number of chunks in a WAVE file to avoid
        /// wasting time on malformed or corrupt WAVE files. This defaults to <c>"10000"</c>.</para>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAVIO"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WaveChunkLimit = "SDL_WAVE_CHUNK_LIMIT";
        
        /// <summary>
        /// <para>A variable controlling how the size of the RIFF chunk affects the loading
        /// of a WAVE file.</para>
        /// <para>The size of the RIFF chunk (which includes all the sub-chunks of the WAVE
        /// file) is not always reliable. In case the size is wrong, it's possible to
        /// just ignore it and step through the chunks until a fixed limit is reached.</para>
        /// <para>Note that files that have trailing data unrelated to the WAVE file or
        /// corrupt files may slow down the loading process without a reliable
        /// boundary. By default, SDL stops after 10000 chunks to prevent wasting time.
        /// Use <see cref="WaveChunkLimit"/> to adjust this value.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"force"</c> - Always use the RIFF chunk size as a boundary for the chunk
        /// search.</item>
        /// <item><c>"ignorezero"</c> - Like force, but a zero size searches up to 4 GiB.
        /// (default)</item>
        /// <item><c>"ignore"</c> - Ignore the RIFF chunk size and always search up to 4 GiB.</item>
        /// <item><c>"maximum"</c> - Search for chunks until the end of file. (not recommended)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAVIO"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WaveRiffChunkSize = "SDL_WAVE_RIFF_CHUNK_SIZE";
        
        /// <summary>
        /// <para>A variable controlling how a truncated WAVE file is handled.</para>
        /// <para>A WAVE file is considered truncated if any of the chunks are incomplete or
        /// the data chunk size is not a multiple of the block size. By default, SDL
        /// decodes until the first incomplete block, as most applications seem to do.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"verystrict"</c> - Raise an error if the file is truncated.</item>
        /// <item><c>"strict"</c> - Like <c>"verystrict"</c>, but the size of the RIFF chunk is ignored.</item>
        /// <item><c>"dropframe"</c> - Decode until the first incomplete sample frame.</item>
        /// <item><c>"dropblock"</c> - Decode until the first incomplete block. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before calling <see cref="LoadWAV"/> or <see cref="LoadWAVIO"/></remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WaveTruncation = "SDL_WAVE_TRUNCATION";
        
        /// <summary>
        /// <para>A variable controlling whether the window is activated when the
        /// <see cref="RaiseWindow"/> function is called.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The window is not activated when the <see cref="RaiseWindow"/> function is
        /// called.</item>
        /// <item><c>"1"</c>: The window is activated when the <see cref="RaiseWindow"/> function is called.
        /// (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowActivateWhenRaised = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
        
        /// <summary>
        /// <para>A variable controlling whether the window is activated when the
        /// <see cref="ShowWindow"/> function is called.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The window is not activated when the <see cref="ShowWindow"/> function is
        /// called.</item>
        /// <item><c>"1"</c>: The window is activated when the <see cref="ShowWindow"/> function is called.
        /// (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowActivateWhenShown = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
        
        /// <summary>
        /// <para>If set to <c>"0"</c> then never set the top-most flag on an SDL Window even if the
        /// application requests it.</para>
        /// <para>This is a debugging aid for developers and not expected to be used by end
        /// users.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: don't allow topmost</item>
        /// <item><c>"1"</c>: allow topmost (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowAllowTopmost = "SDL_WINDOW_ALLOW_TOPMOST";
        
        /// <summary>
        /// <para>A variable controlling whether the window frame and title bar are
        /// interactive when the cursor is hidden.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The window frame is not interactive when the cursor is hidden (no
        /// move, resize, etc).</item>
        /// <item><c>"1"</c>: The window frame is interactive when the cursor is hidden. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowFrameUsableWhileCursorHidden = "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";
        
        /// <summary>
        /// <para>A variable controlling whether SDL generates window-close events for Alt+F4
        /// on Windows.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL will only do normal key handling for Alt+F4.</item>
        /// <item><c>"1"</c>: SDL will generate a window-close event when it sees Alt+F4.
        /// (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsCloseOnAltF4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
        
        /// <summary>
        /// <para>A variable controlling whether menus can be opened with their keyboard
        /// shortcut (Alt+mnemonic).</para>
        /// <para>If the mnemonics are enabled, then menus can be opened by pressing the Alt
        /// key and the corresponding mnemonic (for example, Alt+F opens the File
        /// menu). However, in case an invalid mnemonic is pressed, Windows makes an
        /// audible beep to convey that nothing happened. This is true even if the
        /// window has no menu at all!</para>
        /// <para>Because most SDL applications don't have menus, and some want to use the
        /// Alt key for other purposes, SDL disables mnemonics (and the beeping) by
        /// default.</para>
        /// <para>Note: This also affects keyboard events: with mnemonics enabled, when a
        /// menu is opened from the keyboard, you will not receive a KEYUP event for
        /// the mnemonic key, and <b>might</b> not receive one for Alt.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Alt+mnemonic does nothing, no beeping. (default)</item>
        /// <item><c>"1"</c>: Alt+mnemonic opens menus, invalid mnemonics produce a beep.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsEnableMenuMnemonics = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
        
        /// <summary>
        /// <para>A variable controlling whether the windows message loop is processed by
        /// SDL.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The window message loop is not run.</item>
        /// <item><c>"1"</c>: The window message loop is processed in <see cref="SDL.PumpEvents"/>. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsEnableMessageLoop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
        
        /// <summary>
        /// <para>A variable controlling whether GameInput is used for raw keyboard and mouse
        /// on Windows.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: GameInput is not used for raw keyboard and mouse events.</item>
        /// <item><c>"1"</c>: GameInput is used for raw keyboard and mouse events, if available.
        /// (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsGameInput = "SDL_WINDOWS_GAMEINPUT";
        
        /// <summary>
        /// <para>A variable controlling whether raw keyboard events are used on Windows.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: The Windows message loop is used for keyboard events. (default)</item>
        /// <item><c>"1"</c>: Low latency raw keyboard events are used.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsRawKeyboard = "SDL_WINDOWS_RAW_KEYBOARD";
        
        /// <summary>
        /// <para>A variable controlling whether SDL uses Kernel Semaphores on Windows.</para>
        /// <para>Kernel Semaphores are inter-process and require a context switch on every
        /// interaction. On Windows 8 and newer, the WaitOnAddress API is available.
        /// Using that and atomics to implement semaphores increases performance. SDL
        /// will fall back to Kernel Objects on older OS versions or if forced to by
        /// this hint.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use Atomics and WaitOnAddress API when available, otherwise fall
        /// back to Kernel Objects. (default)</item>
        /// <item><c>"1"</c>: Force the use of Kernel Objects in all cases.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsForceSemaphoreKernel = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
        
        /// <summary>
        /// <para>A variable to specify custom icon resource id from RC file on Windows
        /// platform.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsIntresourceIcon = "SDL_WINDOWS_INTRESOURCE_ICON";
        
        /// <summary>
        /// <para>A variable to specify custom icon resource id from RC file on Windows
        /// platform.</para>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsIntresourceIconSmall = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";
        
        /// <summary>
        /// <para>A variable controlling whether SDL uses the D3D9Ex API introduced in
        /// Windows Vista, instead of normal D3D9.</para>
        /// <para>Direct3D 9Ex contains changes to state management that can eliminate device
        /// loss errors during scenarios like Alt+Tab or UAC prompts. D3D9Ex may
        /// require some changes to your application to cope with the new behavior, so
        /// this is disabled by default.</para>
        /// <para>For more information on Direct3D 9Ex, see:</para>
        /// <list type="bullet">
        /// <item>https://docs.microsoft.com/en-us/windows/win32/direct3darticles/graphics-apis-in-windows-vista#direct3d-9ex</item>
        /// <item>https://docs.microsoft.com/en-us/windows/win32/direct3darticles/direct3d-9ex-improvements</item>
        /// </list>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Use the original Direct3D 9 API. (default)</item>
        /// <item><c>"1"</c>: Use the Direct3D 9Ex API on Vista and later (and fall back if D3D9Ex
        /// is unavailable)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsUseD3D9Ex = "SDL_WINDOWS_USE_D3D9EX";
        
        /// <summary>
        /// <para>A variable controlling whether SDL will clear the window contents when the
        /// WM_ERASEBKGND message is received.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>/<c>"never"</c>: Never clear the window.</item>
        /// <item><c>"1"</c>/<c>"initial"</c>: Clear the window when the first WM_ERASEBKGND event fires.
        /// (default)</item>
        /// <item><c>"2"</c>/<c>"always"</c>: Clear the window on every WM_ERASEBKGND event.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string WindowsEraseBackgroundMode = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
        
        /// <summary>
        /// <para>A variable controlling whether X11 windows are marked as override-redirect.</para>
        /// <para>If set, this _might_ increase framerate at the expense of the desktop not
        /// working as expected. Override-redirect windows aren't noticed by the window
        /// manager at all.</para>
        /// <para>You should probably only use this for fullscreen windows, and you probably
        /// shouldn't even use it for that. But it's here if you want to try!</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Do not mark the window as override-redirect. (default)</item>
        /// <item><c>"1"</c>: Mark the window as override-redirect.</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string X11ForceOverrideRedirect = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
        
        /// <summary>
        /// <para>A variable specifying the type of an X11 window.</para>
        /// <para>During <see cref="SDL.CreateWindow"/>, SDL uses the _NET_WM_WINDOW_TYPE X11 property to
        /// report to the window manager the type of window it wants to create. This
        /// might be set to various things if <see cref="WindowFlags.Tooltip"/> or
        /// <see cref="WindowFlags.PopupMenu"/>, etc, were specified. For <c>"normal"</c> windows that
        /// haven't set a specific type, this hint can be used to specify a custom
        /// type. For example, a dock window might set this to
        /// _NET_WM_WINDOW_TYPE_DOCK.</para>
        /// </summary>
        /// <remarks>This hint should be set before creating a window.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string X11WindowType = "SDL_X11_WINDOW_TYPE";
        
        /// <summary>
        /// <para>Specify the XCB library to load for the X11 driver.</para>
        /// <para>The default is platform-specific, often <c>"libX11-xcb.so.1"</c>.</para>
        /// </summary>
        /// <remarks>This hint should be set before initializing the video subsystem.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string X11XcbLibrary = "SDL_X11_XCB_LIBRARY";
        
        /// <summary>
        /// <para>A variable controlling whether XInput should be used for controller
        /// handling.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: XInput is not enabled.</item>
        /// <item><c>"1"</c>: XInput is enabled. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint should be set before SDL is initialized.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string XInputEnabled = "SDL_XINPUT_ENABLED";

        
        /// <summary>
        /// <para>A variable controlling response to SDL_assert failures.</para>
        /// <para>The variable can be set to the following case-sensitive values:</para>
        /// <list type="bullet">
        /// <item><c>"abort"</c>: Program terminates immediately.</item>
        /// <item><c>"break"</c>: Program triggers a debugger breakpoint.</item>
        /// <item><c>"retry"</c>: Program reruns the SDL_assert's test again.</item>
        /// <item><c>"ignore"</c>: Program continues on, ignoring this assertion failure this
        /// time.</item>
        /// <item><c>"always_ignore"</c>: Program continues on, ignoring this assertion failure
        /// for the rest of the run.</item>
        /// </list>
        /// <para>Note that SDL_SetAssertionHandler offers a programmatic means to deal with
        /// assertion failures through a callback, and this hint is largely intended to
        /// be used via environment variables by end users and automated tools.</para>
        /// </summary>
        /// <remarks>This hint should be set before an assertion failure is triggered and can be
        /// changed at any time.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string Assert = "SDL_ASSERT";
        
        
        /// <summary>
        /// <para>A variable controlling whether pen events should generate synthetic mouse
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Pen events will not generate mouse events.</item>
        /// <item><c>"1"</c>: Pen events will generate mouse events. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string PenMouseEvents = "SDL_PEN_MOUSE_EVENTS";
        
        
        
        /// <summary>
        /// <para>A variable controlling whether pen events should generate synthetic touch
        /// events.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: Pen events will not generate touch events.</item>
        /// <item><c>"1"</c>: Pen events will generate touch events. (default)</item>
        /// </list>
        /// </summary>
        /// <remarks>This hint can be set anytime.</remarks>
        /// <since>This hint is available since SDL 3.2.0</since>
        public const string PenTouchEvents = "SDL_PEN_TOUCH_EVENTS";


        /// <summary>
        /// <para>A variable controlling whether SDL logs some debug information.</para>
        /// <para>The variable can be set to the following values:</para>
        /// <list type="bullet">
        /// <item><c>"0"</c>: SDL debug information will not be logged. (default)</item>
        /// <item><c>"1"</c>: SDL debug information will be logged.</item>
        /// </list>
        /// <para>This is generally meant to be used as an environment variable to let
        /// end-users report what subsystems were chosen on their system, perhaps what
        /// sort of hardware they are running on, etc, to aid in debugging. Logged
        /// information is sent through <see cref="Log"/>, which means by default they appear
        /// on stdout on most platforms, or maybe <c>OutputDebugString()</c> on Windows, and
        /// can be funneled by the app with <see cref="SetLogOutputFunction"/>, etc.</para>
        /// <para>The specific output might change between SDL versions; more information
        /// might be deemed useful in the future.</para>
        /// </summary>
        /// <remarks>This hint can be set anytime, but the specific logs are generated during
        /// subsystem init.</remarks>
        /// <since>This hint is available since SDL 3.4.0.</since>
        public const string DebugLogging = "SDL_DEBUG_LOGGING";
    }
}