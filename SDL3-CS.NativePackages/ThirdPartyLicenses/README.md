# Лицензии нативных пакетов SDL3-CS

Корневой `LICENSE` пакета относится к коду SDL3-CS. Тексты в `licenses/` относятся к нативным библиотекам и зависимостям. При распространении пакета сохраняйте обе группы документов.

| Компонент пакета | Каталоги в `licenses/` | Платформы |
| --- | --- | --- |
| SDL | `SDL` | Windows, Linux, MacOS, Android, iOS, tvOS |
| Image | `Image`, `libwebp` | Windows, Linux, MacOS, Android, iOS, tvOS |
| Mixer | `Mixer` | Windows, Linux, MacOS, Android, iOS, tvOS |
| TTF | `TTF` | Windows, Linux, MacOS, Android, iOS, tvOS |
| Shadercross | `Shadercross` | Windows, Linux, MacOS, Android, iOS, tvOS |
| Shadercross | `DirectXShaderCompiler` | Windows, Linux, MacOS |
| Shadercross | `ShadercrossVkd3d` | Linux, MacOS |

Тексты лицензий SDL, SDL_image, SDL_mixer, SDL_ttf и SDL_shadercross взяты из исходников, зафиксированных в [release-manifest.json](../../.github/release-tools/release-manifest.json) через `sourceRef`. Тексты зависимостей взяты из соответствующих upstream-проектов. Набор для каждого компонента охватывает разные варианты сборки и может содержать лицензии зависимостей, не входящих в отдельный RID.

Для FreeType сохранён текст FreeType License. Для компонентов под LGPL, включая game-music-emu, mpg123 и vkd3d, одного текста лицензии недостаточно: при распространении бинарников нужны соответствующие исходники и соблюдение условий замены или повторной компоновки библиотеки. Точные ревизии исходников этих уже включённых нативных бинарников и выполнение этих условий следует подтвердить перед следующим выпуском. Этот каталог не удостоверяет выполнение указанных обязательств.
