using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("produtos")]
public class Product
{
    [Key]
    [Column("id_produto")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("id_categoria")]
    public int CategoryId { get; set; }
    [Column("nome")]
    public string Name { get; set; } = null!;
    [Column("descricao")]
    public string? Description { get; set; }
    [Column("especificacoes")]
    public string? Specification { get; set; }
    [Column("info_adicional_imagem")]
    public string? PictureInfo { get; set; }
    [Column("validadedias")]
    public int? ValidityDays { get; set; }
    [Column("imagem1")]
    public string? FirstImage { get; set; }
    [Column("imagem2")]
    public string? SecondImage { get; set; }
    [Column("imagem3")]
    public string? ThirdImage { get; set; }
    [Column("imagem4")]
    public string? FourthImage { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("codigo")]
    public string Code { get; set; }
    [Column("imagem5")]
    public string? FifthImage { get; set; }
    [Column("imagemdetalhe")]
    public string? ImageDetail { get; set; }
    [Column("quantidademinima")]
    public int? MinimumQuantity { get; set; }
    [Column("ismobile")]
    public bool? IsMobile { get; set; }
    [Column("mobilechips")]
    public int? MobileChips { get; set; }
    [Column("ncm")]
    public string? NCM { get; set; }
    [Column("cest")]
    public string? Cest { get; set; }
    [Column("cfop")]
    public string? Cfop { get; set; }
    [Column("extipi")]
    public string? Extipi { get; set; }
    [Column("icms_cst")]
    public int? IcmsCst { get; set; }
    [Column("icms_aliq")]
    public decimal? IcmsAliq { get; set; }
    [Column("icms_origem")]
    public int? IcmsOrigem { get; set; }
    [Column("pis_cst")]
    public int? PisCst { get; set; }
    [Column("pis_aliq")]
    public decimal? PisAliq { get; set; }
    [Column("cofins_cst")]
    public int? ConfinsCst { get; set; }
    [Column("cofins_aliq")]
    public decimal? ConfinsAliq { get; set; }
    [Column("ca")]
    public string? Ca { get; set; }
    [Column("id_planta")]
    public int? PlantId { get; set; }
    [Column("id_tipoProduto",TypeName ="bigint")]
    public int? ProductTypeId { get; set; }
    [Column("unidade_medida")]
    public string? UnitOfMeasurement { get; set; }
    [Column("capacidade")]
    public int? Capacity { get; set; }

    public virtual Client Client { get; set; } = null!;
    public virtual Category Category { get; set; } = null!;
    public virtual Plant? Plant { get; set; } = null!;
    public virtual ProductType? ProductType { get; set; } = null!;
    public virtual List<EmployeeItems?>? EmployeeItems { get; set; } = null!;

}
