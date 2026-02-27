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
    }
}