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

    [HttpGet("byRequestId/{requestId}")]
    public async Task<IActionResult> GetStatusesByRequest(int requestId)
    {
        ArgumentNullException.ThrowIfNull(requestId);

        var objectRequest = await _service.GetStatusesByRequest(requestId);

        return Ok(objectRequest);
    }

    [HttpGet("byCode/{code}")]
    public async Task<IActionResult> GetStatusesByCode(int code)
    {
        ArgumentNullException.ThrowIfNull(code);

        var statuses = await _service.GetStatusesByCode(code);

        return Ok(statuses);
    }
}