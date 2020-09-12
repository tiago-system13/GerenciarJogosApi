using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IJogoServico
    {
        Task<JogoRequest> InserirAsync(JogoRequest amigoRequest);
        Task<JogoRequest> BuscarPorIdAsync(Guid id);
        Task<JogoRequest> AtualizarAsync(JogoRequest amigoRequest);
        Task<bool> ExcluirAsync(Guid id);
        Task<ListaPaginavel<JogoRequest>> PesquisarAsync(PesquisaResquest pesquisa);
    }
}
