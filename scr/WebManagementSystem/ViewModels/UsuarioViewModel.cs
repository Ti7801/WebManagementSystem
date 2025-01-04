using System.ComponentModel.DataAnnotations;

namespace WebManagementSystem.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Celular { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public IFormFile Foto { get; set; }
        [Required]
        public bool EhGestor { get; set; }
    }
}