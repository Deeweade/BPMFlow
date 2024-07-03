namespace BPFlow.Application.Models.Views;

public class RequestStatusView : BaseEntityView
{
    public int? BusinessProcessStageId { get; set; }
    public int? GoalOwnershipTypeId { get; set; }
    public int? ResponsibleRoleId { get; set; }
}