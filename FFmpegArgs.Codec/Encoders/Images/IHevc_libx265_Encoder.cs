/*libx265 AVOptions:
  -crf               <float>      E..V....... set the x265 crf (from -1 to FLT_MAX) (default -1)
  -qp                <int>        E..V....... set the x265 qp (from -1 to INT_MAX) (default -1)
  -forced-idr        <boolean>    E..V....... if forcing keyframes, force them as IDR frames (default false)
  -preset            <string>     E..V....... set the x265 preset
  -tune              <string>     E..V....... set the x265 tune parameter
  -profile           <string>     E..V....... set the x265 profile
  -udu_sei           <boolean>    E..V....... Use user data unregistered SEI if available (default false)
  -a53cc             <boolean>    E..V....... Use A53 Closed Captions (if available) (default true)
  -x265-params       <dictionary> E..V....... set the x265 configuration using a :-separated list of key=value parameters
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// Interface for libx265 H.265/HEVC encoder options.
    /// </summary>
    public interface IHevc_libx265_Encoder
    {
        /// <summary>
        /// Set the x265 crf (from -1 to FLT_MAX) (default -1)
        /// </summary>
        IHevc_libx265_Encoder CRF(float crf);

        /// <summary>
        /// Set the x265 qp (from -1 to INT_MAX) (default -1)
        /// </summary>
        IHevc_libx265_Encoder QP(int qp);

        /// <summary>
        /// If forcing keyframes, force them as IDR frames (default false)
        /// </summary>
        IHevc_libx265_Encoder ForcedIdr(bool flag);

        /// <summary>
        /// Set the x265 preset (e.g. ultrafast, superfast, veryfast, faster, fast, medium, slow, slower, veryslow, placebo)
        /// </summary>
        IHevc_libx265_Encoder Preset(string preset);

        /// <summary>
        /// Set the x265 tune parameter (e.g. psnr, ssim, grain, zerolatency, fastdecode, animation)
        /// </summary>
        IHevc_libx265_Encoder Tune(string tune);

        /// <summary>
        /// Set the x265 profile (e.g. main, main10, mainstillpicture)
        /// </summary>
        IHevc_libx265_Encoder Profile(string profile);

        /// <summary>
        /// Use user data unregistered SEI if available (default false)
        /// </summary>
        IHevc_libx265_Encoder UduSEI(bool flag);

        /// <summary>
        /// Use A53 Closed Captions (if available) (default true)
        /// </summary>
        IHevc_libx265_Encoder A53CC(bool flag);

        /// <summary>
        /// Set the x265 configuration using a :-separated list of key=value parameters
        /// </summary>
        IHevc_libx265_Encoder X265Params(IReadOnlyDictionary<string, string> x265params);
    }

    /// <summary>
    /// libx265 H.265/HEVC encoder.
    /// </summary>
    public class Libx265_CodecEncoder : BaseImageCodecEncoder, IHevc_libx265_Encoder
    {
        /// <summary>
        ///
        /// </summary>
        public Libx265_CodecEncoder(ImageOutputAVStream stream) : base("libx265", stream)
        {
        }

        /// <inheritdoc/>
        public IHevc_libx265_Encoder CRF(float crf)
            => this.SetOption("-crf", crf);

        /// <inheritdoc/>
        public IHevc_libx265_Encoder QP(int qp)
            => this.SetOptionRange("-qp", qp, -1, INT_MAX);

        /// <inheritdoc/>
        public IHevc_libx265_Encoder ForcedIdr(bool flag)
            => this.SetOption("-forced-idr", flag.ToFFmpegFlag());

        /// <inheritdoc/>
        public IHevc_libx265_Encoder Preset(string preset)
            => this.SetOption("-preset", preset);

        /// <inheritdoc/>
        public IHevc_libx265_Encoder Tune(string tune)
            => this.SetOption("-tune", tune);

        /// <inheritdoc/>
        public IHevc_libx265_Encoder Profile(string profile)
            => this.SetOption("-profile", profile);

        /// <inheritdoc/>
        public IHevc_libx265_Encoder UduSEI(bool flag)
            => this.SetOption("-udu_sei", flag.ToFFmpegFlag());

        /// <inheritdoc/>
        public IHevc_libx265_Encoder A53CC(bool flag)
            => this.SetOption("-a53cc", flag.ToFFmpegFlag());

        /// <inheritdoc/>
        public IHevc_libx265_Encoder X265Params(IReadOnlyDictionary<string, string> x265params)
            => this.SetOption("-x265-params", string.Join(":", x265params.Select(x => $"{x.Key}={x.Value}")));
    }

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// libx265 H.265/HEVC encoder.
        /// </summary>
        public static Libx265_CodecEncoder Libx265_Codec(this ImageOutputAVStream stream)
            => new Libx265_CodecEncoder(stream);
        public static T Libx265_Codec<T>(this T stream, Action<Libx265_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Libx265_Codec());
            return stream;
        }
    }
}
