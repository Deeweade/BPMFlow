namespace BPMFlow.Domain.Models.Enums;

public enum BusinessProcesses
{
    /// <summary>
    /// Постановка целей
    /// </summary>
    Planning = 1,

    /// <summary>
    /// Корректировка целей
    /// </summary>
    Editing = 2,

    /// <summary>
    /// Промежуточная оценка целей
    /// </summary>
    InterimAssessment = 3,

    /// <summary>
    /// Итоговая оценка целей
    /// </summary>
    Assessment = 4,

    /// <summary>
    /// Постановка целей новичкам
    /// </summary>
    PlanningForNewbies = 5

}
