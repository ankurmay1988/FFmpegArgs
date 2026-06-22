/*Encoder libvorbis [libvorbis]:
    General capabilities: dr1 delay small
    Threading capabilities: none
    Supported sample formats: fltp
libvorbis AVOptions:
  -iblock            <double>     E...A...... Sets the impulse block bias (from -15 to 0) (default 0)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// libvorbis (Vorbis)
    /// </summary>
    public class Libvorbis_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Libvorbis_CodecEncoder(AudioOutputAVStream stream) : base("libvorbis", stream)
        {
        }

        /// <summary>
        /// Sets the impulse block bias (from -15 to 0) (default 0)
        /// </summary>
        public Libvorbis_CodecEncoder Iblock(double iblock)
            => this.SetOptionRange("-iblock", iblock, -15, 0);
    }

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// libvorbis (Vorbis) encoder.
        /// </summary>
        public static Libvorbis_CodecEncoder Libvorbis_Codec(this AudioOutputAVStream stream)
            => new Libvorbis_CodecEncoder(stream);
        /// <summary>
        /// libvorbis (Vorbis) encoder.
        /// </summary>
        public static T Libvorbis_Codec<T>(this T stream, Action<Libvorbis_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Libvorbis_Codec());
            return stream;
        }
    }
}
