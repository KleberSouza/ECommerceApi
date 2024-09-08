using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Repositories
{
    public interface IEntregaRepository : IRepository<Entrega> { }
    
    public class EntregaRepository : Repository<Entrega, ApiDbContext>, IEntregaRepository
    {
        public EntregaRepository(ApiDbContext context) : base(context) { }
    }
}
