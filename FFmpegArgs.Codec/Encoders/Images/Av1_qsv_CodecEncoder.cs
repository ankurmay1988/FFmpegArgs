/*av1_qsv encoder AVOptions:
  -async_depth       <int>        E..V....... Maximum processing parallelism (from 1 to INT_MAX) (default 4)
  -preset            <int>        E..V....... (from 0 to 7) (default 0)
     veryfast        7            E..V.......
     faster          6            E..V.......
     fast            5            E..V.......
     medium          4            E..V.......
     slow            3            E..V.......
     slower          2            E..V.......
     veryslow        1            E..V.......
  -forced_idr        <boolean>    E..V....... Forcing I frames as IDR frames (default false)
  -low_power         <boolean>    E..V....... enable low power mode (default auto)
  -b_strategy        <int>        E..V....... Strategy to choose between I/P/B-frames (from -1 to 1) (default -1)
  -adaptive_i        <int>        E..V....... Adaptive I-frame placement (from -1 to 1) (default -1)
  -adaptive_b        <int>        E..V....... Adaptive B-frame placement (from -1 to 1) (default -1)
  -extbrc            <int>        E..V....... Extended bitrate control (from -1 to 1) (default -1)
  -low_delay_brc     <boolean>    E..V....... Allow to strictly obey avg frame size (default auto)
  -max_frame_size    <int>        E..V....... Maximum encoded frame size in bytes (from -1 to INT_MAX) (default -1)
  -max_frame_size_i  <int>        E..V....... Maximum encoded I frame size in bytes (from -1 to INT_MAX) (default -1)
  -max_frame_size_p  <int>        E..V....... Maximum encoded P frame size in bytes (from -1 to INT_MAX) (default -1)
  -profile           <int>        E..V....... (from 0 to INT_MAX) (default unknown)
     unknown         0            E..V.......
     main            1            E..V.......
  -tile_cols         <int>        E..V....... Number of columns for tiled encoding (from 0 to 65535) (default 0)
  -tile_rows         <int>        E..V....... Number of rows for tiled encoding (from 0 to 65535) (default 0)
  -look_ahead_depth  <int>        E..V....... Depth of look ahead in number frames (from 0 to 100) (default 0)
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// Intel QuickSync AV1 encoder (av1_qsv).
    /// </summary>
    public class Av1_qsv_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Av1_qsv_CodecEncoder(ImageOutputAVStream stream) : base("av1_qsv", stream)
        {
        }

        /// <summary>
        /// Maximum processing parallelism (from 1 to INT_MAX) (default 4)
        /// </summary>
        public Av1_qsv_CodecEncoder AsyncDepth(int depth)
            => this.SetOptionRange("-async_depth", depth, 1, INT_MAX);

        /// <summary>
        /// Encoding preset (from 0 to 7) (default 0)
        /// </summary>
        public Av1_qsv_CodecEncoder Preset(Av1_qsv_Preset preset)
            => this.SetOptionRange("-preset", (int)preset, 0, 7);

        /// <summary>
        /// Forcing I frames as IDR frames (default false)
        /// </summary>
        public Av1_qsv_CodecEncoder ForcedIdr(bool flag)
            => this.SetOption("-forced_idr", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable low power mode (default auto)
        /// </summary>
        public Av1_qsv_CodecEncoder LowPower(bool flag)
            => this.SetOption("-low_power", flag.ToFFmpegFlag());

        /// <summary>
        /// Strategy to choose between I/P/B-frames (from -1 to 1) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder BStrategy(int strategy)
            => this.SetOptionRange("-b_strategy", strategy, -1, 1);

        /// <summary>
        /// Adaptive I-frame placement (from -1 to 1) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder AdaptiveI(int value)
            => this.SetOptionRange("-adaptive_i", value, -1, 1);

        /// <summary>
        /// Adaptive B-frame placement (from -1 to 1) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder AdaptiveB(int value)
            => this.SetOptionRange("-adaptive_b", value, -1, 1);

        /// <summary>
        /// Extended bitrate control (from -1 to 1) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder ExtBRC(int value)
            => this.SetOptionRange("-extbrc", value, -1, 1);

        /// <summary>
        /// Allow to strictly obey avg frame size (default auto)
        /// </summary>
        public Av1_qsv_CodecEncoder LowDelayBRC(bool flag)
            => this.SetOption("-low_delay_brc", flag.ToFFmpegFlag());

        /// <summary>
        /// Maximum encoded frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder MaxFrameSize(int size)
            => this.SetOptionRange("-max_frame_size", size, -1, INT_MAX);

        /// <summary>
        /// Maximum encoded I frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder MaxFrameSizeI(int size)
            => this.SetOptionRange("-max_frame_size_i", size, -1, INT_MAX);

        /// <summary>
        /// Maximum encoded P frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Av1_qsv_CodecEncoder MaxFrameSizeP(int size)
            => this.SetOptionRange("-max_frame_size_p", size, -1, INT_MAX);

        /// <summary>
        /// Set encoding profile (default unknown)
        /// </summary>
        public Av1_qsv_CodecEncoder Profile(Av1_qsv_Profile profile)
            => this.SetOptionRange("-profile", (int)profile, 0, INT_MAX);

        /// <summary>
        /// Number of columns for tiled encoding (from 0 to 65535) (default 0)
        /// </summary>
        public Av1_qsv_CodecEncoder TileCols(int cols)
            => this.SetOptionRange("-tile_cols", cols, 0, 65535);

        /// <summary>
        /// Number of rows for tiled encoding (from 0 to 65535) (default 0)
        /// </summary>
        public Av1_qsv_CodecEncoder TileRows(int rows)
            => this.SetOptionRange("-tile_rows", rows, 0, 65535);

        /// <summary>
        /// Depth of look ahead in number frames, available when extbrc option is enabled (from 0 to 100) (default 0)
        /// </summary>
        public Av1_qsv_CodecEncoder LookAheadDepth(int depth)
            => this.SetOptionRange("-look_ahead_depth", depth, 0, 100);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Av1_qsv_Preset
    {
        veryfast = 7,
        faster = 6,
        fast = 5,
        medium = 4,
        slow = 3,
        slower = 2,
        veryslow = 1,
    }
    public enum Av1_qsv_Profile
    {
        unknown = 0,
        main = 1,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// Intel QuickSync AV1 encoder.
        /// </summary>
        public static Av1_qsv_CodecEncoder Av1_qsv_Codec(this ImageOutputAVStream stream)
            => new Av1_qsv_CodecEncoder(stream);
        public static T Av1_qsv_Codec<T>(this T stream, Action<Av1_qsv_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Av1_qsv_Codec());
            return stream;
        }
    }
}
