using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using SDL3.Tests;

namespace SDL3.Tests.SDL.FileAndIOAbstractions.Storage;

internal static class PInvokeTests
{
    private static string? capturedOverride;
    private static string? capturedOrg;
    private static string? capturedApp;
    private static string? capturedPath;
    private static string? capturedOldPath;
    private static string? capturedNewPath;
    private static string? capturedPattern;
    private static uint capturedProps;
    private static IntPtr capturedStorage;
    private static IntPtr capturedUserdata;
    private static IntPtr capturedBuffer;
    private static ulong capturedLength;
    private static SDL3.SDL.GlobFlags capturedFlags;
    private static SDL3.SDL.EnumerateDirectoryCallback? capturedCallback;
    private static SDL3.SDL.StorageInterface capturedInterface;
    private static SDL3.SDL.PathInfo nextPathInfo;
    private static IntPtr nextPointer;
    private static bool nextBool;
    private static ulong nextULong;
    private static int nextCount;
    private static int capturedCallCount;

    public static void RunAll()
    {
        OpenTitleStorage_ForwardsOverridePropsAndReturnsNativePointer();
        OpenUserStorage_ForwardsOrgAppPropsAndReturnsNativePointer();
        OpenFileStorage_ForwardsPathAndReturnsNativePointer();
        OpenStorage_ForwardsInterfaceUserdataAndReturnsNativePointer();
        CloseStorage_ForwardsStorageAndReturnsNativeValue();
        StorageReady_ForwardsStorageAndReturnsNativeValue();
        GetStorageFileSize_ForwardsPathAndWritesLength();
        ReadStorageFile_ForwardsPathDestinationLengthAndReturnsNativeValue();
        WriteStorageFile_ForwardsPathSourceLengthAndReturnsNativeValue();
        CreateStorageDirectory_ForwardsStoragePathAndReturnsNativeValue();
        EnumerateStorageDirectory_ForwardsPathCallbackUserdataAndReturnsNativeValue();
        RemoveStoragePath_ForwardsStoragePathAndReturnsNativeValue();
        RenameStoragePath_ForwardsStoragePathsAndReturnsNativeValue();
        CopyStorageFile_ForwardsStoragePathsAndReturnsNativeValue();
        GetStoragePathInfo_ForwardsPathAndWritesInfo();
        GetStorageSpaceRemaining_ForwardsStorageAndReturnsNativeValue();
        SDL_GlobStorageDirectory_UsesExpectedNativeMetadata();
        GlobStorageDirectory_ForwardsArgumentsReturnsArrayAndNull();
    }

    public static void OpenTitleStorage_ForwardsOverridePropsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenTitleStorage");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenTitleStorage");
        AssertStringParameterMarshal(nativeMethod, "override", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1001;
        using NativeHookScope _ = NativeHookScope.Install("OpenTitleStorageNativeFunction", nameof(CaptureOpenTitleStorage));

        IntPtr result = SDL3.SDL.OpenTitleStorage("title-root", 12);

        TestAssert.Equal((IntPtr)1001, result, "SDL.OpenTitleStorage must return native storage pointer.");
        TestAssert.Equal("title-root", capturedOverride, "SDL.OpenTitleStorage must forward override.");
        TestAssert.Equal(12u, capturedProps, "SDL.OpenTitleStorage must forward props.");

        result = SDL3.SDL.OpenTitleStorage(null, 13);
        TestAssert.Equal((IntPtr)1001, result, "SDL.OpenTitleStorage must allow null override.");
        TestAssert.Equal<string?>(null, capturedOverride, "SDL.OpenTitleStorage must forward null override.");
        TestAssert.Equal(13u, capturedProps, "SDL.OpenTitleStorage must forward props for null override.");
        TestAssert.Equal(2, capturedCallCount, "SDL.OpenTitleStorage must call native hook for both branches.");
    }

    public static void OpenUserStorage_ForwardsOrgAppPropsAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenUserStorage");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenUserStorage");
        AssertStringParameterMarshal(nativeMethod, "org", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "app", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1002;
        using NativeHookScope _ = NativeHookScope.Install("OpenUserStorageNativeFunction", nameof(CaptureOpenUserStorage));

        IntPtr result = SDL3.SDL.OpenUserStorage("Org", "Game", 14);

        TestAssert.Equal((IntPtr)1002, result, "SDL.OpenUserStorage must return native storage pointer.");
        TestAssert.Equal("Org", capturedOrg, "SDL.OpenUserStorage must forward org.");
        TestAssert.Equal("Game", capturedApp, "SDL.OpenUserStorage must forward app.");
        TestAssert.Equal(14u, capturedProps, "SDL.OpenUserStorage must forward props.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenUserStorage must call native hook once.");
    }

    public static void OpenFileStorage_ForwardsPathAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenFileStorage");
        AssertSdlLibraryImport(nativeMethod, "SDL_OpenFileStorage");
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1003;
        using NativeHookScope _ = NativeHookScope.Install("OpenFileStorageNativeFunction", nameof(CaptureOpenFileStorage));

        IntPtr result = SDL3.SDL.OpenFileStorage("C:/saves");

        TestAssert.Equal((IntPtr)1003, result, "SDL.OpenFileStorage must return native storage pointer.");
        TestAssert.Equal("C:/saves", capturedPath, "SDL.OpenFileStorage must forward path.");

        result = SDL3.SDL.OpenFileStorage(null);
        TestAssert.Equal((IntPtr)1003, result, "SDL.OpenFileStorage must allow null path.");
        TestAssert.Equal<string?>(null, capturedPath, "SDL.OpenFileStorage must forward null path.");
        TestAssert.Equal(2, capturedCallCount, "SDL.OpenFileStorage must call native hook for both branches.");
    }

    public static void OpenStorage_ForwardsInterfaceUserdataAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenStorage");
        AssertSdlDllImport(nativeMethod, "SDL_OpenStorage");
        AssertByRefParameter(nativeMethod, "iface");

        ResetCaptureState();
        nextPointer = (IntPtr)1004;
        using NativeHookScope _ = NativeHookScope.Install("OpenStorageNativeFunction", nameof(CaptureOpenStorage));

        SDL3.SDL.StorageInterface iface = new()
        {
            Version = 88,
            Close = TestClose,
            Ready = TestReady,
            Enumerate = TestEnumerate,
            Info = TestInfo,
            ReadFile = TestReadFile,
            WriteFile = TestWriteFile,
            Mkdir = TestMkdir,
            Remove = TestRemove,
            Rename = TestRename,
            Copy = TestCopy,
            SpaceRemaining = TestSpaceRemaining
        };
        IntPtr result = SDL3.SDL.OpenStorage(in iface, (IntPtr)44);

        TestAssert.Equal((IntPtr)1004, result, "SDL.OpenStorage must return native storage pointer.");
        TestAssert.Equal((IntPtr)44, capturedUserdata, "SDL.OpenStorage must forward userdata.");
        TestAssert.Equal(88u, capturedInterface.Version, "SDL.OpenStorage must forward iface.Version.");
        TestAssert.Equal<SDL3.SDL.StorageInterface.CloseDelegate?>(TestClose, capturedInterface.Close, "SDL.OpenStorage must forward iface.Close.");
        TestAssert.Equal<SDL3.SDL.StorageInterface.SpaceRemainingDelegate?>(TestSpaceRemaining, capturedInterface.SpaceRemaining, "SDL.OpenStorage must forward iface.SpaceRemaining.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenStorage must call native hook once.");
    }

    public static void CloseStorage_ForwardsStorageAndReturnsNativeValue()
    {
        AssertStorageBoolMethod("CloseStorage", "SDL_CloseStorage", "CloseStorageNativeFunction", true, (IntPtr)2001);
    }

    public static void StorageReady_ForwardsStorageAndReturnsNativeValue()
    {
        AssertStorageBoolMethod("StorageReady", "SDL_StorageReady", "StorageReadyNativeFunction", false, (IntPtr)2002);
    }

    public static void GetStorageFileSize_ForwardsPathAndWritesLength()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetStorageFileSize");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetStorageFileSize");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        nextULong = 4096;
        using NativeHookScope _ = NativeHookScope.Install("GetStorageFileSizeNativeFunction", nameof(CaptureGetStorageFileSize));

        bool result = SDL3.SDL.GetStorageFileSize((IntPtr)2003, "save.bin", out ulong length);

        TestAssert.Equal(true, result, "SDL.GetStorageFileSize must return native bool value.");
        TestAssert.Equal(4096UL, length, "SDL.GetStorageFileSize must write length.");
        AssertStoragePath((IntPtr)2003, "save.bin", "SDL.GetStorageFileSize");
    }

    public static void ReadStorageFile_ForwardsPathDestinationLengthAndReturnsNativeValue()
    {
        AssertStorageBufferMethod("ReadStorageFile", "SDL_ReadStorageFile", "ReadStorageFileNativeFunction", (IntPtr)2004, "in.dat", (IntPtr)2005, 32, true);
    }

    public static void WriteStorageFile_ForwardsPathSourceLengthAndReturnsNativeValue()
    {
        AssertStorageBufferMethod("WriteStorageFile", "SDL_WriteStorageFile", "WriteStorageFileNativeFunction", (IntPtr)2006, "out.dat", (IntPtr)2007, 64, false);
    }

    public static void CreateStorageDirectory_ForwardsStoragePathAndReturnsNativeValue()
    {
        AssertStoragePathBoolMethod("CreateStorageDirectory", "SDL_CreateStorageDirectory", "CreateStorageDirectoryNativeFunction", (IntPtr)2008, "profiles", true);
    }

    public static void EnumerateStorageDirectory_ForwardsPathCallbackUserdataAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_EnumerateStorageDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_EnumerateStorageDirectory");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        SDL3.SDL.EnumerateDirectoryCallback callback = TestEnumerateDirectory;
        using NativeHookScope _ = NativeHookScope.Install("EnumerateStorageDirectoryNativeFunction", nameof(CaptureEnumerateStorageDirectory));

        bool result = SDL3.SDL.EnumerateStorageDirectory((IntPtr)2009, "levels", callback, (IntPtr)2010);

        TestAssert.Equal(false, result, "SDL.EnumerateStorageDirectory must return native bool value.");
        AssertStoragePath((IntPtr)2009, "levels", "SDL.EnumerateStorageDirectory");
        TestAssert.Equal(callback, capturedCallback!, "SDL.EnumerateStorageDirectory must forward callback.");
        TestAssert.Equal((IntPtr)2010, capturedUserdata, "SDL.EnumerateStorageDirectory must forward userdata.");
        TestAssert.Equal(SDL3.SDL.EnumerationResult.Success, capturedCallback!((IntPtr)1, "levels/", "one.map"), "SDL.EnumerateStorageDirectory callback must remain callable.");
    }

    public static void RemoveStoragePath_ForwardsStoragePathAndReturnsNativeValue()
    {
        AssertStoragePathBoolMethod("RemoveStoragePath", "SDL_RemoveStoragePath", "RemoveStoragePathNativeFunction", (IntPtr)2011, "old.tmp", true);
    }

    public static void RenameStoragePath_ForwardsStoragePathsAndReturnsNativeValue()
    {
        AssertStorageTwoPathMethod("RenameStoragePath", "SDL_RenameStoragePath", "RenameStoragePathNativeFunction", (IntPtr)2012, "old.sav", "new.sav", false);
    }

    public static void CopyStorageFile_ForwardsStoragePathsAndReturnsNativeValue()
    {
        AssertStorageTwoPathMethod("CopyStorageFile", "SDL_CopyStorageFile", "CopyStorageFileNativeFunction", (IntPtr)2013, "source.dat", "copy.dat", true);
    }

    public static void GetStoragePathInfo_ForwardsPathAndWritesInfo()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetStoragePathInfo");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetStoragePathInfo");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = true;
        nextPathInfo = new SDL3.SDL.PathInfo
        {
            Type = SDL3.SDL.PathType.Directory,
            Size = 12,
            CreateTime = 34,
            ModifyTime = 56,
            AccessTime = 78
        };
        using NativeHookScope _ = NativeHookScope.Install("GetStoragePathInfoNativeFunction", nameof(CaptureGetStoragePathInfo));

        bool result = SDL3.SDL.GetStoragePathInfo((IntPtr)2014, "folder", out SDL3.SDL.PathInfo info);

        TestAssert.Equal(true, result, "SDL.GetStoragePathInfo must return native bool value.");
        AssertStoragePath((IntPtr)2014, "folder", "SDL.GetStoragePathInfo");
        AssertPathInfo(nextPathInfo, info, "SDL.GetStoragePathInfo");
    }

    public static void GetStorageSpaceRemaining_ForwardsStorageAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetStorageSpaceRemaining");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetStorageSpaceRemaining");

        ResetCaptureState();
        nextULong = 999;
        using NativeHookScope _ = NativeHookScope.Install("GetStorageSpaceRemainingNativeFunction", nameof(CaptureGetStorageSpaceRemaining));

        ulong result = SDL3.SDL.GetStorageSpaceRemaining((IntPtr)2015);

        TestAssert.Equal(999UL, result, "SDL.GetStorageSpaceRemaining must return native value.");
        TestAssert.Equal((IntPtr)2015, capturedStorage, "SDL.GetStorageSpaceRemaining must forward storage.");
        TestAssert.Equal(1, capturedCallCount, "SDL.GetStorageSpaceRemaining must call native hook once.");
    }

    public static void SDL_GlobStorageDirectory_UsesExpectedNativeMetadata()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GlobStorageDirectory");
        AssertSdlLibraryImport(nativeMethod, "SDL_GlobStorageDirectory");
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "pattern", UnmanagedType.LPUTF8Str);
    }

    public static void GlobStorageDirectory_ForwardsArgumentsReturnsArrayAndNull()
    {
        ResetCaptureState();
        using NativeHookScope _ = NativeHookScope.Install("GlobStorageDirectoryNativeFunction", nameof(CaptureGlobStorageDirectory));

        nextPointer = AllocateSdlStringPointerArray(["one.txt", "two.txt"], out nextCount);
        string[]? result = SDL3.SDL.GlobStorageDirectory((IntPtr)2016, "assets", "*.txt", SDL3.SDL.GlobFlags.CaseInsensitive, out int count);

        TestAssert.NotNull(result, "SDL.GlobStorageDirectory must convert a native string pointer array.");
        TestAssert.Equal(2, result!.Length, "SDL.GlobStorageDirectory must preserve item count.");
        TestAssert.Equal("one.txt", result[0], "SDL.GlobStorageDirectory must convert first result.");
        TestAssert.Equal("two.txt", result[1], "SDL.GlobStorageDirectory must convert second result.");
        TestAssert.Equal(2, count, "SDL.GlobStorageDirectory must forward native count.");
        AssertGlobArguments((IntPtr)2016, "assets", "*.txt", SDL3.SDL.GlobFlags.CaseInsensitive, "SDL.GlobStorageDirectory");

        nextPointer = AllocateSdlStringPointerArray([], out nextCount);
        result = SDL3.SDL.GlobStorageDirectory((IntPtr)2017, "empty", null, 0, out count);

        TestAssert.NotNull(result, "SDL.GlobStorageDirectory must return an empty array for zero native count.");
        TestAssert.Equal(0, result!.Length, "SDL.GlobStorageDirectory must preserve zero native count.");
        TestAssert.Equal(0, count, "SDL.GlobStorageDirectory must return zero count.");
        AssertGlobArguments((IntPtr)2017, "empty", null, 0, "SDL.GlobStorageDirectory empty branch");

        nextPointer = IntPtr.Zero;
        nextCount = 0;
        result = SDL3.SDL.GlobStorageDirectory((IntPtr)2018, "missing", null, 0, out count);

        TestAssert.Equal<string[]?>(null, result, "SDL.GlobStorageDirectory must return null for native null.");
        TestAssert.Equal(0, count, "SDL.GlobStorageDirectory must keep native count for null branch.");
        AssertGlobArguments((IntPtr)2018, "missing", null, 0, "SDL.GlobStorageDirectory null branch");
        TestAssert.Equal(3, capturedCallCount, "SDL.GlobStorageDirectory must call native hook for all branches.");
    }

    private static void ResetCaptureState()
    {
        capturedOverride = null;
        capturedOrg = null;
        capturedApp = null;
        capturedPath = null;
        capturedOldPath = null;
        capturedNewPath = null;
        capturedPattern = null;
        capturedProps = 0;
        capturedStorage = IntPtr.Zero;
        capturedUserdata = IntPtr.Zero;
        capturedBuffer = IntPtr.Zero;
        capturedLength = 0;
        capturedFlags = default;
        capturedCallback = null;
        capturedInterface = default;
        nextPathInfo = default;
        nextPointer = IntPtr.Zero;
        nextBool = false;
        nextULong = 0;
        nextCount = 0;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureOpenTitleStorage(string? @override, uint props)
    {
        capturedOverride = @override;
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureOpenUserStorage(string org, string app, uint props)
    {
        capturedOrg = org;
        capturedApp = app;
        capturedProps = props;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureOpenFileStorage(string? path)
    {
        capturedPath = path;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureOpenStorage(in SDL3.SDL.StorageInterface iface, IntPtr userdata)
    {
        capturedInterface = iface;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureStorageBool(IntPtr storage)
    {
        capturedStorage = storage;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStoragePathBool(IntPtr storage, string path)
    {
        capturedStorage = storage;
        capturedPath = path;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetStorageFileSize(IntPtr storage, string path, out ulong length)
    {
        capturedStorage = storage;
        capturedPath = path;
        length = nextULong;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStorageBuffer(IntPtr storage, string path, IntPtr buffer, ulong length)
    {
        capturedStorage = storage;
        capturedPath = path;
        capturedBuffer = buffer;
        capturedLength = length;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureEnumerateStorageDirectory(IntPtr storage, string path, SDL3.SDL.EnumerateDirectoryCallback callback, IntPtr userdata)
    {
        capturedStorage = storage;
        capturedPath = path;
        capturedCallback = callback;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureStorageTwoPathBool(IntPtr storage, string oldpath, string newpath)
    {
        capturedStorage = storage;
        capturedOldPath = oldpath;
        capturedNewPath = newpath;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureGetStoragePathInfo(IntPtr storage, string path, out SDL3.SDL.PathInfo info)
    {
        capturedStorage = storage;
        capturedPath = path;
        info = nextPathInfo;
        capturedCallCount++;
        return nextBool;
    }

    private static ulong CaptureGetStorageSpaceRemaining(IntPtr storage)
    {
        capturedStorage = storage;
        capturedCallCount++;
        return nextULong;
    }

    private static IntPtr CaptureGlobStorageDirectory(IntPtr storage, string path, string? pattern, SDL3.SDL.GlobFlags flags, out int count)
    {
        capturedStorage = storage;
        capturedPath = path;
        capturedPattern = pattern;
        capturedFlags = flags;
        count = nextCount;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool TestClose(IntPtr userdata)
    {
        return userdata != IntPtr.Zero;
    }

    private static bool TestReady(IntPtr userdata)
    {
        return userdata != IntPtr.Zero;
    }

    private static bool TestEnumerate(IntPtr userdata, string path, SDL3.SDL.EnumerateDirectoryCallback callback, IntPtr callbackUserdata)
    {
        return callback(userdata, path, "entry") == SDL3.SDL.EnumerationResult.Success && callbackUserdata != IntPtr.Zero;
    }

    private static bool TestInfo(IntPtr userdata, string path, out SDL3.SDL.PathInfo info)
    {
        info = default;
        return path.Length > 0;
    }

    private static bool TestReadFile(IntPtr userdata, string path, IntPtr destination, ulong length)
    {
        return destination != IntPtr.Zero && length > 0;
    }

    private static bool TestWriteFile(IntPtr userdata, string path, IntPtr source, ulong length)
    {
        return source != IntPtr.Zero && length > 0;
    }

    private static bool TestMkdir(IntPtr userdata, string path)
    {
        return path.Length > 0;
    }

    private static bool TestRemove(IntPtr userdata, string path)
    {
        return path.Length > 0;
    }

    private static bool TestRename(IntPtr userdata, string oldpath, string newpath)
    {
        return oldpath != newpath;
    }

    private static bool TestCopy(IntPtr userdata, string oldpath, string newpath)
    {
        return oldpath != newpath;
    }

    private static ulong TestSpaceRemaining(IntPtr userdata)
    {
        return 1;
    }

    private static SDL3.SDL.EnumerationResult TestEnumerateDirectory(IntPtr userdata, string dirname, string fname)
    {
        return SDL3.SDL.EnumerationResult.Success;
    }

    private static void AssertStorageBoolMethod(string publicName, string nativeName, string hookFieldName, bool expected, IntPtr storage)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureStorageBool));

        bool result = (bool)InvokePublic(publicName, storage)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        TestAssert.Equal(storage, capturedStorage, $"SDL.{publicName} must forward storage.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertStoragePathBoolMethod(string publicName, string nativeName, string hookFieldName, IntPtr storage, string path, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureStoragePathBool));

        bool result = (bool)InvokePublic(publicName, storage, path)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        AssertStoragePath(storage, path, $"SDL.{publicName}");
    }

    private static void AssertStorageBufferMethod(string publicName, string nativeName, string hookFieldName, IntPtr storage, string path, IntPtr buffer, ulong length, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "path", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureStorageBuffer));

        bool result = (bool)InvokePublic(publicName, storage, path, buffer, length)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        AssertStoragePath(storage, path, $"SDL.{publicName}");
        TestAssert.Equal(buffer, capturedBuffer, $"SDL.{publicName} must forward buffer pointer.");
        TestAssert.Equal(length, capturedLength, $"SDL.{publicName} must forward length.");
    }

    private static void AssertStorageTwoPathMethod(string publicName, string nativeName, string hookFieldName, IntPtr storage, string oldpath, string newpath, bool expected)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "oldpath", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "newpath", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, nameof(CaptureStorageTwoPathBool));

        bool result = (bool)InvokePublic(publicName, storage, oldpath, newpath)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        TestAssert.Equal(storage, capturedStorage, $"SDL.{publicName} must forward storage.");
        TestAssert.Equal(oldpath, capturedOldPath, $"SDL.{publicName} must forward oldpath.");
        TestAssert.Equal(newpath, capturedNewPath, $"SDL.{publicName} must forward newpath.");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertStoragePath(IntPtr expectedStorage, string expectedPath, string apiName)
    {
        TestAssert.Equal(expectedStorage, capturedStorage, $"{apiName} must forward storage.");
        TestAssert.Equal(expectedPath, capturedPath, $"{apiName} must forward path.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertGlobArguments(IntPtr expectedStorage, string expectedPath, string? expectedPattern, SDL3.SDL.GlobFlags expectedFlags, string apiName)
    {
        TestAssert.Equal(expectedStorage, capturedStorage, $"{apiName} must forward storage.");
        TestAssert.Equal(expectedPath, capturedPath, $"{apiName} must forward path.");
        TestAssert.Equal(expectedPattern, capturedPattern, $"{apiName} must forward pattern.");
        TestAssert.Equal(expectedFlags, capturedFlags, $"{apiName} must forward flags.");
    }

    private static void AssertPathInfo(SDL3.SDL.PathInfo expected, SDL3.SDL.PathInfo actual, string apiName)
    {
        TestAssert.Equal(expected.Type, actual.Type, $"{apiName} must write info.Type.");
        TestAssert.Equal(expected.Size, actual.Size, $"{apiName} must write info.Size.");
        TestAssert.Equal(expected.CreateTime, actual.CreateTime, $"{apiName} must write info.CreateTime.");
        TestAssert.Equal(expected.ModifyTime, actual.ModifyTime, $"{apiName} must write info.ModifyTime.");
        TestAssert.Equal(expected.AccessTime, actual.AccessTime, $"{apiName} must write info.AccessTime.");
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

    private static object? InvokePublic(string methodName, params object[] arguments)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} public wrapper must exist.");
        return method!.Invoke(null, arguments);
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
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertSdlDllImport(MethodInfo method, string entryPoint)
    {
        DllImportAttribute? dllImport = method.GetCustomAttribute<DllImportAttribute>();
        TestAssert.NotNull(dllImport, $"SDL.{method.Name} must keep DllImport metadata.");
        TestAssert.Equal("SDL3", dllImport!.Value, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, dllImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
        AssertCdecl(method, $"SDL.{method.Name}");
    }

    private static void AssertCdecl(MethodInfo method, string apiName)
    {
        UnmanagedCallConvAttribute? callConv = method.GetCustomAttribute<UnmanagedCallConvAttribute>();
        TestAssert.NotNull(callConv, $"{apiName} must keep unmanaged calling convention metadata.");
        Type[] callConvs = callConv!.CallConvs ?? Array.Empty<Type>();
        TestAssert.Equal(1, callConvs.Length, $"{apiName} must declare one unmanaged calling convention.");
        TestAssert.Equal(typeof(CallConvCdecl), callConvs[0], $"{apiName} must use cdecl calling convention.");
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

    private static void AssertByRefParameter(MethodInfo method, string parameterName)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        TestAssert.True(parameter.ParameterType.IsByRef, $"SDL.{method.Name} parameter {parameterName} must stay by reference.");
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
