using FFmpegArgs.Filters.AudioSources;
using FFmpegArgs.Filters.VideoSinks;
using FFmpegArgs.Filters.AudioSinks;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class SinkFilterTest
    {
        [TestMethod]
        public void VideoNullsink()
        {
            FilterGraph filterGraph = new FilterGraph();
            filterGraph.NullsrcFilter().Size(new Size(1280, 720)).MapOut
                .NullsinkFilter();

            Assert.AreEqual("nullsrc=s=1280x720,nullsink", filterGraph.GetFiltersArgs(false, true));
            Assert.AreEqual("nullsrc=s=1280x720[f_0];[f_0]nullsink", filterGraph.GetFiltersArgs(false, false));
        }

        [TestMethod]
        public void VideoBuffersink()
        {
            FilterGraph filterGraph = new FilterGraph();
            filterGraph.NullsrcFilter().MapOut
                .BuffersinkFilter();

            Assert.AreEqual("nullsrc,buffersink", filterGraph.GetFiltersArgs(false, true));
            Assert.AreEqual("nullsrc[f_0];[f_0]buffersink", filterGraph.GetFiltersArgs(false, false));
        }

        [TestMethod]
        public void AudioAnullsink()
        {
            FilterGraph filterGraph = new FilterGraph();
            filterGraph.AnullsrcFilter().MapOut
                .AnullsinkFilter();

            Assert.AreEqual("anullsrc,anullsink", filterGraph.GetFiltersArgs(false, true));
            Assert.AreEqual("anullsrc[f_0];[f_0]anullsink", filterGraph.GetFiltersArgs(false, false));
        }

        [TestMethod]
        public void AudioAbuffersink()
        {
            FilterGraph filterGraph = new FilterGraph();
            filterGraph.AnullsrcFilter().MapOut
                .AbuffersinkFilter();

            Assert.AreEqual("anullsrc,abuffersink", filterGraph.GetFiltersArgs(false, true));
            Assert.AreEqual("anullsrc[f_0];[f_0]abuffersink", filterGraph.GetFiltersArgs(false, false));
        }
    }
}
