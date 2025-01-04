using System.ComponentModel.DataAnnotations;

namespace BibliotecaBusiness.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime Nascimento { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Celular {  get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Required]
        public string Foto { get; set; } 
    }
}
