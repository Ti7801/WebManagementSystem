﻿using BibliotecaBusiness.Models;

namespace BibliotecaBusiness.Abstractions
{
    public interface IUsuarioRepository
    {
        public void AdicionarUsuario(Usuario usuario);
        public Usuario? ObterUsuario(Guid id);
        public List<Usuario>? ObterListaDeUsuarios();
    }
}
