namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class SystemObject : BaseEntity
{
    public string Title { get; set; }

    // SystemObject -> BusinessProcess
    public virtual ICollection<BusinessProcess> BusinessProcesses { get; set; }

    // SystemObject -> System
    public int SystemId { get; set; }
    public virtual System System { get; set; }
}