namespace FFprobeArgs
{
    /// <summary>
    /// Render extensions for <see cref="FFprobeArg"/>.
    /// </summary>
    public static class FFprobeRenderExtensions
    {
        /// <summary>
        /// Render with default <see cref="FFprobeRenderConfig"/> (ffprobe from PATH and WorkingDirectory is Current).
        /// </summary>
        /// <param name="ffprobeArg"></param>
        /// <returns></returns>
        public static FFprobeRender Render(this FFprobeArg ffprobeArg) => FFprobeRender.FromArguments(ffprobeArg, new FFprobeRenderConfig());
        /// <summary>
        ///
        /// </summary>
        /// <param name="ffprobeArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static FFprobeRender Render(this FFprobeArg ffprobeArg, FFprobeRenderConfig config) => FFprobeRender.FromArguments(ffprobeArg, config);
        /// <summary>
        ///
        /// </summary>
        /// <param name="ffprobeArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static FFprobeRender Render(this FFprobeArg ffprobeArg, Action<FFprobeRenderConfig> config) => FFprobeRender.FromArguments(ffprobeArg, config);
    }
}
