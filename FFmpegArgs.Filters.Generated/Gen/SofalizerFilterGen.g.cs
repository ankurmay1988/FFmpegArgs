namespace FFmpegArgs.Filters.Generated
{
    /// <summary>
    /// .S sofalizer         A-&gt;A       SOFAlizer (Spatially Oriented Format for Acoustics).
    /// </summary>
    public class SofalizerFilterGen : AudioToAudioFilter, ISliceThreading
    {
        internal SofalizerFilterGen(AudioMap input) : base("sofalizer", input)
        {
            AddMapOut();
        }

        /// <summary>
        ///  sofa filename
        /// </summary>
        public SofalizerFilterGen sofa(String sofa) => this.SetOption("sofa", sofa.ToString());
        /// <summary>
        ///  set gain in dB (from -20 to 40) (default 0)
        /// </summary>
        public SofalizerFilterGen gain(float gain) => this.SetOptionRange("gain", gain, -20, 40);
        /// <summary>
        ///  set rotation (from -360 to 360) (default 0)
        /// </summary>
        public SofalizerFilterGen rotation(float rotation) => this.SetOptionRange("rotation", rotation, -360, 360);
        /// <summary>
        ///  set elevation (from -90 to 90) (default 0)
        /// </summary>
        public SofalizerFilterGen elevation(float elevation) => this.SetOptionRange("elevation", elevation, -90, 90);
        /// <summary>
        ///  set radius (from 0 to 5) (default 1)
        /// </summary>
        public SofalizerFilterGen radius(float radius) => this.SetOptionRange("radius", radius, 0, 5);
        /// <summary>
        ///  set processing (from 0 to 1) (default freq)
        /// </summary>
        public SofalizerFilterGen type(SofalizerFilterGenType type) => this.SetOption("type", type.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        ///  set speaker custom positions
        /// </summary>
        public SofalizerFilterGen speakers(String speakers) => this.SetOption("speakers", speakers.ToString());
        /// <summary>
        ///  set lfe gain (from -20 to 40) (default 0)
        /// </summary>
        public SofalizerFilterGen lfegain(float lfegain) => this.SetOptionRange("lfegain", lfegain, -20, 40);
        /// <summary>
        ///  set frame size (from 1024 to 96000) (default 1024)
        /// </summary>
        public SofalizerFilterGen framesize(int framesize) => this.SetOptionRange("framesize", framesize, 1024, 96000);
        /// <summary>
        ///  normalize IRs (default true)
        /// </summary>
        public SofalizerFilterGen normalize(bool normalize) => this.SetOption("normalize", normalize.ToFFmpegFlag());
        /// <summary>
        ///  interpolate IRs from neighbors (default false)
        /// </summary>
        public SofalizerFilterGen interpolate(bool interpolate) => this.SetOption("interpolate", interpolate.ToFFmpegFlag());
        /// <summary>
        ///  minphase IRs (default false)
        /// </summary>
        public SofalizerFilterGen minphase(bool minphase) => this.SetOption("minphase", minphase.ToFFmpegFlag());
        /// <summary>
        ///  set neighbor search angle step (from 0.01 to 10) (default 0.5)
        /// </summary>
        public SofalizerFilterGen anglestep(float anglestep) => this.SetOptionRange("anglestep", anglestep, 0.01, 10);
        /// <summary>
        ///  set neighbor search radius step (from 0.01 to 1) (default 0.01)
        /// </summary>
        public SofalizerFilterGen radstep(float radstep) => this.SetOptionRange("radstep", radstep, 0.01, 1);
    }

    /// <summary>
    ///  set processing (from 0 to 1) (default freq)
    /// </summary>
    public enum SofalizerFilterGenType
    {
        /// <summary>
        /// time            0            ..F.A...... time domain
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("time")]
        time = 0,
        /// <summary>
        /// freq            1            ..F.A...... frequency domain
        /// </summary>
        [FFmpegArgs.Cores.Attributes.NameAttribute("freq")]
        freq = 1
    }

    public static partial class FilterGeneratedExtensions
    {
        /// <summary>
        /// SOFAlizer (Spatially Oriented Format for Acoustics).
        /// </summary>
        public static SofalizerFilterGen SofalizerFilterGen(this AudioMap input0) => new SofalizerFilterGen(input0);
    }
}