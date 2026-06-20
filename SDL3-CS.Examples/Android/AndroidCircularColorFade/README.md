# Android example

Этот пример показывает официальный SDL3-CS Android path: `MainActivity` наследуется от `Org.Libsdl.App.SDLActivity`, загружает `SDL3` через `GetLibraries()` и запускает управляемый SDL loop в override `Main()`.

Bridge больше не хранится локально в примере. Пример использует project reference на `SDL3-CS.Android`, который доставляет bindings для `SDLActivity`, Java bridge SDL и базовые Android native assets в опубликованном package.

Для сборки нужна Android-среда:

```powershell
dotnet workload restore SDL3-CS.Examples\Android\AndroidCircularColorFade\AndroidCircularColorFade.csproj
dotnet build SDL3-CS.Examples\Android\AndroidCircularColorFade\AndroidCircularColorFade.csproj -c Release
```
