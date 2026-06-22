namespace FFmpegArgs.Cores.Extensions
{
    internal static class FilterExtensions
    {
        /// <summary>
        /// https://ffmpeg.org/ffmpeg-filters.html#Notes-on-filtergraph-escaping <br></br>
        /// https://ffmpeg.org/ffmpeg-utils.html#quoting_005fand_005fescaping <br></br>
        /// Text Level
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string FiltergraphEscapingLv1(this string input)
        {
            return input
                .Replace(@"\", @"\\\\\\\\")
                .Replace(@"'", @"\'")
                .Replace(@":", @"\:");
        }
        /// <summary>
        /// Filter level
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        internal static string FiltergraphEscapingLv2(this string input)
        {
            return input
              .Replace(@"\'", @"\\\'")
              .Replace(@"\:", @"\\:")
              .Replace(@"[", @"\[")
              .Replace(@"]", @"\]")
              .Replace(@";", @"\;")
              .Replace(@",", @"\,")
              .Replace("\"", "\\\"");
        }
        // Lv3 (mức process arg / shell): hien chua can vi thuc thi qua ProcessStartInfo.ArgumentList
        // (UseShellExecute=false) nen khong co tang shell de escape. Giu lai stub cho truong hop
        // tuong lai can build chuoi command qua shell.
        ///// <summary>
        ///// process arg level
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //internal static string FiltergraphEscapingLv3(this string input)
        //{
        //    return ;
        //}
    }
}
