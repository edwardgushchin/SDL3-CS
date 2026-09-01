# SDL_mixer в Avalonia Activity на Android

Этот пример показывает узкий поддерживаемый сценарий: SDL используется только для audio, а единственное Android-окно остаётся `AvaloniaMainActivity`. Пример не наследуется от `SDLActivity`, не создаёт SDL surface и не заявляет поддержку SDL video/input внутри Avalonia visual tree.

После запуска приложение автоматически создаёт трёхсекундный PCM WAV с тоном 440 Гц, загружает его из managed memory через `SDL.IOFromConstMem` и проигрывает через SDL_mixer. Это даёт детерминированную проверку, не зависящую от asset или игрового файла.

## Что демонстрирует пример

- подготовку SDL Android JNI/context bridge внутри существующей `AvaloniaMainActivity`;
- инициализацию только `SDL.InitFlags.Audio` и SDL_mixer;
- явную проверку каждого результата с `SDL.GetError()`;
- удержание callback и pinned memory на необходимое время;
- регистрацию stopped callback до запуска track;
- pause/resume при изменениях Activity lifecycle;
- уничтожение track, audio и mixer до очистки Android SDL context;
- диагностические logcat markers для driver, device format, decoder list и прогресса track.

## Сборка

```powershell
dotnet build .\SDL3-CS.Examples\Android\AndroidAvaloniaMixerAudio\AndroidAvaloniaMixerAudio.csproj -c Release
```

Android SDK и JDK можно передать стандартными MSBuild properties `AndroidSdkDirectory` и `JavaSdkDirectory`.

## Автоматический smoke на устройстве или эмуляторе

```powershell
pwsh .\.github\release-tools\Test-AndroidAvaloniaMixerAudioSmoke.ps1 `
  -AndroidSdkDirectory C:\Android\Sdk `
  -JavaSdkDirectory 'C:\Program Files\Microsoft\jdk-17'
```

Скрипт выбирает RID по ABI устройства, собирает Release APK, устанавливает его без fast deployment, ожидает markers `bridge-ready`, `playback-started`, `playback-progress` и `playback-complete`, затем останавливает и удаляет тестовое приложение. Для нескольких подключённых устройств передайте `-DeviceId`.

## Диагностика пользовательского payload

Если детерминированный tone работает, а реальный файл нет, проверяйте нулевой результат `Mixer.LoadAudioIO` как ошибку и сохраняйте:

- `SDL.GetError()` сразу после отказавшего вызова;
- длину payload;
- первые 4–16 bytes в hex для определения контейнера;
- список `Mixer.GetAudioDecoder(...)`;
- Android ABI/API и `SDL.GetCurrentAudioDriver()`.

Не записывайте целый пользовательский audio payload в log. Для device-specific AAudio-проблемы отдельно можно проверить `SDL.SetHint(SDL.Hints.AndroidLowLatencyAudio, "0")` или `SDL.SetHint(SDL.Hints.AudioDriver, "openslES")` до `SDL.Init`, но это диагностический эксперимент, а не универсальная настройка примера.

## Связанная документация

- [Спецификация](../../../docs/specifications/android/avalonia-mixer-audio.md)
- [Документация реализации](../../../docs/documentation/android/avalonia-mixer-audio.md)
- [GitHub Discussion #266](https://github.com/edwardgushchin/SDL3-CS/discussions/266)
