namespace BFF.Client.Dispatches;

public interface IDispatchServiceClient
{
    Task<DispatchResponse?> GetDispatchByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default);
}
