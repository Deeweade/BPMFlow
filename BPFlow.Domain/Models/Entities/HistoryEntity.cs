namespace BPFlow.Domain.Models.Entities;

public abstract class HistoryEntity : BaseEntity
{
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool IsActive { get; set; }
}
