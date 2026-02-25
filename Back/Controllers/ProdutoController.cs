using Back.Data;
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
}
