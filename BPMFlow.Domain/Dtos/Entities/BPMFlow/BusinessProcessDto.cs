namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class BusinessProcessDto : BaseEntityDto
{
    public string Title { get; set; }
    public int SystemId { get; set; }
    public int SystemObjectId { get; set; }
}