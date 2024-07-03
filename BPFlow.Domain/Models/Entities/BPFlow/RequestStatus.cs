using BPFlow.Domain.Models.Entities.PerfManagement1;

namespace BPFlow.Domain.Models.Entities.BPFlow;

public class RequestStatus : BaseEntity
{
    public RequestStatus()
    {
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
        RequestStatusesOrders = new HashSet<RequestStatusesOrder>();
        AssignedRequests = new HashSet<AssignedRequest>();
    }

    public string Name { get; set; }

    //RequestStatus -> GroupRequest
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequests { get; set; }
    
    // RequestStatus
    public int? ResponsibleRoleId { get; set; }
    public virtual Role? Roles { get; set; }

    // RequestStatus -> AssignedRequest
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }

    // RequestStatus -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }

    // RequestStatus -> RequestStatusesOrder
    public virtual ICollection<RequestStatusesOrder> RequestStatusesOrders { get; set; }
}