using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);

if (SDL.Hints.JoystickDrumDevices != "SDL_JOYSTICK_DRUM_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickDrumDevices hint value.");
}

if (SDL.Hints.JoystickGuitarDevices != "SDL_JOYSTICK_GUITAR_DEVICES")
{
    throw new InvalidOperationException("Unexpected JoystickGuitarDevices hint value.");
}

Console.WriteLine("CreateAudioStream overload smoke test passed.");
Console.WriteLine("Joystick device hint constants smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);
