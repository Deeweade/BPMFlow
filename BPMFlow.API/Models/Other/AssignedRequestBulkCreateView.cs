using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.API.Models.Other;

public class AssignedRequestBulkCreateView
{
    public ICollection<int>? EmployeeIds { get; set; }
    public ICollection<AssignedRequestView>? AssignedRequests { get; set; }
}