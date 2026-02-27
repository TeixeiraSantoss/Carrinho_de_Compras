using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using Back;
using Back.Data;
using Back.DTOs.ProdutoDTO;
using Back.Models;
using Back.Service;
using System.Data.Common;

namespace Back.Tests.Service
{
    public class ProdutoServiceTest
    {
        private AppDbContext CriarContexto(string nomeBanco)
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: nomeBanco)
                .Options;

            return new AppDbContext(options);            
        }

        //
        //Inicio testes cadastro de produto
        [Fact]
        public void CadastraProduto_DeveCadastrarProduto()
        {
            //Arrange
            var contexto = CriarContexto(nameof(CadastraProduto_DeveCadastrarProduto));

            var service = new ProdutoService(contexto);

            var produtoTeste = new CreateProdutoDTO
            {
                nome = "TESTE",
                preco = 15
            };

            //Act
            service.CadastrarProdutoService(produtoTeste);

            //Assert
            var produtoExistente = contexto.Produtos.FirstOrDefault(p => p.id == 1);
            Assert.NotNull(produtoExistente);
        }

        [Fact]
        public void CadastraProduto_DeveLancarException_QuandoProdutoJaExistir()
        {
            //Arrange
            var contexto = CriarContexto(nameof(CadastraProduto_DeveLancarException_QuandoProdutoJaExistir));

            var service = new ProdutoService(contexto);

            var produtoTeste = new CreateProdutoDTO
            {
                id = 1,
                nome = "TESTE",
                preco = 15
            };

            var produtoTeste2 = new ProdutoModel
            {
                id = 1,
                nome = "TESTE",
                preco = 15
            };

            contexto.Produtos.Add(produtoTeste2);
            contexto.SaveChanges();

            //Act
            var exception = Assert.Throws<DomainException>(() =>
                service.CadastrarProdutoService(produtoTeste)        
            );            

            //Assert
            Assert.Equal("Produto já cadastrado", exception.Message);
        }

        //Fim testes cadastro de produto
        //

        //
        //Inicio testes listar produtos
        [Fact]
        public void ListaProduto_DeveListarOsProdutosCadastrados()
        {
            //Arrange
            var contexto = CriarContexto(nameof(ListaProduto_DeveListarOsProdutosCadastrados));

            var service = new ProdutoService(contexto);

            var produtoTeste = new CreateProdutoDTO
            {
                nome = "TESTE",
                preco = 15
            };

            service.CadastrarProdutoService(produtoTeste);

            //Act
            List<ReadProdutoDTO> produtos = service.ListarProdutosService();

            //Assert
            Assert.NotNull(produtos);
        }

        [Fact]
        public void ListaProduto_DeveLancarException_QuandoListaEstiverVazia()
        {
            //Arrange
            var contexto = CriarContexto(nameof(ListaProduto_DeveLancarException_QuandoListaEstiverVazia));

            var service = new ProdutoService(contexto);

            //Act
            var exception = Assert.Throws<DomainException>(() =>
                service.ListarProdutosService()
            );
            
            //Arrange
            Assert.Equal("Nenhum produto encontrado", exception.Message);
        }

        //Fim testes listar produtos
        //

        //
        //Inicio testes editar produto
        [Fact]
        public void EditaProduto_DeveEditarProduto()
        {
            //Arrange
            var contexto = CriarContexto(nameof(EditaProduto_DeveEditarProduto));

            var service = new ProdutoService(contexto);

            var produtoTeste = new CreateProdutoDTO
            {
                nome = "TESTE",
                preco = 15
            };

            service.CadastrarProdutoService(produtoTeste);

            var produtoEditado = new EditProdutoDTO
            {
              nome = "Nome alterado",
              preco = 10  
            };

            //Act
            service.EditarProdutoService(produtoEditado, 1);

            //Assert
            var produtoExistente = contexto.Produtos.FirstOrDefault(p => p.id == 1);

            Assert.NotNull(produtoExistente);
            Assert.Equal("Nome alterado", produtoExistente.nome);
            Assert.Equal(10, produtoExistente.preco);
        }

        [Fact]
        public void EditaProduto_DeveLancarException_QuandoNaoHouverProdutoExistente()
        {
            //Arrange
            var contexto = CriarContexto(nameof(EditaProduto_DeveLancarException_QuandoNaoHouverProdutoExistente));

            var service = new ProdutoService(contexto);
            
            var produtoEditado = new EditProdutoDTO
            {
              nome = "Nome alterado",
              preco = 10  
            };

            //Act
            var exception = Assert.Throws<DomainException>(() =>
                service.EditarProdutoService(produtoEditado, 1)
            );           

            //Assert
            Assert.Equal("Nenhum produto encontrado", exception.Message);
        }
        //Fim testes editar produto
        //
    }
}