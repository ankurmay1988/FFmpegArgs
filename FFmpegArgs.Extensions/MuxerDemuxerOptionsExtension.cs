using System;
using System.Linq;
using FFmpegArgs.Cores;

namespace FFmpegArgs
{
    /// <summary>
    /// Common muxer (output) / demuxer (input) format-specific options.
    /// <br></br>
    /// Note: there were placeholder projects <c>FFmpegArgs.Inputs.Demuxers</c> / <c>FFmpegArgs.Outputs.Muxers</c>
    /// that are no longer part of the solution; these format-specific options live here as extensions on
    /// <c>BaseInput</c> / <c>BaseOutput</c>, consistent with <see cref="InputOutputOptionsExtension"/>.
    /// </summary>
    public static class MuxerDemuxerOptionsExtension
    {
        #region Demuxer (input)
        /// <summary>
        /// -re<br></br>
        /// Read input at the native frame rate. Mainly used to simulate a live/realtime input
        /// (e.g. when streaming or when testing time-bounded behavior).
        /// </summary>
        public static T Re<T>(this T t) where T : BaseInput
            => t.SetFlag("-re");

        /// <summary>
        /// -start_number (image2 demuxer)<br></br>
        /// Set the index of the file matched by the image file pattern to start to read from.
        /// </summary>
        /// <exception cref="InvalidRangeException"></exception>
        public static T StartNumber<T>(this T t, int number) where T : BaseInput
            => t.SetOptionRange("-start_number", number, 0, int.MaxValue);

        /// <summary>
        /// -pattern_type (image2 demuxer)<br></br>
        /// Select how the image file pattern is interpreted (sequence numbering, glob, etc.).
        /// </summary>
        public static T PatternType<T>(this T t, Image2PatternType patternType) where T : BaseInput
            => t.SetOption("-pattern_type", patternType.ToString());
        #endregion


        #region Muxer (output)
        /// <summary>
        /// -movflags (mp4/mov muxer)<br></br>
        /// Set one or more MOV/MP4 muxer flags. The flags are emitted in the additive (<c>+flag</c>) form,
        /// e.g. <c>-movflags +faststart+empty_moov</c>. The most common is <see cref="MovFlag.faststart"/>
        /// (moves the moov atom to the start of the file for progressive/web playback).
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public static T MovFlags<T>(this T t, params MovFlag[] flags) where T : BaseOutput
        {
            if (flags is null || flags.Length == 0) throw new ArgumentNullException(nameof(flags));
            return t.SetOption("-movflags", string.Concat(flags.Select(f => "+" + f.ToString())));
        }
        #endregion
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    /// <summary>
    /// -pattern_type values for the image2 demuxer.
    /// </summary>
    public enum Image2PatternType
    {
        /// <summary>Select a sequence of files numbered by a printf-style pattern (e.g. img-%03d.png).</summary>
        sequence,
        /// <summary>Select all files matching a glob pattern (e.g. *.png).</summary>
        glob,
        /// <summary>Try glob first, then fall back to sequence (deprecated in newer ffmpeg).</summary>
        glob_sequence,
        /// <summary>The pattern is treated as a single literal file name.</summary>
        none,
    }

    /// <summary>
    /// -movflags values for the MOV/MP4/ISMV family of muxers.
    /// </summary>
    public enum MovFlag
    {
        /// <summary>Run a second pass to put the index (moov atom) at the beginning of the file (progressive/web playback).</summary>
        faststart,
        /// <summary>Fragment at video keyframes.</summary>
        frag_keyframe,
        /// <summary>Fragment at every frame.</summary>
        frag_every_frame,
        /// <summary>Make the initial moov atom empty (fragmented output).</summary>
        empty_moov,
        /// <summary>Write separate moof/mdat atoms for each track.</summary>
        separate_moof,
        /// <summary>Signal that the next fragment is discontinuous from earlier ones.</summary>
        frag_discont,
        /// <summary>Delay writing the initial moov until the first fragment is cut/flushed.</summary>
        delay_moov,
        /// <summary>Write a global sidx index at the start of the file.</summary>
        global_sidx,
        /// <summary>Write DASH compatible fragmented MP4.</summary>
        dash,
        /// <summary>Write CMAF compatible fragmented MP4.</summary>
        cmaf,
        /// <summary>Flush fragments on caller requests.</summary>
        frag_custom,
        /// <summary>Create a live smooth streaming feed (for pushing to a publishing point).</summary>
        isml,
        /// <summary>Add RTP hint tracks.</summary>
        rtphint,
        /// <summary>Disable Nero chapter atom.</summary>
        disable_chpl,
        /// <summary>Omit the base data offset in tfhd atoms.</summary>
        omit_tfhd_offset,
        /// <summary>Set the default-base-is-moof flag in tfhd atoms.</summary>
        default_base_moof,
        /// <summary>Use negative CTS offsets (reducing the need for edit lists).</summary>
        negative_cts_offsets,
        /// <summary>Write colr atom even if the color info is unspecified (experimental).</summary>
        write_colr,
        /// <summary>Write deprecated gama atom.</summary>
        write_gama,
        /// <summary>If writing colr atom, prioritise the ICC profile if present in stream side data.</summary>
        prefer_icc,
        /// <summary>Skip writing of sidx atom.</summary>
        skip_sidx,
        /// <summary>Skip writing the mfra/tfra/mfro trailer for fragmented files.</summary>
        skip_trailer,
        /// <summary>Use mdta atom for metadata.</summary>
        use_metadata_tags,
        /// <summary>Write a fragmented file that is converted to non-fragmented at the end (recoverability).</summary>
        hybrid_fragmented,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
