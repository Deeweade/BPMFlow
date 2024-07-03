namespace BPFlow.Domain.Models.Entities;

public class BusinessProcessStage : BaseEntity
{
    public BusinessProcessStage()
    {
        RequestStatuses = new HashSet<RequestStatus>();
    }

    public string? Name { get; set; }
    
    // BusinessProcessStage -> BusinessProcessType
    public int? BusinessProcessTypeId { get; set; }
    public virtual BusinessProcessType? BusinessProcessTypes { get; set; }

    // BusinessProcessStage -> RequestStatus
    public virtual ICollection<RequestStatus> RequestStatuses { get; set; }
}