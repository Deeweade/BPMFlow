using AutoMapper;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Views.PerfManagement1;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;

namespace BPMFlow.Infrastructure.Models.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateAssignedRequestMapping();
        CreateBusinessProcessMappings();
        CreateGroupRequestMappings();
        CreateRequestStatusMappings();
        CreateRequestStatusesOrderMappings();
        CreateRequestStatusTransitionMappings();

        CreateActionMapping();
        CreateEmployeeRoleMappings();
        CreateEmployeeMappings();
        CreateEntityStatusMappings();
        CreateRoleAllowedActionMappings();
        CreateRoleTypeMappings();
        CreateRoleMappings();
    }

    #region BPMFlowMappings
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
    #endregion

    #region PerfManagement1Mappings
        private void CreateActionMapping()
        {
            CreateMap<ActionView, ActionDto>();
            CreateMap<ActionDto, ActionView>();
        }

        private void CreateEmployeeRoleMappings()
        {
            CreateMap<EmployeeRoleView, EmployeeRoleDto>();
            CreateMap<EmployeeRoleDto, EmployeeRoleView>();
        }

        private void CreateEmployeeMappings()
        {
            CreateMap<EmployeeView, EmployeeDto>();
            CreateMap<EmployeeDto, EmployeeView>();
        }

        private void CreateEntityStatusMappings()
        {
            CreateMap<EntityStatusView, EntityStatusDto>();
            CreateMap<EntityStatusDto, EntityStatusView>();
        }
        
        private void CreateRoleAllowedActionMappings()
        {
            CreateMap<RoleAllowedActionView, RoleAllowedActionDto>();
            CreateMap<RoleAllowedActionDto, RoleAllowedActionView>();
        }

        private void CreateRoleTypeMappings()
        {
            CreateMap<RoleTypeView, RoleTypeDto>();
            CreateMap<RoleTypeDto, RoleTypeView>();
        }

        private void CreateRoleMappings()
        {
            CreateMap<RoleView, RoleDto>();
            CreateMap<RoleDto, RoleView>();
        }
    #endregion
}
