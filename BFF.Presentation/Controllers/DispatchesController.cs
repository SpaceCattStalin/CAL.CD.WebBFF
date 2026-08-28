using BFF.Services.Dispatches;
using Microsoft.AspNetCore.Mvc;

namespace BFF.Presentation.Controllers;

[ApiController]
[Route("dispatch")]
public class DispatchesController(IDispatchesService dispatchesService) : ControllerBase
{
    [HttpGet("{dispatchId:guid}")]
    public async Task<IActionResult> GetById(Guid dispatchId, CancellationToken cancellationToken)
    {
        var dispatch = await dispatchesService.GetByIdAsync(dispatchId, cancellationToken);
        return dispatch is not null ? Ok(dispatch) : NotFound();
    }
}
