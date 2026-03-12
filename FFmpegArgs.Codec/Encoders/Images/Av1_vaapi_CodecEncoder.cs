/*av1_vaapi AVOptions:
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
  -profile           <int>        E..V....... Set profile (seq_profile) (from -99 to 255) (default -99)
     main            0            E..V.......
     high            1            E..V.......
     professional    2            E..V.......
  -tier              <int>        E..V....... Set tier (seq_tier) (from 0 to 1) (default main)
     main            0            E..V.......
     high            1            E..V.......
  -level             <int>        E..V....... Set level (seq_level_idx) (from -99 to 31) (default -99)
     2.0             0            E..V.......
     2.1             1            E..V.......
     3.0             4            E..V.......
     3.1             5            E..V.......
     4.0             8            E..V.......
     4.1             9            E..V.......
     5.0             12           E..V.......
     5.1             13           E..V.......
     5.2             14           E..V.......
     5.3             15           E..V.......
     6.0             16           E..V.......
     6.1             17           E..V.......
     6.2             18           E..V.......
     6.3             19           E..V.......
  -tiles             <image_size> E..V....... Tile columns x rows
  -tile_groups       <int>        E..V....... Number of tile groups for encoding (from 1 to 4096) (default 1)
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// VA-API AV1 encoder (av1_vaapi).
    /// </summary>
    public class Av1_vaapi_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Av1_vaapi_CodecEncoder(ImageOutputAVStream stream) : base("av1_vaapi", stream)
        {
        }

        /// <summary>
        /// Use low-power encoding mode (default false)
        /// </summary>
        public Av1_vaapi_CodecEncoder LowPower(bool flag)
            => this.SetOption("-low_power", flag.ToFFmpegFlag());

        /// <summary>
        /// Distance (in I-frames) between IDR frames (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Av1_vaapi_CodecEncoder IdrInterval(int interval)
            => this.SetOptionRange("-idr_interval", interval, 0, INT_MAX);

        /// <summary>
        /// Maximum B-frame reference depth (from 1 to INT_MAX) (default 1)
        /// </summary>
        public Av1_vaapi_CodecEncoder BDepth(int depth)
            => this.SetOptionRange("-b_depth", depth, 1, INT_MAX);

        /// <summary>
        /// Maximum processing parallelism (from 1 to 64) (default 2)
        /// </summary>
        public Av1_vaapi_CodecEncoder AsyncDepth(int depth)
            => this.SetOptionRange("-async_depth", depth, 1, 64);

        /// <summary>
        /// Maximum frame size (in bytes) (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Av1_vaapi_CodecEncoder MaxFrameSize(int size)
            => this.SetOptionRange("-max_frame_size", size, 0, INT_MAX);

        /// <summary>
        /// Set rate control mode (from 0 to 6) (default auto)
        /// </summary>
        public Av1_vaapi_CodecEncoder RCMode(Av1_vaapi_RCMode mode)
            => this.SetOptionRange("-rc_mode", (int)mode, 0, 6);

        /// <summary>
        /// Block level based bitrate control (default false)
        /// </summary>
        public Av1_vaapi_CodecEncoder BLBRC(bool flag)
            => this.SetOption("-blbrc", flag.ToFFmpegFlag());

        /// <summary>
        /// Set profile (seq_profile) (default -99)
        /// </summary>
        public Av1_vaapi_CodecEncoder Profile(Av1_vaapi_Profile profile)
            => this.SetOptionRange("-profile", (int)profile, -99, 255);

        /// <summary>
        /// Set tier (seq_tier) (from 0 to 1) (default main)
        /// </summary>
        public Av1_vaapi_CodecEncoder Tier(Av1_vaapi_Tier tier)
            => this.SetOptionRange("-tier", (int)tier, 0, 1);

        /// <summary>
        /// Set level (seq_level_idx) (default -99)
        /// </summary>
        public Av1_vaapi_CodecEncoder Level(Av1_vaapi_Level level)
            => this.SetOptionRange("-level", (int)level, -99, 31);

        /// <summary>
        /// Tile columns x rows (e.g. "2x2")
        /// </summary>
        public Av1_vaapi_CodecEncoder Tiles(string colsXrows)
            => this.SetOption("-tiles", colsXrows);

        /// <summary>
        /// Number of tile groups for encoding (from 1 to 4096) (default 1)
        /// </summary>
        public Av1_vaapi_CodecEncoder TileGroups(int groups)
            => this.SetOptionRange("-tile_groups", groups, 1, 4096);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Av1_vaapi_RCMode
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
    public enum Av1_vaapi_Profile
    {
        main = 0,
        high = 1,
        professional = 2,
    }
    public enum Av1_vaapi_Tier
    {
        main = 0,
        high = 1,
    }
    public enum Av1_vaapi_Level
    {
        Level_2_0 = 0,
        Level_2_1 = 1,
        Level_3_0 = 4,
        Level_3_1 = 5,
        Level_4_0 = 8,
        Level_4_1 = 9,
        Level_5_0 = 12,
        Level_5_1 = 13,
        Level_5_2 = 14,
        Level_5_3 = 15,
        Level_6_0 = 16,
        Level_6_1 = 17,
        Level_6_2 = 18,
        Level_6_3 = 19,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// VA-API AV1 encoder.
        /// </summary>
        public static Av1_vaapi_CodecEncoder Av1_vaapi_Codec(this ImageOutputAVStream stream)
            => new Av1_vaapi_CodecEncoder(stream);
        public static T Av1_vaapi_Codec<T>(this T stream, Action<Av1_vaapi_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Av1_vaapi_Codec());
            return stream;
        }
    }
}
