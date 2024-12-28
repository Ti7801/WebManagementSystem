using BibliotecaBusiness.Abstractions;
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

        public Tarefa? ObterTarefa(Guid id)
        {
            Tarefa? tarefa = appDbContext.Tarefas.Where(x => x.Id == id).SingleOrDefault(); 

            return tarefa;
        }

    }
}
