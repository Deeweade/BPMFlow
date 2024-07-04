namespace BPFlow.Domain.Dtos.Entities.BPFlow;

public class RequestStatusesOrderDto : BaseEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? RequestStatusId { get; set; }
    public int? StatusOrder { get; set; }
    public bool IsFinalStatus { get; set; }
}