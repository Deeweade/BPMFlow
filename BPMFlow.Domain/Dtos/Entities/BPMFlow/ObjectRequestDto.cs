namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class ObjectRequestDto : HistoryEntityDto
{
    public int ObjectId { get; set; }
    public int AuthorEmployeeId { get; set; }
    public int ResponsibleEmployeeId { get; set; }
    public int RequestId { get; set; }
    public int RequestStatusId { get; set; }
    public int RequestStatusTransitionId { get; set; }
    public int PeriodId { get; set; }
    public int EntityStatusId { get; set; }
}