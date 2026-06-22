namespace FFmpegArgs.Filters.VideoSinks
{
    /// <summary>
    /// ... buffersink        V-&gt;|       Buffer video frames, and make them available to the end of the filter graph.<br></br>
    /// https://ffmpeg.org/ffmpeg-filters.html#buffersink
    /// </summary>
    public class BuffersinkFilter : ImageToSinkFilter
    {
        internal BuffersinkFilter(ImageMap imageMap) : base("buffersink", imageMap)
        {
        }
    }
    /// <summary>
    ///
    /// </summary>
    public static class BuffersinkFilterExtensions
    {
        /// <summary>
        /// Buffer video frames, and make them available to the end of the filter graph.
        /// </summary>
        /// <param name="imageMap"></param>
        /// <returns></returns>
        public static BuffersinkFilter BuffersinkFilter(this ImageMap imageMap)
          => new BuffersinkFilter(imageMap);
    }
}
