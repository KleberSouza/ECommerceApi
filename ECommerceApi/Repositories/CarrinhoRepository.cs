using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ECommerceApi.Repositories
{
    public interface ICarrinhoRepository : IRepository<Carrinho>
    {
        Task<Carrinho> GetByUsuarioIdAsync(int userId);

        Task<Carrinho> AddProdutoAsync(int userId, int produtoId, int quantidade);

        Task<Carrinho> DeleteProdutoAsync(int userId, int produtoId);

        Task<Carrinho> DeleteProdutosAsync(int userId);
    }

    public class CarrinhoRepository : Repository<Carrinho, ApiDbContext>, ICarrinhoRepository
    {
        public CarrinhoRepository(ApiDbContext context) : base(context) { }

        public async Task<Carrinho> GetByUsuarioIdAsync(int userId)
        {
            try
            {
                var entity = await _context.Carrinhos
                    .Include(c => c.Produtos).ThenInclude(c => c.Produto)
                    .FirstOrDefaultAsync(e => e.UsuarioId == userId);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Falha ao recuperar a entidade com o Id {userId}.", ex);
            }
        }

        public async Task<Carrinho> AddProdutoAsync(int userId, int produtoId, int quantidade)
        {
            try
            {
                // VERIFICA SE O PRODUTO EXISTE
                var produto = await _context.Produtos.FindAsync(produtoId)
                    ?? throw new KeyNotFoundException($"Produto não encontrado");

                // VERIFICA SE O CARRINHO EXISTE
                var carrinho = await GetByUsuarioIdAsync(userId);
                if (carrinho == null)
                {
                    carrinho = new Carrinho { UsuarioId = userId };
                    await _context.Carrinhos.AddAsync(carrinho);
                    await _context.SaveChangesAsync();
                }


                // VERIFICA SE O PRODUTO JÁ ESTÁ INCLUIDO NO CARRINHO
                if (carrinho.Produtos != null && carrinho.Produtos.Any(c => c.ProdutoId == produtoId))
                {
                    var produtos = await _context.CarrinhoProdutos
                        .Where(c => c.CarrinhoId == carrinho.Id && c.ProdutoId == produtoId)
                        .SingleOrDefaultAsync();
                    produtos.Quantidade = quantidade;
                    produtos.PrecoUnitario = produto.Preco;
                    _context.Update(produtos);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    _context.CarrinhoProdutos.Add(new CarrinhoProdutos
                    {
                        CarrinhoId = carrinho.Id,
                        ProdutoId = produtoId,
                        Quantidade = quantidade,
                        PrecoUnitario = produto.Preco
                    });
                    await _context.SaveChangesAsync();
                }

                return await GetByUsuarioIdAsync(userId);
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

        public async Task<Carrinho> DeleteProdutoAsync(int userId, int produtoId)
        {
            try
            {
                // VERIFICA SE O PRODUTO EXISTE
                var produto = await _context.Produtos.FindAsync(produtoId)
                    ?? throw new KeyNotFoundException($"Produto não encontrado.");

                // VERIFICA SE O CARRINHO EXISTE
                var carrinho = await GetByUsuarioIdAsync(userId)
                    ?? throw new KeyNotFoundException($"Carrinho não encontrado.");

                var carrinhoProduto = await _context.CarrinhoProdutos
                        .Where(c => c.CarrinhoId == carrinho.Id && c.ProdutoId == produtoId)
                        .FirstOrDefaultAsync();

                if (carrinhoProduto != null)
                {
                    _context.CarrinhoProdutos.Remove(carrinhoProduto);
                    await _context.SaveChangesAsync();
                }

                return await GetByUsuarioIdAsync(userId);
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


        public async Task<Carrinho> DeleteProdutosAsync(int userId)
        {
            try
            {
                // VERIFICA SE O CARRINHO EXISTE
                var carrinho = await GetByUsuarioIdAsync(userId)
                    ?? throw new KeyNotFoundException($"Carrinho não encontrado.");

                var carrinhoProduto = await _context.CarrinhoProdutos
                        .Where(c => c.CarrinhoId == carrinho.Id)
                        .ToListAsync();

                if (carrinhoProduto != null)
                {
                    _context.CarrinhoProdutos.RemoveRange(carrinhoProduto);
                    await _context.SaveChangesAsync();
                }

                return await GetByUsuarioIdAsync(userId);
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
    }
}