namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. qrencodesrc       |-&gt;V       Generate a QR code.
    /// </summary>
    public class QrencodesrcFilterGen : SourceToImageFilter
    {
        internal QrencodesrcFilterGen(IImageFilterGraph input) : base("qrencodesrc", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  set rendered QR code width expression (default &quot;64&quot;)
        /// </summary>
        public QrencodesrcFilterGen qrcode_width(ExpressionValue qrcode_width) => this.SetOption("qrcode_width", (string)qrcode_width);
        /// <summary>
        ///  set rendered padded QR code width expression (default &quot;q&quot;)
        /// </summary>
        public QrencodesrcFilterGen padded_qrcode_width(ExpressionValue padded_qrcode_width) => this.SetOption("padded_qrcode_width", (string)padded_qrcode_width);
        /// <summary>
        ///  set rendered padded QR code width expression (default &quot;q&quot;)
        /// </summary>
        public QrencodesrcFilterGen Q(ExpressionValue Q) => this.SetOption("Q", (string)Q);
        /// <summary>
        ///  generate code which is case sensitive (default true)
        /// </summary>
        public QrencodesrcFilterGen case_sensitive(bool case_sensitive) => this.SetOption("case_sensitive", case_sensitive.ToFFmpegFlag());
        /// <summary>
        ///  generate code which is case sensitive (default true)
        /// </summary>
        public QrencodesrcFilterGen cs(bool cs) => this.SetOption("cs", cs.ToFFmpegFlag());
        /// <summary>
        ///  error correction level, lowest is L (from 0 to 3) (default Q)
        /// </summary>
        public QrencodesrcFilterGen level(QrencodesrcFilterGenLevel level) => this.SetOption("level", level.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  set the expansion mode (from 0 to 2) (default normal)
        /// </summary>
        public QrencodesrcFilterGen expansion(QrencodesrcFilterGenExpansion expansion) => this.SetOption("expansion", expansion.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  set QR foreground color (default &quot;black&quot;)
        /// </summary>
        public QrencodesrcFilterGen foreground_color(Color foreground_color) => this.SetOption("foreground_color", foreground_color.ToHexStringRGBA());
        /// <summary>
        ///  set QR foreground color (default &quot;black&quot;)
        /// </summary>
        public QrencodesrcFilterGen fc(Color fc) => this.SetOption("fc", fc.ToHexStringRGBA());
        /// <summary>
        ///  set QR background color (default &quot;white&quot;)
        /// </summary>
        public QrencodesrcFilterGen background_color(Color background_color) => this.SetOption("background_color", background_color.ToHexStringRGBA());
        /// <summary>
        ///  set QR background color (default &quot;white&quot;)
        /// </summary>
        public QrencodesrcFilterGen bc(Color bc) => this.SetOption("bc", bc.ToHexStringRGBA());
        /// <summary>
        ///  set text to encode
        /// </summary>
        public QrencodesrcFilterGen text(String text) => this.SetOption("text", text.ToString());
        /// <summary>
        ///  set text file to encode
        /// </summary>
        public QrencodesrcFilterGen textfile(String textfile) => this.SetOption("textfile", textfile.ToString());
        /// <summary>
        ///  set video rate (default &quot;25&quot;)
        /// </summary>
        public QrencodesrcFilterGen rate(Rational rate) => this.SetOption("rate", rate.ToString());
    }

    /// <summary>
    ///  error correction level, lowest is L (from 0 to 3) (default Q)
    /// </summary>
    public enum QrencodesrcFilterGenLevel
    {
        /// <summary>
        /// L               0            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("L")]
        L = 0,
        /// <summary>
        /// M               1            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("M")]
        M = 1,
        /// <summary>
        /// Q               2            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("Q")]
        Q = 2,
        /// <summary>
        /// H               3            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("H")]
        H = 3
    }

    /// <summary>
    ///  set the expansion mode (from 0 to 2) (default normal)
    /// </summary>
    public enum QrencodesrcFilterGenExpansion
    {
        /// <summary>
        /// none            0            ..FV....... set no expansion
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("none")]
        none = 0,
        /// <summary>
        /// normal          1            ..FV....... set normal expansion
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("normal")]
        normal = 1
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Generate a QR code.
        /// </summary>
        public static QrencodesrcFilterGen QrencodesrcFilterGen(this IImageFilterGraph input) => new QrencodesrcFilterGen(input);
    }
}