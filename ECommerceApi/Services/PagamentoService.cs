using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services
{
    public interface IPagamentoService : IService<Pagamento> {}

    public class PagamentoService : Service<Pagamento, ApiDbContext>, IPagamentoService
    {
        public PagamentoService(IPagamentoRepository repository, IConfiguration configuration) : base(repository)
        {
        }
    }
}
