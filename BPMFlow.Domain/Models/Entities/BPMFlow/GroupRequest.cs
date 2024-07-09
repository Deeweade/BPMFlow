namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class GroupRequest : BaseEntity
{
    public GroupRequest()
    {
        AssignedRequests = new HashSet<AssignedRequest>();
        RequestStatuses = new HashSet<RequestStatus>();
        RequestStatusesOrders = new HashSet<RequestStatusesOrder>();
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
    }

    public string Name { get; set; }

    // GroupRequest -> BusinessProcess
    public int? BusinessProcessId { get; set; }
    public virtual BusinessProcess? BusinessProcess { get; set; }

    // GroupRequest -> AssignedRequest
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }

    // GroupRequest -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }

    // GroupRequest -> RequestStatusesOrder
    public virtual ICollection<RequestStatusesOrder> RequestStatusesOrders { get; set; }
    
    //GroupRequest -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }
}