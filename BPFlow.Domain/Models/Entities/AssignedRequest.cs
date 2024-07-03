namespace BPFlow.Domain.Models.Entities;

public class AssignedRequest : HistoryEntity
{
    public AssignedRequest()
    {

    }

    public int? Code { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public int? EntityStatusId { get; set; }

    // AssingedRequest -> GroupRequest one-to-many
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequests { get; set; }

    // AssignedRequest -> RequestStatus one-to-one
    public int? RequestStatusId { get; set; }
    public virtual RequestStatus? RequestStatuses { get; set; }
}