using FFmpegArgs.Cores;

namespace FFmpegArgs
{
    /// <summary>
    /// https://ffmpeg.org/ffmpeg.html#Main-options
    /// </summary>
    public static class InputOutputOptionsExtension
    {
        #region Input
        /// <summary>
        /// -f<br>
        /// </br>Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static T F<T>(this T t, DemuxingFileFormat format) where T : BaseOption, IDemux
           => t.SetOption("-f", format.GetEnumAttribute<NameAttribute>().Name);
        /// <summary>
        /// -f<br>
        /// </br>Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static T Format<T>(this T t, DemuxingFileFormat format) where T : BaseOption, IDemux
            => t.SetOption("-f", format.GetEnumAttribute<NameAttribute>().Name);

        /// <summary>
        /// -i<br>
        /// </br>input file url
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public static T Input<T>(this T t, string input) where T : BaseInput
            => t.SetOption("-i", input.Contains(" ") ? $"\"{input}\"" : input);

        /// <summary>
        /// Set number of times input stream shall be looped. Loop 0 means no loop, loop -1 means infinite loop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        /// <exception cref="InvalidRangeException"></exception>
        public static T StreamLoop<T>(this T t, int number) where T : BaseInput
            => t.SetOptionRange("-stream_loop", number, -1, int.MaxValue);

        /// <summary>
        /// -sseof<br>
        /// </br>Like the -ss option but relative to the "end of file". That is negative values are earlier in the file, 0 is at EOF.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T SsEof<T>(this T t, TimeSpan position) where T : BaseInput
            => t.SetOptionRange("-sseof", position, TimeSpan.MinValue, TimeSpan.Zero);

        /// <summary>
        /// -itsoffset<br>
        /// </br>The offset is added to the timestamps of the input files. Specifying a positive offset means that the corresponding streams are delayed by the time duration specified in offset.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        public static T ItsOffset<T>(this T t, TimeSpan offset) where T : BaseInput
           => t.SetOptionRange("-itsoffset", offset, TimeSpan.Zero, TimeSpan.MaxValue);

        /// <summary>
        /// -re<br></br>
        /// Read input at the native frame rate. Mainly used to simulate a live/realtime input
        /// (e.g. streaming, or testing time-bounded behavior). Often paired with -stream_loop.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Re<T>(this T t) where T : BaseInput
            => t.SetFlag("-re");

        /// <summary>
        /// -hwaccel<br></br>
        /// Use hardware acceleration to decode the matching stream(s) of the input that follows.
        /// The allowed values depend on how the running ffmpeg binary was built.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="hwaccel">A well-known hardware acceleration method.</param>
        /// <returns></returns>
        public static T Hwaccel<T>(this T t, HardwareAccel hwaccel) where T : BaseInput
            => t.SetOption("-hwaccel", hwaccel);

        /// <summary>
        /// -hwaccel<br></br>
        /// Use hardware acceleration to decode the matching stream(s) of the input that follows.
        /// The allowed values depend on how the running ffmpeg binary was built.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="hwaccel">The hardware acceleration method name (e.g. <c>cuda</c>, <c>qsv</c>, <c>vaapi</c>).</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T Hwaccel<T>(this T t, string hwaccel) where T : BaseInput
            => t.SetOption("-hwaccel", hwaccel);

        /// <summary>
        /// -hwaccel_device<br></br>
        /// Select a device to use for hardware acceleration of the input that follows.<br></br>
        /// This option only makes sense when the -hwaccel option is also specified. Its exact meaning
        /// depends on the specific hardware acceleration method chosen.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="device">The hardware device to use (e.g. a device index, a DRM path or an X11 display name).</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T HwaccelDevice<T>(this T t, string device) where T : BaseInput
            => t.SetOption("-hwaccel_device", device);

        /// <summary>
        /// -hwaccel_output_format<br></br>
        /// Select the output (hardware) pixel format for the hardware accelerated decoding of the input
        /// that follows. If not set, the default for the chosen method is used.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="pixelFormat">The hardware pixel format (e.g. <c>cuda</c>, <c>qsv</c>, <c>vaapi</c>).</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T HwaccelOutputFormat<T>(this T t, string pixelFormat) where T : BaseInput
            => t.SetOption("-hwaccel_output_format", pixelFormat);
        #endregion


        #region Output
        /// <summary>
        /// -f<br>
        /// </br>Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static T F<T>(this T t, MuxingFileFormat format) where T : BaseOption, IMux
            => t.SetOption("-f", format.GetEnumAttribute<NameAttribute>().Name);

        /// <summary>
        /// -f<br>
        /// </br>Force input or output file format. The format is normally auto detected for input files and guessed from the file extension for output files, so this option is not needed in most cases.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static T Format<T>(this T t, MuxingFileFormat format) where T : BaseOption, IMux
            => t.SetOption("-f", format.GetEnumAttribute<NameAttribute>().Name);

        /// <summary>
        /// -fs<br>
        /// </br>Set the file size limit, expressed in bytes. No further chunk of bytes is written after the limit is exceeded. The size of the output file is slightly more than the requested file size.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="limit_size"></param>
        /// <returns></returns>
        public static T Fs<T>(this T t, long limit_size) where T : BaseOutput
            => t.SetOptionRange("-fs", limit_size, 0, long.MaxValue);

        /// <summary>
        /// -timestamp<br>
        /// </br>Set the recording timestamp in the container.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static T Timestamp<T>(this T t, DateTime date) where T : BaseOutput
            => t.SetOption("-timestamp", $"\"{date.ToFFmpegDate()}\"");

        /// <summary>
        /// Specify target file type (vcd, svcd, dvd, dv, dv50, .....). type may be prefixed with pal-, ntsc- or film- to use the corresponding standard. All the format options (bitrate, codecs, buffer sizes) are then set automatically.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static T Target<T>(this T t, MuxingFileFormat format) where T : BaseOutput
            => t.SetOption("-target", format.GetEnumAttribute<NameAttribute>().Name);

        /// <summary>
        /// Set the number of data frames to output. This is an obsolete alias for -frames:d, which you should use instead.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="number"></param>
        /// <returns></returns>
        [Obsolete("This is an obsolete alias for -frames:d, which you should use instead.")]
        public static T Dframes<T>(this T t, int number) where T : BaseOutput
            => t.SetOptionRange("-dframes", number, -1, int.MaxValue);
        /// <summary>
        /// -shortest<br></br>
        /// Finish encoding when the shortest output stream ends.<br></br>
        /// Note that this option may require buffering frames, which introduces extra latency.The maximum amount of this latency may be controlled with the -shortest_buf_duration option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Shortest<T>(this T t) where T : BaseOutput
            => t.SetFlag("-shortest");

        /// <summary>
        /// -shortest_buf_duration<br></br>
        /// The -shortest option may require buffering potentially large amounts of data when at least one of the streams is "sparse" (i.e. has large gaps between frames – this is typically the case for subtitles).<br></br>
        /// This option controls the maximum duration of buffered frames in seconds.Larger values may allow the -shortest option to produce more accurate results, but increase memory use and latency.<br></br>
        /// The default value is 10 seconds.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static T ShortestBufDuration<T>(this T t, TimeSpan duration) where T : BaseOutput
            => t.SetOptionRange("-shortest_buf_duration", duration, TimeSpan.Zero, TimeSpan.MaxValue);
        #endregion



        #region InputOutput
        /// <summary>
        /// -t
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static T T<T>(this T t, TimeSpan duration) where T : BaseInputOutput
            => t.SetOptionRange("-t", duration, TimeSpan.Zero, TimeSpan.MaxValue);
        /// <summary>
        /// -t
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static T Duration<T>(this T t, TimeSpan duration) where T : BaseInputOutput
           => t.SetOptionRange("-t", duration, TimeSpan.Zero, TimeSpan.MaxValue);

        /// <summary>
        /// -to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T To<T>(this T t, TimeSpan position) where T : BaseInputOutput
            => t.SetOptionRange("-to", position, TimeSpan.Zero, TimeSpan.MaxValue);
        /// <summary>
        /// -to
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T ToPosition<T>(this T t, TimeSpan position) where T : BaseInputOutput
            => t.SetOptionRange("-to", position, TimeSpan.Zero, TimeSpan.MaxValue);

        /// <summary>
        /// -ss
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T Ss<T>(this T t, TimeSpan position) where T : BaseInputOutput
            => t.SetOptionRange("-ss", position, TimeSpan.Zero, TimeSpan.MaxValue);
        /// <summary>
        /// -ss
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <param name="position"></param>
        /// <returns></returns>
        public static T SsPosition<T>(this T t, TimeSpan position) where T : BaseInputOutput
            => t.SetOptionRange("-ss", position, TimeSpan.Zero, TimeSpan.MaxValue);

        /// <summary>
        /// -dn<br>
        /// </br>As an input option, blocks all data streams of a file from being filtered or being automatically selected or mapped for any output. See -discard option to disable streams individually.<br>
        /// </br>As an output option, disables data recording i.e.automatic selection or mapping of any data stream. For full manual control see the -map option.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Dn<T>(this T t) where T : BaseInputOutput
            => t.SetFlag("-dn");

        /// <summary>
        /// -bitexact<br></br>
        /// Enable bitexact mode for (de)muxer and (de/en)coder
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static T Bitexact<T>(this T t) where T : BaseInputOutput
            => t.SetFlag("-bitexact");
        #endregion
    }
}
