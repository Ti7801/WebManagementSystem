namespace BibliotecaBusiness.Exceptions
{
    public class UsuarioNaoEncontradoException : Exception
    {
        public UsuarioNaoEncontradoException() { }
        public UsuarioNaoEncontradoException(string message) : base(message) { }
        public UsuarioNaoEncontradoException(string message, Exception e) : base(message, e) { }
    }
}
