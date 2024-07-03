using BPFlow.Domain.Dtos.Entities;

namespace BPFlow.Domain.Dtos.Entities;

public class RequestStatusTransitionDto : BaseEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? SourceStatusId { get; set; }
    public int? NextStatusId { get; set; }
    public string? Name { get; set; }
    public bool? IsNextStageTransition { get; set; }
    public bool SkipValidation { get; set; }
    public int? ResponsibleRoleId { get; set; }
}