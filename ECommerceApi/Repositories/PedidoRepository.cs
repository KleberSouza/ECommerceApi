using AutoMapper;
using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Dtos;
using ECommerceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

namespace ECommerceApi.Repositories
{
    public interface IPedidoRepository : IRepository<Pedido> {

        Task<Pedido> AddAsync(int userId, PedidoDto entity);

        Task<Pedido> UpdatePagamentoAsync(int id, PagamentoStatusDto entity);

        Task<Pedido> UpdateEntregaoAsync(int id, EntregaStatusDto entity);
    }
    
    public class PedidoRepository : Repository<Pedido, ApiDbContext>, IPedidoRepository
    {
        private readonly IMapper _mapper;

        public PedidoRepository(ApiDbContext context, IMapper mapper) : base(context) { _mapper = mapper;  }

        public async Task<Pedido> AddAsync(int userId, PedidoDto entity)
        {
            try
            {
                // VERIFICA SE O USUARIO POSSUI CARRINHO
                var carrinho = await _context.Carrinhos
                   .Include(c => c.Produtos)
                   .FirstOrDefaultAsync(e => e.UsuarioId == userId);
                if(carrinho == null || carrinho.Produtos.Count == 0)
                    throw new KeyNotFoundException($"Carrinho não encontrado");

                // CRIA O PEDIDO
                Pedido pedido = new Pedido { 
                    UsuarioId = userId,
                    DataPedido = DateTime.UtcNow,
                    Status = PedidoStatus.Processando,
                    Pagamento = new Pagamento
                        {
                            Status = StatusPagamento.Pendente,
                            Valor = carrinho.Total,
                            MetodoPagamento =  entity.MetodoPagamento
                        },
                        Entrega = new Entrega
                        {
                            Status = EntregaStatus.Pendente,
                            EnderecoEntrega = entity.EnderecoEntrega
                        },
                        Produtos = _mapper.Map<List<PedidoProdutos>>(carrinho.Produtos)
                };

                await _context.Pedidos.AddAsync(pedido);

                if(await _context.SaveChangesAsync() > 0)
                {
                    _context.CarrinhoProdutos.RemoveRange(carrinho.Produtos);
                    await _context.SaveChangesAsync();
                }

                return pedido;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao recuperar a entidade com o Id {userId}.", ex);
            }
        }

        public async Task<Pedido> UpdatePagamentoAsync(int id, PagamentoStatusDto entity)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(c => c.Pagamento)
                    .Include(c => c.Entrega)
                    .FirstOrDefaultAsync(c => c.Id == id)
                    ?? throw new KeyNotFoundException($"Pedido não encontrado");

                if(pedido.Status != PedidoStatus.Processando)
                    throw new ArgumentException($"Pedido não está em processanto.");

                switch (entity.Status, pedido.Pagamento.Status)
                {
                    case (StatusPagamento.Pendente, StatusPagamento.Aprovado):
                        {
                            pedido.Status = PedidoStatus.Enviado;
                            pedido.Pagamento.Status = StatusPagamento.Aprovado;
                            pedido.Pagamento.DataPagamento = DateTime.UtcNow;
                            pedido.Entrega.Status = EntregaStatus.EmTransito;
                            pedido.Entrega.DataEnvio = DateTime.UtcNow;
                        }
                        break;
                    case (StatusPagamento.Pendente, StatusPagamento.Recusado):
                        {
                            pedido.Status = PedidoStatus.Cancelado;
                            pedido.Pagamento.Status = StatusPagamento.Recusado;
                            pedido.Pagamento.DataPagamento = DateTime.UtcNow;
                        }
                        break;
                    default:
                        throw new ArgumentException($"Status inválido.");
                }

                _context.Update(pedido);
                await _context.SaveChangesAsync();

                return pedido;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao tentar mudar o status do pagamento.", ex);
            }
        }

        public async Task<Pedido> UpdateEntregaoAsync(int id, EntregaStatusDto entity)
        {
            try
            {
                var pedido = await _context.Pedidos
                    .Include(c => c.Pagamento)
                    .Include(c => c.Entrega)
                    .FirstOrDefaultAsync(c => c.Id == id)
                    ?? throw new KeyNotFoundException($"Pedido não encontrado");

                if (pedido.Status != PedidoStatus.Enviado)
                    throw new ArgumentException($"Pedido não está disponível para envio.");

                switch (entity.Status, pedido.Entrega.Status)
                {
                    case (EntregaStatus.EmTransito, EntregaStatus.Entregue ):
                        {
                            pedido.Status = PedidoStatus.Entregue;
                            pedido.Entrega.Status = EntregaStatus.Entregue;
                            pedido.Entrega.DataEntrega = DateTime.UtcNow;
                        }
                        break;                   
                    default:
                        throw new ArgumentException($"Status inválido.");
                }

                _context.Update(pedido);
                await _context.SaveChangesAsync();

                return pedido;
            }
            catch (ArgumentException ex)
            {
                throw new ArgumentException(ex.Message);
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao tentar mudar o status da entrega.", ex);
            }
        }
    }
}
