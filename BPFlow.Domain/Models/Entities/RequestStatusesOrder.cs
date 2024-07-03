namespace BPFlow.Domain.Models.Entities;

public class RequestStatusesOrder : BaseEntity
{
    public RequestStatusesOrder()
    {

    }

    public int? StatusOrder { get; set; }
    public bool? IsFinalStatus { get; set; }

    // RequestStatusesOrder -> GroupRequest
    public int? GroupRequestId { get; set; }
    public virtual GroupRequest? GroupRequests { get; set; }

    // RequestStatusesOrder -> RequestStatus
    public int? RequestStatusId { get; set; }
    public virtual RequestStatus? RequestStatuses { get; set; }
}