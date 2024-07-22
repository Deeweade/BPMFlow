namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestDto : BaseEntityDto
{
    public string Title { get; set; }
    public int BusinessProcessId { get; set; }
}