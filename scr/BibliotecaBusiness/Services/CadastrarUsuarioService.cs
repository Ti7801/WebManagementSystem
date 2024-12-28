using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Services
{
    public class CadastrarUsuarioService
    {
        private readonly IUsuarioRepository usuarioRepository;

        public CadastrarUsuarioService(IUsuarioRepository usuarioRepository)
        {
            this.usuarioRepository = usuarioRepository; 
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
                serviceResult.Success = false;
                serviceResult.Erros.Add(e.Message);

            }
            return serviceResult;
        }
    }
}
