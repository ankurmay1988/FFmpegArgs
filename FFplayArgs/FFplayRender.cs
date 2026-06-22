namespace FFplayArgs
{
    /// <summary>
    /// Runs ffplay built from an <see cref="FFplayArg"/> or raw arguments.
    /// </summary>
    public class FFplayRender
    {
        /// <summary>
        ///
        /// </summary>
        public FFplayRenderConfig Config { get; }

        /// <summary>
        ///
        /// </summary>
        public string Arguments { get; }
        /// <summary>
        ///
        /// </summary>
        public IReadOnlyList<string> ArgumentsList { get; }


        private Stream? StdIn { get; set; }
        private bool _isFromFFplayArg = false;

        private FFplayRender(FFplayRenderConfig config, IReadOnlyList<string> argumentList)
        {
            this.Config = config;
            this.ArgumentsList = argumentList;
            this.Arguments = string.Empty;
        }
        private FFplayRender(FFplayRenderConfig config, string arguments)
        {
            this.Config = config;
            this.ArgumentsList = new string[0];
            this.Arguments = arguments;
        }


        private Process BuildProcess(FFplayRenderResult renderResult)
        {
            ProcessStartInfo info = new ProcessStartInfo(this.Config.FFplayBinaryPath)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardInput = this.StdIn != null,
                RedirectStandardError = true,
                WorkingDirectory = this.Config.WorkingDirectory
            };
            if (ArgumentsList.Any())
            {
#if NET5_0_OR_GREATER
                foreach (var item in this.ArgumentsList)
                {
                    info.ArgumentList.Add(item);
                }
                renderResult.ArgumentList = info.ArgumentList;
#else
                string arguments = string.Join(" ", ArgumentsList.Select(x =>
                {
                    if (x.Contains(" ")) return $"\"{x}\"";
                    return x;
                }));

                info.Arguments = arguments;
                renderResult.Arguments = arguments;
#endif
            }
            else
            {
                info.Arguments = this.Arguments;
                renderResult.Arguments = this.Arguments;
            }
            Process process = new Process();
            process.ErrorDataReceived += (s, e) =>
            {
                renderResult._LogDatas.Add(e?.Data ?? string.Empty);
            };
            process.StartInfo = info;
            return process;
        }



        #region SetPipe
        /// <summary>
        /// Set pipe input
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public FFplayRender WithStdInStream(Stream stream)
        {
            if (this.StdIn != null)
                throw new InvalidOperationException("StdIn Stream was setted");

            if (this._isFromFFplayArg)
                throw new InvalidOperationException($"Not allow set pipe to {nameof(FFplayRender)} build from {nameof(FFplayArg)}");

            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            if (!stream.CanRead)
                throw new InvalidOperationException("input stream.CanRead is required");

            this.StdIn = stream;

            return this;
        }
        #endregion



        #region Execute

        /// <summary>
        ///
        /// </summary>
        /// <param name="token">Trigger kill process</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public FFplayRenderResult Execute(CancellationToken token = default)
        {
            FFplayRenderResult renderResult = new FFplayRenderResult();
            using Process process = this.BuildProcess(renderResult);
            if (!process.Start())
            {
                throw new InvalidOperationException("Failed to obtain the handle when starting a process. " +
                    "This could mean that the target executable doesn't exist or that execute permission is missing.");
            }
            using var register = token.Register(() => { try { process.Kill(); } catch { } });
            process.BeginErrorReadLine();
            if (this.StdIn != null)
            {
                this.StdIn.CopyTo(process.StandardInput.BaseStream);
                process.StandardInput.BaseStream.Close();
            }
            process.WaitForExit();
            renderResult.ExitCode = process.ExitCode;
            return renderResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="token">Trigger kill process</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<FFplayRenderResult> ExecuteAsync(CancellationToken token = default)
        {
            FFplayRenderResult renderResult = new FFplayRenderResult();
            using Process process = this.BuildProcess(renderResult);

#if !NET5_0_OR_GREATER
            //https://github.com/Tyrrrz/CliWrap/blob/8ff36a648d57b22497a7cb6feae14ef28bbb2be8/CliWrap/Utils/ProcessEx.cs#L41
            var tcs = new TaskCompletionSource<object?>(TaskCreationOptions.RunContinuationsAsynchronously);
            process.EnableRaisingEvents = true;
            process.Exited += (sender, args) => tcs.TrySetResult(null);
#endif
            if (!process.Start())
            {
                throw new InvalidOperationException("Failed to obtain the handle when starting a process. " +
                    "This could mean that the target executable doesn't exist or that execute permission is missing.");
            }
            using var register = token.Register(() => { try { process.Kill(); } catch { } });
            process.BeginErrorReadLine();
            if (this.StdIn != null)
            {
                await this.StdIn.CopyToAsync(process.StandardInput.BaseStream, 81920, token);
                process.StandardInput.BaseStream.Close();
            }
#if NET5_0_OR_GREATER
            await process.WaitForExitAsync();
#else
            await tcs.Task.ConfigureAwait(false);
#endif
            renderResult.ExitCode = process.ExitCode;
            return renderResult;
        }

        #endregion



        #region Build

        /// <summary>
        ///
        /// </summary>
        /// <param name="ffplayArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArguments(FFplayArg ffplayArg, Action<FFplayRenderConfig> config)
        {
            if (ffplayArg == null) throw new ArgumentNullException(nameof(ffplayArg));
            if (config == null) throw new ArgumentNullException(nameof(config));
            FFplayRenderConfig buildConfig = new FFplayRenderConfig();
            config.Invoke(buildConfig);
            return FromArguments(ffplayArg, buildConfig);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ffplayArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArguments(FFplayArg ffplayArg, FFplayRenderConfig config)
        {
            if (ffplayArg == null) throw new ArgumentNullException(nameof(ffplayArg));
            if (config == null) throw new ArgumentNullException(nameof(config));
            string[] args = ffplayArg.GetFullCommandline(config.IsUseFilterChain).ToArray();
            if (config.IsForceUseScript || (config.ArgumentsMaxLength > 0 && args.Sum(x => x.Length) > config.ArgumentsMaxLength))
            {
                string scripts = ffplayArg.FilterGraph.GetFiltersArgs(true, true);
                if (string.IsNullOrWhiteSpace(scripts)) throw new ProcessArgumentOutOfRangeException($"{nameof(FFplayArg)} argument too long");
                File.WriteAllText(Path.Combine(config.WorkingDirectory, config.FilterScriptName), scripts);
                args = ffplayArg.GetFullCommandlineWithFilterScript(config.FilterScriptName).ToArray();
                if (config.ArgumentsMaxLength > 0 && args.Sum(x => x.Length) > config.ArgumentsMaxLength)
                    throw new ProcessArgumentOutOfRangeException($"{nameof(FFplayArg)} argument too long");
            }
            FFplayRender ffplayBuild = new FFplayRender(config, args);
            ffplayBuild._isFromFFplayArg = true;
            ffplayBuild.StdIn = ffplayArg.Inputs.FirstOrDefault(x => x.PipeStream != null)?.PipeStream;
            return ffplayBuild;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArguments(string commands, FFplayRenderConfig config)
        {
            if (string.IsNullOrWhiteSpace(commands)) throw new ArgumentNullException(nameof(commands));
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.ArgumentsMaxLength > 0 && commands.Length > config.ArgumentsMaxLength)
                throw new ProcessArgumentOutOfRangeException($"{nameof(commands)} too long");
            return new FFplayRender(config, commands);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArguments(string commands, Action<FFplayRenderConfig> config)
        {
            if (string.IsNullOrWhiteSpace(commands)) throw new ArgumentNullException(nameof(commands));
            if (config == null) throw new ArgumentNullException(nameof(config));
            FFplayRenderConfig buildConfig = new FFplayRenderConfig();
            config.Invoke(buildConfig);
            return FromArguments(commands, buildConfig);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArgumentsList(FFplayRenderConfig config, params string[] args)
        {
            if (args is null || args.Length == 0 || args.Any(x => x is null)) throw new ArgumentNullException(nameof(args));
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (config.ArgumentsMaxLength > 0 && args.Sum(x => x.Length + 1) > config.ArgumentsMaxLength)
                throw new ProcessArgumentOutOfRangeException($"{nameof(args)} too long");
            return new FFplayRender(config, args);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FFplayRender FromArgumentsList(FFplayRenderConfig config, IEnumerable<string> args)
            => FromArgumentsList(config, args.ToArray());

        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFplayRender FromArgumentsList(Action<FFplayRenderConfig> config, params string[] args)
        {
            FFplayRenderConfig buildConfig = new FFplayRenderConfig();
            config.Invoke(buildConfig);
            return FromArgumentsList(buildConfig, args);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FFplayRender FromArgumentsList(Action<FFplayRenderConfig> config, IEnumerable<string> args)
            => FromArgumentsList(config, args.ToArray());
        #endregion
    }
}
