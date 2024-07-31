using BPMFlow.Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class RequestStatusTransitionController : ControllerBase
{
    private readonly IRequestStatusTransitionService _service;

    public RequestStatusTransitionController(IRequestStatusTransitionService service)
    {
        _service = service;
    }

    [HttpPost("transition/{code}")]
    public async Task<IActionResult> GetAvailableTransition(int code)
    {
        ArgumentNullException.ThrowIfNull(code);

        var login = User.Identity?.Name;

        var objectRequest = await _service.GetAvailableTransition(code, login);

        return Ok(objectRequest);
    }
}