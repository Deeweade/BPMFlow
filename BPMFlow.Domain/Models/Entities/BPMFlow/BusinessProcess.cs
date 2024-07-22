namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class BusinessProcess : BaseEntity
{
    public BusinessProcess()
    {
        Requests = new HashSet<Request>();
    }

    public string Title { get; set; }
    public int SystemId { get; set; }
    public int SystemObjectId { get; set; }

    // BusinessProcess -> Request
    public ICollection<Request> Requests { get; set; }
}