[![](https://img.shields.io/nuget/v/soenneker.plex.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.openapiclient/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.plex.openapiclient/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.openapiclient/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.plex.openapiclient/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.plex.openapiclient.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.plex.openapiclient/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.plex.openapiclient/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.plex.openapiclient/actions/workflows/codeql.yml)

# Soenneker.Plex.OpenApiClient

A Kiota-generated client and model set for the Plex Media Server API.

## Installation

```bash
dotnet add package Soenneker.Plex.OpenApiClient
```

## Create a client

Use a dedicated `HttpClient` so the Plex token cannot be sent by unrelated requests:

```csharp
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Plex.OpenApiClient;

var httpClient = new HttpClient
{
    BaseAddress = new Uri("http://localhost:32400")
};
httpClient.DefaultRequestHeaders.Add("X-Plex-Token", plexToken);

var adapter = new HttpClientRequestAdapter(
    new AnonymousAuthenticationProvider(),
    httpClient: httpClient)
{
    BaseUrl = httpClient.BaseAddress.AbsoluteUri.TrimEnd('/')
};

var client = new PlexOpenApiClient(adapter);
```

`AnonymousAuthenticationProvider` is appropriate here because the dedicated HTTP client already supplies `X-Plex-Token`. Prefer HTTPS for a Plex server outside the local machine when available.

For dependency injection, configuration binding, and cached transport reuse, use [`Soenneker.Plex.OpenApiClientUtil`](https://www.nuget.org/packages/Soenneker.Plex.OpenApiClientUtil/) instead of constructing the adapter directly.

## Call the server

Request builders follow the API path. For example, retrieve the server identity:

```csharp
using Soenneker.Plex.OpenApiClient.Identity;

IdentityGetResponse? identity =
    await client.Identity.GetAsync(cancellationToken: cancellationToken);

Console.WriteLine(identity?.MediaContainer?.MachineIdentifier);
Console.WriteLine(identity?.MediaContainer?.Version);
```

Generated methods accept an optional request-configuration callback followed by a cancellation token. Item endpoints use indexers, and non-success responses are surfaced through Kiota exceptions.

The source is regenerated from the Plex OpenAPI document. Put custom behavior in a wrapper or separate partial files because direct edits to generated files can be replaced by the next update.
