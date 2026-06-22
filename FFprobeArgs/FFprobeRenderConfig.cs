namespace FFprobeArgs
{
    /// <summary>
    /// Configuration for <see cref="FFprobeRender"/>.
    /// </summary>
    public class FFprobeRenderConfig
    {
        /// <summary>
        /// Default: ffprobe
        /// </summary>
        public string FFprobeBinaryPath
        {
            get { return _FFprobeBinaryPath; }
            set
            {
                if (File.Exists(value)) _FFprobeBinaryPath = value;
                else throw new FileNotFoundException(value);
            }
        }
        string _FFprobeBinaryPath = "ffprobe";

        /// <summary>
        /// Default: Directory.GetCurrentDirectory()
        /// </summary>
        public string WorkingDirectory { get; set; } = Directory.GetCurrentDirectory();

        /// <summary>
        /// Window default: 32766<br></br>
        /// default: -1
        /// </summary>
        public int ArgumentsMaxLength
        {
            get { return _ArgumentsMaxLength; }
            set
            {
                if (value > 10) _ArgumentsMaxLength = value;
                else throw new InvalidDataException($"ArgumentsMaxLength should be > 10");
            }
        }
        int _ArgumentsMaxLength = -1;


        /// <summary>
        /// Default: ffprobe
        /// </summary>
        public FFprobeRenderConfig WithFFprobeBinaryPath(string filePath)
        {
            if (File.Exists(filePath)) FFprobeBinaryPath = filePath;
            else throw new FileNotFoundException(filePath);
            return this;
        }

        /// <summary>
        /// Default: Directory.GetCurrentDirectory()
        /// </summary>
        public FFprobeRenderConfig WithWorkingDirectory(string workingDir)
        {
            if (string.IsNullOrWhiteSpace(workingDir)) throw new ArgumentNullException(nameof(workingDir));
            else WorkingDirectory = workingDir;
            return this;
        }
    }
}
