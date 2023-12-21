using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("cores")]
public class Colours
{
    [Key]
    [Column("id_cor")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("valor")]
    public string Value { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }

    public virtual Client Client { get; set; }
    public virtual List<EmployeeItems?>? EmployeeItems { get; set; }

}
