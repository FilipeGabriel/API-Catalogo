using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public interface IProdutoRepository : IRepository<Produto>
{
    PagedList<Produto> GetProdutos(ProdutosParameters produtosParams);

    PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParam);

    IEnumerable<Produto> GetProdutosPorCategoria(int id);
}
