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

### T2 — P2: Làm giàu filter generated  · trạng thái: ⬜ (RỦI RO CAO — chia sub-step, mỗi step 1 commit, mỗi step build+test xanh)
> Nâng cấp [Autogens/Filter/FiltersGen.cs](../Autogens/Filter/FiltersGen.cs). **Điều tra trước khi đổi**: đọc dump `.other/ffmpeg filters ... n8.0.1 ...txt` để xác nhận format cờ thực tế (phiên trước thấy regex 2-cờ `^([TS.]{2})` match đúng với 8.0.1; xác minh lại trước khi sửa).
- **T2a** Cờ Command support `C` → `ICommandSupport`: hiện `GetFilterInterface` (FiltersGen.cs:~349-354) đã gắn `T`→`ITimelineSupport`, `S`→`ISliceThreading`; dòng `C`→`ICommandSupport` đang comment vì regex chỉ bắt 2 ký tự (`support[2]` out-of-range). Nếu dump có cờ thứ 3 → mở rộng `regex_filter` bắt cờ thứ 3, bật `ICommandSupport`. **Xác minh interface `ICommandSupport` tồn tại** (FFmpegArgs.Cores.Interfaces) trước khi dùng; chưa có thì tạo theo mẫu `ITimelineSupport`/`ISliceThreading`.
- **T2b** Hỗ trợ loại đang bỏ qua `N->N`, `|->N` (xem `.other/NotAutoGen_*.txt`). Cần base filter cho dynamic N in/out → **độ phức tạp cao**; nếu rủi ro lớn thì `⏭️ defer` và ghi rõ lý do, KHÔNG cố ép.
- **T2c** Nhận diện tham số expression → dùng `ExpressionValue` thay `string` (xem `FilterData`/`FilterFunctionGen`). Bắt đầu hẹp: chỉ param mô tả rõ là expression.
- Regen: prepend PATH 8.0.1 (§1) → `dotnet run --project Autogens ...` → ghi số `.g.cs` đổi (HR10) → build solution + 53 test → commit `feat(autogen): ...` (gộp đổi generator + output regen của cùng sub-step).
- Lưu ý: delete-loop [FiltersGen.cs:39-42](../Autogens/Filter/FiltersGen.cs#L39-L42) đang comment → filter bị gỡ sẽ để lại `.g.cs` cũ (không bật lại khi không giám sát).

### T3 — P4 còn lại (chỉ làm nếu còn thời gian/token, mỗi mục 1 task riêng, ưu tiên rủi ro thấp)  · trạng thái: ⬜
- Validate đồ thị mạnh hơn (additive) → ưu tiên.
- `-hwaccel`/`hwupload`/`hwdownload`/`-init_hw_device` (additive extension).
- Autogen → Roslyn Source Generator (lớn, kiến trúc).
- AOT/trim friendly (annotation).
- API streaming nâng cao.

## 5. Nhật ký iteration (append-only, mới nhất ở trên)

- `T1 ✅` — ffprobe wrapper. Tạo project `FFprobeArgs` self-contained (FFprobeArg token-list + FFprobePrintFormat + FFprobeRender/Config/Result/Extensions + model JSON tối thiểu), đăng ký vào `FFmpegArgs.sln` (GUID mới), thêm ProjectReference + `FFprobeArgTest.cs` (9 test) vào `FFmpegArgs.Test`. Build solution 0 error; **62/62 test pass** (53 baseline + 9). Commit `deefb99` (14 files). FFprobeRender ĐÃ gồm (parse JSON qua System.Text.Json, netstandard2.0 thêm package có điều kiện). Chưa thêm execute-test vào `FFmpegArgs.Test.Render` (follow-up tuỳ chọn). Tiếp theo: T2 (làm giàu filter).
- `INIT` — Tạo branch `auto/overnight-roadmap` từ `dev` (đỉnh `e4f2637`). Viết tracker này. Baseline: FFmpegArgs.Test 53/53. Tiếp theo: T1 (ffprobe).
