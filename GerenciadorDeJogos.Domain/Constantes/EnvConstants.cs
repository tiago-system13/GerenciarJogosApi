using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Domain.Constantes
{
    public class EnvConstants
    {
        public static readonly string ISSUER = Environment.GetEnvironmentVariable("ISSUER");
        public static readonly string  AUDIENCE = Environment.GetEnvironmentVariable("AUDIENCE");
        public static readonly string TEMPOEXPIRACAOTOKEN = Environment.GetEnvironmentVariable("TEMPOEXPIRACAOTOKEN");
    }
}
