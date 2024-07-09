namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestStatusTransitionDto : BaseEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? SourceStatusId { get; set; }
    public int? NextStatusId { get; set; }
    public string Name { get; set; }
    public bool IsNextStageTransition { get; set; }
    public int? ResponsibleRoleId { get; set; }
    public bool SkipValidation { get; set; }
}