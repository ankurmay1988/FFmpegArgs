namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// T. drawvg            V-&gt;V       Draw vector graphics on top of video frames.
    /// </summary>
    public class DrawvgFilterGen : ImageToImageFilter, ITimelineSupport
    {
        internal DrawvgFilterGen(ImageMap input) : base("drawvg", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  script source to draw the graphics
        /// </summary>
        public DrawvgFilterGen script(String script) => this.SetOption("script", script.ToString());
        /// <summary>
        ///  file to load the script source
        /// </summary>
        public DrawvgFilterGen file(String file) => this.SetOption("file", file.ToString());
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Draw vector graphics on top of video frames.
        /// </summary>
        public static DrawvgFilterGen DrawvgFilterGen(this ImageMap input0) => new DrawvgFilterGen(input0);
    }
}