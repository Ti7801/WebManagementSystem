using BibliotecaBusiness.Abstractions;
using BibliotecaBusiness.Models;


namespace BibliotecaData.Data
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext appDbContext; 

        public UsuarioRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;   
        } 

        public void AdicionarUsuario(Usuario usuario)
        {
            appDbContext.Usuarios.Add(usuario); 
            appDbContext.SaveChanges(); 
        }

        public Usuario? ObterUsuario(Guid id)
        {
            Usuario? usuario = appDbContext.Usuarios.Where(x => x.Id == id).SingleOrDefault();

            return usuario; 
        }

        public List<Usuario>? ObterListaDeUsuarios()
        {
            List<Usuario>? usuarios = appDbContext.Usuarios.ToList();

            return usuarios;
        }
    }
}
