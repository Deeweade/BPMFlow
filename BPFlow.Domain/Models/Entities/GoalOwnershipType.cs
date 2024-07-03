namespace BPFlow.Domain.Models.Entities;

public class GoalOwnershipType : BaseEntity
{
    public GoalOwnershipType()
    {
        RequestStatuses = new HashSet<RequestStatus>();
    }

    public string? Name { get; set; }

    // GoalOwnershipType -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
}