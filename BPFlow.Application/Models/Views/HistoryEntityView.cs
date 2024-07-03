namespace BPFlow.Application.Models.Views;

public abstract class HistoryEntityView : BaseEntityView
{
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public bool IsActive { get; set; }
}
