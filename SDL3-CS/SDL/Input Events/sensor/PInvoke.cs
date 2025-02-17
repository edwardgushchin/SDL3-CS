#region License
/* Copyright (c) 2024-2025 Eduard Gushchin.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 */
#endregion

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensors"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensors(out int count);
    /// <code>extern SDL_DECLSPEC SDL_SensorID * SDLCALL SDL_GetSensors(int *count);</code>
    /// <summary>
    /// Get a list of currently connected sensors.
    /// </summary>
    /// <param name="count">a pointer filled in with the number of sensors returned, may
    /// be <c>null</c>.</param>
    /// <returns>a 0 terminated array of sensor instance IDs or <c>null</c> on failure;
    /// call <see cref="GetError"/> for more information. This should be freed
    /// with <see cref="Free"/> when it is no longer needed.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static uint[]? GetSensors(out int count)
    {
        var ptr = SDL_GetSensors(out count);
        
        try
        {
            return PointerToStructureArray<uint>(ptr, count);
        }
        finally
        {
            if (ptr != IntPtr.Zero) Free(ptr);
        }
    }
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorNameForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorNameForID(int instanceId);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetSensorNameForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// </summary>
    /// <param name="instanceId">the sensor instance ID.</param>
    /// <returns>the sensor name, or <c>null</c> if <c>instanceID</c> is not valid.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetSensorNameForID(int instanceId)
    {
        var value = SDL_GetSensorNameForID(instanceId); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }
    
    
    /// <code>extern SDL_DECLSPEC SDL_SensorType SDLCALL SDL_GetSensorTypeForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>the <c>SensorType</c>, or <see cref="SensorType.Invalid"/> if <c>instanceID</c> is
    /// not valid.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SensorType GetSensorTypeForID(int instanceID);
    
    
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSensorNonPortableTypeForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the platform dependent type of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>the sensor platform dependent type, or -1 if <c>instanceID</c> is not
    /// valid.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorNonPortableTypeForID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSensorNonPortableTypeForID(int instanceID);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Sensor * SDLCALL SDL_OpenSensor(SDL_SensorID instance_id);</code>
    /// <summary>
    /// Open a sensor for use.
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>an SDL_Sensor object or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_OpenSensor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr OpenSensor(int instanceID);
    
    
    /// <code>extern SDL_DECLSPEC SDL_Sensor * SDLCALL SDL_GetSensorFromID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// Return the SDL_Sensor associated with an instance ID.
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>an SDL_Sensor object or <c>null</c> on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorFromID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial IntPtr GetSensorFromID(int instanceID);
    
    
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetSensorProperties(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Get the properties associated with a sensor.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object.</param>
    /// <returns>a valid property ID on success or 0 on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorProperties"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetSensorProperties(IntPtr sensor);
    
    
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorName"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorName(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC const char * SDLCALL SDL_GetSensorName(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Get the implementation dependent name of a sensor.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object.</param>
    /// <returns>the sensor name or <c>null</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    public static string? GetSensorName(IntPtr sensor)
    {
        var value = SDL_GetSensorName(sensor); 
        return value == IntPtr.Zero ? null : Marshal.PtrToStringUTF8(value);
    }


    /// <code>extern SDL_DECLSPEC SDL_SensorType SDLCALL SDL_GetSensorType(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Get the type of a sensor.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <returns>the <see cref="SensorType"/> type, or <see cref="SensorType.Invalid"/> if <c>sensor</c> is
    /// <c>null</c>.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "GetSensorType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial SensorType GetSensorType(IntPtr sensor);


    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSensorNonPortableType(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Get the platform dependent type of a sensor.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <returns>the sensor platform dependent type, or -1 if <c>sensor</c> is <c>null</c>.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorNonPortableType"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSensorNonPortableType(IntPtr sensor);
    

    /// <code>extern SDL_DECLSPEC SDL_SensorID SDLCALL SDL_GetSensorID(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Get the instance ID of a sensor.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object to inspect.</param>
    /// <returns>the sensor instance ID, or 0 on failure; call <see cref="GetError"/> for
    /// more information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorID"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetSensorID(IntPtr sensor);


    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetSensorData(SDL_Sensor *sensor, float *data, int num_values);</code>
    /// <summary>
    /// <para>Get the current state of an opened sensor.</para>
    /// <para>The number of values and interpretation of the data is sensor dependent.</para>
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object to query.</param>
    /// <param name="data">a pointer filled with the current sensor state.</param>
    /// <param name="numValues">the number of values to write to data.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetSensorData"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetSensorData(IntPtr sensor, out float data, int numValues);
    
    
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseSensor(SDL_Sensor *sensor);</code>
    /// <summary>
    /// Close a sensor previously opened with <see cref="OpenSensor"/>.
    /// </summary>
    /// <param name="sensor">the SDL_Sensor object to close.</param>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_CloseSensor"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void CloseSensor(IntPtr sensor);
    

    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateSensors(void);</code>
    /// <summary>
    /// <para>Update the current state of the open sensors.</para>
    /// <para>This is called automatically by the event loop if sensor events are
    /// enabled.</para>
    /// <para>This needs to be called from the thread that initialized the sensor
    /// subsystem.</para>
    /// </summary>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_UpdateSensors"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UpdateSensors();
}