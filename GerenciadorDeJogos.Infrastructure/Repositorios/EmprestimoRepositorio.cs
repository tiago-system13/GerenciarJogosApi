using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class EmprestimoRepositorio : RepositorioBase<Emprestimo>, IEmprestimoRepositorio
    {

        public EmprestimoRepositorio(JogosContexto contexto) : base(contexto) { }

        private IQueryable<Emprestimo> Query()
        {
            return ListarTodos().Include(i => i.ItensEmprestados).ThenInclude(j => j.Jogo);
        }

        public Emprestimo BuscarEmprestimoNaoDevolvidoPorAmigo(int amigoId)
        {
            return Query().FirstOrDefault(e => e.AmigoId == amigoId && e.ItensEmprestados.Any(item => (item.Devolvido == null || item.Devolvido == false)));
        }

        public Emprestimo BuscarEmprestimoNaoDevolvidoPorJogo(int jogoId, int proprietarioId)
        {
            return Query().FirstOrDefault(e => e.ItensEmprestados.Any(item=> item.JogoId == jogoId && item.Jogo.ProprietarioId == proprietarioId && (item.Devolvido == null || item.Devolvido == false)));
        }

        public ListaPaginavel<Emprestimo> PesquisarEmprestimos(PesquisaEmprestimo pesquisaEmprestimo)
        {
            IQueryable<Emprestimo> query;

            query = Query().Include(a=> a.Amigo);

            if (pesquisaEmprestimo.AmigoId != null)
            {
                query = query.Where(x => x.AmigoId == pesquisaEmprestimo.AmigoId);
            }

            if (pesquisaEmprestimo.JogoId != null)
            {
                query = query.Where(x => x.ItensEmprestados.Any(item => item.JogoId == pesquisaEmprestimo.JogoId));
            }

            if (pesquisaEmprestimo.DataEmprestimo != null)
            {
                query = query.Where(x => x.DataEmprestimo == pesquisaEmprestimo.DataEmprestimo);
            }

            return query.ParaListaPaginavel(pesquisaEmprestimo.IndiceDePagina, pesquisaEmprestimo.RegistrosPorPagina, pesquisaEmprestimo.Ordenacao, x => x.DataEmprestimo);
        }
    }
}
