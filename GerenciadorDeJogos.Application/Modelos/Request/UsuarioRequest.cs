namespace GerenciadorDeJogos.Application.Models.Request
{
    public class UsuarioRequest:ModelBase
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
