using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.FileAndIOAbstractions.Iostream;

internal static class PInvokeTests
{
    private static string? capturedFile;
    private static string? capturedMode;
    private static string? capturedFmt;
    private static string[]? capturedAp;
    private static IntPtr capturedContext;
    private static IntPtr capturedMem;
    private static IntPtr capturedPtr;
    private static IntPtr capturedSrc;
    private static IntPtr capturedDst;
    private static IntPtr capturedData;
    private static IntPtr capturedUserdata;
    private static UIntPtr capturedSize;
    private static UIntPtr capturedDatasize;
    private static long capturedOffset;
    private static SDL3.SDL.IOWhence capturedWhence;
    private static bool capturedCloseio;
    private static SDL3.SDL.IOStreamInterface capturedInterface;
    private static IntPtr nextPointer;
    private static bool nextBool;
    private static uint nextUInt;
    private static long nextLong;
    private static ulong nextULong;
    private static UIntPtr nextUIntPtr;
    private static UIntPtr nextDatasize;
    private static SDL3.SDL.IOStatus nextStatus;
    private static byte nextByte;
    private static sbyte nextSByte;
    private static ushort nextUShort;
    private static short nextShort;
    private static int nextInt;
    private static IntPtr expectedReadSrc;
    private static int capturedCallCount;

    public static void RunAll()
    {
        IOFromFile_ForwardsFileModeAndReturnsNativePointer();
        IOFromMem_ForwardsMemoryAndReturnsNativePointer();
        IOFromConstMem_ForwardsMemoryAndReturnsNativePointer();
        IOFromDynamicMem_ReturnsNativePointer();
        OpenIO_ForwardsInterfaceUserdataAndReturnsNativePointer();
        CloseIO_ForwardsContextAndReturnsNativeValue();
        GetIOProperties_ForwardsContextAndReturnsNativeValue();
        GetIOStatus_ForwardsContextAndReturnsNativeValue();
        GetIOSize_ForwardsContextAndReturnsNativeValue();
        SeekIO_ForwardsContextOffsetWhenceAndReturnsNativeValue();
        TellIO_ForwardsContextAndReturnsNativeValue();
        ReadIO_ForwardsBufferAndReturnsNativeValue();
        WriteIO_ForwardsBufferAndReturnsNativeValue();
        IOprintf_ForwardsFormatAndReturnsNativeValue();
        IOvprintf_ForwardsFormatArgumentsAndReturnsNativeValue();
        FlushIO_ForwardsContextAndReturnsNativeValue();
        LoadFileIO_ForwardsSourceCloseFlagAndWritesDatasize();
        LoadFile_ForwardsFileAndWritesDatasize();
        SaveFileIO_ForwardsSourceDataSizeCloseFlagAndReturnsNativeValue();
        SaveFile_ForwardsFileDataSizeAndReturnsNativeValue();
        ReadU8_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS8_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU16LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS16LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU16BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS16BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU32LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS32LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU32BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS32BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU64LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS64LE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadU64BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        ReadS64BE_ForwardsStreamWritesValueAndReturnsNativeValue();
        WriteU8_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS8_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU16LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS16LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU16BE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS16BE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU32LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS32LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU32BE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS32BE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU64LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS64LE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteU64BE_ForwardsDestinationValueAndReturnsNativeValue();
        WriteS64BE_ForwardsDestinationValueAndReturnsNativeValue();
    }

    public static void IOFromFile_ForwardsFileModeAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOFromFile");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOFromFile");
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);
        AssertStringParameterMarshal(nativeMethod, "mode", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)1001;
        using NativeHookScope _ = NativeHookScope.Install("IOFromFileNativeFunction", nameof(CaptureIOFromFile));

        IntPtr result = SDL3.SDL.IOFromFile("save.dat", "rb");

        TestAssert.Equal((IntPtr)1001, result, "SDL.IOFromFile must return the native stream pointer.");
        TestAssert.Equal("save.dat", capturedFile, "SDL.IOFromFile must forward file.");
        TestAssert.Equal("rb", capturedMode, "SDL.IOFromFile must forward mode.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IOFromFile must call native hook once.");
    }

    public static void IOFromMem_ForwardsMemoryAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOFromMem");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOFromMem");

        ResetCaptureState();
        nextPointer = (IntPtr)1002;
        using NativeHookScope _ = NativeHookScope.Install("IOFromMemNativeFunction", nameof(CaptureMemStream));

        IntPtr result = SDL3.SDL.IOFromMem((IntPtr)11, (UIntPtr)12);

        TestAssert.Equal((IntPtr)1002, result, "SDL.IOFromMem must return the native stream pointer.");
        AssertMemoryArguments((IntPtr)11, (UIntPtr)12, "SDL.IOFromMem");
    }

    public static void IOFromConstMem_ForwardsMemoryAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOFromConstMem");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOFromConstMem");

        ResetCaptureState();
        nextPointer = (IntPtr)1003;
        using NativeHookScope _ = NativeHookScope.Install("IOFromConstMemNativeFunction", nameof(CaptureMemStream));

        IntPtr result = SDL3.SDL.IOFromConstMem((IntPtr)21, (UIntPtr)22);

        TestAssert.Equal((IntPtr)1003, result, "SDL.IOFromConstMem must return the native stream pointer.");
        AssertMemoryArguments((IntPtr)21, (UIntPtr)22, "SDL.IOFromConstMem");
    }

    public static void IOFromDynamicMem_ReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOFromDynamicMem");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOFromDynamicMem");

        ResetCaptureState();
        nextPointer = (IntPtr)1004;
        using NativeHookScope _ = NativeHookScope.Install("IOFromDynamicMemNativeFunction", nameof(CaptureNoArgumentPointer));

        IntPtr result = SDL3.SDL.IOFromDynamicMem();

        TestAssert.Equal((IntPtr)1004, result, "SDL.IOFromDynamicMem must return the native stream pointer.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IOFromDynamicMem must call native hook once.");
    }

    public static void OpenIO_ForwardsInterfaceUserdataAndReturnsNativePointer()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_OpenIO");
        AssertSdlDllImport(nativeMethod, "SDL_OpenIO");
        AssertByRefParameter(nativeMethod, "iface");

        ResetCaptureState();
        nextPointer = (IntPtr)1005;
        using NativeHookScope _ = NativeHookScope.Install("OpenIONativeFunction", nameof(CaptureOpenIO));

        SDL3.SDL.IOStreamInterface iface = new()
        {
            Version = 72,
            Size = TestSize,
            Seek = TestSeek,
            Read = TestRead,
            Write = TestWrite,
            Flush = TestFlush,
            Close = TestClose
        };
        IntPtr result = SDL3.SDL.OpenIO(in iface, (IntPtr)55);

        TestAssert.Equal((IntPtr)1005, result, "SDL.OpenIO must return the native stream pointer.");
        TestAssert.Equal((IntPtr)55, capturedUserdata, "SDL.OpenIO must forward userdata.");
        TestAssert.Equal(72u, capturedInterface.Version, "SDL.OpenIO must forward iface.Version.");
        TestAssert.Equal<SDL3.SDL.IOStreamInterface.SizeDelegate?>(TestSize, capturedInterface.Size, "SDL.OpenIO must forward iface.Size.");
        TestAssert.Equal<SDL3.SDL.IOStreamInterface.CloseDelegate?>(TestClose, capturedInterface.Close, "SDL.OpenIO must forward iface.Close.");
        TestAssert.Equal(1, capturedCallCount, "SDL.OpenIO must call native hook once.");
    }

    public static void CloseIO_ForwardsContextAndReturnsNativeValue()
    {
        AssertBoolContextMethod("CloseIO", "SDL_CloseIO", "CloseIONativeFunction", nameof(CaptureContextBool), true, (IntPtr)2001);
    }

    public static void GetIOProperties_ForwardsContextAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetIOProperties");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetIOProperties");

        ResetCaptureState();
        nextUInt = 7001;
        using NativeHookScope _ = NativeHookScope.Install("GetIOPropertiesNativeFunction", nameof(CaptureGetIOProperties));

        uint result = SDL3.SDL.GetIOProperties((IntPtr)2002);

        TestAssert.Equal(7001u, result, "SDL.GetIOProperties must return native properties ID.");
        AssertContext((IntPtr)2002, "SDL.GetIOProperties");
    }

    public static void GetIOStatus_ForwardsContextAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_GetIOStatus");
        AssertSdlLibraryImport(nativeMethod, "SDL_GetIOStatus");

        ResetCaptureState();
        nextStatus = SDL3.SDL.IOStatus.NotReady;
        using NativeHookScope _ = NativeHookScope.Install("GetIOStatusNativeFunction", nameof(CaptureGetIOStatus));

        SDL3.SDL.IOStatus result = SDL3.SDL.GetIOStatus((IntPtr)2003);

        TestAssert.Equal(SDL3.SDL.IOStatus.NotReady, result, "SDL.GetIOStatus must return native status.");
        AssertContext((IntPtr)2003, "SDL.GetIOStatus");
    }

    public static void GetIOSize_ForwardsContextAndReturnsNativeValue()
    {
        AssertLongContextMethod("GetIOSize", "SDL_GetIOSize", "GetIOSizeNativeFunction", nameof(CaptureContextLong), 4096, (IntPtr)2004);
    }

    public static void SeekIO_ForwardsContextOffsetWhenceAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SeekIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_SeekIO");

        ResetCaptureState();
        nextLong = 125;
        using NativeHookScope _ = NativeHookScope.Install("SeekIONativeFunction", nameof(CaptureSeekIO));

        long result = SDL3.SDL.SeekIO((IntPtr)2005, -12, SDL3.SDL.IOWhence.End);

        TestAssert.Equal(125L, result, "SDL.SeekIO must return native offset.");
        TestAssert.Equal((IntPtr)2005, capturedContext, "SDL.SeekIO must forward context.");
        TestAssert.Equal(-12L, capturedOffset, "SDL.SeekIO must forward offset.");
        TestAssert.Equal(SDL3.SDL.IOWhence.End, capturedWhence, "SDL.SeekIO must forward whence.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SeekIO must call native hook once.");
    }

    public static void TellIO_ForwardsContextAndReturnsNativeValue()
    {
        AssertLongContextMethod("TellIO", "SDL_TellIO", "TellIONativeFunction", nameof(CaptureContextLong), 512, (IntPtr)2006);
    }

    public static void ReadIO_ForwardsBufferAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_ReadIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_ReadIO");

        ResetCaptureState();
        nextULong = 6;
        using NativeHookScope _ = NativeHookScope.Install("ReadIONativeFunction", nameof(CaptureBufferIO));

        ulong result = SDL3.SDL.ReadIO((IntPtr)3001, (IntPtr)3002, (UIntPtr)6);

        TestAssert.Equal(6UL, result, "SDL.ReadIO must return native byte count.");
        AssertBufferArguments((IntPtr)3001, (IntPtr)3002, (UIntPtr)6, "SDL.ReadIO");
    }

    public static void WriteIO_ForwardsBufferAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_WriteIO");
        AssertSdlLibraryImport(nativeMethod, "SDL_WriteIO");

        ResetCaptureState();
        nextULong = 8;
        using NativeHookScope _ = NativeHookScope.Install("WriteIONativeFunction", nameof(CaptureBufferIO));

        ulong result = SDL3.SDL.WriteIO((IntPtr)3003, (IntPtr)3004, (UIntPtr)8);

        TestAssert.Equal(8UL, result, "SDL.WriteIO must return native byte count.");
        AssertBufferArguments((IntPtr)3003, (IntPtr)3004, (UIntPtr)8, "SDL.WriteIO");
    }

    public static void IOprintf_ForwardsFormatAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOprintf");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOprintf");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextUIntPtr = (UIntPtr)9;
        using NativeHookScope _ = NativeHookScope.Install("IOprintfNativeFunction", nameof(CaptureIOprintf));

        UIntPtr result = SDL3.SDL.IOprintf((IntPtr)4001, "score=%d");

        TestAssert.Equal((UIntPtr)9, result, "SDL.IOprintf must return native byte count.");
        TestAssert.Equal((IntPtr)4001, capturedContext, "SDL.IOprintf must forward context.");
        TestAssert.Equal("score=%d", capturedFmt, "SDL.IOprintf must forward fmt.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IOprintf must call native hook once.");
    }

    public static void IOvprintf_ForwardsFormatArgumentsAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_IOvprintf");
        AssertSdlLibraryImport(nativeMethod, "SDL_IOvprintf");
        AssertStringParameterMarshal(nativeMethod, "fmt", UnmanagedType.LPUTF8Str);
        AssertStringArrayParameterMarshal(nativeMethod, "ap", UnmanagedType.LPArray, UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextUIntPtr = (UIntPtr)10;
        using NativeHookScope _ = NativeHookScope.Install("IOvprintfNativeFunction", nameof(CaptureIOvprintf));

        string[] ap = ["one", "two"];
        UIntPtr result = SDL3.SDL.IOvprintf((IntPtr)4002, "%s%s", ap);

        TestAssert.Equal((UIntPtr)10, result, "SDL.IOvprintf must return native byte count.");
        TestAssert.Equal((IntPtr)4002, capturedContext, "SDL.IOvprintf must forward context.");
        TestAssert.Equal("%s%s", capturedFmt, "SDL.IOvprintf must forward fmt.");
        TestAssert.Equal(ap, capturedAp!, "SDL.IOvprintf must forward the argument list.");
        TestAssert.Equal(1, capturedCallCount, "SDL.IOvprintf must call native hook once.");
    }

    public static void FlushIO_ForwardsContextAndReturnsNativeValue()
    {
        AssertBoolContextMethod("FlushIO", "SDL_FlushIO", "FlushIONativeFunction", nameof(CaptureContextBool), false, (IntPtr)4003);
    }

    public static void LoadFileIO_ForwardsSourceCloseFlagAndWritesDatasize()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LoadFile_IO");
        AssertSdlLibraryImport(nativeMethod, "SDL_LoadFile_IO");
        AssertBoolParameterMarshal(nativeMethod, "closeio", UnmanagedType.I1);

        ResetCaptureState();
        nextPointer = (IntPtr)5001;
        nextDatasize = (UIntPtr)123;
        using NativeHookScope _ = NativeHookScope.Install("LoadFileIONativeFunction", nameof(CaptureLoadFileIO));

        IntPtr result = SDL3.SDL.LoadFileIO((IntPtr)5002, out UIntPtr datasize, true);

        TestAssert.Equal((IntPtr)5001, result, "SDL.LoadFileIO must return native data pointer.");
        TestAssert.Equal((UIntPtr)123, datasize, "SDL.LoadFileIO must write datasize.");
        TestAssert.Equal((IntPtr)5002, capturedSrc, "SDL.LoadFileIO must forward src.");
        TestAssert.Equal(true, capturedCloseio, "SDL.LoadFileIO must forward closeio.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LoadFileIO must call native hook once.");
    }

    public static void LoadFile_ForwardsFileAndWritesDatasize()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_LoadFile");
        AssertSdlLibraryImport(nativeMethod, "SDL_LoadFile");
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextPointer = (IntPtr)5003;
        nextDatasize = (UIntPtr)456;
        using NativeHookScope _ = NativeHookScope.Install("LoadFileNativeFunction", nameof(CaptureLoadFile));

        IntPtr result = SDL3.SDL.LoadFile("state.bin", out UIntPtr datasize);

        TestAssert.Equal((IntPtr)5003, result, "SDL.LoadFile must return native data pointer.");
        TestAssert.Equal((UIntPtr)456, datasize, "SDL.LoadFile must write datasize.");
        TestAssert.Equal("state.bin", capturedFile, "SDL.LoadFile must forward file.");
        TestAssert.Equal(1, capturedCallCount, "SDL.LoadFile must call native hook once.");
    }

    public static void SaveFileIO_ForwardsSourceDataSizeCloseFlagAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SaveFile_IO");
        AssertSdlLibraryImport(nativeMethod, "SDL_SaveFile_IO");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertBoolParameterMarshal(nativeMethod, "closeio", UnmanagedType.I1);

        ResetCaptureState();
        nextBool = true;
        using NativeHookScope _ = NativeHookScope.Install("SaveFileIONativeFunction", nameof(CaptureSaveFileIO));

        bool result = SDL3.SDL.SaveFileIO((IntPtr)5004, (IntPtr)5005, (UIntPtr)99, false);

        TestAssert.Equal(true, result, "SDL.SaveFileIO must return native bool value.");
        TestAssert.Equal((IntPtr)5004, capturedSrc, "SDL.SaveFileIO must forward src.");
        TestAssert.Equal((IntPtr)5005, capturedData, "SDL.SaveFileIO must forward data.");
        TestAssert.Equal((UIntPtr)99, capturedDatasize, "SDL.SaveFileIO must forward datasize.");
        TestAssert.Equal(false, capturedCloseio, "SDL.SaveFileIO must forward closeio.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SaveFileIO must call native hook once.");
    }

    public static void SaveFile_ForwardsFileDataSizeAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = GetNativeMethod("SDL_SaveFile");
        AssertSdlLibraryImport(nativeMethod, "SDL_SaveFile");
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        AssertStringParameterMarshal(nativeMethod, "file", UnmanagedType.LPUTF8Str);

        ResetCaptureState();
        nextBool = false;
        using NativeHookScope _ = NativeHookScope.Install("SaveFileNativeFunction", nameof(CaptureSaveFile));

        bool result = SDL3.SDL.SaveFile("state.bin", (IntPtr)5006, (UIntPtr)100);

        TestAssert.Equal(false, result, "SDL.SaveFile must return native bool value.");
        TestAssert.Equal("state.bin", capturedFile, "SDL.SaveFile must forward file.");
        TestAssert.Equal((IntPtr)5006, capturedData, "SDL.SaveFile must forward data.");
        TestAssert.Equal((UIntPtr)100, capturedDatasize, "SDL.SaveFile must forward datasize.");
        TestAssert.Equal(1, capturedCallCount, "SDL.SaveFile must call native hook once.");
    }

    public static void ReadU8_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU8");
        using NativeHookScope _ = InstallReadHook("ReadU8NativeFunction", nameof(CaptureReadU8), true, (IntPtr)6001);
        nextByte = 0xAB;

        bool result = SDL3.SDL.ReadU8((IntPtr)6001, out byte value);

        AssertReadResult(nativeMethod, result, true, value, nextByte, "SDL.ReadU8");
    }

    public static void ReadS8_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS8");
        using NativeHookScope _ = InstallReadHook("ReadS8NativeFunction", nameof(CaptureReadS8), false, (IntPtr)6002);
        nextSByte = -12;

        bool result = SDL3.SDL.ReadS8((IntPtr)6002, out sbyte value);

        AssertReadResult(nativeMethod, result, false, value, nextSByte, "SDL.ReadS8");
    }

    public static void ReadU16LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU16LE");
        using NativeHookScope _ = InstallReadHook("ReadU16LENativeFunction", nameof(CaptureReadUShort), true, (IntPtr)6003);
        nextUShort = 0x1234;

        bool result = SDL3.SDL.ReadU16LE((IntPtr)6003, out ushort value);

        AssertReadResult(nativeMethod, result, true, value, nextUShort, "SDL.ReadU16LE");
    }

    public static void ReadS16LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS16LE");
        using NativeHookScope _ = InstallReadHook("ReadS16LENativeFunction", nameof(CaptureReadShort), false, (IntPtr)6004);
        nextShort = -1234;

        bool result = SDL3.SDL.ReadS16LE((IntPtr)6004, out short value);

        AssertReadResult(nativeMethod, result, false, value, nextShort, "SDL.ReadS16LE");
    }

    public static void ReadU16BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU16BE");
        using NativeHookScope _ = InstallReadHook("ReadU16BENativeFunction", nameof(CaptureReadUShort), true, (IntPtr)6005);
        nextUShort = 0x5678;

        bool result = SDL3.SDL.ReadU16BE((IntPtr)6005, out ushort value);

        AssertReadResult(nativeMethod, result, true, value, nextUShort, "SDL.ReadU16BE");
    }

    public static void ReadS16BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS16BE");
        using NativeHookScope _ = InstallReadHook("ReadS16BENativeFunction", nameof(CaptureReadShort), false, (IntPtr)6006);
        nextShort = -5678;

        bool result = SDL3.SDL.ReadS16BE((IntPtr)6006, out short value);

        AssertReadResult(nativeMethod, result, false, value, nextShort, "SDL.ReadS16BE");
    }

    public static void ReadU32LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU32LE");
        using NativeHookScope _ = InstallReadHook("ReadU32LENativeFunction", nameof(CaptureReadUInt), true, (IntPtr)6007);
        nextUInt = 0x12345678;

        bool result = SDL3.SDL.ReadU32LE((IntPtr)6007, out uint value);

        AssertReadResult(nativeMethod, result, true, value, nextUInt, "SDL.ReadU32LE");
    }

    public static void ReadS32LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS32LE");
        using NativeHookScope _ = InstallReadHook("ReadS32LENativeFunction", nameof(CaptureReadInt), false, (IntPtr)6008);
        nextInt = -123456;

        bool result = SDL3.SDL.ReadS32LE((IntPtr)6008, out int value);

        AssertReadResult(nativeMethod, result, false, value, nextInt, "SDL.ReadS32LE");
    }

    public static void ReadU32BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU32BE");
        using NativeHookScope _ = InstallReadHook("ReadU32BENativeFunction", nameof(CaptureReadUInt), true, (IntPtr)6009);
        nextUInt = 0x87654321;

        bool result = SDL3.SDL.ReadU32BE((IntPtr)6009, out uint value);

        AssertReadResult(nativeMethod, result, true, value, nextUInt, "SDL.ReadU32BE");
    }

    public static void ReadS32BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS32BE");
        using NativeHookScope _ = InstallReadHook("ReadS32BENativeFunction", nameof(CaptureReadInt), false, (IntPtr)6010);
        nextInt = -654321;

        bool result = SDL3.SDL.ReadS32BE((IntPtr)6010, out int value);

        AssertReadResult(nativeMethod, result, false, value, nextInt, "SDL.ReadS32BE");
    }

    public static void ReadU64LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU64LE");
        using NativeHookScope _ = InstallReadHook("ReadU64LENativeFunction", nameof(CaptureReadULong), true, (IntPtr)6011);
        nextULong = 0x1234567890ABCDEF;

        bool result = SDL3.SDL.ReadU64LE((IntPtr)6011, out ulong value);

        AssertReadResult(nativeMethod, result, true, value, nextULong, "SDL.ReadU64LE");
    }

    public static void ReadS64LE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS64LE");
        using NativeHookScope _ = InstallReadHook("ReadS64LENativeFunction", nameof(CaptureReadLong), false, (IntPtr)6012);
        nextLong = -1234567890;

        bool result = SDL3.SDL.ReadS64LE((IntPtr)6012, out long value);

        AssertReadResult(nativeMethod, result, false, value, nextLong, "SDL.ReadS64LE");
    }

    public static void ReadU64BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadU64BE");
        using NativeHookScope _ = InstallReadHook("ReadU64BENativeFunction", nameof(CaptureReadULong), true, (IntPtr)6013);
        nextULong = 0xFEDCBA0987654321;

        bool result = SDL3.SDL.ReadU64BE((IntPtr)6013, out ulong value);

        AssertReadResult(nativeMethod, result, true, value, nextULong, "SDL.ReadU64BE");
    }

    public static void ReadS64BE_ForwardsStreamWritesValueAndReturnsNativeValue()
    {
        MethodInfo nativeMethod = AssertReadScalarMetadata("SDL_ReadS64BE");
        using NativeHookScope _ = InstallReadHook("ReadS64BENativeFunction", nameof(CaptureReadLong), false, (IntPtr)6014);
        nextLong = -9876543210;

        bool result = SDL3.SDL.ReadS64BE((IntPtr)6014, out long value);

        AssertReadResult(nativeMethod, result, false, value, nextLong, "SDL.ReadS64BE");
    }

    public static void WriteU8_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU8", "SDL_WriteU8", "WriteU8NativeFunction", nameof(CaptureWriteByte), (IntPtr)7001, (byte)0x7A, true);
    }

    public static void WriteS8_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS8", "SDL_WriteS8", "WriteS8NativeFunction", nameof(CaptureWriteSByte), (IntPtr)7002, (sbyte)-7, false);
    }

    public static void WriteU16LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU16LE", "SDL_WriteU16LE", "WriteU16LENativeFunction", nameof(CaptureWriteUShort), (IntPtr)7003, (ushort)0x1234, true);
    }

    public static void WriteS16LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS16LE", "SDL_WriteS16LE", "WriteS16LENativeFunction", nameof(CaptureWriteShort), (IntPtr)7004, (short)-1234, false);
    }

    public static void WriteU16BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU16BE", "SDL_WriteU16BE", "WriteU16BENativeFunction", nameof(CaptureWriteUShort), (IntPtr)7005, (ushort)0x5678, true);
    }

    public static void WriteS16BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS16BE", "SDL_WriteS16BE", "WriteS16BENativeFunction", nameof(CaptureWriteShort), (IntPtr)7006, (short)-5678, false);
    }

    public static void WriteU32LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU32LE", "SDL_WriteU32LE", "WriteU32LENativeFunction", nameof(CaptureWriteUInt), (IntPtr)7007, 0x12345678u, true);
    }

    public static void WriteS32LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS32LE", "SDL_WriteS32LE", "WriteS32LENativeFunction", nameof(CaptureWriteInt), (IntPtr)7008, -123456, false);
    }

    public static void WriteU32BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU32BE", "SDL_WriteU32BE", "WriteU32BENativeFunction", nameof(CaptureWriteUInt), (IntPtr)7009, 0x87654321u, true);
    }

    public static void WriteS32BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS32BE", "SDL_WriteS32BE", "WriteS32BENativeFunction", nameof(CaptureWriteInt), (IntPtr)7010, -654321, false);
    }

    public static void WriteU64LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU64LE", "SDL_WriteU64LE", "WriteU64LENativeFunction", nameof(CaptureWriteULong), (IntPtr)7011, 0x1234567890ABCDEFUL, true);
    }

    public static void WriteS64LE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS64LE", "SDL_WriteS64LE", "WriteS64LENativeFunction", nameof(CaptureWriteLong), (IntPtr)7012, -1234567890L, false);
    }

    public static void WriteU64BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteU64BE", "SDL_WriteU64BE", "WriteU64BENativeFunction", nameof(CaptureWriteULong), (IntPtr)7013, 0xFEDCBA0987654321UL, true);
    }

    public static void WriteS64BE_ForwardsDestinationValueAndReturnsNativeValue()
    {
        AssertWriteScalar("WriteS64BE", "SDL_WriteS64BE", "WriteS64BENativeFunction", nameof(CaptureWriteLong), (IntPtr)7014, -9876543210L, false);
    }

    private static void ResetCaptureState()
    {
        capturedFile = null;
        capturedMode = null;
        capturedFmt = null;
        capturedAp = null;
        capturedContext = IntPtr.Zero;
        capturedMem = IntPtr.Zero;
        capturedPtr = IntPtr.Zero;
        capturedSrc = IntPtr.Zero;
        capturedDst = IntPtr.Zero;
        capturedData = IntPtr.Zero;
        capturedUserdata = IntPtr.Zero;
        capturedSize = UIntPtr.Zero;
        capturedDatasize = UIntPtr.Zero;
        capturedOffset = 0;
        capturedWhence = default;
        capturedCloseio = false;
        capturedInterface = default;
        nextPointer = IntPtr.Zero;
        nextBool = false;
        nextUInt = 0;
        nextLong = 0;
        nextULong = 0;
        nextUIntPtr = UIntPtr.Zero;
        nextDatasize = UIntPtr.Zero;
        nextStatus = default;
        nextByte = 0;
        nextSByte = 0;
        nextUShort = 0;
        nextShort = 0;
        nextInt = 0;
        expectedReadSrc = IntPtr.Zero;
        capturedCallCount = 0;
    }

    private static IntPtr CaptureIOFromFile(string file, string mode)
    {
        capturedFile = file;
        capturedMode = mode;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureMemStream(IntPtr mem, UIntPtr size)
    {
        capturedMem = mem;
        capturedSize = size;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureNoArgumentPointer()
    {
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureOpenIO(in SDL3.SDL.IOStreamInterface iface, IntPtr userdata)
    {
        capturedInterface = iface;
        capturedUserdata = userdata;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureContextBool(IntPtr context)
    {
        capturedContext = context;
        capturedCallCount++;
        return nextBool;
    }

    private static uint CaptureGetIOProperties(IntPtr context)
    {
        capturedContext = context;
        capturedCallCount++;
        return nextUInt;
    }

    private static SDL3.SDL.IOStatus CaptureGetIOStatus(IntPtr context)
    {
        capturedContext = context;
        capturedCallCount++;
        return nextStatus;
    }

    private static long CaptureContextLong(IntPtr context)
    {
        capturedContext = context;
        capturedCallCount++;
        return nextLong;
    }

    private static long CaptureSeekIO(IntPtr context, long offset, SDL3.SDL.IOWhence whence)
    {
        capturedContext = context;
        capturedOffset = offset;
        capturedWhence = whence;
        capturedCallCount++;
        return nextLong;
    }

    private static ulong CaptureBufferIO(IntPtr context, IntPtr ptr, UIntPtr size)
    {
        capturedContext = context;
        capturedPtr = ptr;
        capturedSize = size;
        capturedCallCount++;
        return nextULong;
    }

    private static UIntPtr CaptureIOprintf(IntPtr context, string fmt)
    {
        capturedContext = context;
        capturedFmt = fmt;
        capturedCallCount++;
        return nextUIntPtr;
    }

    private static UIntPtr CaptureIOvprintf(IntPtr context, string fmt, string[] ap)
    {
        capturedContext = context;
        capturedFmt = fmt;
        capturedAp = ap;
        capturedCallCount++;
        return nextUIntPtr;
    }

    private static IntPtr CaptureLoadFileIO(IntPtr src, out UIntPtr datasize, bool closeio)
    {
        capturedSrc = src;
        capturedCloseio = closeio;
        datasize = nextDatasize;
        capturedCallCount++;
        return nextPointer;
    }

    private static IntPtr CaptureLoadFile(string file, out UIntPtr datasize)
    {
        capturedFile = file;
        datasize = nextDatasize;
        capturedCallCount++;
        return nextPointer;
    }

    private static bool CaptureSaveFileIO(IntPtr src, IntPtr data, UIntPtr datasize, bool closeio)
    {
        capturedSrc = src;
        capturedData = data;
        capturedDatasize = datasize;
        capturedCloseio = closeio;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureSaveFile(string file, IntPtr data, UIntPtr datasize)
    {
        capturedFile = file;
        capturedData = data;
        capturedDatasize = datasize;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadU8(IntPtr src, out byte value)
    {
        capturedSrc = src;
        value = nextByte;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadS8(IntPtr src, out sbyte value)
    {
        capturedSrc = src;
        value = nextSByte;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadUShort(IntPtr src, out ushort value)
    {
        capturedSrc = src;
        value = nextUShort;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadShort(IntPtr src, out short value)
    {
        capturedSrc = src;
        value = nextShort;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadUInt(IntPtr src, out uint value)
    {
        capturedSrc = src;
        value = nextUInt;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadInt(IntPtr src, out int value)
    {
        capturedSrc = src;
        value = nextInt;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadULong(IntPtr src, out ulong value)
    {
        capturedSrc = src;
        value = nextULong;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureReadLong(IntPtr src, out long value)
    {
        capturedSrc = src;
        value = nextLong;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteByte(IntPtr dst, byte value)
    {
        capturedDst = dst;
        nextByte = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteSByte(IntPtr dst, sbyte value)
    {
        capturedDst = dst;
        nextSByte = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteUShort(IntPtr dst, ushort value)
    {
        capturedDst = dst;
        nextUShort = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteShort(IntPtr dst, short value)
    {
        capturedDst = dst;
        nextShort = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteUInt(IntPtr dst, uint value)
    {
        capturedDst = dst;
        nextUInt = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteInt(IntPtr dst, int value)
    {
        capturedDst = dst;
        nextInt = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteULong(IntPtr dst, ulong value)
    {
        capturedDst = dst;
        nextULong = value;
        capturedCallCount++;
        return nextBool;
    }

    private static bool CaptureWriteLong(IntPtr dst, long value)
    {
        capturedDst = dst;
        nextLong = value;
        capturedCallCount++;
        return nextBool;
    }

    private static long TestSize(IntPtr userdata)
    {
        return userdata.ToInt64();
    }

    private static long TestSeek(IntPtr userdata, long offset, SDL3.SDL.IOWhence whence)
    {
        return userdata.ToInt64() + offset + (int)whence;
    }

    private static ulong TestRead(IntPtr userdata, IntPtr ptr, ulong size, out SDL3.SDL.IOStatus status)
    {
        status = SDL3.SDL.IOStatus.ReadOnly;
        return size;
    }

    private static ulong TestWrite(IntPtr userdata, IntPtr ptr, ulong size, out SDL3.SDL.IOStatus status)
    {
        status = SDL3.SDL.IOStatus.WriteOnly;
        return size;
    }

    private static bool TestFlush(IntPtr userdata, out SDL3.SDL.IOStatus status)
    {
        status = SDL3.SDL.IOStatus.Ready;
        return true;
    }

    private static bool TestClose(IntPtr userdata)
    {
        return userdata != IntPtr.Zero;
    }

    private static void AssertBoolContextMethod(string publicName, string nativeName, string hookFieldName, string hookMethodName, bool expected, IntPtr context)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);

        bool result = (bool)InvokePublic(publicName, context)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native bool value.");
        AssertContext(context, $"SDL.{publicName}");
    }

    private static void AssertLongContextMethod(string publicName, string nativeName, string hookFieldName, string hookMethodName, long expected, IntPtr context)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);

        ResetCaptureState();
        nextLong = expected;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);

        long result = (long)InvokePublic(publicName, context)!;

        TestAssert.Equal(expected, result, $"SDL.{publicName} must return native value.");
        AssertContext(context, $"SDL.{publicName}");
    }

    private static MethodInfo AssertReadScalarMetadata(string nativeName)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);
        return nativeMethod;
    }

    private static NativeHookScope InstallReadHook(string hookFieldName, string hookMethodName, bool expectedBool, IntPtr expectedSrc)
    {
        ResetCaptureState();
        nextBool = expectedBool;
        expectedReadSrc = expectedSrc;
        return NativeHookScope.Install(hookFieldName, hookMethodName);
    }

    private static void AssertReadResult<T>(MethodInfo nativeMethod, bool actualResult, bool expectedResult, T actualValue, T expectedValue, string apiName)
    {
        _ = nativeMethod;
        TestAssert.Equal(expectedResult, actualResult, $"{apiName} must return native bool value.");
        TestAssert.Equal(expectedValue, actualValue, $"{apiName} must write value.");
        TestAssert.Equal(expectedReadSrc, capturedSrc, $"{apiName} must forward src.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertWriteScalar<T>(string publicName, string nativeName, string hookFieldName, string hookMethodName, IntPtr dst, T value, bool expectedResult)
    {
        MethodInfo nativeMethod = GetNativeMethod(nativeName);
        AssertSdlLibraryImport(nativeMethod, nativeName);
        AssertBoolReturnMarshal(nativeMethod, UnmanagedType.I1);

        ResetCaptureState();
        nextBool = expectedResult;
        using NativeHookScope _ = NativeHookScope.Install(hookFieldName, hookMethodName);

        bool result = (bool)InvokePublic(publicName, dst, value!)!;

        TestAssert.Equal(expectedResult, result, $"SDL.{publicName} must return native bool value.");
        TestAssert.Equal(dst, capturedDst, $"SDL.{publicName} must forward dst.");
        AssertCapturedWriteValue(value, $"SDL.{publicName}");
        TestAssert.Equal(1, capturedCallCount, $"SDL.{publicName} must call native hook once.");
    }

    private static void AssertCapturedWriteValue<T>(T expectedValue, string apiName)
    {
        object actual = expectedValue switch
        {
            byte => nextByte,
            sbyte => nextSByte,
            ushort => nextUShort,
            short => nextShort,
            uint => nextUInt,
            int => nextInt,
            ulong => nextULong,
            long => nextLong,
            _ => throw new NotSupportedException($"Unsupported write test type {typeof(T).Name}.")
        };
        TestAssert.Equal(expectedValue, (T)actual, $"{apiName} must forward value.");
    }

    private static void AssertMemoryArguments(IntPtr expectedMem, UIntPtr expectedSize, string apiName)
    {
        TestAssert.Equal(expectedMem, capturedMem, $"{apiName} must forward mem.");
        TestAssert.Equal(expectedSize, capturedSize, $"{apiName} must forward size.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertContext(IntPtr expectedContext, string apiName)
    {
        TestAssert.Equal(expectedContext, capturedContext, $"{apiName} must forward context.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
    }

    private static void AssertBufferArguments(IntPtr expectedContext, IntPtr expectedPtr, UIntPtr expectedSize, string apiName)
    {
        TestAssert.Equal(expectedContext, capturedContext, $"{apiName} must forward context.");
        TestAssert.Equal(expectedPtr, capturedPtr, $"{apiName} must forward ptr.");
        TestAssert.Equal(expectedSize, capturedSize, $"{apiName} must forward size.");
        TestAssert.Equal(1, capturedCallCount, $"{apiName} must call native hook once.");
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

    private static void AssertStringArrayParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType, UnmanagedType arraySubType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected array marshalling.");
        TestAssert.Equal(arraySubType, marshalAs.ArraySubType, $"SDL.{method.Name} parameter {parameterName} must use expected string array subtype.");
    }

    private static void AssertBoolParameterMarshal(MethodInfo method, string parameterName, UnmanagedType unmanagedType)
    {
        ParameterInfo parameter = method.GetParameters().Single(param => param.Name == parameterName);
        MarshalAsAttribute? marshalAs = parameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} parameter {parameterName} must keep MarshalAs metadata.");
        TestAssert.Equal(unmanagedType, marshalAs!.Value, $"SDL.{method.Name} parameter {parameterName} must use expected bool marshalling.");
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
