namespace BPMFlow.Application.Models.Views.BPMFlow;

public class RequestStatusTransitionView : BaseEntityView
{
    public int SourceStatusOrder { get; set; }
    public int RequestId { get; set; }
    public int NextStatusOrder { get; set; }
    public string Title { get; set; }
    public bool IsNextStageTransition { get; set; }
    public int ResponsibleRoleId { get; set; }
    public bool SkipValidation { get; set; }
}