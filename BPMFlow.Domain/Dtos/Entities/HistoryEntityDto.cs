namespace BPMFlow.Domain.Dtos.Entities;
public abstract class HistoryEntityDto : BaseEntityDto
{
    public int Code { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool IsActive { get; set; }
}
