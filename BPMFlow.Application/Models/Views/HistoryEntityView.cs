namespace BPMFlow.Application.Models.Views;

public abstract class HistoryEntityView : BaseEntityView
{
    public int Code { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool IsActive { get; set; }
}
