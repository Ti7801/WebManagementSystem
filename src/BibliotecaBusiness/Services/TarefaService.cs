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
                tarefa.StatusTarefa = StatusTarefa.pendente;
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

        public ServiceResult<List<Tarefa>?> ConsultarTarefasPorGestor(Guid usuarioId)
        {
            ServiceResult<List<Tarefa>?> serviceResult = new ServiceResult<List<Tarefa>?>();

            try
            {
                List<Tarefa>? tarefas = tarefaRepository.ObterTarefasPorUsuarioGestor(usuarioId);
                serviceResult.Value = tarefas;
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

        
        public ServiceResult MudarStatusDaTarefa(Guid id, StatusTarefa statusTarefa)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                Tarefa? tarefa = tarefaRepository.ObterTarefaPorId(id);

                if (tarefa == null)
                {
                    serviceResult.Erros.Add($"Não existe a tarefa de {id} correspondente");
                    serviceResult.Success = false;

                    return serviceResult;
                }

                tarefa.Id = id;
                tarefa.StatusTarefa = statusTarefa;

                tarefaRepository.AtualizarTarefa(tarefa);
                serviceResult.Success = true;

                return serviceResult;
            }
            catch (Exception e) 
            {
                logger.LogError(e.ToString());
                serviceResult.Success= false;
                serviceResult.Erros.Add(e.ToString());

                return serviceResult;
            }            
        }


        public ServiceResult<List<Tarefa>?> ConsultarTarefaPorUsuario(Guid usuarioId)
        {
            ServiceResult<List<Tarefa>?> serviceResult = new ServiceResult<List<Tarefa>?>();

            try
            {
                List<Tarefa>? tarefas = tarefaRepository.ObterTarefaPorUsuario(usuarioId);
                serviceResult.Value = tarefas;
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

