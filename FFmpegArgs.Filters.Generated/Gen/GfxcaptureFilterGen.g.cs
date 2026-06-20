namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. gfxcapture        |-&gt;V       Capture graphics/screen content as a video source
    /// </summary>
    public class GfxcaptureFilterGen : SourceToImageFilter
    {
        internal GfxcaptureFilterGen(IImageFilterGraph input) : base("gfxcapture", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  ECMAScript regular expression to match against the window title. Supports PCRE style (?i) prefix for case-insensitivity.
        /// </summary>
        public GfxcaptureFilterGen window_title(ExpressionValue window_title) => this.SetOption("window_title", (string)window_title);
        /// <summary>
        ///  as window_title, but against the window class
        /// </summary>
        public GfxcaptureFilterGen window_class(String window_class) => this.SetOption("window_class", window_class.ToString());
        /// <summary>
        ///  as window_title, but against the windows executable name
        /// </summary>
        public GfxcaptureFilterGen window_exe(String window_exe) => this.SetOption("window_exe", window_exe.ToString());
        /// <summary>
        ///  index of the monitor to capture (from -2 to INT_MAX) (default -2)
        /// </summary>
        public GfxcaptureFilterGen monitor_idx(GfxcaptureFilterGenMonitor_idx monitor_idx) => this.SetOption("monitor_idx", monitor_idx.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  capture mouse cursor (default true)
        /// </summary>
        public GfxcaptureFilterGen capture_cursor(bool capture_cursor) => this.SetOption("capture_cursor", capture_cursor.ToFFmpegFlag());
        /// <summary>
        ///  capture full window border (default false)
        /// </summary>
        public GfxcaptureFilterGen capture_border(bool capture_border) => this.SetOption("capture_border", capture_border.ToFFmpegFlag());
        /// <summary>
        ///  display yellow border around captured window (default false)
        /// </summary>
        public GfxcaptureFilterGen display_border(bool display_border) => this.SetOption("display_border", display_border.ToFFmpegFlag());
        /// <summary>
        ///  set maximum capture frame rate (default &quot;60&quot;)
        /// </summary>
        public GfxcaptureFilterGen max_framerate(Rational max_framerate) => this.SetOption("max_framerate", max_framerate.ToString());
        /// <summary>
        ///  pre-existing HWND handle (from 0 to 1.84467e+19) (default 0)
        /// </summary>
        public GfxcaptureFilterGen hwnd(String hwnd) => this.SetOption("hwnd", hwnd.ToString());
        /// <summary>
        ///  pre-existing HMONITOR handle (from 0 to 1.84467e+19) (default 0)
        /// </summary>
        public GfxcaptureFilterGen hmonitor(String hmonitor) => this.SetOption("hmonitor", hmonitor.ToString());
        /// <summary>
        ///  force width of the output frames, negative values round down the width to the nearest multiple of that number (from INT_MIN to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen width(int width) => this.SetOptionRange("width", width, INT_MIN, INT_MAX);
        /// <summary>
        ///  force height of the output frames, negative values round down the height to the nearest multiple of that number (from INT_MIN to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen height(int height) => this.SetOptionRange("height", height, INT_MIN, INT_MAX);
        /// <summary>
        ///  number of pixels to crop from the left of the captured area (from 0 to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen crop_left(int crop_left) => this.SetOptionRange("crop_left", crop_left, 0, INT_MAX);
        /// <summary>
        ///  number of pixels to crop from the top of the captured area (from 0 to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen crop_top(int crop_top) => this.SetOptionRange("crop_top", crop_top, 0, INT_MAX);
        /// <summary>
        ///  number of pixels to crop from the right of the captured area (from 0 to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen crop_right(int crop_right) => this.SetOptionRange("crop_right", crop_right, 0, INT_MAX);
        /// <summary>
        ///  number of pixels to crop from the bottom of the captured area (from 0 to INT_MAX) (default 0)
        /// </summary>
        public GfxcaptureFilterGen crop_bottom(int crop_bottom) => this.SetOptionRange("crop_bottom", crop_bottom, 0, INT_MAX);
        /// <summary>
        ///  return premultiplied alpha frames (default false)
        /// </summary>
        public GfxcaptureFilterGen premultiplied(bool premultiplied) => this.SetOption("premultiplied", premultiplied.ToFFmpegFlag());
        /// <summary>
        ///  capture source resize behavior (from 0 to 2) (default crop)
        /// </summary>
        public GfxcaptureFilterGen resize_mode(GfxcaptureFilterGenResize_mode resize_mode) => this.SetOption("resize_mode", resize_mode.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  scaling algorithm (from 0 to 2) (default bilinear)
        /// </summary>
        public GfxcaptureFilterGen scale_mode(GfxcaptureFilterGenScale_mode scale_mode) => this.SetOption("scale_mode", scale_mode.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  desired output format (from 0 to INT_MAX) (default 8bit)
        /// </summary>
        public GfxcaptureFilterGen output_fmt(GfxcaptureFilterGenOutput_fmt output_fmt) => this.SetOption("output_fmt", output_fmt.GetEnumAttribute<NameAttribute>().Name);
    }

    /// <summary>
    ///  index of the monitor to capture (from -2 to INT_MAX) (default -2)
    /// </summary>
    public enum GfxcaptureFilterGenMonitor_idx
    {
        /// <summary>
        /// window          -1           ..FV....... derive from selected window
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("window")]
        window = -1
    }

    /// <summary>
    ///  capture source resize behavior (from 0 to 2) (default crop)
    /// </summary>
    public enum GfxcaptureFilterGenResize_mode
    {
        /// <summary>
        /// crop            0            ..FV....... crop or add black bars into frame
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("crop")]
        crop = 0,
        /// <summary>
        /// scale           1            ..FV....... scale source to fit initial size
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("scale")]
        scale = 1,
        /// <summary>
        /// scale_aspect    2            ..FV....... scale source to fit initial size while preserving aspect ratio
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("scale_aspect")]
        scale_aspect = 2
    }

    /// <summary>
    ///  scaling algorithm (from 0 to 2) (default bilinear)
    /// </summary>
    public enum GfxcaptureFilterGenScale_mode
    {
        /// <summary>
        /// point           0            ..FV....... use point scaling
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("point")]
        point = 0,
        /// <summary>
        /// bilinear        1            ..FV....... use bilinear scaling
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("bilinear")]
        bilinear = 1,
        /// <summary>
        /// bicubic         2            ..FV....... use bicubic scaling
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("bicubic")]
        bicubic = 2
    }

    /// <summary>
    ///  desired output format (from 0 to INT_MAX) (default 8bit)
    /// </summary>
    public enum GfxcaptureFilterGenOutput_fmt
    {
        /// <summary>
        /// 8bit            28           ..FV....... output default 8 Bit format
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("8bit")]
        _8bit = 28,
        /// <summary>
        /// bgra            28           ..FV....... output 8 Bit BGRA
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("bgra")]
        bgra = 28,
        /// <summary>
        /// 10bit           195          ..FV....... output default 10 Bit format
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("10bit")]
        _10bit = 195,
        /// <summary>
        /// x2bgr10         195          ..FV....... output 10 Bit X2BGR10
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("x2bgr10")]
        x2bgr10 = 195,
        /// <summary>
        /// 16bit           207          ..FV....... output default 16 Bit format
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("16bit")]
        _16bit = 207,
        /// <summary>
        /// rgbaf16         207          ..FV....... output 16 Bit RGBAF16
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("rgbaf16")]
        rgbaf16 = 207
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Capture graphics/screen content as a video source
        /// </summary>
        public static GfxcaptureFilterGen GfxcaptureFilterGen(this IImageFilterGraph input) => new GfxcaptureFilterGen(input);
    }
}