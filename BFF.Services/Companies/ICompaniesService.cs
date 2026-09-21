using BFF.Client.Dispatches;

namespace BFF.Services.Companies;

public interface ICompaniesService
{
    Task<DownstreamResponse> GetCarriersAsync(CancellationToken cancellationToken = default);
}
