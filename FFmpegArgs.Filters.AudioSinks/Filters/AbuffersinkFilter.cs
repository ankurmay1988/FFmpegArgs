namespace FFmpegArgs.Filters.AudioSinks
{
    /// <summary>
    /// ... abuffersink       A-&gt;|       Buffer audio frames, and make them available to the end of the filter graph.<br></br>
    /// https://ffmpeg.org/ffmpeg-filters.html#abuffersink
    /// </summary>
    public class AbuffersinkFilter : AudioToSinkFilter
    {
        internal AbuffersinkFilter(AudioMap audioMap) : base("abuffersink", audioMap)
        {
        }
    }
    /// <summary>
    ///
    /// </summary>
    public static class AbuffersinkFilterExtensions
    {
        /// <summary>
        /// Buffer audio frames, and make them available to the end of the filter graph.
        /// </summary>
        /// <param name="audioMap"></param>
        /// <returns></returns>
        public static AbuffersinkFilter AbuffersinkFilter(this AudioMap audioMap)
          => new AbuffersinkFilter(audioMap);
    }
}
