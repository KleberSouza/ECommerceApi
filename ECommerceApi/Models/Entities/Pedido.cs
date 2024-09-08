using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceApi.Models.Entities
{
    [Table("Pedidos")]
    public class Pedido : BaseEntity
    {
        [Required(ErrorMessage = "O ID do usuário é obrigatório.")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "A data do pedido é obrigatória.")]
        public DateTime DataPedido { get; set; }

        [Required(ErrorMessage = "O status do pedido é obrigatório.")]
        public PedidoStatus Status { get; set; }

        public string StatusDescricao
        {
            get { return Status.ToString(); }
        }

        [Required(ErrorMessage = "Os itens do pedido são obrigatórios.")]
        public List<PedidoProdutos> Produtos { get; set; }

        public decimal Total
        {
            get { return Produtos != null ? Produtos.Sum(item => item.PrecoTotal) : 0; }
        }

        public int PagamentoId { get; set; }
        public Pagamento Pagamento { get; set; }

        public int EntregaId { get; set; }
        public Entrega Entrega { get; set; }
    }

    [Table("PedidoProdutos")]
    public class PedidoProdutos : BaseEntity
    {
        [Required(ErrorMessage = "O ID do pedido é obrigatório.")]
        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }

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
    public enum PedidoStatus
    {
        [Display(Name = "Processando")]
        Processando,
        [Display(Name = "Enviado")]
        Enviado,
        [Display(Name = "Entregue")]
        Entregue,
        [Display(Name = "Cancelado")]
        Cancelado
    }
}
