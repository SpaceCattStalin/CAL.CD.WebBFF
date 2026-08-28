using BFF.Client.Dispatches;

namespace BFF.Services.Dispatches;

public class DispatchesService(IDispatchServiceClient dispatchServiceClient) : IDispatchesService
{
    public Task<DispatchResponse?> GetByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default)
    {
        return dispatchServiceClient.GetDispatchByIdAsync(dispatchId, cancellationToken);
    }
}
