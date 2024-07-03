using BPFlow.Domain.Dtos.Entities;

namespace BPFlow.Domain.Dtos.Entities;

public class BusinessProcessStageDto : BaseEntityDto
{
    public string? Name { get; set; }
    public int? BusinessProcessTypeId { get; set; }
}