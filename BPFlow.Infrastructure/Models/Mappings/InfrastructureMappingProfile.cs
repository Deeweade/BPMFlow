using AutoMapper;
using BPFlow.Domain.Dtos.Entities;
using BPFlow.Domain.Models.Entities;

namespace BPFlow.Infrastructure.Models.Mappings;

public class InfrastructureMappingProfile : Profile
{
    public InfrastructureMappingProfile()
    {
        CreateAssignedRequestMapping();
        CreateBusinessProcessStageMappings();
        CreateBusinessProcessTypeMappings();
        CreateGoalOwnershipTypeMappings();
        CreateGroupRequestMappings();
        CreateRequestStatusMappings();
        CreateRequestStatusesOrderMappings();
        CreateRequestStatusTransitionMappings();
    }

    private void CreateAssignedRequestMapping()
    {
        CreateMap<AssignedRequest, AssignedRequestDto>();
        CreateMap<AssignedRequestDto, AssignedRequest>();
    }

    private void CreateBusinessProcessStageMappings()
    {
        CreateMap<BusinessProcessStage, BusinessProcessStageDto>();
        CreateMap<BusinessProcessStageDto, BusinessProcessStage>();
    }

    private void CreateBusinessProcessTypeMappings()
    {
        CreateMap<BusinessProcessType, BusinessProcessTypeDto>();
        CreateMap<BusinessProcessTypeDto, BusinessProcessType>();
    }

    private void CreateGoalOwnershipTypeMappings()
    {
        CreateMap<GoalOwnershipType, GoalOwnershipTypeDto>();
        CreateMap<GoalOwnershipTypeDto, GoalOwnershipType>();
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
