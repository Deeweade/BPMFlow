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

    [HttpPost("transition")]
    public async Task<IActionResult> GetAvailableTransition(int code, string login)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(login);

        var objectRequest = await _service.GetAvailableTransitionByUser(code, login);

        return Ok(objectRequest);
    }
}