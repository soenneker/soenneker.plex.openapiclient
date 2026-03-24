using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Plex.OpenApiClient.Tests;

[Collection("Collection")]
public sealed class PlexOpenApiClientTests : FixturedUnitTest
{
    public PlexOpenApiClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
    }

    [Fact]
    public void Default()
    {

    }
}
