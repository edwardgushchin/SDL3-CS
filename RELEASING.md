# SDL3-CS Release Policy

This document defines the process for preparing SDL3-CS patch and hotfix releases. It applies to the managed wrapper, native NuGet packages, release tooling, and GitHub Releases. Instructions for reporting vulnerabilities are available in the [security policy](SECURITY.md).

## Severity Levels

Every confirmed defect must have exactly one severity label. The target release window starts when the fix has been implemented, verified, and declared ready for release.

| Label | Criteria | Target release window |
| --- | --- | --- |
| `severity: critical` | A vulnerability, memory or data corruption, an ABI defect causing widespread crashes, or the inability to install or load a published package | Emergency hotfix within 24 hours |
| `severity: high` | A serious regression or an unavailable key capability with no practical workaround | Release within 2–3 days |
| `severity: medium` | A localized defect, missing binding, or missing typed API for which a workaround exists | The next scheduled patch release, usually within 7–14 days |
| `severity: low` | Documentation, API usability, or minor behavior with no significant effect on usage | The next appropriate scheduled release |

The `release: blocker` label is applied separately from severity when an issue makes a specific release unsafe or technically impossible. Publication is prohibited while a milestone contains an open blocker.

## Scheduled Release Cadence

A scheduled patch release is prepared when at least one user-facing fix is available and any one of the following conditions is met:

- 3–5 verified fixes have accumulated;
- 14 calendar days have passed since the previous patch release in the current release line;
- a `severity: high` fix is ready and cannot wait for the regular release window.

Documentation, workflow-only changes, and GitHub Actions updates do not require a new NuGet release by themselves. Empty releases are not created solely because a calendar date has been reached.

## Emergency Hotfix

A `severity: critical` issue overrides the regular schedule. An emergency release must contain the minimum necessary change, regression tests, and only compatible accompanying fixes that have already passed verification. The reason for the expedited release and any residual risks must be stated in the release notes.

Mandatory checks must not be skipped, branch parity must not be violated, and unverified packages must not be published even for an emergency hotfix. If a safe fix is not yet ready, the maintainer communicates the status and any available temporary mitigation instead of publishing a knowingly defective package.

## Branches and Main Parity

Changes for the current release line follow this path:

1. The fix, documentation, and tests are created and committed on the active `release-*` branch.
2. The `release-*` branch is pushed to GitHub.
3. A pull request is opened from `release-*` into `main`; direct writes to `main` are prohibited.
4. The pull request is merged only after successful CI and review of the applicable changes.
5. Before publication, confirm that the release commit or an equivalent change is already present in `main`.
6. The release is published from the verified release branch only after an explicit maintainer decision.

A release branch must not contain release-only fixes that are absent from `main`.

## Milestones and Labels

Every planned release has a separate milestone with the exact version and target date. An issue or pull request is included in a milestone only when the change is genuinely intended for that release.

- `bug`, `enhancement`, `documentation`, and similar labels describe the type of work.
- `severity: *` describes the impact of a confirmed defect and does not replace the type label.
- `release: blocker` prohibits publication until the issue is closed or explicitly removed from the milestone.
- Workflow-only Dependabot pull requests are not added to a managed patch release milestone unless they resolve a build or publication problem affecting that release.

## Release Readiness

Before publication, the maintainer confirms that:

- the milestone contains no open `release: blocker` items, and every included change has a clear status;
- `severity: critical` and `severity: high` issues associated with the release are closed or explicitly deferred with a documented reason;
- the specification, implementation documentation, wrapper, and automated tests are aligned for every runtime or API change;
- CI for the target release branch and the pull request into `main` completed successfully;
- the release branch and `main` have the required parity;
- the version, package scope, and release notes match the actual release contents;
- the readiness check completed successfully:

```powershell
pwsh .\.github\release-tools\Test-ReleaseReadiness.ps1 -FailOnError
```

For a managed-only release, only inapplicable native artifact or toolchain steps may be skipped. The reason for each skip must follow from the milestone and the actual diff; managed build, tests, package validation, and release-scope validation remain mandatory.

## GitHub Wiki Freshness

The public API reference in the GitHub Wiki is a generated release artifact. The public C# API and XML documentation from the exact release commit remain the source of truth.

- The accumulation task creates and verifies a Wiki candidate after merge and main parity, but does not publish it.
- Before the production release, the release task rebuilds the exact release commit, generates the eight managed Wiki pages, and publishes them to `SDL3-CS.wiki.git` with a regular fast-forward push to `master`.
- Every managed page must contain the same managed version, full source commit, and UTC generation timestamp.
- After the push, a fresh read-only verification of the remote Wiki against the exact version, source commit, and SHA-256 content hash is mandatory.
- A matching content hash must not create a duplicate Wiki commit.

A Wiki publication failure, stale metadata, mismatched content hash, or inability to verify the remote Wiki blocks the production workflow. Until that blocker is resolved, NuGet packages and the GitHub Release must not be published, issues must not be commented on or closed, and the milestone must not be closed.

The fast-forward push of the eight managed Wiki pages by the `sdl3-cs-2` task is a narrow exception to the general prohibition on scheduled tasks performing pushes. This exception applies only after a verified Wiki candidate and does not authorize changes to repository branches, source files, issues, or milestones.

## Release Decision

The readiness review produces one of three outcomes:

- `Release now` — the release threshold has been met, no blockers remain, and all mandatory checks have passed;
- `Wait` — verified fixes are ready, but the regular threshold has not yet been met and there is no urgency;
- `Blocked` — a blocker, failing CI, branch parity violation, or incomplete mandatory check is present.

Scheduled tasks may collect facts and recommend an outcome, but they must not publish NuGet packages or a GitHub Release, perform a push or merge, or replace an explicit maintainer decision, except for the explicitly described fast-forward push of managed Wiki pages by the `sdl3-cs-2` task.
