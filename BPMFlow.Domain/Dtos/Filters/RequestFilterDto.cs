namespace BPMFlow.Domain.Dtos.Filters;

public class RequestFilterDto
{
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public bool WithSubordinates { get; set; }

    public ICollection<int> SubordinateEmployeeIds { get; set; } = [];
}