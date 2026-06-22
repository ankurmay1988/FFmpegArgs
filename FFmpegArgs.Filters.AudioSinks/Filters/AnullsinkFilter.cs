namespace FFmpegArgs.Filters.AudioSinks
{
    /// <summary>
    /// ... anullsink         A-&gt;|       Do absolutely nothing with the input audio.<br></br>
    /// https://ffmpeg.org/ffmpeg-filters.html#anullsink
    /// </summary>
    public class AnullsinkFilter : AudioToSinkFilter
    {
        internal AnullsinkFilter(AudioMap audioMap) : base("anullsink", audioMap)
        {
        }
    }
    /// <summary>
    ///
    /// </summary>
    public static class AnullsinkFilterExtensions
    {
        /// <summary>
        /// Do absolutely nothing with the input audio.
        /// </summary>
        /// <param name="audioMap"></param>
        /// <returns></returns>
        public static AnullsinkFilter AnullsinkFilter(this AudioMap audioMap)
          => new AnullsinkFilter(audioMap);
    }
}
