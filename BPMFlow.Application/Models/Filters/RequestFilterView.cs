namespace BPMFlow.Application.Models.Filters;

public class RequestFilterView
{
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public bool WithSubordinates { get; set; }
}