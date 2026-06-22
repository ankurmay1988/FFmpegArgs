namespace FFmpegArgs.Test
{
    [TestClass]
    public class AutoSinkRenderTest
    {
        /// <summary>
        /// KHONG loi: auto-sink nuot nhanh thua cua split, van con output that -> ffmpeg chay OK (exit 0).
        /// </summary>
        [TestMethod]
        public void AutoSink_RenderOk()
        {
            string outputFileName = $"{nameof(AutoSink_RenderOk)}.mp4";
            string filterFileName = $"{nameof(AutoSink_RenderOk)}.txt";

            FFmpegArg ffmpegArg = new FFmpegArg().OverWriteOutput();
            ffmpegArg.FilterGraph.AutoSinkUnusedMapOut = true;

            ImageFilterGraphInput filterInput = new ImageFilterGraphInput();
            filterInput.FilterGraph.ColorFilter().Color(Color.Red).Size(new Size(640, 480)).MapOut
                .FpsFilter().Fps(25);
            var imageMap = ffmpegArg.AddImagesInput(filterInput).First();

            var split = imageMap.SplitFilter(2).MapsOut;//2 nhanh
            var output = split.First();//dung 1 nhanh; split.Last() bo lung -> auto-sink nullsink

            ImageFileOutput imageFileOutput = new ImageFileOutput(outputFileName, output)
                .Duration(TimeSpan.FromSeconds(1));
            ffmpegArg.AddOutput(imageFileOutput);

            ffmpegArg.TestRender(filterFileName, outputFileName);
        }

        /// <summary>
        /// LOI: auto-sink nuot het nhanh (khong them output) -> command 0 output
        /// -> GetOutputsArgs nem "Output is empty" (dam bao >=1 output o muc command).
        /// </summary>
        [TestMethod]
        public void AutoSink_NoOutput_Throws()
        {
            string outputFileName = $"{nameof(AutoSink_NoOutput_Throws)}.mp4";
            string filterFileName = $"{nameof(AutoSink_NoOutput_Throws)}.txt";

            FFmpegArg ffmpegArg = new FFmpegArg().OverWriteOutput();
            ffmpegArg.FilterGraph.AutoSinkUnusedMapOut = true;

            ImageFilterGraphInput filterInput = new ImageFilterGraphInput();
            filterInput.FilterGraph.ColorFilter().Color(Color.Red).Size(new Size(640, 480)).MapOut
                .FpsFilter().Fps(25);
            var imageMap = ffmpegArg.AddImagesInput(filterInput).First();
            imageMap.ScaleFilter().W("320").H("240");//map out bo lung, KHONG AddOutput

            var ex = Assert.ThrowsException<InvalidOperationException>(
                () => ffmpegArg.TestRender(filterFileName, outputFileName));
            StringAssert.Contains(ex.Message, "Output is empty");
        }
    }
}
