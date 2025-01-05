using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;
using Microsoft.Extensions.Logging;

namespace BibliotecaBusiness.Services
{
    public class UsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;
        private readonly ILogger<UsuarioService> logger;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                              ILogger<UsuarioService> logger)
        {
            this.usuarioRepository = usuarioRepository; 
            this.logger = logger;
        }

        public ServiceResult CadastrarUsuario(Usuario usuario)
        {
            ServiceResult serviceResult = new ServiceResult();  

            try
            {
                usuarioRepository.AdicionarUsuario(usuario);
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

        public ServiceResult<Usuario> ConsultarUsuario(Guid id)
        {
            ServiceResult<Usuario> serviceResult = new ServiceResult<Usuario>();

            try
            {
                Usuario? usuario = usuarioRepository.ObterUsuario(id);
                serviceResult.Value = usuario;
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


        public ServiceResult<List<Usuario>?> ObterUsuarios()
        {
            ServiceResult<List<Usuario>?> serviceResult = new ServiceResult<List<Usuario>?>();

            try
            {
                List<Usuario>? usuarios = usuarioRepository.ObterListaDeUsuarios();
                serviceResult.Value = usuarios;
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
