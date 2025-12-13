using SDL3;

namespace Mixer_Track_Playback;

internal class Program
{
    static void Main(string[] args)
    {
        const string mp3Path = "test.mp3";

        // SDL init
        if (!SDL.Init(SDL.InitFlags.Audio))
        {
            Console.WriteLine($"SDL Init Error: {SDL.GetError()}");
            return;
        }

        // Mixer init
        if (!Mixer.Init())
        {
            Console.WriteLine($"Mixer Init Error: {SDL.GetError()}");
            SDL.Quit();
            return;
        }
        
        // Open audio device from mixer
        // If a specification is required here, pass null for auto-fitting.
        var mixer = Mixer.CreateMixerDevice(SDL.AudioDeviceDefaultPlayback, IntPtr.Zero);
        if (mixer == IntPtr.Zero)
        {
            Console.WriteLine($"MixerDevice creation failed: {SDL.GetError()}");
            Mixer.Quit();
            SDL.Quit();
            return;
        }
        
        // Load audio file from file
        var audio = Mixer.LoadAudio(mixer, mp3Path, predecode: true);
        if (audio == IntPtr.Zero)
        {
            Console.WriteLine($"Failed loading audio: {SDL.GetError()}");
            Mixer.DestroyMixer(mixer);
            Mixer.Quit();
            SDL.Quit();
            return;
        }
        
        // Create track and attach loaded audio
        var track = Mixer.CreateTrack(mixer);
        if (track == IntPtr.Zero || !Mixer.SetTrackAudio(track, audio))
        {
            Console.WriteLine($"Failed creating track: {SDL.GetError()}");
            Mixer.DestroyAudio(audio);
            Mixer.DestroyMixer(mixer);
            Mixer.Quit();
            SDL.Quit();
            return;
        }

        // Play
        if (!Mixer.PlayTrack(track, 0))
        {
            Console.WriteLine($"Play failed: {SDL.GetError()}");
        }
        else
        {
            Console.WriteLine("Playing... Press Enter for exit.");
            Console.ReadLine();
        }

        // Destroyed
        Mixer.DestroyTrack(track);
        Mixer.DestroyAudio(audio);
        Mixer.DestroyMixer(mixer);
        Mixer.Quit();
        SDL.Quit();
    }
}