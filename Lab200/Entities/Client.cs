using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("clientes")]
public class Client
{
    [Key]
    [Column("id_cliente")]
    public int Id { get; set; }
    [Column("nome")]
    public string Name { get; set; }
    [Column("cpfcnpj")]
    public string? Cpfcnpj { get; set; }
    [Column("rgie")]
    public string? Rgie { get; set; }
    [Column("email")]
    public string Email { get; set; }
    [Column("telefone")]
    public string? PhoneNumber { get; set; }
    [Column("celular")]
    public string? CellPhone { get; set; }
    [Column("senha")]
    public string Password { get; set; }
    [Column("cep")]
    public string? Cep { get; set; }
    [Column("logradouro")]
    public string? Street { get; set; }
    [Column("complemento")]
    public string? Complement { get; set; }
    [Column("bairro")]
    public string? District { get; set; }
    [Column("cidade")]
    public string? City { get; set; }
    [Column("uf")]
    public string? Uf { get; set; }
    [Column("contato")]
    public string? Contact { get; set; }
    [Column("ativo")]
    public bool? IsActive { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("id_cr_usuario")]
    public int CrUserId { get; set; }
    [Column("created")]
    public DateTime? CreatedAt { get; set; }
    [Column("id_up_usuario")]
    public int? UpUserId { get; set; }
    [Column("updated")]
    public DateTime? UpdatedAt { get; set; }
    [Column("last_login")]
    public DateTime? LastLoginAt { get; set; }
    [Column("usar_api")]
    public bool? UseApi { get; set; }
    [Column("textoretirada")]
    public string? TextWithdrawn { get; set; }

    public virtual List<VirtualMachine>? VirtualMachines { get; set; }
    public virtual List<Plant>? Plants { get; set; }
    public virtual List<Sector>? Sectors { get; set; }
    public virtual List<User>? Users { get; set; }
    public virtual List<CostCenter>? CostCenters { get; set; }
    public virtual List<Function>? Functions { get; set; }
    public virtual List<EmployeeItems?>? EmployeeItems { get; set; }
    public virtual List<VmUser>? VmUsers { get; set; }


}
