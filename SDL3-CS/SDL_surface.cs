using System.Runtime.InteropServices;

namespace SDL3;

public static partial class SDL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Surface
    {
        //SDL_SurfaceFlags flags;     /**< Read-only */
        //SDL_PixelFormat format;     /**< Read-only */
        //int w, h;                   /**< Read-only */
        //int pitch;                  /**< Read-only */
        //void *pixels;               /**< Read-only pointer, writable pixels if non-NULL */

        //int refcount;               /**< Application reference count, used when freeing surface */

        //SDL_SurfaceData *internal;  /**< Private */

    }
}