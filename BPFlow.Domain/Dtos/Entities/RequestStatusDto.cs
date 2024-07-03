namespace BPFlow.Domain.Dtos.Entities;

public class RequestStatusDto : BaseEntityDto
{
    public int? BusinessProcessStageId { get; set; }
    public int? GoalOwnershipTypeId { get; set; }
    public int? ResponsibleRoleId { get; set; }
}