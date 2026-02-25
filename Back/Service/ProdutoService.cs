using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Data;
using Back.DTOs.ProdutoDTO;
using Back.Interface;
using Back.Models;

namespace Back.Service
{
    public class ProdutoService: IProdutoService
    {
        private readonly AppDbContext _ctx;

        public ProdutoService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void CadastrarProduto(CreateProdutoDTO dadosProduto)
        {
            ProdutoModel produtoExistente = _ctx.Produtos.FirstOrDefault(p => p.id == dadosProduto.id);

            if(produtoExistente != null)
            {
                throw new DomainException("Produto já cadastrado");
            }

            ProdutoModel novoProduto = new ProdutoModel
            {
                nome = dadosProduto.nome,
                preco = dadosProduto.preco
            };

            _ctx.Produtos.Add(novoProduto);
            _ctx.SaveChanges();
        }
        
    }
}