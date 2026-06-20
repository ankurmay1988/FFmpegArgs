namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. ladspa            N-&gt;A       Apply LADSPA effect.
    /// </summary>
    public class LadspaFilterGen : AudioToAudioFilter
    {
        internal LadspaFilterGen(params AudioMap[] inputs) : base("ladspa", inputs)
        {
            AddMapOut();
        }

        /// <summary>
        ///  set library name or full path
        /// </summary>
        public LadspaFilterGen file(String file) => this.SetOption("file", file.ToString());
        /// <summary>
        ///  set plugin name
        /// </summary>
        public LadspaFilterGen plugin(String plugin) => this.SetOption("plugin", plugin.ToString());
        /// <summary>
        ///  set plugin options
        /// </summary>
        public LadspaFilterGen controls(String controls) => this.SetOption("controls", controls.ToString());
        /// <summary>
        ///  set sample rate (from 1 to INT_MAX) (default 44100)
        /// </summary>
        public LadspaFilterGen sample_rate(int sample_rate) => this.SetOptionRange("sample_rate", sample_rate, 1, INT_MAX);
        /// <summary>
        ///  set the number of samples per requested frame (from 1 to INT_MAX) (default 1024)
        /// </summary>
        public LadspaFilterGen nb_samples(int nb_samples) => this.SetOptionRange("nb_samples", nb_samples, 1, INT_MAX);
        /// <summary>
        ///  set audio duration (default -0.000001)
        /// </summary>
        public LadspaFilterGen duration(TimeSpan duration) => this.SetOptionRange("duration", duration, TimeSpan.Zero, TimeSpan.MaxValue);
        /// <summary>
        ///  enable latency compensation (default false)
        /// </summary>
        public LadspaFilterGen latency(bool latency) => this.SetOption("latency", latency.ToFFmpegFlag());
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Apply LADSPA effect.
        /// </summary>
        public static LadspaFilterGen LadspaFilterGen(this AudioMap input) => new LadspaFilterGen(input);
    }
}