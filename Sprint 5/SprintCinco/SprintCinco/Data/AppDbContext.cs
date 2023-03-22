using Microsoft.EntityFrameworkCore;
using IEcommerceAPI.Models;


namespace IEcommerceAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subcategoria>()
                .HasOne(subcategoria => subcategoria.Categoria)
                .WithMany(categoria => categoria.Subcategoria)
                .HasForeignKey(subcategoria => subcategoria.CategoriaId);

            builder.Entity<Produto>()
                            .HasOne(produto => produto.Subcategoria)
                            .WithMany(subcategoria => subcategoria.Produto)
                            .HasForeignKey(produto => produto.SubcategoriaId);

            builder.Entity<Produto>()
                            .HasOne(prod => prod.CentroDistribuicao)
                            .WithMany(centro => centro.Produtos)
                            .HasForeignKey(produto => produto.CentroId);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CentroDistribuicao> CentrosDistribuicoes { get; set; }
    }
}
