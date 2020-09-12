namespace GerenciadorDeJogos.Application.Seguranca.Configuracao
{
    public class TokenConfiguracao
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
    }
}
