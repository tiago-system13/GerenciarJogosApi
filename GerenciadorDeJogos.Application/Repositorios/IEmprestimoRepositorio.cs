using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IEmprestimoRepositorio : IRepositorioBase<Emprestimo>
    {
        Emprestimo BuscarEmprestimoNaoDevolvidoPorAmigo(int amigoId);

        Emprestimo BuscarEmprestimoNaoDevolvidoPorJogo(int jogoId, int Proprietario);

        ListaPaginavel<Emprestimo> PesquisarEmprestimos(PesquisaEmprestimo pesquisaEmprestimo);
    }
}
