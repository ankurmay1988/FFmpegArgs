using System.Threading;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class FFplayRenderTest
    {
        /// <summary>
        /// Arg-build only (no ffplay launch): FFplayRender.FromArguments must produce a non-empty
        /// ArgumentList containing the input flag '-i'. Deterministic; runs in every configuration.
        /// </summary>
        [TestMethod]
        public void FromArguments_BuildsArgumentList()
        {
            FFplayArg ffplayArg = new FFplayArg();
            ImageFilterGraphInput filterInput = new ImageFilterGraphInput();
            filterInput.FilterGraph.ColorFilter().Color(Color.Red).Size(new Size(320, 240)).MapOut
                .FpsFilter().Fps(10);
            ffplayArg.AddImagesInput(filterInput).First();

            FFplayRender render = FFplayRender.FromArguments(ffplayArg, new FFplayRenderConfig());

            Assert.IsNotNull(render);
            Assert.IsTrue(render.ArgumentsList.Count > 0, "ArgumentsList should not be empty");
            CollectionAssert.Contains(render.ArgumentsList.ToArray(), "-i", "expected '-i' input token");
        }

#if Render
        /// <summary>
        /// Headless launch: -nodisp (no window) + -autoexit + a 0.3s lavfi audio source.
        /// ffplay should auto-exit cleanly with ExitCode 0.
        /// NOTE: an AUDIO lavfi source (sine) is used instead of a video testsrc because ffplay's
        /// video filtergraph requires the display/SDL video subsystem even under -nodisp, which is
        /// unavailable in a headless CI environment; an audio-only graph plays fully headless.
        /// </summary>
        [TestMethod]
        public void Execute_HeadlessAutoExit_ReturnsZero()
        {
            FFplayRender render = FFplayRender.FromArgumentsList(
                new FFplayRenderConfig(),
                "-nodisp", "-autoexit", "-f", "lavfi", "-i", "sine=frequency=1000:duration=0.3");

            FFplayRenderResult result = render.Execute();

            Assert.AreEqual(0, result.ExitCode, "ffplay headless auto-exit should return 0");
        }

        /// <summary>
        /// Cancellation: infinite lavfi audio source, no display. CancelAfter 600ms must KILL ffplay
        /// (FFplayRender registers token.Register(() => process.Kill())) -> Execute returns promptly
        /// with a non-zero exit code.
        /// </summary>
        [TestMethod]
        public void Execute_Cancel_KillsFFplay_ReturnsPromptly()
        {
            FFplayRender render = FFplayRender.FromArgumentsList(
                new FFplayRenderConfig(),
                "-nodisp", "-f", "lavfi", "-i", "sine=frequency=1000");

            using CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(600));

            Stopwatch sw = Stopwatch.StartNew();
            FFplayRenderResult result = render.Execute(cts.Token);
            sw.Stop();

            Assert.AreNotEqual(0, result.ExitCode, $"killed process should not exit 0 (elapsed {sw.Elapsed})");
            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(30), $"cancel should return promptly, took {sw.Elapsed}");
        }
#endif
    }
}
