using Soenneker.GitLab.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.GitLab.OpenApiClientUtil.Abstract;

/// <summary>
/// Provides a cached GitLab OpenAPI client backed by the shared authenticated HTTP provider.
/// </summary>
public interface IGitLabOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task containing the result of the operation.</returns>
    ValueTask<GitLabOpenApiClient> Get(CancellationToken cancellationToken = default);
}
