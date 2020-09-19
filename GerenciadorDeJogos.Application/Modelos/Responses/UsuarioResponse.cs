using GerenciadorDeJogos.Application.Models.Request;

namespace GerenciadorDeJogos.Application.Models.Responses
{
    public class UsuarioResponse:ModelBase
    {
        public string Nome { get; set; }

        public string Login { get; set; }
    }
}
