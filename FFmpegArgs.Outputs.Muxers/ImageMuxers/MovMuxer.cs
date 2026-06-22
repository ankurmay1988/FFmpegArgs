namespace FFmpegArgs.Outputs.Muxers
{
    /// <summary>
    /// QuickTime / MOV muxer (-f mov). Shares the movenc options of the MP4 family via <see cref="BaseMp4Muxer"/>.
    /// </summary>
    public class MovMuxer : BaseMp4Muxer
    {
        internal MovMuxer(BaseOutput baseOutput) : base(MuxingFileFormat.mov, baseOutput)
        {
        }
    }

    public static partial class MuxerExtensions
    {
        /// <summary>
        /// mov muxer (-f mov).
        /// </summary>
        public static MovMuxer MovMux<TOutput>(this TOutput output) where TOutput : BaseOutput
          => new MovMuxer(output);

        /// <summary>
        /// mov muxer (-f mov).
        /// </summary>
        public static TOutput MovMux<TOutput>(this TOutput output, Action<MovMuxer> action) where TOutput : BaseOutput
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            MovMuxer movMuxer = new MovMuxer(output);
            action.Invoke(movMuxer);
            return output;
        }
    }
}
