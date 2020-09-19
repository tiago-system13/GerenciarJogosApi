using GerenciadorDeJogos.Application.Models;
using System;

namespace GerenciadorDeJogos.Application.Exceptions
{
    public class NegocioException : Exception
    {
        public Problema Problema { get; set; }

        public NegocioException(string mensagem) : base(mensagem) { }

        public NegocioException(Problema problema)
        {
            this.Problema = problema;
        }
    }
}
