namespace FFplayArgs
{
    /// <summary>
    /// Result of a <see cref="FFplayRender"/> execution.
    /// </summary>
    public class FFplayRenderResult
    {
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
        /// Captured stderr lines from ffplay (ffplay logs to stderr).
        /// </summary>
        public IReadOnlyList<string> LogDatas { get { return _LogDatas; } }
    }
}
