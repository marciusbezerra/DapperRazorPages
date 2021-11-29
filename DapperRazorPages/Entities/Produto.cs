using System.ComponentModel.DataAnnotations;

namespace DapperRazorPages.Entities
{
    public class Produto
    {
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nome do Produto")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }

        [Required]
        [Display(Name = "Estoque")]
        [Range(1, 10000, ErrorMessage = "O estoque deve estar entre 1 e 10000")]
        public decimal Estoque { get; set; }

        [Required]
        [Display(Name = "Preço")]
        public decimal Preco { get; set; }
    }
}
