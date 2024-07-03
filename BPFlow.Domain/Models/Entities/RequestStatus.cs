namespace BPFlow.Domain.Models.Entities;

public class RequestStatus : BaseEntity
{
    public RequestStatus()
    {
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
        RequestStatusesOrders = new HashSet<RequestStatusesOrder>();
        AssignedRequests = new HashSet<AssignedRequest>();
    }
    public int? ResponsibleRoleId { get; set; }

    // RequestStatus -> AssignedRequest
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }

    // RequestStatus -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }

    // RequestStatus -> RequestStatusesOrder
    public virtual ICollection<RequestStatusesOrder> RequestStatusesOrders { get; set; }

    // RequestStatus -> BusinessProcessStage
    public int? BusinessProcessStageId { get; set; }
    public BusinessProcessType? BusinessProcessTypes { get; set; }

    // RequestStatus -> GoalOwnershipType
    public int? GoalOwnershipTypeId { get; set; }
    public GoalOwnershipType? GoalOwnershipTypes { get; set;}
}