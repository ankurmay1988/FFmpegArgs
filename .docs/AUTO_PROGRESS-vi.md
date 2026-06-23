# AUTO_PROGRESS — Trạng thái vòng lặp tự động (overnight)

> File điều phối cho **làm việc không giám sát** (chủ máy đang ngủ, máy để idle).
> Mọi session (kể cả cron 7h sáng) và mọi subagent **PHẢI đọc file này trước**, làm theo, rồi cập nhật.
> Nguồn sự thật về tiến độ là file này + checkbox trong [ROADMAP-vi.md](ROADMAP-vi.md).

---

## 0. Bối cảnh

- Người dùng giao 2 nhóm việc roadmap để chạy qua đêm: **"Làm giàu filter generated"** (P2) và **P4** (3.0).
- Yêu cầu: giao việc cho **subagent** (tiết kiệm context phiên chính), tạo **cron ~7h sáng** để chạy tiếp phòng hết token giữa chừng.
- Cô lập trên branch **`auto/overnight-roadmap`** (tách khỏi `dev`) để sáng review = 1 diff branch, `dev` không bị regen lớn không giám sát làm bẩn.

## 1. Môi trường

- Repo: `d:\IT\Csharp\Libraries\FFmpegArgs` — luôn làm việc tại đây, CWD = gốc repo.
- ffmpeg sinh/test chuẩn: **n8.0.1** tại `D:\IT\lib\FFmpeg\ffmpeg-n8.0.1-48-g0592be14ff-win64-gpl-shared-8.0\bin`.
  - PATH mặc định đang là n8.0-23 → **khi chạy Autogens phải prepend bin 8.0.1**:
    - PowerShell: `$env:PATH = "D:\IT\lib\FFmpeg\ffmpeg-n8.0.1-48-g0592be14ff-win64-gpl-shared-8.0\bin;" + $env:PATH`
- Autogens ghi file bằng **đường dẫn tương đối** → CWD bắt buộc = gốc repo.
  - Lệnh regen: `dotnet run --project Autogens -c Debug -p:EnableGitVersion=false`
- Multi-target: `netstandard2.0;net6.0;net8.0`; test chạy net8.0; LangVersion 12; Nullable enable.
- Baseline test: **FFmpegArgs.Test = 53/53 pass** (không cần ffmpeg). Đây là mốc không được tụt.

## 2. Guardrails (HARD RULES — không vi phạm)

- **HR1** Luôn ở branch `auto/overnight-roadmap`. Nếu chưa: `git switch auto/overnight-roadmap` (chưa có thì `git switch -c auto/overnight-roadmap dev`). **KHÔNG** commit thẳng vào `dev`/`master`.
- **HR2** Trước MỖI commit phải XANH: `dotnet build FFmpegArgs.sln -c Debug -p:EnableGitVersion=false` **và** `dotnet test FFmpegArgs.Test -c Debug -p:EnableGitVersion=false` (≥ 53 pass). Nếu đỏ và không sửa nhanh được → revert thay đổi của mình (`git restore .` / `git checkout -- <files>`), ghi blocker vào mục §5, chuyển task khác. **TUYỆT ĐỐI không commit code đỏ.**
- **HR3** Stage theo đường dẫn cụ thể (`git add <path>`). Tránh `git add -A`/`git add .` trừ khi đã `git status` xác nhận chỉ có đúng file của mình. Không stage file lạ.
- **HR4** **KHÔNG** `git push`, **KHÔNG** `git tag`, KHÔNG amend/reset/rebase commit đã có. Chỉ tạo commit mới.
- **HR5** Commit theo từng đơn vị logic, theo Conventional Commits tiếng Anh (xem [COMMIT_CONVENTION.md](COMMIT_CONVENTION.md)). Trailer cuối message: `Co-Authored-By: Claude Opus 4.8 <noreply@anthropic.com>`.
- **HR6** Tôn trọng netstandard2.0: API chỉ có ở net mới phải guard `#if`.
- **HR7** Sau mỗi task: cập nhật §4 (status) + §5 (log) trong file này và commit kèm.
- **HR8** Tiết kiệm context điều phối: **giao phần đọc/sửa nặng cho subagent** (Agent tool), chỉ giữ summary. Không tự đọc dump lớn trong phiên điều phối.
- **HR9** KHÔNG đụng workstream khác (changelog/versioning/CI: `cliff.toml`, `Changelog.ps1`, `CHANGELOG.md`, `.github/workflows/*`, `NugetAll.ps1`, `GitVersion.yml`, `*.psm1`).
- **HR10** Regen lớn (làm giàu filter): giữ diff review được, **ghi số file `.g.cs` thay đổi** vào log.

## 3. Resume protocol (cron / phiên mới làm theo)

1. CWD = `d:\IT\Csharp\Libraries\FFmpegArgs`.
2. `git switch auto/overnight-roadmap` (chưa có → `git switch -c auto/overnight-roadmap dev`).
3. Đọc file này. Tìm task đầu tiên **chưa `✅`** trong §4 (theo thứ tự).
4. Spawn **1 subagent foreground** (Agent tool, `subagent_type: general-purpose`) với spec task + toàn bộ guardrails §2 + môi trường §1.
5. Subagent build+test xanh → commit (HR2/HR5). Nếu đỏ/blocked → revert + log, sang task kế.
6. Cập nhật §4/§5, commit tracker.
7. Khi tất cả task mục tiêu `✅`: ghi "ALL DONE" vào §5, thử `CronDelete` cron tự-resume, dừng.
8. Mỗi lần fire chỉ chạy **một** subagent rồi để vòng lặp/cron kế tiếp tiếp tục — tránh chạy quá lâu một lượt.

## 4. Hàng đợi task (theo thứ tự ưu tiên an toàn)

> Trạng thái: `⬜ pending` · `🔄 in-progress` · `✅ done` · `⛔ blocked` · `⏭️ deferred`

### T1 — P4: ffprobe wrapper `FFprobeArgs`  · trạng thái: ✅ (commit `deefb99`)
- Project mới `FFprobeArgs`, **mirror cấu trúc & convention của `FFplayArgs`** (multi-target, csproj props, self-contained — KHÔNG reference `FFmpegArgs.Executes`).
- `FFprobeArg`: dựng token-list argument (giống `FFplayArg.GetAllArgs()`). Hỗ trợ tối thiểu: input (`-i`/positional), `-hide_banner`, `-v/-loglevel`, `-print_format`/`-of` (json/xml/csv/flat/ini/default), `-show_format`, `-show_streams`, `-show_packets`, `-show_frames`, `-select_streams <spec>`, `-show_entries <...>`, `-count_frames`/`-count_packets`. Fluent extension methods cho các option phổ biến.
- (Tuỳ chọn, nếu còn thời gian) `FFprobeRender`/`FFprobeRenderConfig`/`FFprobeRenderResult` self-contained mirror [FFmpegRender](../FFmpegArgs.Executes/FFmpegRender.cs): chạy ffprobe, lấy stdout, trả raw + parse JSON tối thiểu (model `format{duration,bit_rate,format_name}`, `streams[]{index,codec_type,codec_name,width,height,duration}`) bằng `System.Text.Json`. Test parse/execute (cần ffprobe) để ở `FFmpegArgs.Test.Render`.
- Thêm `FFprobeArgs` vào `FFmpegArgs.sln` (tree đang sạch nên an toàn).
- Test arg-build trong `FFmpegArgs.Test` (thêm ProjectReference `FFprobeArgs`) — assert token-list các case đại diện. KHÔNG cần ffmpeg.
- Gate: solution build + 53 test cũ pass + test mới pass. Commit `feat(ffprobe): ...`.

### T2 — P2: Làm giàu filter generated  · trạng thái: ✅ ĐÃ ĐIỀU TRA, không thay đổi code (kết quả đúng đắn)
> **Kết luận điều tra (subagent a42b10ea):** baseline regen 8.0.1 = **0 diff** (generated code đã đồng bộ).
> - **T2a — đóng, BẤT KHẢ THI:** ffmpeg 8.0.1 `-filters` chỉ có **2 cột cờ (T, S)**, KHÔNG có cột Command-support `C` (561/561 dòng khớp `^ [TS.]{2} `, 0 dòng 3 cờ). Interface `ICommandSupport` đã tồn tại tại `FFmpegArgs.Filters\Interfaces\ICommandSupport.cs` nhưng không có dữ liệu để gắn. → Không sửa regex/GetFilterInterface.
> - **T2c — đóng, ĐÃ LÀM:** `FilterFunctionGen.cs:321-332` đã emit `ExpressionValue` khi mô tả chứa "expression" hoặc tên param kết thúc "expr". ffmpeg không có bit máy-đọc cho expression (cột 11 cờ EDFVASXRBTP: `T`=RUNTIME_PARAM ≠ expression) → heuristic hiện tại là tốt nhất khả dĩ.
> - **T2b — HOÃN ⏭️ (architectural):** dynamic `N→N`/`|→N` cần base class mới; rủi ro cao, để làm có giám sát. Đây là phần "làm giàu" thực chất còn lại duy nhất.

<!-- spec gốc giữ lại bên dưới để tham khảo -->
#### (spec gốc T2)
> Nâng cấp [Autogens/Filter/FiltersGen.cs](../Autogens/Filter/FiltersGen.cs). **Điều tra trước khi đổi**: đọc dump `.other/ffmpeg filters ... n8.0.1 ...txt` để xác nhận format cờ thực tế (phiên trước thấy regex 2-cờ `^([TS.]{2})` match đúng với 8.0.1; xác minh lại trước khi sửa).
- **T2a** Cờ Command support `C` → `ICommandSupport`: hiện `GetFilterInterface` (FiltersGen.cs:~349-354) đã gắn `T`→`ITimelineSupport`, `S`→`ISliceThreading`; dòng `C`→`ICommandSupport` đang comment vì regex chỉ bắt 2 ký tự (`support[2]` out-of-range). Nếu dump có cờ thứ 3 → mở rộng `regex_filter` bắt cờ thứ 3, bật `ICommandSupport`. **Xác minh interface `ICommandSupport` tồn tại** (FFmpegArgs.Cores.Interfaces) trước khi dùng; chưa có thì tạo theo mẫu `ITimelineSupport`/`ISliceThreading`.
- **T2b** Hỗ trợ loại đang bỏ qua `N->N`, `|->N` (xem `.other/NotAutoGen_*.txt`). Cần base filter cho dynamic N in/out → **độ phức tạp cao**; nếu rủi ro lớn thì `⏭️ defer` và ghi rõ lý do, KHÔNG cố ép.
- **T2c** Nhận diện tham số expression → dùng `ExpressionValue` thay `string` (xem `FilterData`/`FilterFunctionGen`). Bắt đầu hẹp: chỉ param mô tả rõ là expression.
- Regen: prepend PATH 8.0.1 (§1) → `dotnet run --project Autogens ...` → ghi số `.g.cs` đổi (HR10) → build solution + 53 test → commit `feat(autogen): ...` (gộp đổi generator + output regen của cùng sub-step).
- Lưu ý: delete-loop [FiltersGen.cs:39-42](../Autogens/Filter/FiltersGen.cs#L39-L42) đang comment → filter bị gỡ sẽ để lại `.g.cs` cũ (không bật lại khi không giám sát).

### T3 — P4 còn lại (mỗi mục 1 task riêng, ưu tiên rủi ro thấp trước)
- **T3a** `-hwaccel`/`-hwaccel_device`/`-hwaccel_output_format`/`-init_hw_device`/`-filter_hw_device` (additive extension input+global). Điều tra (không trùng) filter `hwupload`/`hwdownload` đã auto-gen. Test arg-build. **AN TOÀN — ưu tiên.** · trạng thái: ✅ (commit `aeb2aad`, 70/70)
- **T3b** Validate đồ thị mạnh hơn. **ĐÃ LÀM opt-in additive, read-only** (không đụng throw core). · trạng thái: ✅ (commit `246dad2`, 78/78)
  - API `Validate(this IFFmpegArg)` → `IReadOnlyList<GraphValidationIssue>`. Check: input-map reuse (Warning), dangling output (Error/Info nếu AutoSink), no-input/no-output (Error). Bỏ qua (đã được type-system/ctor chặn sẵn): filter-output dùng 2 lần (ctor throw "Map is only one to one"), lệch kiểu audio/video (generic ImageMap/AudioMap).
- **T3c** Autogen → Roslyn Source Generator. **⏭️ DEFER** (đại tu kiến trúc, cần giám sát). · trạng thái: ⏭️
- **T3d** AOT/trim friendly. **⏭️ DEFER** (cross-cutting, cần phân tích reflection toàn repo). · trạng thái: ⏭️
- **T3e** API streaming nâng cao (RTMP/SRT/HLS helper). **⏭️ DEFER** (design-heavy). · trạng thái: ⏭️

## 5. Nhật ký iteration (append-only, mới nhất ở trên)

- `ALL DONE` — Hết task tự-động-được. Đã xong: T1 (ffprobe), T2 (điều tra), T3a (hwaccel), T3b (graph validation). **Còn lại đều DEFER có chủ đích, cần GIÁM SÁT/THIẾT KẾ** (không làm không giám sát): T2b (filter động N→N/|→N — base class mới), T3c (source-generator — đại tu), T3d (AOT/trim — cross-cutting), T3e (streaming API — design-heavy). Cron tự-resume `e945c43a` đã **xoá** (hết việc, tránh fire vô ích 7 ngày). Branch `auto/overnight-roadmap` đỉnh `246dad2`, **chưa push/tag** — chờ chủ máy review & merge. FFmpegArgs.Test: **78/78**.
- `T3b ✅` — graph validation opt-in read-only. `Validate(this IFFmpegArg)` trả list `GraphValidationIssue` (severity). Phát hiện: input-map reuse, dangling output, no-input/no-output. Không đụng hành vi throw core (filter-output reuse & lệch kiểu audio/video vốn đã bị ctor/generic chặn → bỏ qua, ghi chú). 3 file Extensions\Validation + test (8). Build 0 error; 78/78. Commit `246dad2` (+413).
- `STOP đêm` — Phiên chính (chủ máy ngủ) dừng tại đây sau T1/T2/T3a. Còn lại cho **cron 6:53 sáng (job e945c43a)**: **T3b** (validate đồ thị — chỉ làm opt-in additive, KHÔNG đổi throw core; rủi ro → defer). T3c/d/e đã defer (architectural/design-heavy, cần giám sát). T2b defer (architectural). Branch `auto/overnight-roadmap` đỉnh `aeb2aad`, chưa push/tag.
- `T3a ✅` — hwaccel options. Input ext (`-hwaccel`/`-hwaccel_device`/`-hwaccel_output_format`, `where T:BaseInput`) trong `InputOutputOptionsExtension.cs` cạnh `Re()`; global (`-init_hw_device`/`-filter_hw_device`) trong `Globals\AdvancedGlobalOptionsExtension.cs` (đã có stub TODO sẵn); enum `HardwareAccel` (12 method) trong `FFmpegArgs.Cores\Enums`. `hwupload` đã auto-gen (không trùng); `hwdownload`/`hwmap`/`hwupload_cuda` chưa gen (ngoài scope). Build 0 error; **70/70 test** (62+8). Commit `aeb2aad` (4 files, +275).
- `T2 ✅ (no-op)` — Điều tra "làm giàu filter". Baseline regen 8.0.1 = 0 diff. T2a bất khả thi (8.0.1 `-filters` không còn cột cờ `C`), T2c đã làm sẵn (ExpressionValue heuristic), T2b hoãn (architectural). KHÔNG sửa code — đúng directive "không thay đổi cũng chấp nhận được". Tree sạch tại `4a8b177`. T3: tách rủi ro → T3a hwaccel (làm), T3b validate (opt-in/defer), T3c/d/e defer. Tiếp theo: T3a.
- `T1 ✅` — ffprobe wrapper. Tạo project `FFprobeArgs` self-contained (FFprobeArg token-list + FFprobePrintFormat + FFprobeRender/Config/Result/Extensions + model JSON tối thiểu), đăng ký vào `FFmpegArgs.sln` (GUID mới), thêm ProjectReference + `FFprobeArgTest.cs` (9 test) vào `FFmpegArgs.Test`. Build solution 0 error; **62/62 test pass** (53 baseline + 9). Commit `deefb99` (14 files). FFprobeRender ĐÃ gồm (parse JSON qua System.Text.Json, netstandard2.0 thêm package có điều kiện). Chưa thêm execute-test vào `FFmpegArgs.Test.Render` (follow-up tuỳ chọn). Tiếp theo: T2 (làm giàu filter).
- `INIT` — Tạo branch `auto/overnight-roadmap` từ `dev` (đỉnh `e4f2637`). Viết tracker này. Baseline: FFmpegArgs.Test 53/53. Tiếp theo: T1 (ffprobe).
