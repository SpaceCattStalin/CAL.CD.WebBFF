using System.Text.Json;
using BFF.Client.Companies;
using BFF.Client.Dispatches;
using BFF.Services.Companies;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Presentation.Controllers;

[ApiController]
[Route("company")]
public class CompaniesController(ICompaniesService companiesService) : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    [HttpGet("carriers")]
    public async Task<IActionResult> GetCarriers(CancellationToken cancellationToken)
    {
        var response = await companiesService.GetCarriersAsync(cancellationToken);
        return ToActionResult<IEnumerable<CarrierResponse>>(response);
    }

    [HttpGet("drivers")]
    public async Task<IActionResult> GetDrivers(CancellationToken cancellationToken)
    {
        var response = await companiesService.GetDriversAsync(cancellationToken);
        return ToActionResult<IEnumerable<DriverResponse>>(response);
    }

    private static IActionResult ToActionResult<T>(DownstreamResponse response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Relay(response);
        }

        return new ObjectResult(Deserialize<T>(response)) { StatusCode = response.StatusCode };
    }

    private static ContentResult Relay(DownstreamResponse response) => new()
    {
        StatusCode = response.StatusCode,
        Content = response.RawBody,
        ContentType = response.ContentType
    };

    private static T Deserialize<T>(DownstreamResponse response) =>
        JsonSerializer.Deserialize<T>(response.RawBody, JsonOptions)
            ?? throw new InvalidOperationException("CentralDispatch returned an empty success body.");
}
