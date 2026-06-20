namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. deinterlace_d3d12 V-&gt;V       Deinterlacing using Direct3D12 Video Processor
    /// </summary>
    public class Deinterlace_d3d12FilterGen : ImageToImageFilter
    {
        internal Deinterlace_d3d12FilterGen(ImageMap input) : base("deinterlace_d3d12", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  Deinterlacing method (from 0 to 2) (default default)
        /// </summary>
        public Deinterlace_d3d12FilterGen method(Deinterlace_d3d12FilterGenMethod method) => this.SetOption("method", method.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  Specify the interlacing mode (from 0 to 1) (default frame)
        /// </summary>
        public Deinterlace_d3d12FilterGen mode(Deinterlace_d3d12FilterGenMode mode) => this.SetOption("mode", mode.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  Specify which frames to deinterlace (from 0 to 1) (default all)
        /// </summary>
        public Deinterlace_d3d12FilterGen deint(Deinterlace_d3d12FilterGenDeint deint) => this.SetOption("deint", deint.GetEnumAttribute<NameAttribute>().Name);
    }

    /// <summary>
    ///  Deinterlacing method (from 0 to 2) (default default)
    /// </summary>
    public enum Deinterlace_d3d12FilterGenMethod
    {
        /// <summary>
        /// default         0            ..FV....... Use best available deinterlacing method
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("default")]
        _default = 0,
        /// <summary>
        /// bob             1            ..FV....... Bob deinterlacing (simple field interpolation)
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("bob")]
        bob = 1,
        /// <summary>
        /// custom          2            ..FV....... Driver-defined advanced deinterlacing
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("custom")]
        custom = 2
    }

    /// <summary>
    ///  Specify the interlacing mode (from 0 to 1) (default frame)
    /// </summary>
    public enum Deinterlace_d3d12FilterGenMode
    {
        /// <summary>
        /// frame           0            ..FV....... Send one frame for each frame
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("frame")]
        frame = 0,
        /// <summary>
        /// field           1            ..FV....... Send one frame for each field
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("field")]
        field = 1
    }

    /// <summary>
    ///  Specify which frames to deinterlace (from 0 to 1) (default all)
    /// </summary>
    public enum Deinterlace_d3d12FilterGenDeint
    {
        /// <summary>
        /// all             0            ..FV....... Deinterlace all frames
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("all")]
        all = 0,
        /// <summary>
        /// interlaced      1            ..FV....... Only deinterlace frames marked as interlaced
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("interlaced")]
        interlaced = 1
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Deinterlacing using Direct3D12 Video Processor
        /// </summary>
        public static Deinterlace_d3d12FilterGen Deinterlace_d3d12FilterGen(this ImageMap input0) => new Deinterlace_d3d12FilterGen(input0);
    }
}