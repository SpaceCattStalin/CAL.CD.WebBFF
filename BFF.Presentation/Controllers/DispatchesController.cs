using System.Text.Json;
using BFF.Client.Dispatches;
using BFF.Services.Dispatches;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Presentation.Controllers;

[ApiController]
[Route("dispatch")]
public class DispatchesController(IDispatchesService dispatchesService) : ControllerBase
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    [HttpGet("{dispatchId:guid}")]
    public async Task<IActionResult> GetById(Guid dispatchId, CancellationToken cancellationToken)
    {
        var response = await dispatchesService.GetByIdAsync(dispatchId, cancellationToken);
        return ToActionResult(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateDispatchRequest request, CancellationToken cancellationToken)
    {
        var response = await dispatchesService.CreateAsync(request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            return Relay(response);
        }

        var dispatch = Deserialize(response);
        return Created($"/dispatch/{dispatch.DispatchId}", dispatch);
    }

    [HttpPut("{dispatchId:guid}")]
    public async Task<IActionResult> Update(Guid dispatchId, UpdateDispatchRequest request, CancellationToken cancellationToken)
    {
        var response = await dispatchesService.UpdateAsync(dispatchId, request, cancellationToken);
        return ToActionResult(response);
    }

    private static IActionResult ToActionResult(DownstreamResponse response)
    {
        if (!response.IsSuccessStatusCode)
        {
            return Relay(response);
        }

        return new ObjectResult(Deserialize(response)) { StatusCode = response.StatusCode };
    }

    private static ContentResult Relay(DownstreamResponse response) => new()
    {
        StatusCode = response.StatusCode,
        Content = response.RawBody,
        ContentType = response.ContentType
    };

    private static DispatchResponse Deserialize(DownstreamResponse response) =>
        JsonSerializer.Deserialize<DispatchResponse>(response.RawBody, JsonOptions)
            ?? throw new InvalidOperationException("CentralDispatch returned an empty success body.");
}
