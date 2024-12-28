using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Services
{
    public class ConsultarUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public ConsultarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository;
        }

        public ServiceResult ConsultarUsuario(Guid id)
        {
            ServiceResult serviceResult = new ServiceResult();

            try
            {
                usuarioRepository.ObterUsuario(id);
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
