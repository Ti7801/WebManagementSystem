using System.ComponentModel.DataAnnotations;

namespace BibliotecaBusiness.Models
{
    public class Tarefa
    {
        public long Id { get; set; }
        [Required]
        public string Message { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        [Required]
        public StatusTarefa StatusTarefa { get; set; }
        public long GestorId { get; set; }
    }
}
