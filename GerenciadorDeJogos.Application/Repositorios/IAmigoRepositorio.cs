using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IAmigoRepositorio : IRepositorioBase<Amigo>
    {
        ListaPaginavel<Amigo> PesquisarAmigos(Pesquisa pesquisa);
    }
}
