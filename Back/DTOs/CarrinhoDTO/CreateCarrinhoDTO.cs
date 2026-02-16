using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.Models;

namespace Back.DTOs.CarrinhoDTO
{
    public class CreateCarrinhoDTO
    {
        public int id { get; set; }
        public ICollection<ItemCarrinhoModel> itens { get; set; } = new List<ItemCarrinhoModel>();
    }
}