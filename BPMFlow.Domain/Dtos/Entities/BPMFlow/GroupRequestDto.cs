namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class GroupRequestDto : BaseEntityDto
{
    public string Name { get; set; }
    public int BusinessProcessId { get; set; }
}