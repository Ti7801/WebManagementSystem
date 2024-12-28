using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Services
{
    public class CadastrarTarefaService
    {
        private readonly ITarefaRepository tarefaRepository;

        public CadastrarTarefaService(ITarefaRepository tarefaRepository)
        {
            this.tarefaRepository = tarefaRepository;
        }

        public ServiceResult CadastrarTarefa(Tarefa tarefa)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                tarefaRepository.AdicionarTarefa(tarefa);
                serviceResult.Success = true;
            }
            catch (Exception e)
            {
                serviceResult.Success = false;
                serviceResult.Erros.Add(e.Message);
            }
            return serviceResult;
        }
    }
}
