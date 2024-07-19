using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensors(out int count);
    public static int GetSensors(out int count) => SDL_GetSensors(out count);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorNameForID(int instanceID);
    public static string? GetSensorNameForID(int instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetSensorNameForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorTypeForID(int instanceID);
    public static SensorType GetSensorTypeForID(int instanceID) => SDL_GetSensorTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableTypeForID(int instanceID);
    public static int GetSensorNonPortableTypeForID(int instanceID) => SDL_GetSensorNonPortableTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sensor SDL_OpenSensor(int instanceID);
    public static Sensor OpenSensor(int instanceID) => SDL_OpenSensor(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sensor SDL_GetSensorFromID(int instanceID);
    public static Sensor GetSensorFromID(int instanceID) => SDL_GetSensorFromID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorProperties(IntPtr sensor);
    public static uint GetSensorProperties(Sensor sensor) => SDL_GetSensorProperties(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorName(IntPtr sensor);
    public static string? GetSensorName(Sensor sensor) => Marshal.PtrToStringUTF8(SDL_GetSensorName(sensor.Handle));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorType(IntPtr sensor);
    public static SensorType GetSensorType(Sensor sensor) => SDL_GetSensorType(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableType(IntPtr sensor);
    public static int GetSensorNonPortableType(Sensor sensor) => SDL_GetSensorNonPortableType(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorID(IntPtr sensor);
    public static uint GetSensorID(Sensor sensor) => SDL_GetSensorID(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorData(IntPtr sensor, out float data, int numValues);
    public static int GetSensorData(Sensor sensor, out float data, int numValues) =>
        SDL_GetSensorData(sensor.Handle, out data, numValues);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseSensor(IntPtr sensor);
    public static void CloseSensor(Sensor sensor) => SDL_CloseSensor(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateSensors();
    public static void UpdateSensors() => SDL_UpdateSensors();
}