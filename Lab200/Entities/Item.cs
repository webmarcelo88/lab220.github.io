using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab200.Entities;

[Table("itens")]
public class Item
{
    [Key]
    [Column("id_item")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("id_vm")]
    public int VirtualMachineId { get; set; }
    [Column("id_produto")]
    public int ProductId { get; set; }
    [Column("id_placa")]
    public int PlacaId { get; set; }
    [Column("id_motor")]
    public int MotorId { get; set; }
    [Column("preco")]
    [Precision(18,2)]
    public decimal Price { get; set; }
    [Column("quantidade")]
    public int? Quantity { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("id_tamanho")]
    public int ScaleId { get; set; }
    [Column("id_cor")]
    public int ColoursId { get; set; }
    [Column("ordem")]
    public int Ordem { get; set; }
    [Column("id_motor2")]
    public int? MotorSecondId { get; set; }

    public virtual Client Client { get; set; } = null!;
    public virtual VirtualMachine VirtualMachine { get; set; } = null!;
    public virtual Product Product{ get; set; } = null!;
    public virtual Scale Size { get; set; } = null!;
    public virtual Colours Color { get; set; } = null!;


}