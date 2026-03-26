using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.GitLab.HttpClients.Registrars;
using Soenneker.GitLab.OpenApiClientUtil.Abstract;

namespace Soenneker.GitLab.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class GitLabOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="GitLabOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddGitLabOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddGitLabOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IGitLabOpenApiClientUtil, GitLabOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="GitLabOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddGitLabOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddGitLabOpenApiHttpClientAsSingleton()
                .TryAddScoped<IGitLabOpenApiClientUtil, GitLabOpenApiClientUtil>();

        return services;
    }
}
