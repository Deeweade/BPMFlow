namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class RequestStatus : BaseEntity
{
    public string Title { get; set; }
    public int ResponsibleRoleId { get; set; }
    public int StatusOrder { get; set; }
    public bool IsFinalApproved { get; set; }
    public bool IsFinalDenied { get; set; }

    // public int RequestId { get; set; }
    // public virtual Request Request { get; set; }
    
    // RequestStatus -> ObjectRequest
    public virtual ICollection<ObjectRequest> ObjectRequests { get; set; }

    // RequestStatus -> RequestStatusTrigger
    public virtual ICollection<RequestStatusTrigger> RequestStatusTriggers { get; set; }
}