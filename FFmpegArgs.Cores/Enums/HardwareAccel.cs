namespace FFmpegArgs.Cores.Enums
{
    /// <summary>
    /// Well-known values for ffmpeg's <c>-hwaccel</c> option.<br></br>
    /// https://ffmpeg.org/ffmpeg.html#Advanced-Video-options (Hardware acceleration)
    /// </summary>
    /// <remarks>
    /// The set of methods actually available depends on how the running ffmpeg binary was built.
    /// Use the <c>string</c> overload of the <c>Hwaccel</c> extension for any method not listed here.
    /// </remarks>
    public enum HardwareAccel
    {
        /// <summary>
        /// Do not use any hardware acceleration (the default).
        /// </summary>
        none,
        /// <summary>
        /// Automatically select the hardware acceleration method.
        /// </summary>
        auto,
        /// <summary>
        /// Use the NVIDIA CUVID hardware acceleration (CUDA).
        /// </summary>
        cuda,
        /// <summary>
        /// Use DXVA2 (DirectX Video Acceleration) hardware acceleration (Windows).
        /// </summary>
        dxva2,
        /// <summary>
        /// Use D3D11VA (Direct3D 11 Video Acceleration) hardware acceleration (Windows).
        /// </summary>
        d3d11va,
        /// <summary>
        /// Use D3D12VA (Direct3D 12 Video Acceleration) hardware acceleration (Windows).
        /// </summary>
        d3d12va,
        /// <summary>
        /// Use the Intel QuickSync Video acceleration (QSV).
        /// </summary>
        qsv,
        /// <summary>
        /// Use VAAPI (Video Acceleration API) hardware acceleration (Linux).
        /// </summary>
        vaapi,
        /// <summary>
        /// Use VDPAU (Video Decode and Presentation API for Unix) hardware acceleration (Linux).
        /// </summary>
        vdpau,
        /// <summary>
        /// Use VideoToolbox hardware acceleration (Apple).
        /// </summary>
        videotoolbox,
        /// <summary>
        /// Use OpenCL hardware acceleration.
        /// </summary>
        opencl,
        /// <summary>
        /// Use Vulkan hardware acceleration.
        /// </summary>
        vulkan,
    }
}
