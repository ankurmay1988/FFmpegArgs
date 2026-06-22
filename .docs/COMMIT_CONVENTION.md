# Commit Message Convention

The `CHANGELOG.md` of this repository is generated automatically from commit messages by
[git-cliff](https://git-cliff.org) (see [`cliff.toml`](../cliff.toml)). A commit only shows up
in the changelog if its message follows the rules below — otherwise it is silently dropped.

## 1. Language: English only (mandatory)

**Every commit message MUST be written in English** — subject, body and footer. This is a hard
rule: the package is public, the changelog is English, and mixed-language history is rejected in
review. Code identifiers are English too, so commits stay consistent with the codebase.

> ❌ `fix: sửa lỗi định dạng số theo culture`
> ✅ `fix(culture): format numeric options with InvariantCulture`

## 2. Format (Conventional Commits 1.0.0)

```
<type>(<scope>)<!>: <subject>

<body>          # optional

<footer>        # optional
```

- `<type>` — required, one of the types in the table below.
- `(<scope>)` — optional, the area/module touched (lowercase). See [Scope](#4-scope).
- `<!>` — optional `!` right before the colon to flag a breaking change.
- `<subject>` — required, see [Subject](#5-subject).

## 3. Types

These are the exact types `cliff.toml` recognizes. The changelog prints the type inline, e.g.
`- *(feat)* **(muxers)** Add MovMuxer ...`.

| Type       | Use for                                                        | In changelog |
|------------|----------------------------------------------------------------|:------------:|
| `feat`     | A new feature / public API addition                            | `(feat)`     |
| `fix`      | A bug fix                                                      | `(fix)`      |
| `perf`     | A performance improvement (no behavior change)                | `(perf)`     |
| `refactor` | Code change that neither fixes a bug nor adds a feature       | `(refactor)` |
| `docs`     | Documentation only (README, `.docs/`, XML doc comments)       | `(docs)`     |
| `test`     | Adding or fixing tests                                        | `(test)`     |
| `build`    | Build system, packaging, nuspec, NuGet, GitVersion            | `(build)`    |
| `ci`       | CI workflows (`.github/workflows/`)                           | `(ci)`       |
| `style`    | Formatting / whitespace only, no code meaning change          | `(style)`    |
| `revert`   | Reverting a previous commit                                   | `(revert)`   |
| `chore`    | Maintenance that doesn't fit the above                        | `(chore)`    |

Anything else that is still a valid conventional commit lands under `(other)`. **Non-conventional
messages (no recognized `type:`) are filtered out of the changelog entirely** — including merge
commits and raw one-line messages.

### Skipped on purpose

These never appear in the changelog (configured in `cliff.toml`):

- `chore(release): ...` — reserved for release/version-bump bookkeeping.
- `chore(deps): ...` — dependency bumps.

## 4. Scope

Optional but **strongly recommended** — it groups related work and reads well in the changelog.
Use a short, lowercase noun for the touched area. Keep scopes consistent; reuse existing ones.

Examples already used in this repo: `culture`, `muxers`, `demuxers`, `ffplay`, `codec`,
`subtitle`, `options`, `input`, `execute`, `autogen`, `symbol`, `roadmap`.

## 5. Subject

- **Imperative mood**: "add", "fix", "route" — not "added" / "adds".
- **English**, concise (aim for ≤ 72 chars), no trailing period.
- Describe *what* changed; put the *why* in the body if needed.

The changelog capitalizes the first letter automatically, so you don't need to.

## 6. Breaking changes

Mark a breaking change either way (the changelog tags it `[breaking]`):

- Add `!` before the colon: `feat(api)!: rename FilterGraph to Filtergraph`
- Or add a footer:
  ```
  feat(api): rename FilterGraph to Filtergraph

  BREAKING CHANGE: FilterGraph is now Filtergraph; update all call sites.
  ```

## 7. How it maps to the changelog & releases

This repo tags **`vMAJOR.MINOR.0`** to open a minor line; GitVersion then versions every following
commit as `MAJOR.MINOR.<commits-since-tag>`. The changelog reflects that:

- One section per minor line, e.g. `## [2.3.x] — current`.
- Inside a line, commits are a **timeline** — newest on top, oldest at the bottom.
- The line still in progress is marked `— current`.

So a clean commit history *is* the release notes. Write each message as the line you want your
future self to read in the changelog.

## 8. Examples

```text
feat(codec): add audio encoder wrappers (aac, libmp3lame, ac3, flac)
fix(culture): format numeric options with InvariantCulture
refactor(demuxers): rename *Demuxer extension methods to *Demux
docs: mark demuxer/muxer consolidation as done
build(symbol): embed PDB + sources into the DLL, drop loose .pdb
ci: add GitHub Actions workflow for the no-ffmpeg unit tests
feat(api)!: rename FilterGraph to Filtergraph     # breaking
```

Bad (won't show / rejected):

```text
update stuff                 # no type  -> dropped from changelog
sửa lỗi culture              # not English -> rejected in review
Fixed the bug.               # no type, past tense, trailing period
```

## 9. Regenerating the changelog

```powershell
.\Changelog.ps1              # rebuild CHANGELOG.md (from the 2.1 line onward)
.\Changelog.ps1 -Unreleased  # preview only the current line's commits
```

See [`cliff.toml`](../cliff.toml) for the exact parsing rules.
