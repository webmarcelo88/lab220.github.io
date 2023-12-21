using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("categorias")]
public class Category
{
    [Key]
    [Column("id_categoria")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("imagem")]
    public string Image { get; set; }
    [Column("ordem")]
    public int? Order { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
}
