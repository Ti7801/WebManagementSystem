using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Abstractions
{
    public interface ITarefaRepository
    {
        public void AdicionarTarefa(Tarefa tarefa);
        public Tarefa? ObterTarefa(Guid id);
    }
}
