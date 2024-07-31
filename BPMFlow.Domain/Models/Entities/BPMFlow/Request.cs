namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class Request : BaseEntity
{
    public Request()
    {
        RequestStatusTransitions = new HashSet<RequestStatusTransition>();
        RequestStatuses = new HashSet<RequestStatus>();
        ObjectRequests = new HashSet<ObjectRequest>();
    }

    public string Title { get; set; }

    // Request -> BusinessProcess
    public int BusinessProcessId { get; set; }
    public virtual BusinessProcess BusinessProcess { get; set; }

    // Request -> RequestStatusTransition
    public virtual ICollection<RequestStatusTransition> RequestStatusTransitions { get; set; }

    // Request -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }

    // Request -> ObjectRequest
    public virtual ICollection<ObjectRequest> ObjectRequests { get; set; }
}