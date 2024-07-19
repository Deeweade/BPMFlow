using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.API.Models.Other;

public class ObjectRequestBulkCreateView
{
    public ICollection<int> EmployeeIds { get; set; }
    public ObjectRequestView ObjectRequests { get; set; }
}