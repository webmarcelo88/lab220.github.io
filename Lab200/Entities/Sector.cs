using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("setores")]
public class Sector
{
    [Key]
    [Column("id_setor")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("id_centro_custo")]
    public int CostCenterId { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("deleted")]
    public bool IsDeleted { get; set; }
    [Column("ordem")]
    public int? Order { get; set; }
    [Column("codigo")]
    public string? Code { get; set; }

    public virtual Client Client { get; set; }
    public virtual CostCenter CostCenter { get; set; }
    public virtual List<Employee>? Employees { get; set; }

}
