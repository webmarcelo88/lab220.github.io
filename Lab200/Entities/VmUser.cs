using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("usuarios_vm")]
public class VmUser
{
    [Key]
    [Column("id")]
    public int Id { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("login")]
    public string Login { get; set; }
    [Column("senha")]
    public string Pass { get; set; }
    [Column("id_cliente")]
    public int? ClientId { get; set; }
    [Column("ativo")]
    public bool? IsActive { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("admin")]
    public bool? Admin { get; set; }
    [Column("admin_cliente")]
    public bool? ClientAdmin { get; set; }

    public virtual Client? Client { get; set; }
}
