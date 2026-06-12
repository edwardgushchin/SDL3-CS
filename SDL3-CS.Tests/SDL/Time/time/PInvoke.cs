using System.Reflection;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Time.Time;

internal static class PInvokeTests
{
    private static SDL3.SDL.DateFormat nextDateFormat;
    private static SDL3.SDL.TimeFormat nextTimeFormat;
    private static SDL3.SDL.DateTime nextDateTime;
    private static SDL3.SDL.DateTime capturedDateTime;
    private static bool nextBool;
    private static bool capturedLocalTime;
    private static long nextTicks;
    private static long capturedTicks;
    private static uint nextLowDateTime;
    private static uint nextHighDateTime;
    private static uint capturedLowDateTime;
    private static uint capturedHighDateTime;
    private static int nextInt;
    private static int capturedYear;
    private static int capturedMonth;
    private static int capturedDay;

    public static void RunAll()
    {
        GetDateTimeLocalePreferences_ReturnsFormatsAndNativeValue();
        GetCurrentTime_ReturnsTicksAndNativeValue();
        TimeToDateTime_ForwardsTicksLocalFlagAndReturnsDateTime();
        DateTimeToTime_ForwardsDateTimeAndReturnsTicks();
        TimeToWindows_ForwardsTicksAndReturnsFileTimeParts();
        TimeFromWindows_ForwardsFileTimePartsAndReturnsTicks();
        GetDaysInMonth_ForwardsYearMonthAndReturnsNativeValue();
        GetDayOfYear_ForwardsDateAndReturnsNativeValue();
        GetDayOfWeek_ForwardsDateAndReturnsNativeValue();
    }

    public static void GetDateTimeLocalePreferences_ReturnsFormatsAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextDateFormat = SDL3.SDL.DateFormat.DDMMYYYY;
        nextTimeFormat = SDL3.SDL.TimeFormat.Format12HR;

        using NativeHookScope _ = NativeHookScope.Install("GetDateTimeLocalePreferencesNativeFunction", nameof(CaptureDateTimeLocalePreferences));
        bool result = SDL3.SDL.GetDateTimeLocalePreferences(out SDL3.SDL.DateFormat dateFormat, out SDL3.SDL.TimeFormat timeFormat);

        TestAssert.Equal(true, result, "SDL.GetDateTimeLocalePreferences must return the native hook value.");
        TestAssert.Equal(SDL3.SDL.DateFormat.DDMMYYYY, dateFormat, "SDL.GetDateTimeLocalePreferences must return date format.");
        TestAssert.Equal(SDL3.SDL.TimeFormat.Format12HR, timeFormat, "SDL.GetDateTimeLocalePreferences must return time format.");
    }

    public static void GetCurrentTime_ReturnsTicksAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextTicks = 123456789;

        using NativeHookScope _ = NativeHookScope.Install("GetCurrentTimeNativeFunction", nameof(CaptureCurrentTime));
        bool result = SDL3.SDL.GetCurrentTime(out long ticks);

        TestAssert.Equal(true, result, "SDL.GetCurrentTime must return the native hook value.");
        TestAssert.Equal(123456789L, ticks, "SDL.GetCurrentTime must return ticks.");
    }

    public static void TimeToDateTime_ForwardsTicksLocalFlagAndReturnsDateTime()
    {
        ResetCaptureState();
        nextBool = true;
        nextDateTime = new SDL3.SDL.DateTime
        {
            Year = 2026,
            Month = 6,
            Day = 11,
            Hour = 22,
            Minute = 49,
            Second = 30,
            Nanosecond = 123,
            DayOfWeek = 4,
            UTCOffset = 10800
        };

        using NativeHookScope _ = NativeHookScope.Install("TimeToDateTimeNativeFunction", nameof(CaptureTimeToDateTime));
        bool result = SDL3.SDL.TimeToDateTime(987654321, out SDL3.SDL.DateTime dateTime, true);

        TestAssert.Equal(true, result, "SDL.TimeToDateTime must return the native hook value.");
        TestAssert.Equal(987654321L, capturedTicks, "SDL.TimeToDateTime must forward ticks.");
        TestAssert.Equal(true, capturedLocalTime, "SDL.TimeToDateTime must forward localTime.");
        TestAssert.Equal(2026, dateTime.Year, "SDL.TimeToDateTime must return DateTime.Year.");
        TestAssert.Equal(10800, dateTime.UTCOffset, "SDL.TimeToDateTime must return DateTime.UTCOffset.");
    }

    public static void DateTimeToTime_ForwardsDateTimeAndReturnsTicks()
    {
        ResetCaptureState();
        nextBool = true;
        nextTicks = 22334455;
        SDL3.SDL.DateTime dateTime = new()
        {
            Year = 2025,
            Month = 12,
            Day = 31,
            Hour = 23,
            Minute = 59,
            Second = 58,
            Nanosecond = 77,
            DayOfWeek = 3,
            UTCOffset = 0
        };

        using NativeHookScope _ = NativeHookScope.Install("DateTimeToTimeNativeFunction", nameof(CaptureDateTimeToTime));
        bool result = SDL3.SDL.DateTimeToTime(in dateTime, out long ticks);

        TestAssert.Equal(true, result, "SDL.DateTimeToTime must return the native hook value.");
        TestAssert.Equal(22334455L, ticks, "SDL.DateTimeToTime must return ticks.");
        TestAssert.Equal(2025, capturedDateTime.Year, "SDL.DateTimeToTime must forward DateTime.Year.");
        TestAssert.Equal(77, capturedDateTime.Nanosecond, "SDL.DateTimeToTime must forward DateTime.Nanosecond.");
    }

    public static void TimeToWindows_ForwardsTicksAndReturnsFileTimeParts()
    {
        ResetCaptureState();
        nextLowDateTime = 0x01020304;
        nextHighDateTime = 0xA0B0C0D0;

        using NativeHookScope _ = NativeHookScope.Install("TimeToWindowsNativeFunction", nameof(CaptureTimeToWindows));
        SDL3.SDL.TimeToWindows(99887766, out uint low, out uint high);

        TestAssert.Equal(99887766L, capturedTicks, "SDL.TimeToWindows must forward ticks.");
        TestAssert.Equal(0x01020304u, low, "SDL.TimeToWindows must return low FILETIME value.");
        TestAssert.Equal(0xA0B0C0D0u, high, "SDL.TimeToWindows must return high FILETIME value.");
    }

    public static void TimeFromWindows_ForwardsFileTimePartsAndReturnsTicks()
    {
        ResetCaptureState();
        nextTicks = 66778899;

        using NativeHookScope _ = NativeHookScope.Install("TimeFromWindowsNativeFunction", nameof(CaptureTimeFromWindows));
        long ticks = SDL3.SDL.TimeFromWindows(0x10203040, 0x50607080);

        TestAssert.Equal(66778899L, ticks, "SDL.TimeFromWindows must return the native hook value.");
        TestAssert.Equal(0x10203040u, capturedLowDateTime, "SDL.TimeFromWindows must forward low FILETIME value.");
        TestAssert.Equal(0x50607080u, capturedHighDateTime, "SDL.TimeFromWindows must forward high FILETIME value.");
    }

    public static void GetDaysInMonth_ForwardsYearMonthAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 29;

        using NativeHookScope _ = NativeHookScope.Install("GetDaysInMonthNativeFunction", nameof(CaptureYearMonthToInt));
        int result = SDL3.SDL.GetDaysInMonth(2024, 2);

        TestAssert.Equal(29, result, "SDL.GetDaysInMonth must return the native hook value.");
        TestAssert.Equal(2024, capturedYear, "SDL.GetDaysInMonth must forward year.");
        TestAssert.Equal(2, capturedMonth, "SDL.GetDaysInMonth must forward month.");
    }

    public static void GetDayOfYear_ForwardsDateAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 365;

        using NativeHookScope _ = NativeHookScope.Install("GetDayOfYearNativeFunction", nameof(CaptureDateToInt));
        int result = SDL3.SDL.GetDayOfYear(2024, 12, 31);

        TestAssert.Equal(365, result, "SDL.GetDayOfYear must return the native hook value.");
        TestAssert.Equal(2024, capturedYear, "SDL.GetDayOfYear must forward year.");
        TestAssert.Equal(12, capturedMonth, "SDL.GetDayOfYear must forward month.");
        TestAssert.Equal(31, capturedDay, "SDL.GetDayOfYear must forward day.");
    }

    public static void GetDayOfWeek_ForwardsDateAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextInt = 4;

        using NativeHookScope _ = NativeHookScope.Install("GetDayOfWeekNativeFunction", nameof(CaptureDateToInt));
        int result = SDL3.SDL.GetDayOfWeek(2026, 6, 11);

        TestAssert.Equal(4, result, "SDL.GetDayOfWeek must return the native hook value.");
        TestAssert.Equal(2026, capturedYear, "SDL.GetDayOfWeek must forward year.");
        TestAssert.Equal(6, capturedMonth, "SDL.GetDayOfWeek must forward month.");
        TestAssert.Equal(11, capturedDay, "SDL.GetDayOfWeek must forward day.");
    }

    private static bool CaptureDateTimeLocalePreferences(out SDL3.SDL.DateFormat dateFormat, out SDL3.SDL.TimeFormat timeFormat)
    {
        dateFormat = nextDateFormat;
        timeFormat = nextTimeFormat;
        return nextBool;
    }

    private static bool CaptureCurrentTime(out long ticks)
    {
        ticks = nextTicks;
        return nextBool;
    }

    private static bool CaptureTimeToDateTime(long ticks, out SDL3.SDL.DateTime dateTime, bool localTime)
    {
        capturedTicks = ticks;
        capturedLocalTime = localTime;
        dateTime = nextDateTime;
        return nextBool;
    }

    private static bool CaptureDateTimeToTime(in SDL3.SDL.DateTime dateTime, out long ticks)
    {
        capturedDateTime = dateTime;
        ticks = nextTicks;
        return nextBool;
    }

    private static void CaptureTimeToWindows(long ticks, out uint low, out uint high)
    {
        capturedTicks = ticks;
        low = nextLowDateTime;
        high = nextHighDateTime;
    }

    private static long CaptureTimeFromWindows(uint low, uint high)
    {
        capturedLowDateTime = low;
        capturedHighDateTime = high;
        return nextTicks;
    }

    private static int CaptureYearMonthToInt(int year, int month)
    {
        capturedYear = year;
        capturedMonth = month;
        return nextInt;
    }

    private static int CaptureDateToInt(int year, int month, int day)
    {
        capturedYear = year;
        capturedMonth = month;
        capturedDay = day;
        return nextInt;
    }

    private static void ResetCaptureState()
    {
        nextDateFormat = default;
        nextTimeFormat = default;
        nextDateTime = default;
        capturedDateTime = default;
        nextBool = false;
        capturedLocalTime = false;
        nextTicks = 0;
        capturedTicks = 0;
        nextLowDateTime = 0;
        nextHighDateTime = 0;
        capturedLowDateTime = 0;
        capturedHighDateTime = 0;
        nextInt = 0;
        capturedYear = 0;
        capturedMonth = 0;
        capturedDay = 0;
    }

    private sealed class NativeHookScope : IDisposable
    {
        private readonly FieldInfo field;
        private readonly object? previousValue;

        private NativeHookScope(FieldInfo field, object? hook)
        {
            this.field = field;
            previousValue = field.GetValue(null);
            field.SetValue(null, hook);
        }

        public static NativeHookScope Install(string fieldName, string methodName)
        {
            FieldInfo? field = typeof(SDL3.SDL).GetField(fieldName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(field, $"SDL private hook field {fieldName} must exist.");

            MethodInfo? method = typeof(PInvokeTests).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
            TestAssert.NotNull(method, $"Test hook method {methodName} must exist.");

            Delegate hook = Delegate.CreateDelegate(field!.FieldType, method!);

            return new NativeHookScope(field, hook);
        }

        public void Dispose()
        {
            field.SetValue(null, previousValue);
        }
    }
}
