using Soenneker.GitLab.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.GitLab.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class GitLabOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IGitLabOpenApiClientUtil _openapiclientutil;

    public GitLabOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IGitLabOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
