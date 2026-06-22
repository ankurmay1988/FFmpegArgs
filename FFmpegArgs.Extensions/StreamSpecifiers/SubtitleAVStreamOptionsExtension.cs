using FFmpegArgs.Cores;

namespace FFmpegArgs
{
    /// <summary>
    /// Subtitle options.<br></br>
    /// https://ffmpeg.org/ffmpeg.html#Subtitle-options<br></br>
    /// https://ffmpeg.org/ffmpeg.html#Advanced-Subtitle-options<br></br>
    /// Note: a dedicated subtitle stream/map type (for fully modelled subtitle muxing) is not implemented;
    /// these helpers cover output subtitle-codec selection and input subtitle charset. Burn-in (rendering
    /// subtitles onto video) is handled by the <c>subtitles</c> / <c>ass</c> video filters.
    /// </summary>
    public static class SubtitleAVStreamOptionsExtension
    {
        /// <summary>
        /// -c:s &lt;codec&gt;<br></br>
        /// Select the subtitle encoder for the output (e.g. <c>mov_text</c>, <c>srt</c>, <c>ass</c>,
        /// <c>webvtt</c>, <c>dvdsub</c>). Applies to the output's subtitle streams.
        /// </summary>
        public static T Scodec<T>(this T t, string codec) where T : BaseOutput
            => t.SetOption("-c:s", codec);

        /// <summary>
        /// -c:s copy<br></br>
        /// Copy subtitle streams to the output without re-encoding.
        /// </summary>
        public static T CopySubtitle<T>(this T t) where T : BaseOutput
            => t.SetOption("-c:s", "copy");

        /// <summary>
        /// -sub_charenc &lt;encoding&gt;<br></br>
        /// Set the input subtitle character encoding (only useful for non-UTF-8 text subtitle inputs,
        /// e.g. <c>CP1252</c>, <c>Windows-1256</c>).
        /// </summary>
        public static T SubCharenc<T>(this T t, string charenc) where T : BaseInput
            => t.SetOption("-sub_charenc", charenc);
    }
}
