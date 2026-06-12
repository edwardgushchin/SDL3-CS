using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.AdditionalFunctionality.Stdinc;

internal static class MacroTests
{
    public static void FourCC_ComposesLittleEndianCode()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.FourCC), BindingFlags.Public | BindingFlags.Static);
        TestAssert.NotNull(method, "SDL.FourCC method must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), "SDL.FourCC must keep Macro metadata.");

        uint value = SDL3.SDL.FourCC('A', 'B', 'C', 'D');

        TestAssert.Equal(0x44434241u, value, "SDL.FourCC must compose bytes in SDL little-endian macro order.");
    }

    public static void InitInterface_IOStreamInterface_SetsVersionAndClearsFields()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.InitInterface), BindingFlags.Public | BindingFlags.Static, [typeof(SDL3.SDL.IOStreamInterface).MakeByRefType()]);
        TestAssert.NotNull(method, "SDL.InitInterface(ref IOStreamInterface) method must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), "SDL.InitInterface(ref IOStreamInterface) must keep Macro metadata.");

        SDL3.SDL.IOStreamInterface iface = new()
        {
            Version = 1,
            Size = IOStreamSize
        };
        TestAssert.Equal(7L, iface.Size(IntPtr.Zero), "The IOStream test delegate must be callable before initialization.");

        SDL3.SDL.InitInterface(ref iface);

        TestAssert.Equal((uint)Marshal.SizeOf<SDL3.SDL.IOStreamInterface>(), iface.Version, "SDL.InitInterface must set IOStreamInterface.Version to the struct size.");
        TestAssert.Equal<SDL3.SDL.IOStreamInterface.SizeDelegate?>(null, iface.Size, "SDL.InitInterface must clear IOStreamInterface.Size.");
    }

    public static void InitInterface_StorageInterface_SetsVersionAndClearsFields()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.InitInterface), BindingFlags.Public | BindingFlags.Static, [typeof(SDL3.SDL.StorageInterface).MakeByRefType()]);
        TestAssert.NotNull(method, "SDL.InitInterface(ref StorageInterface) method must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), "SDL.InitInterface(ref StorageInterface) must keep Macro metadata.");

        SDL3.SDL.StorageInterface iface = new()
        {
            Version = 1,
            Ready = StorageReady
        };
        TestAssert.Equal(true, iface.Ready(IntPtr.Zero), "The storage test delegate must be callable before initialization.");

        SDL3.SDL.InitInterface(ref iface);

        TestAssert.Equal((uint)Marshal.SizeOf<SDL3.SDL.StorageInterface>(), iface.Version, "SDL.InitInterface must set StorageInterface.Version to the struct size.");
        TestAssert.Equal<SDL3.SDL.StorageInterface.ReadyDelegate?>(null, iface.Ready, "SDL.InitInterface must clear StorageInterface.Ready.");
    }

    public static void InitInterface_VirtualJoystickDesc_SetsVersionAndClearsFields()
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(nameof(SDL3.SDL.InitInterface), BindingFlags.Public | BindingFlags.Static, [typeof(SDL3.SDL.VirtualJoystickDesc).MakeByRefType()]);
        TestAssert.NotNull(method, "SDL.InitInterface(ref VirtualJoystickDesc) method must be public static.");
        TestAssert.NotNull(method!.GetCustomAttribute<SDL3.SDL.MacroAttribute>(), "SDL.InitInterface(ref VirtualJoystickDesc) must keep Macro metadata.");

        SDL3.SDL.VirtualJoystickDesc iface = new()
        {
            Version = 1,
            VendorID = 42,
            Name = (IntPtr)123,
            Update = VirtualJoystickUpdate
        };
        iface.Update(IntPtr.Zero);

        SDL3.SDL.InitInterface(ref iface);

        TestAssert.Equal((uint)Marshal.SizeOf<SDL3.SDL.VirtualJoystickDesc>(), iface.Version, "SDL.InitInterface must set VirtualJoystickDesc.Version to the struct size.");
        TestAssert.Equal((ushort)0, iface.VendorID, "SDL.InitInterface must clear VirtualJoystickDesc.VendorID.");
        TestAssert.Equal(IntPtr.Zero, iface.Name, "SDL.InitInterface must clear VirtualJoystickDesc.Name.");
        TestAssert.Equal<SDL3.SDL.VirtualJoystickUpdateCallback?>(null, iface.Update, "SDL.InitInterface must clear VirtualJoystickDesc.Update.");
    }

    private static long IOStreamSize(IntPtr userdata)
    {
        return 7;
    }

    private static bool StorageReady(IntPtr userdata)
    {
        return true;
    }

    private static void VirtualJoystickUpdate(IntPtr userdata)
    {
    }
}
