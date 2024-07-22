namespace BPMFlow.Application.Models.Views.BPMFlow;

public class RequestStatusView : BaseEntityView
{
    public string Title { get; set; }
    public int RequestId { get; set; }
    public int ResponsibleRoleId { get; set; }
    public int StatusOrder { get; set; }
    public bool IsFinalApproved { get; set; }
    public bool IsFinalDenied { get; set; }
}