namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class System : BaseEntity
{
    public string Title { get; set; }

    // System -> BusinessProcess
    // public virtual ICollection<BusinessProcess> BusinessProcesses { get; set; }

    // System -> SystemObject
    public virtual ICollection<SystemObject> SystemObjects { get; set; }
}