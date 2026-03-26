using Soenneker.GitLab.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.GitLab.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IGitLabOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<GitLabOpenApiClient> Get(CancellationToken cancellationToken = default);
}
