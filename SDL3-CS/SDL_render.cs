using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL3;

public static partial class SDL
{
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IntPtr SDL_CreateRenderer(IntPtr window, byte* name);
    /// <summary>
    /// Create a 2D rendering context for a window.
    /// </summary>
    /// <param name="window">the window where rendering is displayed.</param>
    /// <param name="name">
    /// the name of the rendering driver to initialize,
    /// or <see cref="IntPtr.Zero"/> to initialize the first one supporting the requested flags.
    /// </param>
    /// <returns>
    /// Returns a valid rendering context or <see cref="IntPtr.Zero"/> if there was an error;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// If you want a specific renderer, you can specify its name here.
    /// A list of available renderers can be obtained by calling <see cref="GetRenderDriver"/> multiple times,
    /// with indices from 0 to <see cref="GetNumRenderDrivers"/>-1. If you don't need a specific renderer,
    /// specify <see cref="IntPtr.Zero"/> and SDL will attempt to choose the best option for you,
    /// based on what is available on the user's system.
    /// By default the rendering size matches the window size in pixels, but you can call
    /// <see cref="SetRenderLogicalPresentation"/> to change the content size and scaling options.
    /// </remarks>
    public static unsafe IntPtr CreateRenderer(IntPtr window, string? name) 
    {
        var utf8TitleBufSize = Utf8Size(name);
        var utf8Title = stackalloc byte[utf8TitleBufSize];
        return SDL_CreateRenderer(window, Utf8Encode(name, utf8Title, utf8TitleBufSize));
    }
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a);
    /// <summary>
    /// Set the color used for drawing operations.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <param name="r">the red value used to draw on the rendering target.</param>
    /// <param name="g">the green value used to draw on the rendering target.</param>
    /// <param name="b">the blue value used to draw on the rendering target.</param>
    /// <param name="a">
    /// the alpha value used to draw on the rendering target;
    /// Use <see cref="SetRenderDrawBlendMode"/> to specify how the alpha channel is used.
    /// </param>
    public static void SetRenderDrawColor(IntPtr renderer, byte r, byte g, byte b, byte a) =>
        SDL_SetRenderDrawColor(renderer, r, g, b, a);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_RenderClear(IntPtr renderer);
    /// <summary>
    /// Clear the current rendering target with the drawing color.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>
    /// Returns 0 on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// This function clears the entire rendering target, ignoring the viewport and the clip rectangle.
    /// Note, that clearing will also set/fill all pixels of the rendering target to current renderer draw color,
    /// so make sure to invoke <see cref="SetRenderDrawColor"/> when needed.
    /// </remarks>
    public static int RenderClear(IntPtr renderer) => SDL_RenderClear(renderer);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int SDL_RenderPresent(IntPtr renderer);
    /// <summary>
    /// Update the screen with any rendering performed since the previous call.
    /// </summary>
    /// <param name="renderer">the rendering context.</param>
    /// <returns>
    /// Returns 0 on success or a negative error code on failure;
    /// call <see cref="GetError"/> for more information.
    /// </returns>
    /// <remarks>
    /// SDL's rendering functions operate on a backbuffer;
    /// that is, calling a rendering function such as <see cref="RenderLine"/> does not directly put a line on
    /// the screen, but rather updates the backbuffer. As such, you compose your entire scene and present the
    /// composed backbuffer to the screen as a complete picture.
    /// Therefore, when using SDL's rendering API, one does all drawing intended for the frame, and then calls this
    /// function once per frame to present the final drawing to the user.
    /// The backbuffer should be considered invalidated after each present;
    /// do not assume that previous contents will exist between frames. Y
    /// ou are strongly encouraged to call <see cref="RenderClear"/> to initialize the backbuffer before
    /// starting each new frame's drawing, even if you plan to overwrite every pixel.
    /// </remarks>
    public static int RenderPresent(IntPtr renderer) => SDL_RenderPresent(renderer);
    
    
    [LibraryImport(SDLLibrary)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void SDL_DestroyRenderer(IntPtr renderer);
    /// <summary>
    /// Destroy the rendering context for a window and free all associated textures
    /// </summary>
    /// <remarks>
    /// This should be called before destroying the associated window.
    /// </remarks>
    /// <param name="renderer">the renderer to destroy.</param>
    public static void DestroyRenderer(IntPtr renderer) => SDL_DestroyRenderer(renderer);
}