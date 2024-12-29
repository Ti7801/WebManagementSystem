using BibliotecaBusiness.Models;
using WebManagementSystem.ViewModels;

namespace BibliotecaBusiness.Mappers
{
    public static class UsuarioMapper
    {
        public static Usuario Map(UsuarioViewModel viewModel)
        {
            Usuario usuario = new Usuario();

            usuario.Id = viewModel.Id;
            usuario.Nome = viewModel.Nome;
            usuario.Nascimento = viewModel.Nascimento;
            usuario.Telefone = viewModel.Telefone;
            usuario.Email = viewModel.Email;
            usuario.Endereco = viewModel.Endereco;
            usuario.Foto = viewModel.Foto;

            return usuario;
        }

        public static UsuarioViewModel Map(Usuario usuario)
        {
            UsuarioViewModel viewModel = new UsuarioViewModel();

            viewModel.Id = usuario.Id;
            viewModel.Nome = usuario.Nome;
            viewModel.Nascimento = usuario.Nascimento;
            viewModel.Telefone = usuario.Telefone;
            viewModel.Email = usuario.Email;
            viewModel.Endereco = usuario.Endereco;
            viewModel.Foto = usuario.Foto;

            return viewModel;
        }

    }
}
