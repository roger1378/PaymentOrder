using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orders.Repository.Models;

public class Product
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    [MaxLength(128)]
    public string Name { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal UnitPrice { get; set; }

    [MaxLength(256)]
    public string Details { get; set; }

    public int Quantity { get; set; }

    [MaxLength(32)]
    public string Status { get; set; }

    public bool IsActive { get; set; }
}
