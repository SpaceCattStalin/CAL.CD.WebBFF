using BFF.Client.Dispatches;

namespace BFF.Client.SearchService;

public interface ISearchServiceClient
{
    Task<DownstreamResponse> SearchAsync(DispatchSearchRequestModel request, CancellationToken cancellationToken = default);
}
