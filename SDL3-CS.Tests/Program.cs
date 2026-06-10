using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;
IsPhoneDelegate isPhone = SDL.IsPhone;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);
GC.KeepAlive(isPhone);

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

Console.WriteLine("CreateAudioStream overload smoke test passed.");
Console.WriteLine("Joystick device hint constants smoke test passed.");
Console.WriteLine("Joystick GameInput raw hint constant smoke test passed.");
Console.WriteLine("IsPhone binding smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);

delegate bool IsPhoneDelegate();
