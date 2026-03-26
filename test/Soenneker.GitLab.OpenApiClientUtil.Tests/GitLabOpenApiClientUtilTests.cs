using Soenneker.GitLab.OpenApiClientUtil.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.GitLab.OpenApiClientUtil.Tests;

[Collection("Collection")]
public sealed class GitLabOpenApiClientUtilTests : FixturedUnitTest
{
    private readonly IGitLabOpenApiClientUtil _openapiclientutil;

    public GitLabOpenApiClientUtilTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _openapiclientutil = Resolve<IGitLabOpenApiClientUtil>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
