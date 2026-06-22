namespace FFmpegArgs.Test
{
    [TestClass]
    public class FilterStringInputTest
    {
        // The filter is emitted as a single RAW token (no manual quoting); the renderer / ProcessStartInfo.ArgumentList
        // handles OS-level quoting. So even a filter with spaces must NOT appear wrapped in double quotes in the token stream.

        [TestMethod]
        public void FilterStringInput_WithSpecialChars_NotManuallyQuoted()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            FilterStringInput input = new FilterStringInput("color=c=red:s=1280x720 [out]");
            ffmpegArg.AddInput(input);

            string commandline = string.Join(" ", ffmpegArg.GetInputsArgs());

            Assert.IsTrue(commandline.Contains("-f lavfi"), $"expected '-f lavfi' in: {commandline}");
            Assert.IsTrue(commandline.Contains("-i color=c=red:s=1280x720 [out]"), $"expected raw '-i' token in: {commandline}");
            Assert.IsFalse(commandline.Contains("\""), $"did not expect manual double-quotes in: {commandline}");
        }

        [TestMethod]
        public void FilterStringInput_WithoutSpaces_IsRawToken()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            FilterStringInput input = new FilterStringInput("testsrc");
            ffmpegArg.AddInput(input);

            string commandline = string.Join(" ", ffmpegArg.GetInputsArgs());

            Assert.IsTrue(commandline.Contains("-f lavfi"), $"expected '-f lavfi' in: {commandline}");
            Assert.IsTrue(commandline.Contains("-i testsrc"), $"expected '-i testsrc' in: {commandline}");
            Assert.IsFalse(commandline.Contains("\""), $"did not expect quotes in: {commandline}");
        }
    }
}
