using System.Text.Json;

namespace FFprobeArgs
{
    /// <summary>
    /// Result of a <see cref="FFprobeRender"/> execution.
    /// </summary>
    public class FFprobeRenderResult
    {
        internal List<string> _StdOutDatas { get; } = new List<string>();
        internal List<string> _LogDatas { get; } = new List<string>();

        /// <summary>
        ///
        /// </summary>
        public string Arguments { get; internal set; } = string.Empty;
        /// <summary>
        ///
        /// </summary>
        public IReadOnlyList<string> ArgumentList { get; internal set; } = new string[0];
        /// <summary>
        ///
        /// </summary>
        public int ExitCode { get; internal set; } = 0;
        /// <summary>
        /// Captured stdout lines from ffprobe (ffprobe writes the probe data to stdout).
        /// </summary>
        public IReadOnlyList<string> StdOutDatas { get { return _StdOutDatas; } }
        /// <summary>
        /// Captured stderr lines from ffprobe (logs / banner).
        /// </summary>
        public IReadOnlyList<string> LogDatas { get { return _LogDatas; } }

        /// <summary>
        /// The raw stdout output of ffprobe joined by newlines.
        /// </summary>
        public string StdOut => string.Join("\n", _StdOutDatas);

        /// <summary>
        /// Try to parse the captured stdout as ffprobe JSON output into a minimal <see cref="FFprobeProbe"/> model.
        /// Requires the probe to have been run with <c>-print_format json</c> (and the relevant <c>-show_*</c> options).
        /// </summary>
        /// <param name="probe">The parsed model when successful; otherwise <see langword="null"/>.</param>
        /// <returns><see langword="true"/> when parsing succeeded.</returns>
        public bool TryParseJson(out FFprobeProbe? probe)
        {
            probe = null;
            string raw = StdOut;
            if (string.IsNullOrWhiteSpace(raw)) return false;
            try
            {
                JsonSerializerOptions options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = System.Text.Json.Serialization.JsonNumberHandling.AllowReadingFromString,
                };
                probe = JsonSerializer.Deserialize<FFprobeProbe>(raw, options);
                return probe != null;
            }
            catch
            {
                probe = null;
                return false;
            }
        }
    }
}
