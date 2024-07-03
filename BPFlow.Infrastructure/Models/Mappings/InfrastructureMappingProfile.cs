using AutoMapper;
using BPFlow.Domain.Dtos.Entities;
using BPFlow.Domain.Models.Entities;

namespace BPFlow.Infrastructure.Models.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateAssignedRequestMappings();
        CreateBusinessProcessMappings();
        CreateGroupRequestMappings();
        CreateRequestStatusMappings();
        CreateRequestStatusesOrderMappings();
        CreateRequestStatusTransitionMappings();
    }

    private void CreateAssignedRequestMappings()
    {
        CreateMap<AssignedRequest, AssignedRequestDto>();
        CreateMap<AssignedRequestDto, AssignedRequest>();
    }

    private void CreateBusinessProcessMappings()
    {
        CreateMap<BusinessProcess, BusinessProcessDto>();
        CreateMap<BusinessProcessDto, BusinessProcess>();
    }

    private void CreateGroupRequestMappings()
    {
        CreateMap<GroupRequest, GroupRequestDto>();
        CreateMap<GroupRequestDto, GroupRequest>();
    }

    private void CreateRequestStatusMappings()
    {
        CreateMap<RequestStatus, RequestStatusDto>();
        CreateMap<RequestStatusDto, RequestStatus>();
    }
    
    private void CreateRequestStatusesOrderMappings()
    {
        CreateMap<RequestStatusesOrder, RequestStatusesOrderDto>();
        CreateMap<RequestStatusesOrderDto, RequestStatusesOrder>();
    }

    private void CreateRequestStatusTransitionMappings()
    {
        CreateMap<RequestStatusTransition, RequestStatusTransitionDto>();
        CreateMap<RequestStatusTransitionDto, RequestStatusTransition>();
    }
}
