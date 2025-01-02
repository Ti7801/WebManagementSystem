using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaBusiness.Exceptions
{
    public class TarefaNaoEncontradaException : Exception
    {
        public TarefaNaoEncontradaException() { }
        public TarefaNaoEncontradaException(string message) : base(message) { }
        public TarefaNaoEncontradaException(string message, Exception e) : base(message, e) { }
    }
}
