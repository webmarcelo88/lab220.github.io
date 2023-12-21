using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("itens_funcionario")]
public class EmployeeItems
{
    [Key]
    [Column("id_item_funcionario")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("id_funcionario")]
    public int EmployeeId { get; set; }
    [Column("id_produto")]
    public int ProductId { get; set; }
    [Column("deleted")]
    public bool? Deleted { get; set; } = false;
    [Column("id_tamanho")]
    public int ScaleId { get; set; }
    [Column("id_cor")]
    public int ColorsId { get; set; }
    [Column("ordem")]
    public int Order { get; set; }
    [Column("sku")]
    public string Sku { get; set; }
    [Column("qtd_permitida")]
    public int? Qty { get; set; }

    public virtual Client Client { get; set; }
    public virtual Employee Employee { get; set; }
    public virtual Product Product { get; set; }
    public virtual Scale Scale { get; set; }
    public virtual Colours Colors { get; set; }
}
