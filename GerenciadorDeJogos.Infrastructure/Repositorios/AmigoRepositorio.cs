using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class AmigoRepositorio: RepositorioBase<Amigo> , IAmigoRepositorio
    {
        public AmigoRepositorio(JogosContexto contexto): base(contexto) { }
    }
}
