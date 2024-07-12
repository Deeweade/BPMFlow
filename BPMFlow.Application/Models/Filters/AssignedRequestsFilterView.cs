namespace BPMFlow.Application.Models.Filters;

public class AssignedRequestsFilterView
{
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public int? GroupRequestId { get; set; }
    public int? RequestStatusId { get; set; }
    public bool WithSubordinates { get; set; }
}