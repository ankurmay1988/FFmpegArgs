namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. iccdetect         V-&gt;V       Detect and parse ICC profiles.
    /// </summary>
    public class IccdetectFilterGen : ImageToImageFilter
    {
        internal IccdetectFilterGen(ImageMap input) : base("iccdetect", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  overwrite existing tags (default true)
        /// </summary>
        public IccdetectFilterGen force(bool force) => this.SetOption("force", force.ToFFmpegFlag());
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Detect and parse ICC profiles.
        /// </summary>
        public static IccdetectFilterGen IccdetectFilterGen(this ImageMap input0) => new IccdetectFilterGen(input0);
    }
}