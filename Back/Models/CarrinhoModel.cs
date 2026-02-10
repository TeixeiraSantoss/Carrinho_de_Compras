using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class CarrinhoModel
    {
        public int id { get; set; }
        public ICollection<ItemCarrinhoModel> itens { get; set; } = new List<ItemCarrinhoModel>();
    }
}