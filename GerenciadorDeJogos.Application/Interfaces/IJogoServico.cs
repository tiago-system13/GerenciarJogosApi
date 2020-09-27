using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IJogoServico
    {
        Task<JogoResponse> InserirAsync(JogoRequest amigoRequest);
        Task<JogoResponse> BuscarPorIdAsync(int id);
        Task<JogoResponse> AtualizarAsync(JogoRequest amigoRequest);
        Task<bool> ExcluirAsync(int id);
        Task<ListaPaginavel<JogoResponse>> PesquisarAsync(PesquisaResquest pesquisa);
        Task<List<JogoResponse>> BuscarPorNome(string nome);
    }
}
