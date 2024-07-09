namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class BusinessProcess : BaseEntity
{
    public BusinessProcess()
    {
        GroupRequests = new HashSet<GroupRequest>();
    }

    public string Name { get; set; }

    // BusinessProcessType -> GroupRequest
    public ICollection<GroupRequest> GroupRequests { get; set; }
}