namespace WebManagementSystem.ViewModels
{
    public class UsuarioViewModel
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public DateTime Nascimento { get; set; }

        public string Telefone { get; set; }

        public string Celular { get; set; }

        public string Email { get; set; }

        public string Endereco { get; set; }

        public string Foto { get; set; }

        public string Senha { get; set; }

        public bool EhGestor { get; set; }
    }
}