namespace FFmpegArgs.Inputs
{
    /// <summary>
    /// Uses a raw filtergraph string as an input via ffmpeg's lavfi virtual input device (<c>-f lavfi -i &lt;filtergraph&gt;</c>).
    /// </summary>
    public class FilterStringInput : VideoInput
    {
        readonly string _filter_string;

        /// <summary>
        /// Creates a lavfi input from a filtergraph string (e.g. <c>color=c=red:s=1280x720</c> or <c>testsrc</c>).
        /// </summary>
        /// <param name="filter">The filtergraph string passed to the lavfi input device.</param>
        /// <param name="imageStreamCount">Number of image streams exposed by this input.</param>
        /// <param name="audioStreamCount">Number of audio streams exposed by this input.</param>
        /// <exception cref="ArgumentNullException"><paramref name="filter"/> is null or empty.</exception>
        public FilterStringInput(
            string filter,
            int imageStreamCount = 1,
            int audioStreamCount = 1) : base(imageStreamCount, audioStreamCount)
        {
            if (string.IsNullOrEmpty(filter)) throw new ArgumentNullException(nameof(filter));
            this._filter_string = filter;
            this.Format(DemuxingFileFormat.lavfi);
        }

        /// <summary>
        /// Builds the input arguments (<c>-f lavfi -i &lt;filtergraph&gt;</c>). The filtergraph is emitted as a single
        /// RAW token (no manual quoting), matching the other inputs (e.g. <c>ImageFilterGraphInput</c>): on net5+ it is
        /// passed as one <c>ProcessStartInfo.ArgumentList</c> element and on older targets the renderer itself quotes
        /// tokens that contain spaces. Quoting here would be double-applied (legacy path) or passed literally to ffmpeg.
        /// </summary>
        /// <returns>The ordered argument tokens for this input.</returns>
        public override IEnumerable<string> GetAllArgs()
        {
            List<string> args =
            [
                .. GetFlagArgs(),
                .. GetOptionArgs(),
                .. GetAVStreamArgs(),
                "-i",
                _filter_string
            ];
            return args;
        }
    }
}
