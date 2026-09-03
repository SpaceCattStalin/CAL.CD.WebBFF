using BFF.Client.Dispatches;

namespace BFF.Services.Dispatches;

public interface IDispatchesService
{
    Task<DownstreamResponse> GetByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default);
    Task<DownstreamResponse> CreateAsync(CreateDispatchRequest request, CancellationToken cancellationToken = default);
    Task<DownstreamResponse> UpdateAsync(Guid dispatchId, UpdateDispatchRequest request, CancellationToken cancellationToken = default);
}
