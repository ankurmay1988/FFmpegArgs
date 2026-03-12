/*hevc_qsv encoder AVOptions:
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
  -rdo               <int>        E..V....... Enable rate distortion optimization (from -1 to 1) (default -1)
  -max_frame_size    <int>        E..V....... Maximum encoded frame size in bytes (from -1 to INT_MAX) (default -1)
  -max_frame_size_i  <int>        E..V....... Maximum encoded I frame size in bytes (from -1 to INT_MAX) (default -1)
  -max_frame_size_p  <int>        E..V....... Maximum encoded P frame size in bytes (from -1 to INT_MAX) (default -1)
  -max_slice_size    <int>        E..V....... Maximum encoded slice size in bytes (from -1 to INT_MAX) (default -1)
  -mbbrc             <int>        E..V....... MB level bitrate control (from -1 to 1) (default -1)
  -extbrc            <int>        E..V....... Extended bitrate control (from -1 to 1) (default -1)
  -p_strategy        <int>        E..V....... Enable P-pyramid: 0-default 1-simple 2-pyramid (from 0 to 2) (default 0)
  -b_strategy        <int>        E..V....... Strategy to choose between I/P/B-frames (from -1 to 1) (default -1)
  -dblk_idc          <int>        E..V....... This option disable deblocking. It has value in range 0~2. (from 0 to 2) (default 0)
  -low_delay_brc     <boolean>    E..V....... Allow to strictly obey avg frame size (default auto)
  -max_qp_i          <int>        E..V....... Maximum video quantizer scale for I frame (from -1 to 51) (default -1)
  -min_qp_i          <int>        E..V....... Minimum video quantizer scale for I frame (from -1 to 51) (default -1)
  -max_qp_p          <int>        E..V....... Maximum video quantizer scale for P frame (from -1 to 51) (default -1)
  -min_qp_p          <int>        E..V....... Minimum video quantizer scale for P frame (from -1 to 51) (default -1)
  -max_qp_b          <int>        E..V....... Maximum video quantizer scale for B frame (from -1 to 51) (default -1)
  -min_qp_b          <int>        E..V....... Minimum video quantizer scale for B frame (from -1 to 51) (default -1)
  -adaptive_i        <int>        E..V....... Adaptive I-frame placement (from -1 to 1) (default -1)
  -adaptive_b        <int>        E..V....... Adaptive B-frame placement (from -1 to 1) (default -1)
  -scenario          <int>        E..V....... A hint to encoder about the scenario for the encoding session (from 0 to 8) (default unknown)
  -avbr_accuracy     <int>        E..V....... Accuracy of the AVBR ratecontrol (unit of tenth of percent) (from 0 to 65535) (default 0)
  -avbr_convergence  <int>        E..V....... Convergence of the AVBR ratecontrol (unit of 100 frames) (from 0 to 65535) (default 0)
  -skip_frame        <int>        E..V....... Allow frame skipping (from 0 to 3) (default no_skip)
  -dual_gfx          <int>        E..V....... Prefer processing on both iGfx and dGfx simultaneously (from 0 to 2) (default off)
  -idr_interval      <int>        E..V....... Distance (in I-frames) between IDR frames (from -1 to INT_MAX) (default 0)
  -load_plugin       <int>        E..V....... A user plugin to load in an internal session (from 0 to 2) (default hevc_hw)
  -load_plugins      <string>     E..V....... A :-separate list of hexadecimal plugin UIDs to load in an internal session
  -look_ahead_depth  <int>        E..V....... Depth of look ahead in number frames (from 0 to 100) (default 0)
  -profile           <int>        E..V....... (from 0 to INT_MAX) (default unknown)
     unknown         0            E..V.......
     main            1            E..V.......
     main10          2            E..V.......
     mainsp          3            E..V.......
     rext            4            E..V.......
     scc             9            E..V.......
  -tier              <int>        E..V....... Set the encoding tier (from 0 to 256) (default high)
     main            0            E..V.......
     high            256          E..V.......
  -gpb               <boolean>    E..V....... 1: GPB (generalized P/B frame); 0: regular P frame (default true)
  -tile_cols         <int>        E..V....... Number of columns for tiled encoding (from 0 to 65535) (default 0)
  -tile_rows         <int>        E..V....... Number of rows for tiled encoding (from 0 to 65535) (default 0)
  -recovery_point_sei <int>       E..V....... Insert recovery point SEI messages (from -1 to 1) (default -1)
  -aud               <boolean>    E..V....... Insert the Access Unit Delimiter NAL (default false)
  -pic_timing_sei    <boolean>    E..V....... Insert picture timing SEI with pic_struct_syntax element (default true)
  -transform_skip    <int>        E..V....... Turn this option ON to enable transformskip (from -1 to 1) (default -1)
  -int_ref_type      <int>        E..V....... Intra refresh type (from -1 to 65535) (default -1)
  -int_ref_cycle_size <int>       E..V....... Number of frames in the intra refresh cycle (from -1 to 65535) (default -1)
  -int_ref_qp_delta  <int>        E..V....... QP difference for the refresh MBs (from -32768 to 32767) (default -32768)
  -int_ref_cycle_dist <int>       E..V....... Distance between the beginnings of the intra-refresh cycles in frames (from -1 to 32767) (default -1)
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// Intel QuickSync H.265/HEVC encoder.
    /// </summary>
    public class Hevc_qsv_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Hevc_qsv_CodecEncoder(ImageOutputAVStream stream) : base("hevc_qsv", stream)
        {
        }

        /// <summary>
        /// Maximum processing parallelism (from 1 to INT_MAX) (default 4)
        /// </summary>
        public Hevc_qsv_CodecEncoder AsyncDepth(int depth)
            => this.SetOptionRange("-async_depth", depth, 1, INT_MAX);

        /// <summary>
        /// Encoding preset (from 0 to 7) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder Preset(Hevc_qsv_Preset preset)
            => this.SetOptionRange("-preset", (int)preset, 0, 7);

        /// <summary>
        /// Forcing I frames as IDR frames (default false)
        /// </summary>
        public Hevc_qsv_CodecEncoder ForcedIdr(bool flag)
            => this.SetOption("-forced_idr", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable low power mode (default auto)
        /// </summary>
        public Hevc_qsv_CodecEncoder LowPower(bool flag)
            => this.SetOption("-low_power", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable rate distortion optimization (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder RDO(int rdo)
            => this.SetOptionRange("-rdo", rdo, -1, 1);

        /// <summary>
        /// Maximum encoded frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxFrameSize(int size)
            => this.SetOptionRange("-max_frame_size", size, -1, INT_MAX);

        /// <summary>
        /// Maximum encoded I frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxFrameSizeI(int size)
            => this.SetOptionRange("-max_frame_size_i", size, -1, INT_MAX);

        /// <summary>
        /// Maximum encoded P frame size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxFrameSizeP(int size)
            => this.SetOptionRange("-max_frame_size_p", size, -1, INT_MAX);

        /// <summary>
        /// Maximum encoded slice size in bytes (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxSliceSize(int size)
            => this.SetOptionRange("-max_slice_size", size, -1, INT_MAX);

        /// <summary>
        /// MB level bitrate control (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MBBRC(int mbbrc)
            => this.SetOptionRange("-mbbrc", mbbrc, -1, 1);

        /// <summary>
        /// Extended bitrate control (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder ExtBRC(int extbrc)
            => this.SetOptionRange("-extbrc", extbrc, -1, 1);

        /// <summary>
        /// Enable P-pyramid: 0-default 1-simple 2-pyramid (from 0 to 2) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder PStrategy(int strategy)
            => this.SetOptionRange("-p_strategy", strategy, 0, 2);

        /// <summary>
        /// Strategy to choose between I/P/B-frames (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder BStrategy(int strategy)
            => this.SetOptionRange("-b_strategy", strategy, -1, 1);

        /// <summary>
        /// Disable deblocking. Value in range 0-2. (from 0 to 2) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder DblkIdc(int value)
            => this.SetOptionRange("-dblk_idc", value, 0, 2);

        /// <summary>
        /// Allow to strictly obey avg frame size (default auto)
        /// </summary>
        public Hevc_qsv_CodecEncoder LowDelayBRC(bool flag)
            => this.SetOption("-low_delay_brc", flag.ToFFmpegFlag());

        /// <summary>
        /// Maximum video quantizer scale for I frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxQPI(int qp)
            => this.SetOptionRange("-max_qp_i", qp, -1, 51);

        /// <summary>
        /// Minimum video quantizer scale for I frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MinQPI(int qp)
            => this.SetOptionRange("-min_qp_i", qp, -1, 51);

        /// <summary>
        /// Maximum video quantizer scale for P frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxQPP(int qp)
            => this.SetOptionRange("-max_qp_p", qp, -1, 51);

        /// <summary>
        /// Minimum video quantizer scale for P frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MinQPP(int qp)
            => this.SetOptionRange("-min_qp_p", qp, -1, 51);

        /// <summary>
        /// Maximum video quantizer scale for B frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MaxQPB(int qp)
            => this.SetOptionRange("-max_qp_b", qp, -1, 51);

        /// <summary>
        /// Minimum video quantizer scale for B frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder MinQPB(int qp)
            => this.SetOptionRange("-min_qp_b", qp, -1, 51);

        /// <summary>
        /// Adaptive I-frame placement (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder AdaptiveI(int value)
            => this.SetOptionRange("-adaptive_i", value, -1, 1);

        /// <summary>
        /// Adaptive B-frame placement (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder AdaptiveB(int value)
            => this.SetOptionRange("-adaptive_b", value, -1, 1);

        /// <summary>
        /// A hint to encoder about the scenario for the encoding session (from 0 to 8) (default unknown)
        /// </summary>
        public Hevc_qsv_CodecEncoder Scenario(Hevc_qsv_Scenario scenario)
            => this.SetOptionRange("-scenario", (int)scenario, 0, 8);

        /// <summary>
        /// Accuracy of the AVBR ratecontrol (unit of tenth of percent) (from 0 to 65535) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder AVBRAccuracy(int accuracy)
            => this.SetOptionRange("-avbr_accuracy", accuracy, 0, 65535);

        /// <summary>
        /// Convergence of the AVBR ratecontrol (unit of 100 frames) (from 0 to 65535) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder AVBRConvergence(int convergence)
            => this.SetOptionRange("-avbr_convergence", convergence, 0, 65535);

        /// <summary>
        /// Allow frame skipping (from 0 to 3) (default no_skip)
        /// </summary>
        public Hevc_qsv_CodecEncoder SkipFrame(Hevc_qsv_SkipFrame skipFrame)
            => this.SetOptionRange("-skip_frame", (int)skipFrame, 0, 3);

        /// <summary>
        /// Prefer processing on both iGfx and dGfx simultaneously (from 0 to 2) (default off)
        /// </summary>
        public Hevc_qsv_CodecEncoder DualGfx(Hevc_qsv_DualGfx dualGfx)
            => this.SetOptionRange("-dual_gfx", (int)dualGfx, 0, 2);

        /// <summary>
        /// Distance (in I-frames) between IDR frames (from -1 to INT_MAX) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder IdrInterval(int interval)
            => this.SetOptionRange("-idr_interval", interval, -1, INT_MAX);

        /// <summary>
        /// A user plugin to load in an internal session (from 0 to 2) (default hevc_hw)
        /// </summary>
        public Hevc_qsv_CodecEncoder LoadPlugin(Hevc_qsv_LoadPlugin plugin)
            => this.SetOptionRange("-load_plugin", (int)plugin, 0, 2);

        /// <summary>
        /// A :-separate list of hexadecimal plugin UIDs to load in an internal session
        /// </summary>
        public Hevc_qsv_CodecEncoder LoadPlugins(string plugins)
            => this.SetOption("-load_plugins", plugins);

        /// <summary>
        /// Depth of look ahead in number frames (from 0 to 100) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder LookAheadDepth(int depth)
            => this.SetOptionRange("-look_ahead_depth", depth, 0, 100);

        /// <summary>
        /// Encoding profile (default unknown)
        /// </summary>
        public Hevc_qsv_CodecEncoder Profile(Hevc_qsv_Profile profile)
            => this.SetOptionRange("-profile", (int)profile, 0, INT_MAX);

        /// <summary>
        /// Set the encoding tier (from 0 to 256) (default high)
        /// </summary>
        public Hevc_qsv_CodecEncoder Tier(Hevc_qsv_Tier tier)
            => this.SetOptionRange("-tier", (int)tier, 0, 256);

        /// <summary>
        /// 1: GPB (generalized P/B frame); 0: regular P frame (default true)
        /// </summary>
        public Hevc_qsv_CodecEncoder GPB(bool flag)
            => this.SetOption("-gpb", flag.ToFFmpegFlag());

        /// <summary>
        /// Number of columns for tiled encoding (from 0 to 65535) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder TileCols(int cols)
            => this.SetOptionRange("-tile_cols", cols, 0, 65535);

        /// <summary>
        /// Number of rows for tiled encoding (from 0 to 65535) (default 0)
        /// </summary>
        public Hevc_qsv_CodecEncoder TileRows(int rows)
            => this.SetOptionRange("-tile_rows", rows, 0, 65535);

        /// <summary>
        /// Insert recovery point SEI messages (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder RecoveryPointSEI(int value)
            => this.SetOptionRange("-recovery_point_sei", value, -1, 1);

        /// <summary>
        /// Insert the Access Unit Delimiter NAL (default false)
        /// </summary>
        public Hevc_qsv_CodecEncoder AUD(bool flag)
            => this.SetOption("-aud", flag.ToFFmpegFlag());

        /// <summary>
        /// Insert picture timing SEI with pic_struct_syntax element (default true)
        /// </summary>
        public Hevc_qsv_CodecEncoder PicTimingSEI(bool flag)
            => this.SetOption("-pic_timing_sei", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable transformskip (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder TransformSkip(int value)
            => this.SetOptionRange("-transform_skip", value, -1, 1);

        /// <summary>
        /// Intra refresh type. B frames should be set to 0 (from -1 to 65535) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder IntRefType(Hevc_qsv_IntRefType type)
            => this.SetOptionRange("-int_ref_type", (int)type, -1, 65535);

        /// <summary>
        /// Number of frames in the intra refresh cycle (from -1 to 65535) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder IntRefCycleSize(int size)
            => this.SetOptionRange("-int_ref_cycle_size", size, -1, 65535);

        /// <summary>
        /// QP difference for the refresh MBs (from -32768 to 32767) (default -32768)
        /// </summary>
        public Hevc_qsv_CodecEncoder IntRefQPDelta(int delta)
            => this.SetOptionRange("-int_ref_qp_delta", delta, -32768, 32767);

        /// <summary>
        /// Distance between the beginnings of the intra-refresh cycles in frames (from -1 to 32767) (default -1)
        /// </summary>
        public Hevc_qsv_CodecEncoder IntRefCycleDist(int dist)
            => this.SetOptionRange("-int_ref_cycle_dist", dist, -1, 32767);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Hevc_qsv_Preset
    {
        veryslow = 1,
        slower = 2,
        slow = 3,
        medium = 4,
        fast = 5,
        faster = 6,
        veryfast = 7,
    }
    public enum Hevc_qsv_Profile
    {
        unknown = 0,
        main = 1,
        main10 = 2,
        mainsp = 3,
        rext = 4,
        scc = 9,
    }
    public enum Hevc_qsv_Tier
    {
        main = 0,
        high = 256,
    }
    public enum Hevc_qsv_Scenario
    {
        unknown = 0,
        displayremoting = 1,
        videoconference = 2,
        archive = 3,
        livestreaming = 4,
        cameracapture = 5,
        videosurveillance = 6,
        gamestreaming = 7,
        remotegaming = 8,
    }
    public enum Hevc_qsv_SkipFrame
    {
        /// <summary>Frame skipping is disabled</summary>
        no_skip = 0,
        /// <summary>Encoder inserts into bitstream frame where all macroblocks are encoded as skipped</summary>
        insert_dummy = 1,
        /// <summary>Encoder inserts nothing into bitstream</summary>
        insert_nothing = 2,
        /// <summary>skip_frame metadata indicates the number of missed frames before the current frame</summary>
        brc_only = 3,
    }
    public enum Hevc_qsv_DualGfx
    {
        /// <summary>Disable HyperEncode mode</summary>
        off = 0,
        /// <summary>Enable HyperEncode mode and return error if incompatible parameters during initialization</summary>
        on = 1,
        /// <summary>Enable HyperEncode mode or fallback to single GPU if incompatible parameters during initialization</summary>
        adaptive = 2,
    }
    public enum Hevc_qsv_LoadPlugin
    {
        none = 0,
        hevc_sw = 1,
        hevc_hw = 2,
    }
    public enum Hevc_qsv_IntRefType
    {
        none = 0,
        vertical = 1,
        horizontal = 2,
        slice = 3,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// Intel QuickSync H.265/HEVC encoder.
        /// </summary>
        public static Hevc_qsv_CodecEncoder Hevc_qsv_Codec(this ImageOutputAVStream stream)
            => new Hevc_qsv_CodecEncoder(stream);
        public static T Hevc_qsv_Codec<T>(this T stream, Action<Hevc_qsv_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Hevc_qsv_Codec());
            return stream;
        }
    }
}
