using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.BPMFlow;

[Table("BusinessProcess")]
public class BusinessProcess : BaseEntity
{
    public BusinessProcess()
    {
        GroupRequests = new HashSet<GroupRequest>();
    }

    public string Name { get; set; }

    // BusinessProcessType -> GroupRequest
    public ICollection<GroupRequest> GroupRequests { get; set; }
}