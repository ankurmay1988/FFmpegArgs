/*Encoder aac [AAC (Advanced Audio Coding)]:
    General capabilities: dr1 delay small
    Threading capabilities: none
    Supported sample rates: 96000 88200 64000 48000 44100 32000 24000 22050 16000 12000 11025 8000 7350
    Supported sample formats: fltp
AAC encoder AVOptions:
  -aac_coder         <int>        E...A...... Coding algorithm (from 0 to 1) (default twoloop)
     twoloop         0            E...A...... Two loop searching method
     fast            1            E...A...... Fast search
  -aac_ms            <boolean>    E...A...... Force M/S stereo coding (default auto)
  -aac_is            <boolean>    E...A...... Intensity stereo coding (default true)
  -aac_pns           <boolean>    E...A...... Perceptual noise substitution (default true)
  -aac_tns           <boolean>    E...A...... Temporal noise shaping (default true)
  -aac_pce           <boolean>    E...A...... Forces the use of PCEs (default false)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// AAC (Advanced Audio Coding)
    /// </summary>
    public class Aac_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Aac_CodecEncoder(AudioOutputAVStream stream) : base("aac", stream)
        {
        }

        /// <summary>
        /// Coding algorithm (from 0 to 1) (default twoloop)
        /// </summary>
        public Aac_CodecEncoder AacCoder(Aac_Coder aac_coder)
            => this.SetOptionRange("-aac_coder", (int)aac_coder, 0, 1);

        /// <summary>
        /// Force M/S stereo coding (default auto)
        /// </summary>
        public Aac_CodecEncoder AacMs(bool aac_ms)
            => this.SetOption("-aac_ms", aac_ms.ToFFmpegFlag());

        /// <summary>
        /// Intensity stereo coding (default true)
        /// </summary>
        public Aac_CodecEncoder AacIs(bool aac_is)
            => this.SetOption("-aac_is", aac_is.ToFFmpegFlag());

        /// <summary>
        /// Perceptual noise substitution (default true)
        /// </summary>
        public Aac_CodecEncoder AacPns(bool aac_pns)
            => this.SetOption("-aac_pns", aac_pns.ToFFmpegFlag());

        /// <summary>
        /// Temporal noise shaping (default true)
        /// </summary>
        public Aac_CodecEncoder AacTns(bool aac_tns)
            => this.SetOption("-aac_tns", aac_tns.ToFFmpegFlag());

        /// <summary>
        /// Forces the use of PCEs (default false)
        /// </summary>
        public Aac_CodecEncoder AacPce(bool aac_pce)
            => this.SetOption("-aac_pce", aac_pce.ToFFmpegFlag());
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Aac_Coder
    {
        /// <summary>Two loop searching method</summary>
        twoloop = 0,
        /// <summary>Fast search</summary>
        fast = 1,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// AAC (Advanced Audio Coding) encoder.
        /// </summary>
        public static Aac_CodecEncoder Aac_Codec(this AudioOutputAVStream stream)
            => new Aac_CodecEncoder(stream);
        /// <summary>
        /// AAC (Advanced Audio Coding) encoder.
        /// </summary>
        public static T Aac_Codec<T>(this T stream, Action<Aac_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Aac_Codec());
            return stream;
        }
    }
}
