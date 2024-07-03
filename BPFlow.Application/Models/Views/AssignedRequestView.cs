namespace BPFlow.Application.Models.Views;

public class AssignedRequestView : HistoryEntityView
{
    public int? Code { get; set; }
    public int? GroupRequestId { get; set; }
    public int? RequestStatusId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public int? EntityStatusId { get; set; }
}