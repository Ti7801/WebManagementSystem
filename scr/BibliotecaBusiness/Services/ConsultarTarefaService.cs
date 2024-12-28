using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Services
{
    public class ConsultarTarefaService
    {
        private readonly ITarefaRepository tarefaRepository;

        public ConsultarTarefaService(ITarefaRepository tarefaRepository)
        {
            this.tarefaRepository = tarefaRepository;
        }

        public ServiceResult ConsultarTarefa(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                tarefaRepository.ObterTarefa(id);
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
