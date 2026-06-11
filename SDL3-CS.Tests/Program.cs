using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;
SetAudioStreamFormatWithNullSpecs setAudioStreamFormatNull = SDL.SetAudioStreamFormat;
SetAudioStreamFormatWithTypedSpecs setAudioStreamFormatTyped = SDL.SetAudioStreamFormat;
AlignedAllocZeroDelegate alignedAllocZero = SDL.AlignedAllocZero;
IsPhoneDelegate isPhone = SDL.IsPhone;
HasSVE2Delegate hasSVE2 = SDL.HasSVE2;
LoadJPGIODelegate loadJPGIO = SDL.LoadJPGIO;
LoadJPGDelegate loadJPG = SDL.LoadJPG;
GamepadHasCapSenseDelegate gamepadHasCapSense = SDL.GamepadHasCapSense;
GetGamepadCapSenseDelegate getGamepadCapSense = SDL.GetGamepadCapSense;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);
GC.KeepAlive(setAudioStreamFormatNull);
GC.KeepAlive(setAudioStreamFormatTyped);
GC.KeepAlive(alignedAllocZero);
GC.KeepAlive(isPhone);
GC.KeepAlive(hasSVE2);
GC.KeepAlive(loadJPGIO);
GC.KeepAlive(loadJPG);
GC.KeepAlive(gamepadHasCapSense);
GC.KeepAlive(getGamepadCapSense);

if (SDL.Hints.JoystickDrumDevices != "SDL_JOYSTICK_DRUM_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickDrumDevices hint value.");
}

if (SDL.Hints.JoystickGuitarDevices != "SDL_JOYSTICK_GUITAR_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickGuitarDevices hint value.");
}

if (SDL.Hints.JoystickGameInputRaw != "SDL_JOYSTICK_GAMEINPUT_RAW")
{
    throw new InvalidOperationException("Unexpected JoystickGameInputRaw hint value.");
}

if (SDL.Hints.DosAllowDirectFramebuffer != "SDL_DOS_ALLOW_DIRECT_FRAMEBUFFER")
{
    throw new InvalidOperationException("Unexpected DosAllowDirectFramebuffer hint value.");
}

if (SDL.Hints.VideoX11EnableXsyncExt != "SDL_VIDEO_X11_ENABLE_XSYNC_EXT")
{
    throw new InvalidOperationException("Unexpected VideoX11EnableXsyncExt hint value.");
}

if (SDL.Hints.AndroidAllowPersistentFolderAccess != "SDL_ANDROID_ALLOW_PERSISTENT_FOLDER_ACCESS")
{
    throw new InvalidOperationException("Unexpected AndroidAllowPersistentFolderAccess hint value.");
}

if (SDL.Hints.AudioDuckOthers != "SDL_AUDIO_DUCK_OTHERS")
{
    throw new InvalidOperationException("Unexpected AudioDuckOthers hint value.");
}

if ((uint)SDL.EventType.WindowSettingsChanged != (uint)SDL.EventType.WindowHDRStateChanged + 1)
{
    throw new InvalidOperationException("Unexpected WindowSettingsChanged event value.");
}

if (SDL.EventType.WindowLast != SDL.EventType.WindowSettingsChanged)
{
    throw new InvalidOperationException("Unexpected WindowLast event value after visionOS settings sync.");
}

if (SDL.Props.WindowCreateVisionOSSettingsString != "SDL.window.create.visionos.settings")
{
    throw new InvalidOperationException("Unexpected WindowCreateVisionOSSettingsString property value.");
}

if (SDL.Props.WindowVisionOSSettingsString != "SDL.window.visionos.settings")
{
    throw new InvalidOperationException("Unexpected WindowVisionOSSettingsString property value.");
}

if (SDL.Props.TextInputTitleString != "SDL.textinput.title")
{
    throw new InvalidOperationException("Unexpected TextInputTitleString property value.");
}

if (SDL.Props.TextInputPlaceholderString != "SDL.textinput.placeholder")
{
    throw new InvalidOperationException("Unexpected TextInputPlaceholderString property value.");
}

if (SDL.Props.TextInputDefaultTextString != "SDL.textinput.default_text")
{
    throw new InvalidOperationException("Unexpected TextInputDefaultTextString property value.");
}

if (SDL.Props.TextInputMaxLengthNumber != "SDL.textinput.max_length")
{
    throw new InvalidOperationException("Unexpected TextInputMaxLengthNumber property value.");
}

if ((int)SDL.SystemCursor.ContextMenu != 20)
{
    throw new InvalidOperationException("Unexpected ContextMenu cursor value.");
}

if ((int)SDL.SystemCursor.ZoomOut != 33)
{
    throw new InvalidOperationException("Unexpected ZoomOut cursor value.");
}

if ((int)SDL.SystemCursor.SDLNumSystemCursors != 34)
{
    throw new InvalidOperationException("Unexpected SDLNumSystemCursors value after CSS cursor sync.");
}

if ((int)SDL.GamepadCapSenseType.Invalid != -1)
{
    throw new InvalidOperationException("Unexpected Invalid gamepad cap sense value.");
}

if ((int)SDL.GamepadCapSenseType.LeftStick != 0)
{
    throw new InvalidOperationException("Unexpected LeftStick gamepad cap sense value.");
}

if ((int)SDL.GamepadCapSenseType.Count != 4)
{
    throw new InvalidOperationException("Unexpected Count gamepad cap sense value.");
}

if ((uint)SDL.EventType.GamepadCapSenseTouch != (uint)SDL.EventType.GamepadSteamHandleUpdated + 1)
{
    throw new InvalidOperationException("Unexpected GamepadCapSenseTouch event value.");
}

if ((uint)SDL.EventType.GamepadCapSenseRelease != (uint)SDL.EventType.GamepadCapSenseTouch + 1)
{
    throw new InvalidOperationException("Unexpected GamepadCapSenseRelease event value.");
}

SDL.Event capSenseUnion = default;
capSenseUnion.GCapSense.Type = SDL.EventType.GamepadCapSenseTouch;
if (capSenseUnion.GCapSense.Type != SDL.EventType.GamepadCapSenseTouch)
{
    throw new InvalidOperationException("Unexpected gamepad cap sense union field behavior.");
}

if ((int)SDL.GamepadType.Steam != (int)SDL.GamepadType.GameCube + 1)
{
    throw new InvalidOperationException("Unexpected Steam gamepad type value.");
}

if ((int)SDL.GamepadType.Count != (int)SDL.GamepadType.Steam + 1)
{
    throw new InvalidOperationException("Unexpected gamepad type count after Steam insertion.");
}

Console.WriteLine("CreateAudioStream overload smoke test passed.");
Console.WriteLine("SetAudioStreamFormat overload smoke test passed.");
Console.WriteLine("AlignedAllocZero binding smoke test passed.");
Console.WriteLine("Joystick device hint constants smoke test passed.");
Console.WriteLine("Joystick GameInput raw hint constant smoke test passed.");
Console.WriteLine("DOS framebuffer hint constant smoke test passed.");
Console.WriteLine("X11 XSync hint constant smoke test passed.");
Console.WriteLine("Android persistent folder access hint smoke test passed.");
Console.WriteLine("Audio duck others hint smoke test passed.");
Console.WriteLine("visionOS settings window constants smoke test passed.");
Console.WriteLine("Text input property constants smoke test passed.");
Console.WriteLine("IsPhone binding smoke test passed.");
Console.WriteLine("SVE2 CPU binding smoke test passed.");
Console.WriteLine("JPEG surface loader binding smoke test passed.");
Console.WriteLine("CSS system cursor enum smoke test passed.");
Console.WriteLine("Gamepad cap sense API smoke test passed.");
Console.WriteLine("Steam gamepad type smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate bool SetAudioStreamFormatWithNullSpecs(IntPtr stream, IntPtr srcSpec, IntPtr dstSpec);

delegate bool SetAudioStreamFormatWithTypedSpecs(IntPtr stream, in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate IntPtr AlignedAllocZeroDelegate(UIntPtr alignment, UIntPtr size);

delegate bool IsPhoneDelegate();

delegate bool HasSVE2Delegate();

delegate IntPtr LoadJPGIODelegate(IntPtr src, bool closeio);

delegate IntPtr LoadJPGDelegate(string file);

delegate bool GamepadHasCapSenseDelegate(IntPtr gamepad, SDL.GamepadCapSenseType type);

delegate bool GetGamepadCapSenseDelegate(IntPtr gamepad, SDL.GamepadCapSenseType type);
