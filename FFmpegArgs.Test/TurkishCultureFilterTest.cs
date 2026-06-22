using System.Globalization;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class TurkishCultureFilterTest
    {
        /// <summary>
        /// On tr-TR, a culture-sensitive ToLower turns 'I' (U+0049) into the dotless 'ı' (U+0131).
        /// Enum values that become ffmpeg keywords must be lowercased with InvariantCulture, so
        /// FadeType.In -> "in" (not "ın"). Guards against the Turkish-i bug in `enum.ToString().ToLower()`.
        /// </summary>
        [TestMethod]
        public void EnumOption_LowercasedInvariant_UnderTurkishCulture()
        {
            CultureInfo original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("tr-TR");

                FilterGraph graph = new FilterGraph { AutoSinkUnusedMapOut = true };
                graph.NullsrcFilter().Size(new Size(640, 480)).MapOut
                    .FadeFilter().Type(FadeType.In);

                string args = graph.GetFiltersArgs(false, true);

                Assert.IsTrue(args.Contains("t=in"), $"expected fade t=in, got: {args}");
                Assert.IsFalse(args.Contains("t=ın"), $"dotless-i (Turkish-i bug) leaked: {args}");
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }
    }
}
