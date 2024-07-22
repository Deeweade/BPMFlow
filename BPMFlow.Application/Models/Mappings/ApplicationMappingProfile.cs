using AutoMapper;
using BPMFlow.Application.Models.Filters;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Views.PerfManagement1;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.PerfManagement1;
using BPMFlow.Domain.Dtos.Filters;

namespace BPMFlow.Infrastructure.Models.Mappings;

public class ApplicationMappingProfile : Profile
{
    public ApplicationMappingProfile()
    {
        CreateObjectRequestMapping();
        CreateBusinessProcessMappings();
        CreateGroupRequestMappings();
        CreateRequestStatusMappings();
        CreateRequestStatusTransitionMappings();
        CreateRequestStatusTriggerMappings();
        CreateSystemMappings();
        CreateSystemObjectMappings();

        CreateActionMapping();
        CreateEmployeeRoleMappings();
        CreateEmployeeMappings();
        CreateEntityStatusMappings();
        CreateRoleAllowedActionMappings();
        CreateRoleTypeMappings();
        CreateRoleMappings();
    }

    #region BPMFlowMappings
        private void CreateObjectRequestMapping()
        {
            CreateMap<ObjectRequestView, ObjectRequestDto>();
            CreateMap<ObjectRequestDto, ObjectRequestView>();

            //filters
            CreateMap<ObjectRequestsFilterView, ObjectRequestsFilterDto>();
            CreateMap<ObjectRequestsFilterDto, ObjectRequestsFilterView>();
        }

        private void CreateBusinessProcessMappings()
        {
            CreateMap<BusinessProcessView, BusinessProcessDto>();
            CreateMap<BusinessProcessDto, BusinessProcessView>();
        }

        private void CreateGroupRequestMappings()
        {
            CreateMap<RequestView, RequestDto>();
            CreateMap<RequestDto, RequestView>();
        }

        private void CreateRequestStatusMappings()
        {
            CreateMap<RequestStatusView, RequestStatusDto>();
            CreateMap<RequestStatusDto, RequestStatusView>();
        }

        private void CreateRequestStatusTransitionMappings()
        {
            CreateMap<RequestStatusTransitionView, RequestStatusTransitionDto>();
            CreateMap<RequestStatusTransitionDto, RequestStatusTransitionView>();
        }

        private void CreateRequestStatusTriggerMappings()
        {
            CreateMap<RequestStatusTriggerView, RequestStatusTriggerDto>();
            CreateMap<RequestStatusTriggerDto, RequestStatusTriggerView>();
        }

        private void CreateSystemMappings()
        {
            CreateMap<SystemView, SystemDto>();
            CreateMap<SystemDto, SystemView>();
        }

        private void CreateSystemObjectMappings()
        {
            CreateMap<SystemObjectView, SystemObjectDto>();
            CreateMap<SystemObjectDto, SystemObjectView>();
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
