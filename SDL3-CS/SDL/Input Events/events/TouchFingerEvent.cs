using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    //TODO: Implement SDL_TouchFingerEvent

    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent;
}