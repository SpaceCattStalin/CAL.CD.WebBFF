using BFF.Client.Dispatches;

namespace BFF.Client.Companies;

public interface ICompanyServiceClient
{
    Task<DownstreamResponse> GetCarriersAsync(CancellationToken cancellationToken = default);
    Task<DownstreamResponse> GetDriversAsync(CancellationToken cancellationToken = default);
}
