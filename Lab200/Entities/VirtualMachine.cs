using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("vms")]
public class VirtualMachine
{
    [Key]
    [Column("id_vm")]
    public int Id { get; set; }
    [Column("id_cliente")]
    public int ClientId { get; set; }
    [Column("numero")]
    public string Number { get; set; }
    [Column("identificacao")]
    public string Identification { get; set; }
    [Column("localizacao")]
    public string Localization { get; set; }
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
    [Column("adquirente")]
    public string? Acquirer { get; set; }
    [Column("numerologico")]
    public string? LogicNumber { get; set; }
    [Column("numeroafiliacao")]
    public string? AfiliationNumber { get; set; }
    [Column("op_debito")]
    public bool? IsDebit { get; set; }
    [Column("op_credito")]
    public bool? IsCredit { get; set; }
    [Column("op_cred_parcelas")]
    public int? CreditInstallments { get; set; }
    [Column("op_voucher")]
    public bool? IsVouncher { get; set; }
    [Column("ativo")]
    public bool IsActive { get; set; }
    [Column("deleted")]
    public bool? IsDeleted { get; set; }
    [Column("id_cr_usuario")]
    public int CrUser { get; set; }
    [Column("created")]
    public DateTime? CreatedAt{ get; set; }
    [Column("id_up_usuario")]
    public int? UpUser { get; set; }
    [Column("updated")]
    public DateTime? UpdatedAt { get; set; }
    [Column("id_tema")]
    public int? ThemeId { get; set; }
    [Column("versao")]
    public int? Version { get; set; }
    [Column("enviada")]
    public bool? IsSent { get; set; }
    [Column("op_cupom")]
    public bool? IsCoupon { get; set; }
    [Column("op_sacolas")]
    public bool? WithBags { get; set; }
    [Column("op_recibo")]
    public bool? WithReceipt { get; set; }
    [Column("smtp_server")]
    public string? SmtpServer { get; set; }
    [Column("smtp_user")]
    public string? SmtpUser { get; set; }
    [Column("smtp_password")]
    public string? SmtpPassword { get; set; }
    [Column("smtp_port")]
    public int? SmtpPort { get; set; }
    [Column("smtp_remetente")]
    public string? SmtpSender { get; set; }
    [Column("smtp_copia")]
    public string? SmtpCopy { get; set; }
    [Column("smtp_email_header")]
    public string? SmtpEmailHeader { get; set; }
    [Column("smtp_email_footer")]
    public string? SmtpEmailFooter { get; set; }
    [Column("api_code")]
    public string? ApiCode { get; set; }
    [Column("op_cracha")]
    public bool? HasBadge { get; set; }
    [Column("op_cracha_teclado")]
    public bool? HasKeyboardBadge { get; set; }
    [Column("op_imei")]
    public bool? HasImei { get; set; }
    [Column("op_nfe")]
    public bool? HasNfe { get; set; }
    [Column("userid")]
    public string? Userid { get; set; }
    [Column("senha")]
    public string? Password { get; set; }
    [Column("op_biometria")]
    public bool? HasBiometry { get; set; }
    [Column("chave_senha")]
    public string? PassKey { get; set; }
    [Column("urlapi")]
    public string? ApiUrl { get; set; }
    [Column("clientid")]
    public string? Clientid { get; set; }

    public virtual Client Client { get; set; }
    //public virtual Theme? Theme { get; set; }
}
