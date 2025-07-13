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

namespace SDL3;

public static partial class TTF
{
    public static class Props
    {
        public const string FontCreateFilenameString = "SDL_ttf.font.create.filename";
        public const string FontCreateIOStreamPointer = "SDL_ttf.font.create.iostream";
        public const string FontCreateIOStreamOffsetNumber = "SDL_ttf.font.create.iostream.offset";
        public const string FontCreateIOStreamAutoCloseBoolean = "SDL_ttf.font.create.iostream.autoclose";
        public const string FontCreateSizeFloat = "SDL_ttf.font.create.size";
        public const string FontCreateFaceNumber = "SDL_ttf.font.create.face";
        public const string FontCreateHorizontalDPINumber = "SDL_ttf.font.create.hdpi";
        public const string FontCreateVerticalDPINumber = "SDL_ttf.font.create.vdpi";
        public const string FontCreateExistingFontPointer = "SDL_ttf.font.create.existing_font";

        public const string FontOutlineLineCapNumber = "SDL_ttf.font.outline.line_cap";
        public const string FontOutlineLineJoinNumber = "SDL_ttf.font.outline.line_join";
        public const string FontOutlineMiterLimitNumber = "SDL_ttf.font.outline.miter_limit";

        public const string RendererTextEngineRendererPointer = "SDL_ttf.renderer_text_engine.create.renderer";
        public const string RendererTextEngineAtlasTextureSizeNumber = "SDL_ttf.renderer_text_engine.create.atlas_texture_size";

        public const string GPUTextEngineDevicePointer = "SDL_ttf.gpu_text_engine.create.device";
        public const string GPUTextEngineAtlasTextureSizeNumber = "SDL_ttf.gpu_text_engine.create.atlas_texture_size";
    }
}