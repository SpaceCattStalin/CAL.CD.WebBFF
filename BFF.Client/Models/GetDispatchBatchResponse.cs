namespace BFF.Client.Dispatches;

public class GetDispatchBatchResponse(
    IEnumerable<DispatchResponse> Found,
    IEnumerable<Guid> NotFound,
    long Total)
{
    public IEnumerable<DispatchResponse> Found { get; init; } = Found;
    public IEnumerable<Guid> NotFound { get; init; } = NotFound;
    public long Total { get; init; } = Total;
}
