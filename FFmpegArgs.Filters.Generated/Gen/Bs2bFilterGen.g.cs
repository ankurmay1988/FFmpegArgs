namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. bs2b              A-&gt;A       Bauer stereo-to-binaural filter.
    /// </summary>
    public class Bs2bFilterGen : AudioToAudioFilter
    {
        internal Bs2bFilterGen(AudioMap input) : base("bs2b", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  Apply a pre-defined crossfeed level (from 0 to INT_MAX) (default default)
        /// </summary>
        public Bs2bFilterGen profile(Bs2bFilterGenProfile profile) => this.SetOption("profile", profile.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  Set cut frequency (in Hz) (from 0 to 2000) (default 0)
        /// </summary>
        public Bs2bFilterGen fcut(int fcut) => this.SetOptionRange("fcut", fcut, 0, 2000);
        /// <summary>
        ///  Set feed level (in Hz) (from 0 to 150) (default 0)
        /// </summary>
        public Bs2bFilterGen feed(int feed) => this.SetOptionRange("feed", feed, 0, 150);
    }

    /// <summary>
    ///  Apply a pre-defined crossfeed level (from 0 to INT_MAX) (default default)
    /// </summary>
    public enum Bs2bFilterGenProfile
    {
        /// <summary>
        /// default         2949820      ..F.A...... default profile
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("default")]
        _default = 2949820,
        /// <summary>
        /// cmoy            3932860      ..F.A...... Chu Moy circuit
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("cmoy")]
        cmoy = 3932860,
        /// <summary>
        /// jmeier          6226570      ..F.A...... Jan Meier circuit
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("jmeier")]
        jmeier = 6226570
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Bauer stereo-to-binaural filter.
        /// </summary>
        public static Bs2bFilterGen Bs2bFilterGen(this AudioMap input0) => new Bs2bFilterGen(input0);
    }
}