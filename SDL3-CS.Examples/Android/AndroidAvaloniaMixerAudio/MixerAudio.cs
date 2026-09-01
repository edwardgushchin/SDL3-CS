using System.Runtime.InteropServices;
using SDL3;

namespace AndroidAvaloniaMixerAudio;

internal sealed class MixerAudio : IDisposable
{
    private readonly Action<string> _write;
    private readonly Action<Action> _dispatch;
    private readonly Mixer.TrackStoppedCallback _stoppedCallback;
    private GCHandle _stoppedCallbackHandle;

    private IntPtr _mixer;
    private IntPtr _audio;
    private IntPtr _track;
    private bool _sdlInitialized;
    private bool _mixerInitialized;
    private bool _disposed;

    public MixerAudio(Action<string> write, Action<Action> dispatch)
    {
        _write = write;
        _dispatch = dispatch;
        _stoppedCallback = OnTrackStopped;
        _stoppedCallbackHandle = GCHandle.Alloc(_stoppedCallback);

        try
        {
            Require(SDL.SetHint(SDL.Hints.AppName, nameof(AndroidAvaloniaMixerAudio)), "SDL_SetHint(SDL_APP_NAME)");
            Require(SDL.Init(SDL.InitFlags.Audio), "SDL_Init(SDL_INIT_AUDIO)");
            _sdlInitialized = true;

            Require(Mixer.Init(), "MIX_Init");
            _mixerInitialized = true;

            _mixer = Mixer.CreateMixerDevice(SDL.AudioDeviceDefaultPlayback, IntPtr.Zero);
            Require(_mixer != IntPtr.Zero, "MIX_CreateMixerDevice");
            WriteDeviceDiagnostics();
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    public void Play(byte[] data)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        ArgumentNullException.ThrowIfNull(data);
        if (data.Length == 0)
        {
            throw new ArgumentException("Audio payload must not be empty.", nameof(data));
        }

        DisposePlayback();
        try
        {
            GCHandle dataHandle = GCHandle.Alloc(data, GCHandleType.Pinned);
            try
            {
                IntPtr io = SDL.IOFromConstMem(dataHandle.AddrOfPinnedObject(), (nuint)data.Length);
                Require(io != IntPtr.Zero, "SDL_IOFromConstMem");

                _audio = Mixer.LoadAudioIO(_mixer, io, predecode: true, closeio: true);
                Require(_audio != IntPtr.Zero, "MIX_LoadAudio_IO");
            }
            finally
            {
                dataHandle.Free();
            }

            _track = Mixer.CreateTrack(_mixer);
            Require(_track != IntPtr.Zero, "MIX_CreateTrack");
            Require(Mixer.SetTrackAudio(_track, _audio), "MIX_SetTrackAudio");
            Require(
                Mixer.SetTrackStoppedCallback(_track, _stoppedCallback, IntPtr.Zero),
                "MIX_SetTrackStoppedCallback");
            Require(Mixer.PlayTrack(_track, 0), "MIX_PlayTrack");

            _write($"playback-started bytes={data.Length} track={_track}");
            _ = ReportProgressAsync(_track);
        }
        catch
        {
            DisposePlayback();
            throw;
        }
    }

    public void Pause()
    {
        if (_track != IntPtr.Zero && !Mixer.PauseTrack(_track))
        {
            _write($"pause-failed error={SDL.GetError()}");
        }
    }

    public void Resume()
    {
        if (_track != IntPtr.Zero && !Mixer.ResumeTrack(_track))
        {
            _write($"resume-failed error={SDL.GetError()}");
        }
    }

    public void Dispose()
    {
        if (_disposed)
        {
            return;
        }

        _disposed = true;
        DisposePlayback();

        if (_mixer != IntPtr.Zero)
        {
            Mixer.DestroyMixer(_mixer);
            _mixer = IntPtr.Zero;
        }

        if (_mixerInitialized)
        {
            Mixer.Quit();
            _mixerInitialized = false;
        }

        if (_sdlInitialized)
        {
            SDL.QuitSubSystem(SDL.InitFlags.Audio);
            _sdlInitialized = false;
        }

        if (_stoppedCallbackHandle.IsAllocated)
        {
            _stoppedCallbackHandle.Free();
        }
    }

    private async Task ReportProgressAsync(IntPtr expectedTrack)
    {
        await Task.Delay(500).ConfigureAwait(false);
        TryDispatch(() =>
        {
            if (_disposed || _track != expectedTrack)
            {
                return;
            }

            _write(
                $"playback-progress position={Mixer.GetTrackPlaybackPosition(_track)} " +
                $"remaining={Mixer.GetTrackRemaining(_track)}");
        });
    }

    private void OnTrackStopped(IntPtr userdata, IntPtr stoppedTrack)
    {
        TryDispatch(() =>
        {
            if (_disposed || _track != stoppedTrack)
            {
                return;
            }

            _write("playback-complete");
            DisposePlayback();
        });
    }

    private void TryDispatch(Action action)
    {
        try
        {
            _dispatch(action);
        }
        catch (Exception exception)
        {
            _write($"dispatch-failed type={exception.GetType().FullName} message={exception.Message}");
        }
    }

    private void DisposePlayback()
    {
        if (_track != IntPtr.Zero)
        {
            Mixer.DestroyTrack(_track);
            _track = IntPtr.Zero;
        }

        if (_audio != IntPtr.Zero)
        {
            Mixer.DestroyAudio(_audio);
            _audio = IntPtr.Zero;
        }
    }

    private void WriteDeviceDiagnostics()
    {
        uint mixerProperties = Mixer.GetMixerProperties(_mixer);
        uint deviceId = checked((uint)SDL.GetNumberProperty(
            mixerProperties,
            Mixer.Props.MixerDeviceNumber,
            0));
        Require(deviceId != 0, "MIX_PROP_MIXER_DEVICE_NUMBER");
        Require(
            SDL.GetAudioDeviceFormat(deviceId, out SDL.AudioSpec spec, out int sampleFrames),
            "SDL_GetAudioDeviceFormat");

        string decoders = string.Join(
            ',',
            Enumerable.Range(0, Mixer.GetNumAudioDecoders())
                .Select(Mixer.GetAudioDecoder)
                .Where(name => name is not null));
        _write(
            $"mixer-ready driver={SDL.GetCurrentAudioDriver() ?? "<null>"} device={deviceId} " +
            $"format={spec.Format}/{spec.Channels}/{spec.Freq} frames={sampleFrames} decoders={decoders}");
    }

    private static void Require(bool success, string operation)
    {
        if (!success)
        {
            throw new InvalidOperationException($"{operation} failed: {SDL.GetError()}");
        }
    }
}
