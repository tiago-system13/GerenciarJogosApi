using GerenciadorDeJogos.Application.Models.Request;

namespace GerenciadorDeJogos.Application.Models.Result
{
    public class UsuarioResult:ModelBase
    {
        public string Nome { get; set; }

        public string Login { get; set; }

        public string Senha { get; set; }
    }
}
