namespace FFmpegArgs.Cores.Enums
{
    /// <summary>
    /// Severity of a <see cref="FFmpegArgs.GraphValidationIssue"/> reported by the
    /// opt-in, read-only graph validator (<c>FFmpegArgs.GraphValidationExtension.Validate</c>).
    /// </summary>
    public enum GraphValidationSeverity
    {
        /// <summary>
        /// Informational note. The graph still builds; this only points out something
        /// that may be unintended (e.g. a dangling filter output that would be auto-sinked).
        /// </summary>
        Info = 0,

        /// <summary>
        /// Likely a mistake. The command may still build, but ffmpeg is likely to reject it
        /// at runtime, or the resulting command is probably not what the author intended.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Almost certainly invalid. Building the command (<c>GetAllArgs</c>/<c>GetFullCommandline</c>)
        /// is expected to throw, or ffmpeg will fail.
        /// </summary>
        Error = 2,
    }
}
