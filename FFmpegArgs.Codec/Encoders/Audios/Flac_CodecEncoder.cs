/*Encoder flac [FLAC (Free Lossless Audio Codec)]:
    General capabilities: dr1 delay small
    Threading capabilities: none
    Supported sample formats: s16 s32
FLAC encoder AVOptions:
  -lpc_coeff_precision <int>        E...A...... LPC coefficient precision (from 0 to 15) (default 15)
  -lpc_type          <int>        E...A...... LPC algorithm (from -1 to 3) (default -1)
     none            0            E...A......
     fixed           1            E...A......
     levinson        2            E...A......
     cholesky        3            E...A......
  -lpc_passes        <int>        E...A...... Number of passes to use for Cholesky factorization during LPC analysis (from 1 to INT_MAX) (default 2)
  -min_partition_order <int>        E...A...... (from -1 to 8) (default -1)
  -max_partition_order <int>        E...A...... (from -1 to 8) (default -1)
  -prediction_order_method <int>        E...A...... Search method for selecting prediction order (from -1 to 5) (default -1)
     estimation      0            E...A......
     2level          1            E...A......
     4level          2            E...A......
     8level          3            E...A......
     search          4            E...A......
     log             5            E...A......
  -ch_mode           <int>        E...A...... Stereo decorrelation mode (from -1 to 3) (default auto)
     auto            -1           E...A......
     indep           0            E...A......
     left_side       1            E...A......
     right_side      2            E...A......
     mid_side        3            E...A......
  -exact_rice_parameters <boolean>    E...A...... Calculate rice parameters exactly (default false)
  -multi_dim_quant   <boolean>    E...A...... Multi-dimensional quantization (default false)
  -min_prediction_order <int>        E...A...... (from -1 to 32) (default -1)
  -max_prediction_order <int>        E...A...... (from -1 to 32) (default -1)
*/
namespace FFmpegArgs.Codec.Encoders.Audios
{
    /// <summary>
    /// FLAC (Free Lossless Audio Codec)
    /// </summary>
    public class Flac_CodecEncoder : BaseAudioCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        public Flac_CodecEncoder(AudioOutputAVStream stream) : base("flac", stream)
        {
        }

        /// <summary>
        /// LPC coefficient precision (from 0 to 15) (default 15)
        /// </summary>
        public Flac_CodecEncoder LpcCoeffPrecision(int lpc_coeff_precision)
            => this.SetOptionRange("-lpc_coeff_precision", lpc_coeff_precision, 0, 15);

        /// <summary>
        /// LPC algorithm (from -1 to 3) (default -1)
        /// </summary>
        public Flac_CodecEncoder LpcType(Flac_LpcType lpc_type)
            => this.SetOptionRange("-lpc_type", (int)lpc_type, -1, 3);

        /// <summary>
        /// Number of passes to use for Cholesky factorization during LPC analysis (from 1 to INT_MAX) (default 2)
        /// </summary>
        public Flac_CodecEncoder LpcPasses(int lpc_passes)
            => this.SetOptionRange("-lpc_passes", lpc_passes, 1, INT_MAX);

        /// <summary>
        /// Minimum partition order (from -1 to 8) (default -1)
        /// </summary>
        public Flac_CodecEncoder MinPartitionOrder(int min_partition_order)
            => this.SetOptionRange("-min_partition_order", min_partition_order, -1, 8);

        /// <summary>
        /// Maximum partition order (from -1 to 8) (default -1)
        /// </summary>
        public Flac_CodecEncoder MaxPartitionOrder(int max_partition_order)
            => this.SetOptionRange("-max_partition_order", max_partition_order, -1, 8);

        /// <summary>
        /// Search method for selecting prediction order (from -1 to 5) (default -1)
        /// </summary>
        public Flac_CodecEncoder PredictionOrderMethod(Flac_PredictionOrderMethod prediction_order_method)
            => this.SetOptionRange("-prediction_order_method", (int)prediction_order_method, -1, 5);

        /// <summary>
        /// Stereo decorrelation mode (from -1 to 3) (default auto)
        /// </summary>
        public Flac_CodecEncoder ChMode(Flac_ChMode ch_mode)
            => this.SetOptionRange("-ch_mode", (int)ch_mode, -1, 3);

        /// <summary>
        /// Calculate rice parameters exactly (default false)
        /// </summary>
        public Flac_CodecEncoder ExactRiceParameters(bool exact_rice_parameters)
            => this.SetOption("-exact_rice_parameters", exact_rice_parameters.ToFFmpegFlag());

        /// <summary>
        /// Multi-dimensional quantization (default false)
        /// </summary>
        public Flac_CodecEncoder MultiDimQuant(bool multi_dim_quant)
            => this.SetOption("-multi_dim_quant", multi_dim_quant.ToFFmpegFlag());

        /// <summary>
        /// Minimum prediction order (from -1 to 32) (default -1)
        /// </summary>
        public Flac_CodecEncoder MinPredictionOrder(int min_prediction_order)
            => this.SetOptionRange("-min_prediction_order", min_prediction_order, -1, 32);

        /// <summary>
        /// Maximum prediction order (from -1 to 32) (default -1)
        /// </summary>
        public Flac_CodecEncoder MaxPredictionOrder(int max_prediction_order)
            => this.SetOptionRange("-max_prediction_order", max_prediction_order, -1, 32);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Flac_LpcType
    {
        none = 0,
        @fixed = 1,
        levinson = 2,
        cholesky = 3,
    }
    public enum Flac_PredictionOrderMethod
    {
        estimation = 0,
        _2level = 1,
        _4level = 2,
        _8level = 3,
        search = 4,
        log = 5,
    }
    public enum Flac_ChMode
    {
        auto = -1,
        indep = 0,
        left_side = 1,
        right_side = 2,
        mid_side = 3,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class AudioEncoderExtensions
    {
        /// <summary>
        /// FLAC (Free Lossless Audio Codec) encoder.
        /// </summary>
        public static Flac_CodecEncoder Flac_Codec(this AudioOutputAVStream stream)
            => new Flac_CodecEncoder(stream);
        /// <summary>
        /// FLAC (Free Lossless Audio Codec) encoder.
        /// </summary>
        public static T Flac_Codec<T>(this T stream, Action<Flac_CodecEncoder> action) where T : AudioOutputAVStream
        {
            action.Invoke(stream.Flac_Codec());
            return stream;
        }
    }
}
