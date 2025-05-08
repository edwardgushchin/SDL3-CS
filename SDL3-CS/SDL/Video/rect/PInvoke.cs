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
    /// <code>SDL_FORCE_INLINE void SDL_RectToFRect(const SDL_Rect *rect, SDL_FRect *frect)</code>
    /// <summary>
    /// Convert an <see cref="Rect"/> to <see cref="FRect"/>
    /// </summary>
    /// <param name="rect">a pointer to an <see cref="Rect"/>.</param>
    /// <param name="frect">a pointer filled in with the floating point representation of
    /// <c>rect</c>.</param>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static void RectToFRect(in Rect rect, out FRect frect)
    {
        frect = new FRect
        {
            X = rect.X,
            Y = rect.Y,
            W = rect.W,
            H = rect.H
        };
    }
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_PointInRect(const SDL_Point *p, const SDL_Rect *r)</code>
    /// <summary>
    /// <para>Determine whether a point resides inside a rectangle.</para>
    /// <para>A point is considered part of a rectangle if both <c>p</c> and <c>r</c> are not <c>null</c>,
    /// and <c>p</c>'s x and y coordinates are >= to the rectangle's top left corner,
    /// and &lt; the rectangle's x+w and y+h. So a 1x1 rectangle considers point (0,0)
    /// as "inside" and (0,1) as not.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="p">the point to test.</param>
    /// <param name="r">the rectangle to test.</param>
    /// <returns><c>true</c> if <c>p</c> is contained by <c>r</c>, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool PointInRect(in Point? p, in Rect? r)
    {
        if (p == null || r == null)  return false;

        return (p.Value.X >= r.Value.X) &&
               (p.Value.X < (r.Value.X + r.Value.W)) &&
               (p.Value.Y >= r.Value.Y) &&
               (p.Value.Y < (r.Value.Y + r.Value.H));
    }
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_RectEmpty(const SDL_Rect *r)</code>
    /// <summary>
    /// <para>Determine whether a rectangle has no area.</para>
    /// <para>A rectangle is considered "empty" for this function if <c>r</c> is <c>null</c>, or if
    /// <c>r</c>'s width and/or height are &lt;= 0.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="r">the rectangle to test.</param>
    /// <returns><c>true</c> if the rectangle is "empty", <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool RectEmpty(in Rect? r)
    {
        return (r is not {W: > 0} || r.Value.H <= 0);
    }
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_RectsEqual(const SDL_Rect *a, const SDL_Rect *b)</code>
    /// <summary>
    /// <para>Determine whether two rectangles are equal.</para>
    /// <para>Rectangles are considered equal if both are not <c>null</c> and each of their x,
    /// y, width and height match.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <returns><c>true</c> if the rectangles are equal, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool RectsEqual(in Rect? a, in Rect? b)
    {
        return (a.HasValue && b.HasValue && (a.Value.X == b.Value.X) && (a.Value.Y == b.Value.Y) 
                && (a.Value.W == b.Value.W) && (a.Value.H == b.Value.H));
    }
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasRectIntersection(const SDL_Rect *A, const SDL_Rect *B);</code>
    /// <summary>
    /// <para>Determine whether two rectangles intersect.</para>
    /// <para>If either pointer is <c>null</c> the function will return false.</para>
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRectIntersection"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasRectIntersection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasRectIntersection(in Rect a, in Rect b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectIntersection(const SDL_Rect *A, const SDL_Rect *B, SDL_Rect *result);</code>
    /// <summary>
    /// <para>Calculate the intersection of two rectangles.</para>
    /// <para>If <c>result</c> is <c>null</c> then this function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the intersection of
    /// rectangles <c>a</c> and <c>b</c>.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasRectIntersection"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectIntersection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectIntersection(in Rect a, in Rect b, out Rect result);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectUnion(const SDL_Rect *A, const SDL_Rect *B, SDL_Rect *result);</code>
    /// <summary>
    /// Calculate the union of two rectangles.
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the union of rectangles
    /// <c>A</c> and <c>B</c>.</param>
    /// <returns>true on success or false on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectUnion"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectUnion(in Rect a, in Rect b, out Rect result);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectEnclosingPoints(const SDL_Point *points, int count, const SDL_Rect *clip, SDL_Rect *result);</code>
    /// <summary>
    /// <para>Calculate a minimal rectangle enclosing a set of points.</para>
    /// <para>If <c>clip</c> is not <c>null</c> then only points inside of the clipping rectangle are
    /// considered.</para>
    /// </summary>
    /// <param name="points">an array of <see cref="Point"/> structures representing points to be
    /// enclosed.</param>
    /// <param name="count">the number of structures in the <c>points</c> array.</param>
    /// <param name="clip">an <see cref="Rect"/> used for clipping or <c>null</c> to enclose all points.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the minimal enclosing
    /// rectangle.</param>
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the points were
    /// outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectEnclosingPoints"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectEnclosingPoints([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] Point[] points, int count, IntPtr clip, out Rect result);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectEnclosingPoints(const SDL_Point *points, int count, const SDL_Rect *clip, SDL_Rect *result);</code>
    /// <summary>
    /// <para>Calculate a minimal rectangle enclosing a set of points.</para>
    /// <para>If <c>clip</c> is not <c>null</c> then only points inside of the clipping rectangle are
    /// considered.</para>
    /// </summary>
    /// <param name="points">an array of <see cref="Point"/> structures representing points to be
    /// enclosed.</param>
    /// <param name="count">the number of structures in the <c>points</c> array.</param>
    /// <param name="clip">an <see cref="Rect"/> used for clipping or <c>null</c> to enclose all points.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the minimal enclosing
    /// rectangle.</param>
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the points were
    /// outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectEnclosingPoints"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectEnclosingPoints([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] Point[] points, int count, in Rect clip, out Rect result);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectAndLineIntersection(const SDL_Rect *rect, int *X1, int *Y1, int *X2, int *Y2);</code>
    /// <summary>
    /// <para>Calculate the intersection of a rectangle and line segment.</para>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <c>X1</c>, <c>Y1</c>, <c>X2</c>, and/or <c>Y2</c> as necessary.</para>
    /// </summary>
    /// <param name="rect">an <seealso cref="Rect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectAndLineIntersection"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectAndLineIntersection(in Rect rect, ref int x1, ref int y1, ref int x2, ref int y2);
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_PointInRectFloat(const SDL_FPoint *p, const SDL_FRect *r)</code>
    /// <summary>
    /// <para>Determine whether a point resides inside a floating point rectangle.</para>
    /// <para>A point is considered part of a rectangle if both <c>p</c> and <c>r</c> are not <c>null</c>,
    /// and <c>p</c>'s x and y coordinates are >= to the rectangle's top left corner,
    /// and &lt;= the rectangle's x+w and y+h. So a 1x1 rectangle considers point
    /// (0,0) and (0,1) as "inside" and (0,2) as not.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="p">the point to test.</param>
    /// <param name="r">the rectangle to test.</param>
    /// <returns>true if <c>p</c> is contained by <c>r</c>, false otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool PointInRectFloat(in FPoint? p, in FRect? r)
    {
        return (p.HasValue && r.HasValue &&
                p.Value.X >= r.Value.X && p.Value.X < (r.Value.X + r.Value.W) &&
                p.Value.Y >= r.Value.Y && p.Value.Y < (r.Value.Y + r.Value.H));
    }

    
    /// <code>SDL_FORCE_INLINE bool SDL_RectEmptyFloat(const SDL_FRect *r)</code>
    /// <summary>
    /// <para>Determine whether a floating point rectangle takes no space.</para>
    /// <para>A rectangle is considered "empty" for this function if <c>r</c> is <c>null</c>, or if
    /// <c>r</c>'s width and/or height are &lt; 0.0f.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="r">the rectangle to test.</param>
    /// <returns><c>true</c> if the rectangle is "empty", <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    public static bool RectEmptyFloat(in FRect? r)
    {
        return (!r.HasValue || r.Value.W <= 0.0f || r.Value.H <= 0.0f);
    }
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_RectsEqualEpsilon(const SDL_FRect *a, const SDL_FRect *b, const float epsilon)</code>
    /// <summary>
    /// <para>Determine whether two floating point rectangles are equal, within some
    /// given epsilon.</para>
    /// <para>Rectangles are considered equal if both are not <c>null</c> and each of their x,
    /// y, width and height are within <c>epsilon</c> of each other. If you don't know
    /// what value to use for <c>epsilon</c>, you should call the SDL_RectsEqualFloat
    /// function instead.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <param name="epsilon">the epsilon value for comparison.</param>
    /// <returns><c>true</c> if the rectangles are equal, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RectsEqualFloat"/>
    public static bool RectsEqualEpsilon(in FRect? a, in FRect? b, in float epsilon)
    {
        if (!a.HasValue || !b.HasValue) return false;
        
        var rectA = a.Value;
        var rectB = b.Value;

        return (Math.Abs(rectA.X - rectB.X) <= epsilon &&
                Math.Abs(rectA.Y - rectB.Y) <= epsilon &&
                Math.Abs(rectA.W - rectB.W) <= epsilon &&
                Math.Abs(rectA.H - rectB.H) <= epsilon);
    }
    
    
    /// <code>SDL_FORCE_INLINE bool SDL_RectsEqualFloat(const SDL_FRect *a, const SDL_FRect *b)</code>
    /// <summary>
    /// <para>Determine whether two floating point rectangles are equal, within a default
    /// epsilon.</para>
    /// <para>Rectangles are considered equal if both are not <c>null</c> and each of their x,
    /// y, width and height are within <see cref="FloatEpsilon"/> of each other. This is often
    /// a reasonable way to compare two floating point rectangles and deal with the
    /// slight precision variations in floating point calculations that tend to pop
    /// up.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="a">the first rectangle to test.</param>
    /// <param name="b">the second rectangle to test.</param>
    /// <returns><c>true</c> if the rectangles are equal, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="RectsEqualEpsilon"/>
    public static bool RectsEqualFloat(in FRect? a, in FRect? b) => RectsEqualEpsilon(a, b, FloatEpsilon);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasRectIntersectionFloat(const SDL_FRect *A, const SDL_FRect *B);</code>
    /// <summary>
    /// <para>Determine whether two rectangles intersect with float precision.</para>
    /// <para>If either pointer is <c>null</c> the function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="GetRectIntersection"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_HasRectIntersectionFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasRectIntersectionFloat(in FRect a, in FRect b);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectIntersectionFloat(const SDL_FRect *A, const SDL_FRect *B, SDL_FRect *result);</code>
    /// <summary>
    /// <para>Calculate the intersection of two rectangles with float precision.</para>
    /// <para>If <c>result</c> is <c>null</c> then this function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the intersection of
    /// rectangles <c>a</c> and <c>b</c>.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    /// <seealso cref="HasRectIntersectionFloat"/>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectIntersectionFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result);
    

    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectUnionFloat(const SDL_FRect *A, const SDL_FRect *B, SDL_FRect *result);</code>
    /// <summary>
    /// Calculate the union of two rectangles with float precision.
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the union of rectangles
    /// <c>A</c> and <c>B</c>.</param>
    /// <returns><c>true</c> on success or <c>false</c> on failure; call <see cref="GetError"/> for more
    /// information.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectUnionFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectUnionFloat(in FRect a, in FRect b, out FRect result);

    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectEnclosingPointsFloat(const SDL_FPoint *points, int count, const SDL_FRect *clip, SDL_FRect *result);</code>
    /// <summary>
    /// <para>Calculate a minimal rectangle enclosing a set of points with float
    /// precision.</para>
    /// <para>If <c>clip</c> is not <c>null</c> then only points inside of the clipping rectangle are
    /// considered.</para>
    /// </summary>
    /// <param name="points">an array of <see cref="FPoint"/> structures representing points to be
    /// enclosed.</param>
    /// <param name="count">the number of structures in the <c>points</c> array.</param>
    /// <param name="clip">an <see cref="FRect"/> used for clipping or <c>null</c> to enclose all points.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the minimal enclosing
    /// rectangle.</param>
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the points were
    /// outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectEnclosingPointsFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectEnclosingPointsFloat([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] FPoint[] points, int count, IntPtr clip, out FRect result);
    
    
    /// <code>extern SDL_DECLSPEC bool SDLCALL SDL_GetRectEnclosingPointsFloat(const SDL_FPoint *points, int count, const SDL_FRect *clip, SDL_FRect *result);</code>
    /// <summary>
    /// <para>Calculate a minimal rectangle enclosing a set of points with float
    /// precision.</para>
    /// <para>If <c>clip</c> is not <c>null</c> then only points inside of the clipping rectangle are
    /// considered.</para>
    /// </summary>
    /// <param name="points">an array of <see cref="FPoint"/> structures representing points to be
    /// enclosed.</param>
    /// <param name="count">the number of structures in the <c>points</c> array.</param>
    /// <param name="clip">an <see cref="FRect"/> used for clipping or <c>null</c> to enclose all points.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the minimal enclosing
    /// rectangle.</param>
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the points were
    /// outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.2.0</since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectEnclosingPointsFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectEnclosingPointsFloat([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] FPoint[] points, int count, in FRect clip, out FRect result);
    
    
    /// <summary>
    /// <para>Calculate the intersection of a rectangle and line segment with float
    /// precision.</para>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <c>X1</c>, <c>Y1</c>, <c>X2</c>, and/or <c>Y2</c> as necessary.</para>
    /// </summary>
    /// <param name="rect">an <seealso cref="FRect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since></since>
    [LibraryImport(SDLLibrary, EntryPoint = "SDL_GetRectAndLineIntersectionFloat"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetRectAndLineIntersectionFloat(in FRect rect, ref float x1, ref float y1, ref float x2, ref float y2);
}