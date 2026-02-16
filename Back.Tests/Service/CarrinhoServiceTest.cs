using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Data;
using Back.Models;
using Back.Service;
using Microsoft.EntityFrameworkCore;

namespace Back.Tests.Service
{
    public class CarrinhoServiceTest
    {
        private AppDbContext CriarContexto(string nomeBanco)
        {
             var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: nomeBanco)
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public void CriarCarrino_DeveCriarUmNovoCarrinhoNoBanco()
        {
            // Arrange
            var contexto = CriarContexto(nameof(CriarCarrino_DeveCriarUmNovoCarrinhoNoBanco));
            var service = new CarrinhoService(contexto);

            // Act
            var carrinho = service.CriaCarrinho();
        
            // Assert
            Assert.NotNull(carrinho);
            Assert.Equal(1, contexto.Carrinhos.Count());
        }
        
        [Fact]
        public void AdicionarItem_DeveCriarNovoItem_QuandoProdutoNaoExistirNoCarrinho()
        {
            // Arrange
            var contexto = CriarContexto(nameof(AdicionarItem_DeveCriarNovoItem_QuandoProdutoNaoExistirNoCarrinho));

            var produto = new ProdutoModel
            {
                nome = "Produto Teste",
                preco = 10
            };

            contexto.Produtos.Add(produto);
            contexto.SaveChanges();

            var service = new CarrinhoService(contexto);

            var carrinho = service.CriaCarrinho();

            // Act
            service.AdicionarItem(carrinho.id, produto.id, 1);

            // Assert
            var itemSalvo = contexto.ItensCarrinho.FirstOrDefault();

            Assert.NotNull(itemSalvo);
            Assert.Equal(1, itemSalvo.quantidade);
            Assert.Equal(produto.id, itemSalvo.produtoId);
        }
    }
}