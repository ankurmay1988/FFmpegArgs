using FFmpegArgs.Filters.AudioSources;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class ApulsatorFilterTest
    {
        /// <summary>
        /// Timing() must take the timing enum (bpm/ms/hz), not the waveform mode enum.
        /// </summary>
        [TestMethod]
        public void Apulsator_Timing_UsesTimingEnum()
        {
            FilterGraph graph = new FilterGraph { AutoSinkUnusedMapOut = true };
            graph.AnullsrcFilter().MapOut
                .ApulsatorFilter().Timing(ApulsatorTiming.Hz).Mode(ApulsatorMode.Sine);

            string args = graph.GetFiltersArgs(false, true);

            Assert.IsTrue(args.Contains("timing=hz"), args);
            Assert.IsTrue(args.Contains("mode=sine"), args);
        }
    }
}
