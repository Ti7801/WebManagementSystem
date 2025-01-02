using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Abstractions
{
    public interface ITarefaRepository
    {
        public void AdicionarTarefa(Tarefa tarefa);
        public List<Tarefa>? ObterTarefasPorUsuarioGestor(Guid id);
        public Tarefa? ObterTarefaPorId(Guid id);
        public void AtualizarTarefa(Tarefa tarefa);
    }
}
