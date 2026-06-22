# FFmpegArgs — Examples by feature

Copy‑pasteable C# snippets grouped by feature. See [README.MD](../README.MD) for installation and the
big picture. Every snippet below only *builds the argument string*; add a `Render(...)` call (see
[Execute](#execute-run-ffmpeg--ffplay)) to actually run ffmpeg/ffplay.

> Numbers are always formatted with `InvariantCulture` internally, so you can build commands on any
> machine culture (e.g. `vi-VN`, `de-DE`) without getting `0,5` instead of `0.5`. You no longer need a
> manual `CultureScope` around argument building.

## Table of contents
- [Filter graph](#filter-graph)
- [lavfi string input](#lavfi-string-input)
- [Sink filters & auto‑sink](#sink-filters--auto-sink)
- [Audio encoders](#audio-encoders)
- [Hardware video encoders](#hardware-video-encoders)
- [Muxer / demuxer options](#muxer--demuxer-options)
- [Subtitles](#subtitles)
- [Pipes (stdin / stdout)](#pipes-stdin--stdout)
- [Execute: run ffmpeg / ffplay](#execute-run-ffmpeg--ffplay)
- [Progress & cancellation](#progress--cancellation)

---

## Filter graph

```csharp
using FFmpegArgs;
using FFmpegArgs.Filters.VideoSources;   // NullsrcFilter, ColorFilter
using FFmpegArgs.Filters.VideoFilters;   // ScaleFilter, FpsFilter

FFmpegArg ffmpegArg = new FFmpegArg().OverWriteOutput();

ImageFilterGraphInput filterInput = new ImageFilterGraphInput();
filterInput.FilterGraph
    .ColorFilter().Color(Color.Red).Size(new Size(1280, 720)).MapOut
    .FpsFilter().Fps(25);

ImageMap image = ffmpegArg.AddImagesInput(filterInput).First();
ImageFileOutput output = new ImageFileOutput("out.mp4", image).Duration(TimeSpan.FromSeconds(5));
ffmpegArg.AddOutput(output);

string commandline = string.Join(" ", ffmpegArg.GetFullCommandline());
```

## lavfi string input

Use a raw filtergraph string as an input (`-f lavfi -i "..."`):

```csharp
using FFmpegArgs.Inputs;

FFmpegArg ffmpegArg = new FFmpegArg();
FilterStringInput input = new FilterStringInput("color=c=red:s=1280x720");
ffmpegArg.AddInput(input);
// -> -f lavfi -i color=c=red:s=1280x720   (emitted as a single raw token; quoting is handled by the runner)
```

## Sink filters & auto‑sink

A *sink* terminates a branch that you do not want in the output (`V->|` / `A->|`):

```csharp
using FFmpegArgs.Filters.VideoSinks;   // NullsinkFilter
using FFmpegArgs.Filters.AudioSinks;   // AnullsinkFilter

filterGraph.NullsrcFilter().Size(new Size(1280, 720)).MapOut.NullsinkFilter();
// -> nullsrc=s=1280x720,nullsink
```

Or let the graph auto‑attach a sink to every dangling map‑out instead of throwing:

```csharp
FilterGraph graph = new FilterGraph { AutoSinkUnusedMapOut = true };
graph.NullsrcFilter().Size(new Size(320, 240));   // dangling -> auto nullsink
graph.AnullsrcFilter();                            // dangling -> auto anullsink
// -> nullsrc=s=320x240,nullsink;anullsrc,anullsink
```

`AutoSinkUnusedMapOut` only touches *unused* map‑outs; real outputs are never sunk. The command still
needs at least one real output (otherwise `GetOutputsArgs` throws `"Output is empty"`).

## Audio encoders

Dedicated wrappers (parallel to the video encoders) — `aac`, `libmp3lame`, `ac3`, `eac3`, `flac`,
`alac`, `libopus`, `libvorbis`:

```csharp
using FFmpegArgs.Codec.Encoders.Audios;

audioOutputStream.Aac_Codec(e => e.AacCoder(Aac_Coder.fast).AacTns(true));
// -> -c:a:0 aac -aac_coder fast -aac_tns true

audioOutputStream.Libopus_Codec(e => e.Fec(true).PacketLoss(10));
audioOutputStream.Flac_Codec(e => e.MinPartitionOrder(0).MaxPartitionOrder(8));
```

## Hardware video encoders

```csharp
using FFmpegArgs.Codec.Encoders.Images;

imageOutputStream.Hevc_nvenc_Codec(e => e
    .Preset(Hevc_nvenc_Preset.p5)
    .Tune(Hevc_nvenc_Tune.hq)
    .RateControl(Hevc_nvenc_RateControl.vbr));
// -> -c:v:0 hevc_nvenc -preset p5 -tune hq -rc vbr
```
Also available: `Av1_nvenc/amf/qsv/vaapi`, `Hevc_amf/mf/qsv/vaapi`, `H264_*`, software `IH264_libx264`,
`IHevc_libx265`, `ILibaomAv1`, `Librav1e`.

## Muxer / demuxer options

```csharp
// Muxer (output): mp4/mov flags, additive +flag form
output.MovFlags(MovFlag.faststart, MovFlag.empty_moov);
// -> -movflags +faststart+empty_moov

// Demuxer (input)
input.Re();                                   // -re  (read at native rate / simulate realtime)
input.StartNumber(5);                         // -start_number 5   (image2)
input.PatternType(Image2PatternType.glob);    // -pattern_type glob (image2)
```

## Subtitles

Burn‑in (render onto video) with the `subtitles` / `ass` video filters:

```csharp
using FFmpegArgs.Filters.VideoFilters;

imageMap.SubtitlesFilter().Charenc("UTF-8");   // subtitles=...:charenc=UTF-8
imageMap.AssFilter();                          // ass=...
```

Subtitle stream codec / input charset:

```csharp
output.Scodec("mov_text");    // -c:s mov_text
output.CopySubtitle();        // -c:s copy
input.SubCharenc("CP1252");   // -sub_charenc CP1252
```

## Pipes (stdin / stdout)

```csharp
using FFmpegArgs.Inputs;
using FFmpegArgs.Outputs;

FFmpegArg ffmpegArg = new FFmpegArg();
ImagePipeInput pipeIn = new ImagePipeInput(inputStream, DemuxingFileFormat.rawvideo);
ImageMap image = ffmpegArg.AddImagesInput(pipeIn).First();
ImagePipeOutput pipeOut = new ImagePipeOutput(outputStream, MuxingFileFormat.rawvideo, image);
ffmpegArg.AddOutput(pipeOut);
// FromArguments wires the pipe streams to the process stdin/stdout automatically.
```

## Execute: run ffmpeg / ffplay

```csharp
using FFmpegArgs.Executes;

// ffmpeg (default config resolves "ffmpeg" from PATH)
FFmpegRenderResult result = ffmpegArg
    .Render(b => b.WithFilterScriptName("fs.txt").UseFilterChain(true))
    .Execute();
if (result.ExitCode != 0) { /* inspect result error data */ }
```

```csharp
using FFplayArgs;

// ffplay (self-contained FFplayRender in the FFplayArgs package)
FFplayArg ffplayArg = new FFplayArg();
// ... add inputs / filtergraph ...
FFplayRenderResult played = FFplayRender.FromArguments(ffplayArg, new FFplayRenderConfig()).Execute();

// or from raw arguments
FFplayRender.FromArgumentsList(new FFplayRenderConfig(),
    "-nodisp", "-autoexit", "-f", "lavfi", "-i", "sine=duration=0.3").Execute();
```

## Progress & cancellation

```csharp
using System.Threading;

using CancellationTokenSource cts = new CancellationTokenSource();
cts.CancelAfter(TimeSpan.FromSeconds(10));   // cancel -> the ffmpeg process is killed

FFmpegRenderResult result = ffmpegArg
    .Render()
    .Execute(progress =>
    {
        Console.WriteLine($"frame={progress.Frame} time={progress.Time} speed={progress.Speed}");
    }, cts.Token);
```

`Execute`/`ExecuteAsync` accept a `CancellationToken`; cancelling kills the underlying ffmpeg/ffplay
process and returns promptly (the result's `ExitCode` will be non‑zero).
