using ECommerceApi.Data;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;
using ECommerceApi.Repositories;
using System.Linq.Expressions;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
namespace ECommerceApi.Services
{
    public interface ICarrinhoService : IService<Carrinho> {

        Task<Carrinho> GetAsync(int userId);

        Task<Carrinho> AddProdutoAsync(int userId, CarrinhoProdutoDto entity);

        Task<Carrinho> DeleteProdutoAsync(int userId, int produtoId);

        Task<Carrinho> DeleteProdutosAsync(int userId);
    }

    public class CarrinhoService : Service<Carrinho, ApiDbContext>, ICarrinhoService
    {
        private readonly ICarrinhoRepository _carrinhoRepository;

        private readonly IProdutoRepository _produtoRepository;

        public CarrinhoService(ICarrinhoRepository repository, IProdutoRepository produtoRepository) : base(repository)
        {
            _carrinhoRepository = repository;
            _produtoRepository = produtoRepository;
        }

        public async Task<Carrinho> GetAsync(int userId)
        {
            try
            {
                var carrinho = await _carrinhoRepository.GetByUsuarioIdAsync(userId);

                if(carrinho == null)
                    throw new KeyNotFoundException($"Não foi encontrado nenhum carrinho para este usuário.");

                return carrinho;
            }
            catch (KeyNotFoundException ex)
            {
                throw new KeyNotFoundException(ex.Message);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro ao consultar o carrinho do usuário.", ex);
            }
        }

        public async Task<Carrinho> AddProdutoAsync(int userId, CarrinhoProdutoDto entity)
        {
            try
            {
                var carrinho = await _carrinhoRepository.AddProdutoAsync(userId, entity.ProdutoId, entity.Quantidade);
                return carrinho;
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

        public async Task<Carrinho> DeleteProdutoAsync(int userId, int produtoId)
        {
            try
            {
                var carrinho = await _carrinhoRepository.DeleteProdutoAsync(userId, produtoId);
                return carrinho;
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

        public async Task<Carrinho> DeleteProdutosAsync(int userId)
        {
            try
            {
                var carrinho = await _carrinhoRepository.DeleteProdutosAsync(userId);
                return carrinho;
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
    }
}
