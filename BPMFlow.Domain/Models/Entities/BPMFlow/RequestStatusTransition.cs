namespace BPMFlow.Domain.Models.Entities.BPMFlow;

public class RequestStatusTransition : BaseEntity
{
    public int SourceStatusOrder { get; set; }
    public int NextStatusOrder { get; set; }
    public string Title { get; set; }
    public bool IsNextOrderTransition { get; set; }
    public int ResponsibleRoleId { get; set; }
    public bool SkipValidation { get; set; }
    
    // RequestStatusTransition -> Request
    public int RequestId { get; set; }
    public virtual Request Request { get; set; }
}