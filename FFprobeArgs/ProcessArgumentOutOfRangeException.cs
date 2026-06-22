namespace FFprobeArgs
{
    /// <summary>
    /// Thrown when the built ffprobe arguments exceed the allowed length.
    /// </summary>
    public class ProcessArgumentOutOfRangeException : Exception
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        public ProcessArgumentOutOfRangeException(string message) : base(message)
        {
        }
    }
}
