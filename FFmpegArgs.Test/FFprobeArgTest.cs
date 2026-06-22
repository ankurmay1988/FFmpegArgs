using FFprobeArgs;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class FFprobeArgTest
    {
        [TestMethod]
        public void TestShowFormatShowStreamsJson()
        {
            var args = new FFprobeArg()
                .HideBanner()
                .LogLevel("error")
                .PrintFormat(FFprobePrintFormat.Json)
                .ShowFormat()
                .ShowStreams()
                .SetInput("input.mp4")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-hide_banner",
                "-loglevel", "error",
                "-print_format", "json",
                "-show_format",
                "-show_streams",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void TestSelectStreamsVideo0()
        {
            var args = new FFprobeArg()
                .PrintFormat(FFprobePrintFormat.Json)
                .SelectStreams("v:0")
                .ShowStreams()
                .SetInput("input.mkv")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-print_format", "json",
                "-select_streams", "v:0",
                "-show_streams",
                "-i", "input.mkv",
            }, args);
        }

        [TestMethod]
        public void TestShowEntries()
        {
            var args = new FFprobeArg()
                .PrintFormat(FFprobePrintFormat.Csv)
                .ShowEntries("stream=index,codec_type,codec_name")
                .SetInput("clip.mov")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-print_format", "csv",
                "-show_entries", "stream=index,codec_type,codec_name",
                "-i", "clip.mov",
            }, args);
        }

        [TestMethod]
        public void TestInputAsPositional()
        {
            // asOption:false -> the url is a trailing positional arg (no -i).
            var args = new FFprobeArg()
                .ShowFormat()
                .SetInput("with space.mp4", asOption: false)
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-show_format",
                "with space.mp4",
            }, args);

            // Token list must keep the space RAW (no manual quoting); the renderer quotes it.
            Assert.IsTrue(args.Contains("with space.mp4"));
            Assert.IsFalse(args.Contains("\"with space.mp4\""));
        }

        [TestMethod]
        public void TestCountFramesAndPackets()
        {
            var args = new FFprobeArg()
                .PrintFormat(FFprobePrintFormat.Json)
                .SelectStreams("v")
                .ShowStreams()
                .CountFrames()
                .CountPackets()
                .SetInput("video.mp4")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-print_format", "json",
                "-select_streams", "v",
                "-show_streams",
                "-count_frames",
                "-count_packets",
                "-i", "video.mp4",
            }, args);
        }

        [TestMethod]
        public void TestShowPacketsAndFrames()
        {
            var args = new FFprobeArg()
                .PrintFormat(FFprobePrintFormat.Xml)
                .ShowPackets()
                .ShowFrames()
                .SetInput("media.ts")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-print_format", "xml",
                "-show_packets",
                "-show_frames",
                "-i", "media.ts",
            }, args);
        }

        [TestMethod]
        public void TestExtraOptionBeforeInput()
        {
            var args = new FFprobeArg()
                .ShowFormat()
                .AddExtraOption("-read_intervals", "%+10")
                .SetInput("input.mp4")
                .GetAllArgs()
                .ToList();

            CollectionAssert.AreEqual(new[]
            {
                "-show_format",
                "-read_intervals", "%+10",
                "-i", "input.mp4",
            }, args);
        }

        [TestMethod]
        public void TestPrintFormatNames()
        {
            Assert.AreEqual("default", FFprobePrintFormat.Default.ToFFprobeName());
            Assert.AreEqual("json", FFprobePrintFormat.Json.ToFFprobeName());
            Assert.AreEqual("xml", FFprobePrintFormat.Xml.ToFFprobeName());
            Assert.AreEqual("csv", FFprobePrintFormat.Csv.ToFFprobeName());
            Assert.AreEqual("flat", FFprobePrintFormat.Flat.ToFFprobeName());
            Assert.AreEqual("ini", FFprobePrintFormat.Ini.ToFFprobeName());
        }

        [TestMethod]
        public void TestEmptyArgsWhenNothingSet()
        {
            var args = new FFprobeArg().GetAllArgs().ToList();
            Assert.AreEqual(0, args.Count);
        }
    }
}
