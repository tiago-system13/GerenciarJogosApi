using GerenciadorDeJogos.Domain.Entidades;
using System.Collections.Generic;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IEmprestimoRepositorio : IRepositorioBase<Emprestimo>
    {
        Emprestimo BuscarEmprestimoNaoDevolvidoPorAmigo(int amigoId);

        Emprestimo BuscarEmprestimoNaoDevolvidoPorJogo(int jogoId, int Proprietario);

        Emprestimo Devolver(Emprestimo emprestimo);
        
    }
}
