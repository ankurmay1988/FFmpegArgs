namespace FFmpegArgs.Test
{
    [TestClass]
    public class HardwareAccelTest
    {
        [TestMethod]
        public void Hwaccel_Enum_Cuda_Before_Input()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("input.mp4");
            input.Hwaccel(HardwareAccel.cuda);
            ffmpegArg.AddInput(input);

            var args = ffmpegArg.GetInputsArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-hwaccel", "cuda",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void Hwaccel_String_Overload_ForwardCompat()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("input.mp4");
            input.Hwaccel("some_future_method");
            ffmpegArg.AddInput(input);

            var args = ffmpegArg.GetInputsArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-hwaccel", "some_future_method",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void HwaccelDevice_Emits_Device_Token()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("input.mp4");
            input.Hwaccel(HardwareAccel.cuda).HwaccelDevice("0");
            ffmpegArg.AddInput(input);

            var args = ffmpegArg.GetInputsArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-hwaccel", "cuda",
                "-hwaccel_device", "0",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void HwaccelOutputFormat_Emits_PixelFormat_Token()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("input.mp4");
            input.Hwaccel(HardwareAccel.cuda).HwaccelOutputFormat("cuda");
            ffmpegArg.AddInput(input);

            var args = ffmpegArg.GetInputsArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-hwaccel", "cuda",
                "-hwaccel_output_format", "cuda",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void Hwaccel_Enum_Token_Matches_FFmpegName()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("input.mp4");
            input.Hwaccel(HardwareAccel.d3d11va);
            ffmpegArg.AddInput(input);

            var args = ffmpegArg.GetInputsArgs().ToList();

            // Enum name must round-trip to the exact ffmpeg token (no [Name] remap needed).
            Assert.IsTrue(args.SequenceEqual(new[]
            {
                "-hwaccel", "d3d11va",
                "-i", "input.mp4",
            }));
        }

        [TestMethod]
        public void InitHwDevice_Global_Emits_Token()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            ffmpegArg.InitHwDevice("cuda=gpu0");

            var args = ffmpegArg.GetGlobalArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-init_hw_device", "cuda=gpu0",
            }, args);
        }

        [TestMethod]
        public void FilterHwDevice_Global_Emits_Token()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            ffmpegArg.FilterHwDevice("gpu0");

            var args = ffmpegArg.GetGlobalArgs().ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-filter_hw_device", "gpu0",
            }, args);
        }

        [TestMethod]
        public void InitAndFilterHwDevice_Global_Emit_Both_Tokens()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            ffmpegArg.InitHwDevice("vaapi:/dev/dri/renderD128").FilterHwDevice("vaapi");

            string args = string.Join(" ", ffmpegArg.GetGlobalArgs());

            Assert.IsTrue(args.Contains("-init_hw_device vaapi:/dev/dri/renderD128"), args);
            Assert.IsTrue(args.Contains("-filter_hw_device vaapi"), args);
        }
    }
}
