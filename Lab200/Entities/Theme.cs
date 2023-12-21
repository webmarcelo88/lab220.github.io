using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab200.Entities;

[Table("temas")]
public class Theme
{
    [Key]
    [Column("id_tema")]
    public int Id { get; set; }
    
    [Column("id_cliente")]
    public int ClientId { get; set; }
    
    [Column("nome")]
    public string Name { get; set; } = null!;

    [Column("descricao")]
    public string? Description { get; set; } = null!;

    [Column("imagem")]
    public string Image { get; set; } = null!;

    [Column("layout")]
    public int? Layout { get; set; }

    [Column("tabertura_video")]
    public string? OpeningVideo { get; set; } = null!;

    [Column("tabertura_imagem")]
    public string? OpeningImage { get; set; } = null!;

    [Column("tabertura_bt_iniciar")]
    public string? OpeningStartButton { get; set; } = null!;

    [Column("tprincipal_bkg")]
    public string? MainBackground { get; set; } = null!;

    [Column("tprincipal_imagem")]
    public string? MainImage { get; set; } = null!;

    [Column("tprincipal_video")]
    public string? MainVideo { get; set; } = null!;

    [Column("tdetalhe_bkg")]
    public string? DetailBackground { get; set; } = null!;

    [Column("tcarrinho_bkg")]
    public string? CartBackgroud { get; set; } = null!;

    [Column("tcheckout_bkg")]
    public string? CheckoutBackground { get; set; } = null!;

    [Column("bt_carrinho")]
    public string? CartButton { get; set; } = null!;

    [Column("bt_produtos")]
    public string? ProductsButton { get; set; } = null!;

    [Column("bt_comprar")]
    public string? PurchaseButton { get; set; } = null!;

    [Column("bt_voltar")]
    public string? BackButton { get; set; } = null!;

    [Column("bt_menos")]
    public string? LessButton { get; set; } = null!;

    [Column("bt_mais")]
    public string? MoreButton { get; set; } = null!;

    [Column("bt_debito")]
    public string? DebitButton { get; set; } = null!;

    [Column("bt_credito")]
    public string? CreditButton { get; set; } = null!;

    [Column("bt_voucher")]
    public string? VouncherButton { get; set; } = null!;

    [Column("deleted")]
    public bool? IsDeleted { get; set; }

    [Column("bt_cupom")]
    public string? CouponButton { get; set; } = null!;

    [Column("bt_del")]
    public string? DeleteButton { get; set; } = null!;

    [Column("bt_adicionacarrinho")]
    public string? AddCartButton { get; set; } = null!;

    [Column("bt_info_adicional")]
    public string? InfoButton { get; set; } = null!;

    [Column("bt_recibo")]
    public string? ReceiptButton { get; set; } = null!;

    [Column("tpinpad")]
    public string? PinPad { get; set; } = null!;

    [Column("tpagamento_ok")]
    public string? OkPayment { get; set; } = null!;

    [Column("tpagamento_erro")]
    public string? PaymentError { get; set; } = null!;

    [Column("tpagamento_sacolas")]
    public string? PaymentBags { get; set; } = null!;

    [Column("tpagamento_cracha")]
    public string? PaymentBadge { get; set; } = null!;

    [Column("tpagamento_recibo")]
    public string? ReceiptPayment { get; set; } = null!;

    [Column("bt_ok")]
    public string? OkButton { get; set; } = null!;

    [Column("bt_naoquero")]
    public string? DontWantButton { get; set; } = null!;

    [Column("bt_cancelar")]
    public string? CancelButton { get; set; } = null!;

    [Column("bt_sim")]
    public string? YesButton { get; set; } = null!;

    [Column("bt_nao")]
    public string? NoButton { get; set; } = null!;

    [Column("bt_cracha")]
    public string? BadgeButton { get; set; } = null!;

    [Column("tpagamento_cupom")]
    public string? PaymentCoupon { get; set; } = null!;

    [Column("bt_biometria")]
    public string? BiometricsButton { get; set; } = null!;

    [Column("img_indisponivel")]
    public string? ImageUnavailiable { get; set; } = null!;

    [Column("tlogin_bkg")]
    public string? LoginBackground { get; set; } = null!;

    [Column("bt_sair")]
    public string? QuitButton { get; set; } = null!;

    [Column("bckTelaBiometria")]
    public string? BiometricsScreenBackground { get; set; } = null!;

    [Column("bckTelaBiometria_erro")]
    public string? BiometricsScreenBackgroundError { get; set; } = null!;

    [Column("bckTelaBiometria_inv")]
    public string? BiometricsScreenBackgroundInv { get; set; } = null!;

    [Column("bckTelaBiometriapinpad")]
    public string? BiometricsScreenPinPadBackground { get; set; } = null!;

    [Column("bt_loginvoltar")]
    public string? LoginBackButton { get; set; } = null!;

    [Column("bt_login")]
    public string? LoginButton { get; set; } = null!;

    [Column("bt_loginbio")]
    public string? BioButton { get; set; } = null!;
}