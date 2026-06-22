namespace FFmpegArgs.Inputs.Demuxers
{
    /// <summary>
    /// image2 demuxer.<br/>
    /// Read from a list of image files specified by a pattern (printf-style sequence such as img-%03d.png, or a glob).<br/>
    /// <see href="https://ffmpeg.org/ffmpeg-formats.html#image2-1"/>
    /// </summary>
    public class Image2Demuxer : BaseDemuxer, IImage
    {
        internal Image2Demuxer(BaseInput baseInput) : base(DemuxingFileFormat.image2, baseInput)
        {
        }

        /// <summary>
        /// Set the index of the file matched by the image file pattern to start to read from. Default value is 0.
        /// </summary>
        public Image2Demuxer StartNumber(int start_number)
            => this.SetOptionRange("-start_number", start_number, 0, INT_MAX);

        /// <summary>
        /// Select how the image file pattern is interpreted (sequence numbering, glob, etc.).
        /// </summary>
        public Image2Demuxer PatternType(Image2PatternType pattern_type)
            => this.SetOption("-pattern_type", pattern_type.ToString());
    }

    public static partial class DemuxerExtensions
    {
        /// <summary>
        /// image2 demuxer (-f image2): read a printf-style sequence or glob of image files.
        /// </summary>
        public static Image2Demuxer Image2Demux<TInput>(this TInput input) where TInput : BaseInput, IImageInput
          => new Image2Demuxer(input);

        /// <summary>
        /// image2 demuxer (-f image2): read a printf-style sequence or glob of image files.
        /// </summary>
        public static TInput Image2Demux<TInput>(this TInput input, Action<Image2Demuxer> action) where TInput : BaseInput, IImageInput
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            Image2Demuxer image2Demuxer = new Image2Demuxer(input);
            action.Invoke(image2Demuxer);
            return input;
        }
    }

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
}
