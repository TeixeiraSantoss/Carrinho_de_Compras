using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class CarrinhoModel
    {
        public int id { get; set; }
        public List<ItemCarrinhoModel> listaItens { get; set; }
        public int valorTotal { get; set; }
    }
}