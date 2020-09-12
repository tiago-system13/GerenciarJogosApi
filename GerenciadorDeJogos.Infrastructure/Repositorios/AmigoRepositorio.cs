using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class AmigoRepositorio: RepositorioBase<Amigo> , IAmigoRepositorio
    {
        public AmigoRepositorio(JogosContexto contexto): base(contexto) { }

        public ListaPaginavel<Amigo> PesquisarAmigos(Pesquisa pesquisa)
        {
            IQueryable<Amigo> query;

            query = ListarTodos().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa.Nome))
            {
                query = query.Where(x=>x.Nome.ToLower().Contains(pesquisa.Nome.ToLower()));
            }

          return query.ParaListaPaginavel(pesquisa.IndiceDePagina, pesquisa.RegistrosPorPagina, pesquisa.Ordenacao, x => x.Nome);
        }
    }
}
