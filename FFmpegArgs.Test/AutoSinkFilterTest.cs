using FFmpegArgs.Filters.AudioSources;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class AutoSinkFilterTest
    {
        [TestMethod]
        public void AutoSink_Video_Dangling()
        {
            FilterGraph filterGraph = new FilterGraph { AutoSinkUnusedMapOut = true };
            filterGraph.NullsrcFilter().Size(new Size(1280, 720));//map out bo lung

            Assert.AreEqual("nullsrc=s=1280x720,nullsink", filterGraph.GetFiltersArgs(false, true));
        }

        [TestMethod]
        public void AutoSink_Audio_Dangling()
        {
            FilterGraph filterGraph = new FilterGraph { AutoSinkUnusedMapOut = true };
            filterGraph.AnullsrcFilter();//map out bo lung

            Assert.AreEqual("anullsrc,anullsink", filterGraph.GetFiltersArgs(false, true));
        }

        [TestMethod]
        public void AutoSink_Mixed_VideoAudio()
        {
            FilterGraph filterGraph = new FilterGraph { AutoSinkUnusedMapOut = true };
            filterGraph.NullsrcFilter().Size(new Size(320, 240));
            filterGraph.AnullsrcFilter();

            Assert.AreEqual("nullsrc=s=320x240,nullsink;anullsrc,anullsink", filterGraph.GetFiltersArgs(false, true));
        }

        [TestMethod]
        public void AutoSink_Disabled_Throws()
        {
            FilterGraph filterGraph = new FilterGraph();//mac dinh false
            filterGraph.NullsrcFilter().Size(new Size(640, 480));//map out bo lung

            Assert.ThrowsException<InvalidOperationException>(() => filterGraph.GetFiltersArgs(false, true));
        }

        [TestMethod]
        public void AutoSink_Idempotent()
        {
            FilterGraph filterGraph = new FilterGraph { AutoSinkUnusedMapOut = true };
            filterGraph.NullsrcFilter().Size(new Size(320, 240));

            string first = filterGraph.GetFiltersArgs(false, true);
            string second = filterGraph.GetFiltersArgs(false, true);

            Assert.AreEqual(first, second);
            Assert.AreEqual(2, filterGraph.Filters.Count());//nullsrc + 1 sink, khong nhan doi
        }
    }
}
