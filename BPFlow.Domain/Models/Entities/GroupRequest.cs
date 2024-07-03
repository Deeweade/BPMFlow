namespace BPFlow.Domain.Models.Entities;

public class GroupRequest : BaseEntity
{
    public GroupRequest()
    {
        AssignedRequests = new HashSet<AssignedRequest>();
        RequestStatusesOrders = new HashSet<RequestStatusesOrder>();
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
    }

    public string? Name { get; set; }
    public int? BonusSchemeId { get; set; }
    public int? PeriodId { get; set; }

    // GroupRequest -> BusinessProcessType
    public int? BusinessProcessTypeId { get; set; }
    public virtual BusinessProcessType? BusinessProcessTypes { get; set; }

    // GroupRequest -> AssignedRequest 
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }

    // GroupRequest -> RequestStatusesOrder
    public virtual ICollection<RequestStatusesOrder> RequestStatusesOrders { get; set; }
    
    //GroupRequest -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }
}