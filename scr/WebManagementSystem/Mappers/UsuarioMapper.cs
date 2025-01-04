using BibliotecaBusiness.Models;
using WebManagementSystem.ViewModels;

namespace BibliotecaBusiness.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario Map(UsuarioViewModel viewModel, string? path)
        {
            Usuario usuario = new Usuario();

            usuario.Id = viewModel.Id;
            usuario.Nome = viewModel.Nome;
            usuario.Nascimento = viewModel.Nascimento;
            usuario.Telefone = viewModel.Telefone;
            usuario.Celular = viewModel.Celular;
            usuario.Email = viewModel.Email;
            usuario.Endereco = viewModel.Endereco;
            usuario.Foto = path ?? string.Empty;

            return usuario;
        }
    }
}
