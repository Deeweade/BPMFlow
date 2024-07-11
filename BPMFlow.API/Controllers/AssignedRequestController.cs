using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class AssignedRequestController : ControllerBase
{
    private readonly IAssignedRequestService _service;

    public AssignedRequestController(IAssignedRequestService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(AssignedRequestView assignedRequestView)
    {
        if (assignedRequestView is null) throw new ArgumentNullException(nameof(assignedRequestView));

        var assignedRequest = await _service.Create(assignedRequestView);

        return Ok(assignedRequest);
    }
}