using System.Reflection;
using System.Runtime.InteropServices;

namespace SDL3.Tests.Image;

internal static class PInvokeTests
{
    public static void Version_ReturnsLinkedSdlImageVersion()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.Version), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Image.Version method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.Version must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.Version must import from SDL3_image.");
        TestAssert.Equal("IMG_Version", libraryImport.EntryPoint, "Image.Version must bind IMG_Version.");

        int version = SDL3.Image.Version();
        int major = version / 1_000_000;
        int minor = version / 1_000 % 1_000;
        int patch = version % 1_000;

        TestAssert.Equal(3, major, "Image.Version must return an SDL_image 3.x version.");
        TestAssert.True(minor >= 0, "Image.Version minor component must be non-negative.");
        TestAssert.True(patch >= 0, "Image.Version patch component must be non-negative.");
    }

    public static void Load_ReturnsNullForMissingFile()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.Load), BindingFlags.Public | BindingFlags.Static, [typeof(string)]);
        TestAssert.NotNull(method, "Image.Load(string) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.Load(string) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.Load(string) must import from SDL3_image.");
        TestAssert.Equal("IMG_Load", libraryImport.EntryPoint, "Image.Load(string) must bind IMG_Load.");

        string missingPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-missing-{Guid.NewGuid():N}.bmp");
        IntPtr surface = SDL3.Image.Load(missingPath);
        TestAssert.Equal(IntPtr.Zero, surface, "Image.Load(string) must return IntPtr.Zero for a missing file.");
    }

    public static void Load_ReturnsSurfaceForBmpFile()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr surface = IntPtr.Zero;

        try
        {
            surface = SDL3.Image.Load(tempPath);
            TestAssert.True(surface != IntPtr.Zero, "Image.Load(string) must return a surface for a valid BMP file.");
        }
        finally
        {
            if (surface != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(surface);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadIO_ReturnsNullForNullStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(bool)]);
        TestAssert.NotNull(method, "Image.LoadIO(IntPtr, bool) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadIO(IntPtr, bool) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadIO(IntPtr, bool) must import from SDL3_image.");
        TestAssert.Equal("IMG_Load_IO", libraryImport.EntryPoint, "Image.LoadIO(IntPtr, bool) must bind IMG_Load_IO.");

        IntPtr surface = SDL3.Image.LoadIO(IntPtr.Zero, closeio: false);
        TestAssert.Equal(IntPtr.Zero, surface, "Image.LoadIO(IntPtr, bool) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadIO_ReturnsSurfaceAndClosesOwnedStream()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-io-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr io = IntPtr.Zero;
        IntPtr surface = IntPtr.Zero;

        try
        {
            io = SDL3.SDL.IOFromFile(tempPath, "rb");
            TestAssert.True(io != IntPtr.Zero, "SDL.IOFromFile must open the temporary BMP file.");

            surface = SDL3.Image.LoadIO(io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(surface != IntPtr.Zero, "Image.LoadIO(IntPtr, bool) must return a surface for a valid BMP stream.");
        }
        finally
        {
            if (surface != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(surface);
            }

            if (io != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(io);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadTypedIO_ReturnsNullForNullStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadTypedIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(bool), typeof(string)]);
        TestAssert.NotNull(method, "Image.LoadTypedIO(IntPtr, bool, string) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadTypedIO(IntPtr, bool, string) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadTypedIO(IntPtr, bool, string) must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadTyped_IO", libraryImport.EntryPoint, "Image.LoadTypedIO(IntPtr, bool, string) must bind IMG_LoadTyped_IO.");

        IntPtr surface = SDL3.Image.LoadTypedIO(IntPtr.Zero, closeio: false, type: "BMP");
        TestAssert.Equal(IntPtr.Zero, surface, "Image.LoadTypedIO(IntPtr, bool, string) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadTypedIO_ReturnsSurfaceAndClosesOwnedStream()
    {
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-typed-io-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr io = IntPtr.Zero;
        IntPtr surface = IntPtr.Zero;

        try
        {
            io = SDL3.SDL.IOFromFile(tempPath, "rb");
            TestAssert.True(io != IntPtr.Zero, "SDL.IOFromFile must open the temporary BMP file.");

            surface = SDL3.Image.LoadTypedIO(io, closeio: true, type: "BMP");
            io = IntPtr.Zero;

            TestAssert.True(surface != IntPtr.Zero, "Image.LoadTypedIO(IntPtr, bool, string) must return a surface for a valid BMP stream and explicit BMP type.");
        }
        finally
        {
            if (surface != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(surface);
            }

            if (io != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(io);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadTexture_ReturnsNullForMissingFile()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadTexture), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, "Image.LoadTexture(IntPtr, string) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadTexture(IntPtr, string) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadTexture(IntPtr, string) must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadTexture", libraryImport.EntryPoint, "Image.LoadTexture(IntPtr, string) must bind IMG_LoadTexture.");

        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        string missingPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-missing-texture-{Guid.NewGuid():N}.bmp");
        IntPtr texture = SDL3.Image.LoadTexture(renderer.Renderer, missingPath);
        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadTexture(IntPtr, string) must return IntPtr.Zero for a missing file.");
    }

    public static void LoadTexture_ReturnsTextureForBmpFile()
    {
        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-texture-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr texture = IntPtr.Zero;

        try
        {
            texture = SDL3.Image.LoadTexture(renderer.Renderer, tempPath);
            TestAssert.True(texture != IntPtr.Zero, "Image.LoadTexture(IntPtr, string) must return a texture for a valid BMP file.");
        }
        finally
        {
            if (texture != IntPtr.Zero)
            {
                SDL3.SDL.DestroyTexture(texture);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadTextureIO_ReturnsNullForNullStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadTextureIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool)]);
        TestAssert.NotNull(method, "Image.LoadTextureIO(IntPtr, IntPtr, bool) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadTextureIO(IntPtr, IntPtr, bool) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadTextureIO(IntPtr, IntPtr, bool) must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadTexture_IO", libraryImport.EntryPoint, "Image.LoadTextureIO(IntPtr, IntPtr, bool) must bind IMG_LoadTexture_IO.");

        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        IntPtr texture = SDL3.Image.LoadTextureIO(renderer.Renderer, IntPtr.Zero, closeio: false);
        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadTextureIO(IntPtr, IntPtr, bool) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadTextureIO_ReturnsTextureAndClosesOwnedStream()
    {
        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-texture-io-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr io = IntPtr.Zero;
        IntPtr texture = IntPtr.Zero;

        try
        {
            io = SDL3.SDL.IOFromFile(tempPath, "rb");
            TestAssert.True(io != IntPtr.Zero, "SDL.IOFromFile must open the temporary BMP file.");

            texture = SDL3.Image.LoadTextureIO(renderer.Renderer, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(texture != IntPtr.Zero, "Image.LoadTextureIO(IntPtr, IntPtr, bool) must return a texture for a valid BMP stream.");
        }
        finally
        {
            if (texture != IntPtr.Zero)
            {
                SDL3.SDL.DestroyTexture(texture);
            }

            if (io != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(io);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadTextureTypedIO_ReturnsNullForNullStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadTextureTypedIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(string)]);
        TestAssert.NotNull(method, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadTextureTyped_IO", libraryImport.EntryPoint, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) must bind IMG_LoadTextureTyped_IO.");

        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        IntPtr texture = SDL3.Image.LoadTextureTypedIO(renderer.Renderer, IntPtr.Zero, closeio: false, type: "BMP");
        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadTextureTypedIO_ReturnsTextureAndClosesOwnedStream()
    {
        using SoftwareRendererScope renderer = SoftwareRendererScope.Create();
        string tempPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-load-texture-typed-io-{Guid.NewGuid():N}.bmp");
        File.WriteAllBytes(tempPath, CreateOnePixelBmp());

        IntPtr io = IntPtr.Zero;
        IntPtr texture = IntPtr.Zero;

        try
        {
            io = SDL3.SDL.IOFromFile(tempPath, "rb");
            TestAssert.True(io != IntPtr.Zero, "SDL.IOFromFile must open the temporary BMP file.");

            texture = SDL3.Image.LoadTextureTypedIO(renderer.Renderer, io, closeio: true, type: "BMP");
            io = IntPtr.Zero;

            TestAssert.True(texture != IntPtr.Zero, "Image.LoadTextureTypedIO(IntPtr, IntPtr, bool, string) must return a texture for a valid BMP stream and explicit BMP type.");
        }
        finally
        {
            if (texture != IntPtr.Zero)
            {
                SDL3.SDL.DestroyTexture(texture);
            }

            if (io != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(io);
            }

            if (File.Exists(tempPath))
            {
                File.Delete(tempPath);
            }
        }
    }

    public static void LoadGPUTexture_ReturnsNullForMissingFileWithoutGpuContext()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadGPUTexture), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(string), typeof(int).MakeByRefType(), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Image.LoadGPUTexture(IntPtr, IntPtr, string, out int, out int) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadGPUTexture must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadGPUTexture must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadGPUTexture", libraryImport.EntryPoint, "Image.LoadGPUTexture must bind IMG_LoadGPUTexture.");

        string missingPath = Path.Combine(Path.GetTempPath(), $"sdl3-cs-missing-gpu-texture-{Guid.NewGuid():N}.bmp");
        IntPtr texture = SDL3.Image.LoadGPUTexture(IntPtr.Zero, IntPtr.Zero, missingPath, out int width, out int height);

        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadGPUTexture must return IntPtr.Zero without a GPU context.");
        TestAssert.True(width >= 0, "Image.LoadGPUTexture width out parameter must be initialized.");
        TestAssert.True(height >= 0, "Image.LoadGPUTexture height out parameter must be initialized.");
    }

    public static void LoadGPUTextureIO_ReturnsNullForNullStreamWithoutGpuContext()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadGPUTextureIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(int).MakeByRefType(), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Image.LoadGPUTextureIO(IntPtr, IntPtr, IntPtr, bool, out int, out int) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadGPUTextureIO must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadGPUTextureIO must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadGPUTexture_IO", libraryImport.EntryPoint, "Image.LoadGPUTextureIO must bind IMG_LoadGPUTexture_IO.");

        IntPtr texture = SDL3.Image.LoadGPUTextureIO(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, closeio: false, out int width, out int height);

        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadGPUTextureIO must return IntPtr.Zero without a GPU context or stream.");
        TestAssert.True(width >= 0, "Image.LoadGPUTextureIO width out parameter must be initialized.");
        TestAssert.True(height >= 0, "Image.LoadGPUTextureIO height out parameter must be initialized.");
    }

    public static void LoadGPUTextureTypedIO_ReturnsNullForNullStreamWithoutGpuContext()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadGPUTextureTypedIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(string), typeof(int).MakeByRefType(), typeof(int).MakeByRefType()]);
        TestAssert.NotNull(method, "Image.LoadGPUTextureTypedIO(IntPtr, IntPtr, IntPtr, bool, string, out int, out int) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadGPUTextureTypedIO must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadGPUTextureTypedIO must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadGPUTextureTyped_IO", libraryImport.EntryPoint, "Image.LoadGPUTextureTypedIO must bind IMG_LoadGPUTextureTyped_IO.");

        IntPtr texture = SDL3.Image.LoadGPUTextureTypedIO(IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, closeio: false, file: "BMP", out int width, out int height);

        TestAssert.Equal(IntPtr.Zero, texture, "Image.LoadGPUTextureTypedIO must return IntPtr.Zero without a GPU context or stream.");
        TestAssert.True(width >= 0, "Image.LoadGPUTextureTypedIO width out parameter must be initialized.");
        TestAssert.True(height >= 0, "Image.LoadGPUTextureTypedIO height out parameter must be initialized.");
    }

    public static void GetClipboardImage_IsCallableAndUsesExpectedEntryPoint()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.GetClipboardImage), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "Image.GetClipboardImage() method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.GetClipboardImage() must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.GetClipboardImage() must import from SDL3_image.");
        TestAssert.Equal("IMG_GetClipboardImage", libraryImport.EntryPoint, "Image.GetClipboardImage() must bind IMG_GetClipboardImage.");

        IntPtr surface = SDL3.Image.GetClipboardImage();
        SDL3.SDL.DestroySurface(surface);
    }

    public static void IsANI_DetectsAniHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsANI), "IMG_isANI", SDL3.Image.IsANI, AniHeaderBytes(), expectedForSample: true);
    }

    public static void IsAVIF_ReturnsFalseForUnknownData()
    {
        AssertDetector(nameof(SDL3.Image.IsAVIF), "IMG_isAVIF", SDL3.Image.IsAVIF, UnknownImageBytes(), expectedForSample: false);
    }

    public static void IsCUR_DetectsCurHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsCUR), "IMG_isCUR", SDL3.Image.IsCUR, CurHeaderBytes(), expectedForSample: true);
    }

    public static void IsBMP_DetectsBmpHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsBMP), "IMG_isBMP", SDL3.Image.IsBMP, CreateOnePixelBmp(), expectedForSample: true);
    }

    public static void IsGIF_DetectsGifHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsGIF), "IMG_isGIF", SDL3.Image.IsGIF, GifHeaderBytes(), expectedForSample: true);
    }

    public static void IsICO_DetectsIcoHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsICO), "IMG_isICO", SDL3.Image.IsICO, IcoHeaderBytes(), expectedForSample: true);
    }

    public static void IsJPG_DetectsJpegHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsJPG), "IMG_isJPG", SDL3.Image.IsJPG, JpegHeaderBytes(), expectedForSample: true);
    }

    public static void IsJXL_ReturnsFalseForUnknownData()
    {
        AssertDetector(nameof(SDL3.Image.IsJXL), "IMG_isJXL", SDL3.Image.IsJXL, UnknownImageBytes(), expectedForSample: false);
    }

    public static void IsLBM_DetectsLbmHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsLBM), "IMG_isLBM", SDL3.Image.IsLBM, LbmHeaderBytes(), expectedForSample: true);
    }

    public static void IsPCX_DetectsPcxHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsPCX), "IMG_isPCX", SDL3.Image.IsPCX, PcxHeaderBytes(), expectedForSample: true);
    }

    public static void IsPNG_DetectsPngHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsPNG), "IMG_isPNG", SDL3.Image.IsPNG, PngHeaderBytes(), expectedForSample: true);
    }

    public static void IsPNM_DetectsPnmHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsPNM), "IMG_isPNM", SDL3.Image.IsPNM, PnmHeaderBytes(), expectedForSample: true);
    }

    public static void IsQOI_DetectsQoiHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsQOI), "IMG_isQOI", SDL3.Image.IsQOI, QoiHeaderBytes(), expectedForSample: true);
    }

    public static void IsSVG_DetectsSvgTag()
    {
        AssertDetector(nameof(SDL3.Image.IsSVG), "IMG_isSVG", SDL3.Image.IsSVG, SvgBytes(), expectedForSample: true);
    }

    public static void IsTIF_DetectsTifHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsTIF), "IMG_isTIF", SDL3.Image.IsTIF, TifHeaderBytes(), expectedForSample: true);
    }

    public static void IsWEBP_DetectsWebpHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsWEBP), "IMG_isWEBP", SDL3.Image.IsWEBP, WebpHeaderBytes(), expectedForSample: true);
    }

    public static void IsXCF_DetectsXcfHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsXCF), "IMG_isXCF", SDL3.Image.IsXCF, XcfHeaderBytes(), expectedForSample: true);
    }

    public static void IsXPM_DetectsXpmHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsXPM), "IMG_isXPM", SDL3.Image.IsXPM, XpmHeaderBytes(), expectedForSample: true);
    }

    public static void IsXV_DetectsXvHeader()
    {
        AssertDetector(nameof(SDL3.Image.IsXV), "IMG_isXV", SDL3.Image.IsXV, XvHeaderBytes(), expectedForSample: true);
    }

    public static void LoadAVIFIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadAVIFIO), "IMG_LoadAVIF_IO", SDL3.Image.LoadAVIFIO);
    }

    public static void LoadBMPIO_ReturnsSurfaceForBmpStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadBMPIO), "IMG_LoadBMP_IO", SDL3.Image.LoadBMPIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(CreateOnePixelBmp());
        IntPtr surface = SDL3.Image.LoadBMPIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadBMPIO(IntPtr) must load a valid BMP stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadCURIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadCURIO), "IMG_LoadCUR_IO", SDL3.Image.LoadCURIO);
    }

    public static void LoadGIFIO_ReturnsSurfaceForGifStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadGIFIO), "IMG_LoadGIF_IO", SDL3.Image.LoadGIFIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        IntPtr surface = SDL3.Image.LoadGIFIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadGIFIO(IntPtr) must load a valid GIF stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadICOIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadICOIO), "IMG_LoadICO_IO", SDL3.Image.LoadICOIO);
    }

    public static void LoadJPGIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadJPGIO), "IMG_LoadJPG_IO", SDL3.Image.LoadJPGIO);
    }

    public static void LoadJXLIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadJXLIO), "IMG_LoadJXL_IO", SDL3.Image.LoadJXLIO);
    }

    public static void LoadLBMIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadLBMIO), "IMG_LoadLBM_IO", SDL3.Image.LoadLBMIO);
    }

    public static void LoadPCXIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadPCXIO), "IMG_LoadPCX_IO", SDL3.Image.LoadPCXIO);
    }

    public static void LoadPNGIO_ReturnsSurfaceForPngStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadPNGIO), "IMG_LoadPNG_IO", SDL3.Image.LoadPNGIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(SamplePng());
        IntPtr surface = SDL3.Image.LoadPNGIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadPNGIO(IntPtr) must load a valid PNG stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadPNMIO_ReturnsSurfaceForPnmStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadPNMIO), "IMG_LoadPNM_IO", SDL3.Image.LoadPNMIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(PnmHeaderBytes());
        IntPtr surface = SDL3.Image.LoadPNMIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadPNMIO(IntPtr) must load a valid PNM stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadSVGIO_ReturnsSurfaceForSvgStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadSVGIO), "IMG_LoadSVG_IO", SDL3.Image.LoadSVGIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(SvgBytes());
        IntPtr surface = SDL3.Image.LoadSVGIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadSVGIO(IntPtr) must load a valid SVG stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadSizedSVGIO_ReturnsSurfaceForSizedSvgStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.LoadSizedSVGIO), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(int), typeof(int)]);
        TestAssert.NotNull(method, "Image.LoadSizedSVGIO(IntPtr, int, int) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, "Image.LoadSizedSVGIO(IntPtr, int, int) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, "Image.LoadSizedSVGIO(IntPtr, int, int) must import from SDL3_image.");
        TestAssert.Equal("IMG_LoadSizedSVG_IO", libraryImport.EntryPoint, "Image.LoadSizedSVGIO(IntPtr, int, int) must bind IMG_LoadSizedSVG_IO.");

        IntPtr nullSurface = SDL3.Image.LoadSizedSVGIO(IntPtr.Zero, width: 2, height: 2);
        TestAssert.Equal(IntPtr.Zero, nullSurface, "Image.LoadSizedSVGIO(IntPtr, int, int) must return IntPtr.Zero for a null stream.");

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(SvgBytes());
        IntPtr surface = SDL3.Image.LoadSizedSVGIO(stream.Handle, width: 2, height: 2);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadSizedSVGIO(IntPtr, int, int) must load a valid SVG stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadQOIIO_ReturnsSurfaceForQoiStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadQOIIO), "IMG_LoadQOI_IO", SDL3.Image.LoadQOIIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelQoi());
        IntPtr surface = SDL3.Image.LoadQOIIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadQOIIO(IntPtr) must load a valid QOI stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadTGAIO_ReturnsSurfaceForTgaStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadTGAIO), "IMG_LoadTGA_IO", SDL3.Image.LoadTGAIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelTga());
        IntPtr surface = SDL3.Image.LoadTGAIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadTGAIO(IntPtr) must load a valid TGA stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void LoadTIFIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadTIFIO), "IMG_LoadTIF_IO", SDL3.Image.LoadTIFIO);
    }

    public static void LoadWEBPIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadWEBPIO), "IMG_LoadWEBP_IO", SDL3.Image.LoadWEBPIO);
    }

    public static void LoadXCFIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadXCFIO), "IMG_LoadXCF_IO", SDL3.Image.LoadXCFIO);
    }

    public static void LoadXPMIO_ReturnsNullForNullStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadXPMIO), "IMG_LoadXPM_IO", SDL3.Image.LoadXPMIO);
    }

    public static void LoadXVIO_ReturnsSurfaceForXvStream()
    {
        AssertSurfaceLoaderMetadataAndNullStream(nameof(SDL3.Image.LoadXVIO), "IMG_LoadXV_IO", SDL3.Image.LoadXVIO);
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelXv());
        IntPtr surface = SDL3.Image.LoadXVIO(stream.Handle);
        TestAssert.True(surface != IntPtr.Zero, "Image.LoadXVIO(IntPtr) must load a valid XV stream.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void ReadXPMFromArray_ReturnsSurfaceForValidXpmArray()
    {
        AssertXpmArrayLoaderMetadata(nameof(SDL3.Image.ReadXPMFromArray), "IMG_ReadXPMFromArray");

        IntPtr surface = SDL3.Image.ReadXPMFromArray(OnePixelXpmArray());
        TestAssert.True(surface != IntPtr.Zero, "Image.ReadXPMFromArray(string[]) must load a valid XPM array.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void ReadXPMFromArrayToRGB888_ReturnsSurfaceForValidXpmArray()
    {
        AssertXpmArrayLoaderMetadata(nameof(SDL3.Image.ReadXPMFromArrayToRGB888), "IMG_ReadXPMFromArrayToRGB888");

        IntPtr surface = SDL3.Image.ReadXPMFromArrayToRGB888(OnePixelXpmArray());
        TestAssert.True(surface != IntPtr.Zero, "Image.ReadXPMFromArrayToRGB888(string[]) must load a valid XPM array.");
        SDL3.SDL.DestroySurface(surface);
    }

    public static void Save_ReturnsTrueForBmpFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.Save), "IMG_Save");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".bmp");

        try
        {
            bool saved = SDL3.Image.Save(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.Save(IntPtr, string) must save a BMP file when the extension is .bmp.");
            AssertNonEmptyFile(tempPath, "Image.Save(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveTypedIO_ReturnsTrueForBmpStream()
    {
        AssertSaveTypedIOMetadata(nameof(SDL3.Image.SaveTypedIO), "IMG_SaveTyped_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".bmp");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveTypedIO(surface.Handle, io, closeio: true, type: "BMP");
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveTypedIO(IntPtr, IntPtr, bool, string) must save BMP data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveTypedIO(IntPtr, IntPtr, bool, string) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveAVIF_ReturnsFalseForNullSurface()
    {
        AssertSaveFileIntQualityMetadata(nameof(SDL3.Image.SaveAVIF), "IMG_SaveAVIF");
        string tempPath = CreateTempImagePath(".avif");

        try
        {
            bool saved = SDL3.Image.SaveAVIF(IntPtr.Zero, tempPath, quality: 90);
            TestAssert.Equal(false, saved, "Image.SaveAVIF(IntPtr, string, int) must fail safely for a null surface.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveAVIFIO_ReturnsFalseForNullSurface()
    {
        AssertSaveIOIntQualityMetadata(nameof(SDL3.Image.SaveAVIFIO), "IMG_SaveAVIF_IO");
        string tempPath = CreateTempImagePath(".avif");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveAVIFIO(IntPtr.Zero, io, closeio: false, quality: 90);
            TestAssert.Equal(false, saved, "Image.SaveAVIFIO(IntPtr, IntPtr, bool, int) must fail safely for a null surface.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveBMP_ReturnsTrueForBmpFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveBMP), "IMG_SaveBMP");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".bmp");

        try
        {
            bool saved = SDL3.Image.SaveBMP(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveBMP(IntPtr, string) must save a BMP file.");
            AssertNonEmptyFile(tempPath, "Image.SaveBMP(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveBMPIO_ReturnsTrueForBmpStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveBMPIO), "IMG_SaveBMP_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".bmp");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveBMPIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveBMPIO(IntPtr, IntPtr, bool) must save BMP data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveBMPIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveCUR_ReturnsTrueForCurFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveCUR), "IMG_SaveCUR");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".cur");

        try
        {
            bool saved = SDL3.Image.SaveCUR(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveCUR(IntPtr, string) must save a CUR file.");
            AssertNonEmptyFile(tempPath, "Image.SaveCUR(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveCURIO_ReturnsTrueForCurStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveCURIO), "IMG_SaveCUR_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".cur");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveCURIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveCURIO(IntPtr, IntPtr, bool) must save CUR data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveCURIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveGIF_ReturnsTrueForGifFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveGIF), "IMG_SaveGIF");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".gif");

        try
        {
            bool saved = SDL3.Image.SaveGIF(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveGIF(IntPtr, string) must save a GIF file.");
            AssertNonEmptyFile(tempPath, "Image.SaveGIF(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveGIFIO_ReturnsTrueForGifStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveGIFIO), "IMG_SaveGIF_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".gif");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveGIFIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveGIFIO(IntPtr, IntPtr, bool) must save GIF data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveGIFIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveICO_ReturnsTrueForIcoFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveICO), "IMG_SaveICO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".ico");

        try
        {
            bool saved = SDL3.Image.SaveICO(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveICO(IntPtr, string) must save an ICO file.");
            AssertNonEmptyFile(tempPath, "Image.SaveICO(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveICOIO_ReturnsTrueForIcoStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveICOIO), "IMG_SaveICO_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".ico");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveICOIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveICOIO(IntPtr, IntPtr, bool) must save ICO data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveICOIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveJPG_ReturnsTrueForJpgFile()
    {
        AssertSaveFileIntQualityMetadata(nameof(SDL3.Image.SaveJPG), "IMG_SaveJPG");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".jpg");

        try
        {
            bool saved = SDL3.Image.SaveJPG(surface.Handle, tempPath, quality: 90);
            TestAssert.True(saved, "Image.SaveJPG(IntPtr, string, int) must save a JPEG file.");
            AssertNonEmptyFile(tempPath, "Image.SaveJPG(IntPtr, string, int) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveJPGIO_ReturnsTrueForJpgStream()
    {
        AssertSaveIOIntQualityMetadata(nameof(SDL3.Image.SaveJPGIO), "IMG_SaveJPG_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".jpg");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveJPGIO(surface.Handle, io, closeio: true, quality: 90);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveJPGIO(IntPtr, IntPtr, bool, int) must save JPEG data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveJPGIO(IntPtr, IntPtr, bool, int) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SavePNG_ReturnsTrueForPngFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SavePNG), "IMG_SavePNG");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".png");

        try
        {
            bool saved = SDL3.Image.SavePNG(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SavePNG(IntPtr, string) must save a PNG file.");
            AssertNonEmptyFile(tempPath, "Image.SavePNG(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SavePNGIO_ReturnsTrueForPngStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SavePNGIO), "IMG_SavePNG_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".png");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SavePNGIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SavePNGIO(IntPtr, IntPtr, bool) must save PNG data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SavePNGIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveTGA_ReturnsTrueForTgaFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveTGA), "IMG_SaveTGA");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".tga");

        try
        {
            bool saved = SDL3.Image.SaveTGA(surface.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveTGA(IntPtr, string) must save a TGA file.");
            AssertNonEmptyFile(tempPath, "Image.SaveTGA(IntPtr, string) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveTGAIO_ReturnsTrueForTgaStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveTGAIO), "IMG_SaveTGA_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".tga");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveTGAIO(surface.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveTGAIO(IntPtr, IntPtr, bool) must save TGA data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveTGAIO(IntPtr, IntPtr, bool) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveWEBP_ReturnsTrueForWebpFile()
    {
        AssertSaveFileFloatQualityMetadata(nameof(SDL3.Image.SaveWEBP), "IMG_SaveWEBP");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".webp");

        try
        {
            bool saved = SDL3.Image.SaveWEBP(surface.Handle, tempPath, quality: 90.0f);
            TestAssert.True(saved, "Image.SaveWEBP(IntPtr, string, float) must save a WEBP file.");
            AssertNonEmptyFile(tempPath, "Image.SaveWEBP(IntPtr, string, float) must write image data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveWEBPIO_ReturnsTrueForWebpStream()
    {
        AssertSaveIOFloatQualityMetadata(nameof(SDL3.Image.SaveWEBPIO), "IMG_SaveWEBP_IO");
        using TestSurfaceScope surface = TestSurfaceScope.Create();
        string tempPath = CreateTempImagePath(".webp");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveWEBPIO(surface.Handle, io, closeio: true, quality: 90.0f);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveWEBPIO(IntPtr, IntPtr, bool, float) must save WEBP data to an SDL_IOStream.");
            AssertNonEmptyFile(tempPath, "Image.SaveWEBPIO(IntPtr, IntPtr, bool, float) must write image data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void LoadAnimation_ReturnsAnimationForGifFile()
    {
        AssertAnimationFileLoaderMetadata(nameof(SDL3.Image.LoadAnimation), "IMG_LoadAnimation");
        string tempPath = CreateTempImagePath(".gif");
        File.WriteAllBytes(tempPath, OnePixelGif());

        IntPtr animation = IntPtr.Zero;

        try
        {
            animation = SDL3.Image.LoadAnimation(tempPath);
            TestAssert.True(animation != IntPtr.Zero, "Image.LoadAnimation(string) must load a valid GIF animation file.");
        }
        finally
        {
            FreeAnimationIfNeeded(animation);
            File.Delete(tempPath);
        }
    }

    public static void LoadAnimationIO_ReturnsAnimationForGifStream()
    {
        AssertAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadAnimationIO), "IMG_LoadAnimation_IO");
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        IntPtr animation = SDL3.Image.LoadAnimationIO(stream.Handle, closeio: false);
        TestAssert.True(animation != IntPtr.Zero, "Image.LoadAnimationIO(IntPtr, bool) must load a valid GIF animation stream.");
        SDL3.Image.FreeAnimation(animation);
    }

    public static void LoadAnimationTypedIO_ReturnsAnimationForGifStream()
    {
        AssertAnimationTypedIOLoaderMetadata(nameof(SDL3.Image.LoadAnimationTypedIO), "IMG_LoadAnimationTyped_IO");
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        IntPtr animation = SDL3.Image.LoadAnimationTypedIO(stream.Handle, closeio: false, type: "GIF");
        TestAssert.True(animation != IntPtr.Zero, "Image.LoadAnimationTypedIO(IntPtr, bool, string) must load a valid GIF animation stream.");
        SDL3.Image.FreeAnimation(animation);
    }

    public static void LoadANIAnimationIO_ReturnsNullForNullStream()
    {
        AssertDirectAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadANIAnimationIO), "IMG_LoadANIAnimation_IO");
        IntPtr animation = SDL3.Image.LoadANIAnimationIO(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, animation, "Image.LoadANIAnimationIO(IntPtr) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadAPNGAnimationIO_ReturnsNullForNullStream()
    {
        AssertDirectAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadAPNGAnimationIO), "IMG_LoadAPNGAnimation_IO");
        IntPtr animation = SDL3.Image.LoadAPNGAnimationIO(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, animation, "Image.LoadAPNGAnimationIO(IntPtr) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadAVIFAnimationIO_ReturnsNullForNullStream()
    {
        AssertDirectAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadAVIFAnimationIO), "IMG_LoadAVIFAnimation_IO");
        IntPtr animation = SDL3.Image.LoadAVIFAnimationIO(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, animation, "Image.LoadAVIFAnimationIO(IntPtr) must return IntPtr.Zero for a null stream.");
    }

    public static void LoadGIFAnimationIO_ReturnsAnimationForGifStream()
    {
        AssertDirectAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadGIFAnimationIO), "IMG_LoadGIFAnimation_IO");
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        IntPtr animation = SDL3.Image.LoadGIFAnimationIO(stream.Handle);
        TestAssert.True(animation != IntPtr.Zero, "Image.LoadGIFAnimationIO(IntPtr) must load a valid GIF animation stream.");
        SDL3.Image.FreeAnimation(animation);
    }

    public static void LoadWEBPAnimationIO_ReturnsNullForNullStream()
    {
        AssertDirectAnimationIOLoaderMetadata(nameof(SDL3.Image.LoadWEBPAnimationIO), "IMG_LoadWEBPAnimation_IO");
        IntPtr animation = SDL3.Image.LoadWEBPAnimationIO(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, animation, "Image.LoadWEBPAnimationIO(IntPtr) must return IntPtr.Zero for a null stream.");
    }

    public static void SaveAnimation_ReturnsTrueForGifFile()
    {
        AssertSaveFileMetadata(nameof(SDL3.Image.SaveAnimation), "IMG_SaveAnimation");
        using AnimationScope animation = AnimationScope.FromGifBytes();
        string tempPath = CreateTempImagePath(".gif");

        try
        {
            bool saved = SDL3.Image.SaveAnimation(animation.Handle, tempPath);
            TestAssert.True(saved, "Image.SaveAnimation(IntPtr, string) must save a GIF animation file.");
            AssertNonEmptyFile(tempPath, "Image.SaveAnimation(IntPtr, string) must write animation data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void SaveAnimationTypedIO_ReturnsTrueForGifStream()
    {
        AssertSaveTypedIOMetadata(nameof(SDL3.Image.SaveAnimationTypedIO), "IMG_SaveAnimationTyped_IO");
        using AnimationScope animation = AnimationScope.FromGifBytes();
        string tempPath = CreateTempImagePath(".gif");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveAnimationTypedIO(animation.Handle, io, closeio: true, type: "GIF");
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveAnimationTypedIO(IntPtr, IntPtr, bool, string) must save GIF animation data.");
            AssertNonEmptyFile(tempPath, "Image.SaveAnimationTypedIO(IntPtr, IntPtr, bool, string) must write animation data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveANIAnimationIO_ReturnsFalseForNullAnimation()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveANIAnimationIO), "IMG_SaveANIAnimation_IO");
        bool saved = SDL3.Image.SaveANIAnimationIO(IntPtr.Zero, IntPtr.Zero, closeio: false);
        TestAssert.Equal(false, saved, "Image.SaveANIAnimationIO(IntPtr, IntPtr, bool) must fail safely for a null animation.");
    }

    public static void SaveAPNGAnimationIO_ReturnsFalseForNullAnimation()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveAPNGAnimationIO), "IMG_SaveAPNGAnimation_IO");
        bool saved = SDL3.Image.SaveAPNGAnimationIO(IntPtr.Zero, IntPtr.Zero, closeio: false);
        TestAssert.Equal(false, saved, "Image.SaveAPNGAnimationIO(IntPtr, IntPtr, bool) must fail safely for a null animation.");
    }

    public static void SaveAVIFAnimationIO_ReturnsFalseForNullAnimation()
    {
        AssertSaveIOIntQualityMetadata(nameof(SDL3.Image.SaveAVIFAnimationIO), "IMG_SaveAVIFAnimation_IO");
        bool saved = SDL3.Image.SaveAVIFAnimationIO(IntPtr.Zero, IntPtr.Zero, closeio: false, quality: 90);
        TestAssert.Equal(false, saved, "Image.SaveAVIFAnimationIO(IntPtr, IntPtr, bool, int) must fail safely for a null animation.");
    }

    public static void SaveGIFAnimationIO_ReturnsTrueForGifStream()
    {
        AssertSaveIOMetadata(nameof(SDL3.Image.SaveGIFAnimationIO), "IMG_SaveGIFAnimation_IO");
        using AnimationScope animation = AnimationScope.FromGifBytes();
        string tempPath = CreateTempImagePath(".gif");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            bool saved = SDL3.Image.SaveGIFAnimationIO(animation.Handle, io, closeio: true);
            io = IntPtr.Zero;

            TestAssert.True(saved, "Image.SaveGIFAnimationIO(IntPtr, IntPtr, bool) must save GIF animation data.");
            AssertNonEmptyFile(tempPath, "Image.SaveGIFAnimationIO(IntPtr, IntPtr, bool) must write animation data.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void SaveWEBPAnimationIO_ReturnsFalseForNullAnimation()
    {
        AssertSaveIOIntQualityMetadata(nameof(SDL3.Image.SaveWEBPAnimationIO), "IMG_SaveWEBPAnimation_IO");
        bool saved = SDL3.Image.SaveWEBPAnimationIO(IntPtr.Zero, IntPtr.Zero, closeio: false, quality: 90);
        TestAssert.Equal(false, saved, "Image.SaveWEBPAnimationIO(IntPtr, IntPtr, bool, int) must fail safely for a null animation.");
    }

    public static void CreateAnimatedCursor_ReturnsNullForNullAnimation()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.CreateAnimatedCursor), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(int), typeof(int)]);
        TestAssert.NotNull(method, "Image.CreateAnimatedCursor(IntPtr, int, int) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_CreateAnimatedCursor");

        IntPtr cursor = SDL3.Image.CreateAnimatedCursor(IntPtr.Zero, hotX: 0, hotY: 0);
        TestAssert.Equal(IntPtr.Zero, cursor, "Image.CreateAnimatedCursor(IntPtr, int, int) must return IntPtr.Zero for a null animation.");
    }

    public static void FreeAnimation_AcceptsNullAnimation()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.FreeAnimation), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Image.FreeAnimation(IntPtr) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_FreeAnimation");

        SDL3.Image.FreeAnimation(IntPtr.Zero);
    }

    public static void CreateAnimationEncoder_ReturnsEncoderForGifFile()
    {
        AssertAnimationFileLoaderMetadata(nameof(SDL3.Image.CreateAnimationEncoder), "IMG_CreateAnimationEncoder");
        string tempPath = CreateTempImagePath(".gif");

        try
        {
            using AnimationEncoderScope encoder = AnimationEncoderScope.CreateForGifFile(tempPath);
            TestAssert.True(encoder.Handle != IntPtr.Zero, "Image.CreateAnimationEncoder(string) must create a GIF encoder for a .gif path.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void CreateAnimationEncoderIO_ReturnsEncoderForGifStream()
    {
        AssertAnimationTypedIOLoaderMetadata(nameof(SDL3.Image.CreateAnimationEncoderIO), "IMG_CreateAnimationEncoder_IO");
        string tempPath = CreateTempImagePath(".gif");
        IntPtr io = IntPtr.Zero;

        try
        {
            io = OpenWriteIO(tempPath);
            using AnimationEncoderScope encoder = AnimationEncoderScope.CreateForGifIO(io);
            TestAssert.True(encoder.Handle != IntPtr.Zero, "Image.CreateAnimationEncoderIO(IntPtr, bool, string) must create a GIF encoder for a writable stream.");
        }
        finally
        {
            CloseIOIfNeeded(io);
            File.Delete(tempPath);
        }
    }

    public static void CreateAnimationEncoderWithProperties_ReturnsEncoderForGifFile()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.CreateAnimationEncoderWithProperties), BindingFlags.Public | BindingFlags.Static, [typeof(uint)]);
        TestAssert.NotNull(method, "Image.CreateAnimationEncoderWithProperties(uint) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_CreateAnimationEncoderWithProperties");

        string tempPath = CreateTempImagePath(".gif");

        try
        {
            using PropertiesScope props = PropertiesScope.Create();
            TestAssert.True(SDL3.SDL.SetStringProperty(props.Handle, SDL3.Image.Props.AnimationEncoderCreateFilenameString, tempPath), "SDL.SetStringProperty must set the encoder filename property.");
            TestAssert.True(SDL3.SDL.SetStringProperty(props.Handle, SDL3.Image.Props.AnimationEncoderCreateTypeString, "GIF"), "SDL.SetStringProperty must set the encoder type property.");

            IntPtr encoder = SDL3.Image.CreateAnimationEncoderWithProperties(props.Handle);
            using AnimationEncoderScope encoderScope = new(encoder);
            TestAssert.True(encoderScope.Handle != IntPtr.Zero, "Image.CreateAnimationEncoderWithProperties(uint) must create a GIF encoder from filename properties.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void AddAnimationEncoderFrame_ReturnsTrueForGifEncoder()
    {
        AssertBoolReturnMethodMetadata(nameof(SDL3.Image.AddAnimationEncoderFrame), "IMG_AddAnimationEncoderFrame", [typeof(IntPtr), typeof(IntPtr), typeof(UInt64)]);
        string tempPath = CreateTempImagePath(".gif");

        try
        {
            using AnimationEncoderScope encoder = AnimationEncoderScope.CreateForGifFile(tempPath);
            using TestSurfaceScope surface = TestSurfaceScope.Create();

            bool added = SDL3.Image.AddAnimationEncoderFrame(encoder.Handle, surface.Handle, duration: 100);
            TestAssert.True(added, "Image.AddAnimationEncoderFrame(IntPtr, IntPtr, UInt64) must add a valid surface frame to a GIF encoder.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void CloseAnimationEncoder_ReturnsTrueForGifEncoder()
    {
        AssertBoolReturnMethodMetadata(nameof(SDL3.Image.CloseAnimationEncoder), "IMG_CloseAnimationEncoder", [typeof(IntPtr)]);
        string tempPath = CreateTempImagePath(".gif");

        try
        {
            using AnimationEncoderScope encoder = AnimationEncoderScope.CreateForGifFile(tempPath);
            encoder.AddTestFrame();
            bool closed = encoder.Close();

            TestAssert.True(closed, "Image.CloseAnimationEncoder(IntPtr) must close a GIF encoder after a valid frame.");
            AssertNonEmptyFile(tempPath, "Image.CloseAnimationEncoder(IntPtr) must finish writing GIF data.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void CreateAnimationDecoder_ReturnsDecoderForGifFile()
    {
        AssertAnimationFileLoaderMetadata(nameof(SDL3.Image.CreateAnimationDecoder), "IMG_CreateAnimationDecoder");
        string tempPath = CreateTempImagePath(".gif");
        File.WriteAllBytes(tempPath, OnePixelGif());

        try
        {
            using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifFile(tempPath);
            TestAssert.True(decoder.Handle != IntPtr.Zero, "Image.CreateAnimationDecoder(string) must create a decoder for a valid GIF file.");
        }
        finally
        {
            File.Delete(tempPath);
        }
    }

    public static void CreateAnimationDecoderIO_ReturnsDecoderForGifStream()
    {
        AssertAnimationTypedIOLoaderMetadata(nameof(SDL3.Image.CreateAnimationDecoderIO), "IMG_CreateAnimationDecoder_IO");
        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifIO(stream.Handle);
        TestAssert.True(decoder.Handle != IntPtr.Zero, "Image.CreateAnimationDecoderIO(IntPtr, bool, string) must create a decoder for a valid GIF stream.");
    }

    public static void CreateAnimationDecoderWithProperties_ReturnsDecoderForGifStream()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.CreateAnimationDecoderWithProperties), BindingFlags.Public | BindingFlags.Static, [typeof(uint)]);
        TestAssert.NotNull(method, "Image.CreateAnimationDecoderWithProperties(uint) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_CreateAnimationDecoderWithProperties");

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using PropertiesScope props = PropertiesScope.Create();
        TestAssert.True(SDL3.SDL.SetPointerProperty(props.Handle, SDL3.Image.Props.AnimationDecoderCreateIOStreamPointer, stream.Handle), "SDL.SetPointerProperty must set the decoder stream property.");
        TestAssert.True(SDL3.SDL.SetStringProperty(props.Handle, SDL3.Image.Props.AnimationDecoderCreateTypeString, "GIF"), "SDL.SetStringProperty must set the decoder type property.");

        IntPtr decoder = SDL3.Image.CreateAnimationDecoderWithProperties(props.Handle);
        using AnimationDecoderScope decoderScope = new(decoder);
        TestAssert.True(decoderScope.Handle != IntPtr.Zero, "Image.CreateAnimationDecoderWithProperties(uint) must create a decoder from stream properties.");
    }

    public static void GetAnimationDecoderProperties_ReturnsPropertiesForGifDecoder()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.GetAnimationDecoderProperties), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Image.GetAnimationDecoderProperties(IntPtr) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_GetAnimationDecoderProperties");

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifIO(stream.Handle);
        uint properties = SDL3.Image.GetAnimationDecoderProperties(decoder.Handle);
        TestAssert.True(properties != 0, "Image.GetAnimationDecoderProperties(IntPtr) must return decoder properties for a valid decoder.");
    }

    public static void GetAnimationDecoderFrame_ReturnsSurfaceForGifDecoder()
    {
        AssertBoolReturnMethodMetadata(nameof(SDL3.Image.GetAnimationDecoderFrame), "IMG_GetAnimationDecoderFrame", [typeof(IntPtr), typeof(IntPtr).MakeByRefType(), typeof(long)]);

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifIO(stream.Handle);
        bool gotFrame = SDL3.Image.GetAnimationDecoderFrame(decoder.Handle, out IntPtr frame, duration: 0);

        try
        {
            TestAssert.True(gotFrame, "Image.GetAnimationDecoderFrame(IntPtr, out IntPtr, long) must decode the first GIF frame.");
            TestAssert.True(frame != IntPtr.Zero, "Image.GetAnimationDecoderFrame(IntPtr, out IntPtr, long) must return a surface for the first GIF frame.");
        }
        finally
        {
            if (frame != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(frame);
            }
        }
    }

    public static void GetAnimationDecoderStatus_ReturnsInvalidForNullDecoder()
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(nameof(SDL3.Image.GetAnimationDecoderStatus), BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, "Image.GetAnimationDecoderStatus(IntPtr) method must be public static.");
        AssertImageLibraryImport(method!, "IMG_GetAnimationDecoderStatus");

        SDL3.Image.AnimationDecoderStatus status = SDL3.Image.GetAnimationDecoderStatus(IntPtr.Zero);
        TestAssert.Equal(SDL3.Image.AnimationDecoderStatus.Invalid, status, "Image.GetAnimationDecoderStatus(IntPtr) must return Invalid for a null decoder.");
    }

    public static void ResetAnimationDecoder_ReturnsTrueForGifDecoder()
    {
        AssertBoolReturnMethodMetadata(nameof(SDL3.Image.ResetAnimationDecoder), "IMG_ResetAnimationDecoder", [typeof(IntPtr)]);

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifIO(stream.Handle);
        bool reset = SDL3.Image.ResetAnimationDecoder(decoder.Handle);
        TestAssert.True(reset, "Image.ResetAnimationDecoder(IntPtr) must reset a valid GIF decoder.");
    }

    public static void CloseAnimationDecoder_ReturnsTrueForGifDecoder()
    {
        AssertBoolReturnMethodMetadata(nameof(SDL3.Image.CloseAnimationDecoder), "IMG_CloseAnimationDecoder", [typeof(IntPtr)]);

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
        using AnimationDecoderScope decoder = AnimationDecoderScope.CreateForGifIO(stream.Handle);
        bool closed = decoder.Close();
        TestAssert.True(closed, "Image.CloseAnimationDecoder(IntPtr) must close a valid GIF decoder.");
    }

    private static byte[] CreateOnePixelBmp()
    {
        return
        [
            0x42, 0x4D,
            0x3A, 0x00, 0x00, 0x00,
            0x00, 0x00,
            0x00, 0x00,
            0x36, 0x00, 0x00, 0x00,
            0x28, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00,
            0x01, 0x00,
            0x18, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00,
            0x13, 0x0B, 0x00, 0x00,
            0x13, 0x0B, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0xFF, 0x00
        ];
    }

    private static void AssertDetector(string methodName, string entryPoint, Func<IntPtr, bool> detector, byte[] sample, bool expectedForSample)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"Image.{methodName}(IntPtr) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, $"Image.{methodName}(IntPtr) must import from SDL3_image.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"Image.{methodName}(IntPtr) must bind {entryPoint}.");

        TestAssert.Equal(false, detector(IntPtr.Zero), $"Image.{methodName}(IntPtr) must return false for a null stream.");

        using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(sample);
        bool detected = detector(stream.Handle);
        TestAssert.Equal(expectedForSample, detected, $"Image.{methodName}(IntPtr) returned an unexpected result for the sample data.");
    }

    private static void AssertSurfaceLoaderMetadataAndNullStream(string methodName, string entryPoint, Func<IntPtr, IntPtr> loader)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"Image.{methodName}(IntPtr) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, $"Image.{methodName}(IntPtr) must import from SDL3_image.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"Image.{methodName}(IntPtr) must bind {entryPoint}.");

        IntPtr surface = loader(IntPtr.Zero);
        TestAssert.Equal(IntPtr.Zero, surface, $"Image.{methodName}(IntPtr) must return IntPtr.Zero for a null stream.");
    }

    private static void AssertXpmArrayLoaderMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(string[])]);
        TestAssert.NotNull(method, $"Image.{methodName}(string[]) method must be public static.");

        LibraryImportAttribute? libraryImport = method!.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"Image.{methodName}(string[]) must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, $"Image.{methodName}(string[]) must import from SDL3_image.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"Image.{methodName}(string[]) must bind {entryPoint}.");

        ParameterInfo parameter = method!.GetParameters()[0];
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"Image.{methodName}(string[]) parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"Image.{methodName}(string[]) parameter must use LPArray.");
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs.ArraySubType, $"Image.{methodName}(string[]) parameter elements must use UTF-8 strings.");
    }

    private static void AssertSaveFileMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, string) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertUtf8StringParameter(method!, parameterIndex: 1, $"Image.{methodName}(IntPtr, string) file parameter must use UTF-8 marshalling.");
    }

    private static void AssertSaveFileIntQualityMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(int)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, string, int) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertUtf8StringParameter(method!, parameterIndex: 1, $"Image.{methodName}(IntPtr, string, int) file parameter must use UTF-8 marshalling.");
    }

    private static void AssertSaveFileFloatQualityMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(string), typeof(float)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, string, float) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertUtf8StringParameter(method!, parameterIndex: 1, $"Image.{methodName}(IntPtr, string, float) file parameter must use UTF-8 marshalling.");
    }

    private static void AssertSaveIOMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, IntPtr, bool) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 2, $"Image.{methodName}(IntPtr, IntPtr, bool) closeio parameter must use I1 marshalling.");
    }

    private static void AssertSaveIOIntQualityMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(int)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, IntPtr, bool, int) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 2, $"Image.{methodName}(IntPtr, IntPtr, bool, int) closeio parameter must use I1 marshalling.");
    }

    private static void AssertSaveIOFloatQualityMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(float)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, IntPtr, bool, float) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 2, $"Image.{methodName}(IntPtr, IntPtr, bool, float) closeio parameter must use I1 marshalling.");
    }

    private static void AssertSaveTypedIOMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(IntPtr), typeof(bool), typeof(string)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, IntPtr, bool, string) method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 2, $"Image.{methodName}(IntPtr, IntPtr, bool, string) closeio parameter must use I1 marshalling.");
        AssertUtf8StringParameter(method!, parameterIndex: 3, $"Image.{methodName}(IntPtr, IntPtr, bool, string) type parameter must use UTF-8 marshalling.");
    }

    private static void AssertSaveMethodMetadata(MethodInfo method, string entryPoint)
    {
        AssertImageLibraryImport(method, entryPoint);

        MarshalAsAttribute? returnMarshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(returnMarshalAs, $"Image.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, returnMarshalAs!.Value, $"Image.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertAnimationFileLoaderMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(string)]);
        TestAssert.NotNull(method, $"Image.{methodName}(string) method must be public static.");
        AssertImageLibraryImport(method!, entryPoint);
        AssertUtf8StringParameter(method!, parameterIndex: 0, $"Image.{methodName}(string) file parameter must use UTF-8 marshalling.");
    }

    private static void AssertAnimationIOLoaderMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(bool)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, bool) method must be public static.");
        AssertImageLibraryImport(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 1, $"Image.{methodName}(IntPtr, bool) closeio parameter must use I1 marshalling.");
    }

    private static void AssertAnimationTypedIOLoaderMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr), typeof(bool), typeof(string)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr, bool, string) method must be public static.");
        AssertImageLibraryImport(method!, entryPoint);
        AssertI1BoolParameter(method!, parameterIndex: 1, $"Image.{methodName}(IntPtr, bool, string) closeio parameter must use I1 marshalling.");
        AssertUtf8StringParameter(method!, parameterIndex: 2, $"Image.{methodName}(IntPtr, bool, string) type parameter must use UTF-8 marshalling.");
    }

    private static void AssertDirectAnimationIOLoaderMetadata(string methodName, string entryPoint)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, [typeof(IntPtr)]);
        TestAssert.NotNull(method, $"Image.{methodName}(IntPtr) method must be public static.");
        AssertImageLibraryImport(method!, entryPoint);
    }

    private static void AssertImageLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"Image.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3_image", libraryImport!.LibraryName, $"Image.{method.Name} must import from SDL3_image.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"Image.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMethodMetadata(string methodName, string entryPoint, Type[] parameterTypes)
    {
        MethodInfo? method = typeof(SDL3.Image).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static, parameterTypes);
        TestAssert.NotNull(method, $"Image.{methodName} method must be public static.");
        AssertSaveMethodMetadata(method!, entryPoint);
    }

    private static void AssertUtf8StringParameter(MethodInfo method, int parameterIndex, string message)
    {
        ParameterInfo parameter = method.GetParameters()[parameterIndex];
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, message);
        TestAssert.Equal(UnmanagedType.LPUTF8Str, marshalAs!.Value, message);
    }

    private static void AssertI1BoolParameter(MethodInfo method, int parameterIndex, string message)
    {
        ParameterInfo parameter = method.GetParameters()[parameterIndex];
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, message);
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, message);
    }

    private static string CreateTempImagePath(string extension)
    {
        return Path.Combine(Path.GetTempPath(), $"sdl3-cs-image-save-{Guid.NewGuid():N}{extension}");
    }

    private static IntPtr OpenWriteIO(string path)
    {
        IntPtr io = SDL3.SDL.IOFromFile(path, "wb");
        TestAssert.True(io != IntPtr.Zero, "SDL.IOFromFile must open a writable temporary image file.");
        return io;
    }

    private static void CloseIOIfNeeded(IntPtr io)
    {
        if (io != IntPtr.Zero)
        {
            SDL3.SDL.CloseIO(io);
        }
    }

    private static void AssertNonEmptyFile(string path, string message)
    {
        TestAssert.True(new FileInfo(path).Length > 0, message);
    }

    private static void FreeAnimationIfNeeded(IntPtr animation)
    {
        if (animation != IntPtr.Zero)
        {
            SDL3.Image.FreeAnimation(animation);
        }
    }

    private static byte[] UnknownImageBytes()
    {
        return [0x53, 0x44, 0x4C, 0x33, 0x2D, 0x43, 0x53];
    }

    private static byte[] AniHeaderBytes()
    {
        return [0x52, 0x49, 0x46, 0x46, 0x04, 0x00, 0x00, 0x00, 0x41, 0x43, 0x4F, 0x4E];
    }

    private static byte[] CurHeaderBytes()
    {
        return CreateIcoOrCurBytes(type: 2);
    }

    private static byte[] GifHeaderBytes()
    {
        return [0x47, 0x49, 0x46, 0x38, 0x39, 0x61];
    }

    private static byte[] IcoHeaderBytes()
    {
        return CreateIcoOrCurBytes(type: 1);
    }

    private static byte[] JpegHeaderBytes()
    {
        return [0xFF, 0xD8, 0xFF, 0xDA, 0x00, 0x02];
    }

    private static byte[] LbmHeaderBytes()
    {
        return [0x46, 0x4F, 0x52, 0x4D, 0x00, 0x00, 0x00, 0x04, 0x49, 0x4C, 0x42, 0x4D];
    }

    private static byte[] PcxHeaderBytes()
    {
        byte[] bytes = new byte[128];
        bytes[0] = 10;
        bytes[1] = 5;
        bytes[2] = 1;
        return bytes;
    }

    private static byte[] PngHeaderBytes()
    {
        return [0x89, 0x50, 0x4E, 0x47, 0x0D, 0x0A, 0x1A, 0x0A];
    }

    private static byte[] PnmHeaderBytes()
    {
        return [0x50, 0x36, 0x0A, 0x31, 0x20, 0x31, 0x0A, 0x32, 0x35, 0x35, 0x0A, 0x00, 0x00, 0x00];
    }

    private static byte[] OnePixelGif()
    {
        return Convert.FromBase64String("R0lGODlhAQABAPAAAP///wAAACH5BAAAAAAALAAAAAABAAEAAAICRAEAOw==");
    }

    private static byte[] SamplePng()
    {
        return Convert.FromBase64String("iVBORw0KGgoAAAANSUhEUgAAABcAAAAqCAIAAADXrFeEAAADGUlEQVRIx21WSYIjMQiTZP//wZMC5sBiV9K3LBiDEJL5iCYE6aQLTrjogotOmBikCS446YSTIZhYkaQL20TPXzvOBBdDdLzyGhmCiyYGOxEZjP3ZCNHIILoc2sKrNJ7DPlVXRXRh+6KRHXqlIFwMwFaFBmlVF43ovhjk/reyc0aeOS10j8J9TSJoijyVMTsRavz6PFFAsPqdf+2gS190MoD92TOdA40JTs2kclhZXQBO+nS94OSuahORQUeMquX8YrzHf3UN7GfLiOjvdX9GkzGHG6PgK2MGb1vVf0ElOLLhigseyK3neJpdiUsVNgcYVTbi7pEX8S5Est5ti9PtubmvCsGQlIVJwWgcaZNO2M+mM1nEQJgUghVZEeRBoTkBVoqJ3FaUy++6yW412guFTm2v5dD+rBl+UStIXzTCk3K6/6ULA1yPErsXby4c1hWcGJrwtRZ3/H6WemZwIoAq5KKMM0yHynERzzoLX0S6SBzTaTdr905217a4bfWCkb4QwAF7uLvooC246AD4VjVyPws+Szh8vUTnqNcM6GxWqd9+pIQz5xdD//x60Jn7S4zvCe5nZZ/yUycONVWK98WXFxUV2xZHPkdNfoT+5I3jGTMTJboI4Rlx1oVRQfOjMoJRtWjk/rQ4T/NV/0BwYWH9GYQxQsWJbYuOIwiXPyS6cIz7FK1D3wzaT20qQ7X1Ubnuoepyu9YKHooOukeu32qY6od4+dRtFXQicXmNMOE8C3lnHLeodgrg49NBmuJ7X1s6f0auYtaCU/vZsGO98lvJefofDbNFR9RCqrxpW2/A7O6xm05XJslZVF7mn1q34eB9xosOr9H8Tehufz9S9O61dkW2nXw54+eB+QWNShlaqKoWNX1p3UgP6Myu66UR+5EwyCeLiFi8qjvXXHJ3E4q7FGyeN0Lw9eBJfTuvtt772YPUutygHw++Bfjls60ea9416bDzKuq4wOXN+p6IiY58ZlTG/Vnqsc8q4ej5MezzNLXrvkRgj7L3gbAlFBD3i/LL2OCXJe1nMZ34GJXoDCdtcah0+H7xM1j+8x/GScvIK2DrYAAAAABJRU5ErkJggg==");
    }

    private static byte[] OnePixelQoi()
    {
        return
        [
            0x71, 0x6F, 0x69, 0x66,
            0x00, 0x00, 0x00, 0x01,
            0x00, 0x00, 0x00, 0x01,
            0x04,
            0x00,
            0xFF, 0xFF, 0x00, 0x00, 0xFF,
            0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x01
        ];
    }

    private static byte[] OnePixelTga()
    {
        return
        [
            0x00,
            0x00,
            0x02,
            0x00, 0x00, 0x00, 0x00, 0x00,
            0x00, 0x00,
            0x00, 0x00,
            0x01, 0x00,
            0x01, 0x00,
            0x18,
            0x00,
            0x00, 0x00, 0xFF
        ];
    }

    private static byte[] OnePixelXv()
    {
        return
        [
            0x50, 0x37, 0x20, 0x33, 0x33, 0x32, 0x0A,
            0x23, 0x45, 0x4E, 0x44, 0x5F, 0x4F, 0x46, 0x5F, 0x43, 0x4F, 0x4D, 0x4D, 0x45, 0x4E, 0x54, 0x53, 0x0A,
            0x31, 0x20, 0x31, 0x0A,
            0x00
        ];
    }

    private static string[] OnePixelXpmArray()
    {
        return ["1 1 1 1", "a c #ffffff", "a", null!];
    }

    private static byte[] QoiHeaderBytes()
    {
        return [0x71, 0x6F, 0x69, 0x66];
    }

    private static byte[] SvgBytes()
    {
        return
        [
            0x3C, 0x73, 0x76, 0x67, 0x20,
            0x77, 0x69, 0x64, 0x74, 0x68, 0x3D, 0x22, 0x31, 0x22, 0x20,
            0x68, 0x65, 0x69, 0x67, 0x68, 0x74, 0x3D, 0x22, 0x31, 0x22,
            0x3E, 0x3C, 0x2F, 0x73, 0x76, 0x67, 0x3E
        ];
    }

    private static byte[] TifHeaderBytes()
    {
        return [0x49, 0x49, 0x2A, 0x00];
    }

    private static byte[] WebpHeaderBytes()
    {
        return [0x52, 0x49, 0x46, 0x46, 0x0C, 0x00, 0x00, 0x00, 0x57, 0x45, 0x42, 0x50, 0x56, 0x50, 0x38, 0x20, 0x00, 0x00, 0x00, 0x00];
    }

    private static byte[] XcfHeaderBytes()
    {
        return [0x67, 0x69, 0x6D, 0x70, 0x20, 0x78, 0x63, 0x66, 0x20, 0x76, 0x30, 0x30, 0x30, 0x00];
    }

    private static byte[] XpmHeaderBytes()
    {
        return [0x2F, 0x2A, 0x20, 0x58, 0x50, 0x4D, 0x20, 0x2A, 0x2F];
    }

    private static byte[] XvHeaderBytes()
    {
        return [0x50, 0x37, 0x20, 0x33, 0x33, 0x32, 0x0A, 0x23, 0x45, 0x4E, 0x44, 0x5F, 0x4F, 0x46, 0x5F, 0x43, 0x4F, 0x4D, 0x4D, 0x45, 0x4E, 0x54, 0x53, 0x0A, 0x31, 0x20, 0x31, 0x0A];
    }

    private static byte[] CreateIcoOrCurBytes(ushort type)
    {
        byte word4;
        byte word5;

        if (type == 1)
        {
            word4 = 0x01;
            word5 = 0x00;
        }
        else
        {
            word4 = 0x00;
            word5 = 0x00;
        }

        return
        [
            0x00, 0x00,
            (byte)type, 0x00,
            0x01, 0x00,
            0x01,
            0x01,
            0x00,
            0x00,
            word4, 0x00,
            word5, 0x00,
            0x30, 0x00, 0x00, 0x00,
            0x16, 0x00, 0x00, 0x00,
            0x28, 0x00, 0x00, 0x00,
            0x01, 0x00, 0x00, 0x00,
            0x02, 0x00, 0x00, 0x00,
            0x01, 0x00,
            0x20, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x04, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0x00, 0x00,
            0x00, 0x00, 0xFF, 0xFF,
            0x00, 0x00, 0x00, 0x00
        ];
    }

    private sealed class SoftwareRendererScope : IDisposable
    {
        private IntPtr surface;

        private SoftwareRendererScope(IntPtr surface, IntPtr renderer)
        {
            this.surface = surface;
            Renderer = renderer;
        }

        public IntPtr Renderer { get; private set; }

        public static SoftwareRendererScope Create()
        {
            IntPtr surface = SDL3.SDL.CreateSurface(2, 2, SDL3.SDL.PixelFormat.RGBA8888);
            TestAssert.True(surface != IntPtr.Zero, "SDL.CreateSurface must create a surface for software renderer tests.");

            IntPtr renderer = SDL3.SDL.CreateSoftwareRenderer(surface);
            if (renderer == IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(surface);
                throw new InvalidOperationException("SDL.CreateSoftwareRenderer must create a renderer for texture tests.");
            }

            return new SoftwareRendererScope(surface, renderer);
        }

        public void Dispose()
        {
            if (Renderer != IntPtr.Zero)
            {
                SDL3.SDL.DestroyRenderer(Renderer);
                Renderer = IntPtr.Zero;
            }

            if (surface != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(surface);
                surface = IntPtr.Zero;
            }
        }
    }

    private sealed class TestSurfaceScope : IDisposable
    {
        private TestSurfaceScope(IntPtr surface)
        {
            Handle = surface;
        }

        public IntPtr Handle { get; private set; }

        public static TestSurfaceScope Create()
        {
            IntPtr surface = SDL3.SDL.CreateSurface(1, 1, SDL3.SDL.PixelFormat.RGB24);
            TestAssert.True(surface != IntPtr.Zero, "SDL.CreateSurface must create a surface for image save tests.");
            TestAssert.True(SDL3.SDL.FillSurfaceRect(surface, IntPtr.Zero, 0x000000FFu), "SDL.FillSurfaceRect must fill the image save test surface.");

            return new TestSurfaceScope(surface);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL3.SDL.DestroySurface(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }

    private sealed class AnimationScope : IDisposable
    {
        private AnimationScope(IntPtr animation)
        {
            Handle = animation;
        }

        public IntPtr Handle { get; private set; }

        public static AnimationScope FromGifBytes()
        {
            using ConstMemIOStreamScope stream = ConstMemIOStreamScope.Create(OnePixelGif());
            IntPtr animation = SDL3.Image.LoadGIFAnimationIO(stream.Handle);
            TestAssert.True(animation != IntPtr.Zero, "Image.LoadGIFAnimationIO(IntPtr) must create a reusable animation for animation save tests.");

            return new AnimationScope(animation);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL3.Image.FreeAnimation(Handle);
                Handle = IntPtr.Zero;
            }
        }
    }

    private sealed class AnimationEncoderScope : IDisposable
    {
        public AnimationEncoderScope(IntPtr encoder)
        {
            Handle = encoder;
        }

        public IntPtr Handle { get; private set; }

        public static AnimationEncoderScope CreateForGifFile(string path)
        {
            IntPtr encoder = SDL3.Image.CreateAnimationEncoder(path);
            TestAssert.True(encoder != IntPtr.Zero, "Image.CreateAnimationEncoder(string) must create a GIF encoder.");

            return new AnimationEncoderScope(encoder);
        }

        public static AnimationEncoderScope CreateForGifIO(IntPtr io)
        {
            IntPtr encoder = SDL3.Image.CreateAnimationEncoderIO(io, closeio: false, type: "GIF");
            TestAssert.True(encoder != IntPtr.Zero, "Image.CreateAnimationEncoderIO(IntPtr, bool, string) must create a GIF encoder.");

            return new AnimationEncoderScope(encoder);
        }

        public void AddTestFrame()
        {
            using TestSurfaceScope surface = TestSurfaceScope.Create();
            TestAssert.True(SDL3.Image.AddAnimationEncoderFrame(Handle, surface.Handle, duration: 100), "Image.AddAnimationEncoderFrame(IntPtr, IntPtr, UInt64) must add a test frame.");
        }

        public bool Close()
        {
            bool closed = SDL3.Image.CloseAnimationEncoder(Handle);
            Handle = IntPtr.Zero;
            return closed;
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                Close();
            }
        }
    }

    private sealed class AnimationDecoderScope : IDisposable
    {
        public AnimationDecoderScope(IntPtr decoder)
        {
            Handle = decoder;
        }

        public IntPtr Handle { get; private set; }

        public static AnimationDecoderScope CreateForGifFile(string path)
        {
            IntPtr decoder = SDL3.Image.CreateAnimationDecoder(path);
            TestAssert.True(decoder != IntPtr.Zero, "Image.CreateAnimationDecoder(string) must create a GIF decoder.");

            return new AnimationDecoderScope(decoder);
        }

        public static AnimationDecoderScope CreateForGifIO(IntPtr io)
        {
            IntPtr decoder = SDL3.Image.CreateAnimationDecoderIO(io, closeio: false, type: "GIF");
            TestAssert.True(decoder != IntPtr.Zero, "Image.CreateAnimationDecoderIO(IntPtr, bool, string) must create a GIF decoder.");

            return new AnimationDecoderScope(decoder);
        }

        public bool Close()
        {
            bool closed = SDL3.Image.CloseAnimationDecoder(Handle);
            Handle = IntPtr.Zero;
            return closed;
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                Close();
            }
        }
    }

    private sealed class PropertiesScope : IDisposable
    {
        private PropertiesScope(uint properties)
        {
            Handle = properties;
        }

        public uint Handle { get; private set; }

        public static PropertiesScope Create()
        {
            uint properties = SDL3.SDL.CreateProperties();
            TestAssert.True(properties != 0, "SDL.CreateProperties must create a properties object.");

            return new PropertiesScope(properties);
        }

        public void Dispose()
        {
            if (Handle != 0)
            {
                SDL3.SDL.DestroyProperties(Handle);
                Handle = 0;
            }
        }
    }

    private sealed class ConstMemIOStreamScope : IDisposable
    {
        private GCHandle handle;

        private ConstMemIOStreamScope(GCHandle handle, IntPtr io)
        {
            this.handle = handle;
            Handle = io;
        }

        public IntPtr Handle { get; private set; }

        public static ConstMemIOStreamScope Create(byte[] data)
        {
            GCHandle handle = GCHandle.Alloc(data, GCHandleType.Pinned);
            IntPtr io = SDL3.SDL.IOFromConstMem(handle.AddrOfPinnedObject(), (UIntPtr)data.Length);
            if (io == IntPtr.Zero)
            {
                handle.Free();
                throw new InvalidOperationException("SDL.IOFromConstMem must create a stream for image detector tests.");
            }

            return new ConstMemIOStreamScope(handle, io);
        }

        public void Dispose()
        {
            if (Handle != IntPtr.Zero)
            {
                SDL3.SDL.CloseIO(Handle);
                Handle = IntPtr.Zero;
            }

            if (handle.IsAllocated)
            {
                handle.Free();
            }
        }
    }
}
