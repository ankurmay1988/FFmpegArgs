namespace FFmpegArgs.Outputs.Muxers
{
    /// <summary>
    /// Shared options of the MOV/MP4/ISMV family (movenc) muxers.
    /// </summary>
    public abstract class BaseMp4Muxer : BaseMuxer
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        /// <param name="baseOutput"></param>
        protected BaseMp4Muxer(MuxingFileFormat format, BaseOutput baseOutput) : base(format, baseOutput)
        {
        }

        /// <summary>
        /// -movflags<br></br>
        /// Set one or more MOV/MP4 muxer flags. The flags are emitted in the additive (<c>+flag</c>) form,
        /// e.g. <c>-movflags +faststart+empty_moov</c>. The most common is <see cref="MovFlag.faststart"/>
        /// (moves the moov atom to the start of the file for progressive/web playback).
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public BaseMp4Muxer MovFlags(params MovFlag[] flags)
        {
            if (flags is null || flags.Length == 0) throw new ArgumentNullException(nameof(flags));
            return this.SetOption("-movflags", string.Concat(flags.Select(f => "+" + f.ToString())));
        }
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
}
