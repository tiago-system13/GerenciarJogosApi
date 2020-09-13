using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class JogoRepositorio : RepositorioBase<Jogo>, IJogoRepositorio
    {
        public JogoRepositorio(JogosContexto contexto) : base(contexto) { }
    }
}
