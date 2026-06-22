namespace FFplayArgs
{
    /// <summary>
    /// Configuration for <see cref="FFplayRender"/>.
    /// </summary>
    public class FFplayRenderConfig
    {
        /// <summary>
        /// Default: ffplay
        /// </summary>
        public string FFplayBinaryPath
        {
            get { return _FFplayBinaryPath; }
            set
            {
                if (File.Exists(value)) _FFplayBinaryPath = value;
                else throw new FileNotFoundException(value);
            }
        }
        string _FFplayBinaryPath = "ffplay";

        /// <summary>
        /// Default: False
        /// </summary>
        public bool IsForceUseScript { get; set; } = false;

        /// <summary>
        /// Default: True
        /// </summary>
        public bool IsUseFilterChain { get; set; } = true;

        /// <summary>
        /// Default: FS.txt
        /// </summary>
        public string FilterScriptName
        {
            get { return _FilterScriptName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) throw new ArgumentNullException(nameof(value));
                else _FilterScriptName = value;
            }
        }
        string _FilterScriptName = "FS.txt";

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
        /// Default: ffplay
        /// </summary>
        public FFplayRenderConfig WithFFplayBinaryPath(string filePath)
        {
            if (File.Exists(filePath)) FFplayBinaryPath = filePath;
            else throw new FileNotFoundException(filePath);
            return this;
        }
        /// <summary>
        /// Default: FS.txt
        /// </summary>
        public FFplayRenderConfig WithFilterScriptName(string scriptName, bool forceUseScript = false)
        {
            if (string.IsNullOrWhiteSpace(scriptName)) throw new ArgumentNullException(nameof(scriptName));
            else FilterScriptName = scriptName;
            IsForceUseScript = forceUseScript;
            return this;
        }
        /// <summary>
        /// Default: Directory.GetCurrentDirectory()
        /// </summary>
        public FFplayRenderConfig WithWorkingDirectory(string workingDir)
        {
            if (string.IsNullOrWhiteSpace(workingDir)) throw new ArgumentNullException(nameof(workingDir));
            else WorkingDirectory = workingDir;
            return this;
        }

        /// <summary>
        /// Default: True
        /// </summary>
        /// <param name="isUse"></param>
        /// <returns></returns>
        public FFplayRenderConfig UseFilterChain(bool isUse)
        {
            IsUseFilterChain = isUse;
            return this;
        }
    }
}
