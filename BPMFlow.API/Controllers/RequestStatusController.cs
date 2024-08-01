using BPMFlow.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class RequestStatusController(IRequestStatusService service) : ControllerBase
{
    private readonly IRequestStatusService _service = service;

    [HttpPost("getStatusByRequest/{requestId}")]
    public async Task<IActionResult> GetStatusByRequest(int requestId)
    {
        ArgumentNullException.ThrowIfNull(requestId);

        var objectRequest = await _service.GetStatusByRequest(requestId);

        return Ok(objectRequest);
    }
}