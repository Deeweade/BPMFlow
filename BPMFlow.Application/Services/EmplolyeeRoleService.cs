using AutoMapper;
using BPMFlow.Application.Interfaces.Services;
using BPMFlow.Domain.Interfaces.Repositories;
using BPMFlow.Domain.Models.Entities.PerfManagement1;
using BPMFlow.Domain.Models.Enums;

namespace BPMFlow.Application.Services;

public class EmployeeRoleService : IEmployeeRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmployeeRoleService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Roles> GetRoleInRequest(int currentEmployeeId, int objectId, int requestCode)
        {
            if (currentEmployeeId == objectId)
                return Roles.Myself;

            if (requestCode != 0)
            {
                var request = await _unitOfWork.ObjectRequestRepository.GetActiveByCode(requestCode);

                if (currentEmployeeId == request.ResponsibleEmployeeId)
                {
                    var responsibleRoleId = request.RequestStatus.ResponsibleRoleId;

                    return responsibleRoleId switch
                    {
                        (int)Roles.Myself => Roles.Myself,
                        (int)Roles.DirectManager => Roles.DirectManager,
                        (int)Roles.HigherManager => Roles.HigherManager,
                        (int)Roles.Peer => Roles.Peer,
                        (int)Roles.ResponsiblePeer => Roles.ResponsiblePeer,
                        _ => throw new ArgumentOutOfRangeException(nameof(responsibleRoleId),
                                                    $"There is no employee role with id: {responsibleRoleId}"),
                    };
                }
            }

            return Roles.Peer;
        }

        public async Task<Roles> GetRoleInOrgStructure(int currentEmployeeId, int objectId)
        {
            if (currentEmployeeId == objectId) 
                return Roles.Myself;
            
            var employee = await _unitOfWork.EmployeeRepository.GetById(objectId);

            if (employee.Parent == currentEmployeeId)
            {
                return Roles.DirectManager;
            }
            else if (employee.Parents.Contains(currentEmployeeId.ToString()))
            {
                return Roles.HigherManager;
            }

            return Roles.Peer;
        }
}