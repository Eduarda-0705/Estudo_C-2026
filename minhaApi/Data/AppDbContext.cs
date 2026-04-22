using Microsoft.EntityFrameworkCore;
using MinhaApi.Models;

namespace MinhaApi.Data;

public class AppDbContext : DbContext{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<Produto> Produtos => Set<Produto>();
    public DbSet<Categoria> Categorias => Set<Categoria>();
    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder){
        modelBuilder.Entity<Produto>()
            .HasMany(p => p.Categorias)
            .WithMany(c => c.Produtos)
            .UsingEntity(j => j.ToTable("ProdutosCategorias"));

        modelBuilder.Entity<Produto>(entity =>{
            entity.Property(p => p.Preco).HasPrecision(18,2).IsRequired();
            entity.Property(p => p.Nome).HasMaxLength(120).IsRequired();
        });
        
        modelBuilder.Entity<Categoria>(entity =>{
            entity.Property(p => p.Nome).HasMaxLength(80).IsRequired();
        });

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Login)
            .IsUnique();
    }   
}