using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IAmigoServico
    {
        Task<AmigoResponse> InserirAsync(AmigoRequest amigoRequest);
        Task<AmigoResponse> BuscarPorIdAsync(int id);
        Task<AmigoResponse> AtualizarAsync(AmigoRequest amigoRequest);
        Task<bool> ExcluirAsync(int id);
        Task<List<AmigoResponse>> BuscarPorNome(string nome);
        Task<ListaPaginavel<AmigoResponse>> PesquisarAsync(PesquisaResquest pesquisa);
    }
}
