/*Encoder libmp3lame [libmp3lame MP3 (MPEG audio layer 3)]:
    General capabilities: dr1 delay small
    Threading capabilities: none
    Supported sample rates: 44100 48000 32000 22050 24000 16000 11025 12000 8000
    Supported sample formats: s32p fltp s16p
    Supported channel layouts: mono stereo
libmp3lame encoder AVOptions:
  -reservoir         <boolean>    E...A...... use bit reservoir (default true)
  -joint_stereo      <boolean>    E...A...... use joint stereo (default true)
  -abr               <boolean>    E...A...... use ABR (default false)
  -copyright         <boolean>    E...A...... set copyright flag (default false)
  -original          <boolean>    E...A...... set original flag (default true)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// libmp3lame MP3 (MPEG audio layer 3)
    /// </summary>
    public class Libmp3lame_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Libmp3lame_CodecEncoder(AudioOutputAVStream stream) : base("libmp3lame", stream)
        {
        }

        /// <summary>
        /// use bit reservoir (default true)
        /// </summary>
        public Libmp3lame_CodecEncoder Reservoir(bool reservoir)
            => this.SetOption("-reservoir", reservoir.ToFFmpegFlag());

        /// <summary>
        /// use joint stereo (default true)
        /// </summary>
        public Libmp3lame_CodecEncoder JointStereo(bool joint_stereo)
            => this.SetOption("-joint_stereo", joint_stereo.ToFFmpegFlag());

        /// <summary>
        /// use ABR (default false)
        /// </summary>
        public Libmp3lame_CodecEncoder Abr(bool abr)
            => this.SetOption("-abr", abr.ToFFmpegFlag());

        /// <summary>
        /// set copyright flag (default false)
        /// </summary>
        public Libmp3lame_CodecEncoder Copyright(bool copyright)
            => this.SetOption("-copyright", copyright.ToFFmpegFlag());

        /// <summary>
        /// set original flag (default true)
        /// </summary>
        public Libmp3lame_CodecEncoder Original(bool original)
            => this.SetOption("-original", original.ToFFmpegFlag());
    }

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// libmp3lame MP3 (MPEG audio layer 3) encoder.
        /// </summary>
        public static Libmp3lame_CodecEncoder Libmp3lame_Codec(this AudioOutputAVStream stream)
            => new Libmp3lame_CodecEncoder(stream);
        /// <summary>
        /// libmp3lame MP3 (MPEG audio layer 3) encoder.
        /// </summary>
        public static T Libmp3lame_Codec<T>(this T stream, Action<Libmp3lame_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Libmp3lame_Codec());
            return stream;
        }
    }
}
