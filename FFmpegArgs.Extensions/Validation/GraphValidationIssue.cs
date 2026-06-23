namespace FFmpegArgs
{
    /// <summary>
    /// A single problem (or note) found by the opt-in, read-only graph validator
    /// <see cref="GraphValidationExtension.Validate(IFFmpegArg)"/>.
    /// </summary>
    /// <remarks>
    /// This type is purely descriptive: producing it never mutates the graph and never
    /// changes when/whether building the command throws. It is meant to be inspected by the
    /// caller before rendering, as an extra (optional) safety net on top of the construction-time
    /// checks already enforced by the core API.
    /// </remarks>
    public sealed class GraphValidationIssue
    {
        /// <summary>
        /// How serious the issue is.
        /// </summary>
        public GraphValidationSeverity Severity { get; }

        /// <summary>
        /// Stable, machine-readable code for the kind of issue
        /// (e.g. <c>"InputMapReusedWithoutSplit"</c>). Useful for filtering/asserting in tests.
        /// </summary>
        public string Code { get; }

        /// <summary>
        /// Human-readable explanation of the issue.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Optional extra context (e.g. the offending map name or filter name). May be <c>null</c>.
        /// </summary>
        public string? Context { get; }

        /// <summary>
        ///
        /// </summary>
        public GraphValidationIssue(GraphValidationSeverity severity, string code, string message, string? context = null)
        {
            Severity = severity;
            Code = code ?? throw new ArgumentNullException(nameof(code));
            Message = message ?? throw new ArgumentNullException(nameof(message));
            Context = context;
        }

        /// <summary>
        ///
        /// </summary>
        public override string ToString()
            => Context == null
                ? $"[{Severity}] {Code}: {Message}"
                : $"[{Severity}] {Code}: {Message} ({Context})";
    }
}
