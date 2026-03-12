/*av1_amf AVOptions:
  -usage             <int>        E..V....... Set the encoding usage (from 0 to 1) (default transcoding)
     transcoding     0            E..V.......
     lowlatency      1            E..V.......
  -profile           <int>        E..V....... Set the profile (default main) (from 1 to 1) (default main)
     main            1            E..V.......
  -level             <int>        E..V....... Set the encoding level (default auto) (from 0 to 23) (default auto)
     auto            0            E..V.......
     2.0             0            E..V.......
     2.1             1            E..V.......
     2.2             2            E..V.......
     2.3             3            E..V.......
     3.0             4            E..V.......
     3.1             5            E..V.......
     3.2             6            E..V.......
     3.3             7            E..V.......
     4.0             8            E..V.......
     4.1             9            E..V.......
     4.2             10           E..V.......
     4.3             11           E..V.......
     5.0             12           E..V.......
     5.1             13           E..V.......
     5.2             14           E..V.......
     5.3             15           E..V.......
     6.0             16           E..V.......
     6.1             17           E..V.......
     6.2             18           E..V.......
     6.3             19           E..V.......
     7.0             20           E..V.......
     7.1             21           E..V.......
     7.2             22           E..V.......
     7.3             23           E..V.......
  -quality           <int>        E..V....... Set the encoding quality (from 0 to 100) (default speed)
     balanced        70           E..V.......
     speed           100          E..V.......
     quality         30           E..V.......
     high_quality    0            E..V.......
  -rc                <int>        E..V....... Set the rate control mode (from -1 to 6) (default -1)
     cqp             0            E..V....... Constant Quantization Parameter
     vbr_latency     1            E..V....... Latency Constrained Variable Bitrate
     vbr_peak        2            E..V....... Peak Constrained Variable Bitrate
     cbr             3            E..V....... Constant Bitrate
     qvbr            4            E..V....... Quality Variable Bitrate
     hqvbr           5            E..V....... High Quality Variable Bitrate
     hqcbr           6            E..V....... High Quality Constant Bitrate
  -qvbr_quality_level <int>       E..V....... Sets the QVBR quality level (from -1 to 51) (default -1)
  -header_insertion_mode <int>    E..V....... Set header insertion mode (from -1 to 2) (default -1)
     none            0            E..V.......
     gop             1            E..V.......
     frame           2            E..V.......
  -preencode         <boolean>    E..V....... Enable preencode (default false)
  -enforce_hrd       <boolean>    E..V....... Enforce HRD (default false)
  -filler_data       <boolean>    E..V....... Filler Data Enable (default false)
  -high_motion_quality_boost_enable <boolean> E..V....... Enable High motion quality boost mode (default auto)
  -min_qp_i          <int>        E..V....... min quantization parameter for I-frame (from -1 to 255) (default -1)
  -max_qp_i          <int>        E..V....... max quantization parameter for I-frame (from -1 to 255) (default -1)
  -min_qp_p          <int>        E..V....... min quantization parameter for P-frame (from -1 to 255) (default -1)
  -max_qp_p          <int>        E..V....... max quantization parameter for P-frame (from -1 to 255) (default -1)
  -qp_p              <int>        E..V....... quantization parameter for P-frame (from -1 to 255) (default -1)
  -qp_i              <int>        E..V....... quantization parameter for I-frame (from -1 to 255) (default -1)
  -skip_frame        <boolean>    E..V....... Rate Control Based Frame Skip (default false)
  -align             <int>        E..V....... alignment mode (from 1 to 3) (default none)
     64x16           1            E..V.......
     1080p           2            E..V.......
     none            3            E..V.......
  -log_to_dbg        <boolean>    E..V....... Enable AMF logging to debug output (default false)
  -preanalysis       <boolean>    E..V....... Enable preanalysis (default auto)
  -pa_activity_type  <int>        E..V....... Set the type of activity analysis (from -1 to 1) (default -1)
     y               0            E..V....... activity y
     yuv             1            E..V....... activity yuv
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
    /// AMD AMF AV1 encoder (av1_amf).
    /// </summary>
    public class Av1_amf_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Av1_amf_CodecEncoder(ImageOutputAVStream stream) : base("av1_amf", stream)
        {
        }

        /// <summary>
        /// Set the encoding usage (from 0 to 1) (default transcoding)
        /// </summary>
        public Av1_amf_CodecEncoder Usage(Av1_amf_Usage usage)
            => this.SetOptionRange("-usage", (int)usage, 0, 1);

        /// <summary>
        /// Set the encoding level (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder Level(Av1_amf_Level level)
            => this.SetOptionRange("-level", (int)level, 0, 23);

        /// <summary>
        /// Set the encoding quality (from 0 to 100) (default speed)
        /// </summary>
        public Av1_amf_CodecEncoder Quality(Av1_amf_Quality quality)
            => this.SetOptionRange("-quality", (int)quality, 0, 100);

        /// <summary>
        /// Set the rate control mode (from -1 to 6) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder RC(Av1_amf_RateControl rc)
            => this.SetOptionRange("-rc", (int)rc, -1, 6);

        /// <summary>
        /// Sets the QVBR quality level (from -1 to 51) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder QvbrQualityLevel(int level)
            => this.SetOptionRange("-qvbr_quality_level", level, -1, 51);

        /// <summary>
        /// Set header insertion mode (from -1 to 2) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder HeaderInsertionMode(Av1_amf_HeaderInsertionMode mode)
            => this.SetOptionRange("-header_insertion_mode", (int)mode, -1, 2);

        /// <summary>
        /// Enable preencode (default false)
        /// </summary>
        public Av1_amf_CodecEncoder Preencode(bool flag)
            => this.SetOption("-preencode", flag.ToFFmpegFlag());

        /// <summary>
        /// Enforce HRD (default false)
        /// </summary>
        public Av1_amf_CodecEncoder EnforceHRD(bool flag)
            => this.SetOption("-enforce_hrd", flag.ToFFmpegFlag());

        /// <summary>
        /// Filler Data Enable (default false)
        /// </summary>
        public Av1_amf_CodecEncoder FillerData(bool flag)
            => this.SetOption("-filler_data", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable High motion quality boost mode (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder HighMotionQualityBoostEnable(bool flag)
            => this.SetOption("-high_motion_quality_boost_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Min quantization parameter for I-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder MinQpI(int qp)
            => this.SetOptionRange("-min_qp_i", qp, -1, 255);

        /// <summary>
        /// Max quantization parameter for I-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder MaxQpI(int qp)
            => this.SetOptionRange("-max_qp_i", qp, -1, 255);

        /// <summary>
        /// Min quantization parameter for P-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder MinQpP(int qp)
            => this.SetOptionRange("-min_qp_p", qp, -1, 255);

        /// <summary>
        /// Max quantization parameter for P-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder MaxQpP(int qp)
            => this.SetOptionRange("-max_qp_p", qp, -1, 255);

        /// <summary>
        /// Quantization parameter for P-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder QpP(int qp)
            => this.SetOptionRange("-qp_p", qp, -1, 255);

        /// <summary>
        /// Quantization parameter for I-frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder QpI(int qp)
            => this.SetOptionRange("-qp_i", qp, -1, 255);

        /// <summary>
        /// Rate Control Based Frame Skip (default false)
        /// </summary>
        public Av1_amf_CodecEncoder SkipFrame(bool flag)
            => this.SetOption("-skip_frame", flag.ToFFmpegFlag());

        /// <summary>
        /// Alignment mode (from 1 to 3) (default none)
        /// </summary>
        public Av1_amf_CodecEncoder Align(Av1_amf_Align align)
            => this.SetOptionRange("-align", (int)align, 1, 3);

        /// <summary>
        /// Enable AMF logging to debug output (default false)
        /// </summary>
        public Av1_amf_CodecEncoder LogToDbg(bool flag)
            => this.SetOption("-log_to_dbg", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable preanalysis (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder Preanalysis(bool flag)
            => this.SetOption("-preanalysis", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the type of activity analysis (from -1 to 1) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaActivityType(Av1_amf_PaActivityType type)
            => this.SetOptionRange("-pa_activity_type", (int)type, -1, 1);

        /// <summary>
        /// Enable scene change detection (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder PaSceneChangeDetectionEnable(bool flag)
            => this.SetOption("-pa_scene_change_detection_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the sensitivity of scene change detection (from -1 to 2) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaSceneChangeDetectionSensitivity(Av1_amf_Sensitivity sensitivity)
            => this.SetOptionRange("-pa_scene_change_detection_sensitivity", (int)sensitivity, -1, 2);

        /// <summary>
        /// Enable static scene detection (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder PaStaticSceneDetectionEnable(bool flag)
            => this.SetOption("-pa_static_scene_detection_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Set the sensitivity of static scene detection (from -1 to 2) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaStaticSceneDetectionSensitivity(Av1_amf_Sensitivity sensitivity)
            => this.SetOptionRange("-pa_static_scene_detection_sensitivity", (int)sensitivity, -1, 2);

        /// <summary>
        /// The QP value that is used immediately after a scene change (from -1 to 51) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaInitialQpAfterSceneChange(int qp)
            => this.SetOptionRange("-pa_initial_qp_after_scene_change", qp, -1, 51);

        /// <summary>
        /// The QP threshold to allow a skip frame (from -1 to 51) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaMaxQpBeforeForceSkip(int qp)
            => this.SetOptionRange("-pa_max_qp_before_force_skip", qp, -1, 51);

        /// <summary>
        /// Content Adaptive Quantization strength (from -1 to 2) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaCaqStrength(Av1_amf_Sensitivity strength)
            => this.SetOptionRange("-pa_caq_strength", (int)strength, -1, 2);

        /// <summary>
        /// Enable Frame SAD algorithm (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder PaFrameSadEnable(bool flag)
            => this.SetOption("-pa_frame_sad_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable long term reference frame management (default auto)
        /// </summary>
        public Av1_amf_CodecEncoder PaLtrEnable(bool flag)
            => this.SetOption("-pa_ltr_enable", flag.ToFFmpegFlag());

        /// <summary>
        /// Sets the PA lookahead buffer size (from -1 to 41) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaLookaheadBufferDepth(int depth)
            => this.SetOptionRange("-pa_lookahead_buffer_depth", depth, -1, 41);

        /// <summary>
        /// Sets the perceptual adaptive quantization mode (from -1 to 1) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaPaqMode(Av1_amf_PaqMode mode)
            => this.SetOptionRange("-pa_paq_mode", (int)mode, -1, 1);

        /// <summary>
        /// Sets the temporal adaptive quantization mode (from -1 to 2) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaTaqMode(Av1_amf_TaqMode mode)
            => this.SetOptionRange("-pa_taq_mode", (int)mode, -1, 2);

        /// <summary>
        /// Sets the PA high motion quality boost mode (from -1 to 1) (default -1)
        /// </summary>
        public Av1_amf_CodecEncoder PaHighMotionQualityBoostMode(Av1_amf_HighMotionQualityBoostMode mode)
            => this.SetOptionRange("-pa_high_motion_quality_boost_mode", (int)mode, -1, 1);
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Av1_amf_Usage
    {
        transcoding = 0,
        lowlatency = 1,
    }
    public enum Av1_amf_Level
    {
        auto = 0,
        Level_2_0 = 0,
        Level_2_1 = 1,
        Level_2_2 = 2,
        Level_2_3 = 3,
        Level_3_0 = 4,
        Level_3_1 = 5,
        Level_3_2 = 6,
        Level_3_3 = 7,
        Level_4_0 = 8,
        Level_4_1 = 9,
        Level_4_2 = 10,
        Level_4_3 = 11,
        Level_5_0 = 12,
        Level_5_1 = 13,
        Level_5_2 = 14,
        Level_5_3 = 15,
        Level_6_0 = 16,
        Level_6_1 = 17,
        Level_6_2 = 18,
        Level_6_3 = 19,
        Level_7_0 = 20,
        Level_7_1 = 21,
        Level_7_2 = 22,
        Level_7_3 = 23,
    }
    public enum Av1_amf_Quality
    {
        /// <summary>Balanced (70)</summary>
        balanced = 70,
        /// <summary>Speed (100)</summary>
        speed = 100,
        /// <summary>Quality (30)</summary>
        quality = 30,
        /// <summary>High Quality (0)</summary>
        high_quality = 0,
    }
    public enum Av1_amf_RateControl
    {
        /// <summary>Constant Quantization Parameter</summary>
        cqp = 0,
        /// <summary>Latency Constrained Variable Bitrate</summary>
        vbr_latency = 1,
        /// <summary>Peak Constrained Variable Bitrate</summary>
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
    public enum Av1_amf_HeaderInsertionMode
    {
        none = 0,
        gop = 1,
        frame = 2,
    }
    public enum Av1_amf_Align
    {
        /// <summary>64x16 alignment</summary>
        Align_64x16 = 1,
        /// <summary>1080p alignment</summary>
        Align_1080p = 2,
        /// <summary>No alignment</summary>
        none = 3,
    }
    public enum Av1_amf_PaActivityType
    {
        y = 0,
        yuv = 1,
    }
    public enum Av1_amf_Sensitivity
    {
        low = 0,
        medium = 1,
        high = 2,
    }
    public enum Av1_amf_PaqMode
    {
        none = 0,
        caq = 1,
    }
    public enum Av1_amf_TaqMode
    {
        none = 0,
        Mode1 = 1,
        Mode2 = 2,
    }
    public enum Av1_amf_HighMotionQualityBoostMode
    {
        none = 0,
        auto = 1,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// AMD AMF AV1 encoder.
        /// </summary>
        public static Av1_amf_CodecEncoder Av1_amf_Codec(this ImageOutputAVStream stream)
            => new Av1_amf_CodecEncoder(stream);
        public static T Av1_amf_Codec<T>(this T stream, Action<Av1_amf_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Av1_amf_Codec());
            return stream;
        }
    }
}
