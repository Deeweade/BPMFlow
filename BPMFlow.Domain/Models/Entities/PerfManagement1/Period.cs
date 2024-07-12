using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities.PerfManagement1;

[Table("Quarter")]
public class Period : BaseEntity
{
    public string Title { get; set; }
    public DateTime? DateStart { get; set; }
    public DateTime? DateEnd { get; set; }
    public int? NumberY { get; set; }
    public int? NumberQ { get; set; }
    public int IsYear { get; set; }
}