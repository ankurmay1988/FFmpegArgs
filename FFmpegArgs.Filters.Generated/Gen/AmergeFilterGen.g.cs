namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. amerge            N-&gt;A       Merge two or more audio streams into a single multi-channel stream.
    /// </summary>
    public class AmergeFilterGen : AudioToAudioFilter
    {
        internal AmergeFilterGen(params AudioMap[] inputs) : base("amerge", inputs)
        {
            AddMapOut();
        }

        /// <summary>
        ///  specify the number of inputs (from 1 to 64) (default 2)
        /// </summary>
        public AmergeFilterGen inputs(int inputs) => this.SetOptionRange("inputs", inputs, 1, 64);
        /// <summary>
        ///  method used to determine the output channel layout (from 0 to 2) (default legacy)
        /// </summary>
        public AmergeFilterGen layout_mode(AmergeFilterGenLayout_mode layout_mode) => this.SetOption("layout_mode", layout_mode.GetEnumAttribute<NameAttribute>().Name);
    }

    /// <summary>
    ///  method used to determine the output channel layout (from 0 to 2) (default legacy)
    /// </summary>
    public enum AmergeFilterGenLayout_mode
    {
        /// <summary>
        /// legacy          0            ..F.A......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("legacy")]
        legacy = 0,
        /// <summary>
        /// reset           1            ..F.A......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("reset")]
        reset = 1,
        /// <summary>
        /// normal          2            ..F.A......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("normal")]
        normal = 2
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Merge two or more audio streams into a single multi-channel stream.
        /// </summary>
        public static AmergeFilterGen AmergeFilterGen(this AudioMap input) => new AmergeFilterGen(input);
    }
}