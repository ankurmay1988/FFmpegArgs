namespace FFmpegArgs.Filters.VideoSinks
{
    /// <summary>
    /// ... nullsink          V-&gt;|       Do absolutely nothing with the input video.<br></br>
    /// https://ffmpeg.org/ffmpeg-filters.html#nullsink
    /// </summary>
    public class NullsinkFilter : ImageToSinkFilter
    {
        internal NullsinkFilter(ImageMap imageMap) : base("nullsink", imageMap)
        {
        }
    }
    /// <summary>
    ///
    /// </summary>
    public static class NullsinkFilterExtensions
    {
        /// <summary>
        /// Do absolutely nothing with the input video.
        /// </summary>
        /// <param name="imageMap"></param>
        /// <returns></returns>
        public static NullsinkFilter NullsinkFilter(this ImageMap imageMap)
          => new NullsinkFilter(imageMap);
    }
}
