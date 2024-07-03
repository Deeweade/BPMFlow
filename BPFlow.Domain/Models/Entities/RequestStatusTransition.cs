namespace BPFlow.Domain.Models.Entities;

public class RequestStatusTransition : BaseEntity
{
    public RequestStatusTransition()
    {

    }

    public int? NextStatusId { get; set; }
    public string? Name { get; set; }
    public bool? IsNextStageTransition { get; set; }
    public int? ResponsibleRoleId { get; set; }
    public bool SkipValidation { get; set; }

    // RequestStatusTransition -> GroupRequest
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequests { get; set; }

    // RequestStatusTransition -> RequestStatus
    public int? SourceStatusId { get; set; }
    public virtual RequestStatus? RequestStatuses { get; set; }
}