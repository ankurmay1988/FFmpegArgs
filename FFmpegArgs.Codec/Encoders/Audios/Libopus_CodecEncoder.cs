/*Encoder libopus [libopus Opus]:
    General capabilities: dr1 delay small
    Threading capabilities: none
    Supported sample rates: 48000 24000 16000 12000 8000
    Supported sample formats: s16 flt
libopus AVOptions:
  -application       <int>        E...A...... Intended application type (from 2048 to 2051) (default audio)
     voip            2048         E...A...... Favor improved speech intelligibility
     audio           2049         E...A...... Favor faithfulness to the input
     lowdelay        2051         E...A...... Restrict to only the lowest delay modes, disable voice-optimized modes
  -frame_duration    <float>      E...A...... Duration of a frame in milliseconds (from 2.5 to 120) (default 20)
  -packet_loss       <int>        E...A...... Expected packet loss percentage (from 0 to 100) (default 0)
  -fec               <boolean>    E...A...... Enable inband FEC. Expected packet loss must be non-zero (default false)
  -vbr               <int>        E...A...... Variable bit rate mode (from 0 to 2) (default on)
     off             0            E...A...... Use constant bit rate
     on              1            E...A...... Use variable bit rate
     constrained     2            E...A...... Use constrained VBR
  -mapping_family    <int>        E...A...... Channel Mapping Family (from -1 to 255) (default -1)
  -apply_phase_inv   <boolean>    E...A...... Apply intensity stereo phase inversion (default true)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// libopus Opus
    /// </summary>
    public class Libopus_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Libopus_CodecEncoder(AudioOutputAVStream stream) : base("libopus", stream)
        {
        }

        /// <summary>
        /// Intended application type (from 2048 to 2051) (default audio)
        /// </summary>
        public Libopus_CodecEncoder Application(Libopus_Application application)
            => this.SetOptionRange("-application", (int)application, 2048, 2051);

        /// <summary>
        /// Duration of a frame in milliseconds (from 2.5 to 120) (default 20)
        /// </summary>
        public Libopus_CodecEncoder FrameDuration(float frame_duration)
            => this.SetOptionRange("-frame_duration", frame_duration, 2.5f, 120);

        /// <summary>
        /// Expected packet loss percentage (from 0 to 100) (default 0)
        /// </summary>
        public Libopus_CodecEncoder PacketLoss(int packet_loss)
            => this.SetOptionRange("-packet_loss", packet_loss, 0, 100);

        /// <summary>
        /// Enable inband FEC. Expected packet loss must be non-zero (default false)
        /// </summary>
        public Libopus_CodecEncoder Fec(bool fec)
            => this.SetOption("-fec", fec.ToFFmpegFlag());

        /// <summary>
        /// Variable bit rate mode (from 0 to 2) (default on)
        /// </summary>
        public Libopus_CodecEncoder Vbr(Libopus_Vbr vbr)
            => this.SetOptionRange("-vbr", (int)vbr, 0, 2);

        /// <summary>
        /// Channel Mapping Family (from -1 to 255) (default -1)
        /// </summary>
        public Libopus_CodecEncoder MappingFamily(int mapping_family)
            => this.SetOptionRange("-mapping_family", mapping_family, -1, 255);

        /// <summary>
        /// Apply intensity stereo phase inversion (default true)
        /// </summary>
        public Libopus_CodecEncoder ApplyPhaseInv(bool apply_phase_inv)
            => this.SetOption("-apply_phase_inv", apply_phase_inv.ToFFmpegFlag());
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Libopus_Application
    {
        /// <summary>Favor improved speech intelligibility</summary>
        voip = 2048,
        /// <summary>Favor faithfulness to the input</summary>
        audio = 2049,
        /// <summary>Restrict to only the lowest delay modes, disable voice-optimized modes</summary>
        lowdelay = 2051,
    }
    public enum Libopus_Vbr
    {
        /// <summary>Use constant bit rate</summary>
        off = 0,
        /// <summary>Use variable bit rate</summary>
        on = 1,
        /// <summary>Use constrained VBR</summary>
        constrained = 2,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// libopus Opus encoder.
        /// </summary>
        public static Libopus_CodecEncoder Libopus_Codec(this AudioOutputAVStream stream)
            => new Libopus_CodecEncoder(stream);
        /// <summary>
        /// libopus Opus encoder.
        /// </summary>
        public static T Libopus_Codec<T>(this T stream, Action<Libopus_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Libopus_Codec());
            return stream;
        }
    }
}
