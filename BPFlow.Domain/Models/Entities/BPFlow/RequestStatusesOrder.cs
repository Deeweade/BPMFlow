namespace BPFlow.Domain.Models.Entities.BPFlow;

public class RequestStatusesOrder : BaseEntity
{
    public RequestStatusesOrder()
    {

    }

    public int? StatusOrder { get; set; }
    public bool IsFinalStatus { get; set; }

    // RequestStatusesOrder -> GroupRequest
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequest { get; set; }

    // RequestStatusesOrder -> RequestStatus
    public int? RequestStatusId { get; set; }
    public virtual RequestStatus? RequestStatus { get; set; }
}