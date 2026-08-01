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

Mandatory checks must not be skipped, the exact release commit must be verified, and unverified packages must not be published even for an emergency hotfix. If a safe fix is not yet ready, the maintainer communicates the status and any available temporary mitigation instead of publishing a knowingly defective package.

## Branches and Main

Changes for the current release line follow this path:

1. Create a short-lived topic branch from the latest `origin/main` for each coherent change or tightly related group of changes.
2. Commit the implementation, documentation, and tests on that topic branch and push it to GitHub.
3. Open a pull request from the topic branch into `main`; direct writes to `main` are prohibited.
4. Merge the pull request only after successful CI and review of the applicable changes, then delete the topic branch.
5. Select the exact verified commit from `main` that contains every change assigned to the milestone and record its full 40-character SHA.
6. Build, validate, tag, and publish the release from that exact commit.

`main` is the only persistent branch in the SDL3-CS wrapper repository. Git tags and GitHub Releases preserve published history; persistent wrapper `release-*` and `preview-*` branches are not used. Native SDL-family forks retain their exact upstream `release-*` branches as required by the native fork alignment policy; those branches are not wrapper development branches.

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
- every included pull request is merged into `main`, and each included change is an ancestor of the selected release commit;
- CI for the included pull requests and the selected exact `main` commit completed successfully;
- the version, package scope, and release notes match the actual release contents;
- the readiness check completed successfully:

```powershell
pwsh .\.github\release-tools\Test-ReleaseReadiness.ps1 -FailOnError
```

For a managed-only release, only inapplicable native artifact or toolchain steps may be skipped. The reason for each skip must follow from the milestone and the actual diff; managed build, tests, package validation, and release-scope validation remain mandatory.

## Native Package Revision Consistency

Every native component package family must use one package revision across all supported platforms. A component family includes all platform-specific packages that share the same native component and version, such as `SDL3-CS.<Platform>.Image` packages for one SDL_image version.

- If any platform package requires a new revision because of a native binary, metadata, licensing, packaging, signing, or other package-content change, the same new revision must be reserved, produced, validated, and published for every supported platform package in that component family.
- A platform whose native payload did not otherwise change must be repacked from its previously verified package. Its native binaries and source revision must remain unchanged; only the package revision and unavoidable NuGet metadata may differ.
- Selective publication that leaves platform packages in the same component family on different revisions is prohibited.
- If the intended revision is already occupied for any package ID in the component family, the release must use the next revision that is available for every package ID in that family.
- Release readiness must verify that every supported platform package exists at the common revision before examples, shared dependency properties, release notes, or downstream consumers are updated to that revision and before the release is considered complete.

A packaging-only revision does not change the component's upstream `nativeVersion` or source commit. It changes only the final package revision shared by the complete platform family.

## GitHub Wiki Freshness

The public API reference in the GitHub Wiki is a generated release artifact. The public C# API and XML documentation from the exact release commit remain the source of truth.

- The accumulation task creates and verifies a Wiki candidate after the pull request is merged and its exact `main` commit is confirmed, but does not publish it.
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
- `Blocked` — a blocker, failing CI, an unverifiable release commit, or an incomplete mandatory check is present.

Scheduled tasks may collect facts and recommend an outcome, but they must not publish NuGet packages or a GitHub Release, perform a push or merge, or replace an explicit maintainer decision, except for the explicitly described fast-forward push of managed Wiki pages by the `sdl3-cs-2` task.
