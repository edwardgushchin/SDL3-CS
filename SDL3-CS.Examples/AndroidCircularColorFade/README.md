# Android Example
1. `SDL3Bridge` and `libSDL3.so` from official release `.aar` build.
2. `Metadata.xml` exclude internal java element, fixed xamarin generate error (from ppy bindings).
3. `[Activity]` and `AndroidManifest.xml` content copy from SDL3 `android-project`.
4. For `X86_64 API 28`, in fact the sdl3 and xamarin both support the `API 21`, but official release build has some problem (maybe hash-style).