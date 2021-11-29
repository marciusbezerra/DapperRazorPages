using DapperRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;


namespace DapperRazorPages.Pages.Produto
{
    public class IndexModel : PageModel
    {
        IProdutoRepository _produtoRepository;

        public IndexModel(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [BindProperty]
        public List<Entities.Produto> ListaProdutos { get; set; }

        [BindProperty]
        public Entities.Produto Produto { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet()
        {
            ListaProdutos = _produtoRepository.GetProdutos();
        }

        public IActionResult OnPostDelete(int id)
        {
            if (id > 0)
            {
                var count = _produtoRepository.Delete(id);
                if (count >0)
                {
                    Message = "Produto deletado com sucesso!";
                    return RedirectToPage("/Produto/Index");
                }
            }
            return Page();
        }
    }
}