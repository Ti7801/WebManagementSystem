using BibliotecaBusiness.Models;
using System.ComponentModel.DataAnnotations;

namespace WebManagementSystem.ViewModels
{
    public class TarefaViewModel
    {
        public Guid Id { get; set; }
        [Required]
        public string Messagem { get; set; }
        [Required]
        public  DateTime DataLimiteExecucao { get; set; }
        [Required]
        public StatusTarefa StatusTarefa { get; set; }
        [Required]
        public Guid GestorId { get; set; }

        public Guid? UsuarioId { get; set; }
    }
}
