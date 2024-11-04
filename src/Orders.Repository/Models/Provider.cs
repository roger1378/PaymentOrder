using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orders.Repository.Models;

public class Provider
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [MaxLength(32)]
    public string Name { get; set; }

    public bool IsActive { get; set; }
}
