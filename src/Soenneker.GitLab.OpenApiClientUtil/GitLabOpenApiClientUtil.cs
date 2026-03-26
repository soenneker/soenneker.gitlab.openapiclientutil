using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.GitLab.HttpClients.Abstract;
using Soenneker.GitLab.OpenApiClientUtil.Abstract;
using Soenneker.GitLab.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.GitLab.OpenApiClientUtil;

///<inheritdoc cref="IGitLabOpenApiClientUtil"/>
public sealed class GitLabOpenApiClientUtil : IGitLabOpenApiClientUtil
{
    private readonly AsyncSingleton<GitLabOpenApiClient> _client;

    public GitLabOpenApiClientUtil(IGitLabOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<GitLabOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("GitLab:ApiKey");
            string authHeaderValueTemplate = configuration["GitLab:AuthHeaderValueTemplate"] ?? "{token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerValue: authHeaderValue), httpClient: httpClient);

            return new GitLabOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<GitLabOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
