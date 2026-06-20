namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. flite             |-&gt;A       Synthesize voice from text using libflite.
    /// </summary>
    public class FliteFilterGen : SourceToAudioFilter
    {
        internal FliteFilterGen(IAudioFilterGraph input) : base("flite", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  list voices and exit (default false)
        /// </summary>
        public FliteFilterGen list_voices(bool list_voices) => this.SetOption("list_voices", list_voices.ToFFmpegFlag());
        /// <summary>
        ///  set number of samples per frame (from 0 to INT_MAX) (default 512)
        /// </summary>
        public FliteFilterGen nb_samples(int nb_samples) => this.SetOptionRange("nb_samples", nb_samples, 0, INT_MAX);
        /// <summary>
        ///  set text to speak
        /// </summary>
        public FliteFilterGen text(String text) => this.SetOption("text", text.ToString());
        /// <summary>
        ///  set filename of the text to speak
        /// </summary>
        public FliteFilterGen textfile(String textfile) => this.SetOption("textfile", textfile.ToString());
        /// <summary>
        ///  set voice (default &quot;kal&quot;)
        /// </summary>
        public FliteFilterGen voice(String voice) => this.SetOption("voice", voice.ToString());
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Synthesize voice from text using libflite.
        /// </summary>
        public static FliteFilterGen FliteFilterGen(this IAudioFilterGraph input) => new FliteFilterGen(input);
    }
}