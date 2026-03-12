/*librav1e AVOptions:
  -qp                <int>        E..V....... use constant quantizer mode (from -1 to 255) (default -1)
  -speed             <int>        E..V....... what speed preset to use (from -1 to 10) (default -1)
  -tiles             <int>        E..V....... number of tiles encode with (from -1 to I64_MAX) (default 0)
  -tile-rows         <int>        E..V....... number of tiles rows to encode with (from -1 to I64_MAX) (default 0)
  -tile-columns      <int>        E..V....... number of tiles columns to encode with (from -1 to I64_MAX) (default 0)
  -rav1e-params      <dictionary> E..V....... set the rav1e configuration using a :-separated list of key=value parameters
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// librav1e AV1 encoder (librav1e).
    /// </summary>
    public class Librav1e_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Librav1e_CodecEncoder(ImageOutputAVStream stream) : base("librav1e", stream)
        {
        }

        /// <summary>
        /// Use constant quantizer mode (from -1 to 255) (default -1)
        /// </summary>
        public Librav1e_CodecEncoder QP(int qp)
            => this.SetOptionRange("-qp", qp, -1, 255);

        /// <summary>
        /// Speed preset to use (from -1 to 10) (default -1)
        /// </summary>
        public Librav1e_CodecEncoder Speed(int speed)
            => this.SetOptionRange("-speed", speed, -1, 10);

        /// <summary>
        /// Number of tiles to encode with (from -1 to INT_MAX) (default 0)
        /// </summary>
        public Librav1e_CodecEncoder Tiles(int tiles)
            => this.SetOptionRange("-tiles", tiles, -1, INT_MAX);

        /// <summary>
        /// Number of tile rows to encode with (from -1 to INT_MAX) (default 0)
        /// </summary>
        public Librav1e_CodecEncoder TileRows(int rows)
            => this.SetOptionRange("-tile-rows", rows, -1, INT_MAX);

        /// <summary>
        /// Number of tile columns to encode with (from -1 to INT_MAX) (default 0)
        /// </summary>
        public Librav1e_CodecEncoder TileColumns(int cols)
            => this.SetOptionRange("-tile-columns", cols, -1, INT_MAX);

        /// <summary>
        /// Set the rav1e configuration using a :-separated list of key=value parameters
        /// </summary>
        public Librav1e_CodecEncoder Rav1eParams(IReadOnlyDictionary<string, string> rav1eParams)
            => this.SetOption("-rav1e-params", string.Join(":", rav1eParams.Select(x => $"{x.Key}={x.Value}")));
    }

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// librav1e AV1 encoder.
        /// </summary>
        public static Librav1e_CodecEncoder Librav1e_Codec(this ImageOutputAVStream stream)
            => new Librav1e_CodecEncoder(stream);
        public static T Librav1e_Codec<T>(this T stream, Action<Librav1e_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Librav1e_Codec());
            return stream;
        }
    }
}
