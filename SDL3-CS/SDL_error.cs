using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IntPtr SDL_GetError();
    /// <summary>
    /// Retrieves a message with information about the specific SDL error that occurred, or an empty string if no
    /// error message has been set since the last call to <see cref="ClearError"/>.
    /// </summary>
    /// <returns>
    /// A managed string containing the error message from SDL. If there hasn't been an error set since the last
    /// call to <see cref="ClearError"/>, returns an empty string.
    /// If an error occurs during the conversion, returns null.
    /// </returns>
    /// <remarks>
    /// It is possible for multiple errors to occur before calling <see cref="GetError"/>.
    /// Only the last error is returned.
    /// The message is only applicable when an SDL function has signaled an error.
    /// You must check the return values of SDL function calls to determine when to appropriately
    /// call <see cref="GetError"/>. Do not use the results of <see cref="GetError"/> to decide if an error has
    /// occurred, as SDL may set an error string even when reporting success.
    /// SDL will not clear the error string for successful API calls. You must check return values for failure
    /// cases before you can assume the error string applies.
    /// Error strings are set per-thread, so an error set in a different thread will not interfere
    /// with the current thread's operation.
    /// The returned string is valid until the current thread's error string is changed. Therefore,
    /// if the string is to be used after calling into SDL again, the caller should make a copy of the string.
    /// </remarks>
    public static string? GetError()
    {
        return UTF8_ToManaged(SDL_GetError());
    }
}