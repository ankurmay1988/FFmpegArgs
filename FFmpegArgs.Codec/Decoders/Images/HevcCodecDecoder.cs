/*HEVC decoder AVOptions:
  -apply_defdispwin  <boolean>    .D.V....... Apply default display window from VUI (default false)
  -strict-displaywin <boolean>    .D.V....... stricly apply default display window size (default false)
*/
namespace FFmpegArgs.Codec.Decoders.Images
{
    /// <summary>
    /// DEV.L. hevc H.265 / HEVC (High Efficiency Video Coding)<br/>
    /// (decoders: hevc hevc_qsv hevc_amf hevc_cuvid)<br/>
    /// (encoders: libx265 hevc_amf hevc_mf hevc_nvenc hevc_qsv hevc_vaapi libkvazaar)
    /// </summary>
    public class HevcCodecDecoder : BaseImageCodecDecoder
    {
        /// <summary>
        ///
        /// </summary>
        public HevcCodecDecoder(ImageInputAVStream stream) : base(Codecs.hevc, stream)
        {
        }

        /// <summary>
        /// Apply default display window from VUI (default false)
        /// </summary>
        public HevcCodecDecoder ApplyDefDispWin(bool flag)
            => this.SetOption("-apply_defdispwin", flag.ToFFmpegFlag());

        /// <summary>
        /// Strictly apply default display window size (default false)
        /// </summary>
        public HevcCodecDecoder StrictDisplayWin(bool flag)
            => this.SetOption("-strict-displaywin", flag.ToFFmpegFlag());
    }

    public static partial class ImageDecoderExtensions
    {
        /// <summary>
        /// H.265/HEVC decoder.
        /// </summary>
        public static HevcCodecDecoder HevcCodec(this ImageInputAVStream stream)
            => new HevcCodecDecoder(stream);
        public static T HevcCodec<T>(this T stream, Action<HevcCodecDecoder> action) where T : ImageInputAVStream
        {
            action.Invoke(stream.HevcCodec());
            return stream;
        }
    }
}
