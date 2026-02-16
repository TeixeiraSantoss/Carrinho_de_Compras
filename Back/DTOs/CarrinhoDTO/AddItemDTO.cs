using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.DTOs.CarrinhoDTO
{
    public class AddItemDTO
    {
        public int produtoId { get; set; }
        public int carrinhoId { get; set; }
        public int quantidade { get; set; }
    }
}