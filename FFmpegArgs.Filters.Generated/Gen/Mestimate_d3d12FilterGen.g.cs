namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .. mestimate_d3d12   V-&gt;V       Generate motion vectors using D3D12 hardware acceleration.
    /// </summary>
    public class Mestimate_d3d12FilterGen : ImageToImageFilter
    {
        internal Mestimate_d3d12FilterGen(ImageMap input) : base("mestimate_d3d12", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  macroblock size, only 8 and 16 are supported (from 8 to 16) (default 16)
        /// </summary>
        public Mestimate_d3d12FilterGen mb_size(int mb_size) => this.SetOptionRange("mb_size", mb_size, 8, 16);
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// Generate motion vectors using D3D12 hardware acceleration.
        /// </summary>
        public static Mestimate_d3d12FilterGen Mestimate_d3d12FilterGen(this ImageMap input0) => new Mestimate_d3d12FilterGen(input0);
    }
}