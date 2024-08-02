namespace BPMFlow.Application.Models.Filters;

public class ObjectRequestsFilterView
{
    public int? ObjectId { get; set; }
    public int? RequestId { get; set; }
    public int? PeriodId { get; set; }
    public int? RequestStatusId { get; set; }
    public bool WithSubordinates { get; set; }
    public int? SystemId { get; set; }
    public int? SystemObjectId { get; set; }
}