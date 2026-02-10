using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Back.Models
{
    public class ItemCarrinho
    {
        public int id { get; set; }
        public int produtoId { get; set; }
        public int quantidade { get; set; }
        public int subTotal { get; set; }
    }
}