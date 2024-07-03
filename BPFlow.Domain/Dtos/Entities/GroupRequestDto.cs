namespace BPFlow.Domain.Dtos.Entities;

public class GroupRequestDto : BaseEntityDto
{
    public string? Name { get; set; }
    public int? BonusSchemeId { get; set; }
    public int? BusinessProcessTypeId { get; set; }
    public int? PeriodId { get; set; }
}