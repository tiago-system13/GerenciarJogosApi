using GerenciadorDeJogos.Domain.Entidades.Base;

namespace GerenciadorDeJogos.Domain.Entidades
{
    public class Usuario: Entidade
    { 
        public string Login { get; set; }
        public string AccessKey { get; set; }
    }
}
