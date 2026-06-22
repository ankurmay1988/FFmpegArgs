namespace FFprobeArgs
{
    /// <summary>
    /// Runs ffprobe built from an <see cref="FFprobeArg"/> or raw arguments.
    /// </summary>
    public class FFprobeRender
    {
        /// <summary>
        ///
        /// </summary>
        public FFprobeRenderConfig Config { get; }

        /// <summary>
        ///
        /// </summary>
        public string Arguments { get; }
        /// <summary>
        ///
        /// </summary>
        public IReadOnlyList<string> ArgumentsList { get; }

        private FFprobeRender(FFprobeRenderConfig config, IReadOnlyList<string> argumentList)
        {
            this.Config = config;
            this.ArgumentsList = argumentList;
            this.Arguments = string.Empty;
        }
        private FFprobeRender(FFprobeRenderConfig config, string arguments)
        {
            this.Config = config;
            this.ArgumentsList = new string[0];
            this.Arguments = arguments;
        }


        private Process BuildProcess(FFprobeRenderResult renderResult)
        {
            ProcessStartInfo info = new ProcessStartInfo(this.Config.FFprobeBinaryPath)
            {
                CreateNoWindow = true,
                UseShellExecute = false,
                RedirectStandardOutput = true,
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
                renderResult.ArgumentList = info.ArgumentList.ToArray();
#else
                string arguments = string.Join(" ", ArgumentsList.Select(x =>
                {
                    if (x.Contains(" ")) return $"\"{x}\"";
                    return x;
                }));

                info.Arguments = arguments;
                renderResult.Arguments = arguments;
                renderResult.ArgumentList = ArgumentsList.ToArray();
#endif
            }
            else
            {
                info.Arguments = this.Arguments;
                renderResult.Arguments = this.Arguments;
            }
            Process process = new Process();
            process.OutputDataReceived += (s, e) =>
            {
                if (e?.Data != null) renderResult._StdOutDatas.Add(e.Data);
            };
            process.ErrorDataReceived += (s, e) =>
            {
                if (e?.Data != null) renderResult._LogDatas.Add(e.Data);
            };
            process.StartInfo = info;
            return process;
        }



        #region Execute

        /// <summary>
        ///
        /// </summary>
        /// <param name="token">Trigger kill process</param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public FFprobeRenderResult Execute(CancellationToken token = default)
        {
            FFprobeRenderResult renderResult = new FFprobeRenderResult();
            using Process process = this.BuildProcess(renderResult);
            if (!process.Start())
            {
                throw new InvalidOperationException("Failed to obtain the handle when starting a process. " +
                    "This could mean that the target executable doesn't exist or that execute permission is missing.");
            }
            using var register = token.Register(() => { try { process.Kill(); } catch { } });
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
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
        public async Task<FFprobeRenderResult> ExecuteAsync(CancellationToken token = default)
        {
            FFprobeRenderResult renderResult = new FFprobeRenderResult();
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
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();
#if NET5_0_OR_GREATER
            await process.WaitForExitAsync(token);
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
        /// <param name="ffprobeArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFprobeRender FromArguments(FFprobeArg ffprobeArg, Action<FFprobeRenderConfig> config)
        {
            if (ffprobeArg == null) throw new ArgumentNullException(nameof(ffprobeArg));
            if (config == null) throw new ArgumentNullException(nameof(config));
            FFprobeRenderConfig buildConfig = new FFprobeRenderConfig();
            config.Invoke(buildConfig);
            return FromArguments(ffprobeArg, buildConfig);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ffprobeArg"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFprobeRender FromArguments(FFprobeArg ffprobeArg, FFprobeRenderConfig config)
        {
            if (ffprobeArg == null) throw new ArgumentNullException(nameof(ffprobeArg));
            if (config == null) throw new ArgumentNullException(nameof(config));
            string[] args = ffprobeArg.GetAllArgs().ToArray();
            if (config.ArgumentsMaxLength > 0 && args.Sum(x => x.Length + 1) > config.ArgumentsMaxLength)
                throw new ProcessArgumentOutOfRangeException($"{nameof(FFprobeArg)} argument too long");
            return new FFprobeRender(config, args);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFprobeRender FromArguments(string commands, FFprobeRenderConfig config)
        {
            if (string.IsNullOrWhiteSpace(commands)) throw new ArgumentNullException(nameof(commands));
            if (config == null) throw new ArgumentNullException(nameof(config));
            if (config.ArgumentsMaxLength > 0 && commands.Length > config.ArgumentsMaxLength)
                throw new ProcessArgumentOutOfRangeException($"{nameof(commands)} too long");
            return new FFprobeRender(config, commands);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="commands"></param>
        /// <param name="config"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFprobeRender FromArguments(string commands, Action<FFprobeRenderConfig> config)
        {
            if (string.IsNullOrWhiteSpace(commands)) throw new ArgumentNullException(nameof(commands));
            if (config == null) throw new ArgumentNullException(nameof(config));
            FFprobeRenderConfig buildConfig = new FFprobeRenderConfig();
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
        public static FFprobeRender FromArgumentsList(FFprobeRenderConfig config, params string[] args)
        {
            if (args is null || args.Length == 0 || args.Any(x => x is null)) throw new ArgumentNullException(nameof(args));
            if (config is null) throw new ArgumentNullException(nameof(config));
            if (config.ArgumentsMaxLength > 0 && args.Sum(x => x.Length + 1) > config.ArgumentsMaxLength)
                throw new ProcessArgumentOutOfRangeException($"{nameof(args)} too long");
            return new FFprobeRender(config, args);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FFprobeRender FromArgumentsList(FFprobeRenderConfig config, IEnumerable<string> args)
            => FromArgumentsList(config, args.ToArray());

        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ProcessArgumentOutOfRangeException"></exception>
        public static FFprobeRender FromArgumentsList(Action<FFprobeRenderConfig> config, params string[] args)
        {
            FFprobeRenderConfig buildConfig = new FFprobeRenderConfig();
            config.Invoke(buildConfig);
            return FromArgumentsList(buildConfig, args);
        }
        /// <summary>
        ///
        /// </summary>
        /// <param name="config"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static FFprobeRender FromArgumentsList(Action<FFprobeRenderConfig> config, IEnumerable<string> args)
            => FromArgumentsList(config, args.ToArray());
        #endregion
    }
}
