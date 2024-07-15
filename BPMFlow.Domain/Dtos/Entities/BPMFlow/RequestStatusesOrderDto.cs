namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestStatusesOrderDto : BaseEntityDto
{
    public int RequestStatusId { get; set; }
    public int StatusOrder { get; set; }
    public bool IsFinalStatus { get; set; }
}