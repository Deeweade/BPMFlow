using System.ComponentModel.DataAnnotations;
using BPMFlow.Application.Models.Views.BPMFlow;

namespace BPMFlow.Application.Interfaces.Services;

public interface IRequestStatusTransitionService
{
    Task<IEnumerable<RequestStatusTransitionView>> GetAvailableTransition(int code, string login);
}