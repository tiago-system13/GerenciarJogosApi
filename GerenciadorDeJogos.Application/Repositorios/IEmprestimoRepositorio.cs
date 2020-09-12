using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IEmprestimoRepositorio : IRepositorioBase<Emprestimo>
    {
        Emprestimo BuscarEmprestimoNaoDevolvidoPorAmigo(Guid amigoId);

        Emprestimo BuscarEmprestimoNaoDevolvidoPorJogo(Guid jogoId, Guid Proprietario);

        ListaPaginavel<Emprestimo> PesquisarEmprestimos(PesquisaEmprestimo pesquisaEmprestimo);
    }
}
