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
    private readonly IObjectRequestService _orService;
    private readonly IRequestStatusTransitionService _rstService;

    public ObjectRequestController(IObjectRequestService orService, IRequestStatusTransitionService rstService)
    {
        _orService = orService;
        _rstService = rstService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(ObjectRequestView objectRequestView)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);

        var objectRequest = await _orService.Create(objectRequestView);

        return Ok(objectRequest);
    }

    [HttpPost("create/bulk")]
    public async Task<IActionResult> BulkCreate(ObjectRequestBulkCreateView view)
    {
        if (view.EmployeeIds is null || view.ObjectRequests is null) throw new ArgumentNullException(nameof(view));

        var codes = await _orService.BulkCreate(view.EmployeeIds, view.ObjectRequests);

        return Ok(codes);
    }

    [HttpPost("getFiltered")]
    public async Task<IActionResult> GetFiltered(ObjectRequestsFilterView filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var requests = await _orService.GetByFilter(filter);

        return Ok(requests);
    }

    [HttpPost("changeStatus")]
    public async Task<IActionResult> ChangeStatus(ObjectRequestView objectRequestView)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);

        var requestStatusTransition = await _rstService.GetById(objectRequestView.RequestStatusTransitionId);

        var objectRequest = await _orService.ChangeStatus(objectRequestView, requestStatusTransition.NextStatusOrder);

        return Ok(objectRequest);
    }
}