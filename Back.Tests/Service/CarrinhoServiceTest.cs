// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Back.Data;
// using Back.Models;
// using Back.DTOs.CarrinhoDTO;
// using Back.Service;
// using Back;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.VisualBasic;

// namespace Back.Tests.Service
// {
//     public class CarrinhoServiceTest
//     {
//         private AppDbContext CriarContexto(string nomeBanco)
//         {
//              var options = new DbContextOptionsBuilder<AppDbContext>()
//                 .UseInMemoryDatabase(databaseName: nomeBanco)
//                 .Options;

//             return new AppDbContext(options);
//         }

//         [Fact]
//         public void CriarCarrino_DeveCriarUmNovoCarrinhoNoBanco()
//         {
//             // Arrange
//             var contexto = CriarContexto(nameof(CriarCarrino_DeveCriarUmNovoCarrinhoNoBanco));
//             var service = new CarrinhoService(contexto);

//             // Act
//             var carrinho = service.CriaCarrinhoService();
        
//             // Assert
//             Assert.NotNull(carrinho);
//             Assert.Equal(1, contexto.Carrinhos.Count());
//         }
        
//         [Fact]
//         public void AdicionarItem_DeveCriarNovoItem_QuandoProdutoNaoExistirNoCarrinho()
//         {
//             // Arrange
//             var contexto = CriarContexto(nameof(AdicionarItem_DeveCriarNovoItem_QuandoProdutoNaoExistirNoCarrinho));

//             var produto = new ProdutoModel
//             {
//                 nome = "Produto Teste",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             var novoItem = new AddItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id,
//                 quantidade = 1  
//             };

//             // Act
//             service.AdicionarItemService(novoItem);

//             // Assert
//             var itemSalvo = contexto.ItensCarrinho.FirstOrDefault();

//             Assert.NotNull(itemSalvo);
//             Assert.Equal(1, itemSalvo.quantidade);
//             Assert.Equal(produto.id, itemSalvo.produtoId);
//         }

//         [Fact]
//         public void RemoverItem_DeveRemoverItem_QuandoItemExistirNoCarrinho()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(RemoverItem_DeveRemoverItem_QuandoItemExistirNoCarrinho));

//             var produto = new ProdutoModel
//             {
//                 nome = "Omega 3",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             var novoItem = new AddItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id,
//                 quantidade = 1
//             };

//             service.AdicionarItemService(novoItem);

//             var itemExistente = new RemoveItemDTO
//             {
//                 produtoId = novoItem.produtoId,
//                 carrinhoId = novoItem.carrinhoId
//             };

//             //Act
//             service.RemoverItemService(itemExistente);

//             //Assert
//             var itemExcluido = contexto.ItensCarrinho.FirstOrDefault();

//             Assert.Null(itemExcluido);
//         }

//         [Fact]
//         public void SomaQuantidade_DeveSomarQuantidade_SeProdutoJaExistirNoBanco()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(SomaQuantidade_DeveSomarQuantidade_SeProdutoJaExistirNoBanco));

//             var produto = new ProdutoModel
//             {
//                 nome = "Teste quantidade",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();
            
//             var novoItem = new AddItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id,
//                 quantidade = 1
//             };

//             service.AdicionarItemService(novoItem);

//             //Act
//             service.AdicionarItemService(novoItem);

//             //Assert
//             var itemExistente = contexto.ItensCarrinho.FirstOrDefault();

//             Assert.Equal(2, itemExistente.quantidade);
//         }
        
//         [Fact]
//         public void AdicionarItemInvalido_DeveDarErro_QuandoQuantidadeForInvalida()
//         {
//             // Arrange
//             var contexto = CriarContexto(nameof(AdicionarItemInvalido_DeveDarErro_QuandoQuantidadeForInvalida));

//             var produto = new ProdutoModel
//             {
//                 nome = "Produto Teste",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             var novoItem = new AddItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id,
//                 quantidade = 0
//             };

//             // Act
//             var exception = Assert.Throws<DomainException>(() =>
//                 service.AdicionarItemService(novoItem)
//             );            

//             // Assert
//             Assert.Equal("Quantidade invalida", exception.Message);
//         }

//         [Fact]
//         public void RemoverItemInvalido_DeveDarErro_QuandoItemNaoExistirNoCarrinho()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(RemoverItemInvalido_DeveDarErro_QuandoItemNaoExistirNoCarrinho));

//             var produto = new ProdutoModel
//             {
//                 nome = "Omega 3",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             var itemExistente = new RemoveItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id
//             };

//             //Act
//             var exception = Assert.Throws<DomainException>(() =>
//                 service.RemoverItemService(itemExistente)
//             );            

//             //Assert
//             Assert.Equal("Produto não encontrado no carrinho", exception.Message);
//         }

//         [Fact]
//         public void SomaTotalCarrinho_DeveSomarPrecoDosItens_QuandoHouverItemNoCarrinho()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(SomaTotalCarrinho_DeveSomarPrecoDosItens_QuandoHouverItemNoCarrinho));

//             var produto = new ProdutoModel
//             {
//                 nome = "Teste total",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             var item = new AddItemDTO
//             {
//                 produtoId = produto.id,
//                 carrinhoId = carrinho.id,
//                 quantidade = 1
//             };

//             service.AdicionarItemService(item);
//             service.AdicionarItemService(item);

//             //Act
//             var valorTotal = service.CalculoTotalCarrinho(carrinho.id);

//             //Assert
//             Assert.Equal(20, valorTotal);
//         }

//         [Fact]
//         public void SomarTotalCarrinhoInvalido_DeveLancarErro_QuandoCarrinhoNaoExistir()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(SomarTotalCarrinhoInvalido_DeveLancarErro_QuandoCarrinhoNaoExistir));

//             var produto = new ProdutoModel
//             {
//                 nome = "Teste total",
//                 preco = 10
//             };

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             //Act
//             var exception = Assert.Throws<DomainException>(() =>
//                 service.CalculoTotalCarrinho(1)
//             );

//             //Assert
//             Assert.Equal("Nenhum carrinho encontrado", exception.Message);
//         }

//         [Fact]
//         public void SomarTotalCarrinhoVazio_DeveRetornarZero_QuandoCarrinhoEstiverVazio()
//         {
//             //Arrange
//             var contexto = CriarContexto(nameof(SomaTotalCarrinho_DeveSomarPrecoDosItens_QuandoHouverItemNoCarrinho));

//             var produto = new ProdutoModel
//             {
//                 nome = "Teste total",
//                 preco = 10
//             }; 

//             contexto.Produtos.Add(produto);
//             contexto.SaveChanges();

//             var service = new CarrinhoService(contexto);

//             var carrinho = service.CriaCarrinhoService();

//             //Act
//             var valorTotal = service.CalculoTotalCarrinho(carrinho.id);

//             //Assert
//             Assert.Equal(0, valorTotal);
//         }
//     }
// }