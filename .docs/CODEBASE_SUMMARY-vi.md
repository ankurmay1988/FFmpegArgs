# FFmpegArgs — Tóm tắt Codebase

> Tài liệu sinh tự động từ việc đọc codebase (cập nhật: 2026-06-22).
> Phiên bản hiện tại: **2.2** (`AssemblyVersion 2.1`, `FileVersion 2.2.0.0`).
> Link code dùng đường dẫn tương đối từ thư mục `.docs/` (prefix `../`).

---

## 1. Tổng quan

**FFmpegArgs** là một thư viện C# kiểu *CLI-wrapper*: nó **sinh chuỗi argument** cho file thực thi `ffmpeg`/`ffplay` bằng một fluent API type-safe, rồi (tùy chọn) chạy tiến trình ffmpeg và đọc tiến độ encode.

Điểm nổi bật so với các wrapper khác: hệ thống **FilterGraph** mô hình hóa `-filter_complex` của ffmpeg — cho phép nối filter dạng chuỗi/đồ thị, tự sinh tên map (`[0:v:0]`, `[f_3]`...) và tự escape ký tự đặc biệt.

- Hỗ trợ input/output dạng **file / url / pipe (stdin/stdout)**.
- Hoạt động trên mọi hệ điều hành nếu chạy được `ffmpeg`.
- **~230 filter viết tay** + **470 filter auto-generate** ≈ **700 filter**.
- Không phụ thuộc NuGet bên ngoài (chỉ dùng BCL).

### Multi-targeting & ngôn ngữ
Cấu hình chung tại [ProjectBuildProperties.targets](../ProjectBuildProperties.targets):
- `TargetFrameworks`: **netstandard2.0; net6.0; net8.0**
- `LangVersion`: **12.0**, `Nullable`: **enable**
- `GenerateDocumentationFile`: true, tắt cảnh báo `CS1591`.

---

## 2. Bản đồ Solution (25 project code)

File giải pháp: [FFmpegArgs.sln](../FFmpegArgs.sln)

| Nhóm | Project | Vai trò |
|------|---------|---------|
| **Lõi** | `FFmpegArgs.Cores` | Class/interface nền tảng: Option, Map, Stream, Input/Output base, **FilterGraph**, enums, escaping. |
| | `FFmpegArgs` | Entry point: class **`FFmpegArg`** — gom input/output/filtergraph, sinh full commandline. |
| | `FFmpegArgs.Extensions` | Extension method fluent cho global/input/output/per-stream options. |
| **I/O** | `FFmpegArgs.Inputs` | Các loại input cụ thể (File/Url/Pipe/Concat cho Video/Audio/Image). |
| | `FFmpegArgs.Outputs` | Các loại output cụ thể. |
| | `FFmpegArgs.Inputs.Demuxers` | **Typed demuxer** (merge từ `features/mux-demux`): [BaseDemuxer](../FFmpegArgs.Inputs.Demuxers/BaseDemuxer.cs) + 4 ImageDemuxer ([Apng](../FFmpegArgs.Inputs.Demuxers/ImageDemuxers/ApngDemuxer.cs)/[Asf](../FFmpegArgs.Inputs.Demuxers/ImageDemuxers/AsfDemuxer.cs)/[Dash](../FFmpegArgs.Inputs.Demuxers/ImageDemuxers/DashDemuxer.cs)/[Rawvideo](../FFmpegArgs.Inputs.Demuxers/ImageDemuxers/RawvideoDemuxer.cs)) + `DemuxerExtensions`. |
| | `FFmpegArgs.Outputs.Muxers` | **Typed muxer** (merge từ `features/mux-demux`): hiện mới có [BaseMuxer](../FFmpegArgs.Outputs.Muxers/BaseMuxer.cs) skeleton (chưa có muxer cụ thể; lưu ý namespace đang sai → xem mục 9). |
| **Thực thi** | `FFmpegArgs.Executes` | `FFmpegRender`, `FFmpegRenderConfig`, `FFmpegRenderResult`, `RenderProgress` — chạy ffmpeg, đọc tiến độ. |
| **Codec** | `FFmpegArgs.Codec` | Wrapper encoder/decoder (H.264 / HEVC / AV1 + nhiều bộ tăng tốc phần cứng). |
| **Filter** | `FFmpegArgs.Filters` | Base filter, attribute, **Expression engine**, interface khả năng. |
| | `.Filters.VideoFilters` (76) | Filter video viết tay. |
| | `.Filters.AudioFilters` (97) | Filter audio viết tay. |
| | `.Filters.VideoSources` (24) / `.AudioSources` (9) / `.MultimediaSources` (2) | Source filter (sinh khung hình/âm thanh: testsrc, color, sine...). |
| | `.Filters.Multimedia` (7) | Filter đa phương tiện (concat, split, select...). |
| | `.Filters.VideoSinks` (2) / `.AudioSinks` (2) | Sink filter (`V/A->|`) — **đã hoàn thiện**: buffersink/nullsink, abuffersink/anullsink. |
| | `.Filters.OpenCLVideoFilters` (17) | Filter tăng tốc OpenCL. |
| | `.Filters.VAAPIVideoFilters` (1) | Filter VAAPI. |
| | `.Filters.Generated` (470) | Filter **auto-generate** từ `ffmpeg -filters`/`-h full`. |
| **ffplay** | `FFplayArgs` | Wrapper `ffplay` — **sơ khai** (chỉ sinh args, chưa có execute). |
| **Công cụ** | `Autogens` | Console app (net8.0) sinh code: filter `.g.cs`, enum Codecs/Demuxer/Muxer. |
| **Test** | `FFmpegArgs.Test` | MSTest (net8.0) — **không cần ffmpeg**, chỉ dựng & kiểm tra chuỗi argument. Chạy trên CI. |
| | `FFmpegArgs.Test.Render` | MSTest (net8.0) — **cần ffmpeg + media** (FFmpegArg, Pipe, AVStreamOption, DrawText, slideshow). Không chạy CI; define `Render` ở Debug để thực thi ffmpeg. |

---

## 3. Kiến trúc lõi (`FFmpegArgs.Cores`)

### 3.1 Cây kế thừa Option
Mọi thành phần sinh argument đều bắt nguồn từ một dictionary option:

```
BaseOption (Options: Dictionary<key,val>)          -> ../FFmpegArgs.Cores/BaseOption.cs
 └ BaseArgsOption (GetOptionArgs)                   -> ../FFmpegArgs.Cores/BaseArgsOption.cs
    └ BaseArgsOptionFlag (Flags: HashSet)           -> ../FFmpegArgs.Cores/BaseArgsOptionFlag.cs
       └ BaseInputOutput (PipeStream, AVStreamArgs) -> ../FFmpegArgs.Cores/BaseInputOutput.cs
          ├ BaseInput  -> ../FFmpegArgs.Cores/Inputs/BaseInput.cs
          └ BaseOutput -> ../FFmpegArgs.Cores/Outputs/BaseOutput.cs
```
- `SetOption / SetOptionRange / SetFlag` là cách "thoát hiểm" để thêm option chưa có wrapper.

### 3.2 Input / Output / Stream
- Input base: [BaseInput.cs](../FFmpegArgs.Cores/Inputs/BaseInput.cs), phân nhánh [ImageInput](../FFmpegArgs.Cores/Inputs/ImageInput.cs) / [AudioInput](../FFmpegArgs.Cores/Inputs/AudioInput.cs) / [VideoInput](../FFmpegArgs.Cores/Inputs/VideoInput.cs) (video = nhiều image stream + audio stream).
- Output base: [BaseOutput.cs](../FFmpegArgs.Cores/Outputs/BaseOutput.cs), tương tự với [VideoOutput](../FFmpegArgs.Cores/Outputs/VideoOutput.cs).
- Stream (một luồng `v`/`a` với index): [BaseAVStream.cs](../FFmpegArgs.Cores/Streams/BaseAVStream.cs).
  - `InputAVStream.GetAllArgs()` sinh `-c:v:0 ...` (per-stream codec/option đầu vào).
  - `OutputAVStream.GetAllArgs()` sinh cả `-map [name]` + `-c:v:0 ...` (đầu ra).

### 3.3 Hệ thống Map (kết nối)
[BaseMap.cs](../FFmpegArgs.Cores/Maps/BaseMap.cs) là "dây nối" giữa input/filter và filter/output:
- `MapName`: nếu là input → `"{index}:v:{streamIndex}"`; nếu là output của filter → tên tự sinh `f_{filterIndex}`.
- `IsInput`, `IsMapped` (đã dùng chưa) — dùng để validate đồ thị.
- Phân loại: [ImageMap](../FFmpegArgs.Cores/Maps/ImageMap.cs), [AudioMap](../FFmpegArgs.Cores/Maps/AudioMap.cs), [VideoMap](../FFmpegArgs.Cores/Maps/VideoMap.cs) (cặp image+audio maps).

### 3.4 FilterGraph — trái tim của thư viện
[FilterGraph.cs](../FFmpegArgs.Cores/Filters/FilterGraph.cs) + [BaseFilter.cs](../FFmpegArgs.Cores/Filters/BaseFilter.cs) + [FilterChain.cs](../FFmpegArgs.Cores/Filters/FilterChain.cs):

1. Mỗi `BaseFilter` khi khởi tạo nhận các map đầu vào, **tự đăng ký** vào `FilterGraph` (`AddFilter`) và tự tạo map đầu ra qua `AddMapOut()`.
2. `GetFilterValue()` sinh `[in1][in2]name=opt1=val1:opt2=val2[out1]` — giá trị một filter.
3. Khi build, `FilterGraph.GetFiltersArgs()`:
   - Kiểm tra không còn map nào "treo" (chưa map vào đâu) → ném lỗi nếu có.
   - Cờ `AutoSinkUnusedMapOut` (mặc định `false`): nếu bật, mọi map out chưa dùng được tự gắn sink (`nullsink`/`anullsink` theo kiểu map qua [AutoSinkFilter](../FFmpegArgs.Cores/Filters/AutoSinkFilter.cs)) thay vì ném lỗi "treo". Output thật (đã map ra stream) không bao giờ bị sink; việc bảo đảm command có ≥1 output do `FFmpegArg.GetOutputsArgs` lo (ném "Output is empty").
   - **useChain=true** (mặc định): [FilterChain.BuildChains](../FFmpegArgs.Cores/Filters/FilterChain.cs) gộp các filter nối tiếp 1-vào-1-ra thành chuỗi `f1,f2,f3` để rút gọn; nối các chuỗi bằng `;`.
   - **useChain=false**: mỗi filter là một mệnh đề riêng, nối bằng `;`.
4. **Escaping 2 cấp** tại [FilterExtensions.cs](../FFmpegArgs.Cores/Extensions/FilterExtensions.cs): Lv1 (giá trị option) + Lv2 (mức filter). *(Lv3 — mức argument — còn bỏ ngỏ, đang comment.)*

### 3.5 Interface đánh dấu khả năng
Hệ thống interface marker phong phú trong [Interfaces/](../FFmpegArgs.Cores/Interfaces/) và [FFmpegArgs.Filters/Interfaces/](../FFmpegArgs.Filters/Interfaces/):
- Loại media: `IImage`, `IAudio`.
- Codec/stream: `ICodec`, `ICodecEncoder`, `ICodecDecoder`, `IStream`, `IInputStream`, `IOutputStream`...
- Demux/Mux (merge `features/mux-demux`): `IDemux` (← `IInput`), `IMux` (← `IOutput`), `IAudioDemux`/`IImageDemux`, `IAudioMux`/`IImageMux` — ràng buộc cho `Format(...)` và các typed demuxer/muxer.
- Khả năng filter: `ITimelineSupport` (`.Enable("expr")`), `ICommandSupport`, `ISliceThreading`, `IFramesync`, `IResamplerOptions`.
- FilterGraph: `IFilterGraph`, `IImageFilterGraph`, `IAudioFilterGraph`.

---

## 4. Hệ thống Filter

### 4.1 Base & cách viết một filter
- Base type theo cặp vào→ra: `ImageToImageFilter`, `AudioToAudioFilter`, `SourceToImageFilter`, `SourceToAudioFilter`... trong [FFmpegArgs.Filters/BaseFilters/](../FFmpegArgs.Filters/BaseFilters/).
- **Expression engine** [FFmpegExpression.cs](../FFmpegArgs.Filters/Expressions/FFmpegExpression.cs): parse & validate biểu thức ffmpeg (abs/sin/cos/if/clip...) theo thuật toán shunting-yard, dùng cho các tham số động (vd. `x="(W-w)/2"`).
- Attribute `FloatRangeAttribute` để gắn min/max cho enum option.

Mẫu một filter (vd. [ScaleFilter.cs](../FFmpegArgs.Filters.VideoFilters/Filters/ScaleFilter.cs), [OverLayFilter.cs](../FFmpegArgs.Filters.VideoFilters/Filters/OverLayFilter.cs), [ColorKeyFilter.cs](../FFmpegArgs.Filters.VideoFilters/Filters/ColorKeyFilter.cs)):
```csharp
public class ScaleFilter : ImageToImageFilter, ICommandSupport {
    internal ScaleFilter(ImageMap map) : base("scale", map) { AddMapOut(); }
    public ScaleFilter W(ExpressionValue w) { ... }   // fluent setter
}
public static class ScaleFilterExtension {
    public static ScaleFilter ScaleFilter(this ImageMap m) => new ScaleFilter(m);
}
```
→ Người dùng gọi `map.ScaleFilter().W("iw/3").H("ih/3").MapOut` (lấy map đầu ra), hoặc `.SplitFilter(2).MapsOut` (nhiều đầu ra).

### 4.2 Filter auto-generate
- Project [Autogens](../Autogens/Program.cs) đọc output của `ffmpeg -filters` và `ffmpeg -h full` (bản backup lưu ở [.other/](../.other/)), dùng **Roslyn** (`Microsoft.CodeAnalysis.CSharp`) sinh ra 470 file `.g.cs` vào [FFmpegArgs.Filters.Generated/Gen/](../FFmpegArgs.Filters.Generated/Gen/).
- Generated khác filter viết tay: **chỉ có option cơ bản**, không có validate expression, ít interface, doc lấy từ help text. Ví dụ [HflipFilterGen.g.cs](../FFmpegArgs.Filters.Generated/Gen/HflipFilterGen.g.cs).
- Logic sinh: [FiltersGen.cs](../Autogens/Filter/FiltersGen.cs); danh sách filter **bị bỏ qua** (loại `N->N`, `|->N`, thiếu doc) ghi tại `.other/NotAutoGen_window.txt`.

---

## 5. Input / Output / Options

- Input cụ thể ([FFmpegArgs.Inputs/](../FFmpegArgs.Inputs/)): `VideoFileInput`, `AudioFileInput`, `ImageFileInput`, `*PipeInput`, `*UrlInput`, `VideoFilesConcatInput`, `ImageFilesConcatInput`, `*FilterGraphInput`. *(`FilterStringInput` đang comment toàn bộ — chưa kích hoạt.)*
- Output cụ thể ([FFmpegArgs.Outputs/](../FFmpegArgs.Outputs/)): `VideoFileOutput`, `AudioFileOutput`, `ImageFileOutput`, `*PipeOutput`, `*UrlOutput`.
- Option khai báo qua extension ([FFmpegArgs.Extensions/](../FFmpegArgs.Extensions/)): global (`-y`, `-hide_banner`...), input/output (`-f`, `-ss`, `-stream_loop`...), per-stream video/audio/subtitle (`-r`, `-vframes`...). Enum format sinh sẵn: [DemuxingFileFormat.g.cs](../FFmpegArgs.Cores/Enums/DemuxingFileFormat.g.cs), [MuxingFileFormat.g.cs](../FFmpegArgs.Cores/Enums/MuxingFileFormat.g.cs).

---

## 6. Thực thi (`FFmpegArgs.Executes`)

Luồng: `FFmpegArg.GetFullCommandline()` → `FFmpegRender` → `Execute()`.

- [FFmpegRender.cs](../FFmpegArgs.Executes/FFmpegRender.cs): `BuildProcess()` tạo `ProcessStartInfo` (redirect stdin/stdout/stderr), gắn handler parse stderr; `Execute()/ExecuteAsync()` (có/không callback tiến độ, có `CancellationToken`). Hỗ trợ pipe qua `WithStdInStream` / `WithStdOutStream`.
- [FFmpegRenderConfig.cs](../FFmpegArgs.Executes/FFmpegRenderConfig.cs): đường dẫn binary (mặc định `"ffmpeg"`), thư mục làm việc, dùng filter-chain hay không, ép dùng filter-script (`FS.txt`), giới hạn độ dài argument (Windows ~32766).
- [FFmpegRenderResult.cs](../FFmpegArgs.Executes/FFmpegRenderResult.cs): `ExitCode`, `ErrorDatas`, `EnsureSuccess()` (ném `FFmpegRenderException` nếu lỗi).
- [RenderProgress.cs](../FFmpegArgs.Executes/RenderProgress.cs): regex parse `frame=/fps=/size=/time=/bitrate=/speed=` từ stderr.

---

## 7. Codec (`FFmpegArgs.Codec`)

39 file wrapper. Base: [BaseCodec.cs](../FFmpegArgs.Codec/BaseCodec.cs) → `BaseCodecEncoder` / `BaseCodecDecoder`.

Độ phủ encoder video rất rộng (mới bổ sung HEVC & AV1 gần đây — xem git log):
- **H.264**: libx264, nvenc, vaapi, qsv, mf, amf, libopenh264.
- **HEVC/H.265**: libx265, nvenc, vaapi, qsv, mf, amf.
- **AV1**: libaom-av1, librav1e, nvenc, qsv, vaapi, amf.
- Decoder: H264 / HEVC / AV1 ([Decoders/Images/](../FFmpegArgs.Codec/Decoders/Images/)).
- Audio: chủ yếu qua extension ([Encoders/Audios/](../FFmpegArgs.Codec/Encoders/Audios/)) — ít wrapper class chuyên biệt.

Mỗi encoder phơi bày enum preset/tune/profile/rate-control... dưới dạng fluent (vd. [IH264_libx264_Encoder.cs](../FFmpegArgs.Codec/Encoders/Images/IH264_libx264_Encoder.cs)).

---

## 8. Build / Đóng gói / Test

- **Đóng gói NuGet** bằng PowerShell + `dotnet pack` + `.nuspec` per-project:
  - [NugetAll.ps1](../NugetAll.ps1) → lặp ~17 project, gọi [FunctionModule.psm1](../FunctionModule.psm1) (`NugetPack`/`NugetPush`); [BuildSingle.psm1](../BuildSingle.psm1) build một project. Cả hai **chỉ chạy ở branch `master`** (`Assert-MasterBranch`).
  - **Version sinh tự động bằng GitVersion** ([GitVersion.yml](../GitVersion.yml), `mode: ManualDeployment`, tag-prefix `v`, master `increment: None`): tag `vMAJOR.MINOR.0` → `PackageVersion = Major.Minor.<số commit kể từ tag>` (vd HEAD hiện tại = `2.2.6`). `AssemblyVersion = Major.Minor.0.0`. Cấu hình tại target `SetVersionFromGitVersion` trong [ProjectBuildProperties.targets](../ProjectBuildProperties.targets); nuspec dùng token `$version$`/`$id$`. Package phụ thuộc khóa khoảng `[2.2,2.3)`.
  - Đóng gói kèm `.dll/.pdb/.xml` + README; **chưa** có symbol package `.snupkg`.
- **Test** (2 project, MSTest net8.0):
  - [FFmpegArgs.Test/](../FFmpegArgs.Test/) — **không phụ thuộc ffmpeg**, chỉ dựng & kiểm tra chuỗi argument (build-args [BuildTest/](../FFmpegArgs.Test/BuildTest/), feature [FeatureTest/](../FFmpegArgs.Test/FeatureTest/): Expression/Rational, codec, render-progress, escaping...). Chạy được trên CI, không cần `ffmpeg`/media.
  - [FFmpegArgs.Test.Render/](../FFmpegArgs.Test.Render/) — **integration cần ffmpeg + media** (`FFmpegArgTest`, `PipeTest`, `AVStreamOptionTest`, `DrawTextTest`, [TanersenerSlideShow/](../FFmpegArgs.Test.Render/TanersenerSlideShow/) + [Resources/](../FFmpegArgs.Test.Render/Resources/)). Define `Render` ở Debug để thực thi ffmpeg; không chạy trên CI.
- **CI** ([.github/workflows/ci.yml](../.github/workflows/ci.yml)): build + chạy `FFmpegArgs.Test` trên `ubuntu-latest` (.NET 8), `-p:EnableGitVersion=false`.
- **Quy ước code** [.editorconfig](../.editorconfig): C# 12, PascalCase, interface prefix `I`, namespace block-scoped, CRLF, indent 4.

---

## 9. Vấn đề / khoảng trống đã ghi nhận

| # | Hạng mục | Trạng thái | Vị trí |
|---|----------|-----------|--------|
| 1 | ~~`FFplayArg.GetFullCommandlineWithFilterScript` dùng `"filter_complex_script"` **thiếu dấu `-`**~~ | **Đã sửa**: thêm `-` + test chống tái phát | [FFplayArg.cs:152](../FFplayArgs/FFplayArg.cs#L152), [FFplayArgTest.cs](../FFmpegArgs.Test/FFplayArgTest.cs) |
| 2 | ~~Video/Audio **Sinks** comment toàn bộ~~ | **Đã làm**: `AllowNoMapOut` + base sink + 4 sink filter + test | [SinkFilterTest.cs](../FFmpegArgs.Test/SinkFilterTest.cs) |
| 3 | ~~`FilterStringInput` (lavfi) comment toàn bộ~~ | **Đã làm**: kích hoạt lại (token RAW) + test | [FilterStringInput.cs](../FFmpegArgs.Inputs/FilterStringInput.cs) |
| 4 | ~~`FFplayArgs` không có lớp execute (chỉ sinh args)~~ | **Đã làm**: `FFplayRender` self-contained + cancel + test | [FFplayArgs/](../FFplayArgs/) |
| 5 | `Inputs.Demuxers` / `Outputs.Muxers` **đã merge lại** (`features/mux-demux`) theo kiến trúc **typed** (BaseDemuxer + 4 ImageDemuxer; muxer mới có BaseMuxer skeleton) — đang **trùng** với extension generic; thêm bug `BaseMuxer.cs` khai báo nhầm `namespace FFmpegArgs.Inputs.Demuxers` | Kế hoạch **hợp nhất** extension → typed class (style `input.XxxDemux(d => …)`) + sửa namespace — xem [ROADMAP-vi.md](ROADMAP-vi.md) | [BaseMuxer.cs](../FFmpegArgs.Outputs.Muxers/BaseMuxer.cs), [MuxerDemuxerOptionsExtension.cs](../FFmpegArgs.Extensions/MuxerDemuxerOptionsExtension.cs) |
| 6 | Filter generated thiếu expression/timeline/validate; loại `N->N`, `|->N` bị bỏ qua | Hạn chế độ phủ (**HOÃN** regen) | `.other/NotAutoGen_window.txt` |
| 7 | Escaping Lv3 (mức process arg/shell) | **Giữ stub** (không cần do thực thi qua `ArgumentList`, không qua shell) | [FilterExtensions.cs](../FFmpegArgs.Cores/Extensions/FilterExtensions.cs) |
| 8 | ~~Test phụ thuộc nhiều vào ffmpeg thật + media cục bộ~~ | **Đã xử lý**: tách [FFmpegArgs.Test/](../FFmpegArgs.Test/) (no-ffmpeg, CI) và [FFmpegArgs.Test.Render/](../FFmpegArgs.Test.Render/) (cần ffmpeg) | [ci.yml](../.github/workflows/ci.yml) |

> Kế hoạch xử lý các mục này nằm ở [ROADMAP-vi.md](ROADMAP-vi.md).

---

## 10. Cập nhật v2.4–v2.5 (đợt mở rộng roadmap)

- **Symbol/source debug** ([ProjectBuildProperties.targets](../ProjectBuildProperties.targets)): `DebugType=embedded` + `EmbedAllSources` + `Microsoft.SourceLink.GitHub`; 20 nuspec bỏ pack `.pdb` rời. Không dùng `.snupkg` thật được do pack qua custom `<NuspecFile>` (SDK bỏ qua symbol package).
- **lavfi input** `FilterStringInput` (`-f lavfi -i`) — token RAW (không tự bọc `"`).
- **Execute ffplay**: `FFplayRender`/`FFplayRenderConfig`/`FFplayRenderResult` + `Render(this FFplayArg)` **self-contained** trong [FFplayArgs/](../FFplayArgs/) (không reference Executes → package decoupled).
- **Cancel**: xác nhận `FFmpegRender` kill process khi cancel (`token.Register(Kill)`) + render test timeout.
- **Audio encoders**: 8 lớp [Encoders/Audios/](../FFmpegArgs.Codec/Encoders/Audios/) (aac/libmp3lame/ac3/eac3/flac/alac/libopus/libvorbis), selector `-c:a:0 <name>`.
- **Muxer/demuxer options** — hiện **tồn tại song song 2 hướng** (sẽ hợp nhất):
  - (a) Extension generic [MuxerDemuxerOptionsExtension.cs](../FFmpegArgs.Extensions/MuxerDemuxerOptionsExtension.cs): `-movflags` (enum `MovFlag`), `-re`, image2 `-start_number`/`-pattern_type` — gắn thẳng trên `BaseInput`/`BaseOutput`.
  - (b) **Typed** (merge `features/mux-demux`): project [FFmpegArgs.Inputs.Demuxers](../FFmpegArgs.Inputs.Demuxers/) (BaseDemuxer + 4 ImageDemuxer Apng/Asf/Dash/Rawvideo, mỗi cái fluent option riêng + `DemuxerExtensions` 2 overload: bare + `Action<TDemuxer>`) và [FFmpegArgs.Outputs.Muxers](../FFmpegArgs.Outputs.Muxers/) (mới có BaseMuxer skeleton); interface `IDemux`/`IMux`/`IAudioDemux`/`IImageDemux`/`IAudioMux`/`IImageMux` trong [FFmpegArgs.Cores/Interfaces/](../FFmpegArgs.Cores/Interfaces/); `Format(DemuxingFileFormat/MuxingFileFormat)` ràng buộc `IDemux`/`IMux`; thêm overload `SetOption(Size)` trong [BaseOption.cs](../FFmpegArgs.Cores/BaseOption.cs).
  - **Kế hoạch hợp nhất**: dồn option format-specific vào typed class, đặt tên extension `XxxDemux`/`XxxMux` để dùng kiểu `input.Image2Demux(d => d.StartNumber(5)…)` / `output.MovMux(m => m.MovFlags(…))`; giữ `-re` ở dạng generic (không thuộc demuxer cụ thể) — xem [ROADMAP-vi.md](ROADMAP-vi.md).
- **Subtitle**: `-c:s` (`Scodec`/`CopySubtitle`) + `-sub_charenc` ([SubtitleAVStreamOptionsExtension.cs](../FFmpegArgs.Extensions/StreamSpecifiers/SubtitleAVStreamOptionsExtension.cs)); burn-in (`subtitles`/`ass`) đã có sẵn.
- **Culture**: (1) [BaseOption.cs](../FFmpegArgs.Cores/BaseOption.cs) format số bằng `InvariantCulture` (định tuyến `SetOptionRange`/`SetOption(object)` qua `IFormattable`); (2) Turkish-i: bỏ `enum.ToString().ToLower()` ở filter, dùng `[Name(...)]` + `GetEnumAttribute` (vd `FadeType.In`→`in` chứ không `ın`); Autogens helper dùng `ToUpperInvariant`. Sửa HẸP, **không** lặp lại commit #7 (đã revert).
- **Tài liệu**: [EXAMPLES.md](EXAMPLES.md) ví dụ theo nhóm tính năng.
- **HOÃN** (rủi ro khi không giám sát): làm giàu filter generated, đồng bộ ffmpeg version (regen → diff lớn); subtitle stream/map đầy đủ; `.snupkg` thật (cần migrate SDK-pack).
