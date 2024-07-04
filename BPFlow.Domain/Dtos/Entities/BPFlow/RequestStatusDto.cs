namespace BPFlow.Domain.Dtos.Entities.BPFlow;

public class RequestStatusDto : BaseEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? ResponsibleRoleId { get; set; }
    public string Name { get; set; }
}