using SDL3.Tests;

namespace SDL3.Tests.SDL.Audio.Audio;

internal static class PropsTests
{
    public static void AudioDeviceUniqueIdString_UsesNativePropertyName()
    {
        TestAssert.Equal("SDL.audio.device.unique_id", SDL3.SDL.Props.AudioDeviceUniqueIdString, "SDL.Props.AudioDeviceUniqueIdString must match SDL_PROP_AUDIO_DEVICE_UNIQUE_ID_STRING.");
    }
}
