/*av1_nvenc AVOptions:
  -preset            <int>        E..V....... Set the encoding preset (from 0 to 18) (default p4)
     default         0            E..V.......
     slow            1            E..V....... hq 2 passes
     medium          2            E..V....... hq 1 pass
     fast            3            E..V....... hp 1 pass
     p1              12           E..V....... fastest (lowest quality)
     p2              13           E..V....... faster (lower quality)
     p3              14           E..V....... fast (low quality)
     p4              15           E..V....... medium (default)
     p5              16           E..V....... slow (good quality)
     p6              17           E..V....... slower (better quality)
     p7              18           E..V....... slowest (best quality)
  -tune              <int>        E..V....... Set the encoding tuning info (from 1 to 4) (default hq)
     hq              1            E..V....... High quality
     ll              2            E..V....... Low latency
     ull             3            E..V....... Ultra low latency
     lossless        4            E..V....... Lossless
  -level             <int>        E..V....... Set the encoding level restriction (from 0 to 24) (default auto)
     auto            24           E..V.......
     2               0            E..V.......
     2.0             0            E..V.......
     2.1             1            E..V.......
     2.2             2            E..V.......
     2.3             3            E..V.......
     3               4            E..V.......
     3.0             4            E..V.......
     3.1             5            E..V.......
     3.2             6            E..V.......
     3.3             7            E..V.......
     4               8            E..V.......
     4.0             8            E..V.......
     4.1             9            E..V.......
     4.2             10           E..V.......
     4.3             11           E..V.......
     5               12           E..V.......
     5.0             12           E..V.......
     5.1             13           E..V.......
     5.2             14           E..V.......
     5.3             15           E..V.......
     6               16           E..V.......
     6.0             16           E..V.......
     6.1             17           E..V.......
     6.2             18           E..V.......
     6.3             19           E..V.......
     7               20           E..V.......
     7.0             20           E..V.......
     7.1             21           E..V.......
     7.2             22           E..V.......
     7.3             23           E..V.......
  -tier              <int>        E..V....... Set the encoding tier (from 0 to 1) (default 0)
  -rc                <int>        E..V....... Override the preset rate-control (from -1 to INT_MAX) (default -1)
     constqp         0            E..V....... Constant QP mode
     vbr             1            E..V....... Variable bitrate mode
     cbr             2            E..V....... Constant bitrate mode
  -multipass         <int>        E..V....... Set the multipass encoding (from 0 to 2) (default disabled)
     disabled        0            E..V....... Single Pass
     qres            1            E..V....... Two Pass encoding is enabled where first Pass is quarter resolution
     fullres         2            E..V....... Two Pass encoding is enabled where first Pass is full resolution
  -highbitdepth      <boolean>    E..V....... Enable 10 bit encode for 8 bit input (default false)
  -tile-rows         <int>        E..V....... Number of tile rows to encode with (from -1 to 64) (default -1)
  -tile-columns      <int>        E..V....... Number of tile columns to encode with (from -1 to 64) (default -1)
  -surfaces          <int>        E..V....... Number of concurrent surfaces (from 0 to 64) (default 0)
  -gpu               <int>        E..V....... Selects which NVENC capable GPU to use. First GPU is 0, second is 1, and so on. (from -2 to INT_MAX) (default any)
  -rgb_mode          <int>        E..V....... Configure how nvenc handles packed RGB input.
  -delay             <int>        E..V....... Delay frame output by the given amount of frames (from 0 to INT_MAX) (default INT_MAX)
  -rc-lookahead      <int>        E..V....... Number of frames to look ahead for rate-control (from 0 to INT_MAX) (default 0)
  -cq                <float>      E..V....... Set target quality level (0 to 51, 0 means automatic) for constant quality mode in VBR rate control (from 0 to 51) (default 0)
  -init_qpP          <int>        E..V....... Initial QP value for P frame (from -1 to 255) (default -1)
  -init_qpB          <int>        E..V....... Initial QP value for B frame (from -1 to 255) (default -1)
  -init_qpI          <int>        E..V....... Initial QP value for I frame (from -1 to 255) (default -1)
  -qp                <int>        E..V....... Constant quantization parameter rate control method (from -1 to 255) (default -1)
  -qp_cb_offset      <int>        E..V....... Quantization parameter offset for cb channel (from -12 to 12) (default 0)
  -qp_cr_offset      <int>        E..V....... Quantization parameter offset for cr channel (from -12 to 12) (default 0)
  -no-scenecut       <boolean>    E..V....... When lookahead is enabled, set this to 1 to disable adaptive I-frame insertion at scene cuts (default false)
  -forced-idr        <boolean>    E..V....... If forcing keyframes, force them as IDR frames. (default false)
  -b_adapt           <boolean>    E..V....... When lookahead is enabled, set this to 0 to disable adaptive B-frame decision (default true)
  -spatial-aq        <boolean>    E..V....... set to 1 to enable Spatial AQ (default false)
  -temporal-aq       <boolean>    E..V....... set to 1 to enable Temporal AQ (default false)
  -zerolatency       <boolean>    E..V....... Set 1 to indicate zero latency operation (no reordering delay) (default false)
  -nonref_p          <boolean>    E..V....... Set this to 1 to enable automatic insertion of non-reference P-frames (default false)
  -strict_gop        <boolean>    E..V....... Set 1 to minimize GOP-to-GOP rate fluctuations (default false)
  -aq-strength       <int>        E..V....... When Spatial AQ is enabled, this field is used to specify AQ strength. AQ strength scale is from 1 (low) - 15 (aggressive) (from 1 to 15) (default 8)
  -weighted_pred     <boolean>    E..V....... Enable weighted prediction (default false)
  -b_ref_mode        <int>        E..V....... Use B frames as references (from -1 to 2) (default -1)
  -dpb_size          <int>        E..V....... Specifies the DPB size used for encoding (0 means automatic) (from 0 to INT_MAX) (default 0)
  -ldkfs             <int>        E..V....... Low delay key frame scale (from 0 to 255) (default 0)
  -intra-refresh     <boolean>    E..V....... Use Periodic Intra Refresh instead of IDR frames (default false)
  -timing-info       <boolean>    E..V....... Include timing info in sequence/frame headers (default false)
  -extra_sei         <boolean>    E..V....... Pass on extra SEI data (e.g. a53 cc) to be included in the bitstream (default true)
  -a53cc             <boolean>    E..V....... Use A53 Closed Captions (if available) (default true)
  -s12m_tc           <boolean>    E..V....... Use timecode (if available) (default true)
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// NVIDIA NVENC AV1 encoder.
    /// </summary>
    public class Av1_nvenc_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Av1_nvenc_CodecEncoder(ImageOutputAVStream stream) : base("av1_nvenc", stream)
        {
        }

        /// <summary>
        /// Set the encoding preset (from 0 to 18) (default p4)
        /// </summary>
        public Av1_nvenc_CodecEncoder Preset(Av1_nvenc_Preset preset)
            => this.SetOptionRange("-preset", (int)preset, 0, 18);

        /// <summary>
        /// Set the encoding tuning info (from 1 to 4) (default hq)
        /// </summary>
        public Av1_nvenc_CodecEncoder Tune(Av1_nvenc_Tune tune)
            => this.SetOptionRange("-tune", (int)tune, 1, 4);

        /// <summary>
        /// Set the encoding level restriction (from 0 to 24) (default auto)
        /// </summary>
        public Av1_nvenc_CodecEncoder Level(Av1_nvenc_Level level)
            => this.SetOptionRange("-level", (int)level, 0, 24);

        /// <summary>
        /// Set the encoding tier (from 0 to 1) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder Tier(int tier)
            => this.SetOptionRange("-tier", tier, 0, 1);

        /// <summary>
        /// Override the preset rate-control (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder RateControl(Av1_nvenc_RateControl rateControl)
            => this.SetOptionRange("-rc", (int)rateControl, -1, INT_MAX);

        /// <summary>
        /// Set the multipass encoding (from 0 to 2) (default disabled)
        /// </summary>
        public Av1_nvenc_CodecEncoder MultiPass(Av1_nvenc_MultiPass multiPass)
            => this.SetOptionRange("-multipass", (int)multiPass, 0, 2);

        /// <summary>
        /// Enable 10 bit encode for 8 bit input (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder HighBitDepth(bool flag)
            => this.SetOption("-highbitdepth", flag.ToFFmpegFlag());

        /// <summary>
        /// Number of tile rows to encode with (from -1 to 64) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder TileRows(int rows)
            => this.SetOptionRange("-tile-rows", rows, -1, 64);

        /// <summary>
        /// Number of tile columns to encode with (from -1 to 64) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder TileColumns(int cols)
            => this.SetOptionRange("-tile-columns", cols, -1, 64);

        /// <summary>
        /// Number of concurrent surfaces (from 0 to 64) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder Surfaces(int surfaces)
            => this.SetOptionRange("-surfaces", surfaces, 0, 64);

        /// <summary>
        /// Selects which NVENC capable GPU to use. First GPU is 0, second is 1, and so on. (from 0 to INT_MAX) (default any)
        /// </summary>
        public Av1_nvenc_CodecEncoder Gpu(int index)
            => this.SetOptionRange("-gpu", index, 0, INT_MAX);

        /// <summary>
        /// Configure how nvenc handles packed RGB input. (from 0 to INT_MAX) (default yuv420)
        /// </summary>
        public Av1_nvenc_CodecEncoder RgbMode(Av1_nvenc_RgbMode mode)
            => this.SetOptionRange("-rgb_mode", (int)mode, 0, INT_MAX);

        /// <summary>
        /// Delay frame output by the given amount of frames (from 0 to INT_MAX) (default INT_MAX)
        /// </summary>
        public Av1_nvenc_CodecEncoder Delay(int delay)
            => this.SetOptionRange("-delay", delay, 0, INT_MAX);

        /// <summary>
        /// Number of frames to look ahead for rate-control (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder RateControlLookahead(int rc_lookahead)
            => this.SetOptionRange("-rc-lookahead", rc_lookahead, 0, INT_MAX);

        /// <summary>
        /// Set target quality level (0 to 51, 0 means automatic) for constant quality mode in VBR rate control (from 0 to 51) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder CQ(float cq)
            => this.SetOptionRange("-cq", cq, 0, 51);

        /// <summary>
        /// Initial QP value for P frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder InitQP_P(int init_qpP)
            => this.SetOptionRange("-init_qpP", init_qpP, -1, 255);

        /// <summary>
        /// Initial QP value for B frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder InitQP_B(int init_qpB)
            => this.SetOptionRange("-init_qpB", init_qpB, -1, 255);

        /// <summary>
        /// Initial QP value for I frame (from -1 to 255) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder InitQP_I(int init_qpI)
            => this.SetOptionRange("-init_qpI", init_qpI, -1, 255);

        /// <summary>
        /// Constant quantization parameter rate control method (from -1 to 255) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder QP(int qp)
            => this.SetOptionRange("-qp", qp, -1, 255);

        /// <summary>
        /// Quantization parameter offset for cb channel (from -12 to 12) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder QP_Cb_Offset(int offset)
            => this.SetOptionRange("-qp_cb_offset", offset, -12, 12);

        /// <summary>
        /// Quantization parameter offset for cr channel (from -12 to 12) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder QP_Cr_Offset(int offset)
            => this.SetOptionRange("-qp_cr_offset", offset, -12, 12);

        /// <summary>
        /// When lookahead is enabled, set this to 1 to disable adaptive I-frame insertion at scene cuts (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder NoScenecut(bool noScenecut)
            => this.SetOption("-no-scenecut", noScenecut.ToFFmpegFlag());

        /// <summary>
        /// If forcing keyframes, force them as IDR frames. (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder ForcedIdr(bool forcedIdr)
            => this.SetOption("-forced-idr", forcedIdr.ToFFmpegFlag());

        /// <summary>
        /// When lookahead is enabled, set this to 0 to disable adaptive B-frame decision (default true)
        /// </summary>
        public Av1_nvenc_CodecEncoder BAdapt(bool bAdapt)
            => this.SetOption("-b_adapt", bAdapt.ToFFmpegFlag());

        /// <summary>
        /// set to 1 to enable Spatial AQ (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder SpatialAQ(bool spatialAQ)
            => this.SetOption("-spatial-aq", spatialAQ.ToFFmpegFlag());

        /// <summary>
        /// set to 1 to enable Temporal AQ (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder TemporalAQ(bool temporalAQ)
            => this.SetOption("-temporal-aq", temporalAQ.ToFFmpegFlag());

        /// <summary>
        /// Set 1 to indicate zero latency operation (no reordering delay) (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder ZeroLatency(bool zeroLatency)
            => this.SetOption("-zerolatency", zeroLatency.ToFFmpegFlag());

        /// <summary>
        /// Set this to 1 to enable automatic insertion of non-reference P-frames (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder NonRefP(bool nonRefP)
            => this.SetOption("-nonref_p", nonRefP.ToFFmpegFlag());

        /// <summary>
        /// Set 1 to minimize GOP-to-GOP rate fluctuations (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder StrictGop(bool strictGop)
            => this.SetOption("-strict_gop", strictGop.ToFFmpegFlag());

        /// <summary>
        /// When Spatial AQ is enabled, this field is used to specify AQ strength. AQ strength scale is from 1 (low) - 15 (aggressive) (from 1 to 15) (default 8)
        /// </summary>
        public Av1_nvenc_CodecEncoder AQStrength(int aqStrength)
            => this.SetOptionRange("-aq-strength", aqStrength, 1, 15);

        /// <summary>
        /// Enable weighted prediction (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder WeightedPred(bool flag)
            => this.SetOption("-weighted_pred", flag.ToFFmpegFlag());

        /// <summary>
        /// Use B frames as references (from -1 to 2) (default -1)
        /// </summary>
        public Av1_nvenc_CodecEncoder BRefMode(Av1_nvenc_BRefMode b_ref_mode)
            => this.SetOptionRange("-b_ref_mode", (int)b_ref_mode, -1, 2);

        /// <summary>
        /// Specifies the DPB size used for encoding (0 means automatic) (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder DpbSize(int dpb_size)
            => this.SetOptionRange("-dpb_size", dpb_size, 0, INT_MAX);

        /// <summary>
        /// Low delay key frame scale (from 0 to 255) (default 0)
        /// </summary>
        public Av1_nvenc_CodecEncoder Ldkfs(int ldkfs)
            => this.SetOptionRange("-ldkfs", ldkfs, 0, 255);

        /// <summary>
        /// Use Periodic Intra Refresh instead of IDR frames (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder IntraRefresh(bool intraRefresh)
            => this.SetOption("-intra-refresh", intraRefresh.ToFFmpegFlag());

        /// <summary>
        /// Include timing info in sequence/frame headers (default false)
        /// </summary>
        public Av1_nvenc_CodecEncoder TimingInfo(bool flag)
            => this.SetOption("-timing-info", flag.ToFFmpegFlag());

        /// <summary>
        /// Pass on extra SEI data (e.g. a53 cc) to be included in the bitstream (default true)
        /// </summary>
        public Av1_nvenc_CodecEncoder ExtraSEI(bool extraSEI)
            => this.SetOption("-extra_sei", extraSEI.ToFFmpegFlag());

        /// <summary>
        /// Use A53 Closed Captions (if available) (default true)
        /// </summary>
        public Av1_nvenc_CodecEncoder A53CC(bool a53cc)
            => this.SetOption("-a53cc", a53cc.ToFFmpegFlag());

        /// <summary>
        /// Use timecode (if available) (default true)
        /// </summary>
        public Av1_nvenc_CodecEncoder S12mTc(bool s12m_tc)
            => this.SetOption("-s12m_tc", s12m_tc.ToFFmpegFlag());
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Av1_nvenc_Preset
    {
        @default = 0,
        /// <summary>hq 2 passes</summary>
        slow = 1,
        /// <summary>hq 1 pass</summary>
        medium = 2,
        /// <summary>hp 1 pass</summary>
        fast = 3,
        /// <summary>fastest (lowest quality)</summary>
        p1 = 12,
        /// <summary>faster (lower quality)</summary>
        p2 = 13,
        /// <summary>fast (low quality)</summary>
        p3 = 14,
        /// <summary>medium (default)</summary>
        p4 = 15,
        /// <summary>slow (good quality)</summary>
        p5 = 16,
        /// <summary>slower (better quality)</summary>
        p6 = 17,
        /// <summary>slowest (best quality)</summary>
        p7 = 18,
    }
    public enum Av1_nvenc_Tune
    {
        /// <summary>High quality</summary>
        hq = 1,
        /// <summary>Low latency</summary>
        ll = 2,
        /// <summary>Ultra low latency</summary>
        ull = 3,
        /// <summary>Lossless</summary>
        lossless = 4,
    }
    public enum Av1_nvenc_Level
    {
        /// <summary>auto</summary>
        auto = 24,
        Level_2 = 0,
        Level_2_0 = 0,
        Level_2_1 = 1,
        Level_2_2 = 2,
        Level_2_3 = 3,
        Level_3 = 4,
        Level_3_0 = 4,
        Level_3_1 = 5,
        Level_3_2 = 6,
        Level_3_3 = 7,
        Level_4 = 8,
        Level_4_0 = 8,
        Level_4_1 = 9,
        Level_4_2 = 10,
        Level_4_3 = 11,
        Level_5 = 12,
        Level_5_0 = 12,
        Level_5_1 = 13,
        Level_5_2 = 14,
        Level_5_3 = 15,
        Level_6 = 16,
        Level_6_0 = 16,
        Level_6_1 = 17,
        Level_6_2 = 18,
        Level_6_3 = 19,
        Level_7 = 20,
        Level_7_0 = 20,
        Level_7_1 = 21,
        Level_7_2 = 22,
        Level_7_3 = 23,
    }
    public enum Av1_nvenc_RateControl
    {
        /// <summary>Constant QP mode</summary>
        constqp = 0,
        /// <summary>Variable bitrate mode</summary>
        vbr = 1,
        /// <summary>Constant bitrate mode</summary>
        cbr = 2,
    }
    public enum Av1_nvenc_MultiPass
    {
        /// <summary>Single Pass</summary>
        disabled = 0,
        /// <summary>Two Pass encoding is enabled where first Pass is quarter resolution</summary>
        qres = 1,
        /// <summary>Two Pass encoding is enabled where first Pass is full resolution</summary>
        fullres = 2,
    }
    public enum Av1_nvenc_RgbMode
    {
        /// <summary>Disables support, throws an error.</summary>
        disabled = 0,
        /// <summary>Convert to yuv420</summary>
        yuv420 = 1,
        /// <summary>Convert to yuv444</summary>
        yuv444 = 2,
    }
    public enum Av1_nvenc_BRefMode
    {
        /// <summary>B frames will not be used for reference</summary>
        disabled = 0,
        /// <summary>Each B frame will be used for reference</summary>
        each = 1,
        /// <summary>Only (number of B frames)/2 will be used for reference</summary>
        middle = 2,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// NVIDIA NVENC AV1 encoder.
        /// </summary>
        public static Av1_nvenc_CodecEncoder Av1_nvenc_Codec(this ImageOutputAVStream stream)
            => new Av1_nvenc_CodecEncoder(stream);
        public static T Av1_nvenc_Codec<T>(this T stream, Action<Av1_nvenc_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Av1_nvenc_Codec());
            return stream;
        }
    }
}
