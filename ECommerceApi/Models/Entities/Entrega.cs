using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models.Entities
{
    [Table("Entregas")]
    public class Entrega : BaseEntity
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

        [Required(ErrorMessage = "A data de envio é obrigatória.")]
        public DateTime DataEnvio { get; set; }

        [Required(ErrorMessage = "A data da entrega é obrigatória.")]
        public DateTime DataEntrega { get; set; }

        [Required(ErrorMessage = "O status da entrega é obrigatório.")]
        public EntregaStatus Status { get; set; }

        public string StatusDescricao
        {
            get { return Status.ToString(); }
        }

        [Required(ErrorMessage = "O endereço de entrega é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O endereço de entrega não pode exceder 300 caracteres.")]
        public string EnderecoEntrega { get; set; }
    }

    public enum EntregaStatus
    {
        [Display(Name = "Pendente")]
        Pendente,
        [Display(Name = "Em Trânsito")]
        EmTransito,
        [Display(Name = "Entregue")]
        Entregue
    }
}
