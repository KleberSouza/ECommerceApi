using ECommerceApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.Dtos
{
    public class PagamentoStatusDto
    {
        [Required(ErrorMessage = "O status do pagamento é obrigatório.")]
        public StatusPagamento Status { get; set; }
    }
}
