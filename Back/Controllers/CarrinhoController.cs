using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Back.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers
{
    [ApiController]
    [Route("api/carrinho")]
    public class CarrinhoController : ControllerBase
    {
        private readonly ICarrinhoService _carrinhoService;
        public CarrinhoController(ICarrinhoService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpPost("criar")]
        public IActionResult CriarCarrinho()
        {
            _carrinhoService.CriaCarrinhoService();
            return Ok(new {message = "Carrinho criado com sucesso"});
        }

        [HttpPost("adicionarItem")]
        public IActionResult AdicionarItem()
        {
            
        }
    }
}