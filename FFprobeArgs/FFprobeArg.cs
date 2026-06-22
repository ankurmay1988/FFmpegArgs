namespace FFprobeArgs
{
    /// <summary>
    /// Builds an ffprobe argument token list.
    /// <para>
    /// Tokens are RAW (not shell-quoted). Quoting is handled by the renderer via
    /// <see cref="System.Diagnostics.ProcessStartInfo"/>.ArgumentList. Do not wrap tokens in quotes manually.
    /// </para>
    /// </summary>
    public class FFprobeArg
    {
        string? _input;
        bool _inputAsOption = true;

        bool _hideBanner;
        string? _logLevel;
        FFprobePrintFormat? _printFormat;
        bool _showFormat;
        bool _showStreams;
        bool _showPackets;
        bool _showFrames;
        bool _showPrograms;
        bool _showChapters;
        bool _showError;
        bool _showData;
        bool _showPrivateData;
        bool _countFrames;
        bool _countPackets;
        string? _selectStreams;
        string? _showEntries;

        readonly List<string> _extraOptions = new List<string>();

        /// <summary>
        /// The configured input url/path (without the <c>-i</c> flag), or <see langword="null"/> if not set.
        /// </summary>
        public string? Input => _input;

        #region Input
        /// <summary>
        /// Set the input url/path. By default it is emitted as <c>-i &lt;url&gt;</c>.
        /// </summary>
        /// <param name="url">Input url or file path (RAW, do not quote).</param>
        /// <param name="asOption">
        /// When <see langword="true"/> (default) emit <c>-i &lt;url&gt;</c>; otherwise emit the url as a trailing positional argument.
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FFprobeArg SetInput(string url, bool asOption = true)
        {
            if (string.IsNullOrWhiteSpace(url)) throw new ArgumentNullException(nameof(url));
            _input = url;
            _inputAsOption = asOption;
            return this;
        }
        #endregion

        #region Global / logging
        /// <summary>
        /// Add <c>-hide_banner</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg HideBanner(bool enable = true)
        {
            _hideBanner = enable;
            return this;
        }

        /// <summary>
        /// Set the log level, emitted as <c>-loglevel &lt;level&gt;</c> (e.g. quiet, error, warning, info, verbose, debug).
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FFprobeArg LogLevel(string level)
        {
            if (string.IsNullOrWhiteSpace(level)) throw new ArgumentNullException(nameof(level));
            _logLevel = level;
            return this;
        }
        #endregion

        #region Print format
        /// <summary>
        /// Set the writer/print format, emitted as <c>-print_format &lt;format&gt;</c>.
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public FFprobeArg PrintFormat(FFprobePrintFormat format)
        {
            _printFormat = format;
            return this;
        }
        #endregion

        #region Show sections
        /// <summary>
        /// Add <c>-show_format</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowFormat(bool enable = true)
        {
            _showFormat = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_streams</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowStreams(bool enable = true)
        {
            _showStreams = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_packets</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowPackets(bool enable = true)
        {
            _showPackets = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_frames</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowFrames(bool enable = true)
        {
            _showFrames = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_programs</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowPrograms(bool enable = true)
        {
            _showPrograms = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_chapters</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowChapters(bool enable = true)
        {
            _showChapters = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_error</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowError(bool enable = true)
        {
            _showError = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_data</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowData(bool enable = true)
        {
            _showData = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-show_private_data</c> (alias <c>-private</c> in ffprobe; uses long form here).
        /// </summary>
        /// <returns></returns>
        public FFprobeArg ShowPrivateData(bool enable = true)
        {
            _showPrivateData = enable;
            return this;
        }
        #endregion

        #region Counting
        /// <summary>
        /// Add <c>-count_frames</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg CountFrames(bool enable = true)
        {
            _countFrames = enable;
            return this;
        }

        /// <summary>
        /// Add <c>-count_packets</c>.
        /// </summary>
        /// <returns></returns>
        public FFprobeArg CountPackets(bool enable = true)
        {
            _countPackets = enable;
            return this;
        }
        #endregion

        #region Selection / entries
        /// <summary>
        /// Set the stream specifier, emitted as <c>-select_streams &lt;spec&gt;</c> (e.g. <c>v:0</c>, <c>a</c>, <c>v</c>).
        /// </summary>
        /// <param name="streamSpecifier"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FFprobeArg SelectStreams(string streamSpecifier)
        {
            if (string.IsNullOrWhiteSpace(streamSpecifier)) throw new ArgumentNullException(nameof(streamSpecifier));
            _selectStreams = streamSpecifier;
            return this;
        }

        /// <summary>
        /// Set the entries filter, emitted as <c>-show_entries &lt;entries&gt;</c>
        /// (e.g. <c>stream=index,codec_type</c>, <c>format=duration:stream=codec_name</c>).
        /// </summary>
        /// <param name="entries"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FFprobeArg ShowEntries(string entries)
        {
            if (string.IsNullOrWhiteSpace(entries)) throw new ArgumentNullException(nameof(entries));
            _showEntries = entries;
            return this;
        }
        #endregion

        #region Extra
        /// <summary>
        /// Append a raw extra option token (e.g. a flag) that is not covered by a dedicated fluent method.
        /// Tokens are emitted verbatim (RAW, do not quote), after the dedicated options and before the input.
        /// </summary>
        /// <param name="tokens"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public FFprobeArg AddExtraOption(params string[] tokens)
        {
            if (tokens == null || tokens.Length == 0 || tokens.Any(x => x == null))
                throw new ArgumentNullException(nameof(tokens));
            _extraOptions.AddRange(tokens);
            return this;
        }
        #endregion

        /// <summary>
        /// Build the full ffprobe argument token list. Tokens are RAW (not shell-quoted).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetAllArgs()
        {
            List<string> args = new List<string>();

            if (_hideBanner) args.Add("-hide_banner");

            if (!string.IsNullOrWhiteSpace(_logLevel))
            {
                args.Add("-loglevel");
                args.Add(_logLevel!);
            }

            if (_printFormat.HasValue)
            {
                args.Add("-print_format");
                args.Add(_printFormat.Value.ToFFprobeName());
            }

            if (_selectStreams != null)
            {
                args.Add("-select_streams");
                args.Add(_selectStreams);
            }

            if (_showFormat) args.Add("-show_format");
            if (_showStreams) args.Add("-show_streams");
            if (_showPackets) args.Add("-show_packets");
            if (_showFrames) args.Add("-show_frames");
            if (_showPrograms) args.Add("-show_programs");
            if (_showChapters) args.Add("-show_chapters");
            if (_showError) args.Add("-show_error");
            if (_showData) args.Add("-show_data");
            if (_showPrivateData) args.Add("-show_private_data");
            if (_countFrames) args.Add("-count_frames");
            if (_countPackets) args.Add("-count_packets");

            if (_showEntries != null)
            {
                args.Add("-show_entries");
                args.Add(_showEntries);
            }

            args.AddRange(_extraOptions);

            if (!string.IsNullOrWhiteSpace(_input))
            {
                if (_inputAsOption) args.Add("-i");
                args.Add(_input!);
            }

            return args;
        }
    }
}
