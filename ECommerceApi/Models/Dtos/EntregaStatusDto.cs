using ECommerceApi.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models.Dtos
{
    public class EntregaStatusDto
    {
        [Required(ErrorMessage = "O status da entrega é obrigatório.")]
        public EntregaStatus Status { get; set; }
    }
}
