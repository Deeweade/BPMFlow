using AutoMapper;
using BPFlow.Application.Models.Views;
using BPFlow.Domain.Dtos.Entities;

namespace BPFlow.Infrastructure.Models.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateAssignedRequestMapping();
        CreateBusinessProcessMappings();
        CreateGroupRequestMappings();
        CreateRequestStatusMappings();
        CreateRequestStatusesOrderMappings();
        CreateRequestStatusTransitionMappings();
    }

    private void CreateAssignedRequestMapping()
    {
        CreateMap<AssignedRequestView, AssignedRequestDto>();
        CreateMap<AssignedRequestDto, AssignedRequestView>();
    }

    private void CreateBusinessProcessMappings()
    {
        CreateMap<BusinessProcessView, BusinessProcessDto>();
        CreateMap<BusinessProcessDto, BusinessProcessView>();
    }

    private void CreateGroupRequestMappings()
    {
        CreateMap<GroupRequestView, GroupRequestDto>();
        CreateMap<GroupRequestDto, GroupRequestView>();
    }

    private void CreateRequestStatusMappings()
    {
        CreateMap<RequestStatusView, RequestStatusDto>();
        CreateMap<RequestStatusDto, RequestStatusView>();
    }
    
    private void CreateRequestStatusesOrderMappings()
    {
        CreateMap<RequestStatusesOrderView, RequestStatusesOrderDto>();
        CreateMap<RequestStatusesOrderDto, RequestStatusesOrderView>();
    }

    private void CreateRequestStatusTransitionMappings()
    {
        CreateMap<RequestStatusTransitionView, RequestStatusTransitionDto>();
        CreateMap<RequestStatusTransitionDto, RequestStatusTransitionView>();
    }
}
