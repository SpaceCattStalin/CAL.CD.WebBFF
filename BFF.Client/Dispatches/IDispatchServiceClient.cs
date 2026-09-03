namespace BFF.Client.Dispatches;

public interface IDispatchServiceClient
{
    Task<DownstreamResponse> GetDispatchByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default);
    Task<DownstreamResponse> CreateDispatchAsync(CreateDispatchRequest request, CancellationToken cancellationToken = default);
    Task<DownstreamResponse> UpdateDispatchAsync(Guid dispatchId, UpdateDispatchRequest request, CancellationToken cancellationToken = default);
}
