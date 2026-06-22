using FFmpegArgs.Inputs.Demuxers;
using FFmpegArgs.Outputs.Muxers;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class MuxerDemuxerOptionsTest
    {
        [TestMethod]
        public void Image2Demux_Emits_Format_And_Options()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            ImageFileInput input = new ImageFileInput("img-%03d.png");
            input.Re()
                 .Image2Demux(d => d.StartNumber(5).PatternType(Image2PatternType.glob));
            ffmpegArg.AddInput(input);

            string args = string.Join(" ", ffmpegArg.GetInputsArgs());

            Assert.IsTrue(args.Contains("-re"), args);
            Assert.IsTrue(args.Contains("-f image2"), args);
            Assert.IsTrue(args.Contains("-start_number 5"), args);
            Assert.IsTrue(args.Contains("-pattern_type glob"), args);
        }

        [TestMethod]
        public void MovMux_Emits_Format_Mov_And_MovFlags()
        {
            ImageFileOutput output = new ImageFileOutput("out.mov", new ImageMap(new FilterGraph(), "v"));
            output.MovMux(m => m.MovFlags(MovFlag.faststart, MovFlag.empty_moov));

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-f mov"), args);
            Assert.IsTrue(args.Contains("-movflags +faststart+empty_moov"), args);
        }

        [TestMethod]
        public void Mp4Mux_Emits_Format_Mp4_And_MovFlags()
        {
            ImageFileOutput output = new ImageFileOutput("out.mp4", new ImageMap(new FilterGraph(), "v"));
            output.Mp4Mux(m => m.MovFlags(MovFlag.faststart));

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-f mp4"), args);
            Assert.IsTrue(args.Contains("-movflags +faststart"), args);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void MovFlags_Empty_Throws()
        {
            ImageFileOutput output = new ImageFileOutput("out.mov", new ImageMap(new FilterGraph(), "v"));
            output.MovMux(m => m.MovFlags());
        }
    }
}
