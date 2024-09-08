using Microsoft.EntityFrameworkCore;
using ECommerceApi.Models;
using ECommerceApi.Models.Entities;

namespace ECommerceApi.Data
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Carrinho>()
            .HasIndex(c => c.UsuarioId)
            .IsUnique();

            modelBuilder.Entity<CarrinhoProdutos>()
            .HasIndex(cp => new { cp.CarrinhoId, cp.ProdutoId })
            .IsUnique();

            modelBuilder.Entity<PedidoProdutos>()
            .HasIndex(ip => new { ip.PedidoId, ip.ProdutoId })
            .IsUnique();

            modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Entrega)
            .WithOne(e => e.Pedido)
            .HasForeignKey<Entrega>(e => e.PedidoId);

            modelBuilder.Entity<Pedido>()
            .HasOne(p => p.Pagamento)
            .WithOne(e => e.Pedido)
            .HasForeignKey<Pagamento>(e => e.PedidoId);

            modelBuilder.ApplyConfiguration(new UserConfiguration());
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Carrinho> Carrinhos { get; set; }
        public DbSet<CarrinhoProdutos> CarrinhoProdutos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoProdutos> PedidoProdutos { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Entrega> Entregas { get; set; }
    }
}