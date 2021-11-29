using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperRazorPages.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DapperRazorPages.Pages.Produto
{
    public class EditModel : PageModel
    {
        IProdutoRepository _produtoRepository;

        public EditModel(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [BindProperty]
        public Entities.Produto Produto { get; set; }

        [TempData]
        public string Message { get; set; }

        public void OnGet(int id)
        {
            Produto = _produtoRepository.Get(id);
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var count = _produtoRepository.Edit(Produto);
                if (count > 0)
                {
                    Message = "Produto modificado com sucesso!";
                    return RedirectToPage("/Produto/Index");
                }
            }
            return Page();
        }
    }
}