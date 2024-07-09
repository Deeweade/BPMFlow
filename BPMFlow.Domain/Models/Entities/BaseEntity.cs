using System.ComponentModel.DataAnnotations.Schema;

namespace BPMFlow.Domain.Models.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
