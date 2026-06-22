namespace FFmpegArgs.Test
{
    internal static class Render
    {
        internal static void TestRender(this IFFmpegArg ffmpegArg,string filterFileName, string outputFileName)
        {
            var render = ffmpegArg.Render(b => b.WithFilterScriptName(filterFileName).UseFilterChain(true));
#if Render
            var render_result = render.Execute();
            Assert.IsTrue(render_result.ExitCode == 0);
#if Play
            Process.Start("ffplay", outputFileName);
#endif
#else
            // Khong dinh nghia 'Render' (vd Release): chi dung args, khong chay ffmpeg.
            Assert.IsNotNull(render);
#endif
        }
    }
}
