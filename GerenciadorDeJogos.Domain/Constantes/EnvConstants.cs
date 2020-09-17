using System;
using System.Collections.Generic;
using System.Text;

namespace GerenciadorDeJogos.Domain.Constantes
{
    public class EnvConstants
    {
        public static readonly string ISSUER = Environment.GetEnvironmentVariable("ISSUER");
        public static readonly string Audience = Environment.GetEnvironmentVariable("Audience");
        public static readonly string TempoExpiracaoToken = Environment.GetEnvironmentVariable("TempoExpiracaoToken");
    }
}
