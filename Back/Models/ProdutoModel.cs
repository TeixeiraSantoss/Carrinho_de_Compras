using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class ProdutoModel
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal preco { get; set; }
        public ICollection<ItemCarrinhoModel> itensCarrinho { get; set; } = new List<ItemCarrinhoModel>();
    }
}