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

public static partial class Mixer
{
    public class Props
    {
        public const string AudioLoadIOStreamPointer = "SDL_mixer.audio.load.iostream";
        public const string AudioLoadCloseIOBoolean = "SDL_mixer.audio.load.closeio";
        public const string AudioLoadPreDecodeBoolean = "SDL_mixer.audio.load.predecode";
        public const string AudioLoadPreferredMixerPointer = "SDL_mixer.audio.load.Preferredmixer";
        public const string AudioLoadSkipMetadataTagsBoolean = "SDL_mixer.audio.load.SkipMetadatatags";
        public const string AudioDecoderString = "SDL_mixer.audio.decoder";

        public const string MetadataTitleString = "SDL_mixer.metadata.title";
        public const string MetadataArtistString = "SDL_mixer.metadata.artist";
        public const string MetadataAlbumString = "SDL_mixer.metadata.album";
        public const string MetadataCopyrightString = "SDL_mixer.metadata.copyright";
        public const string MetadataTrackNumber = "SDL_mixer.metadata.track";
        public const string MetadataTotalTrackSNumber = "SDL_mixer.metadata.total_tracks";
        public const string MetadataYearNumber = "SDL_mixer.metadata.year";
        public const string MetadataDurationFramesNumber = "SDL_mixer.metadata.duration_frames";
        public const string MetadataDurationInfiniteBoolean = "SDL_mixer.metadata.duration_infinite";

        public const string PlayLoopsNumber = "SDL_mixer.play.loops";
        public const string PlayMaxFrameNumber = "SDL_mixer.play.max_frame";
        public const string PlayMaxMillisecondsNumber = "SDL_mixer.play.max_milliseconds";
        public const string PlayStartFrameNumber = "SDL_mixer.play.start_frame";
        public const string PlayStartMillisecondNumber = "SDL_mixer.play.start_millisecond";
        public const string PlayLoopStartFrameNumber = "SDL_mixer.play.Loopstart_frame";
        public const string PlayLoopStartMillisecondNumber = "SDL_mixer.play.Loopstart_millisecond";
        public const string PlayFadeInFramesNumber = "SDL_mixer.play.fade_in_frames";
        public const string PlayFadeInMillisecondsNumber = "SDL_mixer.play.fade_in_milliseconds";
        public const string PlayAppendSilenceFramesNumber = "SDL_mixer.play.append_silence_frames";
        public const string PlayAppendSilenceMillisecondsNumber = "SDL_mixer.play.append_silence_milliseconds";
    }
}