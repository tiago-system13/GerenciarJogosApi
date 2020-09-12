using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IJogoRepositorio:IRepositorioBase<Jogo>
    {
        ListaPaginavel<Jogo> PesquisarJogos(Pesquisa pesquisa);

    }
}
