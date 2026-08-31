using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.GitLab.HttpClients.Abstract;
using Soenneker.GitLab.OpenApiClientUtil.Abstract;
using Soenneker.GitLab.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.GitLab.OpenApiClientUtil;

public sealed class GitLabOpenApiClientUtil : IGitLabOpenApiClientUtil
{
    private readonly AsyncSingleton<GitLabOpenApiClient> _client;

    public GitLabOpenApiClientUtil(IGitLabOpenApiHttpClient httpClientUtil)
    {
        _client = new AsyncSingleton<GitLabOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();
            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new GitLabOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<GitLabOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    /// <summary>
    /// Releases resources used by the current instance.
    /// </summary>
    public void Dispose()
    {
        _client.Dispose();
    }

    /// <summary>
    /// Asynchronously releases resources used by the current instance.
    /// </summary>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
