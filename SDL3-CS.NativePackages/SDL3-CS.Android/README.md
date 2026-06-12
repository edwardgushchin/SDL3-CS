# SDL3-CS.Android

`SDL3-CS.Android` доставляет Android `libSDL3.so`, привязки `Org.Libsdl.App.SDLActivity` и Java bridge SDL для Android-приложений на SDL3-CS.

Пакет нужен Android-потребителям, которые хотят запускать управляемый C# entrypoint вместо C `SDL_main`. Приложение наследуется от `SDLActivity`, переопределяет `GetLibraries()` и `Main()`, а обычные вызовы SDL выполняются через `SDL3.SDL`.

Минимальный набор пакетов для Android-приложения:

```powershell
dotnet add package SDL3-CS
dotnet add package SDL3-CS.Android
```

Для SDL_image, SDL_mixer, SDL_ttf или SDL_shadercross добавьте соответствующие packages: `SDL3-CS.Android.Image`, `SDL3-CS.Android.Mixer`, `SDL3-CS.Android.TTF` или `SDL3-CS.Android.Shadercross`.
