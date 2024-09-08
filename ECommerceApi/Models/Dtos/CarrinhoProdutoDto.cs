using System.ComponentModel.DataAnnotations;

namespace ECommerceApi.Models
{
    public class CarrinhoProdutoDto
    {
        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public int ProdutoId { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser ao menos 1.")]
        public int Quantidade { get; set; }
    }
}
