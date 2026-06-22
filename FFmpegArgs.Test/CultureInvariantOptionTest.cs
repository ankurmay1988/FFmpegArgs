using System.Globalization;

namespace FFmpegArgs.Test
{
    [TestClass]
    public class CultureInvariantOptionTest
    {
        private sealed class TestOption : BaseOption { }

        /// <summary>
        /// Numbers must be formatted with InvariantCulture so ffmpeg never gets a localized decimal
        /// separator (e.g. "0,5") even when the process runs under a culture like de-DE / vi-VN.
        /// </summary>
        [TestMethod]
        public void NumericOptions_UseInvariantDecimalSeparator()
        {
            CultureInfo original = CultureInfo.CurrentCulture;
            try
            {
                CultureInfo.CurrentCulture = new CultureInfo("de-DE");//comma decimal separator

                TestOption o = new TestOption();
                o.SetOptionRange("-f", 0.5f, 0f, 1f);    //float
                o.SetOptionRange("-d", 1.25, 0.0, 2.0);  //double
                o.SetOptionRange("-m", 3.5m, 0m, 10m);   //decimal
                o.SetOption("-obj", (object)2.75);       //object path (boxed double)

                Assert.AreEqual("0.5", o.Options["-f"]);
                Assert.AreEqual("1.25", o.Options["-d"]);
                Assert.AreEqual("3.5", o.Options["-m"]);
                Assert.AreEqual("2.75", o.Options["-obj"]);
            }
            finally
            {
                CultureInfo.CurrentCulture = original;
            }
        }
    }
}
