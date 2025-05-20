using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Exceptions;
using BibliotecaBusiness.Models;

namespace BibliotecaData.Data
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly AppDbContext appDbContext;

        public TarefaRepository(AppDbContext appDbContext) 
        {
            this.appDbContext = appDbContext;
        }

        public void AdicionarTarefa(Tarefa tarefa)
        {
            appDbContext.Tarefas.Add(tarefa);
            appDbContext.SaveChanges(); 
        }

        public List<Tarefa>? ObterTarefasPorUsuarioGestor(Guid gestorId)
        {
            List<Tarefa> tarefas = appDbContext.Tarefas.Where(x => x.GestorId == gestorId).ToList(); 

            return tarefas;
        }

        public Tarefa? ObterTarefaPorId(Guid id)
        {
            Tarefa? tarefa = appDbContext.Tarefas.SingleOrDefault(tarefa => tarefa.Id == id);

            return tarefa;
        }

        public List<Tarefa>? ObterTarefaPorUsuario(Guid usuarioId)
        {
            List<Tarefa>? tarefas = appDbContext.Tarefas.Where(x => x.UsuarioId == usuarioId).ToList();

            return tarefas;
        }


        public void AtualizarTarefa(Tarefa tarefa)
        {
            Tarefa? tarefaPesquisada = ObterTarefaPorId(tarefa.Id);

            if (tarefaPesquisada == null)
            {
                const string message = "Identificação da tarefa não encontrada";
                throw new TarefaNaoEncontradaException(message);
            }

            tarefaPesquisada.Id = tarefa.Id;
            tarefaPesquisada.Messagem = tarefa.Messagem;
            tarefaPesquisada.DataLimiteExecucao = tarefa.DataLimiteExecucao;
            tarefaPesquisada.StatusTarefa = tarefa.StatusTarefa;
            tarefaPesquisada.GestorId = tarefa.GestorId;
            tarefaPesquisada.UsuarioId = tarefa.UsuarioId;  

            appDbContext.Update(tarefaPesquisada);
            appDbContext.SaveChanges();
        }
    }
}
