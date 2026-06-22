namespace FFmpegArgs.Outputs.Muxers
{
    public abstract class BaseMuxer : BaseOption, IMux
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="format"></param>
        /// <param name="baseOutput"></param>
        protected BaseMuxer(MuxingFileFormat format, BaseOutput baseOutput) : base(baseOutput)
        {
            this.Format(format);
        }
    }
}
