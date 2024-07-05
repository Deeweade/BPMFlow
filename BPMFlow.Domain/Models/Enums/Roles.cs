namespace BPMFlow.Domain.Models.Enums;

public enum Roles
{
    /// <summary>
    /// Я
    /// </summary>
    Myself = 1,

    /// <summary>
    /// Прямой руководитель
    /// </summary>
    DirectManager = 2,

    /// <summary>
    /// Вышестоящий руководитель
    /// </summary>
    HigherManager = 3,

    /// <summary>
    /// Коллега
    /// </summary>
    Peer = 4,

    /// <summary>
    /// Ответственный коллега
    /// </summary>
    ResponsiblePeer = 5,
    
    /// <summary>
    /// Администратор
    /// </summary>
    Admin = 6
}