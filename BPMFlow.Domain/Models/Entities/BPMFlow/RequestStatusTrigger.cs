namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class RequestStatusTrigger : BaseEntity
{
    public string Trigger { get; set; }
    
    // RequestStatusTrigger -> RequestStatus
    public int RequestStatusId { get; set; }
    public virtual RequestStatus RequestStatus { get; set; }
}