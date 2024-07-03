namespace BPFlow.Application.Models.Views.BPFlow;

public class RequestStatusView : BaseEntityView
{
    public int? GroupRequestId { get; set; }
    public int? ResponsibleRoleId { get; set; }
    public string Name { get; set; }
}