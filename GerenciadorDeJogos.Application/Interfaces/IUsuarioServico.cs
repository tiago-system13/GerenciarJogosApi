using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IUsuarioServico
    {
        Task<UsuarioResponse> InserirAsync(UsuarioRequest usuarioRequest);
        Task<UsuarioResponse> BuscarPorIdAsync(int id);
        Task<UsuarioResponse> AtualizarAsync(UsuarioRequest usuarioRequest);
        Task<bool> ExcluirAsync(int id);
        Task<ListaPaginavel<UsuarioResponse>> PesquisarAsync(PesquisaResquest pesquisa);
    }
}
