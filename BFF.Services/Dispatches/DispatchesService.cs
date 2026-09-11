using BFF.Client.Dispatches;
using BFF.Client.SearchService;

namespace BFF.Services.Dispatches;

public class DispatchesService(IDispatchServiceClient dispatchServiceClient, ISearchServiceClient searchServiceClient) : IDispatchesService
{
    public Task<DownstreamResponse> GetByIdAsync(Guid dispatchId, CancellationToken cancellationToken = default)
    {
        return dispatchServiceClient.GetDispatchByIdAsync(dispatchId, cancellationToken);
    }
    public Task<DownstreamResponse> CreateAsync(CreateDispatchRequest request, CancellationToken cancellationToken = default)
    {
        return dispatchServiceClient.CreateDispatchAsync(request, cancellationToken);
    }
    public Task<DownstreamResponse> UpdateAsync(Guid dispatchId, UpdateDispatchRequest request, CancellationToken cancellationToken = default)
    {
        return dispatchServiceClient.UpdateDispatchAsync(dispatchId, request, cancellationToken);
    }
    public Task<DownstreamResponse> SearchAsync(DispatchSearchRequestModel request, CancellationToken cancellationToken = default)
    {
        return searchServiceClient.SearchAsync(request, cancellationToken);
    }
}
