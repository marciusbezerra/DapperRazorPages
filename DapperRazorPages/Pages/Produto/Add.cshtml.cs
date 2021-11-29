using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperRazorPages.Pages.Produto
{
    public class AddModel : PageModel
    {
        IProdutoRepository _produtoRepository;

        public AddModel(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [BindProperty]
        public Entities.Produto Produto { get; set; } = new Entities.Produto();

        [TempData]
        public string Message { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var count = _produtoRepository.Add(Produto);
                if (count > 0)
                {
                    Message = "Produto incluído com sucesso!";
                    return RedirectToPage("/Produto/Index");
                }
            }
            return Page();
        }
    }
}