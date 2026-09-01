using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Util;
using Android.Widget;
using Avalonia.Android;
using AndroidSdl = Org.Libsdl.App.SDL;
using JavaSystem = Java.Lang.JavaSystem;

namespace AndroidAvaloniaMixerAudio;

[Activity(
    Label = "SDL3-CS Avalonia audio",
    Theme = "@style/AppTheme",
    MainLauncher = true,
    Exported = true,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public sealed class MainActivity : AvaloniaMainActivity
{
    private const string LogTag = "SDL3CS-AvaloniaAudio";

    private MixerAudio? _audio;
    private bool _bridgeReady;

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        try
        {
            JavaSystem.LoadLibrary("SDL3");
            AndroidSdl.SetupJNI();
            AndroidSdl.Initialize();
            AndroidSdl.Context = this;
            _bridgeReady = true;
            VolumeControlStream = Android.Media.Stream.Music;
            Write("bridge-ready");

            _audio = new MixerAudio(Write, action => RunOnUiThread(action));
            byte[] wave = WaveFactory.CreateSineWaveWav(
                sampleRate: 48_000,
                frequency: 440.0,
                duration: TimeSpan.FromSeconds(3));
            _audio.Play(wave);
        }
        catch (Exception exception)
        {
            Write($"playback-failed type={exception.GetType().FullName} message={exception.Message}");
            Toast.MakeText(this, exception.Message, ToastLength.Long)?.Show();
        }
    }

    protected override void OnPause()
    {
        _audio?.Pause();
        base.OnPause();
    }

    protected override void OnResume()
    {
        base.OnResume();
        _audio?.Resume();
    }

    protected override void OnDestroy()
    {
        try
        {
            _audio?.Dispose();
            _audio = null;

            if (_bridgeReady)
            {
                AndroidSdl.Context = null;
                _bridgeReady = false;
            }
        }
        finally
        {
            base.OnDestroy();
        }
    }

    private static void Write(string message)
    {
        Log.Info(LogTag, message);
    }
}
