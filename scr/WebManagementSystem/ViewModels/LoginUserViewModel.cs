using System.ComponentModel.DataAnnotations;

namespace WebManagementSystem.ViewModels
{
    public class LoginUserViewModel
    {
        [Display(Name = "email")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [EmailAddress(ErrorMessage = "O campo {0} está formato inválido")]
        public string Email { get; set; }

        [Display(Name = "senha")]
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter {2} e {1} caracteres", MinimumLength = 6)]
        public string Senha {  get; set; }


    }
}
