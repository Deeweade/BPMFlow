using AutoMapper;
using BPFlow.Domain.Dtos.Entities.BPFlow;
using BPFlow.Domain.Models.Entities.BPFlow;
using BPFlow.Domain.Dtos.Entities.PerfManagement1;
using BPFlow.Domain.Models.Entities.PerfManagement1;

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

        CreateActionMapping();
        CreateEmployeeMappings();
        CreateEmployeeRoleMappings();
        CreateEntityStatusMappings();
        CreateRoleMappings();
        CreateRoleAllowedActionMappings();
        CreateRoleTypeMappings();
    }

    #region BPFlowMappings
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
    #endregion

    #region PerfManagement1Mappings
    private void CreateActionMapping()
        {
            CreateMap<Domain.Models.Entities.PerfManagement1.Action, ActionDto>();
            CreateMap<ActionDto, Domain.Models.Entities.PerfManagement1.Action>();
        }

        private void CreateEmployeeRoleMappings()
        {
            CreateMap<EmployeeRole, EmployeeRoleDto>();
            CreateMap<EmployeeRoleDto, EmployeeRole>();
        }

        private void CreateEmployeeMappings()
        {
            CreateMap<Employee, EmployeeDto>();
            CreateMap<EmployeeDto, Employee>();
        }

        private void CreateEntityStatusMappings()
        {
            CreateMap<EntityStatus, EntityStatusDto>();
            CreateMap<EntityStatusDto, EntityStatus>();
        }
        
        private void CreateRoleAllowedActionMappings()
        {
            CreateMap<RoleAllowedAction, RoleAllowedActionDto>();
            CreateMap<RoleAllowedActionDto, RoleAllowedAction>();
        }

        private void CreateRoleTypeMappings()
        {
            CreateMap<RoleType, RoleTypeDto>();
            CreateMap<RoleTypeDto, RoleType>();
        }

        private void CreateRoleMappings()
        {
            CreateMap<Role, RoleDto>();
            CreateMap<RoleDto, Role>();
        }
    #endregion
}
