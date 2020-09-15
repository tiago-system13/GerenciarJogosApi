using GerenciadorDeJogos.Domain.Entidades.Base;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class Usuario: Entidade
    {
        public string Nome { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }
    }
}
