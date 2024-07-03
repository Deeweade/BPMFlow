using System.Runtime.CompilerServices;

namespace BPFlow.Domain.Dtos.Entities;

public class RequestStatusDto : BaseEntityDto
{
    public int? GroupRequestId { get; set; }
    public int? ResponsibleRoleId { get; set; }
    public string? Name { get; set; }
}