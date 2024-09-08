using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services
{
    public interface IProdutoService : IService<Produto> {}

    public class ProdutoService : Service<Produto, ApiDbContext>, IProdutoService
    {
        public ProdutoService(IProdutoRepository repository, IConfiguration configuration) : base(repository)
        {
        }
    }
}
