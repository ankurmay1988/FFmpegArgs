namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// TS lensfun           V-&gt;V       Apply correction to an image based on info derived from the lensfun database.
    /// </summary>
    public class LensfunFilterGen : ImageToImageFilter, ITimelineSupport, ISliceThreading
    {
        internal LensfunFilterGen(ImageMap input) : base("lensfun", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  set camera maker
        /// </summary>
        public LensfunFilterGen make(String make) => this.SetOption("make", make.ToString());
        /// <summary>
        ///  set camera model
        /// </summary>
        public LensfunFilterGen model(String model) => this.SetOption("model", model.ToString());
        /// <summary>
        ///  set lens model
        /// </summary>
        public LensfunFilterGen lens_model(String lens_model) => this.SetOption("lens_model", lens_model.ToString());
        /// <summary>
        ///  set path to database
        /// </summary>
        public LensfunFilterGen db_path(String db_path) => this.SetOption("db_path", db_path.ToString());
        /// <summary>
        ///  set mode (from 0 to 7) (default geometry)
        /// </summary>
        public LensfunFilterGen mode(LensfunFilterGenMode mode) => this.SetOption("mode", mode.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  focal length of video (zoom; constant for the duration of the use of this filter) (from 0 to DBL_MAX) (default 18)
        /// </summary>
        public LensfunFilterGen focal_length(float focal_length) => this.SetOptionRange("focal_length", focal_length, 0, DBL_MAX);
        /// <summary>
        ///  aperture (constant for the duration of the use of this filter) (from 0 to DBL_MAX) (default 3.5)
        /// </summary>
        public LensfunFilterGen aperture(float aperture) => this.SetOptionRange("aperture", aperture, 0, DBL_MAX);
        /// <summary>
        ///  focus distance (constant for the duration of the use of this filter) (from 0 to DBL_MAX) (default 1000)
        /// </summary>
        public LensfunFilterGen focus_distance(float focus_distance) => this.SetOptionRange("focus_distance", focus_distance, 0, DBL_MAX);
        /// <summary>
        ///  scale factor applied after corrections (0.0 means automatic scaling) (from 0 to DBL_MAX) (default 0)
        /// </summary>
        public LensfunFilterGen scale(float scale) => this.SetOptionRange("scale", scale, 0, DBL_MAX);
        /// <summary>
        ///  target geometry of the lens correction (only when geometry correction is enabled) (from 0 to INT_MAX) (default rectilinear)
        /// </summary>
        public LensfunFilterGen target_geometry(LensfunFilterGenTarget_geometry target_geometry) => this.SetOption("target_geometry", target_geometry.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  Does reverse correction (regular image to lens distorted) (default false)
        /// </summary>
        public LensfunFilterGen reverse(bool reverse) => this.SetOption("reverse", reverse.ToFFmpegFlag());
        /// <summary>
        ///  Type of interpolation (from 0 to 2) (default linear)
        /// </summary>
        public LensfunFilterGen interpolation(LensfunFilterGenInterpolation interpolation) => this.SetOption("interpolation", interpolation.GetEnumAttribute<NameAttribute>().Name);
    }

    /// <summary>
    ///  set mode (from 0 to 7) (default geometry)
    /// </summary>
    public enum LensfunFilterGenMode
    {
        /// <summary>
        /// vignetting      1            ..FV....... fix lens vignetting
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("vignetting")]
        vignetting = 1,
        /// <summary>
        /// geometry        2            ..FV....... correct geometry distortion
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("geometry")]
        geometry = 2,
        /// <summary>
        /// subpixel        4            ..FV....... fix chromatic aberrations
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("subpixel")]
        subpixel = 4,
        /// <summary>
        /// vig_geo         3            ..FV....... fix lens vignetting and correct geometry distortion
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("vig_geo")]
        vig_geo = 3,
        /// <summary>
        /// vig_subpixel    5            ..FV....... fix lens vignetting and chromatic aberrations
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("vig_subpixel")]
        vig_subpixel = 5,
        /// <summary>
        /// distortion      6            ..FV....... correct geometry distortion and chromatic aberrations
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("distortion")]
        distortion = 6,
        /// <summary>
        /// all             7            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("all")]
        all = 7
    }

    /// <summary>
    ///  target geometry of the lens correction (only when geometry correction is enabled) (from 0 to INT_MAX) (default rectilinear)
    /// </summary>
    public enum LensfunFilterGenTarget_geometry
    {
        /// <summary>
        /// rectilinear     1            ..FV....... rectilinear lens (default)
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("rectilinear")]
        rectilinear = 1,
        /// <summary>
        /// fisheye         2            ..FV....... fisheye lens
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("fisheye")]
        fisheye = 2,
        /// <summary>
        /// panoramic       3            ..FV....... panoramic (cylindrical)
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("panoramic")]
        panoramic = 3,
        /// <summary>
        /// equirectangular 4            ..FV....... equirectangular
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("equirectangular")]
        equirectangular = 4,
        /// <summary>
        /// fisheye_orthographic 5            ..FV....... orthographic fisheye
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("fisheye_orthographic")]
        fisheye_orthographic = 5,
        /// <summary>
        /// fisheye_stereographic 6            ..FV....... stereographic fisheye
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("fisheye_stereographic")]
        fisheye_stereographic = 6,
        /// <summary>
        /// fisheye_equisolid 7            ..FV....... equisolid fisheye
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("fisheye_equisolid")]
        fisheye_equisolid = 7,
        /// <summary>
        /// fisheye_thoby   8            ..FV....... fisheye as measured by thoby
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("fisheye_thoby")]
        fisheye_thoby = 8
    }

    /// <summary>
    ///  Type of interpolation (from 0 to 2) (default linear)
    /// </summary>
    public enum LensfunFilterGenInterpolation
    {
        /// <summary>
        /// nearest         0            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("nearest")]
        nearest = 0,
        /// <summary>
        /// linear          1            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("linear")]
        linear = 1,
        /// <summary>
        /// lanczos         2            ..FV.......
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("lanczos")]
        lanczos = 2
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Apply correction to an image based on info derived from the lensfun database.
        /// </summary>
        public static LensfunFilterGen LensfunFilterGen(this ImageMap input0) => new LensfunFilterGen(input0);
    }
}