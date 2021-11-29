using DapperRazorPages.Entities;
using System.Collections.Generic;

namespace DapperRazorPages.Repository
{
    public interface IProdutoRepository
    {
        int Add(Produto produto);
        List<Produto> GetProdutos();
        Produto Get(int id);
        int Edit(Produto produto);
        int Delete(int id);
    }
}
