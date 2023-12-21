using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Entities;

[Table("grade")]
[Keyless]
public class Grid
{
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("id_produto")]
    public int ProductId { get; set; }
    [Column("sku")]
    public string Sku { get; set; }
    [Column("plu")]
    public string? Plu { get; set; }
    [Column("ean")]
    public string? Ean { get; set; }
    [Column("id_tamanho")]
    public int ScaleId { get; set; }
    [Column("id_cor")]
    public int ColorsId { get; set; }
    [Column("preco")]
    [Precision(18,2)]
    public decimal? Price { get; set; }

    public virtual Client Client { get; set; }
    [ForeignKey("ProductId")]
    public virtual Product Product { get; set; }
    public virtual Scale Scale { get; set; }
    public virtual Colours Colors { get; set; }

}

