namespace FFmpegArgs.Test
{
    /// <summary>
    /// Tests for the opt-in, read-only graph validator
    /// (<see cref="GraphValidationExtension.Validate(IFFmpegArg)"/>).
    /// No ffmpeg execution; pure arg-build / read-only inspection.
    /// </summary>
    [TestClass]
    public class GraphValidationTest
    {
        // ---- valid graph -> 0 issues ----

        [TestMethod]
        public void ValidGraph_HasNoIssues()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);

            ImageMap scaled = videoMap.ImageMaps.First().ScaleFilter().MapOut;

            VideoFileOutput output = new VideoFileOutput("out.mp4", scaled, videoMap.AudioMaps.First());
            ffmpegArg.AddOutput(output);

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            Assert.AreEqual(0, issues.Count, string.Join("; ", issues.Select(x => x.ToString())));
        }

        // ---- input map reused without split (constructible, runtime-invalid) ----

        [TestMethod]
        public void InputMapReusedWithoutSplit_IsReported()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);

            ImageMap inputImage = videoMap.ImageMaps.First();

            // Feed the SAME input map to two filters. The core API allows this for input maps
            // (the "one to one, except input" exemption), but ffmpeg rejects re-using an input label.
            ImageMap a = inputImage.ScaleFilter().MapOut;
            ImageMap b = inputImage.ScaleFilter().MapOut;

            // Bind both branch outputs so the only flagged problem is the input reuse.
            VideoFileOutput output = new VideoFileOutput("out.mp4", new[] { a, b }, videoMap.AudioMaps.ToArray());
            ffmpegArg.AddOutput(output);

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            GraphValidationIssue? reuse = issues.FirstOrDefault(x => x.Code == "InputMapReusedWithoutSplit");
            Assert.IsNotNull(reuse, "Expected InputMapReusedWithoutSplit issue.");
            Assert.AreEqual(GraphValidationSeverity.Warning, reuse!.Severity);
            Assert.AreEqual(inputImage.MapName, reuse.Context);
        }

        [TestMethod]
        public void InputMapUsedOnceViaSplit_HasNoReuseIssue()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);

            // Correct pattern: consume the input ONCE, fork with split.
            var split = videoMap.ImageMaps.First().SplitFilter(2);
            ImageMap a = split.MapsOut.First().ScaleFilter().MapOut;
            ImageMap b = split.MapsOut.Last().ScaleFilter().MapOut;

            VideoFileOutput output = new VideoFileOutput("out.mp4", new[] { a, b }, videoMap.AudioMaps.ToArray());
            ffmpegArg.AddOutput(output);

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            Assert.IsFalse(issues.Any(x => x.Code == "InputMapReusedWithoutSplit"),
                string.Join("; ", issues.Select(x => x.ToString())));
        }

        // ---- dangling filter output ----

        [TestMethod]
        public void DanglingFilterOutput_IsReportedAsError()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);

            // Scale output is never mapped/consumed -> dangling.
            _ = videoMap.ImageMaps.First().ScaleFilter();

            // Provide an output (from the audio map) so the only relevant issue is the dangling video output.
            AudioFileOutput output = new AudioFileOutput("out.mp3", videoMap.AudioMaps.First());
            ffmpegArg.AddOutput(output);

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            GraphValidationIssue? dangling = issues.FirstOrDefault(x => x.Code == "DanglingFilterOutput");
            Assert.IsNotNull(dangling, "Expected DanglingFilterOutput issue.");
            Assert.AreEqual(GraphValidationSeverity.Error, dangling!.Severity);
        }

        [TestMethod]
        public void DanglingFilterOutput_WithAutoSink_IsInfoOnly()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            ffmpegArg.FilterGraph.AutoSinkUnusedMapOut = true;

            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);

            _ = videoMap.ImageMaps.First().ScaleFilter();// dangling but auto-sinked

            AudioFileOutput output = new AudioFileOutput("out.mp3", videoMap.AudioMaps.First());
            ffmpegArg.AddOutput(output);

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            Assert.IsFalse(issues.Any(x => x.Code == "DanglingFilterOutput"),
                "Auto-sink on: should not be a hard error.");
            Assert.IsTrue(issues.Any(x => x.Code == "DanglingFilterOutputAutoSinked"
                                          && x.Severity == GraphValidationSeverity.Info));
        }

        // ---- empty input / output ----

        [TestMethod]
        public void NoInput_And_NoOutput_AreReported()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();

            IReadOnlyList<GraphValidationIssue> issues = ffmpegArg.Validate();

            Assert.IsTrue(issues.Any(x => x.Code == "NoInput" && x.Severity == GraphValidationSeverity.Error));
            Assert.IsTrue(issues.Any(x => x.Code == "NoOutput" && x.Severity == GraphValidationSeverity.Error));
        }

        [TestMethod]
        public void Validate_IsReadOnly_DoesNotChangeBuildOutput()
        {
            FFmpegArg ffmpegArg = new FFmpegArg();
            VideoFileInput input = new VideoFileInput("in.mp4");
            VideoMap videoMap = ffmpegArg.AddVideoInput(input);
            ImageMap scaled = videoMap.ImageMaps.First().ScaleFilter().MapOut;
            VideoFileOutput output = new VideoFileOutput("out.mp4", scaled, videoMap.AudioMaps.First());
            ffmpegArg.AddOutput(output);

            string before = string.Join(" ", ffmpegArg.GetFullCommandline());
            int filterCountBefore = ffmpegArg.FilterGraph.Filters.Count();

            _ = ffmpegArg.Validate();

            string after = string.Join(" ", ffmpegArg.GetFullCommandline());
            int filterCountAfter = ffmpegArg.FilterGraph.Filters.Count();

            Assert.AreEqual(before, after, "Validate must not change the rendered command.");
            Assert.AreEqual(filterCountBefore, filterCountAfter, "Validate must not add filters.");
        }

        [TestMethod]
        public void Validate_NullArg_Throws()
        {
            FFmpegArg? ffmpegArg = null;
            Assert.ThrowsException<ArgumentNullException>(() => ffmpegArg!.Validate());
        }
    }
}
