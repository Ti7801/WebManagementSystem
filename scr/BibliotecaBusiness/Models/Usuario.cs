using System.ComponentModel.DataAnnotations;

namespace BibliotecaBusiness.Models
{
    public class Usuario
    {
        public long Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Senha { get; set; }
        [Required]
        public TipoUsuario TipoUsuario { get; set; }
        public DateTime DataCriacao { get; set; }
    }
}
