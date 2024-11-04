using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Orders.Repository.Models;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public Guid Id { get; set; }

    public Guid ProviderId { get; set; }

    [MaxLength(32)]
    public string Status { get; set; }

    [MaxLength(128)]
    public string ReferenceId { get; set; }

    [ForeignKey(nameof(ProviderId))]
    public virtual Provider Provider { get; set; }
}
