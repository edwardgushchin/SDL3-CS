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

Для FreeType сохранён текст FreeType License. Исходные архивы game-music-emu, mpg123 и vkd3d находятся в `licenses/sources/` соответствующих пакетов, а хэши бинарников и исходные коммиты — в `licenses/provenance/`. Проверка выпуска отвергает архив или бинарник, не совпадающий с закреплённым источником и квитанцией сборки. Для LGPL-компонентов конечный разработчик игры также должен сохранять уведомления и, при статической компоновке, обеспечить получателям возможность повторной компоновки с изменённой библиотекой; проверка NuGet-пакета не удостоверяет каждый экспорт игры.
