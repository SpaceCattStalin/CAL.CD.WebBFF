using System.Net;
using System.Net.Http.Json;

namespace BFF.Client.Dispatches;

public class DispatchServiceClient(HttpClient httpClient) : IDispatchServiceClient
{
    public async Task<DispatchResponse?> GetDispatchByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync($"dispatch/{dispatchId}", cancellationToken);

        if (response.StatusCode == HttpStatusCode.NotFound)
        {
            return null;
        }

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<DispatchResponse>(cancellationToken);
    }
}
