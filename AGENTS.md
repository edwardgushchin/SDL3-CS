# Universal Agent Instructions

These instructions are global defaults for Codex agents across projects. They apply when a repository does not provide a stricter local rule.

## Project-Local Storage Override

For this repository, agent operational materials live inside the worktree and are tracked in Git.

- Keep `TASKS.md`, `dev-diary/`, `docs/`, and `completed-tasks/` in `G:\Projects\2025\SDL3-CS`.
- Do not list these paths in `.gitignore`.
- Stage and commit agent context when it is related to the task being completed.

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
