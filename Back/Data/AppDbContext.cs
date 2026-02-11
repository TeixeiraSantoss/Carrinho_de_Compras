using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ItemCarrinhoModel>()
                .HasOne(ic => ic.Produto)
                .WithMany(p => p.itensCarrinho)
                .HasForeignKey(ic => ic.produtoId);

            modelBuilder.Entity<ItemCarrinhoModel>()
                .HasOne(ic => ic.Carrinho)
                .WithMany(c => c.itens)
                .HasForeignKey(ic => ic.carrinhoId);            
        }

        public DbSet<ProdutoModel> Produtos { get; set; }
        public DbSet<ItemCarrinhoModel> ItensCarrinho { get; set; }
        public DbSet<CarrinhoModel> Carrinhos { get; set; }
    }
}