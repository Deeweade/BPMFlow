namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class GroupRequest : BaseEntity
{
    public GroupRequest()
    {
        AssignedRequests = new HashSet<AssignedRequest>();
        RequestStatuses = new HashSet<RequestStatus>();
    }

    public string Name { get; set; }

    // GroupRequest -> BusinessProcess
    public int BusinessProcessId { get; set; }
    public virtual BusinessProcess BusinessProcess { get; set; }

    // GroupRequest -> AssignedRequest
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }

    // GroupRequest -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
}