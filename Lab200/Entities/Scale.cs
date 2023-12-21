using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("tamanhos")]
public class Scale
{
    [Key]
    [Column("id_tamanho")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("deleted")]
    public bool IsDeleted { get; set; }
    [Column("ordem")]
    public int? Order { get; set; }

    public virtual Client Client { get; set; }
    public virtual List<EmployeeItems?>? EmployeeItems { get; set; }

}
