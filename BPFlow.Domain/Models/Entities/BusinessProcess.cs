namespace BPFlow.Domain.Models.Entities;

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