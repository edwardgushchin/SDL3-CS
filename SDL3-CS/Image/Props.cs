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

public partial class Image
{
    public static class Props
    {
        public const string AnimationEncoderCreateFilenameString = "SDL_image.animation_encoder.create.filename";
        public const string AnimationEncoderCreateIOStreamPointer = "SDL_image.animation_encoder.create.iostream";
        public const string AnimationEncoderCreateIOStreamAutoCloseBoolean = "SDL_image.animation_encoder.create.iostream.autoclose";
        public const string AnimationEncoderCreateTypeString = "SDL_image.animation_encoder.create.type";
        public const string AnimationEncoderCreateQualityNumber = "SDL_image.animation_encoder.create.quality";
        public const string AnimationEncoderCreateTimebaseNumeratorNumber = "SDL_image.animation_encoder.create.timebase.numerator";
        public const string AnimationEncoderCreateTimebaseDenominatorNumber = "SDL_image.animation_encoder.create.timebase.denominator";

        public const string AnimationDecoderCreateFilenameString = "SDL_image.animation_decoder.create.filename";
        public const string AnimationDecoderCreateIOStreamPointer = "SDL_image.animation_decoder.create.iostream";
        public const string AnimationDecoderCreateIOStreamAutoCloseBoolean = "SDL_image.animation_decoder.create.iostream.autoclose";
        public const string AnimationDecoderCreateTypeString = "SDL_image.animation_decoder.create.type";
        public const string AnimationDecoderCreateTimebaseNumeratorNumber = "SDL_image.animation_decoder.create.timebase.numerator";
        public const string AnimationDecoderCreateTimebaseDenominatorNumber = "SDL_image.animation_decoder.create.timebase.denominator";

        public const string MetadataIgnorePropsBoolean = "SDL_image.metadata.ignore_props";
        public const string MetadataDescriptionString = "SDL_image.metadata.description";
        public const string MetadataCopyrightString = "SDL_image.metadata.copyright";
        public const string MetadataTitleString = "SDL_image.metadata.title";
        public const string MetadataAuthorStringG = "SDL_image.metadata.author";
        public const string MetadataCreationTimeString = "SDL_image.metadata.creation_time";
        public const string MetadataLoopCountNumber = "SDL_image.metadata.loop_count";
    }
}

