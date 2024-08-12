using BPMFlow.Domain.Models.Entities.PerfManagement1;

namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class ObjectRequest : HistoryEntity
{
    public int ObjectId { get; set; }
    public int AuthorEmployeeId { get; set; }
    public int ResponsibleEmployeeId { get; set; }
    public int RequestId { get; set; }
    public int PeriodId { get; set; }
    public int EntityStatusId { get; set; }

    // ObjectRequest -> RequestStatus
    public int RequestStatusId { get; set; }
    public virtual RequestStatus RequestStatus { get; set; }
    
    // ObjectRequest -> RequestStatusTransition
    public int RequestStatusTransitionId { get; set; }
    public virtual RequestStatusTransition RequestStatusTransition { get; set; }
}