using BibliotecaBusiness.Models;
using Microsoft.Extensions.Logging;
using BibliotecaBusiness.Abstractions;

namespace BibliotecaBusiness.Services
{
    public class TarefaService
    {
        private readonly ITarefaRepository tarefaRepository;
        private readonly ILogger<TarefaService> logger;  

        public TarefaService(ITarefaRepository tarefaRepository,
                            ILogger<TarefaService> logger)
        {
            this.tarefaRepository = tarefaRepository;
            this.logger = logger;   
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
                logger.LogError(e.ToString());
                serviceResult.Success = false;
                serviceResult.Erros.Add(e.Message);
            }
            return serviceResult;
        }

        public ServiceResult<Tarefa> ConsultarTarefa(Guid id)
        {
            ServiceResult<Tarefa> serviceResult = new ServiceResult<Tarefa>();

            try
            {
                Tarefa? tarefa = tarefaRepository.ObterTarefa(id);
                serviceResult.Value = tarefa;
                serviceResult.Success = true;
                return serviceResult;
            }
            catch (Exception e)
            {
                logger.LogError(e.ToString());
                serviceResult.Success = false;
                return serviceResult;
            }
        }
    }
}

