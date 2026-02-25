using Back.Data;
using Back.DTOs.ProdutoDTO;
using Back.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Back.Controllers;

[ApiController]
[Route("api/produto")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpPost("cadastrar")]
    public IActionResult CadastrarProduto([FromBody] CreateProdutoDTO dadosProduto)
    {
        _produtoService.CadastrarProdutoService(dadosProduto);
        return Ok(new {message = "Produto cadastrado com sucesso"});
    }

    [HttpGet("listar")]
    public IActionResult ListarProdutos()
    {
        return Ok(_produtoService.ListarProdutosService()); 
    }

    [HttpPatch("editar/{id}")]
    public IActionResult EditarProduto([FromRoute] int id, [FromBody] EditProdutoDTO dadosProduto)
    {
        _produtoService.EditarProdutoService(dadosProduto, id);

        return Ok(new {message = "Produto editado com sucesso"});
    }

    [HttpDelete("deletar/{id}")]
    public IActionResult DeletarProduto([FromRoute] int id)
    {
        _produtoService.DeletarProdutoService(id);
        return Ok(new {message = "Produto removido com sucesso"});
    }
}
