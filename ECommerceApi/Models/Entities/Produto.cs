using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models
{
    [Table("Produtos")]
    public class Produto : BaseEntity
    {
        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [MaxLength(200, ErrorMessage = "O nome do produto não pode exceder 200 caracteres.")]
        public string Nome { get; set; }

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres.")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "O preço deve ser maior que zero.")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A quantidade em estoque é obrigatória.")]
        [Range(0, int.MaxValue, ErrorMessage = "A quantidade em estoque deve ser um valor positivo.")]
        public int QuantidadeEstoque { get; set; }
    }
}
