/*hevc_amf AVOptions:
  -usage             <int>        E..V....... Set the encoding usage (from 0 to 5) (default transcoding)
     transcoding     0            E..V....... Generic Transcoding
     ultralowlatency 1            E..V....... ultra low latency trancoding
     lowlatency      2            E..V....... low latency trancoding
     webcam          3            E..V....... Webcam
     high_quality    4            E..V....... high quality trancoding
     lowlatency_high_quality 5   E..V....... low latency yet high quality trancoding
  -profile           <int>        E..V....... Set the profile (default main) (from 1 to 1)
     main            1            E..V.......
  -profile_tier      <int>        E..V....... Set the profile tier (default main) (from 0 to 1)
     main            0            E..V.......
     high            1            E..V.......
  -level             <int>        E..V....... Set the encoding level (default auto) (from 0 to 186)
     auto            0            E..V.......
     1.0             30           E..V.......
     2.0             60           E..V.......
     2.1             63           E..V.......
     3.0             90           E..V.......
     3.1             93           E..V.......
     4.0             120          E..V.......
     4.1             123          E..V.......
     5.0             150          E..V.......
     5.1             153          E..V.......
     5.2             156          E..V.......
     6.0             180          E..V.......
     6.1             183          E..V.......
     6.2             186          E..V.......
  -quality           <int>        E..V....... Set the encoding quality (from 0 to 10) (default speed)
     balanced        5            E..V.......
     speed           10           E..V.......
     quality         0            E..V.......
  -rc                <int>        E..V....... Set the rate control mode (from -1 to 6) (default -1)
     cqp             0            E..V....... Constant Quantization Parameter
     cbr             3            E..V....... Constant Bitrate
     vbr_peak        2            E..V....... Peak Contrained Variable Bitrate
     vbr_latency     1            E..V....... Latency Constrained Variable Bitrate
     qvbr            4            E..V....... Quality Variable Bitrate
     hqvbr           5            E..V....... High Quality Variable Bitrate
     hqcbr           6            E..V....... High Quality Constant Bitrate
  -qvbr_quality_level <int>       E..V....... Sets the QVBR quality level (from -1 to 51) (default -1)
  -header_insertion_mode <int>    E..V....... Set header insertion mode (from 0 to 2) (default none)
     none            0            E..V.......
     gop             1            E..V.......
     idr             2            E..V.......
  -high_motion_quality_boost_enable <boolean> E..V....... Enable High motion quality boost mode (default auto)
  -gops_per_idr      <int>        E..V....... GOPs per IDR 0-no IDR will be inserted (from 0 to INT_MAX) (default 1)
  -preencode         <boolean>    E..V....... Enable preencode (default false)
  -vbaq              <boolean>    E..V....... Enable VBAQ (default false)
  -enforce_hrd       <boolean>    E..V....... Enforce HRD (default false)
  -filler_data       <boolean>    E..V....... Filler Data Enable (default false)
  -max_au_size       <int>        E..V....... Maximum Access Unit Size for rate control (in bits) (from 0 to INT_MAX) (default 0)
  -min_qp_i          <int>        E..V....... min quantization parameter for I-frame (from -1 to 51) (default -1)
  -max_qp_i          <int>        E..V....... max quantization parameter for I-frame (from -1 to 51) (default -1)
  -min_qp_p          <int>        E..V....... min quantization parameter for P-frame (from -1 to 51) (default -1)
  -max_qp_p          <int>        E..V....... max quantization parameter for P-frame (from -1 to 51) (default -1)
  -qp_p              <int>        E..V....... quantization parameter for P-frame (from -1 to 51) (default -1)
  -qp_i              <int>        E..V....... quantization parameter for I-frame (from -1 to 51) (default -1)
  -skip_frame        <boolean>    E..V....... Rate Control Based Frame Skip (default false)
  -me_half_pel       <boolean>    E..V....... Enable ME Half Pixel (default true)
  -me_quarter_pel    <boolean>    E..V....... Enable ME Quarter Pixel  (default true)
  -aud               <boolean>    E..V....... Inserts AU Delimiter NAL unit (default false)
  -log_to_dbg        <boolean>    E..V....... Enable AMF logging to debug output (default false)
  -preanalysis       <boolean>    E..V....... Enable preanalysis (default auto)
  -pa_activity_type  <int>        E..V....... Set the type of activity analysis (from -1 to 1) (default -1)
  -pa_scene_change_detection_enable <boolean> E..V....... Enable scene change detection (default auto)
  -pa_scene_change_detection_sensitivity <int> E..V....... Set the sensitivity of scene change detection (from -1 to 2) (default -1)
  -pa_static_scene_detection_enable <boolean> E..V....... Enable static scene detection (default auto)
  -pa_static_scene_detection_sensitivity <int> E..V....... Set the sensitivity of static scene detection (from -1 to 2) (default -1)
  -pa_initial_qp_after_scene_change <int>     E..V....... The QP value that is used immediately after a scene change (from -1 to 51) (default -1)
  -pa_max_qp_before_force_skip <int>          E..V....... The QP threshold to allow a skip frame (from -1 to 51) (default -1)
  -pa_caq_strength   <int>        E..V....... Content Adaptive Quantization strength (from -1 to 2) (default -1)
  -pa_frame_sad_enable <boolean>  E..V....... Enable Frame SAD algorithm (default auto)
  -pa_ltr_enable     <boolean>    E..V....... Enable long term reference frame management (default auto)
  -pa_lookahead_buffer_depth <int> E..V....... Sets the PA lookahead buffer size (from -1 to 41) (default -1)
  -pa_paq_mode       <int>        E..V....... Sets the perceptual adaptive quantization mode (from -1 to 1) (default -1)
  -pa_taq_mode       <int>        E..V....... Sets the temporal adaptive quantization mode (from -1 to 2) (default -1)
  -pa_high_motion_quality_boost_mode <int>    E..V....... Sets the PA high motion quality boost mode (from -1 to 1) (default -1)
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// AMD AMF H.265/HEVC encoder.
    /// </summary>
    public class Hevc_amf_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Hevc_amf_CodecEncoder(ImageOutputAVStream stream) : base("hevc_amf", stream)
        {
        }

        /// <summary>
        /// Set the encoding usage (from 0 to 5) (default transcoding)
        /// </summary>
        public Hevc_amf_CodecEncoder Usage(Hevc_amf_Usage usage)
            => this.SetOptionRange("-usage", (int)usage, 0, 5);

        /// <summary>
        /// Set the profile tier (from 0 to 1) (default main)
        /// </summary>
        public Hevc_amf_CodecEncoder ProfileTier(Hevc_amf_Tier tier)
            => this.SetOptionRange("-profile_tier", (int)tier, 0, 1);

        /// <summary>
        /// Set the encoding level (from 0 to 186) (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder Level(Hevc_amf_Level level)
            => this.SetOptionRange("-level", (int)level, 0, 186);

        /// <summary>
        /// Set the encoding quality (from 0 to 10) (default speed)
        /// </summary>
        public Hevc_amf_CodecEncoder Quality(Hevc_amf_Quality quality)
            => this.SetOptionRange("-quality", (int)quality, 0, 10);

        /// <summary>
        /// Set the rate control mode (from -1 to 6) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder RateControl(Hevc_amf_RateControl rateControl)
            => this.SetOptionRange("-rc", (int)rateControl, -1, 6);

        /// <summary>
        /// Sets the QVBR quality level (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder QVBRQualityLevel(int quality)
            => this.SetOptionRange("-qvbr_quality_level", quality, -1, 51);

        /// <summary>
        /// Set header insertion mode (from 0 to 2) (default none)
        /// </summary>
        public Hevc_amf_CodecEncoder HeaderInsertionMode(Hevc_amf_HeaderInsertionMode mode)
            => this.SetOptionRange("-header_insertion_mode", (int)mode, 0, 2);

        /// <summary>
        /// Enable High motion quality boost mode (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder HighMotionQualityBoostEnable(bool flag)
            => this.SetOption("-high_motion_quality_boost_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// GOPs per IDR 0-no IDR will be inserted (from 0 to INT_MAX) (default 1)
        /// </summary>
        public Hevc_amf_CodecEncoder GopsPerIdr(int gops)
            => this.SetOptionRange("-gops_per_idr", gops, 0, INT_MAX);

        /// <summary>
        /// Enable preencode (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder Preencode(bool flag)
            => this.SetOption("-preencode", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable VBAQ (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder VBAQ(bool flag)
            => this.SetOption("-vbaq", flag.ToFFmpegFlag());

        /// <summary>
        /// Enforce HRD (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder EnforceHRD(bool flag)
            => this.SetOption("-enforce_hrd", flag.ToFFmpegFlag());

        /// <summary>
        /// Filler Data Enable (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder FillerData(bool flag)
            => this.SetOption("-filler_data", flag.ToFFmpegFlag());

        /// <summary>
        /// Maximum Access Unit Size for rate control (in bits) (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Hevc_amf_CodecEncoder MaxAUSize(int size)
            => this.SetOptionRange("-max_au_size", size, 0, INT_MAX);

        /// <summary>
        /// min quantization parameter for I-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder MinQPI(int qp)
            => this.SetOptionRange("-min_qp_i", qp, -1, 51);

        /// <summary>
        /// max quantization parameter for I-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder MaxQPI(int qp)
            => this.SetOptionRange("-max_qp_i", qp, -1, 51);

        /// <summary>
        /// min quantization parameter for P-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder MinQPP(int qp)
            => this.SetOptionRange("-min_qp_p", qp, -1, 51);

        /// <summary>
        /// max quantization parameter for P-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder MaxQPP(int qp)
            => this.SetOptionRange("-max_qp_p", qp, -1, 51);

        /// <summary>
        /// quantization parameter for P-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder QPP(int qp)
            => this.SetOptionRange("-qp_p", qp, -1, 51);

        /// <summary>
        /// quantization parameter for I-frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder QPI(int qp)
            => this.SetOptionRange("-qp_i", qp, -1, 51);

        /// <summary>
        /// Rate Control Based Frame Skip (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder SkipFrame(bool flag)
            => this.SetOption("-skip_frame", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable ME Half Pixel (default true)
        /// </summary>
        public Hevc_amf_CodecEncoder MEHalfPel(bool flag)
            => this.SetOption("-me_half_pel", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable ME Quarter Pixel (default true)
        /// </summary>
        public Hevc_amf_CodecEncoder MEQuarterPel(bool flag)
            => this.SetOption("-me_quarter_pel", flag.ToFFmpegFlag());

        /// <summary>
        /// Inserts AU Delimiter NAL unit (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder AUD(bool flag)
            => this.SetOption("-aud", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable AMF logging to debug output (default false)
        /// </summary>
        public Hevc_amf_CodecEncoder LogToDbg(bool flag)
            => this.SetOption("-log_to_dbg", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable preanalysis (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder Preanalysis(bool flag)
            => this.SetOption("-preanalysis", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the type of activity analysis (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAActivityType(Hevc_amf_PaActivityType type)
            => this.SetOptionRange("-pa_activity_type", (int)type, -1, 1);

        /// <summary>
        /// Enable scene change detection (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder PASceneChangeDetectionEnable(bool flag)
            => this.SetOption("-pa_scene_change_detection_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the sensitivity of scene change detection (from -1 to 2) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PASceneChangeDetectionSensitivity(Hevc_amf_Sensitivity sensitivity)
            => this.SetOptionRange("-pa_scene_change_detection_sensitivity", (int)sensitivity, -1, 2);

        /// <summary>
        /// Enable static scene detection (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder PAStaticSceneDetectionEnable(bool flag)
            => this.SetOption("-pa_static_scene_detection_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the sensitivity of static scene detection (from -1 to 2) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAStaticSceneDetectionSensitivity(Hevc_amf_Sensitivity sensitivity)
            => this.SetOptionRange("-pa_static_scene_detection_sensitivity", (int)sensitivity, -1, 2);

        /// <summary>
        /// The QP value that is used immediately after a scene change (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAInitialQPAfterSceneChange(int qp)
            => this.SetOptionRange("-pa_initial_qp_after_scene_change", qp, -1, 51);

        /// <summary>
        /// The QP threshold to allow a skip frame (from -1 to 51) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAMaxQPBeforeForceSkip(int qp)
            => this.SetOptionRange("-pa_max_qp_before_force_skip", qp, -1, 51);

        /// <summary>
        /// Content Adaptive Quantization strength (from -1 to 2) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PACaqStrength(Hevc_amf_Sensitivity strength)
            => this.SetOptionRange("-pa_caq_strength", (int)strength, -1, 2);

        /// <summary>
        /// Enable Frame SAD algorithm (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder PAFrameSADEnable(bool flag)
            => this.SetOption("-pa_frame_sad_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable long term reference frame management (default auto)
        /// </summary>
        public Hevc_amf_CodecEncoder PALTREnable(bool flag)
            => this.SetOption("-pa_ltr_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Sets the PA lookahead buffer size (from -1 to 41) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PALookaheadBufferDepth(int depth)
            => this.SetOptionRange("-pa_lookahead_buffer_depth", depth, -1, 41);

        /// <summary>
        /// Sets the perceptual adaptive quantization mode (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAPaqMode(Hevc_amf_PaqMode mode)
            => this.SetOptionRange("-pa_paq_mode", (int)mode, -1, 1);

        /// <summary>
        /// Sets the temporal adaptive quantization mode (from -1 to 2) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PATaqMode(Hevc_amf_TaqMode mode)
            => this.SetOptionRange("-pa_taq_mode", (int)mode, -1, 2);

        /// <summary>
        /// Sets the PA high motion quality boost mode (from -1 to 1) (default -1)
        /// </summary>
        public Hevc_amf_CodecEncoder PAHighMotionQualityBoostMode(Hevc_amf_HighMotionQualityBoostMode mode)
            => this.SetOptionRange("-pa_high_motion_quality_boost_mode", (int)mode, -1, 1);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Hevc_amf_Usage
    {
        /// <summary>Generic Transcoding</summary>
        transcoding = 0,
        /// <summary>ultra low latency trancoding</summary>
        ultralowlatency = 1,
        /// <summary>low latency trancoding</summary>
        lowlatency = 2,
        /// <summary>Webcam</summary>
        webcam = 3,
        /// <summary>high quality trancoding</summary>
        high_quality = 4,
        /// <summary>low latency yet high quality trancoding</summary>
        lowlatency_high_quality = 5,
    }
    public enum Hevc_amf_Tier
    {
        main = 0,
        high = 1,
    }
    public enum Hevc_amf_Level
    {
        auto = 0,
        level_1_0 = 30,
        level_2_0 = 60,
        level_2_1 = 63,
        level_3_0 = 90,
        level_3_1 = 93,
        level_4_0 = 120,
        level_4_1 = 123,
        level_5_0 = 150,
        level_5_1 = 153,
        level_5_2 = 156,
        level_6_0 = 180,
        level_6_1 = 183,
        level_6_2 = 186,
    }
    public enum Hevc_amf_Quality
    {
        /// <summary>Prefer Quality</summary>
        quality = 0,
        /// <summary>Balanced</summary>
        balanced = 5,
        /// <summary>Prefer Speed</summary>
        speed = 10,
    }
    public enum Hevc_amf_RateControl
    {
        /// <summary>Constant Quantization Parameter</summary>
        cqp = 0,
        /// <summary>Latency Constrained Variable Bitrate</summary>
        vbr_latency = 1,
        /// <summary>Peak Contrained Variable Bitrate</summary>
        vbr_peak = 2,
        /// <summary>Constant Bitrate</summary>
        cbr = 3,
        /// <summary>Quality Variable Bitrate</summary>
        qvbr = 4,
        /// <summary>High Quality Variable Bitrate</summary>
        hqvbr = 5,
        /// <summary>High Quality Constant Bitrate</summary>
        hqcbr = 6,
    }
    public enum Hevc_amf_HeaderInsertionMode
    {
        none = 0,
        gop = 1,
        idr = 2,
    }
    public enum Hevc_amf_PaActivityType
    {
        /// <summary>activity y</summary>
        y = 0,
        /// <summary>activity yuv</summary>
        yuv = 1,
    }
    public enum Hevc_amf_Sensitivity
    {
        low = 0,
        medium = 1,
        high = 2,
    }
    public enum Hevc_amf_PaqMode
    {
        /// <summary>no perceptual adaptive quantization</summary>
        none = 0,
        /// <summary>caq perceptual adaptive quantization</summary>
        caq = 1,
    }
    public enum Hevc_amf_TaqMode
    {
        /// <summary>no temporal adaptive quantization</summary>
        none = 0,
        /// <summary>temporal adaptive quantization mode 1</summary>
        mode_1 = 1,
        /// <summary>temporal adaptive quantization mode 2</summary>
        mode_2 = 2,
    }
    public enum Hevc_amf_HighMotionQualityBoostMode
    {
        /// <summary>no high motion quality boost</summary>
        none = 0,
        /// <summary>auto high motion quality boost</summary>
        auto = 1,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// AMD AMF H.265/HEVC encoder.
        /// </summary>
        public static Hevc_amf_CodecEncoder Hevc_amf_Codec(this ImageOutputAVStream stream)
            => new Hevc_amf_CodecEncoder(stream);
        public static T Hevc_amf_Codec<T>(this T stream, Action<Hevc_amf_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Hevc_amf_Codec());
            return stream;
        }
    }
}
