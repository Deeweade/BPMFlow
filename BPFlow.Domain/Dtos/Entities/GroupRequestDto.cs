namespace BPFlow.Domain.Dtos.Entities;

public class GroupRequestDto : BaseEntityDto
{
    public string Name { get; set; }
    public int? BusinessProcessId { get; set; }
}