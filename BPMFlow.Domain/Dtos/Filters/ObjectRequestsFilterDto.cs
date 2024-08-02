namespace BPMFlow.Domain.Dtos.Filters;

public class ObjectRequestsFilterDto
{
    public int? ObjectId { get; set; }
    public int? RequestId { get; set; }
    public int? PeriodId { get; set; }
    public int? RequestStatusId { get; set; }
    public bool WithSubordinates { get; set; }
    public int? SystemId { get; set; }
    public int? SystemObjectId { get; set; }

    public ICollection<int> PeriodIds { get; set; } = [];
    public ICollection<int> SubordinateEmployeeIds { get; set; } = [];
}