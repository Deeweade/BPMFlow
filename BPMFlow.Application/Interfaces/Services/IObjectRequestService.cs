using BPMFlow.Application.Models.Views.BPMFlow;
using BPMFlow.Application.Models.Filters;

namespace BPMFlow.Application.Interfaces.Services;

public interface IObjectRequestService
{

    Task<ObjectRequestView> GetActiveByCode(int code);
    Task<IEnumerable<ObjectRequestView>> GetManyActiveByCode(int[] codes);
    Task<IEnumerable<ObjectRequestView>> GetByResponsibleLogin(string login);
    
    /// <summary>
    /// Метод Create() создает запись в таблице ObjectRequest и возвращает эту запись на фронт
    /// </summary>
    /// <param name = "authorLogin"> Логин сотрудника, который создал запрос </param>
    /// <summary>
    Task<ObjectRequestView> Create(ObjectRequestView objectRequestView, string authorLogin);

    /// <summary>
    /// Метод BulkCreate() создает одну и ту же запись в таблице ObjectRequest, но с разными ResponsibleEmployeeId,
        ///и возвращает на фронт массив кодов созданных записей
    /// </summary>
    /// <param name = "employeeIds"> Id сотрудников, для которых назначается запрос </param> 
    /// <param name = "authorLogin"> Логин сотрудника, который создал запрос </param>
    /// <summary>
    Task<IEnumerable<int>> BulkCreate(ICollection<int> employeeIds, ObjectRequestView objectRequests, string authorLogin);

    Task<IEnumerable<ObjectRequestView>> GetByFilter(ObjectRequestsFilterView filterView);
    Task<ObjectRequestView> ChangeStatus(ObjectRequestView objectRequestView, int nextStatusOrder);
    Task<IEnumerable<ObjectRequestView>> ChangeResponsibleEmployee(int[] requestCodes, int newResponsibleEmployeeId);
}