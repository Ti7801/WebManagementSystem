namespace BibliotecaBusiness.Models
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public List<string> Erros { get; set; }

        public ServiceResult()
        {
            this.Erros = new List<string>();
        }
    }

    public class ServiceResult<T>
    {
        public bool Success { get; set; }

        public List<string> Erros { get; set; }

        public T? Value { get; set; }

        public ServiceResult()
        {
            this.Erros = new List<string>();
        }
        public bool HasValue() => Success && Value != null;
    }
}
