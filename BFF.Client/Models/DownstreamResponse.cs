namespace BFF.Client.Dispatches;

public class DownstreamResponse
{
    public required int StatusCode { get; init; }
    public string? ContentType { get; init; }
    public required string RawBody { get; init; }

    public bool IsSuccessStatusCode => StatusCode is >= 200 and < 300;
}
