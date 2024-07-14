namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class RequestStatusesOrder : BaseEntity
{
    public RequestStatusesOrder()
    {

    }

    public int StatusOrder { get; set; }
    public bool IsFinalStatus { get; set; }

    // RequestStatusesOrder -> RequestStatus
    public int RequestStatusId { get; set; }
    public virtual RequestStatus RequestStatus { get; set; }
}