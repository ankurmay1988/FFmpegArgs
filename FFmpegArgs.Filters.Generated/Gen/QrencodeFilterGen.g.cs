namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// T. qrencode          V-&gt;V       Draw a QR code on top of video frames.
    /// </summary>
    public class QrencodeFilterGen : ImageToImageFilter, ITimelineSupport
    {
        internal QrencodeFilterGen(ImageMap input) : base("qrencode", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  set rendered QR code width expression (default &quot;64&quot;)
        /// </summary>
        public QrencodeFilterGen qrcode_width(ExpressionValue qrcode_width) => this.SetOption("qrcode_width", (string)qrcode_width);
        /// <summary>
        ///  set rendered padded QR code width expression (default &quot;q&quot;)
        /// </summary>
        public QrencodeFilterGen padded_qrcode_width(ExpressionValue padded_qrcode_width) => this.SetOption("padded_qrcode_width", (string)padded_qrcode_width);
        /// <summary>
        ///  set rendered padded QR code width expression (default &quot;q&quot;)
        /// </summary>
        public QrencodeFilterGen Q(ExpressionValue Q) => this.SetOption("Q", (string)Q);
        /// <summary>
        ///  generate code which is case sensitive (default true)
        /// </summary>
        public QrencodeFilterGen case_sensitive(bool case_sensitive) => this.SetOption("case_sensitive", case_sensitive.ToFFmpegFlag());
        /// <summary>
        ///  generate code which is case sensitive (default true)
        /// </summary>
        public QrencodeFilterGen cs(bool cs) => this.SetOption("cs", cs.ToFFmpegFlag());
        /// <summary>
        ///  error correction level, lowest is L (from 0 to 3) (default Q)
        /// </summary>
        public QrencodeFilterGen level(QrencodeFilterGenLevel level) => this.SetOption("level", level.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  set the expansion mode (from 0 to 2) (default normal)
        /// </summary>
        public QrencodeFilterGen expansion(QrencodeFilterGenExpansion expansion) => this.SetOption("expansion", expansion.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  set QR foreground color (default &quot;black&quot;)
        /// </summary>
        public QrencodeFilterGen foreground_color(Color foreground_color) => this.SetOption("foreground_color", foreground_color.ToHexStringRGBA());
        /// <summary>
        ///  set QR foreground color (default &quot;black&quot;)
        /// </summary>
        public QrencodeFilterGen fc(Color fc) => this.SetOption("fc", fc.ToHexStringRGBA());
        /// <summary>
        ///  set QR background color (default &quot;white&quot;)
        /// </summary>
        public QrencodeFilterGen background_color(Color background_color) => this.SetOption("background_color", background_color.ToHexStringRGBA());
        /// <summary>
        ///  set QR background color (default &quot;white&quot;)
        /// </summary>
        public QrencodeFilterGen bc(Color bc) => this.SetOption("bc", bc.ToHexStringRGBA());
        /// <summary>
        ///  set text to encode
        /// </summary>
        public QrencodeFilterGen text(String text) => this.SetOption("text", text.ToString());
        /// <summary>
        ///  set text file to encode
        /// </summary>
        public QrencodeFilterGen textfile(String textfile) => this.SetOption("textfile", textfile.ToString());
        /// <summary>
        ///  set x expression (default &quot;0&quot;)
        /// </summary>
        public QrencodeFilterGen x(ExpressionValue x) => this.SetOption("x", (string)x);
        /// <summary>
        ///  set y expression (default &quot;0&quot;)
        /// </summary>
        public QrencodeFilterGen y(ExpressionValue y) => this.SetOption("y", (string)y);
    }

    /// <summary>
    ///  error correction level, lowest is L (from 0 to 3) (default Q)
    /// </summary>
    public enum QrencodeFilterGenLevel
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
    public enum QrencodeFilterGenExpansion
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
        /// Draw a QR code on top of video frames.
        /// </summary>
        public static QrencodeFilterGen QrencodeFilterGen(this ImageMap input0) => new QrencodeFilterGen(input0);
    }
}