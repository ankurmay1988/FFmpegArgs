namespace FFmpegArgs.Outputs.Muxers
{
    /// <summary>
    /// MP4 muxer (-f mp4). Shares the movenc options of the MOV family via <see cref="BaseMp4Muxer"/>.
    /// </summary>
    public class Mp4Muxer : BaseMp4Muxer
    {
        internal Mp4Muxer(BaseOutput baseOutput) : base(MuxingFileFormat.mp4, baseOutput)
        {
        }
    }

    public static partial class MuxerExtensions
    {
        /// <summary>
        /// mp4 muxer (-f mp4).
        /// </summary>
        public static Mp4Muxer Mp4Mux<TOutput>(this TOutput output) where TOutput : BaseOutput
          => new Mp4Muxer(output);

        /// <summary>
        /// mp4 muxer (-f mp4).
        /// </summary>
        public static TOutput Mp4Mux<TOutput>(this TOutput output, Action<Mp4Muxer> action) where TOutput : BaseOutput
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            Mp4Muxer mp4Muxer = new Mp4Muxer(output);
            action.Invoke(mp4Muxer);
            return output;
        }
    }
}
