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
    public class ProdutoService : IProdutoService
    {
        private readonly AppDbContext _ctx;

        public ProdutoService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void CadastrarProdutoService(CreateProdutoDTO dadosProduto)
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

        public List<ReadProdutoDTO> ListarProdutosService()
        {
            List<ReadProdutoDTO> produtos = _ctx.Produtos.Select(p => new ReadProdutoDTO{ 
                                                                            id = p.id,
                                                                            nome = p.nome,
                                                                            preco = p.preco 
                                                                            }).ToList();

            if(produtos.Count() == 0)
            {
                throw new DomainException("Nenhum produto encontrado");
            }

            return produtos;
        }

        public ReadProdutoDTO BuscarProdutoService(int id)
        {
            ReadProdutoDTO produtoExistente = _ctx.Produtos.Select(p => new ReadProdutoDTO{ id = p.id, nome = p.nome, preco = p.preco }).FirstOrDefault(p => p.id == id);

            if(produtoExistente != null)
            {
                throw new DomainException("Nenhum Produto encontrado");
            }

            return produtoExistente;
        }

        public void EditarProdutoService(EditProdutoDTO dadosProduto, int id)
        {
            ProdutoModel produtoExistente = _ctx.Produtos.Find(id);

            if(produtoExistente == null)
            {
                throw new DomainException("Nenhum produto encontrado");
            }

            produtoExistente.nome = dadosProduto.nome;
            produtoExistente.preco = dadosProduto.preco;

            _ctx.SaveChanges();
        }

        public void DeletarProdutoService(int id)
        {
            ProdutoModel produtoExistente = _ctx.Produtos.Find(id);

            if(produtoExistente == null)
            {
                throw new DomainException("Nenhum produto encontrado");
            }

            _ctx.Produtos.Remove(produtoExistente);
            _ctx.SaveChanges();
        }
    }
}