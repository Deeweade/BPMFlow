namespace BPFlow.Domain.Dtos.Entities;

public class AssignedRequestDto : HistoryEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? RequestStatusId { get; set; }
    public int? ResponsibleEmployeeId { get; set; }
    public int? EmployeeId { get; set; }
    public int? PeriodId { get; set; }
    public int? EntityStatusId { get; set; }
}