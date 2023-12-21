using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("plantas")]
public class Plant
{
    [Key]
    [Column("id_planta")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("deleted")]
    public bool IsDeleted { get; set; }
    [Column("ordem")]
    public int? Order { get; set; }
    [Column("codigo")]
    public string? Code { get; set; }
    [Column("userid")]
    public string? Userid { get; set; }
    [Column("senha")]
    public string? Password { get; set; }
    [Column("urlapi")]
    public string? Urlapi { get; set; }
    [Column("clientid")]
    public string? Clientid { get; set; }

    public virtual Client? Client { get; set; }
}
