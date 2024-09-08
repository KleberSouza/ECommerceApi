using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories
{
    public interface IPagamentoRepository : IRepository<Pagamento> { }
    
    public class PagamentoRepository : Repository<Pagamento, ApiDbContext>, IPagamentoRepository
    {
        public PagamentoRepository(ApiDbContext context) : base(context) { }
    }
}
