using GerenciadorDeJogos.Application.Models.Request;

namespace GerenciadorDeJogos.Application.Models.Responses
{
    public class AmigoResponse:ModelBase
    {
        public string Nome { get; set; }

        public string Telefone { get; set; }
    }
}
