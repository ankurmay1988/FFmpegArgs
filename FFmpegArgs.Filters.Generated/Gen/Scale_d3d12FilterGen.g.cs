namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. scale_d3d12       V-&gt;V       Scale video using Direct3D12
    /// </summary>
    public class Scale_d3d12FilterGen : ImageToImageFilter
    {
        internal Scale_d3d12FilterGen(ImageMap input) : base("scale_d3d12", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  Output video width (default &quot;iw&quot;)
        /// </summary>
        public Scale_d3d12FilterGen w(String w) => this.SetOption("w", w.ToString());
        /// <summary>
        ///  Output video height (default &quot;ih&quot;)
        /// </summary>
        public Scale_d3d12FilterGen h(String h) => this.SetOption("h", h.ToString());
        /// <summary>
        ///  Output video pixel format (default none)
        /// </summary>
        public Scale_d3d12FilterGen format(PixFmt format) => this.SetOption("format", format.ToString());
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Scale video using Direct3D12
        /// </summary>
        public static Scale_d3d12FilterGen Scale_d3d12FilterGen(this ImageMap input0) => new Scale_d3d12FilterGen(input0);
    }
}