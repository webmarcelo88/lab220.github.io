using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("funcionarios")]
public class Employee
{
    [Key]
    [Column("id_funcionario")]
    public int Id { get; set; }
    [Column("nome")]
    public string Name { get; set; } = null!;
    [Column("matricula")]
    public string Registration { get; set; } = null!;
    [Column("ordem")]
    public int? Order { get; set; } = null!;
    [Column("id_cliente")] 
    public int ClientId { get; set; }
    [Column("id_setor")]
    public int? SectorId { get; set; }
    [Column("id_funcao")]
    public int? FunctionId { get; set; }
    [Column("biometria")]
    public string? Biometry { get; set; }
    [Column("RG")]
    public string? RG { get; set; }
    [Column("CPF")]
    public string? CPF { get; set; }
    [Column("CTPS")]
    public string? CTPS { get; set; }
    [Column("id_planta")]
    public int? PlantId { get; set; }
    [Column("foto")]
    public string? Picture { get; set; }
    [Column("data_admissao")]
    public DateTime? AdmissionDate { get; set; }
    [Column("hora_inicial")]
    public TimeSpan? StartTime { get; set; }
    [Column("hora_final")]
    public TimeSpan? EndTime { get; set; }
    [Column("segunda")]
    public bool? Monday { get; set; } = true;
    [Column("terca")]
    public bool? Tuesday { get; set; } = true;
    [Column("quarta")]
    public bool? Wednesday { get; set; } = true;
    [Column("quinta")]
    public bool? Thursday { get; set; } = true;
    [Column("sexta")]
    public bool? Friday { get; set; } = true;
    [Column("sabado")]
    public bool? Saturday { get; set; } = false;
    [Column("domingo")]
    public bool? Sunday { get; set; } = false;
    [Column("deleted")]
    public bool IsDeleted { get; set; }
    [Column("id_centro_custo")]
    public int? CostCenterId { get; set; }
    [Column("status")]
    public string? Status { get; set; }
    [Column("senha")]
    public string? Password { get; set; }
    [Column("biometria2")]
    public string? SecondBiometry { get; set; }
    [Column("email")]
    public string? Email { get; set; }

    public virtual Client Client { get; set; }
    public virtual Plant? Plant { get; set; }
    public virtual CostCenter? CostCenter { get; set; }
    public virtual Sector? Sector { get; set; }
    public virtual Function? Function { get; set; }
    public virtual List<EmployeeItems?>? EmployeeItems { get; set; }
}

