/*h265_vaapi AVOptions:
  -low_power         <boolean>    E..V....... Use low-power encoding mode (default false)
  -idr_interval      <int>        E..V....... Distance (in I-frames) between IDR frames (from 0 to INT_MAX) (default 0)
  -b_depth           <int>        E..V....... Maximum B-frame reference depth (from 1 to INT_MAX) (default 1)
  -async_depth       <int>        E..V....... Maximum processing parallelism (from 1 to 64) (default 2)
  -max_frame_size    <int>        E..V....... Maximum frame size (in bytes) (from 0 to INT_MAX) (default 0)
  -rc_mode           <int>        E..V....... Set rate control mode (from 0 to 6) (default auto)
     auto            0            E..V....... Choose mode automatically based on other parameters
     CQP             1            E..V....... Constant-quality
     CBR             2            E..V....... Constant-bitrate
     VBR             3            E..V....... Variable-bitrate
     ICQ             4            E..V....... Intelligent constant-quality
     QVBR            5            E..V....... Quality-defined variable-bitrate
     AVBR            6            E..V....... Average variable-bitrate
  -blbrc             <boolean>    E..V....... Block level based bitrate control (default false)
  -qp                <int>        E..V....... Constant QP (for P-frames; scaled by qfactor/qoffset for I/B) (from 0 to 52) (default 0)
  -aud               <boolean>    E..V....... Include AUD (default false)
  -profile           <int>        E..V....... Set profile (general_profile_idc) (from -99 to 255) (default -99)
     main            1            E..V.......
     main10          2            E..V.......
     rext            4            E..V.......
  -tier              <int>        E..V....... Set tier (general_tier_flag) (from 0 to 1) (default main)
     main            0            E..V.......
     high            1            E..V.......
  -level             <int>        E..V....... Set level (general_level_idc) (from -99 to 255) (default -99)
     1               30           E..V.......
     2               60           E..V.......
     2.1             63           E..V.......
     3               90           E..V.......
     3.1             93           E..V.......
     4               120          E..V.......
     4.1             123          E..V.......
     5               150          E..V.......
     5.1             153          E..V.......
     5.2             156          E..V.......
     6               180          E..V.......
     6.1             183          E..V.......
     6.2             186          E..V.......
  -sei               <flags>      E..V....... Set SEI to include (default hdr+a53_cc)
     hdr                          E..V....... Include HDR metadata
     a53_cc                       E..V....... Include A/53 caption data
  -tiles             <image_size> E..V....... Tile columns x rows
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// VA-API H.265/HEVC encoder (hevc_vaapi).
    /// </summary>
    public class Hevc_vaapi_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Hevc_vaapi_CodecEncoder(ImageOutputAVStream stream) : base("hevc_vaapi", stream)
        {
        }

        /// <summary>
        /// Use low-power encoding mode (default false)
        /// </summary>
        public Hevc_vaapi_CodecEncoder LowPower(bool flag)
            => this.SetOption("-low_power", flag.ToFFmpegFlag());

        /// <summary>
        /// Distance (in I-frames) between IDR frames (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Hevc_vaapi_CodecEncoder IdrInterval(int interval)
            => this.SetOptionRange("-idr_interval", interval, 0, INT_MAX);

        /// <summary>
        /// Maximum B-frame reference depth (from 1 to INT_MAX) (default 1)
        /// </summary>
        public Hevc_vaapi_CodecEncoder BDepth(int depth)
            => this.SetOptionRange("-b_depth", depth, 1, INT_MAX);

        /// <summary>
        /// Maximum processing parallelism (from 1 to 64) (default 2)
        /// </summary>
        public Hevc_vaapi_CodecEncoder AsyncDepth(int depth)
            => this.SetOptionRange("-async_depth", depth, 1, 64);

        /// <summary>
        /// Maximum frame size (in bytes) (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Hevc_vaapi_CodecEncoder MaxFrameSize(int size)
            => this.SetOptionRange("-max_frame_size", size, 0, INT_MAX);

        /// <summary>
        /// Set rate control mode (from 0 to 6) (default auto)
        /// </summary>
        public Hevc_vaapi_CodecEncoder RCMode(Hevc_vaapi_RCMode mode)
            => this.SetOptionRange("-rc_mode", (int)mode, 0, 6);

        /// <summary>
        /// Block level based bitrate control (default false)
        /// </summary>
        public Hevc_vaapi_CodecEncoder BLBRC(bool flag)
            => this.SetOption("-blbrc", flag.ToFFmpegFlag());

        /// <summary>
        /// Constant QP for P-frames (from 0 to 52) (default 0)
        /// </summary>
        public Hevc_vaapi_CodecEncoder QP(int qp)
            => this.SetOptionRange("-qp", qp, 0, 52);

        /// <summary>
        /// Include AUD (default false)
        /// </summary>
        public Hevc_vaapi_CodecEncoder AUD(bool flag)
            => this.SetOption("-aud", flag.ToFFmpegFlag());

        /// <summary>
        /// Set profile (general_profile_idc) (default -99)
        /// </summary>
        public Hevc_vaapi_CodecEncoder Profile(Hevc_vaapi_Profile profile)
            => this.SetOptionRange("-profile", (int)profile, -99, 255);

        /// <summary>
        /// Set tier (general_tier_flag) (from 0 to 1) (default main)
        /// </summary>
        public Hevc_vaapi_CodecEncoder Tier(Hevc_vaapi_Tier tier)
            => this.SetOptionRange("-tier", (int)tier, 0, 1);

        /// <summary>
        /// Set level (general_level_idc) (default -99)
        /// </summary>
        public Hevc_vaapi_CodecEncoder Level(Hevc_vaapi_Level level)
            => this.SetOptionRange("-level", (int)level, -99, 255);

        /// <summary>
        /// Tile columns x rows (e.g. "2x2")
        /// </summary>
        public Hevc_vaapi_CodecEncoder Tiles(string colsXrows)
            => this.SetOption("-tiles", colsXrows);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Hevc_vaapi_RCMode
    {
        /// <summary>Choose mode automatically based on other parameters</summary>
        auto = 0,
        /// <summary>Constant-quality</summary>
        CQP = 1,
        /// <summary>Constant-bitrate</summary>
        CBR = 2,
        /// <summary>Variable-bitrate</summary>
        VBR = 3,
        /// <summary>Intelligent constant-quality</summary>
        ICQ = 4,
        /// <summary>Quality-defined variable-bitrate</summary>
        QVBR = 5,
        /// <summary>Average variable-bitrate</summary>
        AVBR = 6,
    }
    public enum Hevc_vaapi_Profile
    {
        main = 1,
        main10 = 2,
        rext = 4,
    }
    public enum Hevc_vaapi_Tier
    {
        main = 0,
        high = 1,
    }
    public enum Hevc_vaapi_Level
    {
        Level_1 = 30,
        Level_2 = 60,
        Level_2_1 = 63,
        Level_3 = 90,
        Level_3_1 = 93,
        Level_4 = 120,
        Level_4_1 = 123,
        Level_5 = 150,
        Level_5_1 = 153,
        Level_5_2 = 156,
        Level_6 = 180,
        Level_6_1 = 183,
        Level_6_2 = 186,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// VA-API H.265/HEVC encoder.
        /// </summary>
        public static Hevc_vaapi_CodecEncoder Hevc_vaapi_Codec(this ImageOutputAVStream stream)
            => new Hevc_vaapi_CodecEncoder(stream);
        public static T Hevc_vaapi_Codec<T>(this T stream, Action<Hevc_vaapi_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Hevc_vaapi_Codec());
            return stream;
        }
    }
}
