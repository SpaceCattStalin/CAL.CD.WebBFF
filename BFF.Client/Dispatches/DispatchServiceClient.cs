using System.Net.Http.Json;

namespace BFF.Client.Dispatches;

public class DispatchServiceClient(HttpClient httpClient) : IDispatchServiceClient
{
    public async Task<DownstreamResponse> GetDispatchByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"api/dispatch/{dispatchId}", cancellationToken);
        return await ToDownstreamResponseAsync(response, cancellationToken);
    }

    public async Task<DownstreamResponse> CreateDispatchAsync(CreateDispatchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/dispatch", request, cancellationToken);
        return await ToDownstreamResponseAsync(response, cancellationToken);
    }

    public async Task<DownstreamResponse> UpdateDispatchAsync(Guid dispatchId, UpdateDispatchRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"api/dispatch/{dispatchId}", request, cancellationToken);
        return await ToDownstreamResponseAsync(response, cancellationToken);
    }

    private static async Task<DownstreamResponse> ToDownstreamResponseAsync(HttpResponseMessage response, CancellationToken cancellationToken)
    {
        var rawBody = await response.Content.ReadAsStringAsync(cancellationToken);

        return new DownstreamResponse
        {
            StatusCode = (int)response.StatusCode,
            ContentType = response.Content.Headers.ContentType?.ToString(),
            RawBody = rawBody
        };
    }
}
