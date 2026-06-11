using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;
IsPhoneDelegate isPhone = SDL.IsPhone;
LoadJPGIODelegate loadJPGIO = SDL.LoadJPGIO;
LoadJPGDelegate loadJPG = SDL.LoadJPG;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);
GC.KeepAlive(isPhone);
GC.KeepAlive(loadJPGIO);
GC.KeepAlive(loadJPG);

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

if ((uint)SDL.EventType.WindowCurvatureChanged != (uint)SDL.EventType.WindowHDRStateChanged + 1)
{
    throw new InvalidOperationException("Unexpected WindowCurvatureChanged event value.");
}

if (SDL.EventType.WindowLast != SDL.EventType.WindowCurvatureChanged)
{
    throw new InvalidOperationException("Unexpected WindowLast event value after visionOS curvature sync.");
}

if (SDL.Props.WindowCreateCurvatureFloat != "SDL.window.create.curvature")
{
    throw new InvalidOperationException("Unexpected WindowCreateCurvatureFloat property value.");
}

if (SDL.Props.WindowCurvatureFloat != "SDL.window.curvature")
{
    throw new InvalidOperationException("Unexpected WindowCurvatureFloat property value.");
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

Console.WriteLine("CreateAudioStream overload smoke test passed.");
Console.WriteLine("Joystick device hint constants smoke test passed.");
Console.WriteLine("Joystick GameInput raw hint constant smoke test passed.");
Console.WriteLine("DOS framebuffer hint constant smoke test passed.");
Console.WriteLine("X11 XSync hint constant smoke test passed.");
Console.WriteLine("visionOS curvature window constants smoke test passed.");
Console.WriteLine("Text input property constants smoke test passed.");
Console.WriteLine("IsPhone binding smoke test passed.");
Console.WriteLine("JPEG surface loader binding smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate bool IsPhoneDelegate();

delegate IntPtr LoadJPGIODelegate(IntPtr src, bool closeio);

delegate IntPtr LoadJPGDelegate(string file);
