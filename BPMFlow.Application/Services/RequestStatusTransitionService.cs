using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Domain.Dtos.Entities.BPMFlow;
using BPMFlow.Domain.Interfaces.Repositories;

namespace BPMFlow.Application.Services;

public class RequestStatusTransitionService : IRequestStatusTransitionService
{
    private readonly IEmployeeRoleService _employeeRoleService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RequestStatusTransitionService(IEmployeeRoleService employeeRoleService, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _employeeRoleService = employeeRoleService;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<IEnumerable<RequestStatusTransitionView>> GetAvailableTransition(int code, string login)
    {
        ArgumentNullException.ThrowIfNull(code);
        ArgumentNullException.ThrowIfNull(login);
        
        var requestByEmployee = await _unitOfWork.ObjectRequestRepository.GetBySystemObjectIdEmployee();

        IEnumerable<RequestStatusTransitionDto> transitions = [];

        if (requestByEmployee.Any())
        {
            var employee = await _unitOfWork.EmployeeRepository.GetByUserLogin(login);
            ArgumentNullException.ThrowIfNull(employee);

            var objectRequest = await _unitOfWork.ObjectRequestRepository.GetActiveByCode(code);
            ArgumentNullException.ThrowIfNull(objectRequest);

            var currentEmployeeRoleInRequest = await _employeeRoleService.GetRoleInRequest(employee.Id, objectRequest.ObjectId, objectRequest.Code);
            var currentEmployeeRoleInOrgStructure = await _employeeRoleService.GetRoleInOrgStructure(employee.Id, objectRequest.ObjectId);

            var requestStatus = await _unitOfWork.RequestStatusRepository.GetById(objectRequest.RequestStatusId);

            transitions = await _unitOfWork.RequestStatusTransitionRepository.GetAvailableTransition
                                            (x => x.RequestId == requestStatus.RequestId
                                            && x.SourceStatusOrder == requestStatus.StatusOrder
                                            && (x.ResponsibleRoleId == (int)currentEmployeeRoleInOrgStructure
                                            || x.ResponsibleRoleId == (int)currentEmployeeRoleInOrgStructure));

        }
            
        return _mapper.Map<IEnumerable<RequestStatusTransitionView>>(transitions);
    }
}