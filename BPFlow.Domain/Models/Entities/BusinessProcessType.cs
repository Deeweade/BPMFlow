namespace BPFlow.Domain.Models.Entities;

public class BusinessProcessType : BaseEntity
{
    public BusinessProcessType()
    {
        BusinessProcessStages = new HashSet<BusinessProcessStage>();
        GroupRequests = new HashSet<GroupRequest>();
    }

    public string? Name { get; set; }

    // BusinessProcessType -> BusinessProcessStage
    public ICollection<BusinessProcessStage> BusinessProcessStages { get; set; }

    // BusinessProcessType -> GroupRequest
    public ICollection<GroupRequest> GroupRequests { get; set; }
}