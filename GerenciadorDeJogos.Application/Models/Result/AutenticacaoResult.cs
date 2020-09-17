using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Application.Models.Result
{
    public class AutenticacaoResult
    {
        public bool Autenticated { get; set; }

        public string Created { get; set; }

        public string Expiration { get; set; }

        public string AccessToken { get; set; }

        public string Message { get; set; }
        
    }
}
