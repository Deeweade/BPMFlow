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

    [HttpGet("byRequestId/{requestStatusId}")]
    public async Task<IActionResult> GetStatusesByRequest(int requestStatusId)
    {
        ArgumentNullException.ThrowIfNull(requestStatusId);

        var objectRequest = await _service.GetStatusesByRequestStatus(requestStatusId);

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