using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Back.DTOs.ProdutoDTO;

namespace Back.Interface
{
    public interface IProdutoService
    {
        public void CadastrarProdutoService(CreateProdutoDTO dadosProduto);
        public List<ReadProdutoDTO> ListarProdutosService();
        public void EditarProdutoService(EditProdutoDTO dadosProduto, int id);
        public void DeletarProdutoService(int id);
    }
}