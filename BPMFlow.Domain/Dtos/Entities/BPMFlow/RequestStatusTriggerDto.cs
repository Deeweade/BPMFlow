namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestStatusTriggerDto : BaseEntityDto
{
    public int RequestStatusId { get; set; }
    public string Trigger { get; set; }
}