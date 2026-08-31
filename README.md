[![](https://img.shields.io/nuget/v/soenneker.gitlab.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.gitlab.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.gitlab.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.gitlab.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.gitlab.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.gitlab.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.gitlab.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.gitlab.openapiclientutil/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.GitLab.OpenApiClientUtil

Reuse a configured `GitLabOpenApiClient` throughout an application without rebuilding the Kiota adapter for each request.

## Installation

```bash
dotnet add package Soenneker.GitLab.OpenApiClientUtil
```

## Configuration

```json
{
  "GitLab": {
    "ApiKey": "gitlab-token"
  }
}
```

GitLab.com is used by default. Self-managed host and custom-header settings are inherited from `Soenneker.GitLab.HttpClients`:

```json
{
  "GitLab": {
    "ApiKey": "gitlab-token",
    "ClientBaseUrl": "https://gitlab.example.com/",
    "AuthHeaderName": "PRIVATE-TOKEN",
    "AuthHeaderValueTemplate": "{token}"
  }
}
```

## Registration

```csharp
services.AddGitLabOpenApiClientUtilAsSingleton();
```

Use `AddGitLabOpenApiClientUtilAsScoped()` when the consumer should be scoped. The scoped utility still uses the singleton HTTP provider; disposing a scope releases its cached OpenAPI wrapper without removing the shared authenticated transport.

## Usage

```csharp
public sealed class GitLabService
{
    private readonly IGitLabOpenApiClientUtil _clients;

    public GitLabService(IGitLabOpenApiClientUtil clients)
    {
        _clients = clients;
    }

    public async ValueTask<GitLabOpenApiClient> GetClient(
        CancellationToken cancellationToken = default)
    {
        return await _clients.Get(cancellationToken);
    }
}
```

`Get` returns the same generated client for the lifetime of the utility. Authentication is supplied by the underlying HTTP provider, so the Kiota adapter does not add a second authorization header.
