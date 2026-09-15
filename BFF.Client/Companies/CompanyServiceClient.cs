using BFF.Client.Dispatches;

namespace BFF.Client.Companies;

public class CompanyServiceClient(HttpClient httpClient) : ICompanyServiceClient
{
    public async Task<DownstreamResponse> GetCarriersAsync(CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync("api/company/carriers", cancellationToken);
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
