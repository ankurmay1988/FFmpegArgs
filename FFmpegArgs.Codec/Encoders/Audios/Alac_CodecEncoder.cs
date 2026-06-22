/*Encoder alac [ALAC (Apple Lossless Audio Codec)]:
    General capabilities: dr1 small
    Threading capabilities: none
    Supported sample formats: s32p s16p
    Supported channel layouts: mono stereo 3.0 4.0 5.0 5.1 6.1(back) 7.1(wide)
alacenc AVOptions:
  -min_prediction_order <int>        E...A...... (from 1 to 30) (default 4)
  -max_prediction_order <int>        E...A...... (from 1 to 30) (default 6)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// ALAC (Apple Lossless Audio Codec)
    /// </summary>
    public class Alac_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Alac_CodecEncoder(AudioOutputAVStream stream) : base("alac", stream)
        {
        }

        /// <summary>
        /// Minimum prediction order (from 1 to 30) (default 4)
        /// </summary>
        public Alac_CodecEncoder MinPredictionOrder(int min_prediction_order)
            => this.SetOptionRange("-min_prediction_order", min_prediction_order, 1, 30);

        /// <summary>
        /// Maximum prediction order (from 1 to 30) (default 6)
        /// </summary>
        public Alac_CodecEncoder MaxPredictionOrder(int max_prediction_order)
            => this.SetOptionRange("-max_prediction_order", max_prediction_order, 1, 30);
    }

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// ALAC (Apple Lossless Audio Codec) encoder.
        /// </summary>
        public static Alac_CodecEncoder Alac_Codec(this AudioOutputAVStream stream)
            => new Alac_CodecEncoder(stream);
        /// <summary>
        /// ALAC (Apple Lossless Audio Codec) encoder.
        /// </summary>
        public static T Alac_Codec<T>(this T stream, Action<Alac_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Alac_Codec());
            return stream;
        }
    }
}
