# Universal Agent Instructions

These instructions are global defaults for Codex agents across projects. They apply when a repository does not provide a stricter local rule.

## Project-Local Storage Override

For this repository, agent operational materials live inside the worktree but must stay ignored by Git.

- Keep `.agents/`, `TASKS.md`, `dev-diary/`, `docs/`, and `completed-tasks/` in `G:\Projects\2025\SDL3-CS`.
- Keep these paths listed in `.gitignore`.
- Do not stage or commit these operational materials unless the user explicitly asks to change that policy.

## Precedence
- A project-local `AGENTS.md` is authoritative for project-specific structure, commands, safety constraints, naming, and documentation rules.
- When this file and a local `AGENTS.md` conflict, follow the stricter safe rule. Prefer the project-specific rule when it narrows scope or adds domain constraints.
- The user's explicit current request may override workflow preferences, but it must not override safety, security, data-protection, or destructive-action constraints.
- If a repository has multiple nested instruction files, follow the most specific file for the files being changed.

## Default Human Language
- Keep this global instruction file in English.
- Write human-facing project Markdown in clear Russian by default unless the user or a project-local rule explicitly requires another language.
- Human-facing Markdown includes documentation, specifications, task files, completed-task archives, development diaries, README files, agent notes, check summaries, acceptance explanations, and prose descriptions of UI labels or workflow states.
- Keep English for exact technical identifiers, paths, commands, code symbols, API fields, package names, official product names, protocol names, and values that must match source contracts.
- When editing existing prose, preserve the repository's terminology and language conventions. Avoid mixed Russian-English prose labels when a normal Russian phrase works.

## Repository-First Workflow
- Inspect the repository before changing it. Start with local instructions, `README` or documentation indexes, `TASKS.md`, and nearby source or tests relevant to the request.
- Use fast search tools such as `rg --files` and `rg` when available.
- Let the existing structure, patterns, helper APIs, naming, and test style guide the implementation.
- Keep edits scoped to the requested behavior. Avoid unrelated refactors, formatting churn, dependency changes, and metadata churn unless they are necessary to complete the task safely.
- Prefer structured parsers and project APIs over ad hoc text manipulation when the repository or standard toolchain provides them.

## Wrapper Test Coverage
- Every function in the wrapper project must be covered 100% by automated tests. For this rule, "function" means every C# method declaration in `SDL3-CS/**/*.cs`, including overloads, public APIs, private native entry-point helpers, partial/extern methods, managed convenience wrappers, generic helpers, and marshalling helpers.
- Delegate declarations, enum members, struct fields, constants, and compiler-generated accessors are not functions for this rule unless the source file implements an explicit method body or method declaration for them.
- Tests for wrapper functions must live in `SDL3-CS.Tests/` and mirror the wrapper source structure exactly: a function from `SDL3-CS/<relative path>.cs` is covered from `SDL3-CS.Tests/<relative path>.cs`.
- Function coverage must be meaningful, not only a delegate-assignment smoke check. Cover success paths, failure paths, null/invalid inputs, overload resolution, marshalling shape, ownership/freeing behavior, and native entry-point metadata where applicable.
- A wrapper function task is not complete until the relevant coverage report proves 100% function coverage for that function and the focused test command is recorded in the task notes.
- Direct `LibraryImport` or `extern` stubs that emit no coverable managed IL and therefore do not appear as functions in the coverage report still require a dedicated mirrored test. Prove them with a direct invocation when safe, native metadata/ABI assertions, and a coverage report showing 100% coverage for the mirrored test method that exercises the stub.

## SDL XML Documentation Links
- In wrapper XML documentation prose, when referencing an SDL C identifier that has a managed SDL3-CS equivalent, use a `<see cref="..."/>` link to the managed symbol instead of leaving the raw C identifier as text.
- Example: write `<see cref="EventType.NotificationActionInvoked"/>` instead of `SDL_EVENT_NOTIFICATION_ACTION_INVOKED` when documenting notification events.
- This rule also applies when the upstream identifier was copied inside `<c>...</c>`: replace the whole code span with `<see cref="..."/>` when the managed symbol exists. For example, use `<see cref="WindowFlags.OpenGL"/>`, `<see cref="GLSwapWindow"/>`, `<see cref="RenderPresent"/>`, and `<see cref="HAPTIC_INFINITY"/>` instead of code-wrapped SDL constants or function names.
- In wrapper XML documentation, always write literal values as `<c>null</c>`, `<c>true</c>`, and `<c>false</c>` in prose, `<param>`, `<returns>`, and `<remarks>` text; do not leave bare `null`, `true`, or `false`.
- Do not double-wrap XML documentation literals: existing `<c>null</c>`, `<c>true</c>`, and `<c>false</c>` must stay exactly one `<c>` element, never `<c><c>...</c></c>`.
- In wrapper XML documentation prose, use the public C# parameter name instead of the original SDL C parameter name: for example, write `<c>srcSpec</c>` and `<c>dstSpec</c>`, not `<c>src_spec</c>` or `<c>dst_spec</c>`.
- Place XML documentation on the public managed wrapper that callers use. If an SDL entry point is implemented as a private `LibraryImport` stub plus delegate field plus public wrapper method, the `/// <code>extern ... SDL_Function(...);</code>` block and all `<summary>`, `<param>`, `<returns>`, `<threadsafety>`, `<since>`, and `<seealso>` tags must immediately precede the public wrapper method, not the private native stub. Match `<param>` names to that public method signature.
- Do not combine multiple SDL C declarations in a single XML documentation block. Each documented public wrapper method gets its own `<code>extern ... SDL_Function(...);</code>` line for that one SDL symbol; a second SDL symbol means the second declaration belongs on another public wrapper's docblock.
- Keep raw C identifiers unchanged inside `<code>` blocks, `EntryPoint` values, string literal constants, upstream C declarations, and places where SDL3-CS has no real managed equivalent to link to.
- Do not create fake managed references just to satisfy this rule; if no wrapper symbol exists, use `<c>SDL_NAME</c>` or leave the upstream C declaration intact according to the surrounding documentation style.
- Keep wrapper XML documentation warning-clean under `dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release`: fix invalid XML, unresolved `cref`, ambiguous `cref`, and `///` comments that are not attached to a real language element.

## SDL C API Porting Workflow
- Use this workflow when the user provides an original SDL, SDL_image, SDL_mixer, SDL_ttf, or SDL_shadercross C API declaration with a Doxygen comment and asks to add a new C# binding, update an existing binding, or align XML C# documentation with the original SDL documentation.
- Port declarations and documentation into the existing SDL3-CS style: find the correct file, add or update the member near the matching neighboring function, and convert the Doxygen comment to XML C# documentation.
- Follow repository rules first: inspect `AGENTS.md`, `TASKS.md`, `dev-diary/`, and `git status`. For production/runtime code changes, complete the Feature Gate required by this file.
- Treat the user-provided SDL C snippet as the authoritative source for the C declaration and documentation wording. Do not rewrite, shorten, or improve the prose except where XML/C# validity or repository documentation rules require it.
- Extract the C symbol from the declaration, for example `SDL_HasSVE2`, and check whether it already exists in C# with both the C symbol and likely managed name, for example `rg -n "SDL_HasSVE2|HasSVE2" SDL3-CS`.
- If the method exists, update only its signature and documentation when needed. If it does not exist, find the neighboring function from the snippet or nearest anchor, for example `SDL_HasNEON|HasNEON`, and insert the new member immediately after it.
- Choose the file from the existing structure, not from a guess:
  - `SDL_` core APIs usually live in `SDL3-CS/SDL/<category>/<header>/PInvoke.cs` inside `public static partial class SDL`.
  - `IMG_` APIs live in `SDL3-CS/Image/PInvoke.cs` inside `public partial class Image`.
  - `MIX_` APIs live in `SDL3-CS/Mixer/PInvoke.cs` inside `public partial class Mixer`.
  - `TTF_` APIs live in `SDL3-CS/TTF/PInvoke.cs` inside `public static partial class TTF`.
  - `SDL_ShaderCross_` APIs live in `SDL3-CS/ShaderCross/PInvoke.cs` inside `public partial class ShaderCross`.
- If the file or anchor is ambiguous, search for similar functions using header/domain words from the documentation. Ask the user only when the location cannot be chosen safely.

### SDL C# Binding Rules
- Keep the original C declaration as the first XML documentation line, for example:

```csharp
/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSVE2(void);</code>
```

- Use `LibraryImport` with the exact C symbol in `EntryPoint` and the same library constant used by neighboring functions:

```csharp
[LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSVE2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.I1)]
public static partial bool HasSVE2();
```

- If a direct `LibraryImport` signature is not the public API and the file uses a private native stub plus a public managed wrapper, keep the native plumbing first and put the XML documentation on the public wrapper:

```csharp
[ExcludeFromCodeCoverage]
[LibraryImport(SDLLibrary, EntryPoint = "SDL_wcstoll"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
private static partial long SDL_Wcstoll(IntPtr str, IntPtr endp, int @base);
private delegate long WcstollNative(IntPtr str, IntPtr endp, int @base);
private static WcstollNative WcstollNativeFunction = SDL_Wcstoll;

/// <code>extern SDL_DECLSPEC long long SDLCALL SDL_wcstoll(const wchar_t *str, wchar_t **endp, int base);</code>
/// <summary>
/// <para>Parse a <c>long long</c> from a wide string.</para>
/// </summary>
/// <param name="str">The <c>null</c>-terminated wide string to read. Must not be <c>null</c>.</param>
/// <param name="endp">If not <c>null</c>, the address of the first invalid wide character will be written to this pointer.</param>
/// <param name="base">The base of the integer to read.</param>
/// <returns>the parsed <c>long long</c>, or 0 if no number could be parsed.</returns>
/// <since>This function is available since SDL 3.6.0.</since>
public static long Wcstoll(string str, IntPtr endp, int @base)
{
    // managed marshalling wrapper
}
```

- Strip the library prefix from the public C# method name: `SDL_HasSVE2` -> `HasSVE2`, `IMG_Load` -> `Load`, `MIX_Init` -> `Init`, `TTF_OpenFont` -> `OpenFont`.
- Convert parameter names to local C# style: `mime_types` -> `mimeTypes`, `num_mime_types` -> `numMimeTypes`. Apply `SDL XML Documentation Links` and `SDL Doxygen To XML Rules` for XML parameter tags and prose references.
- For a C `bool` return, add `[return: MarshalAs(UnmanagedType.I1)]` when neighboring functions do so.
- For C `bool` parameters, use `[MarshalAs(UnmanagedType.I1)] bool` when neighboring functions do so.
- For `const char *` input parameters, usually use `[MarshalAs(UnmanagedType.LPUTF8Str)] string`.
- For returned strings, inspect nearby analogs first. This repository often uses a private `IntPtr SDL_Function()` plus a public wrapper that calls `Marshal.PtrToStringUTF8` and frees SDL-owned memory with `Free` only when the original documentation requires freeing.
- For pointers, opaque SDL handles, callback signatures, arrays, `size_t`, structs, and enums, copy the approach from neighboring functions in the same file. Do not invent new marshalling when a local analog exists.

### SDL Doxygen To XML Rules
- Port the original Doxygen text as literally as possible. Only make changes required for valid XML/C# and the authoritative rules in `SDL XML Documentation Links`.
- Use `SDL XML Documentation Links` as the single source for `<see cref="..."/>`, literal formatting, C# parameter names in prose, unresolved `cref`, and raw identifiers without managed equivalents.
- This subsection only defines how to map SDL Doxygen syntax into C# XML documentation:
  - remove `/**`, leading `*`, and `*/` markers;
  - replace Doxygen tags with XML tags;
  - escape XML-sensitive characters;
  - replace parameter names with C# names in XML tag attributes.
- Map Doxygen tags to XML:
  - intro text or `\brief` -> `<summary>`;
  - separate paragraphs inside summary -> `<para>...</para>`;
  - `\param name text` -> `<param name="csharpName">text</param>`;
  - `\returns text` -> `<returns>text</returns>`;
  - `\threadsafety text` -> `<threadsafety>text</threadsafety>`;
  - `\since text` -> `<since>text</since>`;
  - `\sa SDL_Foo` or `\see SDL_Foo` -> `<seealso cref="Foo"/>` when allowed by `SDL XML Documentation Links`.
- Preserve punctuation and version text from the original. If the original says `SDL 3.6.0.`, do not replace it with a neighboring function's version.
- When updating an existing method, replace only that method's documentation block and related signature attributes. Do not touch neighboring functions.

### SDL Porting Example
Input SDL C snippet:

```c
extern SDL_DECLSPEC bool SDLCALL SDL_HasNEON(void);

/**
 * Determine whether the CPU has SVE2 (Scalable Vector Extension 2).
 *
 * This is only relevant on ARM64 Linux. On other platforms it always returns
 * false.
 *
 * \returns true if the CPU has SVE2, false otherwise.
 *
 * \since This function is available since SDL 3.6.0.
 */
extern SDL_DECLSPEC bool SDLCALL SDL_HasSVE2(void);
```

Expected action:
- Find `HasNEON` in `SDL3-CS/SDL/Platform and CPU Information/cpuinfo/PInvoke.cs`.
- Add `HasSVE2` immediately after `HasNEON`.
- Keep `<code>` with `SDL_HasSVE2`.
- Convert the comment to XML:

```csharp
/// <code>extern SDL_DECLSPEC bool SDLCALL SDL_HasSVE2(void);</code>
/// <summary>
/// <para>Determine whether the CPU has SVE2 (Scalable Vector Extension 2).</para>
/// <para>This is only relevant on ARM64 Linux. On other platforms it always returns
/// false.</para>
/// </summary>
/// <returns><c>true</c> if the CPU has SVE2, <c>false</c> otherwise.</returns>
/// <since>This function is available since SDL 3.6.0.</since>
[LibraryImport(SDLLibrary, EntryPoint = "SDL_HasSVE2"), UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
[return: MarshalAs(UnmanagedType.I1)]
public static partial bool HasSVE2();
```

### SDL Porting Verification
- After code changes, run the narrow useful build:

```powershell
dotnet build .\SDL3-CS\SDL3-CS.csproj -c Release
```

- If `Image`, `Mixer`, `TTF`, `ShaderCross`, or native packages are touched, run the corresponding project build.
- Before the final response, check that `rg -n "C_SYMBOL|CSharpName" SDL3-CS` shows exactly the expected locations, `git status --short` has no unexpected files, and `TASKS.md` plus `dev-diary/` are updated when required by local rules.

## Worktree And Git Hygiene
- In a Git repository, check worktree status before editing and again before finalizing when files changed.
- Treat unknown changes as user work. Never revert, overwrite, or reformat unrelated changes unless the user explicitly asks.
- If user changes overlap with the requested area, read them carefully and work with them. Ask only when the overlap makes the task impossible or ambiguous.
- Do not run destructive commands such as hard resets, forced checkouts, recursive deletes, database wipes, or volume cleanup without explicit approval for that exact action.
- Stage, commit, push, tag, or open pull requests only when the user explicitly requests it. Stage only files related to the requested task.
- Commit messages in this repository must be written in English. Conventional technical prefixes are allowed, but both the subject and any human-readable body text must be English.

## Feature Gate
- Feature or runtime work means adding or changing product behavior, UI/API flows, domain rules, integrations, data flow, configuration behavior, startup behavior, production code paths, or user-facing behavior.
- Before implementing feature or runtime work, find or create a concrete specification in the project's specification area, normally `docs/specifications/<domain>/`. Architecture and design documents are useful context, but they do not replace a feature specification unless the user explicitly names them as the specification.
- If the repository lacks a specification structure, create the minimal `docs/specifications/<domain>/` structure and a concise specification before implementation when the requested behavior is clear. If the behavior is unclear, stop and ask or propose the missing specification instead of writing production code.
- Write or update automated tests that encode the specification acceptance criteria before changing production code. When practical, run the focused tests first and confirm they fail against the current implementation.
- After tests are in place, implement the production change with the smallest scope that satisfies the specification.
- Runtime or production code changes must also have implementation documentation in `docs/documentation/<domain>/`. If no suitable document exists, create one and update the documentation index when the project has one.
- A feature or runtime change is not complete until the specification, automated tests, implementation documentation, and code are aligned. If the project has no viable test toolchain, add and document one or mark implementation blocked before writing production code.

## Task Workflow
- Active tasks live in `TASKS.md`. Every substantive project change should be represented there before implementation.
- If `TASKS.md` is missing, create it with a minimal generic task template in the repository's default human language before making substantive changes.
- Task IDs should be stable and sequential, for example `T-0001`. Use local ISO 8601 timestamps with timezone offsets.
- Use the repository task markers `[ ]`, `[/]`, `[?]`, and `[x]` according to the strict `TASKS.md` format below. Mark only the task being worked on as `[/]`.
- Each task should be zero-context for another agent: include priority, dependencies, linked specs/docs/source files, a detailed brief, acceptance criteria, subtasks when useful, and agent notes.
- Acceptance criteria for code changes must explicitly require the specification, automated tests, and implementation documentation described in the Feature Gate.
- Do not close or archive a task just because implementation is finished. Close/archive only after the user explicitly accepts it.
- Completed tasks belong in `completed-tasks/` archives, not in the active task list and not as a future-work backlog.

### Strict `TASKS.md` Format
- For this repository, `TASKS.md` must follow the same structure as `G:\Projects\2026\portfolio-bot\TASKS.md`.
- The file must start with `# Активные задачи`.
- Required top-level sections, in order: introductory sentence, `## Статусы`, `## Нумерация`, `## Текущие задачи`, active task blocks, `## Шаблон новой задачи`.
- Task statuses are encoded only in task-heading markers: `[ ]` for open, `[/]` for in progress, `[?]` for blocked. Accepted closed tasks use `[x]` only after moving to `completed-tasks/`.
- Do not use metadata lines such as `- Статус: open`, `- Статус: in progress`, or English status values inside task blocks.
- Active task headings must use exactly this shape: `## T-XXXX [ ] P1: Заголовок задачи`.
- Priorities must be `P0`, `P1`, `P2`, or `P3`. Do not use `high`, `normal`, `low`, or other priority labels.
- Each task block must include these metadata lines before prose sections: `- Создана: ...`, `- Приоритет: ...`, `- Зависимости: ...`, `- Ссылки:` followed by nested bullets.
- Required task subsections, in order: `### Самодостаточное описание`, `### Критерии приёмки`, `### Подзадачи`, `### Заметки агента`.
- Acceptance criteria and subtasks must be Markdown checklists with `- [ ] ...`.
- `TASKS.md` holds only active tasks. Accepted tasks must be moved to `completed-tasks/` and removed from the active list after explicit user acceptance.

## Development Diary
- Every agent session that works in a repository must keep a development diary entry under `dev-diary/`.
- Use the local date for the file path: `dev-diary/YYYY/MM Month/DD-MM-YYYY.md`. Localize the month directory name to the repository's default human language; Russian is the default when no local rule says otherwise.
- If the diary structure is missing, create it before or while starting work.
- Daily diary files are append-only. If a daily file already exists, add the new entry strictly after the last existing entry. Do not reorder, repair, or rewrite older entries unless the user explicitly asks.
- Add or update the diary when starting work, after important discoveries or file changes, after meaningful checks, after commits or deployments, after blockers or scope changes, and before the final response.
- Every diary entry must include a chronological actions section with nested `HH:MM` bullets, plus concise context, changes, decisions, checks, and next steps.
- Before the final response, inspect the tail of the daily diary file and confirm the newest entry you added is the last entry.

### Strict Development Diary Format
- For this repository, daily diary files must follow the same structure as `G:\Projects\2026\portfolio-bot\dev-diary\2026\06 Июнь\10-06-2026.md`.
- The daily file must start with `# Дневник разработки: DD-MM-YYYY`.
- Each entry heading must use exactly this shape: `## HH:MM +03:00 - Agent: Codex`.
- Each entry body must contain exactly these top-level bullets, in order: `- Задача: ...`, `- Контекст: ...`, `- Действия:`, `- Изменения: ...`, `- Решения: ...`, `- Проверки: ...`, `- Далее: ...`.
- The `- Действия:` bullet must contain nested chronological action bullets in the form `  - HH:MM - ...`.
- Do not use diary subsections such as `### Контекст`, `### Действия`, `### Изменения`, `### Решения`, `### Проверки`, or `### Далее`.
- Preserve append-only diary behavior unless the user explicitly requests a format migration or cleanup.

## Testing And Verification
- Run the narrowest useful checks first, then broader checks when risk or blast radius justifies them.
- Use the repository's documented commands for linting, formatting, tests, builds, contract checks, and generated artifact verification.
- Do not claim a check passed unless it was run in the current work session. If a check cannot run, state the blocker and residual risk.
- For frontend changes, run the relevant lint/build/test command and visually verify the affected local experience when a local server or static file makes that practical.
- For generated artifacts, run the project's generation or consistency check and include resulting files only when they are part of the expected change.

## Docker, Deployment, And Heavy Checks
- Do not deploy, redeploy, rebuild, or replace a local or remote runtime stack unless the user explicitly asks for that action in the current conversation.
- Treat `docker compose up`, rebuilds, service recreation, deployment smoke runs, and equivalent stack-changing commands as deployment actions.
- After any Docker deploy, rebuild, one-off container run, or smoke run, clean up temporary Docker artifacts before the final response unless the user asks to keep them.
- Prefer project-scoped cleanup: inspect Docker usage before and after, remove stopped one-off containers for the current project, prune dangling build cache with the safe non-aggressive option, and prune dangling images when reported reclaimable.
- Never prune Docker volumes, database volumes, named Compose volumes, or run volume-destructive cleanup unless the user explicitly confirms data removal.
- Avoid running resource-heavy checks in parallel on a developer workstation. Run Docker builds, deployments, full test suites, frontend builds, browser automation, and contract generation sequentially unless the user explicitly asks for parallel stress testing.
- After browser automation or local servers started by the agent, close processes that are no longer needed unless the user asked to keep them running.

## Security And External Systems
- Never commit or expose real tokens, passwords, session secrets, private keys, production secret files, customer data, account exports, or other sensitive material.
- Keep secret configuration out of frontend bundles and public examples. Use non-secret example config files when examples are needed.
- Do not enable approval bypasses, destructive production actions, live external transactions, or irreversible side effects unless the user explicitly asks in the current conversation and the repository's safety rules allow it.
- Fail closed when credentials are missing, external payloads are invalid, permissions are unclear, or safety checks cannot be completed.
- Redact sensitive values in logs, summaries, screenshots, and final responses.

## Final Response
- Keep the final response concise and concrete: state what changed, which files matter, and which checks passed or could not run.
- For feature or runtime work, name the specification used or created, the implementation documentation updated, the tests added or changed, and the exact test command result or blocker.
- If a local server was started for the user, provide the URL and note whether it is still running.
- If work is blocked, state the blocker, what remains safe to do next, and any files already changed.
- Do not present unverified assumptions as completed work.
