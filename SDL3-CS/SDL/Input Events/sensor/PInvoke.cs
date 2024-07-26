#region License
/* Copyright (c) 2024 Eduard Gushchin.
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

/*
 * # CategorySensor
 *
 * SDL sensor management.
 */

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensors(out int count);
    /// <code>extern SDL_DECLSPEC SDL_SensorID *SDLCALL SDL_GetSensors(int *count);</code>
    /// <summary>
    /// <para>Get a list of currently connected sensors.</para>
    /// </summary>
    /// <param name="count">A pointer filled in with the number of sensors returned.</param>
    /// <returns>
    /// A 0 terminated array of sensor instance IDs which should be freed
    /// with <c>SDL_free()</c>, or <c>NULL</c> on error; call <see cref="GetError"/> for more
    /// details.
    /// </returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint[]? GetSensors(out int count)
    {
        var sensorArrayPtr = SDL_GetSensors(out count);
        if (sensorArrayPtr == IntPtr.Zero) return null;

        try
        {
            var sensorIds = new uint[count];
            for (var i = 0; i < count; i++)
            {
                sensorIds[i] = (uint)Marshal.ReadInt32(sensorArrayPtr, i * sizeof(uint));
            }

            return sensorIds;
        }
        finally
        {
            Free(sensorArrayPtr);
        }
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorNameForID(int instanceID);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetSensorNameForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>the sensor name, or <c>NULL</c> if <c>instanceID</c> is not valid.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetSensorNameForID(int instanceID) =>
        Marshal.PtrToStringUTF8(SDL_GetSensorNameForID(instanceID));
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorTypeForID(int instanceID);
    /// <code>extern SDL_DECLSPEC SDL_SensorType SDLCALL SDL_GetSensorTypeForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the type of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>the <see cref="SensorType"/>, or <see cref="SensorType.Invalid"/> if <c>instanceID</c> is not valid.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static SensorType GetSensorTypeForID(int instanceID) => SDL_GetSensorTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableTypeForID(int instanceID);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSensorNonPortableTypeForID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Get the platform dependent type of a sensor.</para>
    /// <para>This can be called before any sensors are opened.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>the sensor platform dependent type, or <c>-1</c> if <c>instanceID</c> is not valid.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetSensorNonPortableTypeForID(int instanceID) => SDL_GetSensorNonPortableTypeForID(instanceID);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_OpenSensor(int instanceID);

    /// <code>extern SDL_DECLSPEC SDL_Sensor *SDLCALL SDL_OpenSensor(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Open a sensor for use.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>an <see cref="Sensor"/> sensor object, or <c>NULL</c> if an error occurred.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Sensor? OpenSensor(int instanceID)
    {
        var sensorPtr = SDL_OpenSensor(instanceID);

        return sensorPtr == IntPtr.Zero ? null : new Sensor(sensorPtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorFromID(int instanceID);
    /// <code>extern SDL_DECLSPEC SDL_Sensor *SDLCALL SDL_GetSensorFromID(SDL_SensorID instance_id);</code>
    /// <summary>
    /// <para>Return the <see cref="Sensor"/> associated with an instance ID.</para>
    /// </summary>
    /// <param name="instanceID">the sensor instance ID.</param>
    /// <returns>an <see cref="Sensor"/> object.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static Sensor? GetSensorFromID(int instanceID)
    {
        var sensorPtr = SDL_GetSensorFromID(instanceID);
        return sensorPtr == IntPtr.Zero ? null : new Sensor(sensorPtr);
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorProperties(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC SDL_PropertiesID SDLCALL SDL_GetSensorProperties(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Get the properties associated with a sensor.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object.</param>
    /// <returns>a valid property ID on success or <c>0</c> on failure;
    /// call <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetSensorProperties(Sensor sensor) => SDL_GetSensorProperties(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetSensorName(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC const char *SDLCALL SDL_GetSensorName(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Get the implementation dependent name of a sensor.</para>
    /// <para>The returned string follows the
    /// <a href="https://github.com/libsdl-org/SDL/blob/main/docs/README-strings.md">SDL_GetStringRule</a>.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object.</param>
    /// <returns>the sensor name, or <c>NULL</c> if <c>sensor</c> is <c>NULL</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static string? GetSensorName(Sensor? sensor)
    {
        return Marshal.PtrToStringUTF8(sensor == null ? 
            SDL_GetSensorName(IntPtr.Zero) : 
            SDL_GetSensorName(sensor.Handle));
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType SDL_GetSensorType(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC SDL_SensorType SDLCALL SDL_GetSensorType(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Get the type of a sensor.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object to inspect.</param>
    /// <returns>the <see cref="SensorType"/> type,
    /// or <see cref="SensorType.Invalid"/> if <c>sensor</c> is <c>NULL</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static SensorType GetSensorType(Sensor? sensor)
    {
        return SDL_GetSensorType(sensor == null ? IntPtr.Zero : sensor.Handle);
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorNonPortableType(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSensorNonPortableType(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Get the platform dependent type of a sensor.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object to inspect.</param>
    /// <returns>the sensor platform dependent type, or <c>-1</c> if <c>sensor</c> is <c>NULL</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetSensorNonPortableType(Sensor? sensor)
    {
        return SDL_GetSensorNonPortableType(sensor == null ? IntPtr.Zero : sensor.Handle);
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint SDL_GetSensorID(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC SDL_SensorID SDLCALL SDL_GetSensorID(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Get the instance ID of a sensor.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object to inspect.</param>
    /// <returns>the sensor instance ID, or <c>0</c> if <c>sensor</c> is <c>NULL</c>.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static uint GetSensorID(Sensor? sensor)
    {
        return SDL_GetSensorID(sensor == null ? IntPtr.Zero : sensor.Handle);
    }


    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetSensorData(IntPtr sensor, out float data, int numValues);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetSensorData(SDL_Sensor *sensor, float *data, int num_values);</code>
    /// <summary>
    /// <para>Get the current state of an opened sensor.</para>
    /// <para>The number of values and interpretation of the data is sensor dependent.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object to query.</param>
    /// <param name="data">a pointer filled with the current sensor state.</param>
    /// <param name="numValues">the number of values to write to <c>data</c>.</param>
    /// <returns><c>0</c> on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetSensorData(Sensor sensor, out float data, int numValues) =>
        SDL_GetSensorData(sensor.Handle, out data, numValues);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_CloseSensor(IntPtr sensor);
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_CloseSensor(SDL_Sensor *sensor);</code>
    /// <summary>
    /// <para>Close a sensor previously opened with <see cref="OpenSensor"/>.</para>
    /// </summary>
    /// <param name="sensor">the <see cref="Sensor"/> object to close.</param>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void CloseSensor(Sensor sensor) => SDL_CloseSensor(sensor.Handle);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void SDL_UpdateSensors();
    /// <code>extern SDL_DECLSPEC void SDLCALL SDL_UpdateSensors(void);</code>
    /// <summary>
    /// <para>Update the current state of the open sensors.</para>
    /// <para>This is called automatically by the event loop if sensor events are enabled.</para>
    /// <para>This needs to be called from the thread that initialized the sensor subsystem.</para>
    /// </summary>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static void UpdateSensors() => SDL_UpdateSensors();
}