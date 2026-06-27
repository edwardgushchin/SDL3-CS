using System.Reflection;
using System.Runtime.InteropServices;
using SDL3.Tests;

namespace SDL3.Tests.TTF;

internal static class GPUAtlasDrawSequenceTests
{
    public static void RunAll()
    {
        PublicFields_MatchSdlTtfHeader();
        Layout_MatchesNativeStructure();
    }

    public static void PublicFields_MatchSdlTtfHeader()
    {
        FieldInfo[] fields = typeof(SDL3.TTF.GPUAtlasDrawSequence).GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        string[] names = fields.Select(static field => field.Name).ToArray();

        TestAssert.Equal(
            "AtlasTexture, XY, UV, NumVertices, Indices, NumIndices, ImageType, Next",
            string.Join(", ", names),
            "TTF.GPUAtlasDrawSequence must expose every SDL_ttf draw sequence field.");
        TestAssert.Equal(typeof(IntPtr), typeof(SDL3.TTF.GPUAtlasDrawSequence).GetField(nameof(SDL3.TTF.GPUAtlasDrawSequence.Next))!.FieldType, "TTF.GPUAtlasDrawSequence.Next must be a native pointer.");
    }

    public static void Layout_MatchesNativeStructure()
    {
        int pointerSize = IntPtr.Size;
        int alignedAfterNumVertices = Align(pointerSize * 3 + sizeof(int), pointerSize);
        int numIndicesOffset = alignedAfterNumVertices + pointerSize;
        int imageTypeOffset = numIndicesOffset + sizeof(int);
        int nextOffset = Align(imageTypeOffset + sizeof(int), pointerSize);
        int expectedSize = nextOffset + pointerSize;

        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.AtlasTexture), 0);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.XY), pointerSize);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.UV), pointerSize * 2);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.NumVertices), pointerSize * 3);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.Indices), alignedAfterNumVertices);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.NumIndices), numIndicesOffset);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.ImageType), imageTypeOffset);
        AssertOffset(nameof(SDL3.TTF.GPUAtlasDrawSequence.Next), nextOffset);
        TestAssert.Equal(expectedSize, Marshal.SizeOf<SDL3.TTF.GPUAtlasDrawSequence>(), "TTF.GPUAtlasDrawSequence size must include the native next pointer.");
    }

    private static int Align(int value, int alignment)
    {
        return (value + alignment - 1) / alignment * alignment;
    }

    private static void AssertOffset(string fieldName, int expected)
    {
        int actual = Marshal.OffsetOf<SDL3.TTF.GPUAtlasDrawSequence>(fieldName).ToInt32();
        TestAssert.Equal(expected, actual, $"TTF.GPUAtlasDrawSequence.{fieldName} offset must match SDL_ttf layout.");
    }
}
