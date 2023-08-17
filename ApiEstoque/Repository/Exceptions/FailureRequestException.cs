using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiEstoque.Repository.Exceptions
{
    public class FailureRequestException : Exception
    {
        public int StatusCode { get; }
        public string Mensagem { get; }

        public FailureRequestException(int statusCode, string mensagem) : base(mensagem)
        {
            StatusCode = statusCode;
            Mensagem = mensagem;
        }
    }
}
