using ECommerceApi.Data;
using ECommerceApi.Models;

namespace ECommerceApi.Repositories
{
    public interface IProdutoRepository : IRepository<Produto> { }
    
    public class ProdutoRepository : Repository<Produto, ApiDbContext>, IProdutoRepository
    {
        public ProdutoRepository(ApiDbContext context) : base(context) { }
    }
}
