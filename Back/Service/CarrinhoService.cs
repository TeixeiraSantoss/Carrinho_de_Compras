using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Data;
using Back.DTOs.CarrinhoDTO;
using Back.Interface;
using Back.Models;
using Microsoft.EntityFrameworkCore;

namespace Back.Service
{
    public class CarrinhoService : ICarrinhoService
    {
        private readonly AppDbContext _ctx;
        public CarrinhoService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        //
        //Cria carrinho
        public CarrinhoModel CriaCarrinho()
        {
            CarrinhoModel novoCarrinho = new CarrinhoModel {
                itens = []
            };

            _ctx.Carrinhos.Add(novoCarrinho);
            _ctx.SaveChanges();

            return novoCarrinho;
        }
        //Fim criar carrinho
        //

        //
        //Adicionar Item
        public void AdicionarItem(int produtoId, int carrinhoId, int quantidade)
        {
            //Verifica se a quantidade é menor que 1
            if(quantidade < 1)
            {
                throw new DomainException("Quantidade invalida");
            }

            var carrinho = _ctx.Carrinhos.Include(c => c.itens).FirstOrDefault(c => c.id == carrinhoId);
            //Verifica se existe um carrinho
            if(carrinho == null)
            {
                carrinho = CriaCarrinho();
            }
            
            var produto = _ctx.Produtos.FirstOrDefault(p => p.id == produtoId);

            if(produto == null)
            {
                throw new DomainException("Produto não encontrado");
            }
            
            var itemExistente = carrinho.itens.FirstOrDefault(i => i.produtoId == produtoId);
            
            if(itemExistente != null)
            {
                itemExistente.quantidade += quantidade;
            }

            var novoItem = new ItemCarrinhoModel{
                produtoId = produtoId,
                carrinhoId = carrinhoId,
                quantidade = 1
            };

            _ctx.ItensCarrinho.Add(novoItem);
            _ctx.SaveChanges();

        }
        //Fim adicionar item
        //
    }
}