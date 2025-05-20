using System.ComponentModel.DataAnnotations;

namespace BibliotecaBusiness.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        [Required]
        public string Messagem { get; set; }
        [Required]
        public DateTime DataLimiteExecucao { get; set; }
        public StatusTarefa StatusTarefa { get; set; }
        [Required]
        public Guid GestorId { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
