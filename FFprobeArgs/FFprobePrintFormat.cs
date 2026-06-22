namespace FFprobeArgs
{
    /// <summary>
    /// ffprobe writer (print/output) format, used by <c>-print_format</c> / <c>-of</c>.
    /// </summary>
    public enum FFprobePrintFormat
    {
        /// <summary>
        /// default
        /// </summary>
        Default,
        /// <summary>
        /// json
        /// </summary>
        Json,
        /// <summary>
        /// xml
        /// </summary>
        Xml,
        /// <summary>
        /// csv
        /// </summary>
        Csv,
        /// <summary>
        /// flat
        /// </summary>
        Flat,
        /// <summary>
        /// ini
        /// </summary>
        Ini,
    }

    /// <summary>
    /// Helpers for <see cref="FFprobePrintFormat"/>.
    /// </summary>
    public static class FFprobePrintFormatExtensions
    {
        /// <summary>
        /// The ffprobe writer name for the given format (e.g. <see cref="FFprobePrintFormat.Json"/> =&gt; "json").
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToFFprobeName(this FFprobePrintFormat format)
        {
            switch (format)
            {
                case FFprobePrintFormat.Default: return "default";
                case FFprobePrintFormat.Json: return "json";
                case FFprobePrintFormat.Xml: return "xml";
                case FFprobePrintFormat.Csv: return "csv";
                case FFprobePrintFormat.Flat: return "flat";
                case FFprobePrintFormat.Ini: return "ini";
                default: throw new ArgumentOutOfRangeException(nameof(format));
            }
        }
    }
}
