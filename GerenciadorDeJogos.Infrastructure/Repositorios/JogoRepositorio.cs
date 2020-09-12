using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class JogoRepositorio : RepositorioBase<Jogo>, IJogoRepositorio
    {
        public JogoRepositorio(JogosContexto contexto) : base(contexto) { }

        public ListaPaginavel<Jogo> PesquisarJogos(Pesquisa pesquisa)
        {
            IQueryable<Jogo> query;

            query = TodosIncluindo(x=> x.Proprietario).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa.Nome))
            {
                query = query.Where(x => x.Nome.ToLower().Contains(pesquisa.Nome.ToLower()));
            }

            return query.ParaListaPaginavel(pesquisa.IndiceDePagina, pesquisa.RegistrosPorPagina, pesquisa.Ordenacao, x => x.Nome);
        }
    }
}
