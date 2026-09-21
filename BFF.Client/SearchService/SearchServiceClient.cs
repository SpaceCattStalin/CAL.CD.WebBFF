using System.Globalization;
using System.Net.Http.Json;
using BFF.Client.Dispatches;

namespace BFF.Client.SearchService;

public class SearchServiceClient(HttpClient httpClient) : ISearchServiceClient
{
    public async Task<DownstreamResponse> SearchAsync(DispatchSearchRequestModel request, CancellationToken cancellationToken = default)
    {
        var body = JsonContent.Create(request);

        var response = await httpClient.PostAsync($"api/dispatch/search", body, cancellationToken);
        
        return await ToDownstreamResponseAsync(response, cancellationToken);
    }

    private static string BuildQueryString(DispatchSearchRequestModel request)
    {
        var parameters = new List<string>();

        AddIfNotNull(parameters, nameof(request.PriceTotalMin), request.PriceTotalMin);
        AddIfNotNull(parameters, nameof(request.PriceTotalMax), request.PriceTotalMax);
        AddIfNotNull(parameters, nameof(request.PickupDateFrom), request.PickupDateFrom);
        AddIfNotNull(parameters, nameof(request.PickupDateTo), request.PickupDateTo);
        AddIfNotNull(parameters, nameof(request.DropoffDateFrom), request.DropoffDateFrom);
        AddIfNotNull(parameters, nameof(request.DropoffDateTo), request.DropoffDateTo);
        AddIfNotNull(parameters, nameof(request.DispatchStatus), request.DispatchStatus);
        AddIfNotNull(parameters, nameof(request.VehicleVin), request.VehicleVin);
        AddIfNotNull(parameters, nameof(request.Size), request.Size);
        AddIfNotNull(parameters, nameof(request.CurrentPage), request.CurrentPage);

        return parameters.Count == 0 ? string.Empty : $"?{string.Join('&', parameters)}";
    }

    private static void AddIfNotNull(List<string> parameters, string name, double? value)
    {
        if (value is not null)
            parameters.Add($"{name}={Uri.EscapeDataString(value.Value.ToString(CultureInfo.InvariantCulture))}");
    }

    private static void AddIfNotNull(List<string> parameters, string name, int? value)
    {
        if (value is not null)
            parameters.Add($"{name}={Uri.EscapeDataString(value.Value.ToString(CultureInfo.InvariantCulture))}");
    }

    private static void AddIfNotNull(List<string> parameters, string name, DateTime? value)
    {
        if (value is not null)
            parameters.Add($"{name}={Uri.EscapeDataString(value.Value.ToString("o", CultureInfo.InvariantCulture))}");
    }

    private static void AddIfNotNull(List<string> parameters, string name, string? value)
    {
        if (!string.IsNullOrEmpty(value))
            parameters.Add($"{name}={Uri.EscapeDataString(value)}");
    }

    private static void AddIfNotNull(List<string> parameters, string name, string[]? values)
    {
        if (values is null)
            return;

        foreach (var value in values)
        {
            if (!string.IsNullOrEmpty(value))
                parameters.Add($"{name}={Uri.EscapeDataString(value)}");
        }
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
