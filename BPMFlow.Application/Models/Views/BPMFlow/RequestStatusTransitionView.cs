namespace BPMFlow.Application.Models.Views.BPMFlow;

public class RequestStatusTransitionView : BaseEntityView
{
    public int SourceStatusId { get; set; }
    public int NextStatusId { get; set; }
    public string Name { get; set; }
    public bool IsNextStageTransition { get; set; }
    public bool SkipValidation { get; set; }
    public int ResponsibleRoleId { get; set; }
}