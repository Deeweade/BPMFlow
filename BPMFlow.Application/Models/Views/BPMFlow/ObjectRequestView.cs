namespace BPMFlow.Application.Models.Views.BPMFlow;

public class ObjectRequestView : HistoryEntityView
{
    public int ObjectId { get; set; }
    public int AuthorEmployeeId { get; set; }
    public int ResponsibleEmployeeId { get; set; }
    public int RequestStatusId { get; set; }
    public int RequestStatusTransitionId { get; set; }
    public int PeriodId { get; set; }
    public int SystemId { get; set; }
    public int SystemObjectId { get; set; }
    public int EntityStatusId { get; set; }
}