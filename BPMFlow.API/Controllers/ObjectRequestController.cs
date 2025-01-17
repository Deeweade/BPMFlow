using BPMFlow.API.Models.Other;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BPMFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Policy = "RequireAuthenticatedUser")]

public class ObjectRequestController(IObjectRequestService orService, IRequestStatusTransitionService rstService) : ControllerBase
{
    private readonly IObjectRequestService _orService = orService;
    private readonly IRequestStatusTransitionService _rstService = rstService;

    [HttpGet("byCode/{code}")]
    public async Task<IActionResult> GetActiveByCode(int code)
    {
        ArgumentNullException.ThrowIfNull(code);

        var objectRequest = await _orService.GetActiveByCode(code);
        
        return Ok(objectRequest);
    }

    [HttpGet("byResponsibleLogin/{login}")]
    public async Task<IActionResult> GetByResponsibleLogin(string login)
    {
        ArgumentNullException.ThrowIfNull(login);

        var objectRequests = await _orService.GetByResponsibleLogin(login);

        return Ok(objectRequests);
    }

    [HttpPost("getFiltered")]
    public async Task<IActionResult> GetFiltered(ObjectRequestsFilterView filter)
    {
        ArgumentNullException.ThrowIfNull(filter);

        var requests = await _orService.GetByFilter(filter);

        return Ok(requests);
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(ObjectRequestView objectRequestView)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);

        var login = User.Identity?.Name;

        var objectRequest = await _orService.Create(objectRequestView, login);

        return Ok(objectRequest);
    }

    [HttpPost("create/bulk")]
    public async Task<IActionResult> BulkCreate(ObjectRequestBulkCreateView view)
    {
        if (view.EmployeeIds is null || view.ObjectRequests is null) throw new ArgumentNullException(nameof(view));

        var login = User.Identity?.Name;

        var codes = await _orService.BulkCreate(view.EmployeeIds, view.ObjectRequests, login);

        return Ok(codes);
    }

    [HttpPost("changeStatus")]
    public async Task<IActionResult> ChangeStatus(ObjectRequestView objectRequestView)
    {
        ArgumentNullException.ThrowIfNull(objectRequestView);

        //перенести это в _orService
        var requestStatusTransition = await _rstService.GetById(objectRequestView.RequestStatusTransitionId);

        var objectRequest = await _orService.ChangeStatus(objectRequestView, requestStatusTransition.NextStatusOrder);

        return Ok(objectRequest);
    }

    [HttpPost("changeResponsibleEmployee/{newResponsibleEmployeeId}")]
    public async Task<IActionResult> ChangeResponsibleEmployee([FromBody] int[] requestCodes, int newResponsibleEmployeeId)
    {
        var objectRequests = await _orService.ChangeResponsibleEmployee(requestCodes, newResponsibleEmployeeId);

        return Ok(objectRequests);
    }
}