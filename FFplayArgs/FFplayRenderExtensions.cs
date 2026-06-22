namespace FFplayArgs
{
    /// <summary>
    /// Render extensions for <see cref="FFplayArg"/>.
    /// </summary>
    public static class FFplayRenderExtensions
    {
        /// <summary>
        /// Render with default <see cref="FFplayRenderConfig"/> (ffplay from PATH/CurrentWorkingDirectory and WorkingDirectory is Current)
        /// </summary>
        /// <param name="ffplayArg"></param>
        /// <returns></returns>
        public static FFplayRender Render(this FFplayArg ffplayArg) => FFplayRender.FromArguments(ffplayArg, new FFplayRenderConfig());
        /// <summary>
        ///
        /// </summary>
        /// <param name="ffplayArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static FFplayRender Render(this FFplayArg ffplayArg, FFplayRenderConfig config) => FFplayRender.FromArguments(ffplayArg, config);
        /// <summary>
        ///
        /// </summary>
        /// <param name="ffplayArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        public static FFplayRender Render(this FFplayArg ffplayArg, Action<FFplayRenderConfig> config) => FFplayRender.FromArguments(ffplayArg, config);
    }
}
