using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models.Entities
{
    [Table("Carrinhos")]
    public class Carrinho : BaseEntity
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }

        public List<CarrinhoProdutos> Produtos { get; set; }

        public decimal Total
        {
            get { return Produtos != null ? Produtos.Sum(item => item.PrecoTotal) : 0; }
        }
    }

    [Table("CarrinhoProdutos")]
    public class CarrinhoProdutos : BaseEntity
    {
        [Required(ErrorMessage = "O ID do carinho é obrigatório.")]
        public int CarrinhoId { get; set; }
        public Carrinho Carrinho { get; set; }

        [Required(ErrorMessage = "O ID do produto é obrigatório.")]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        [Required(ErrorMessage = "A quantidade é obrigatória.")]
        [Range(1, int.MaxValue, ErrorMessage = "A quantidade deve ser ao menos 1.")]
        public int Quantidade { get; set; }

        [Required(ErrorMessage = "O preço unitário é obrigatório.")]
        public decimal PrecoUnitario { get; set; }

        public decimal PrecoTotal
        {
            get { return Quantidade * PrecoUnitario; }
        }
    }
}
