using BFF.Client.Dispatches;

namespace BFF.Services.Dispatches;

public interface IDispatchesService
{
    Task<DispatchResponse?> GetByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default);
}
