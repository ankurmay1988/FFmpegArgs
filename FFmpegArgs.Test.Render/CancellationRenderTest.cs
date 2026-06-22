using System.Threading;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class CancellationRenderTest
    {
        /// <summary>
        /// Cancel khi dang render: CancellationToken phai KILL process ffmpeg (FFmpegRender dang ky
        /// token.Register(() => process.Kill())) -> Execute tra ve som thay vi chay het.
        /// Dung render danh nghia rat dai (-t 24h) nen khong may nao encode xong trong vai giay;
        /// cancel sau 800ms -> bien do "tra ve som" cuc lon, khong phu thuoc toc do may.
        /// </summary>
        [TestMethod]
        public void Cancel_KillsFFmpegProcess_ReturnsPromptly()
        {
            string outputFileName = $"{nameof(Cancel_KillsFFmpegProcess_ReturnsPromptly)}.mp4";
            string filterFileName = $"{nameof(Cancel_KillsFFmpegProcess_ReturnsPromptly)}.txt";

            FFmpegArg ffmpegArg = new FFmpegArg().OverWriteOutput();
            ImageFilterGraphInput filterInput = new ImageFilterGraphInput();
            filterInput.FilterGraph.ColorFilter().Color(Color.Red).Size(new Size(1280, 720)).MapOut
                .FpsFilter().Fps(25);
            var imageMap = ffmpegArg.AddImagesInput(filterInput).First();

            ImageFileOutput imageFileOutput = new ImageFileOutput(outputFileName, imageMap)
                .Duration(TimeSpan.FromHours(24));//render rat dai
            ffmpegArg.AddOutput(imageFileOutput);

            var render = ffmpegArg.Render(b => b.WithFilterScriptName(filterFileName).UseFilterChain(true));
#if Render
            using CancellationTokenSource cts = new CancellationTokenSource();
            cts.CancelAfter(TimeSpan.FromMilliseconds(800));

            Stopwatch sw = Stopwatch.StartNew();
            FFmpegRenderResult result = render.Execute(cts.Token);
            sw.Stop();

            //process bi kill -> khong the exit 0
            Assert.AreNotEqual(0, result.ExitCode, $"killed process should not exit 0 (elapsed {sw.Elapsed})");
            //da chay den khi cancel (~800ms), khong phai loi args instant
            Assert.IsTrue(sw.Elapsed >= TimeSpan.FromMilliseconds(300), $"render should run until cancel, took {sw.Elapsed}");
            //tra ve som, khong chay het 24h
            Assert.IsTrue(sw.Elapsed < TimeSpan.FromSeconds(60), $"cancel should return promptly, took {sw.Elapsed}");
#else
            // Khong dinh nghia 'Render' (vd Release): chi dung args, khong chay ffmpeg.
            Assert.IsNotNull(render);
#endif
        }
    }
}
