using System.ComponentModel.DataAnnotations.Schema;
using BPMFlow.Domain.Models.Entities.PerfManagement1;

namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class RequestStatusTransition : BaseEntity
{
    public RequestStatusTransition()
    {

    }

    public int NextStatusId { get; set; }
    public string Name { get; set; }
    public bool IsNextStageTransition { get; set; }
    public bool SkipValidation { get; set; }

    // RequestStatusTransition -> GroupRequest
    public int GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequest { get; set; }

    // RequestStatusTransition -> RequestStatus
    public int SourceStatusId { get; set; }
    public virtual RequestStatus? RequestStatus { get; set; }

    // RequestStatusTransition
    public int ResponsibleRoleId { get; set; }
    public virtual Role? Role { get; set; }
}