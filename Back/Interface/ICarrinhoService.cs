using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.DTOs.CarrinhoDTO;
using Back.Models;

namespace Back.Interface
{
    public interface ICarrinhoService
    {
        public CarrinhoModel CriaCarrinhoService();
        public void AdicionarItemService(AddItemDTO dadosItem);
        public void RemoverItemService(int produtoId, int carrinhoId);
    }
}