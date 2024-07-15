namespace SDL3;

public static partial class SDL
{
    public enum SensorType
    {
        Invalid = -1,    /**< Returned for an invalid sensor */
        Unknown,         /**< Unknown sensor type */
        Accel,           /**< Accelerometer */
        Gyro,            /**< Gyroscope */
        AccelL,         /**< Accelerometer for left Joy-Con controller and Wii nunchuk */
        GyroL,          /**< Gyroscope for left Joy-Con controller */
        AccelR,         /**< Accelerometer for right Joy-Con controller */
        GyroR           /**< Gyroscope for right Joy-Con controller */
    }
}