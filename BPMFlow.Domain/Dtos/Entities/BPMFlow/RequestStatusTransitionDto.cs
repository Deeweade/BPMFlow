namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestStatusTransitionDto : BaseEntityDto
{
    public int SourceStatusOrder { get; set; }
    public int RequestId { get; set; }
    public int NextStatusOrder { get; set; }
    public string Title { get; set; }
    public bool IsNextOrderTransition { get; set; }
    public int ResponsibleRoleId { get; set; }
    public bool SkipValidation { get; set; }
}