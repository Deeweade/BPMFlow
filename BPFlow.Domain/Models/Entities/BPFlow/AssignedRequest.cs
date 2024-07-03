using BPFlow.Domain.Models.Entities.PerfManagement1;

namespace BPFlow.Domain.Models.Entities.BPFlow;

public class AssignedRequest : HistoryEntity
{
    public AssignedRequest()
    {

    }
    
    public int? ResponsibleEmployeeId { get; set; }
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }

    // AssingedRequest -> GroupRequest
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequests { get; set; }

    // AssignedRequest -> RequestStatus
    public int? RequestStatusId { get; set; }
    public virtual RequestStatus? RequestStatuses { get; set; }

    // AssignedRequest -> EntityStatus
    public int? EntityStatusId { get; set; }
    public virtual EntityStatus? EntityStatuses { get; set;}
}