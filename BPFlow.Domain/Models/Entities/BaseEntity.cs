using System.ComponentModel.DataAnnotations.Schema;

namespace BPFlow.Domain.Models.Entities;

public abstract class BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
}
