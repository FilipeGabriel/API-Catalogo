using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{

    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public PagedList<Produto> GetProdutos(ProdutosParameters produtosParams)
    {
        var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
        var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtosParams.PageNumber, produtosParams.PageSize);
        return produtosOrdenados;
    }

    public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroParam)
    {
        var produtos = GetAll().AsQueryable();

        if (produtosFiltroParam.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroParam.PrecoCriterio))
        {
            if (produtosFiltroParam.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco > produtosFiltroParam.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParam.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco < produtosFiltroParam.Preco.Value).OrderBy(p => p.Preco);
            }
            else if (produtosFiltroParam.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
            {
                produtos = produtos.Where(p => p.Preco == produtosFiltroParam.Preco.Value).OrderBy(p => p.Preco);
            }
        }

        var produtosFiltrados = PagedList<Produto>.ToPagedList(produtos, produtosFiltroParam.PageNumber, produtosFiltroParam.PageSize);

        return produtosFiltrados;
    }

    public IEnumerable<Produto> GetProdutosPorCategoria(int id)
    {
        return GetAll().Where(c => c.CategoriaId == id);
    }
}
