namespace BPFlow.Application.Models.Views.BPFlow;

public class RequestStatusTransitionView : BaseEntityView
{
    public int? GroupRequestId { get; set; }
    public int? SourceStatusId { get; set; }
    public int? NextStatusId { get; set; }
    public string Name { get; set; }
    public bool IsNextStageTransition { get; set; }
    public bool SkipValidation { get; set; }
    public int? ResponsibleRoleId { get; set; }
}