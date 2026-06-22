namespace FFmpegArgs.Test
{
    [TestClass]
    public class AudioEncoderTest
    {
        /// <summary>
        /// Build a standalone <c>AudioOutputAVStream</c> (no ffmpeg execution) and return its rendered argument string.
        /// </summary>
        static string RenderStreamArgs(Action<FFmpegArgs.Cores.Streams.AudioOutputAVStream> setCodec)
        {
            AudioMap audioMap = new AudioMap(new FilterGraph(), "0:a:0");
            AudioFileOutput output = new AudioFileOutput("out.mka", audioMap);
            setCodec(output.AudioStream);
            return string.Join(" ", output.AudioStream.GetAllArgs());
        }

        [TestMethod]
        public void Aac_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Aac_Codec());
            StringAssert.Contains(args, "-c:a:0 aac");
        }

        [TestMethod]
        public void Libmp3lame_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Libmp3lame_Codec());
            StringAssert.Contains(args, "-c:a:0 libmp3lame");
        }

        [TestMethod]
        public void Ac3_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Ac3_Codec());
            StringAssert.Contains(args, "-c:a:0 ac3");
        }

        [TestMethod]
        public void Eac3_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Eac3_Codec());
            StringAssert.Contains(args, "-c:a:0 eac3");
        }

        [TestMethod]
        public void Flac_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Flac_Codec());
            StringAssert.Contains(args, "-c:a:0 flac");
        }

        [TestMethod]
        public void Alac_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Alac_Codec());
            StringAssert.Contains(args, "-c:a:0 alac");
        }

        [TestMethod]
        public void Libopus_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Libopus_Codec());
            StringAssert.Contains(args, "-c:a:0 libopus");
        }

        [TestMethod]
        public void Libvorbis_SelectsCodec()
        {
            string args = RenderStreamArgs(s => s.Libvorbis_Codec());
            StringAssert.Contains(args, "-c:a:0 libvorbis");
        }

        // ---- representative option coverage (enum / range / boolean / float) ----

        [TestMethod]
        public void Aac_EnumOption_AacCoder()
        {
            // enum -> -aac_coder 1 (fast)
            string args = RenderStreamArgs(s => s.Aac_Codec(c => c.AacCoder(Aac_Coder.fast)));
            StringAssert.Contains(args, "-c:a:0 aac");
            StringAssert.Contains(args, "-aac_coder:a:0 1");
        }

        [TestMethod]
        public void Aac_BooleanOption_AacMs()
        {
            // boolean -> ToFFmpegFlag() => "1"
            string args = RenderStreamArgs(s => s.Aac_Codec(c => c.AacMs(true)));
            StringAssert.Contains(args, "-aac_ms:a:0 1");
        }

        [TestMethod]
        public void Ac3_EnumOption_RoomType()
        {
            // enum -> -room_type 1 (large)
            string args = RenderStreamArgs(s => s.Ac3_Codec(c => c.RoomType(Ac3_RoomType.large)));
            StringAssert.Contains(args, "-c:a:0 ac3");
            StringAssert.Contains(args, "-room_type:a:0 1");
        }

        [TestMethod]
        public void Ac3_RangeOption_Dialnorm()
        {
            // int range -> -dialnorm -10
            string args = RenderStreamArgs(s => s.Ac3_Codec(c => c.Dialnorm(-10)));
            StringAssert.Contains(args, "-dialnorm:a:0 -10");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidRangeException))]
        public void Ac3_RangeOption_OutOfRange_Throws()
        {
            // -dialnorm valid range is -31..-1; 0 must throw
            RenderStreamArgs(s => s.Ac3_Codec(c => c.Dialnorm(0)));
        }

        [TestMethod]
        public void Libopus_EnumOption_Application()
        {
            // enum with non-zero base value -> -application 2048 (voip)
            string args = RenderStreamArgs(s => s.Libopus_Codec(c => c.Application(Libopus_Application.voip)));
            StringAssert.Contains(args, "-c:a:0 libopus");
            StringAssert.Contains(args, "-application:a:0 2048");
        }
    }
}
