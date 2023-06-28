using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEstoque.Repository.Exceptions
{
    public class CreateUserException : Exception
    {
        public int StatusCode { get; }
        public string Mensagem { get; }

        public CreateUserException(int statusCode, string mensagem) : base(mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
    }
}
