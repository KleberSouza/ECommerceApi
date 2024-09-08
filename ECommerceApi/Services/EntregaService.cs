using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services
{
    public interface IEntregaService : IService<Entrega> {}

    public class EntregaService : Service<Entrega, ApiDbContext>, IEntregaService
    {
        public EntregaService(IEntregaRepository repository, IConfiguration configuration) : base(repository)
        {
        }
    }
}
