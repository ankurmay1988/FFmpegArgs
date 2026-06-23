namespace FFmpegArgs
{
    /// <summary>
    /// Opt-in, read-only validation for a fully-constructed <see cref="IFFmpegArg"/> (e.g. <c>FFmpegArg</c>).
    /// </summary>
    /// <remarks>
    /// <para>
    /// Nothing here runs automatically: <c>Render</c>/<c>GetAllArgs</c>/<c>GetFullCommandline</c>
    /// are completely unaffected. The caller must explicitly call <see cref="Validate(FFmpegArg)"/>.
    /// The validator never mutates the graph and never changes when/whether building throws — it only
    /// <em>reads</em> the already-built graph and returns a list of <see cref="GraphValidationIssue"/>.
    /// </para>
    /// <para>
    /// Several classes of error are intentionally <strong>not</strong> checked here because the core API
    /// already prevents them earlier, before this validator could ever run:
    /// </para>
    /// <list type="bullet">
    ///   <item>
    ///     <description>
    ///     <b>Audio/video type mismatch</b> is a compile-time error. Filters are
    ///     <c>BaseFilter&lt;TIn,TOut&gt;</c> over <c>ImageMap</c>/<c>AudioMap</c> and the fluent
    ///     extensions are typed (e.g. <c>this ImageMap</c>), so an <c>AudioMap</c> cannot be fed to a
    ///     video filter.
    ///     </description>
    ///   </item>
    ///   <item>
    ///     <description>
    ///     <b>Re-using a <em>filter-output</em> map twice without a split</b> is a construction-time
    ///     throw: <c>BaseFilter</c>/<c>BaseOutput</c> reject a non-input map that is already mapped
    ///     ("Map is only one to one, except input"). So by the time you hold a fully-built graph, that
    ///     mistake cannot exist. (Re-using an <em>input</em> map IS constructible and IS checked below.)
    ///     </description>
    ///   </item>
    /// </list>
    /// </remarks>
    public static class GraphValidationExtension
    {
        /// <summary>
        /// Inspect <paramref name="arg"/> read-only and return any issues found. Returns an empty list
        /// for a valid graph. This does not throw for an invalid graph and does not change build behavior.
        /// </summary>
        /// <param name="arg">The fully-constructed argument to inspect.</param>
        /// <returns>Read-only list of issues, ordered by severity (most severe first), then by code.</returns>
        /// <exception cref="ArgumentNullException"><paramref name="arg"/> is null.</exception>
        public static IReadOnlyList<GraphValidationIssue> Validate(this IFFmpegArg arg)
        {
            if (arg == null) throw new ArgumentNullException(nameof(arg));

            List<GraphValidationIssue> issues = new List<GraphValidationIssue>();

            // Snapshot — never mutate anything.
            List<BaseInput> inputs = arg.Inputs.ToList();
            List<BaseOutput> outputs = arg.Outputs.ToList();
            List<IFilter> filters = arg.FilterGraph.Filters.ToList();

            ValidateInputsOutputs(inputs, outputs, issues);
            ValidateInputMapReuse(filters, issues);
            ValidateDanglingFilterOutputs(arg, filters, issues);

            return issues
                .OrderByDescending(x => x.Severity)
                .ThenBy(x => x.Code, StringComparer.Ordinal)
                .ToList();
        }

        /// <summary>
        /// No input / no output. Building the command throws "Input is empty" / "Output is empty",
        /// so flag these as errors (read-only, ahead of the throw).
        /// </summary>
        static void ValidateInputsOutputs(
            List<BaseInput> inputs,
            List<BaseOutput> outputs,
            List<GraphValidationIssue> issues)
        {
            if (inputs.Count == 0)
            {
                issues.Add(new GraphValidationIssue(
                    GraphValidationSeverity.Error,
                    "NoInput",
                    "No input was added. Building the command throws \"Input is empty\"."));
            }

            if (outputs.Count == 0)
            {
                issues.Add(new GraphValidationIssue(
                    GraphValidationSeverity.Error,
                    "NoOutput",
                    "No output was added. Building the command throws \"Output is empty\"."));
            }
        }

        /// <summary>
        /// An INPUT map (a labeled input stream such as <c>0:v:0</c>) fed to more than one consumer.
        /// Unlike a filter-output map, the core API allows this at construction time, but ffmpeg does
        /// not let a single filtergraph input label be consumed twice — a <c>split</c>/<c>asplit</c> is
        /// required. This is the highest-value runtime mistake observable read-only post-build.
        /// </summary>
        static void ValidateInputMapReuse(
            List<IFilter> filters,
            List<GraphValidationIssue> issues)
        {
            // All consumptions of any map by a filter input.
            List<BaseMap> allMapsIn = filters.SelectMany(x => x.MapsIn).ToList();

            // Group input-maps by their stable MapName; >1 consumption == reuse.
            IEnumerable<IGrouping<string, BaseMap>> reusedInputMaps = allMapsIn
                .Where(x => x.IsInput && !string.IsNullOrWhiteSpace(x.MapName))
                .GroupBy(x => x.MapName)
                .Where(g => g.Count() > 1);

            foreach (IGrouping<string, BaseMap> group in reusedInputMaps)
            {
                int count = group.Count();
                issues.Add(new GraphValidationIssue(
                    GraphValidationSeverity.Warning,
                    "InputMapReusedWithoutSplit",
                    $"Input map [{group.Key}] is consumed by {count} filter inputs. " +
                    "ffmpeg does not allow re-using a filtergraph input label; insert a split/asplit filter.",
                    group.Key));
            }
        }

        /// <summary>
        /// Filter outputs that are never consumed (not fed to another filter and not mapped to a real
        /// output stream). With <c>AutoSinkUnusedMapOut == false</c> (the default) building throws
        /// "are not bind"; with auto-sink on, ffmpeg appends a nullsink/anullsink. Either way it is
        /// worth surfacing. Severity depends on whether auto-sink is enabled.
        /// </summary>
        static void ValidateDanglingFilterOutputs(
            IFFmpegArg arg,
            List<IFilter> filters,
            List<GraphValidationIssue> issues)
        {
            bool autoSink = arg.FilterGraph.AutoSinkUnusedMapOut;

            foreach (IFilter filter in filters)
            {
                foreach (BaseMap mapOut in filter.MapsOut)
                {
                    if (mapOut.IsMapped) continue;

                    if (autoSink)
                    {
                        issues.Add(new GraphValidationIssue(
                            GraphValidationSeverity.Info,
                            "DanglingFilterOutputAutoSinked",
                            $"Filter \"{filter.FilterName}\" has an unused output [{mapOut.MapName}]. " +
                            "AutoSinkUnusedMapOut is enabled, so a null sink will be appended automatically.",
                            mapOut.MapName));
                    }
                    else
                    {
                        issues.Add(new GraphValidationIssue(
                            GraphValidationSeverity.Error,
                            "DanglingFilterOutput",
                            $"Filter \"{filter.FilterName}\" has an unused output [{mapOut.MapName}]. " +
                            "Building the filtergraph throws \"are not bind\". Map it to an output, " +
                            "feed it to another filter, add a sink, or enable AutoSinkUnusedMapOut.",
                            mapOut.MapName));
                    }
                }
            }
        }
    }
}
