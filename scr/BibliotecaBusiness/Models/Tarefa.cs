using System.ComponentModel.DataAnnotations;

namespace BibliotecaBusiness.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime DataConclusao { get; set; }
        public StatusTarefa StatusTarefa { get; set; }
        public Guid GestorId { get; set; }
        public Guid? UsuarioId { get; set; }
    }
}
