namespace BPMFlow.Application.Models.Views.PerfManagement1;

public class EmployeeView : BaseEntityView
{
    public string TabNumber { get; set; }
    public string Fio { get; set; }
    public string PositionNum { get; set; }
    public string Position { get; set; }
    public string UnitNum { get; set; }
    public string UnitParentNum { get; set; }
    public string Unit { get; set; }
    public bool Gender { get; set; }
    public string City { get; set; }
    public string FuncManager { get; set; }
    public string AdmManager { get; set; }
    public string UnitManager { get; set; }
    public bool IsManager { get; set; }
    public int State { get; set; }
    public bool IsStaffMember { get; set; }
    public int HeadOffice { get; set; }
    public bool IsActive { get; set; }
    public DateTime Birthday { get; set; }
    public DateTime HireDate { get; set; }
    public int AmountSubordinate { get; set; }
    public string Login { get; set; }
    public int SpId { get; set; }
    public int Parent { get; set; }
    public string Parents { get; set; }
    public int Levels { get; set; }
    public int BlockNum { get; set; }
    public string PhotoUrl { get; set; }
    public string BonusType { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}