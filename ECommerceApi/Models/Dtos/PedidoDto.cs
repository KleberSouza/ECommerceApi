using ECommerceApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.Dtos
{
    public class PedidoDto
    {
        [Required(ErrorMessage = "O endereço de entrega é obrigatório.")]
        [MaxLength(300, ErrorMessage = "O endereço de entrega não pode exceder 300 caracteres.")]
        public string EnderecoEntrega { get; set; }

        [Required(ErrorMessage = "O método de pagamento é obrigatório.")]
        public MetodoPagamento MetodoPagamento { get; set; }
    }
}
