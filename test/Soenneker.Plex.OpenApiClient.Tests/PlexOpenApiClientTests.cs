using Soenneker.Tests.HostedUnit;

namespace Soenneker.Plex.OpenApiClient.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class PlexOpenApiClientTests : HostedUnitTest
{
    public PlexOpenApiClientTests(Host host) : base(host)
    {
    }

    [Test]
    public void Default()
    {

    }
}
