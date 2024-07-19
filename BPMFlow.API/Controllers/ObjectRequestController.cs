using BPMFlow.API.Models.Other;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class ObjectRequestController : ControllerBase
{
    private readonly IObjectRequestService _service;

    public ObjectRequestController(IObjectRequestService service)
    {
        _service = service;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(ObjectRequestView assignedRequestView)
    {
        ArgumentNullException.ThrowIfNull(assignedRequestView);

        var assignedRequest = await _service.Create(assignedRequestView);

        return Ok(assignedRequest);
    }

    [HttpPost("create/bulk")]
    public async Task<IActionResult> BulkCreate(ObjectRequestBulkCreateView view)
    {
        if (view.EmployeeIds is null || view.ObjectRequests is null) throw new ArgumentNullException(nameof(view));

        var codes = await _service.BulkCreate(view.EmployeeIds, view.ObjectRequests);

        return Ok(codes);
    }

    [HttpPost("getFiltered")]
    public async Task<IActionResult> GetFiltered(ObjectRequestsFilterView filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var requests = await _service.GetByFilter(filter);

        return Ok(requests);
    }
}