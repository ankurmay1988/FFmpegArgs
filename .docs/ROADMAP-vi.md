# FFmpegArgs — Roadmap phát triển

> Đề xuất kế hoạch phát triển dựa trên hiện trạng codebase (xem [CODEBASE_SUMMARY-vi.md](CODEBASE_SUMMARY-vi.md)).
> Phiên bản hiện tại: **2.2**. Đây là *đề xuất* — thứ tự ưu tiên có thể điều chỉnh theo nhu cầu thực tế.
> Quy ước: **P0** = sửa gấp + chất lượng & hạ tầng (đã gộp P3) · **P1** = hoàn thiện tính năng dở · **P2** = mở rộng độ phủ · **P4** = nâng cao.

---

## Bảng tổng hợp ưu tiên

| Ưu tiên | Mục tiêu | Phiên bản đề xuất | Quy mô |
|---------|----------|-------------------|--------|
| P0 | Sửa bug FFplay (xong) + **chất lượng & hạ tầng gộp từ P3**: symbol package (ưu tiên 1), tài liệu, ép InvariantCulture | 2.2.x–2.5 | Vừa |
| P1 | Hoàn thiện Sinks, FilterStringInput, execute cho FFplay | 2.3 | Vừa |
| P2 | Mở rộng codec audio, demuxer/muxer options, subtitle, làm giàu filter generated | 2.4 | Lớn |
| P4 | ffprobe wrapper, hwupload/hwdownload, source-generator hóa autogen, AOT/trim | 3.0 | Lớn |

---

## P0 — Sửa gấp, chất lượng & hạ tầng (đã gộp P3, ưu tiên cao)

> Gộp toàn bộ **P3 (chất lượng & hạ tầng)** vào đây theo yêu cầu. Trong nhóm còn dở, **Symbol package** được ưu tiên làm trước.

- [x] **Symbol package** (đã làm, ưu tiên 1): KHÔNG sinh được `.snupkg` thật vì pack qua custom `<NuspecFile>` + `dotnet pack` (SDK bỏ qua symbol package). Thay bằng cách tương đương, tự chứa: `DebugType=embedded` + `EmbedAllSources` + `Microsoft.SourceLink.GitHub` trong [ProjectBuildProperties.targets](../ProjectBuildProperties.targets) → PDB (kèm `.cs` nén) nhúng vào DLL; bỏ dòng pack `.pdb` rời khỏi 20 nuspec. Verify: nupkg `lib/<tfm>/` chỉ còn dll+xml, dll có marker `MPDB`. Muốn `.snupkg` thật phải migrate sang SDK-pack.
- [x] **Tài liệu** (đã làm): thêm [EXAMPLES.md](EXAMPLES.md) — ví dụ copy-paste theo nhóm tính năng (filter graph, lavfi input, sink/auto-sink, audio/hardware encoder, muxer/demuxer options, subtitle, pipe, execute, progress & cancellation), link từ [README.MD](../README.MD). DocFX trang API: chưa.
- [x] **Văn hóa Culture** (đã làm): ép `InvariantCulture` đúng tâm điểm format số trong [BaseOption.cs](../FFmpegArgs.Cores/BaseOption.cs) (`SetOptionRange` int/float/double/long/decimal/TimeSpan + `SetOption(object)` định tuyến qua `IFormattable.ToString(null, InvariantCulture)`) → ffmpeg không nhận `0,5` trên vi-VN/de-DE, khỏi cần `CultureScope`. **CHÚ Ý**: sửa HẸP (chỉ format số), KHÔNG lặp lại commit #7 (InvariantCulture + Ordinal toàn repo + editorconfig-as-errors) đã bị revert. Test de-DE [CultureInvariantOptionTest.cs](../FFmpegArgs.Test/CultureInvariantOptionTest.cs). Thêm: lỗi **Turkish-i** (`enum.ToString().ToLower()` → `In`→`ın` trên `tr-TR`) sửa bằng cách bỏ ToLower ở enum, dùng `[Name(...)]` + `GetEnumAttribute` (16 enum/13 call site); test [TurkishCultureFilterTest.cs](../FFmpegArgs.Test/TurkishCultureFilterTest.cs).
- [x] **Sửa bug FFplay** (đã làm): [FFplayArg.cs:152](../FFplayArgs/FFplayArg.cs#L152) sửa `"filter_complex_script"` → `"-filter_complex_script"` (đồng bộ [FFmpegArg.cs:226](../FFmpegArgs/FFmpegArg.cs#L226)); thêm test chống tái phát `TestFilterComplexScriptArgHasLeadingDash` trong [FFplayArgTest.cs](../FFmpegArgs.Test/FFplayArgTest.cs) (không cần ffmpeg, chạy trên CI).
- [x] **Rà soát escaping** (đã làm): **giữ lại** stub `FiltergraphEscapingLv3` (mức process arg) cho tương lai — hiện chưa cần vì thực thi qua `ProcessStartInfo.ArgumentList` (`UseShellExecute=false`, [FFmpegRender.cs:54-61](../FFmpegArgs.Executes/FFmpegRender.cs#L54-L61)) nên không có "tầng shell" để escape; filtergraph chỉ cần Lv1 (token) + Lv2 (filter) như ffmpeg docs (dùng tại [BaseFilter.cs:83](../FFmpegArgs.Cores/Filters/BaseFilter.cs#L83)). Còn lại (tùy chọn): bổ sung test assert chuỗi escaped Lv1/Lv2 — hiện [TestStringEscape](../FFmpegArgs.Test/FFplayArgTest.cs) mới chỉ build chứ chưa assert.
- [x] **Tách unit test khỏi ffmpeg thật** (đã làm): tách thành 2 project — [FFmpegArgs.Test/](../FFmpegArgs.Test/) chỉ dựng & kiểm tra chuỗi argument (không chạy ffmpeg, chạy được trên CI) và [FFmpegArgs.Test.Render/](../FFmpegArgs.Test.Render/) gồm các test cần ffmpeg + media (`FFmpegArgTest`, `PipeTest`, `AVStreamOptionTest`, `DrawTextTest`, `TanersenerSlideShow`). Bỏ define `Render` ở project không-ffmpeg (đã xoá `Render.cs` bên đó).
- [x] **CI/CD GitHub Actions** (đã làm — phần không-ffmpeg): [.github/workflows/ci.yml](../.github/workflows/ci.yml) build + chạy unit test của `FFmpegArgs.Test` trên `ubuntu-latest` (.NET 8), dùng `-p:EnableGitVersion=false`. Còn lại: matrix đa target, job tùy chọn cài ffmpeg chạy `FFmpegArgs.Test.Render`, và tự đóng gói & publish NuGet khi tag (thay [NugetAll.ps1](../NugetAll.ps1)).
- [x] **Versioning tự động** (đã làm): GitVersion ([GitVersion.yml](../GitVersion.yml)) thay `FileVersion` thủ công + biến `$build`. Tag `vM.m.0` → package `M.m.<commits>`; script pack chỉ chạy ở `master`. Còn lại: cân nhắc tích hợp vào CI.

---

## P1 — Hoàn thiện tính năng còn dở (2.3)

- [x] **Sink filters** (đã làm): thêm khái niệm "filter không có map out" qua `AllowNoMapOut` trong [BaseFilter](../FFmpegArgs.Cores/Filters/BaseFilter.cs) (nới điều kiện ném lỗi "empty output"); thêm base [ImageToSinkFilter](../FFmpegArgs.Filters/BaseFilters/ImageToSinkFilter.cs)/[AudioToSinkFilter](../FFmpegArgs.Filters/BaseFilters/AudioToSinkFilter.cs); hiện thực 4 sink: [BuffersinkFilter](../FFmpegArgs.Filters.VideoSinks/Filters/BuffersinkFilter.cs) + [NullsinkFilter](../FFmpegArgs.Filters.VideoSinks/Filters/NullsinkFilter.cs) (`V->|`), [AbuffersinkFilter](../FFmpegArgs.Filters.AudioSinks/Filters/AbuffersinkFilter.cs) + [AnullsinkFilter](../FFmpegArgs.Filters.AudioSinks/Filters/AnullsinkFilter.cs) (`A->|`); [FilterChain](../FFmpegArgs.Cores/Filters/FilterChain.cs) sẵn xử lý sink làm filter cuối chuỗi. Test arg-build trong [SinkFilterTest.cs](../FFmpegArgs.Test/SinkFilterTest.cs) (không cần ffmpeg). Lưu ý: `buffersink`/`abuffersink` là sink API của libavfilter, chủ yếu dùng với `lavfi`/filter-script; chưa thêm option (`pix_fmts`/`sample_fmts`...).
- [x] **`FilterStringInput`** (lavfi, đã làm): kích hoạt lại [FilterStringInput.cs](../FFmpegArgs.Inputs/FilterStringInput.cs) theo pattern `GetAllArgs()` token-list của sibling — truyền filter RAW, KHÔNG tự bọc `"` (để `ProcessStartInfo.ArgumentList` / renderer tự quote). Test [FilterStringInputTest.cs](../FFmpegArgs.Test/FilterStringInputTest.cs).
- [x] **Execute cho FFplay** (đã làm): thêm `FFplayRender`/`FFplayRenderConfig`/`FFplayRenderResult` + extension `Render(this FFplayArg)` SELF-CONTAINED trong [FFplayArgs/](../FFplayArgs/) (KHÔNG reference Executes → giữ package decoupled, nuspec deps thủ công), mirror [FFmpegRender](../FFmpegArgs.Executes/FFmpegRender.cs), hỗ trợ `CancellationToken` (kill). Test [FFplayRenderTest.cs](../FFmpegArgs.Test.Render/FFplayRenderTest.cs) (headless `-nodisp` nguồn `sine`).
- [x] **Hủy tiến trình** (đã làm): xác nhận [FFmpegRender](../FFmpegArgs.Executes/FFmpegRender.cs) đã `token.Register(() => process.Kill())` ở cả Execute/ExecuteAsync (kill đúng, không cần sửa logic — dùng Kill chứ không gửi `q` vì gửi `q` cần redirect stdin chỉ có khi pipe). Thêm render test timeout [CancellationRenderTest.cs](../FFmpegArgs.Test.Render/CancellationRenderTest.cs) (cancel render 24h sau 800ms → trả về <60s, exit≠0).

---

## P2 — Mở rộng độ phủ (2.4)

- [x] **Audio encoder wrappers** (đã làm): 8 lớp encoder trong [FFmpegArgs.Codec/Encoders/Audios/](../FFmpegArgs.Codec/Encoders/Audios/) — aac, libmp3lame, ac3, eac3, flac, alac, libopus, libvorbis (option lấy từ `ffmpeg -h encoder=<name>` thật, dump kèm comment). `libfdk_aac` không có trong build chuẩn (non-free) nên bỏ. 14 test arg-build trong [AudioEncoderTest.cs](../FFmpegArgs.Test/AudioEncoderTest.cs).
- [x] **Demuxer/Muxer options** (đã làm — **đang tồn tại 2 hướng, cần hợp nhất**):
  - (a) Extension generic [MuxerDemuxerOptionsExtension.cs](../FFmpegArgs.Extensions/MuxerDemuxerOptionsExtension.cs) (đợt P2): muxer `-movflags` (enum `MovFlag`); demuxer `-re`/`-start_number`/`-pattern_type` (image2) — gắn thẳng trên `BaseInput`/`BaseOutput`.
  - (b) **Typed** (merge `features/mux-demux` vào `dev`): 2 project `FFmpegArgs.Inputs.Demuxers` / `FFmpegArgs.Outputs.Muxers` **đã QUAY LẠI solution** với kiến trúc typed — `BaseDemuxer` + 4 ImageDemuxer (Apng/Asf/Dash/Rawvideo) + `DemuxerExtensions`; `Outputs.Muxers` mới có `BaseMuxer` skeleton; interface `IDemux`/`IMux`/…
  - → Hai hướng **trùng chức năng**. Kế hoạch hợp nhất xem mục **[Hợp nhất Demuxer/Muxer](#hợp-nhất-demuxermuxer-typed--extension)** bên dưới. concat/hls/dash sâu hơn: chưa.
- [x] **Subtitle** (đã làm, một phần): burn-in (`subtitles`/`ass` video filter) đã có sẵn; thêm `-c:s` (`Scodec`/`CopySubtitle`) + `-sub_charenc` trong [SubtitleAVStreamOptionsExtension.cs](../FFmpegArgs.Extensions/StreamSpecifiers/SubtitleAVStreamOptionsExtension.cs). Kiến trúc subtitle stream/map đầy đủ (mux subtitle stream như image/audio map): **HOÃN** (thay đổi kiến trúc lớn).
- [ ] **Làm giàu filter generated** (**HOÃN** — chạy lại autogen sinh diff rất lớn, rủi ro khi không giám sát): nâng cấp [Autogens/Filter/FiltersGen.cs](../Autogens/Filter/FiltersGen.cs):
  - Hỗ trợ thêm loại bị bỏ qua (`N->N`, `|->N`) — xem danh sách `.other/NotAutoGen_window.txt`.
  - Tự gắn `ITimelineSupport`/`ICommandSupport`/`ISliceThreading` theo cờ `T/S/C` từ `ffmpeg -filters`.
  - Nhận diện tham số kiểu expression để dùng `ExpressionValue` thay vì string.
- [ ] **Đồng bộ ffmpeg version** (**HOÃN** — cần regen + diff lớn, làm khi có giám sát): cập nhật bản dump trong [.other/](../.other/) lên ffmpeg mới, chạy lại [Autogens](../Autogens/Program.cs), ghi chú ffmpeg version tối thiểu được hỗ trợ.

---

## P4 — Tính năng nâng cao (3.0)

- [ ] **ffprobe wrapper**: project mới `FFprobeArgs` trả về metadata có cấu trúc (duration, streams, codec, resolution) — bổ trợ tự nhiên cho ffmpeg.
- [ ] **Chuỗi tăng tốc phần cứng**: hỗ trợ `-hwaccel`, `hwupload`/`hwdownload`, `-init_hw_device`, format trung gian GPU để dùng trọn vẹn [OpenCLVideoFilters](../FFmpegArgs.Filters.OpenCLVideoFilters/) / [VAAPIVideoFilters](../FFmpegArgs.Filters.VAAPIVideoFilters/).
- [ ] **Validate đồ thị mạnh hơn**: phát hiện sớm map dùng 2 lần, lệch kiểu audio/video, sink/source không khớp — báo lỗi rõ ràng tại thời điểm dựng thay vì khi chạy ffmpeg.
- [ ] **Autogen bằng Roslyn Source Generator**: chuyển [Autogens](../Autogens/) (console app sinh file `.g.cs`) sang incremental source generator để filter generated luôn đồng bộ khi build (giảm rủi ro lệch nguồn).
- [ ] **AOT / trimming friendly**: kiểm tra tương thích NativeAOT/trimming (tránh reflection runtime), gắn annotation phù hợp.
- [ ] **API streaming nâng cao**: helper cho realtime/streaming output (RTMP/SRT/HLS), và pipe đồng thời nhiều stream.

---

## Hợp nhất Demuxer/Muxer (typed ↔ extension)

> Sau khi merge `features/mux-demux`, option demuxer/muxer tồn tại **2 hướng song song**. Mục tiêu: gom về **một** kiểu API typed, dùng coding style closure:
> ```csharp
> imageFileInput.Image2Demux(d => d.StartNumber(5).PatternType(Image2PatternType.glob));
> videoFileOutput.MovMux(m => m.MovFlags(MovFlag.faststart, MovFlag.empty_moov));
> ```

### Hiện trạng 2 hướng

| Option | Hướng (a) generic — [MuxerDemuxerOptionsExtension.cs](../FFmpegArgs.Extensions/MuxerDemuxerOptionsExtension.cs) | Hướng (b) typed — [Demuxers](../FFmpegArgs.Inputs.Demuxers/)/[Muxers](../FFmpegArgs.Outputs.Muxers/) |
|--------|----------------------------------------------------------------------------------------------------------------|----------------------------------------------------------------------------------------------------|
| image2 `-start_number`/`-pattern_type` | `input.StartNumber(5).PatternType(...)` trên mọi `BaseInput` | chưa có `Image2Demuxer` |
| mov/mp4 `-movflags` | `output.MovFlags(...)` trên mọi `BaseOutput` | chưa có `MovMuxer` (mới `BaseMuxer` skeleton) |
| `-re` | `input.Re()` trên mọi `BaseInput` | (không thuộc demuxer cụ thể) |
| apng/asf/dash/rawvideo | — | đã có 4 `*Demuxer` typed |

**Cơ chế style closure** đã có sẵn ở overload thứ 2 của `DemuxerExtensions` (vd [RawvideoDemuxer.cs:35](../FFmpegArgs.Inputs.Demuxers/ImageDemuxers/RawvideoDemuxer.cs#L35)): `static TInput XxxDemuxer<TInput>(this TInput input, Action<XxxDemuxer> action)`. Việc cần làm chỉ là (1) **đổi tên** extension sang hậu tố `Demux`/`Mux`, (2) **chuyển** option generic vào typed class.

### Kế hoạch (chưa code — chờ duyệt)

1. **Quy ước đặt tên**: extension method dùng hậu tố động từ `Demux`/`Mux` (`input.Image2Demux(...)`, `output.MovMux(...)`); **class** giữ danh từ `*Demuxer`/`*Muxer`. → Đổi 4 extension hiện có `ApngDemuxer/AsfDemuxer/DashDemuxer/RawvideoDemuxer` → `ApngDemux/AsfDemux/DashDemux/RawvideoDemux` (giữ nguyên tên class).
2. **Tạo `Image2Demuxer`** (`FFmpegArgs.Inputs.Demuxers`, format `image2`): chuyển `StartNumber`/`PatternType` + enum `Image2PatternType` vào đây. Extension `Image2Demux(this TInput, …)`.
3. **Tạo `MovMuxer`** (`FFmpegArgs.Outputs.Muxers` — muxer cụ thể **đầu tiên**, format mov/mp4): chuyển `MovFlags` + enum `MovFlag` vào đây. Extension `MovMux(this TOutput, …)`.
4. **Giữ `-re` ở dạng generic**: dời `Re()` sang [InputOutputOptionsExtension.cs](../FFmpegArgs.Extensions/) (là option đọc input tốc độ gốc, áp cho mọi input — KHÔNG nhét vào demuxer cụ thể).
5. **Xóa** [MuxerDemuxerOptionsExtension.cs](../FFmpegArgs.Extensions/MuxerDemuxerOptionsExtension.cs) sau khi đã dời hết.
6. **Sửa bug** namespace [BaseMuxer.cs:1](../FFmpegArgs.Outputs.Muxers/BaseMuxer.cs#L1): `FFmpegArgs.Inputs.Demuxers` → `FFmpegArgs.Outputs.Muxers`.
7. **Test**: viết lại [MuxerDemuxerOptionsTest.cs](../FFmpegArgs.Test/MuxerDemuxerOptionsTest.cs) theo API mới + test build-args cho `Image2Demux`/`MovMux`.
8. **Đóng gói**: cân nhắc thêm 2 project mới vào [NugetAll.ps1](../NugetAll.ps1) (quyết định khi release).

### Điểm cần quyết trước khi code

- **Ép `-f` hay không**: `BaseDemuxer`/`BaseMuxer` tự set `-f <format>` trong ctor. `Image2Demux` → thêm `-f image2` (hợp lý). Nhưng `MovMux` → thêm `-f mov`; với output `.mp4` có thể muốn `-f mp4` (hoặc không ép gì, để ffmpeg suy từ đuôi file). Cần chọn: ép `-f mov`, tách `Mp4Muxer`/`MovMuxer` riêng, hay cho `MovFlags` không kèm ép format.
- **Breaking change**: cả 2 hướng đều **chưa release** (mới ở `dev`) → đổi tên/di chuyển API **an toàn**, không phá bản đã phát hành.

---

## Nguyên tắc khi mở rộng

1. **Giữ tính nhất quán API**: filter mới theo đúng mẫu `BaseXToYFilter` + extension `this Map` + `AddMapOut()` (xem [ScaleFilter.cs](../FFmpegArgs.Filters.VideoFilters/Filters/ScaleFilter.cs)).
2. **Ưu tiên auto-generate** cho filter đơn giản; chỉ viết tay khi cần expression/validate/nhiều overload.
3. **Mỗi tính năng kèm test build-args** (không cần ffmpeg) để chạy được trên CI.
4. **Tôn trọng multi-target** netstandard2.0 (tránh API chỉ có ở net mới mà không guard `#if`).
5. **Cập nhật [CODEBASE_SUMMARY-vi.md](CODEBASE_SUMMARY-vi.md)** khi thêm project/đổi kiến trúc.
