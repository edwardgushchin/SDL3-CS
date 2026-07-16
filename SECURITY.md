# Security Policy

## Supported Versions

Security fixes are provided for the latest published SDL3-CS release line. Older release lines may not receive patches.

| Release line | Supported |
| --- | --- |
| `3.4.12.x` | Yes |
| Earlier releases | No |

## Reporting a Vulnerability

Do not open a public issue, discussion, or pull request for a suspected vulnerability.

Use [GitHub private vulnerability reporting](https://github.com/edwardgushchin/SDL3-CS/security/advisories/new) to send the maintainer:

- the affected package, version, platform, and runtime identifier;
- a clear description of the impact and attack scenario;
- minimal reproduction steps or a proof of concept;
- any known mitigation or upstream SDL advisory;
- whether the report or proof of concept has been disclosed elsewhere.

You should receive an initial acknowledgement within 72 hours. The maintainer will validate the report, coordinate with upstream projects when necessary, and provide progress updates at least weekly while the report remains active. Release timing depends on severity, upstream availability, and the affected native package matrix.

Please allow a reasonable remediation period before public disclosure. Credit will be offered unless you prefer to remain anonymous.

## Scope

This policy covers the managed SDL3-CS bindings, repository-owned release tooling, and native NuGet packages published from this repository. Vulnerabilities in upstream SDL projects should also be reported through the applicable upstream security process, but reports involving SDL3-CS packaging or bindings are still welcome here.
