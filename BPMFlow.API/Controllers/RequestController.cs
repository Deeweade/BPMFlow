using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class RequestController(IRequestService service) : ControllerBase
{
    private readonly IRequestService _service = service;

    [HttpPost("getFiltered")]
    public async Task<IActionResult> GetByFilter(RequestFilterView requestFilterView)
    {
        ArgumentNullException.ThrowIfNull(requestFilterView);

        var requests = await _service.GetByFilter(requestFilterView);

        return Ok(requests);
    }
}