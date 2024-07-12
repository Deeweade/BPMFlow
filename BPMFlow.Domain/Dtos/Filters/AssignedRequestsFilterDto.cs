namespace BPMFlow.Domain.Dtos.Filters;

public class AssignedRequestsFilterDto
{
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public int? GroupRequestId { get; set; }
    public int? RequestStatusId { get; set; }
    public bool WithSubordinates { get; set; }

    public ICollection<int> PeriodIds { get; set; } = [];
    public ICollection<int> SubordinateEmployeeIds { get; set; } = [];
}