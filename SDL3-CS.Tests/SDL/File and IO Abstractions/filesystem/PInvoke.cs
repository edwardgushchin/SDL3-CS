using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SDL3.Tests;

namespace SDL3.Tests.SDL.FileAndIOAbstractions.Filesystem;

internal static class PInvokeTests
{
    private static string? capturedPath;
    private static string? capturedOldPath;
    private static string? capturedNewPath;
    private static string? capturedOrg;
    private static string? capturedApp;
    private static string? capturedPattern;
    private static SDL3.SDL.Folder capturedFolder;
    private static SDL3.SDL.GlobFlags capturedGlobFlags;
    private static SDL3.SDL.EnumerateDirectoryCallback? capturedEnumerateCallback;
    private static IntPtr capturedUserdata;
    private static bool nextBool;
    private static IntPtr nextPointer;
    private static int nextCount;
    private static SDL3.SDL.PathInfo nextPathInfo;
    private static int capturedCallCount;

    public static void SDL_GetBasePath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetBasePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetBasePath");
    }

    public static void GetBasePath_ReturnsUtf8StringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetBasePathNativeFunction", nameof(CaptureGetBasePath));

        string? value = CaptureConstUtf8Path(SDL3.SDL.GetBasePath, "C:/app/");
        TestAssert.Equal("C:/app/", value, "SDL.GetBasePath must convert native UTF-8 path.");

        nextPointer = IntPtr.Zero;
        TestAssert.Equal<string?>(null, SDL3.SDL.GetBasePath(), "SDL.GetBasePath must return null for native null.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetBasePath must call native hook for both branches.");
    }

    public static void SDL_GetPrefPath_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPrefPath");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetPrefPath");
        AssertStringParameterMarshal(nativeMethod, "org", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "app", UnmanagedType.LPUTF8Str);
    }

    public static void GetPrefPath_ForwardsOrgAppReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetPrefPathNativeFunction", nameof(CaptureGetPrefPath));

        nextPointer = AllocateSdlUtf8("C:/Users/Test/AppData/");
        string? value = SDL3.SDL.GetPrefPath("Org", "Game");

        TestAssert.Equal("C:/Users/Test/AppData/", value, "SDL.GetPrefPath must convert native UTF-8 path.");
        TestAssert.Equal("Org", capturedOrg, "SDL.GetPrefPath must forward org.");
        TestAssert.Equal("Game", capturedApp, "SDL.GetPrefPath must forward app.");

        nextPointer = IntPtr.Zero;
        value = SDL3.SDL.GetPrefPath("Org2", "Game2");

        TestAssert.Equal<string?>(null, value, "SDL.GetPrefPath must return null for native null.");
        TestAssert.Equal("Org2", capturedOrg, "SDL.GetPrefPath must forward org for null branch.");
        TestAssert.Equal("Game2", capturedApp, "SDL.GetPrefPath must forward app for null branch.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetPrefPath must call native hook for both branches.");
    }

    public static void SDL_GetUserFolder_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetUserFolder");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetUserFolder");
    }

    public static void GetUserFolder_ForwardsFolderReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetUserFolderNativeFunction", nameof(CaptureGetUserFolder));

        string? value = CaptureConstUtf8Path(() => SDL3.SDL.GetUserFolder(SDL3.SDL.Folder.Documents), "C:/Users/Test/Documents/");
        TestAssert.Equal("C:/Users/Test/Documents/", value, "SDL.GetUserFolder must convert native UTF-8 path.");
        TestAssert.Equal(SDL3.SDL.Folder.Documents, capturedFolder, "SDL.GetUserFolder must forward folder.");

        nextPointer = IntPtr.Zero;
        value = SDL3.SDL.GetUserFolder(SDL3.SDL.Folder.Screenshots);

        TestAssert.Equal<string?>(null, value, "SDL.GetUserFolder must return null for native null.");
        TestAssert.Equal(SDL3.SDL.Folder.Screenshots, capturedFolder, "SDL.GetUserFolder must forward folder for null branch.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetUserFolder must call native hook for both branches.");
    }

    public static void CreateDirectory_ForwardsPathAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CreateDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_CreateDirectory");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("CreateDirectoryNativeFunction", nameof(CapturePathBool));

        bool result = SDL3.SDL.CreateDirectory("data/save");

        TestAssert.Equal(true, result, "SDL.CreateDirectory must return native bool value.");
        TestAssert.Equal("data/save", capturedPath, "SDL.CreateDirectory must forward path.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CreateDirectory must call native hook once.");
    }

    public static void EnumerateDirectory_ForwardsPathCallbackUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EnumerateDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_EnumerateDirectory");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);
        AssertCallbackCdecl(typeof(SDL3.SDL.EnumerateDirectoryCallback), "SDL.EnumerateDirectoryCallback");
        AssertCallbackStringParameterMarshal(typeof(SDL3.SDL.EnumerateDirectoryCallback), "dirname", UnmanagedType.LPUTF8Str);
        AssertCallbackStringParameterMarshal(typeof(SDL3.SDL.EnumerateDirectoryCallback), "fname", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.EnumerateDirectoryCallback callback = TestEnumerateDirectory;
        using NativeHookScope _ = NativeHookScope.Install("EnumerateDirectoryNativeFunction", nameof(CaptureEnumerateDirectory));

        bool result = SDL3.SDL.EnumerateDirectory("assets", callback, (IntPtr)123);

        TestAssert.Equal(false, result, "SDL.EnumerateDirectory must return native bool value.");
        TestAssert.Equal("assets", capturedPath, "SDL.EnumerateDirectory must forward path.");
        TestAssert.Equal((IntPtr)123, capturedUserdata, "SDL.EnumerateDirectory must forward userdata.");
        TestAssert.Equal(callback, capturedEnumerateCallback!, "SDL.EnumerateDirectory must forward callback.");
        TestAssert.Equal(SDL3.SDL.EnumerationResult.Success, capturedEnumerateCallback!((IntPtr)1, "assets/", "file.txt"), "SDL.EnumerateDirectory callback must remain callable.");
        TestAssert.Equal(1, capturedCallCount, "SDL.EnumerateDirectory must call native hook once.");
    }

    public static void RemovePath_ForwardsPathAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RemovePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_RemovePath");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("RemovePathNativeFunction", nameof(CapturePathBool));

        bool result = SDL3.SDL.RemovePath("old.tmp");

        TestAssert.Equal(true, result, "SDL.RemovePath must return native bool value.");
        TestAssert.Equal("old.tmp", capturedPath, "SDL.RemovePath must forward path.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RemovePath must call native hook once.");
    }

    public static void RenamePath_ForwardsPathsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_RenamePath");
        AssertSdlLibraryImport(nativeMethod, "SDL_RenamePath");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "oldpath", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "newpath", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("RenamePathNativeFunction", nameof(CaptureTwoPathBool));

        bool result = SDL3.SDL.RenamePath("old.sav", "new.sav");

        TestAssert.Equal(false, result, "SDL.RenamePath must return native bool value.");
        TestAssert.Equal("old.sav", capturedOldPath, "SDL.RenamePath must forward oldpath.");
        TestAssert.Equal("new.sav", capturedNewPath, "SDL.RenamePath must forward newpath.");
        TestAssert.Equal(1, capturedCallCount, "SDL.RenamePath must call native hook once.");
    }

    public static void CopyFile_ForwardsPathsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_CopyFile");
        AssertSdlLibraryImport(nativeMethod, "SDL_CopyFile");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "oldpath", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "newpath", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("CopyFileNativeFunction", nameof(CaptureTwoPathBool));

        bool result = SDL3.SDL.CopyFile("source.dat", "copy.dat");

        TestAssert.Equal(true, result, "SDL.CopyFile must return native bool value.");
        TestAssert.Equal("source.dat", capturedOldPath, "SDL.CopyFile must forward oldpath.");
        TestAssert.Equal("copy.dat", capturedNewPath, "SDL.CopyFile must forward newpath.");
        TestAssert.Equal(1, capturedCallCount, "SDL.CopyFile must call native hook once.");
    }

    public static void GetPathInfo_ForwardsPathAndReturnsInfo()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetPathInfo");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetPathInfo");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        nextPathInfo = new SDL3.SDL.PathInfo
        {
            Type = SDL3.SDL.PathType.File,
            Size = 1234,
            CreateTime = 10,
            ModifyTime = 20,
            AccessTime = 30
        };
        using NativeHookScope _ = NativeHookScope.Install("GetPathInfoNativeFunction", nameof(CaptureGetPathInfo));

        bool result = SDL3.SDL.GetPathInfo("save.dat", out SDL3.SDL.PathInfo info);

        TestAssert.Equal(true, result, "SDL.GetPathInfo must return native bool value.");
        TestAssert.Equal("save.dat", capturedPath, "SDL.GetPathInfo must forward path.");
        AssertPathInfo(nextPathInfo, info, "SDL.GetPathInfo");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetPathInfo must call native hook once.");
    }

    public static void SDL_GlobDirectory_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GlobDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_GlobDirectory");
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "pattern", UnmanagedType.LPUTF8Str);
    }

    public static void GlobDirectory_ForwardsArgumentsReturnsArrayAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GlobDirectoryNativeFunction", nameof(CaptureGlobDirectory));

        nextPointer = AllocateSdlStringPointerArray(["one.txt", "two.txt"], out nextCount);
        string[]? result = SDL3.SDL.GlobDirectory("assets", "*.txt", SDL3.SDL.GlobFlags.CaseInsensitive, out int count);

        TestAssert.NotNull(result, "SDL.GlobDirectory must convert a native string pointer array.");
        TestAssert.Equal(2, result!.Length, "SDL.GlobDirectory must preserve item count.");
        TestAssert.Equal("one.txt", result[0], "SDL.GlobDirectory must convert first result.");
        TestAssert.Equal("two.txt", result[1], "SDL.GlobDirectory must convert second result.");
        TestAssert.Equal(2, count, "SDL.GlobDirectory must forward native count.");
        TestAssert.Equal("assets", capturedPath, "SDL.GlobDirectory must forward path.");
        TestAssert.Equal("*.txt", capturedPattern, "SDL.GlobDirectory must forward pattern.");
        TestAssert.Equal(SDL3.SDL.GlobFlags.CaseInsensitive, capturedGlobFlags, "SDL.GlobDirectory must forward flags.");

        nextPointer = AllocateSdlStringPointerArray([], out nextCount);
        result = SDL3.SDL.GlobDirectory("empty", null, 0, out count);
        TestAssert.NotNull(result, "SDL.GlobDirectory must return an empty array for zero native count.");
        TestAssert.Equal(0, result!.Length, "SDL.GlobDirectory must preserve zero native count.");
        TestAssert.Equal(0, count, "SDL.GlobDirectory must return zero count.");
        TestAssert.Equal<string?>(null, capturedPattern, "SDL.GlobDirectory must forward null pattern.");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        result = SDL3.SDL.GlobDirectory("missing", null, 0, out count);
        TestAssert.Equal<string[]?>(null, result, "SDL.GlobDirectory must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GlobDirectory must keep native count for null branch.");
        TestAssert.Equal(3, capturedCallCount, "SDL.GlobDirectory must call native hook for all branches.");
    }

    public static void SDL_GetCurrentDirectory_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetCurrentDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetCurrentDirectory");
    }

    public static void GetCurrentDirectory_ReturnsStringAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GetCurrentDirectoryNativeFunction", nameof(CaptureGetCurrentDirectory));

        nextPointer = AllocateSdlUtf8("C:/work/");
        string? value = SDL3.SDL.GetCurrentDirectory();

        TestAssert.Equal("C:/work/", value, "SDL.GetCurrentDirectory must convert native UTF-8 path.");

        nextPointer = IntPtr.Zero;
        value = SDL3.SDL.GetCurrentDirectory();

        TestAssert.Equal<string?>(null, value, "SDL.GetCurrentDirectory must return null for native null.");
        TestAssert.Equal(2, capturedCallCount, "SDL.GetCurrentDirectory must call native hook for both branches.");
    }

    private static void ResetCaptureState()
    {
        capturedPath = null;
        capturedOldPath = null;
        capturedNewPath = null;
        capturedOrg = null;
        capturedApp = null;
        capturedPattern = null;
        capturedFolder = default;
        capturedGlobFlags = default;
        capturedEnumerateCallback = null;
        capturedUserdata = IntPtr.Zero;
        nextBool = false;
        nextPointer = IntPtr.Zero;
        nextCount = 0;
        nextPathInfo = default;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureGetBasePath()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetPrefPath(string org, string app)
    {
        capturedOrg = org;
        capturedApp = app;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetUserFolder(SDL3.SDL.Folder folder)
    {
        capturedFolder = folder;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CapturePathBool(string path)
    {
        capturedPath = path;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureEnumerateDirectory(string path, SDL3.SDL.EnumerateDirectoryCallback callback, IntPtr userdata)
    {
        capturedPath = path;
        capturedEnumerateCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureTwoPathBool(string oldpath, string newpath)
    {
        capturedOldPath = oldpath;
        capturedNewPath = newpath;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetPathInfo(string path, out SDL3.SDL.PathInfo info)
    {
        capturedPath = path;
        info = nextPathInfo;
        capturedCallCount++;
        return nextBool;
    }

    private static IntPtr CaptureGlobDirectory(string path, string? pattern, SDL3.SDL.GlobFlags flags, out int count)
    {
        capturedPath = path;
        capturedPattern = pattern;
        capturedGlobFlags = flags;
        count = nextCount;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureGetCurrentDirectory()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static SDL3.SDL.EnumerationResult TestEnumerateDirectory(IntPtr userdata, string dirname, string fname)
    {
        return SDL3.SDL.EnumerationResult.Success;
    }

    private static string? CaptureConstUtf8Path(Func<string?> action, string value)
    {
        IntPtr pointer = Marshal.StringToCoTaskMemUTF8(value);
        nextPointer = pointer;

        try
        {
            return action();
        }
        finally
        {
            Marshal.FreeCoTaskMem(pointer);
        }
    }

    private static IntPtr AllocateSdlUtf8(string value)
    {
        byte[] bytes = Encoding.UTF8.GetBytes(value + '\0');
        IntPtr pointer = SDL3.SDL.Malloc((UIntPtr)bytes.Length);
        TestAssert.True(pointer != IntPtr.Zero, "SDL.Malloc must allocate UTF-8 test memory.");
        Marshal.Copy(bytes, 0, pointer, bytes.Length);
        return pointer;
    }

    private static IntPtr AllocateSdlStringPointerArray(string[] values, out int count)
    {
        count = values.Length;
        byte[][] strings = values.Select(value => Encoding.UTF8.GetBytes(value + '\0')).ToArray();
        int pointerBytes = values.Length * IntPtr.Size;
        int stringBytes = strings.Sum(bytes => bytes.Length);
        IntPtr block = SDL3.SDL.Malloc((UIntPtr)(pointerBytes + stringBytes + 1));
        TestAssert.True(block != IntPtr.Zero, "SDL.Malloc must allocate glob array test memory.");

        int stringOffset = pointerBytes;
        for (int i = 0; i < strings.Length; i++)
        {
            IntPtr stringPointer = block + stringOffset;
            Marshal.WriteIntPtr(block, i * IntPtr.Size, stringPointer);
            Marshal.Copy(strings[i], 0, stringPointer, strings[i].Length);
            stringOffset += strings[i].Length;
        }

        return block;
    }

    private static void AssertPathInfo(SDL3.SDL.PathInfo expected, SDL3.SDL.PathInfo actual, string apiName)
    {
        TestAssert.Equal(expected.Type, actual.Type, $"{apiName} must write info.Type.");
        TestAssert.Equal(expected.Size, actual.Size, $"{apiName} must write info.Size.");
        TestAssert.Equal(expected.CreateTime, actual.CreateTime, $"{apiName} must write info.CreateTime.");
        TestAssert.Equal(expected.ModifyTime, actual.ModifyTime, $"{apiName} must write info.ModifyTime.");
        TestAssert.Equal(expected.AccessTime, actual.AccessTime, $"{apiName} must write info.AccessTime.");
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");

        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"SDL.{method.Name} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"SDL.{method.Name} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"SDL.{method.Name} must use cdecl calling convention.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method, UnmanagedType unmanagedType)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} return value must use expected bool marshalling.");
    }

    private static void AssertStringParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected string marshalling.");
    }

    private static void AssertCallbackCdecl(Type callbackType, string callbackName)
    {
        UnmanagedFunctionPointerAttribute? attribute = callbackType.GetCustomAttribute<UnmanagedFunctionPointerAttribute>();
        TestAssert.NotNull(attribute, $"{callbackName} must keep unmanaged function pointer metadata.");
        TestAssert.Equal(CallingConvention.Cdecl, attribute!.CallingConvention, $"{callbackName} must use cdecl calling convention.");
    }

    private static void AssertCallbackStringParameterMarshal(Type callbackType, string parameterName, UnmanagedType unmanagedType)
    {
        MethodInfo invoke = callbackType.GetMethod("Invoke")!;
        ParameterInfo parameter = invoke.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"{callbackType.Name}.{parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"{callbackType.Name}.{parameterName} must use expected string marshalling.");
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? originalValue;

        private NativeHookScope(FieldInfo field, object? originalValue)
        {
            this.field = field;
            this.originalValue = originalValue;
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL.{fieldName} native hook field must exist.");
            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"{methodName} hook method must exist.");
            object? originalValue = field!.GetValue(null);
            Delegate hook = Delegate.CreateDelegate(field.FieldType, method!);
            field.SetValue(null, hook);
            return new NativeHookScope(field, originalValue);
        }

        public void Dispose()
        {
            field.SetValue(null, originalValue);
        }
    }
}
