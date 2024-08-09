namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class BusinessProcess : BaseEntity
{
    public string Title { get; set; }

    // BusinessProcess -> Request
    public virtual ICollection<Request> Requests { get; set; }

    // BusinessProcess -> System
    public int SystemId { get; set; }
    public virtual System System { get; set; }
    
    // BusinessProcess -> SystemObject
    public int SystemObjectId { get; set; }
    public virtual SystemObject SystemObject { get; set; }
}