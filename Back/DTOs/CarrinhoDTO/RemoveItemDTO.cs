using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.DTOs.CarrinhoDTO
{
    public class RemoveItemDTO
    {
        public int produtoId { get; set; }
        public int carrinhoId { get; set; }
    }
}