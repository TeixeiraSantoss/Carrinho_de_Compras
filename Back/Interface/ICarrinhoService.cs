using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Models;

namespace Back.Interface
{
    public interface ICarrinhoService
    {
        public CarrinhoModel CriaCarrinhoService();
        public void AdicionarItemService(int produtoId, int carrinhoId, int quantidade);
        public void RemoverItemService(int produtoId, int carrinhoId);
    }
}