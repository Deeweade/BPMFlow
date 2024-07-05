namespace BPMFlow.Domain.Models.Entities;

public abstract class HistoryEntity : BaseEntity
{
    public int Code { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool IsActive { get; set; }
}