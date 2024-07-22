namespace BPMFlow.Domain.Dtos.Entities.BPMFlow;

public class RequestStatusDto : BaseEntityDto
{
    public string Title { get; set; }
    public int RequestId { get; set; }
    public int ResponsibleRoleId { get; set; }
    public int StatusOrder { get; set; }
    public bool IsFinalApproved { get; set; }
    public bool IsFinalDenied { get; set; }
}