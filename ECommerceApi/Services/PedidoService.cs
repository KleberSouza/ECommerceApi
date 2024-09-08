using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos;
using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;

namespace ECommerceApi.Services
{
    public interface IPedidoService : IService<Pedido> {
        Task<Pedido> AddAsync(int userId, PedidoDto entity);

        Task<Pedido> UpdatePagamentoAsync(int id, PagamentoStatusDto entity);

        Task<Pedido> UpdateEntregaoAsync(int id, EntregaStatusDto entity);
    }

    public class PedidoService : Service<Pedido, ApiDbContext>, IPedidoService
    {
        private readonly IPedidoRepository _pedidoRepository;

        public PedidoService(IPedidoRepository repository, IConfiguration configuration) : base(repository)
        {
            _pedidoRepository = repository;
        }

        public async Task<Pedido> AddAsync(int userId, PedidoDto entity)
        {
            try
            {
                return await _pedidoRepository.AddAsync(userId, entity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao adicionar o produto no carrinho.", ex);
            }
        }

        public async Task<Pedido> UpdatePagamentoAsync(int id, PagamentoStatusDto entity)
        {
            try
            {
                return await _pedidoRepository.UpdatePagamentoAsync(id, entity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao adicionar o produto no carrinho.", ex);
            }
        }

        public async Task<Pedido> UpdateEntregaoAsync(int id, EntregaStatusDto entity)
        {
            try
            {
                return await _pedidoRepository.UpdateEntregaoAsync(id, entity);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao adicionar o produto no carrinho.", ex);
            }
        }
    }
}
