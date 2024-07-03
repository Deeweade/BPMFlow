namespace BPFlow.Application.Models.Views;

public class GroupRequestView : BaseEntityView
{
    public string? Name { get; set; }
    public int? BonusSchemeId { get; set; }
    public int? BusinessProcessTypeId { get; set; }
    public int? PeriodId { get; set; }
}