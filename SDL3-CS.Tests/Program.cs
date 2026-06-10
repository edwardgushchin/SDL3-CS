using SDL3;

CreateAudioStreamWithNullSpecs nullSpecOverload = SDL.CreateAudioStream;
CreateAudioStreamWithTypedSpecs typedSpecOverload = SDL.CreateAudioStream;

GC.KeepAlive(nullSpecOverload);
GC.KeepAlive(typedSpecOverload);

Console.WriteLine("CreateAudioStream overload smoke test passed.");

delegate IntPtr CreateAudioStreamWithNullSpecs(IntPtr srcSpec, IntPtr dstSpec);

delegate IntPtr CreateAudioStreamWithTypedSpecs(in SDL.AudioSpec srcSpec, in SDL.AudioSpec dstSpec);
