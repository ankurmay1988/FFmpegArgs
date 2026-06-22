namespace FFmpegArgs.Test
{
    [TestClass]
    public class SubtitleOptionsTest
    {
        [TestMethod]
        public void Output_Scodec_Emitted()
        {
            ImageFileOutput output = new ImageFileOutput("out.mkv", new ImageMap(new FilterGraph(), "v"));
            output.Scodec("mov_text");

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-c:s mov_text"), args);
        }

        [TestMethod]
        public void Output_CopySubtitle_Emitted()
        {
            ImageFileOutput output = new ImageFileOutput("out.mkv", new ImageMap(new FilterGraph(), "v"));
            output.CopySubtitle();

            string args = string.Join(" ", output.GetAllArgs());

            Assert.IsTrue(args.Contains("-c:s copy"), args);
        }

        [TestMethod]
        public void Input_SubCharenc_Emitted()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            FilterStringInput input = new FilterStringInput("testsrc");
            input.SubCharenc("CP1252");
            ffmpegArg.AddInput(input);

            string args = string.Join(" ", ffmpegArg.GetInputsArgs());

            Assert.IsTrue(args.Contains("-sub_charenc CP1252"), args);
        }
    }
}
