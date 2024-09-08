using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models.Entities
{
    [Table("Pagamentos")]
    public class Pagamento: BaseEntity
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "A data do pagamento é obrigatória.")]
        public DateTime DataPagamento { get; set; }

        [Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        public StatusPagamento Status { get; set; }

        public string StatusDescricao
        {
            get { return Status.ToString(); }
        }

        [Required(ErrorMessage = "O valor do pagamento é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do pagamento deve ser maior que zero.")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public MetodoPagamento MetodoPagamento { get; set; }

        public string MetodoPagamentoDescricao
        {
            get { return MetodoPagamento.ToString(); }
        }
    }

    public enum StatusPagamento
    {
        [Display(Name = "Pendente")]
        Pendente,
        [Display(Name = "Aprovado")]
        Aprovado,
        [Display(Name = "Recusado")]
        Recusado
    }

    public enum MetodoPagamento
    {
        [Display(Name = "Cartão de Credito")]
        Credito,
        [Display(Name = "Cartão de Debito")]
        Debito,
        [Display(Name = "Pix")]
        Pix
    }
}
