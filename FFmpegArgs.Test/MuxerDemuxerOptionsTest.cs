namespace FFmpegArgs.Test
{
    [TestClass]
    public class MuxerDemuxerOptionsTest
    {
        [TestMethod]
        public void Demuxer_InputOptions_Emitted()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            FilterStringInput input = new FilterStringInput("testsrc");
            input.Re().StartNumber(5).PatternType(Image2PatternType.glob);
            ffmpegArg.AddInput(input);

            string args = string.Join(" ", ffmpegArg.GetInputsArgs());

            Assert.IsTrue(args.Contains("-re"), args);
            Assert.IsTrue(args.Contains("-start_number 5"), args);
            Assert.IsTrue(args.Contains("-pattern_type glob"), args);
        }

        [TestMethod]
        public void Muxer_MovFlags_AdditiveForm()
        {
            ImageFileOutput output = new ImageFileOutput("out.mp4", new ImageMap(new FilterGraph(), "v"));
            output.MovFlags(MovFlag.faststart, MovFlag.empty_moov);

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-movflags +faststart+empty_moov"), args);
        }

        [TestMethod]
        public void Muxer_MovFlags_Single_Faststart()
        {
            ImageFileOutput output = new ImageFileOutput("out.mp4", new ImageMap(new FilterGraph(), "v"));
            output.MovFlags(MovFlag.faststart);

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-movflags +faststart"), args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Muxer_MovFlags_Empty_Throws()
        {
            ImageFileOutput output = new ImageFileOutput("out.mp4", new ImageMap(new FilterGraph(), "v"));
            output.MovFlags();
        }
    }
}
