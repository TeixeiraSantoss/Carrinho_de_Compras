using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class ItemCarrinhoModel
    {
        public int id { get; set; }
        public int quantidade { get; set; }
        public int produtoId { get; set; }
        public ProdutoModel Produto { get; set; } = null!;
        public int carrinhoId { get; set; }
        public CarrinhoModel Carrinho { get; set; } = null!;
    }
}