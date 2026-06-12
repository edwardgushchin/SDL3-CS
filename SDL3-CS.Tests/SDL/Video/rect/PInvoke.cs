using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.SDL.Video.Rect;

internal static class PInvokeTests
{
    private static SDL3.SDL.Rect capturedRectA;
    private static SDL3.SDL.Rect capturedRectB;
    private static SDL3.SDL.Rect capturedRect;
    private static SDL3.SDL.Rect capturedClipRect;
    private static SDL3.SDL.Rect nextRect;
    private static SDL3.SDL.FRect capturedFRectA;
    private static SDL3.SDL.FRect capturedFRectB;
    private static SDL3.SDL.FRect capturedFRect;
    private static SDL3.SDL.FRect capturedFClipRect;
    private static SDL3.SDL.FRect nextFRect;
    private static SDL3.SDL.Point[]? capturedPoints;
    private static SDL3.SDL.FPoint[]? capturedFPoints;
    private static IntPtr capturedClipPointer;
    private static int capturedCount;
    private static bool nextBool;
    private static int capturedX1;
    private static int capturedY1;
    private static int capturedX2;
    private static int capturedY2;
    private static int nextX1;
    private static int nextY1;
    private static int nextX2;
    private static int nextY2;
    private static float capturedFX1;
    private static float capturedFY1;
    private static float capturedFX2;
    private static float capturedFY2;
    private static float nextFX1;
    private static float nextFY1;
    private static float nextFX2;
    private static float nextFY2;

    public static void RunAll()
    {
        NativeEntryPoints_KeepExpectedLibraryImportMetadata();
        RectToFRect_ConvertsIntegerFieldsToFloatFields();
        PointInRect_ReturnsExpectedValuesForNullsAndEdges();
        RectEmpty_ReturnsExpectedValuesForNullAndNonPositiveSize();
        RectsEqual_ReturnsExpectedValuesForNullsAndFieldDifferences();
        HasRectIntersection_ForwardsRectsAndReturnsNativeValue();
        GetRectIntersection_ForwardsRectsReturnsResultAndNativeValue();
        GetRectUnion_ForwardsRectsReturnsResultAndNativeValue();
        GetRectEnclosingPoints_WithPointerClipForwardsArrayClipAndResult();
        GetRectEnclosingPoints_WithRectClipForwardsArrayClipAndResult();
        GetRectAndLineIntersection_ForwardsLineMutatesCoordinatesAndReturnsNativeValue();
        PointInRectFloat_ReturnsExpectedValuesForNullsAndEdges();
        RectEmptyFloat_ReturnsExpectedValuesForNullAndNonPositiveSize();
        RectsEqualEpsilon_ReturnsExpectedValuesForNullsAndFieldDifferences();
        RectsEqualFloat_UsesDefaultEpsilon();
        HasRectIntersectionFloat_ForwardsRectsAndReturnsNativeValue();
        GetRectIntersectionFloat_ForwardsRectsReturnsResultAndNativeValue();
        GetRectUnionFloat_ForwardsRectsReturnsResultAndNativeValue();
        GetRectEnclosingPointsFloat_WithPointerClipForwardsArrayClipAndResult();
        GetRectEnclosingPointsFloat_WithRectClipForwardsArrayClipAndResult();
        GetRectAndLineIntersectionFloat_ForwardsLineMutatesCoordinatesAndReturnsNativeValue();
    }

    public static void NativeEntryPoints_KeepExpectedLibraryImportMetadata()
    {
        AssertNativeBoolImport(GetNativeMethod("SDL_HasRectIntersection"), "SDL_HasRectIntersection");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectIntersection"), "SDL_GetRectIntersection");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectUnion"), "SDL_GetRectUnion");

        MethodInfo enclosingPointsPointer = GetNativeMethod("SDL_GetRectEnclosingPointsPointer");
        AssertNativeBoolImport(enclosingPointsPointer, "SDL_GetRectEnclosingPoints");
        AssertArrayMarshal(enclosingPointsPointer);
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectEnclosingPointsSpanPointer"), "SDL_GetRectEnclosingPoints");

        MethodInfo enclosingPointsClip = GetNativeMethod("SDL_GetRectEnclosingPointsClip");
        AssertNativeBoolImport(enclosingPointsClip, "SDL_GetRectEnclosingPoints");
        AssertArrayMarshal(enclosingPointsClip);
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectEnclosingPointsSpanClip"), "SDL_GetRectEnclosingPoints");

        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectAndLineIntersection"), "SDL_GetRectAndLineIntersection");
        AssertNativeBoolImport(GetNativeMethod("SDL_HasRectIntersectionFloat"), "SDL_HasRectIntersectionFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectIntersectionFloat"), "SDL_GetRectIntersectionFloat");
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectUnionFloat"), "SDL_GetRectUnionFloat");

        MethodInfo enclosingFloatPointsPointer = GetNativeMethod("SDL_GetRectEnclosingPointsFloatPointer");
        AssertNativeBoolImport(enclosingFloatPointsPointer, "SDL_GetRectEnclosingPointsFloat");
        AssertArrayMarshal(enclosingFloatPointsPointer);
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectEnclosingPointsFloatSpanPointer"), "SDL_GetRectEnclosingPointsFloat");

        MethodInfo enclosingFloatPointsClip = GetNativeMethod("SDL_GetRectEnclosingPointsFloatClip");
        AssertNativeBoolImport(enclosingFloatPointsClip, "SDL_GetRectEnclosingPointsFloat");
        AssertArrayMarshal(enclosingFloatPointsClip);
        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectEnclosingPointsFloatSpanClip"), "SDL_GetRectEnclosingPointsFloat");

        AssertNativeBoolImport(GetNativeMethod("SDL_GetRectAndLineIntersectionFloat"), "SDL_GetRectAndLineIntersectionFloat");
    }

    public static void RectToFRect_ConvertsIntegerFieldsToFloatFields()
    {
        SDL3.SDL.Rect rect = CreateRect(-3, 4, 5, 6);

        SDL3.SDL.RectToFRect(in rect, out SDL3.SDL.FRect result);

        AssertFRect(CreateFRect(-3.0f, 4.0f, 5.0f, 6.0f), result, "SDL.RectToFRect must map every field.");
    }

    public static void PointInRect_ReturnsExpectedValuesForNullsAndEdges()
    {
        SDL3.SDL.Point? noPoint = null;
        SDL3.SDL.Rect? noRect = null;
        SDL3.SDL.Rect? rect = CreateRect(10, 20, 5, 6);
        SDL3.SDL.Point? inside = CreatePoint(12, 23);
        SDL3.SDL.Point? topLeft = CreatePoint(10, 20);
        SDL3.SDL.Point? beforeLeft = CreatePoint(9, 23);
        SDL3.SDL.Point? rightEdge = CreatePoint(15, 23);
        SDL3.SDL.Point? aboveTop = CreatePoint(12, 19);
        SDL3.SDL.Point? bottomEdge = CreatePoint(12, 26);

        TestAssert.Equal(false, SDL3.SDL.PointInRect(in noPoint, in rect), "SDL.PointInRect must reject a null point.");
        TestAssert.Equal(false, SDL3.SDL.PointInRect(in inside, in noRect), "SDL.PointInRect must reject a null rect.");
        TestAssert.Equal(true, SDL3.SDL.PointInRect(in inside, in rect), "SDL.PointInRect must accept an interior point.");
        TestAssert.Equal(true, SDL3.SDL.PointInRect(in topLeft, in rect), "SDL.PointInRect must include the top-left edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRect(in beforeLeft, in rect), "SDL.PointInRect must reject points before the left edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRect(in rightEdge, in rect), "SDL.PointInRect must reject the right edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRect(in aboveTop, in rect), "SDL.PointInRect must reject points above the top edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRect(in bottomEdge, in rect), "SDL.PointInRect must reject the bottom edge.");
    }

    public static void RectEmpty_ReturnsExpectedValuesForNullAndNonPositiveSize()
    {
        SDL3.SDL.Rect? noRect = null;
        SDL3.SDL.Rect? zeroWidth = CreateRect(1, 2, 0, 4);
        SDL3.SDL.Rect? zeroHeight = CreateRect(1, 2, 3, 0);
        SDL3.SDL.Rect? positive = CreateRect(1, 2, 3, 4);

        TestAssert.Equal(true, SDL3.SDL.RectEmpty(in noRect), "SDL.RectEmpty must treat null as empty.");
        TestAssert.Equal(true, SDL3.SDL.RectEmpty(in zeroWidth), "SDL.RectEmpty must treat zero width as empty.");
        TestAssert.Equal(true, SDL3.SDL.RectEmpty(in zeroHeight), "SDL.RectEmpty must treat zero height as empty.");
        TestAssert.Equal(false, SDL3.SDL.RectEmpty(in positive), "SDL.RectEmpty must reject positive area as empty.");
    }

    public static void RectsEqual_ReturnsExpectedValuesForNullsAndFieldDifferences()
    {
        SDL3.SDL.Rect? noRect = null;
        SDL3.SDL.Rect? a = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Rect? same = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Rect? differentX = CreateRect(9, 2, 3, 4);
        SDL3.SDL.Rect? differentY = CreateRect(1, 9, 3, 4);
        SDL3.SDL.Rect? differentW = CreateRect(1, 2, 9, 4);
        SDL3.SDL.Rect? differentH = CreateRect(1, 2, 3, 9);

        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in noRect, in same), "SDL.RectsEqual must reject null first rect.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in a, in noRect), "SDL.RectsEqual must reject null second rect.");
        TestAssert.Equal(true, SDL3.SDL.RectsEqual(in a, in same), "SDL.RectsEqual must accept matching fields.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in a, in differentX), "SDL.RectsEqual must compare X.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in a, in differentY), "SDL.RectsEqual must compare Y.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in a, in differentW), "SDL.RectsEqual must compare W.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqual(in a, in differentH), "SDL.RectsEqual must compare H.");
    }

    public static void HasRectIntersection_ForwardsRectsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.Rect a = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Rect b = CreateRect(5, 6, 7, 8);

        using NativeHookScope _ = NativeHookScope.Install("HasRectIntersectionNativeFunction", nameof(CaptureHasRectIntersection));
        bool result = SDL3.SDL.HasRectIntersection(in a, in b);

        TestAssert.Equal(true, result, "SDL.HasRectIntersection must return the native hook value.");
        AssertRect(a, capturedRectA, "SDL.HasRectIntersection must forward first rect.");
        AssertRect(b, capturedRectB, "SDL.HasRectIntersection must forward second rect.");
    }

    public static void GetRectIntersection_ForwardsRectsReturnsResultAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(2, 3, 4, 5);
        SDL3.SDL.Rect a = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Rect b = CreateRect(5, 6, 7, 8);

        using NativeHookScope _ = NativeHookScope.Install("GetRectIntersectionNativeFunction", nameof(CaptureGetRectIntersection));
        bool result = SDL3.SDL.GetRectIntersection(in a, in b, out SDL3.SDL.Rect intersection);

        TestAssert.Equal(true, result, "SDL.GetRectIntersection must return the native hook value.");
        AssertRect(a, capturedRectA, "SDL.GetRectIntersection must forward first rect.");
        AssertRect(b, capturedRectB, "SDL.GetRectIntersection must forward second rect.");
        AssertRect(nextRect, intersection, "SDL.GetRectIntersection must output result.");
    }

    public static void GetRectUnion_ForwardsRectsReturnsResultAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(1, 2, 9, 10);
        SDL3.SDL.Rect a = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Rect b = CreateRect(5, 6, 7, 8);

        using NativeHookScope _ = NativeHookScope.Install("GetRectUnionNativeFunction", nameof(CaptureGetRectUnion));
        bool result = SDL3.SDL.GetRectUnion(in a, in b, out SDL3.SDL.Rect union);

        TestAssert.Equal(true, result, "SDL.GetRectUnion must return the native hook value.");
        AssertRect(a, capturedRectA, "SDL.GetRectUnion must forward first rect.");
        AssertRect(b, capturedRectB, "SDL.GetRectUnion must forward second rect.");
        AssertRect(nextRect, union, "SDL.GetRectUnion must output result.");
    }

    public static void GetRectEnclosingPoints_WithPointerClipForwardsArrayClipAndResult()
    {
        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(1, 2, 3, 4);
        SDL3.SDL.Point[] points = [CreatePoint(1, 2), CreatePoint(3, 4)];

        using NativeHookScope _ = NativeHookScope.Install("GetRectEnclosingPointsPointerNativeFunction", nameof(CaptureGetRectEnclosingPointsPointer));
        bool result = SDL3.SDL.GetRectEnclosingPoints(points, points.Length, (IntPtr)0x1234, out SDL3.SDL.Rect enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPoints(IntPtr) must return the native hook value.");
        TestAssert.Equal(points.Length, capturedCount, "SDL.GetRectEnclosingPoints(IntPtr) must forward count.");
        TestAssert.Equal((IntPtr)0x1234, capturedClipPointer, "SDL.GetRectEnclosingPoints(IntPtr) must forward clip pointer.");
        AssertPoints(points, capturedPoints, "SDL.GetRectEnclosingPoints(IntPtr) must forward points.");
        AssertRect(nextRect, enclosing, "SDL.GetRectEnclosingPoints(IntPtr) must output result.");

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(6, 7, 8, 9);
        using NativeHookScope pointerHook = NativeHookScope.Install("GetRectEnclosingPointsSpanPointerNativeFunction", nameof(CaptureGetRectEnclosingPointsSpanPointer));
        result = SDL3.SDL.GetRectEnclosingPoints(points.AsSpan(1), 1, (IntPtr)0x1235, out enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, IntPtr) must return the native hook value.");
        TestAssert.Equal(1, capturedCount, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, IntPtr) must forward count.");
        TestAssert.Equal((IntPtr)0x1235, capturedClipPointer, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, IntPtr) must forward clip pointer.");
        AssertPoints([points[1]], capturedPoints, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, IntPtr) must forward point slice.");
        AssertRect(nextRect, enclosing, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, IntPtr) must output result.");
    }

    public static void GetRectEnclosingPoints_WithRectClipForwardsArrayClipAndResult()
    {
        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(5, 6, 7, 8);
        SDL3.SDL.Point[] points = [CreatePoint(10, 11), CreatePoint(12, 13)];
        SDL3.SDL.Rect clip = CreateRect(1, 2, 30, 40);

        using NativeHookScope _ = NativeHookScope.Install("GetRectEnclosingPointsClipNativeFunction", nameof(CaptureGetRectEnclosingPointsClip));
        bool result = SDL3.SDL.GetRectEnclosingPoints(points, points.Length, in clip, out SDL3.SDL.Rect enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPoints(in Rect) must return the native hook value.");
        TestAssert.Equal(points.Length, capturedCount, "SDL.GetRectEnclosingPoints(in Rect) must forward count.");
        AssertPoints(points, capturedPoints, "SDL.GetRectEnclosingPoints(in Rect) must forward points.");
        AssertRect(clip, capturedClipRect, "SDL.GetRectEnclosingPoints(in Rect) must forward clip rect.");
        AssertRect(nextRect, enclosing, "SDL.GetRectEnclosingPoints(in Rect) must output result.");

        ResetCaptureState();
        nextBool = true;
        nextRect = CreateRect(10, 11, 12, 13);
        using NativeHookScope pointerHook = NativeHookScope.Install("GetRectEnclosingPointsSpanClipNativeFunction", nameof(CaptureGetRectEnclosingPointsSpanClip));
        result = SDL3.SDL.GetRectEnclosingPoints(points.AsSpan(1), 1, in clip, out enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, in Rect) must return the native hook value.");
        TestAssert.Equal(1, capturedCount, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, in Rect) must forward count.");
        AssertPoints([points[1]], capturedPoints, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, in Rect) must forward point slice.");
        AssertRect(clip, capturedClipRect, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, in Rect) must forward clip rect.");
        AssertRect(nextRect, enclosing, "SDL.GetRectEnclosingPoints(ReadOnlySpan<Point>, in Rect) must output result.");
    }

    public static void GetRectAndLineIntersection_ForwardsLineMutatesCoordinatesAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextX1 = 2;
        nextY1 = 3;
        nextX2 = 4;
        nextY2 = 5;
        SDL3.SDL.Rect rect = CreateRect(1, 2, 30, 40);
        int x1 = -10;
        int y1 = -20;
        int x2 = 50;
        int y2 = 60;

        using NativeHookScope _ = NativeHookScope.Install("GetRectAndLineIntersectionNativeFunction", nameof(CaptureGetRectAndLineIntersection));
        bool result = SDL3.SDL.GetRectAndLineIntersection(in rect, ref x1, ref y1, ref x2, ref y2);

        TestAssert.Equal(true, result, "SDL.GetRectAndLineIntersection must return the native hook value.");
        AssertRect(rect, capturedRect, "SDL.GetRectAndLineIntersection must forward rect.");
        TestAssert.Equal(-10, capturedX1, "SDL.GetRectAndLineIntersection must forward x1.");
        TestAssert.Equal(-20, capturedY1, "SDL.GetRectAndLineIntersection must forward y1.");
        TestAssert.Equal(50, capturedX2, "SDL.GetRectAndLineIntersection must forward x2.");
        TestAssert.Equal(60, capturedY2, "SDL.GetRectAndLineIntersection must forward y2.");
        TestAssert.Equal(2, x1, "SDL.GetRectAndLineIntersection must update x1.");
        TestAssert.Equal(3, y1, "SDL.GetRectAndLineIntersection must update y1.");
        TestAssert.Equal(4, x2, "SDL.GetRectAndLineIntersection must update x2.");
        TestAssert.Equal(5, y2, "SDL.GetRectAndLineIntersection must update y2.");
    }

    public static void PointInRectFloat_ReturnsExpectedValuesForNullsAndEdges()
    {
        SDL3.SDL.FPoint? noPoint = null;
        SDL3.SDL.FRect? noRect = null;
        SDL3.SDL.FRect? rect = CreateFRect(10.0f, 20.0f, 5.0f, 6.0f);
        SDL3.SDL.FPoint? inside = CreateFPoint(12.5f, 23.5f);
        SDL3.SDL.FPoint? topLeft = CreateFPoint(10.0f, 20.0f);
        SDL3.SDL.FPoint? beforeLeft = CreateFPoint(9.99f, 23.5f);
        SDL3.SDL.FPoint? rightEdge = CreateFPoint(15.0f, 23.5f);
        SDL3.SDL.FPoint? aboveTop = CreateFPoint(12.5f, 19.99f);
        SDL3.SDL.FPoint? bottomEdge = CreateFPoint(12.5f, 26.0f);

        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in noPoint, in rect), "SDL.PointInRectFloat must reject a null point.");
        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in inside, in noRect), "SDL.PointInRectFloat must reject a null rect.");
        TestAssert.Equal(true, SDL3.SDL.PointInRectFloat(in inside, in rect), "SDL.PointInRectFloat must accept an interior point.");
        TestAssert.Equal(true, SDL3.SDL.PointInRectFloat(in topLeft, in rect), "SDL.PointInRectFloat must include the top-left edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in beforeLeft, in rect), "SDL.PointInRectFloat must reject points before the left edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in rightEdge, in rect), "SDL.PointInRectFloat must reject the right edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in aboveTop, in rect), "SDL.PointInRectFloat must reject points above the top edge.");
        TestAssert.Equal(false, SDL3.SDL.PointInRectFloat(in bottomEdge, in rect), "SDL.PointInRectFloat must reject the bottom edge.");
    }

    public static void RectEmptyFloat_ReturnsExpectedValuesForNullAndNonPositiveSize()
    {
        SDL3.SDL.FRect? noRect = null;
        SDL3.SDL.FRect? zeroWidth = CreateFRect(1.0f, 2.0f, 0.0f, 4.0f);
        SDL3.SDL.FRect? zeroHeight = CreateFRect(1.0f, 2.0f, 3.0f, 0.0f);
        SDL3.SDL.FRect? positive = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);

        TestAssert.Equal(true, SDL3.SDL.RectEmptyFloat(in noRect), "SDL.RectEmptyFloat must treat null as empty.");
        TestAssert.Equal(true, SDL3.SDL.RectEmptyFloat(in zeroWidth), "SDL.RectEmptyFloat must treat zero width as empty.");
        TestAssert.Equal(true, SDL3.SDL.RectEmptyFloat(in zeroHeight), "SDL.RectEmptyFloat must treat zero height as empty.");
        TestAssert.Equal(false, SDL3.SDL.RectEmptyFloat(in positive), "SDL.RectEmptyFloat must reject positive area as empty.");
    }

    public static void RectsEqualEpsilon_ReturnsExpectedValuesForNullsAndFieldDifferences()
    {
        SDL3.SDL.FRect? noRect = null;
        SDL3.SDL.FRect? a = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect? same = CreateFRect(1.05f, 2.05f, 3.05f, 4.05f);
        SDL3.SDL.FRect? differentX = CreateFRect(1.2f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect? differentY = CreateFRect(1.0f, 2.2f, 3.0f, 4.0f);
        SDL3.SDL.FRect? differentW = CreateFRect(1.0f, 2.0f, 3.2f, 4.0f);
        SDL3.SDL.FRect? differentH = CreateFRect(1.0f, 2.0f, 3.0f, 4.2f);
        float epsilon = 0.1f;

        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in noRect, in same, in epsilon), "SDL.RectsEqualEpsilon must reject null first rect.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in a, in noRect, in epsilon), "SDL.RectsEqualEpsilon must reject null second rect.");
        TestAssert.Equal(true, SDL3.SDL.RectsEqualEpsilon(in a, in same, in epsilon), "SDL.RectsEqualEpsilon must accept values within epsilon.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in a, in differentX, in epsilon), "SDL.RectsEqualEpsilon must compare X.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in a, in differentY, in epsilon), "SDL.RectsEqualEpsilon must compare Y.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in a, in differentW, in epsilon), "SDL.RectsEqualEpsilon must compare W.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualEpsilon(in a, in differentH, in epsilon), "SDL.RectsEqualEpsilon must compare H.");
    }

    public static void RectsEqualFloat_UsesDefaultEpsilon()
    {
        SDL3.SDL.FRect? a = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect? same = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect? different = CreateFRect(1.01f, 2.0f, 3.0f, 4.0f);

        TestAssert.Equal(true, SDL3.SDL.RectsEqualFloat(in a, in same), "SDL.RectsEqualFloat must use the default epsilon for equal values.");
        TestAssert.Equal(false, SDL3.SDL.RectsEqualFloat(in a, in different), "SDL.RectsEqualFloat must reject values outside the default epsilon.");
    }

    public static void HasRectIntersectionFloat_ForwardsRectsAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        SDL3.SDL.FRect a = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect b = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);

        using NativeHookScope _ = NativeHookScope.Install("HasRectIntersectionFloatNativeFunction", nameof(CaptureHasRectIntersectionFloat));
        bool result = SDL3.SDL.HasRectIntersectionFloat(in a, in b);

        TestAssert.Equal(true, result, "SDL.HasRectIntersectionFloat must return the native hook value.");
        AssertFRect(a, capturedFRectA, "SDL.HasRectIntersectionFloat must forward first rect.");
        AssertFRect(b, capturedFRectB, "SDL.HasRectIntersectionFloat must forward second rect.");
    }

    public static void GetRectIntersectionFloat_ForwardsRectsReturnsResultAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(2.0f, 3.0f, 4.0f, 5.0f);
        SDL3.SDL.FRect a = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect b = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);

        using NativeHookScope _ = NativeHookScope.Install("GetRectIntersectionFloatNativeFunction", nameof(CaptureGetRectIntersectionFloat));
        bool result = SDL3.SDL.GetRectIntersectionFloat(in a, in b, out SDL3.SDL.FRect intersection);

        TestAssert.Equal(true, result, "SDL.GetRectIntersectionFloat must return the native hook value.");
        AssertFRect(a, capturedFRectA, "SDL.GetRectIntersectionFloat must forward first rect.");
        AssertFRect(b, capturedFRectB, "SDL.GetRectIntersectionFloat must forward second rect.");
        AssertFRect(nextFRect, intersection, "SDL.GetRectIntersectionFloat must output result.");
    }

    public static void GetRectUnionFloat_ForwardsRectsReturnsResultAndNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(1.0f, 2.0f, 9.0f, 10.0f);
        SDL3.SDL.FRect a = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FRect b = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);

        using NativeHookScope _ = NativeHookScope.Install("GetRectUnionFloatNativeFunction", nameof(CaptureGetRectUnionFloat));
        bool result = SDL3.SDL.GetRectUnionFloat(in a, in b, out SDL3.SDL.FRect union);

        TestAssert.Equal(true, result, "SDL.GetRectUnionFloat must return the native hook value.");
        AssertFRect(a, capturedFRectA, "SDL.GetRectUnionFloat must forward first rect.");
        AssertFRect(b, capturedFRectB, "SDL.GetRectUnionFloat must forward second rect.");
        AssertFRect(nextFRect, union, "SDL.GetRectUnionFloat must output result.");
    }

    public static void GetRectEnclosingPointsFloat_WithPointerClipForwardsArrayClipAndResult()
    {
        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(1.0f, 2.0f, 3.0f, 4.0f);
        SDL3.SDL.FPoint[] points = [CreateFPoint(1.0f, 2.0f), CreateFPoint(3.0f, 4.0f)];

        using NativeHookScope _ = NativeHookScope.Install("GetRectEnclosingPointsFloatPointerNativeFunction", nameof(CaptureGetRectEnclosingPointsFloatPointer));
        bool result = SDL3.SDL.GetRectEnclosingPointsFloat(points, points.Length, (IntPtr)0x5678, out SDL3.SDL.FRect enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPointsFloat(IntPtr) must return the native hook value.");
        TestAssert.Equal(points.Length, capturedCount, "SDL.GetRectEnclosingPointsFloat(IntPtr) must forward count.");
        TestAssert.Equal((IntPtr)0x5678, capturedClipPointer, "SDL.GetRectEnclosingPointsFloat(IntPtr) must forward clip pointer.");
        AssertFPoints(points, capturedFPoints, "SDL.GetRectEnclosingPointsFloat(IntPtr) must forward points.");
        AssertFRect(nextFRect, enclosing, "SDL.GetRectEnclosingPointsFloat(IntPtr) must output result.");

        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(6.5f, 7.5f, 8.5f, 9.5f);
        using NativeHookScope pointerHook = NativeHookScope.Install("GetRectEnclosingPointsFloatSpanPointerNativeFunction", nameof(CaptureGetRectEnclosingPointsFloatSpanPointer));
        result = SDL3.SDL.GetRectEnclosingPointsFloat(points.AsSpan(1), 1, (IntPtr)0x5679, out enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, IntPtr) must return the native hook value.");
        TestAssert.Equal(1, capturedCount, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, IntPtr) must forward count.");
        TestAssert.Equal((IntPtr)0x5679, capturedClipPointer, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, IntPtr) must forward clip pointer.");
        AssertFPoints([points[1]], capturedFPoints, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, IntPtr) must forward point slice.");
        AssertFRect(nextFRect, enclosing, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, IntPtr) must output result.");
    }

    public static void GetRectEnclosingPointsFloat_WithRectClipForwardsArrayClipAndResult()
    {
        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(5.0f, 6.0f, 7.0f, 8.0f);
        SDL3.SDL.FPoint[] points = [CreateFPoint(10.0f, 11.0f), CreateFPoint(12.0f, 13.0f)];
        SDL3.SDL.FRect clip = CreateFRect(1.0f, 2.0f, 30.0f, 40.0f);

        using NativeHookScope _ = NativeHookScope.Install("GetRectEnclosingPointsFloatClipNativeFunction", nameof(CaptureGetRectEnclosingPointsFloatClip));
        bool result = SDL3.SDL.GetRectEnclosingPointsFloat(points, points.Length, in clip, out SDL3.SDL.FRect enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPointsFloat(in FRect) must return the native hook value.");
        TestAssert.Equal(points.Length, capturedCount, "SDL.GetRectEnclosingPointsFloat(in FRect) must forward count.");
        AssertFPoints(points, capturedFPoints, "SDL.GetRectEnclosingPointsFloat(in FRect) must forward points.");
        AssertFRect(clip, capturedFClipRect, "SDL.GetRectEnclosingPointsFloat(in FRect) must forward clip rect.");
        AssertFRect(nextFRect, enclosing, "SDL.GetRectEnclosingPointsFloat(in FRect) must output result.");

        ResetCaptureState();
        nextBool = true;
        nextFRect = CreateFRect(10.5f, 11.5f, 12.5f, 13.5f);
        using NativeHookScope pointerHook = NativeHookScope.Install("GetRectEnclosingPointsFloatSpanClipNativeFunction", nameof(CaptureGetRectEnclosingPointsFloatSpanClip));
        result = SDL3.SDL.GetRectEnclosingPointsFloat(points.AsSpan(1), 1, in clip, out enclosing);

        TestAssert.Equal(true, result, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, in FRect) must return the native hook value.");
        TestAssert.Equal(1, capturedCount, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, in FRect) must forward count.");
        AssertFPoints([points[1]], capturedFPoints, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, in FRect) must forward point slice.");
        AssertFRect(clip, capturedFClipRect, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, in FRect) must forward clip rect.");
        AssertFRect(nextFRect, enclosing, "SDL.GetRectEnclosingPointsFloat(ReadOnlySpan<FPoint>, in FRect) must output result.");
    }

    public static void GetRectAndLineIntersectionFloat_ForwardsLineMutatesCoordinatesAndReturnsNativeValue()
    {
        ResetCaptureState();
        nextBool = true;
        nextFX1 = 2.5f;
        nextFY1 = 3.5f;
        nextFX2 = 4.5f;
        nextFY2 = 5.5f;
        SDL3.SDL.FRect rect = CreateFRect(1.0f, 2.0f, 30.0f, 40.0f);
        float x1 = -10.5f;
        float y1 = -20.5f;
        float x2 = 50.5f;
        float y2 = 60.5f;

        using NativeHookScope _ = NativeHookScope.Install("GetRectAndLineIntersectionFloatNativeFunction", nameof(CaptureGetRectAndLineIntersectionFloat));
        bool result = SDL3.SDL.GetRectAndLineIntersectionFloat(in rect, ref x1, ref y1, ref x2, ref y2);

        TestAssert.Equal(true, result, "SDL.GetRectAndLineIntersectionFloat must return the native hook value.");
        AssertFRect(rect, capturedFRect, "SDL.GetRectAndLineIntersectionFloat must forward rect.");
        TestAssert.Equal(-10.5f, capturedFX1, "SDL.GetRectAndLineIntersectionFloat must forward x1.");
        TestAssert.Equal(-20.5f, capturedFY1, "SDL.GetRectAndLineIntersectionFloat must forward y1.");
        TestAssert.Equal(50.5f, capturedFX2, "SDL.GetRectAndLineIntersectionFloat must forward x2.");
        TestAssert.Equal(60.5f, capturedFY2, "SDL.GetRectAndLineIntersectionFloat must forward y2.");
        TestAssert.Equal(2.5f, x1, "SDL.GetRectAndLineIntersectionFloat must update x1.");
        TestAssert.Equal(3.5f, y1, "SDL.GetRectAndLineIntersectionFloat must update y1.");
        TestAssert.Equal(4.5f, x2, "SDL.GetRectAndLineIntersectionFloat must update x2.");
        TestAssert.Equal(5.5f, y2, "SDL.GetRectAndLineIntersectionFloat must update y2.");
    }

    private static bool CaptureHasRectIntersection(in SDL3.SDL.Rect a, in SDL3.SDL.Rect b)
    {
        capturedRectA = a;
        capturedRectB = b;
        return nextBool;
    }

    private static bool CaptureGetRectIntersection(in SDL3.SDL.Rect a, in SDL3.SDL.Rect b, out SDL3.SDL.Rect result)
    {
        capturedRectA = a;
        capturedRectB = b;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectUnion(in SDL3.SDL.Rect a, in SDL3.SDL.Rect b, out SDL3.SDL.Rect result)
    {
        capturedRectA = a;
        capturedRectB = b;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsPointer(SDL3.SDL.Point[] points, int count, IntPtr clip, out SDL3.SDL.Rect result)
    {
        capturedPoints = [.. points];
        capturedCount = count;
        capturedClipPointer = clip;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsSpanPointer(IntPtr points, int count, IntPtr clip, out SDL3.SDL.Rect result)
    {
        capturedPoints = CopyUnmanaged<SDL3.SDL.Point>(points, count);
        capturedCount = count;
        capturedClipPointer = clip;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsClip(SDL3.SDL.Point[] points, int count, in SDL3.SDL.Rect clip, out SDL3.SDL.Rect result)
    {
        capturedPoints = [.. points];
        capturedCount = count;
        capturedClipRect = clip;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsSpanClip(IntPtr points, int count, in SDL3.SDL.Rect clip, out SDL3.SDL.Rect result)
    {
        capturedPoints = CopyUnmanaged<SDL3.SDL.Point>(points, count);
        capturedCount = count;
        capturedClipRect = clip;
        result = nextRect;
        return nextBool;
    }

    private static bool CaptureGetRectAndLineIntersection(in SDL3.SDL.Rect rect, ref int x1, ref int y1, ref int x2, ref int y2)
    {
        capturedRect = rect;
        capturedX1 = x1;
        capturedY1 = y1;
        capturedX2 = x2;
        capturedY2 = y2;
        x1 = nextX1;
        y1 = nextY1;
        x2 = nextX2;
        y2 = nextY2;
        return nextBool;
    }

    private static bool CaptureHasRectIntersectionFloat(in SDL3.SDL.FRect a, in SDL3.SDL.FRect b)
    {
        capturedFRectA = a;
        capturedFRectB = b;
        return nextBool;
    }

    private static bool CaptureGetRectIntersectionFloat(in SDL3.SDL.FRect a, in SDL3.SDL.FRect b, out SDL3.SDL.FRect result)
    {
        capturedFRectA = a;
        capturedFRectB = b;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectUnionFloat(in SDL3.SDL.FRect a, in SDL3.SDL.FRect b, out SDL3.SDL.FRect result)
    {
        capturedFRectA = a;
        capturedFRectB = b;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsFloatPointer(SDL3.SDL.FPoint[] points, int count, IntPtr clip, out SDL3.SDL.FRect result)
    {
        capturedFPoints = [.. points];
        capturedCount = count;
        capturedClipPointer = clip;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsFloatSpanPointer(IntPtr points, int count, IntPtr clip, out SDL3.SDL.FRect result)
    {
        capturedFPoints = CopyUnmanaged<SDL3.SDL.FPoint>(points, count);
        capturedCount = count;
        capturedClipPointer = clip;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsFloatClip(SDL3.SDL.FPoint[] points, int count, in SDL3.SDL.FRect clip, out SDL3.SDL.FRect result)
    {
        capturedFPoints = [.. points];
        capturedCount = count;
        capturedFClipRect = clip;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectEnclosingPointsFloatSpanClip(IntPtr points, int count, in SDL3.SDL.FRect clip, out SDL3.SDL.FRect result)
    {
        capturedFPoints = CopyUnmanaged<SDL3.SDL.FPoint>(points, count);
        capturedCount = count;
        capturedFClipRect = clip;
        result = nextFRect;
        return nextBool;
    }

    private static bool CaptureGetRectAndLineIntersectionFloat(in SDL3.SDL.FRect rect, ref float x1, ref float y1, ref float x2, ref float y2)
    {
        capturedFRect = rect;
        capturedFX1 = x1;
        capturedFY1 = y1;
        capturedFX2 = x2;
        capturedFY2 = y2;
        x1 = nextFX1;
        y1 = nextFY1;
        x2 = nextFX2;
        y2 = nextFY2;
        return nextBool;
    }

    private static SDL3.SDL.Rect CreateRect(int x, int y, int w, int h)
    {
        return new SDL3.SDL.Rect { X = x, Y = y, W = w, H = h };
    }

    private static SDL3.SDL.Point CreatePoint(int x, int y)
    {
        return new SDL3.SDL.Point { X = x, Y = y };
    }

    private static SDL3.SDL.FRect CreateFRect(float x, float y, float w, float h)
    {
        return new SDL3.SDL.FRect { X = x, Y = y, W = w, H = h };
    }

    private static SDL3.SDL.FPoint CreateFPoint(float x, float y)
    {
        return new SDL3.SDL.FPoint { X = x, Y = y };
    }

    private static void AssertRect(SDL3.SDL.Rect expected, SDL3.SDL.Rect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void AssertFRect(SDL3.SDL.FRect expected, SDL3.SDL.FRect actual, string message)
    {
        TestAssert.Equal(expected.X, actual.X, $"{message} X.");
        TestAssert.Equal(expected.Y, actual.Y, $"{message} Y.");
        TestAssert.Equal(expected.W, actual.W, $"{message} W.");
        TestAssert.Equal(expected.H, actual.H, $"{message} H.");
    }

    private static void AssertPoints(SDL3.SDL.Point[] expected, SDL3.SDL.Point[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i].X, actual[i].X, $"{message} [{i}].X.");
            TestAssert.Equal(expected[i].Y, actual[i].Y, $"{message} [{i}].Y.");
        }
    }

    private static void AssertFPoints(SDL3.SDL.FPoint[] expected, SDL3.SDL.FPoint[]? actual, string message)
    {
        TestAssert.NotNull(actual, message);
        TestAssert.Equal(expected.Length, actual!.Length, $"{message} Length.");
        for (int i = 0; i < expected.Length; i++)
        {
            TestAssert.Equal(expected[i].X, actual[i].X, $"{message} [{i}].X.");
            TestAssert.Equal(expected[i].Y, actual[i].Y, $"{message} [{i}].Y.");
        }
    }

    private static void ResetCaptureState()
    {
        capturedRectA = default;
        capturedRectB = default;
        capturedRect = default;
        capturedClipRect = default;
        nextRect = default;
        capturedFRectA = default;
        capturedFRectB = default;
        capturedFRect = default;
        capturedFClipRect = default;
        nextFRect = default;
        capturedPoints = null;
        capturedFPoints = null;
        capturedClipPointer = IntPtr.Zero;
        capturedCount = 0;
        nextBool = false;
        capturedX1 = 0;
        capturedY1 = 0;
        capturedX2 = 0;
        capturedY2 = 0;
        nextX1 = 0;
        nextY1 = 0;
        nextX2 = 0;
        nextY2 = 0;
        capturedFX1 = 0.0f;
        capturedFY1 = 0.0f;
        capturedFX2 = 0.0f;
        capturedFY2 = 0.0f;
        nextFX1 = 0.0f;
        nextFY1 = 0.0f;
        nextFX2 = 0.0f;
        nextFY2 = 0.0f;
    }

    private static MethodInfo GetNativeMethod(string methodName)
    {
        MethodInfo? method = typeof(SDL3.SDL).GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Static);
        TestAssert.NotNull(method, $"SDL.{methodName} method must be private static.");
        return method!;
    }

    private static void AssertNativeBoolImport(MethodInfo method, string entryPoint)
    {
        AssertSdlLibraryImport(method, entryPoint);
        AssertBoolReturnMarshal(method);
        AssertExcludedFromCoverage(method);
    }

    private static void AssertSdlLibraryImport(MethodInfo method, string entryPoint)
    {
        LibraryImportAttribute? libraryImport = method.GetCustomAttribute<LibraryImportAttribute>();
        TestAssert.NotNull(libraryImport, $"SDL.{method.Name} must keep LibraryImport metadata.");
        TestAssert.Equal("SDL3", libraryImport!.LibraryName, $"SDL.{method.Name} must import from SDL3.");
        TestAssert.Equal(entryPoint, libraryImport.EntryPoint, $"SDL.{method.Name} must bind {entryPoint}.");
    }

    private static void AssertBoolReturnMarshal(MethodInfo method)
    {
        MarshalAsAttribute? marshalAs = method.ReturnParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} return value must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.I1, marshalAs!.Value, $"SDL.{method.Name} return value must use I1 marshalling.");
    }

    private static void AssertArrayMarshal(MethodInfo method)
    {
        ParameterInfo pointsParameter = method.GetParameters()[0];
        MarshalAsAttribute? marshalAs = pointsParameter.GetCustomAttribute<MarshalAsAttribute>();
        TestAssert.NotNull(marshalAs, $"SDL.{method.Name} points parameter must keep MarshalAs metadata.");
        TestAssert.Equal(UnmanagedType.LPArray, marshalAs!.Value, $"SDL.{method.Name} points parameter must use LPArray marshalling.");
        TestAssert.Equal(1, marshalAs.SizeParamIndex, $"SDL.{method.Name} points parameter must use count as SizeParamIndex.");
    }

    private static void AssertExcludedFromCoverage(MethodInfo method)
    {
        ExcludeFromCodeCoverageAttribute? attribute = method.GetCustomAttribute<ExcludeFromCodeCoverageAttribute>();
        TestAssert.NotNull(attribute, $"SDL.{method.Name} native stub must be excluded from code coverage.");
    }

    private static unsafe T[] CopyUnmanaged<T>(IntPtr pointer, int count) where T : unmanaged
    {
        if (pointer == IntPtr.Zero || count <= 0)
        {
            return [];
        }

        T[] result = new T[count];
        new ReadOnlySpan<T>((void*)pointer, count).CopyTo(result);
        return result;
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
