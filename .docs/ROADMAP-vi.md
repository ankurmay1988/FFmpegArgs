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

- [ ] **Symbol package** (ưu tiên 1): phát hành `.snupkg` (Source Link) thay vì nhúng `.pdb`, để debug từ NuGet.
- [ ] **Tài liệu**: trang ví dụ theo nhóm tính năng (filter graph, pipe, codec phần cứng, progress) dựa trên [README.MD](../README.MD); cân nhắc DocFX trang API.
- [ ] **Văn hóa Culture**: README đã cảnh báo về dấu thập phân/Turkish-i; cân nhắc ép `InvariantCulture` nội bộ khi format số/enum để người dùng không phải tự `CultureScope`.
- [x] **Sửa bug FFplay** (đã làm): [FFplayArg.cs:152](../FFplayArgs/FFplayArg.cs#L152) sửa `"filter_complex_script"` → `"-filter_complex_script"` (đồng bộ [FFmpegArg.cs:226](../FFmpegArgs/FFmpegArg.cs#L226)); thêm test chống tái phát `TestFilterComplexScriptArgHasLeadingDash` trong [FFplayArgTest.cs](../FFmpegArgs.Test/FFplayArgTest.cs) (không cần ffmpeg, chạy trên CI).
- [x] **Rà soát escaping** (đã làm): **giữ lại** stub `FiltergraphEscapingLv3` (mức process arg) cho tương lai — hiện chưa cần vì thực thi qua `ProcessStartInfo.ArgumentList` (`UseShellExecute=false`, [FFmpegRender.cs:54-61](../FFmpegArgs.Executes/FFmpegRender.cs#L54-L61)) nên không có "tầng shell" để escape; filtergraph chỉ cần Lv1 (token) + Lv2 (filter) như ffmpeg docs (dùng tại [BaseFilter.cs:83](../FFmpegArgs.Cores/Filters/BaseFilter.cs#L83)). Còn lại (tùy chọn): bổ sung test assert chuỗi escaped Lv1/Lv2 — hiện [TestStringEscape](../FFmpegArgs.Test/FFplayArgTest.cs) mới chỉ build chứ chưa assert.
- [x] **Tách unit test khỏi ffmpeg thật** (đã làm): tách thành 2 project — [FFmpegArgs.Test/](../FFmpegArgs.Test/) chỉ dựng & kiểm tra chuỗi argument (không chạy ffmpeg, chạy được trên CI) và [FFmpegArgs.Test.Render/](../FFmpegArgs.Test.Render/) gồm các test cần ffmpeg + media (`FFmpegArgTest`, `PipeTest`, `AVStreamOptionTest`, `DrawTextTest`, `TanersenerSlideShow`). Bỏ define `Render` ở project không-ffmpeg (đã xoá `Render.cs` bên đó).
- [x] **CI/CD GitHub Actions** (đã làm — phần không-ffmpeg): [.github/workflows/ci.yml](../.github/workflows/ci.yml) build + chạy unit test của `FFmpegArgs.Test` trên `ubuntu-latest` (.NET 8), dùng `-p:EnableGitVersion=false`. Còn lại: matrix đa target, job tùy chọn cài ffmpeg chạy `FFmpegArgs.Test.Render`, và tự đóng gói & publish NuGet khi tag (thay [NugetAll.ps1](../NugetAll.ps1)).
- [x] **Versioning tự động** (đã làm): GitVersion ([GitVersion.yml](../GitVersion.yml)) thay `FileVersion` thủ công + biến `$build`. Tag `vM.m.0` → package `M.m.<commits>`; script pack chỉ chạy ở `master`. Còn lại: cân nhắc tích hợp vào CI.

---

## P1 — Hoàn thiện tính năng còn dở (2.3)

- [x] **Sink filters** (đã làm): thêm khái niệm "filter không có map out" qua `AllowNoMapOut` trong [BaseFilter](../FFmpegArgs.Cores/Filters/BaseFilter.cs) (nới điều kiện ném lỗi "empty output"); thêm base [ImageToSinkFilter](../FFmpegArgs.Filters/BaseFilters/ImageToSinkFilter.cs)/[AudioToSinkFilter](../FFmpegArgs.Filters/BaseFilters/AudioToSinkFilter.cs); hiện thực 4 sink: [BuffersinkFilter](../FFmpegArgs.Filters.VideoSinks/Filters/BuffersinkFilter.cs) + [NullsinkFilter](../FFmpegArgs.Filters.VideoSinks/Filters/NullsinkFilter.cs) (`V->|`), [AbuffersinkFilter](../FFmpegArgs.Filters.AudioSinks/Filters/AbuffersinkFilter.cs) + [AnullsinkFilter](../FFmpegArgs.Filters.AudioSinks/Filters/AnullsinkFilter.cs) (`A->|`); [FilterChain](../FFmpegArgs.Cores/Filters/FilterChain.cs) sẵn xử lý sink làm filter cuối chuỗi. Test arg-build trong [SinkFilterTest.cs](../FFmpegArgs.Test/SinkFilterTest.cs) (không cần ffmpeg). Lưu ý: `buffersink`/`abuffersink` là sink API của libavfilter, chủ yếu dùng với `lavfi`/filter-script; chưa thêm option (`pix_fmts`/`sample_fmts`...).
- [ ] **`FilterStringInput`** (lavfi): kích hoạt lại [FilterStringInput.cs](../FFmpegArgs.Inputs/FilterStringInput.cs) cho phép dùng filtergraph làm input (`-f lavfi -i "..."`).
- [ ] **Execute cho FFplay**: thêm `FFplayRender`/`FFplayRenderConfig` tương tự [FFmpegRender](../FFmpegArgs.Executes/FFmpegRender.cs) (hoặc tách phần chạy process dùng chung). Hiện [FFplayArgs/](../FFplayArgs/) chỉ sinh args, chưa chạy được.
- [ ] **Hủy tiến trình**: kiểm tra `CancellationToken` trong [FFmpegRender](../FFmpegArgs.Executes/FFmpegRender.cs) có thực sự kill process ffmpeg (gửi `q`/Kill) khi hủy; thêm test timeout.

---

## P2 — Mở rộng độ phủ (2.4)

- [ ] **Audio encoder wrappers**: bổ sung lớp encoder chuyên biệt (aac, libfdk_aac, libmp3lame, libopus, ac3, flac...) song song với độ phủ video hiện có trong [FFmpegArgs.Codec/](../FFmpegArgs.Codec/) (hiện audio chủ yếu qua extension thô).
- [ ] **Demuxer/Muxer options**: lấp đầy `FFmpegArgs.Inputs.Demuxers` & `FFmpegArgs.Outputs.Muxers` (đang trống) bằng option đặc thù phổ biến (concat, image2, hls, dash, mp4 movflags, rawvideo...).
- [ ] **Subtitle**: hoàn thiện stream/option subtitle (mux/burn-in, `-c:s`, charenc, `subtitles`/`ass` filter).
- [ ] **Làm giàu filter generated**: nâng cấp [Autogens/Filter/FiltersGen.cs](../Autogens/Filter/FiltersGen.cs):
  - Hỗ trợ thêm loại bị bỏ qua (`N->N`, `|->N`) — xem danh sách `.other/NotAutoGen_window.txt`.
  - Tự gắn `ITimelineSupport`/`ICommandSupport`/`ISliceThreading` theo cờ `T/S/C` từ `ffmpeg -filters`.
  - Nhận diện tham số kiểu expression để dùng `ExpressionValue` thay vì string.
- [ ] **Đồng bộ ffmpeg version**: cập nhật bản dump trong [.other/](../.other/) lên ffmpeg mới, chạy lại [Autogens](../Autogens/Program.cs), ghi chú ffmpeg version tối thiểu được hỗ trợ.

---

## P4 — Tính năng nâng cao (3.0)

- [ ] **ffprobe wrapper**: project mới `FFprobeArgs` trả về metadata có cấu trúc (duration, streams, codec, resolution) — bổ trợ tự nhiên cho ffmpeg.
- [ ] **Chuỗi tăng tốc phần cứng**: hỗ trợ `-hwaccel`, `hwupload`/`hwdownload`, `-init_hw_device`, format trung gian GPU để dùng trọn vẹn [OpenCLVideoFilters](../FFmpegArgs.Filters.OpenCLVideoFilters/) / [VAAPIVideoFilters](../FFmpegArgs.Filters.VAAPIVideoFilters/).
- [ ] **Validate đồ thị mạnh hơn**: phát hiện sớm map dùng 2 lần, lệch kiểu audio/video, sink/source không khớp — báo lỗi rõ ràng tại thời điểm dựng thay vì khi chạy ffmpeg.
- [ ] **Autogen bằng Roslyn Source Generator**: chuyển [Autogens](../Autogens/) (console app sinh file `.g.cs`) sang incremental source generator để filter generated luôn đồng bộ khi build (giảm rủi ro lệch nguồn).
- [ ] **AOT / trimming friendly**: kiểm tra tương thích NativeAOT/trimming (tránh reflection runtime), gắn annotation phù hợp.
- [ ] **API streaming nâng cao**: helper cho realtime/streaming output (RTMP/SRT/HLS), và pipe đồng thời nhiều stream.

---

## Nguyên tắc khi mở rộng

1. **Giữ tính nhất quán API**: filter mới theo đúng mẫu `BaseXToYFilter` + extension `this Map` + `AddMapOut()` (xem [ScaleFilter.cs](../FFmpegArgs.Filters.VideoFilters/Filters/ScaleFilter.cs)).
2. **Ưu tiên auto-generate** cho filter đơn giản; chỉ viết tay khi cần expression/validate/nhiều overload.
3. **Mỗi tính năng kèm test build-args** (không cần ffmpeg) để chạy được trên CI.
4. **Tôn trọng multi-target** netstandard2.0 (tránh API chỉ có ở net mới mà không guard `#if`).
5. **Cập nhật [CODEBASE_SUMMARY-vi.md](CODEBASE_SUMMARY-vi.md)** khi thêm project/đổi kiến trúc.
