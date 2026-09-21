using BFF.Client.Companies;
using BFF.Client.Dispatches;

namespace BFF.Services.Companies;

public class CompaniesService(ICompanyServiceClient companyServiceClient) : ICompaniesService
{
    public Task<DownstreamResponse> GetCarriersAsync(CancellationToken cancellationToken = default)
    {
        return companyServiceClient.GetCarriersAsync(cancellationToken);
    }
}
