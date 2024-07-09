using BPMFlow.Domain.Models.Entities.BPMFlow;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

public class EntityStatus : BaseEntity
{
    public EntityStatus()
    {
        AssignedRequests = new HashSet<AssignedRequest>();
    }
    public string Name { get; set; }

    // EntityStatus -> AssignedRequest
    public virtual ICollection<AssignedRequest> AssignedRequests { get; set; }
}