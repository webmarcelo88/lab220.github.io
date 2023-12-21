using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("usuarios")]
public class User
{
    [Key]
    [Column("id_usuario")]
    public int Id { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("telefone")]
    public string? PhoneNumber { get; set; }
    [Column("celular")]
    public string? Cellphone { get; set; }
    [Column("senha")]
    public string Password { get; set; }
    [Column("ativo")]
    public bool? IsActive { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("last_login")]
    public DateTime? LastLogin { get; set; }
    [Column("id_planta")]
    public int? PlantId { get; set; }
    [Column("id_cliente")]
    public int? ClientId { get; set; }
    [Column("role")]
    public string? Role { get; set; }

    public virtual Plant? Plant { get; set; }
    public virtual Client? Client { get; set; }
}
