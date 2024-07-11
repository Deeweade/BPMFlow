namespace BPMFlow.Application.Models.Views.BPMFlow;
public class RequestStatusesOrderView : BaseEntityView
{
    public int GroupRequestId { get; set; }
    public int RequestStatusId { get; set; }
    public int StatusOrder { get; set; }
    public bool IsFinalStatus { get; set; }
}