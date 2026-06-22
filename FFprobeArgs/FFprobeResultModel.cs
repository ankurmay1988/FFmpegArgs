using System.Text.Json.Serialization;

namespace FFprobeArgs
{
    /// <summary>
    /// Minimal model of the ffprobe JSON output (only common fields).
    /// Populated by <see cref="FFprobeRenderResult.TryParseJson"/>.
    /// </summary>
    public class FFprobeProbe
    {
        /// <summary>
        /// The <c>streams</c> array (from <c>-show_streams</c>).
        /// </summary>
        [JsonPropertyName("streams")]
        public List<FFprobeStream> Streams { get; set; } = new List<FFprobeStream>();

        /// <summary>
        /// The <c>format</c> object (from <c>-show_format</c>).
        /// </summary>
        [JsonPropertyName("format")]
        public FFprobeFormat? Format { get; set; }
    }

    /// <summary>
    /// Minimal model of an ffprobe <c>format</c> object.
    /// </summary>
    public class FFprobeFormat
    {
        /// <summary>
        /// filename
        /// </summary>
        [JsonPropertyName("filename")]
        public string? FileName { get; set; }

        /// <summary>
        /// format_name
        /// </summary>
        [JsonPropertyName("format_name")]
        public string? FormatName { get; set; }

        /// <summary>
        /// duration (seconds, as string in ffprobe output)
        /// </summary>
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }

        /// <summary>
        /// bit_rate
        /// </summary>
        [JsonPropertyName("bit_rate")]
        public string? BitRate { get; set; }
    }

    /// <summary>
    /// Minimal model of an ffprobe <c>stream</c> object.
    /// </summary>
    public class FFprobeStream
    {
        /// <summary>
        /// index
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>
        /// codec_type (video, audio, subtitle, ...)
        /// </summary>
        [JsonPropertyName("codec_type")]
        public string? CodecType { get; set; }

        /// <summary>
        /// codec_name
        /// </summary>
        [JsonPropertyName("codec_name")]
        public string? CodecName { get; set; }

        /// <summary>
        /// width
        /// </summary>
        [JsonPropertyName("width")]
        public int? Width { get; set; }

        /// <summary>
        /// height
        /// </summary>
        [JsonPropertyName("height")]
        public int? Height { get; set; }

        /// <summary>
        /// duration (seconds, as string in ffprobe output)
        /// </summary>
        [JsonPropertyName("duration")]
        public string? Duration { get; set; }
    }
}
