/*libaom-av1 encoder AVOptions:
  -cpu-used          <int>        E..V....... Quality/Speed ratio modifier (from 0 to 8) (default 1)
  -auto-alt-ref      <int>        E..V....... Enable use of alternate reference frames (2-pass only) (from -1 to 2) (default -1)
  -lag-in-frames     <int>        E..V....... Number of frames to look ahead at for alternate reference frame selection (from -1 to INT_MAX) (default -1)
  -arnr-max-frames   <int>        E..V....... altref noise reduction max frame count (from -1 to INT_MAX) (default -1)
  -arnr-strength     <int>        E..V....... altref noise reduction filter strength (from -1 to 6) (default -1)
  -aq-mode           <int>        E..V....... adaptive quantization mode (from -1 to 4) (default -1)
     none            0            E..V....... Aq not used
     variance        1            E..V....... Variance based Aq
     complexity      2            E..V....... Complexity based Aq
     cyclic          3            E..V....... Cyclic Refresh Aq
  -error-resilience  <flags>      E..V....... Error resilience configuration (default 0)
     default                      E..V....... Improve resiliency against losses of whole frames
  -crf               <int>        E..V....... Select the quality for constant quality mode (from -1 to 63) (default -1)
  -static-thresh     <int>        E..V....... A change threshold on blocks below which they will be skipped (from 0 to INT_MAX) (default 0)
  -drop-threshold    <int>        E..V....... Frame drop threshold (from INT_MIN to INT_MAX) (default 0)
  -denoise-noise-level <int>      E..V....... Amount of noise to be removed (from -1 to INT_MAX) (default -1)
  -denoise-block-size <int>       E..V....... Denoise block size (from -1 to INT_MAX) (default -1)
  -undershoot-pct    <int>        E..V....... Datarate undershoot (min) target (%) (from -1 to 100) (default -1)
  -overshoot-pct     <int>        E..V....... Datarate overshoot (max) target (%) (from -1 to 1000) (default -1)
  -minsection-pct    <int>        E..V....... GOP min bitrate (% of target) (from -1 to 100) (default -1)
  -maxsection-pct    <int>        E..V....... GOP max bitrate (% of target) (from -1 to 5000) (default -1)
  -frame-parallel    <boolean>    E..V....... Enable frame parallel decodability features (default auto)
  -tiles             <image_size> E..V....... Tile columns x rows
  -tile-columns      <int>        E..V....... Log2 of number of tile columns to use (from -1 to 6) (default -1)
  -tile-rows         <int>        E..V....... Log2 of number of tile rows to use (from -1 to 6) (default -1)
  -row-mt            <boolean>    E..V....... Enable row based multi-threading (default auto)
  -enable-cdef       <boolean>    E..V....... Enable CDEF filtering (default auto)
  -enable-global-motion <boolean> E..V....... Enable global motion (default auto)
  -enable-intrabc    <boolean>    E..V....... Enable intra block copy prediction mode (default auto)
  -enable-restoration <boolean>   E..V....... Enable Loop Restoration filtering (default auto)
  -usage             <int>        E..V....... Quality and compression efficiency vs speed trade-off (from 0 to INT_MAX) (default good)
     good            0            E..V....... Good quality
     realtime        1            E..V....... Realtime encoding
     allintra        2            E..V....... All Intra encoding
  -tune              <int>        E..V....... The metric that the encoder tunes for (from -1 to 1) (default -1)
     psnr            0            E..V.......
     ssim            1            E..V.......
  -still-picture     <boolean>    E..V....... Encode in single frame mode (typically used for still AVIF images) (default false)
  -enable-rect-partitions <boolean>    E..V....... Enable rectangular partitions (default auto)
  -enable-1to4-partitions <boolean>    E..V....... Enable 1:4/4:1 partitions (default auto)
  -enable-ab-partitions <boolean>      E..V....... Enable ab shape partitions (default auto)
  -enable-angle-delta <boolean>        E..V....... Enable angle delta intra prediction (default auto)
  -enable-cfl-intra  <boolean>         E..V....... Enable chroma predicted from luma intra prediction (default auto)
  -enable-filter-intra <boolean>       E..V....... Enable filter intra predictor (default auto)
  -enable-intra-edge-filter <boolean>  E..V....... Enable intra edge filter (default auto)
  -enable-smooth-intra <boolean>       E..V....... Enable smooth intra prediction mode (default auto)
  -enable-paeth-intra <boolean>        E..V....... Enable paeth predictor in intra prediction (default auto)
  -enable-palette    <boolean>         E..V....... Enable palette prediction mode (default auto)
  -enable-flip-idtx  <boolean>         E..V....... Enable extended transform type (default auto)
  -enable-tx64       <boolean>         E..V....... Enable 64-pt transform (default auto)
  -reduced-tx-type-set <boolean>       E..V....... Use reduced set of transform types (default auto)
  -use-intra-dct-only <boolean>        E..V....... Use DCT only for INTRA modes (default auto)
  -use-inter-dct-only <boolean>        E..V....... Use DCT only for INTER modes (default auto)
  -use-intra-default-tx-only <boolean> E..V....... Use default-transform only for INTRA modes (default auto)
  -enable-ref-frame-mvs <boolean>      E..V....... Enable temporal mv prediction (default auto)
  -enable-reduced-reference-set <boolean> E..V....... Use reduced set of single and compound references (default auto)
  -enable-obmc       <boolean>         E..V....... Enable obmc (default auto)
  -enable-dual-filter <boolean>        E..V....... Enable dual filter (default auto)
  -enable-diff-wtd-comp <boolean>      E..V....... Enable difference-weighted compound (default auto)
  -enable-dist-wtd-comp <boolean>      E..V....... Enable distance-weighted compound (default auto)
  -enable-onesided-comp <boolean>      E..V....... Enable one sided compound (default auto)
  -enable-interinter-wedge <boolean>   E..V....... Enable interinter wedge compound (default auto)
  -enable-interintra-wedge <boolean>   E..V....... Enable interintra wedge compound (default auto)
  -enable-masked-comp <boolean>        E..V....... Enable masked compound (default auto)
  -enable-interintra-comp <boolean>    E..V....... Enable interintra compound (default auto)
  -enable-smooth-interintra <boolean>  E..V....... Enable smooth interintra mode (default auto)
  -aom-params        <dictionary>      E..V....... Set libaom options using a :-separated list of key=value pairs
*/
namespace FFmpegArgs.Codec.Encoders.Images
{
    /// <summary>
    /// libaom AV1 reference encoder (libaom-av1).
    /// </summary>
    public class Libaom_av1_CodecEncoder : BaseImageCodecEncoder
    {
        /// <summary>
        ///
        /// </summary>
        public Libaom_av1_CodecEncoder(ImageOutputAVStream stream) : base("libaom-av1", stream)
        {
        }

        /// <summary>
        /// Quality/Speed ratio modifier (from 0 to 8) (default 1)
        /// </summary>
        public Libaom_av1_CodecEncoder CpuUsed(int value)
            => this.SetOptionRange("-cpu-used", value, 0, 8);

        /// <summary>
        /// Enable use of alternate reference frames (2-pass only) (from -1 to 2) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder AutoAltRef(int value)
            => this.SetOptionRange("-auto-alt-ref", value, -1, 2);

        /// <summary>
        /// Number of frames to look ahead for alternate reference frame selection (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder LagInFrames(int frames)
            => this.SetOptionRange("-lag-in-frames", frames, -1, INT_MAX);

        /// <summary>
        /// Altref noise reduction max frame count (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder ArnrMaxFrames(int frames)
            => this.SetOptionRange("-arnr-max-frames", frames, -1, INT_MAX);

        /// <summary>
        /// Altref noise reduction filter strength (from -1 to 6) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder ArnrStrength(int strength)
            => this.SetOptionRange("-arnr-strength", strength, -1, 6);

        /// <summary>
        /// Adaptive quantization mode (from -1 to 4) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder AqMode(Libaom_av1_AqMode mode)
            => this.SetOptionRange("-aq-mode", (int)mode, -1, 4);

        /// <summary>
        /// Select the quality for constant quality mode (from -1 to 63) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder CRF(int crf)
            => this.SetOptionRange("-crf", crf, -1, 63);

        /// <summary>
        /// A change threshold on blocks below which they will be skipped (from 0 to INT_MAX) (default 0)
        /// </summary>
        public Libaom_av1_CodecEncoder StaticThresh(int threshold)
            => this.SetOptionRange("-static-thresh", threshold, 0, INT_MAX);

        /// <summary>
        /// Frame drop threshold (from INT_MIN to INT_MAX) (default 0)
        /// </summary>
        public Libaom_av1_CodecEncoder DropThreshold(int threshold)
            => this.SetOption("-drop-threshold", threshold);

        /// <summary>
        /// Amount of noise to be removed (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder DenoiseNoiseLevel(int level)
            => this.SetOptionRange("-denoise-noise-level", level, -1, INT_MAX);

        /// <summary>
        /// Denoise block size (from -1 to INT_MAX) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder DenoiseBlockSize(int size)
            => this.SetOptionRange("-denoise-block-size", size, -1, INT_MAX);

        /// <summary>
        /// Datarate undershoot (min) target (%) (from -1 to 100) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder UndershootPct(int pct)
            => this.SetOptionRange("-undershoot-pct", pct, -1, 100);

        /// <summary>
        /// Datarate overshoot (max) target (%) (from -1 to 1000) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder OvershootPct(int pct)
            => this.SetOptionRange("-overshoot-pct", pct, -1, 1000);

        /// <summary>
        /// GOP min bitrate (% of target) (from -1 to 100) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder MinSectionPct(int pct)
            => this.SetOptionRange("-minsection-pct", pct, -1, 100);

        /// <summary>
        /// GOP max bitrate (% of target) (from -1 to 5000) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder MaxSectionPct(int pct)
            => this.SetOptionRange("-maxsection-pct", pct, -1, 5000);

        /// <summary>
        /// Enable frame parallel decodability features (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder FrameParallel(bool flag)
            => this.SetOption("-frame-parallel", flag.ToFFmpegFlag());

        /// <summary>
        /// Tile columns x rows (e.g. "2x2")
        /// </summary>
        public Libaom_av1_CodecEncoder Tiles(string colsXrows)
            => this.SetOption("-tiles", colsXrows);

        /// <summary>
        /// Log2 of number of tile columns to use (from -1 to 6) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder TileColumns(int log2cols)
            => this.SetOptionRange("-tile-columns", log2cols, -1, 6);

        /// <summary>
        /// Log2 of number of tile rows to use (from -1 to 6) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder TileRows(int log2rows)
            => this.SetOptionRange("-tile-rows", log2rows, -1, 6);

        /// <summary>
        /// Enable row based multi-threading (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder RowMT(bool flag)
            => this.SetOption("-row-mt", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable CDEF filtering (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableCdef(bool flag)
            => this.SetOption("-enable-cdef", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable global motion (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableGlobalMotion(bool flag)
            => this.SetOption("-enable-global-motion", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable intra block copy prediction mode (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableIntraBC(bool flag)
            => this.SetOption("-enable-intrabc", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable Loop Restoration filtering (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableRestoration(bool flag)
            => this.SetOption("-enable-restoration", flag.ToFFmpegFlag());

        /// <summary>
        /// Quality and compression efficiency vs speed trade-off (from 0 to INT_MAX) (default good)
        /// </summary>
        public Libaom_av1_CodecEncoder Usage(Libaom_av1_Usage usage)
            => this.SetOptionRange("-usage", (int)usage, 0, INT_MAX);

        /// <summary>
        /// The metric that the encoder tunes for (from -1 to 1) (default -1)
        /// </summary>
        public Libaom_av1_CodecEncoder Tune(Libaom_av1_Tune tune)
            => this.SetOptionRange("-tune", (int)tune, -1, 1);

        /// <summary>
        /// Encode in single frame mode (typically used for still AVIF images) (default false)
        /// </summary>
        public Libaom_av1_CodecEncoder StillPicture(bool flag)
            => this.SetOption("-still-picture", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable rectangular partitions (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableRectPartitions(bool flag)
            => this.SetOption("-enable-rect-partitions", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable 1:4/4:1 partitions (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder Enable1to4Partitions(bool flag)
            => this.SetOption("-enable-1to4-partitions", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable ab shape partitions (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableAbPartitions(bool flag)
            => this.SetOption("-enable-ab-partitions", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable angle delta intra prediction (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableAngleDelta(bool flag)
            => this.SetOption("-enable-angle-delta", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable chroma predicted from luma intra prediction (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableCflIntra(bool flag)
            => this.SetOption("-enable-cfl-intra", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable filter intra predictor (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableFilterIntra(bool flag)
            => this.SetOption("-enable-filter-intra", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable intra edge filter (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableIntraEdgeFilter(bool flag)
            => this.SetOption("-enable-intra-edge-filter", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable smooth intra prediction mode (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableSmoothIntra(bool flag)
            => this.SetOption("-enable-smooth-intra", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable paeth predictor in intra prediction (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnablePaethIntra(bool flag)
            => this.SetOption("-enable-paeth-intra", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable palette prediction mode (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnablePalette(bool flag)
            => this.SetOption("-enable-palette", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable extended transform type (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableFlipIdtx(bool flag)
            => this.SetOption("-enable-flip-idtx", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable 64-pt transform (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableTx64(bool flag)
            => this.SetOption("-enable-tx64", flag.ToFFmpegFlag());

        /// <summary>
        /// Use reduced set of transform types (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder ReducedTxTypeSet(bool flag)
            => this.SetOption("-reduced-tx-type-set", flag.ToFFmpegFlag());

        /// <summary>
        /// Use DCT only for INTRA modes (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder UseIntraDctOnly(bool flag)
            => this.SetOption("-use-intra-dct-only", flag.ToFFmpegFlag());

        /// <summary>
        /// Use DCT only for INTER modes (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder UseInterDctOnly(bool flag)
            => this.SetOption("-use-inter-dct-only", flag.ToFFmpegFlag());

        /// <summary>
        /// Use default-transform only for INTRA modes (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder UseIntraDefaultTxOnly(bool flag)
            => this.SetOption("-use-intra-default-tx-only", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable temporal mv prediction (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableRefFrameMvs(bool flag)
            => this.SetOption("-enable-ref-frame-mvs", flag.ToFFmpegFlag());

        /// <summary>
        /// Use reduced set of single and compound references (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableReducedReferenceSet(bool flag)
            => this.SetOption("-enable-reduced-reference-set", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable obmc (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableObmc(bool flag)
            => this.SetOption("-enable-obmc", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable dual filter (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableDualFilter(bool flag)
            => this.SetOption("-enable-dual-filter", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable difference-weighted compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableDiffWtdComp(bool flag)
            => this.SetOption("-enable-diff-wtd-comp", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable distance-weighted compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableDistWtdComp(bool flag)
            => this.SetOption("-enable-dist-wtd-comp", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable one sided compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableOnesidedComp(bool flag)
            => this.SetOption("-enable-onesided-comp", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable interinter wedge compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableInterinterWedge(bool flag)
            => this.SetOption("-enable-interinter-wedge", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable interintra wedge compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableInterintraWedge(bool flag)
            => this.SetOption("-enable-interintra-wedge", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable masked compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableMaskedComp(bool flag)
            => this.SetOption("-enable-masked-comp", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable interintra compound (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableInterintraComp(bool flag)
            => this.SetOption("-enable-interintra-comp", flag.ToFFmpegFlag());

        /// <summary>
        /// Enable smooth interintra mode (default auto)
        /// </summary>
        public Libaom_av1_CodecEncoder EnableSmoothInterintra(bool flag)
            => this.SetOption("-enable-smooth-interintra", flag.ToFFmpegFlag());

        /// <summary>
        /// Set libaom options using a :-separated list of key=value pairs
        /// </summary>
        public Libaom_av1_CodecEncoder AomParams(IReadOnlyDictionary<string, string> aomParams)
            => this.SetOption("-aom-params", string.Join(":", aomParams.Select(x => $"{x.Key}={x.Value}")));
    }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public enum Libaom_av1_AqMode
    {
        none = 0,
        variance = 1,
        complexity = 2,
        cyclic = 3,
    }
    public enum Libaom_av1_Usage
    {
        good = 0,
        realtime = 1,
        allintra = 2,
    }
    public enum Libaom_av1_Tune
    {
        psnr = 0,
        ssim = 1,
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member

    public static partial class ImageEncoderExtensions
    {
        /// <summary>
        /// libaom AV1 reference encoder.
        /// </summary>
        public static Libaom_av1_CodecEncoder Libaom_av1_Codec(this ImageOutputAVStream stream)
            => new Libaom_av1_CodecEncoder(stream);
        public static T Libaom_av1_Codec<T>(this T stream, Action<Libaom_av1_CodecEncoder> action) where T : ImageOutputAVStream
        {
            action.Invoke(stream.Libaom_av1_Codec());
            return stream;
        }
    }
}
