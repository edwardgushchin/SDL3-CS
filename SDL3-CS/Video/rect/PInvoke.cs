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

public static partial class SDL
{
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
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool PointInRect(in Point p, in Rect r)
    {
        return (p.X >= r.X && p.X < (r.X + r.W) && p.Y >= r.Y && p.Y < (r.Y + r.H));
    }
    
    
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
    public static bool RectEmpty(Rect? r)
    {
        return (r is not {W: > 0} || r.Value.H <= 0);
    }
    
    
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
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool RectsEqual(Rect? a, Rect? b)
    {
        return (a.HasValue && b.HasValue &&
                a.Value.X == b.Value.X &&
                a.Value.Y == b.Value.Y &&
                a.Value.W == b.Value.W &&
                a.Value.H == b.Value.H);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_HasRectIntersection(in Rect a, in Rect b);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasRectIntersection(const SDL_Rect * A, const SDL_Rect * B);</code>
    /// <summary>
    /// <para>Determine whether two rectangles intersect.</para>
    /// <para>If either pointer is <c>null</c> the function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRectIntersection"/>
    public static bool HasRectIntersection(in Rect a, in Rect b) => SDL_HasRectIntersection(a, b);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectIntersection(in Rect a, in Rect b, out Rect result);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetRectIntersection(const SDL_Rect * A, const SDL_Rect * B, SDL_Rect * result);</code>
    /// <summary>
    /// <para>Calculate the intersection of two rectangles.</para>
    /// <para>If <c>result</c> is <c>null</c> then this function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the intersection of
    /// rectangles <c>a</c> and <c>b</c>.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasRectIntersection"/>
    public static bool GetRectIntersection(in Rect a, in Rect b, out Rect result) => SDL_GetRectIntersection(a, b, out result);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetRectUnion(in Rect a, in Rect b, out Rect result);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetRectUnion(const SDL_Rect * A, const SDL_Rect * B, SDL_Rect * result);</code>
    /// <summary>
    /// Calculate the union of two rectangles.
    /// </summary>
    /// <param name="a">an <see cref="Rect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="Rect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="Rect"/> structure filled in with the union of rectangles
    /// <c>a</c> and <c>b</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetRectUnion(in Rect a, in Rect b, out Rect result) => SDL_GetRectUnion(a, b, out result);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectEnclosingPoints([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] Point[] points, int count, ref Rect clip, out Rect result);
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
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the
    /// points were outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool GetRectEnclosingPoints(Point[] points, int count, in Rect? clip, out Rect result)
    {
        var rectClip = clip ?? default;
        return SDL_GetRectEnclosingPoints(points, count, ref rectClip, out result);
    }

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectAndLineIntersection(in Rect rect, ref int x1, ref int y1, ref int x2, ref int y2);
    /// <summary>
    /// <para>Calculate the intersection of a rectangle and line segment.</para>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <c>x1</c>, <c>y1</c>, <c>x2</c>, and/or <c>y2</c> as necessary.</para>
    /// </summary>
    /// <param name="rect">an <see cref="Rect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool GetRectAndLineIntersection(in Rect rect, ref int x1, ref int y1, ref int x2, ref int y2) =>
        SDL_GetRectAndLineIntersection(rect, ref x1, ref y1, ref x2, ref y2);
    
    
    /// <summary>
    /// <para>Determine whether a point resides inside a floating point rectangle.</para>
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
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool PointInRectFloat(FPoint? p, FRect? r)
    {
        return (p.HasValue && r.HasValue &&
                p.Value.X >= r.Value.X && p.Value.X < (r.Value.X + r.Value.W) &&
                p.Value.Y >= r.Value.Y && p.Value.Y < (r.Value.Y + r.Value.H));
    }

    
    /// <summary>
    /// <para>Determine whether a floating point rectangle has no area.</para>
    /// <para>A rectangle is considered "empty" for this function if <c>r</c> is <c>null</c>, or if
    /// <c>r</c>'s width and/or height are &lt;= 0.0f.</para>
    /// <para>Note that this is a forced-inline function in a header, and not a public
    /// API function available in the SDL library (which is to say, the code is
    /// embedded in the calling program and the linker and dynamic loader will not
    /// be able to find this function inside SDL itself).</para>
    /// </summary>
    /// <param name="r">the rectangle to test.</param>
    /// <returns><c>true</c> if the rectangle is "empty", <c>false</c> otherwise.</returns>
    /// <threadsafety>It is safe to call this function from any thread.</threadsafety>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool RectEmptyFloat(FRect? r)
    {
        return (!r.HasValue || r.Value.W <= 0.0f || r.Value.H <= 0.0f);
    }
    
    
    /// <summary>
    /// <para>Determine whether two floating point rectangles are equal, within some
    /// given epsilon.</para>
    /// <para>Rectangles are considered equal if both are not <c>null</c> and each of their x,
    /// y, width and height are within <c>epsilon</c> of each other. If you don't know
    /// what value to use for <c>epsilon</c>, you should call the <see cref="RectsEqualFloat"/>
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
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="RectsEqualFloat"/>
    public static bool RectsEqualEpsilon(FRect? a, FRect? b, float epsilon)
    {
        if (!a.HasValue || !b.HasValue) return false;
        
        var rectA = a.Value;
        var rectB = b.Value;

        return (Math.Abs(rectA.X - rectB.X) <= epsilon &&
                Math.Abs(rectA.Y - rectB.Y) <= epsilon &&
                Math.Abs(rectA.W - rectB.W) <= epsilon &&
                Math.Abs(rectA.H - rectB.H) <= epsilon);
    }
    
    
    /// <summary>
    /// <para>Determine whether two floating point rectangles are equal, within a default
    /// epsilon.</para>
    /// <para>Rectangles are considered equal if both are not <c>null</c> and each of their x,
    /// y, width and height are within <c>SDL_FLT_EPSILON</c> of each other. This is often
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
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="RectsEqualEpsilon"/>
    public static bool RectsEqualFloat(FRect? a, FRect? b) => RectsEqualEpsilon(a, b, 1.1920928955078125e-07F);
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_HasRectIntersectionFloat(in FRect a, in FRect b);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_HasRectIntersectionFloat(const SDL_FRect * A, const SDL_FRect * B);</code>
    /// <summary>
    /// <para>Determine whether two rectangles intersect with float precision.</para>
    /// <para>If either pointer is <c>null</c> the function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="GetRectIntersection"/>
    public static bool HasRectIntersectionFloat(in FRect a, in FRect b) => SDL_HasRectIntersectionFloat(a, b);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetRectIntersectionFloat(const SDL_FRect * A, const SDL_FRect * B, SDL_FRect * result);</code>
    /// <summary>
    /// <para>Calculate the intersection of two rectangles with float precision.</para>
    /// <para>If <c>result</c> is <c>null</c> then this function will return <c>false</c>.</para>
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the intersection of
    /// rectangles <c>a</c> and <c>b</c>.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    /// <seealso cref="HasRectIntersectionFloat"/>
    public static bool GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result) => 
        SDL_GetRectIntersectionFloat(a, b, out result);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int SDL_GetRectUnionFloat(in FRect a, in FRect b, out FRect result);
    /// <code>extern SDL_DECLSPEC int SDLCALL SDL_GetRectUnionFloat(const SDL_FRect * A, const SDL_FRect * B, SDL_FRect * result);</code>
    /// <summary>
    /// Calculate the union of two rectangles with float precision.
    /// </summary>
    /// <param name="a">an <see cref="FRect"/> structure representing the first rectangle.</param>
    /// <param name="b">an <see cref="FRect"/> structure representing the second rectangle.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the union of rectangles
    /// <c>a</c> and <c>b</c>.</param>
    /// <returns>0 on success or a negative error code on failure; call
    /// <see cref="GetError"/> for more information.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static int GetRectUnionFloat(in FRect a, in FRect b, out FRect result) =>
        SDL_GetRectUnionFloat(a, b, out result);

    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectEnclosingPointsFloat([MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)] FPoint[] points, int count, ref FRect clip, out FRect result);
    /// <code>extern SDL_DECLSPEC SDL_bool SDLCALL SDL_GetRectEnclosingPointsFloat(const SDL_FPoint * points, int count, const SDL_FRect * clip, SDL_FRect * result);</code>
    /// <summary>
    /// <para>Calculate a minimal rectangle enclosing a set of points with float
    /// precision.</para>
    /// <para>If <c>clip</c> is not <c>null</c> then only points inside of the clipping rectangle are
    /// considered.</para>
    /// </summary>
    /// <param name="points">an array of SDL_FPoint structures representing points to be
    /// enclosed.</param>
    /// <param name="count">the number of structures in the <c>points</c> array.</param>
    /// <param name="clip">an <see cref="FRect"/> used for clipping or NULL to enclose all points.</param>
    /// <param name="result">an <see cref="FRect"/> structure filled in with the minimal enclosing
    /// rectangle.</param>
    /// <returns><c>true</c> if any points were enclosed or <c>false</c> if all the
    /// points were outside of the clipping rectangle.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool GetRectEnclosingPointsFloat(FPoint[] points, int count, in FRect? clip, out FRect result)
    {
        var rectClip = clip ?? default;
        return SDL_GetRectEnclosingPointsFloat(points, count, ref rectClip, out result);
    }
    
    
    [LibraryImport(SDLLibrary), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool SDL_GetRectAndLineIntersectionFloat(in FRect rect, ref float x1, ref float y1, ref float x2, ref float y2);
    /// <summary>
    /// <para>Calculate the intersection of a rectangle and line segment with float
    /// precision.</para>
    /// <para>This function is used to clip a line segment to a rectangle. A line segment
    /// contained entirely within the rectangle or that does not intersect will
    /// remain unchanged. A line segment that crosses the rectangle at either or
    /// both ends will be clipped to the boundary of the rectangle and the new
    /// coordinates saved in <c>x1</c>, <c>y1</c>, <c>x2</c>, and/or <c>y2</c> as necessary.</para>
    /// </summary>
    /// <param name="rect">an <see cref="FRect"/> structure representing the rectangle to intersect.</param>
    /// <param name="x1">a pointer to the starting X-coordinate of the line.</param>
    /// <param name="y1">a pointer to the starting Y-coordinate of the line.</param>
    /// <param name="x2">a pointer to the ending X-coordinate of the line.</param>
    /// <param name="y2">a pointer to the ending Y-coordinate of the line.</param>
    /// <returns><c>true</c> if there is an intersection, <c>false</c> otherwise.</returns>
    /// <since>This function is available since SDL 3.0.0.</since>
    public static bool GetRectAndLineIntersectionFloat(in FRect rect, ref float x1, ref float y1, ref float x2, ref float y2) =>
        SDL_GetRectAndLineIntersectionFloat(rect, ref x1, ref y1, ref x2, ref y2);
}