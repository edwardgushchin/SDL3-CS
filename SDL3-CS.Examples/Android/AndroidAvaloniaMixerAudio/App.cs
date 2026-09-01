using Android.App;
using Android.Runtime;
using Avalonia;
using Avalonia.Android;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace AndroidAvaloniaMixerAudio;

public sealed class App : global::Avalonia.Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is ISingleViewApplicationLifetime singleView)
        {
            singleView.MainView = new StackPanel
            {
                Margin = new Thickness(24),
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        FontSize = 22,
                        Text = "SDL3-CS + Avalonia Android audio"
                    },
                    new TextBlock
                    {
                        Text = "A three-second 440 Hz tone starts automatically. " +
                               "Filter logcat by SDL3CS-AvaloniaAudio for diagnostics.",
                        TextWrapping = global::Avalonia.Media.TextWrapping.Wrap
                    }
                }
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}

[Application]
public sealed class AvaloniaApp : AvaloniaAndroidApplication<App>
{
    public AvaloniaApp(nint javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
        => base.CustomizeAppBuilder(builder);
}
