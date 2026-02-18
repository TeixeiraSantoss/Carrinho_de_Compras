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
        public CarrinhoModel CriaCarrinhoService()
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
        public void AdicionarItemService(AddItemDTO dadosItem)
        {
            //Verifica se a quantidade é menor que 1
            if(dadosItem.quantidade < 1)
            {
                throw new DomainException("Quantidade invalida");
            }

            var carrinho = _ctx.Carrinhos.Include(c => c.itens).FirstOrDefault(c => c.id == dadosItem.carrinhoId);
            //Verifica se existe um carrinho
            if(carrinho == null)
            {
                carrinho = CriaCarrinhoService();
            }
            
            var produto = _ctx.Produtos.FirstOrDefault(p => p.id == dadosItem.produtoId);

            if(produto == null)
            {
                throw new DomainException("Produto não encontrado");
            }
            
            var itemExistente = carrinho.itens.FirstOrDefault(i => i.produtoId == dadosItem.produtoId);

            //Adiciona a quantidade ao item existente  
            if(itemExistente != null)
            {
                itemExistente.quantidade += dadosItem.quantidade;
            }
            else
            {
                var novoItem = new ItemCarrinhoModel{
                    produtoId = dadosItem.produtoId,
                    carrinhoId = dadosItem.carrinhoId,
                    quantidade = 1
                };   

                _ctx.ItensCarrinho.Add(novoItem);
            }

            _ctx.SaveChanges();

        }
        //Fim adicionar item
        //

        //
        //Inicio Remover Item
        public void RemoverItemService(RemoveItemDTO dadosItem)
        {
            var carrinho = _ctx.Carrinhos.Include(c => c.itens).FirstOrDefault(c => c.id == dadosItem.carrinhoId);
            if(carrinho == null)
            {
                throw new DomainException("Carrinho não encontrado");
            }

            var itemExistente = carrinho.itens.FirstOrDefault(i => i.produtoId == dadosItem.produtoId);
            if(itemExistente == null)
            {
                throw new DomainException("Produto não encontrado no carrinho");
            }

            _ctx.ItensCarrinho.Remove(itemExistente);
            _ctx.SaveChanges();
        }
        //Fim Remover item
        //

        //
        //Inicio calculo total carrinho
        public decimal CalculoTotalCarrinho(int carrinhoId) 
        { 
            CarrinhoModel carrinhoExistente = _ctx.Carrinhos.Include(c => c.itens).ThenInclude(i => i.Produto).FirstOrDefault(c => c.id == carrinhoId);

            if(carrinhoExistente == null)
            {
                throw new DomainException("Nenhum carrinho encontrado");
            }
            
            return carrinhoExistente.itens.Sum(i => i.Produto.preco * i.quantidade);
        }
        //Fim calculo total carrinho
        //
    }
}