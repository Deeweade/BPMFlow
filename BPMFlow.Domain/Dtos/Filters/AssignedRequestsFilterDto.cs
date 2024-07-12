namespace BPMFlow.Domain.Dtos.Filters;

public class AssignedRequestsFilterDto
{
    public int EmployeeId { get; set; }
    public int PeriodId { get; set; }
    public int GroupRequestId { get; set; }
    public int RequestStatusId { get; set; }
    public bool WithSubordinates { get; set; }

    public List<int> PeriodIds { get; set; }
    public List<int> SubordinateEmployeeIds { get; set; }
}