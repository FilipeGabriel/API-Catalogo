using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Pagination;
using X.PagedList;

namespace APICatalogo.Repositories;

public class ProdutoRepository : Repository<Produto>, IProdutoRepository
{

    public ProdutoRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtosParams)
    {
        var produtos = await GetAllAsync();

        var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();

        var resultado = await produtosOrdenados.ToPagedListAsync(produtosParams.PageNumber, produtosParams.PageSize);

        return resultado;
    }

    public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroParam)
    {
        var produtos = await GetAllAsync();

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

        var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroParam.PageNumber, produtosFiltroParam.PageSize);

        return produtosFiltrados;
    }

    public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
    {
        var produtos = await GetAllAsync();

        var produtosCategoria = produtos.Where(c => c.CategoriaId == id);

        return produtosCategoria;
    }
}
