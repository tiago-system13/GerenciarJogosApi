using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class EmprestimoRepositorio : RepositorioBase<Emprestimo>, IEmprestimoRepositorio
    {

        public EmprestimoRepositorio(JogosContexto contexto) : base(contexto) { }

        private IQueryable<Emprestimo> Query()
        {
            return ListarTodos().Include(i => i.ItensEmprestados).ThenInclude(j => j.Jogo).ThenInclude(a=>a.Proprietario).Include(a=> a.Amigo);
            
        }

        public Emprestimo BuscarEmprestimoNaoDevolvidoPorAmigo(int amigoId)
        {
            return Query().FirstOrDefault(e => e.AmigoId == amigoId && e.ItensEmprestados.Any(item => (item.Devolvido == null || item.Devolvido == false)));
        }

        public Emprestimo BuscarEmprestimoNaoDevolvidoPorJogo(int jogoId, int proprietarioId)
        {
            return Query().FirstOrDefault(e => e.ItensEmprestados.Any(item=> item.JogoId == jogoId && item.Jogo.ProprietarioId == proprietarioId && (item.Devolvido == null || item.Devolvido == false)));
        }

        public Emprestimo Devolver(Emprestimo emprestimo)
        {
           _context.ItensEmprestados.UpdateRange(emprestimo.ItensEmprestados);
            Salvar();

            return emprestimo;
        }
    }
}
