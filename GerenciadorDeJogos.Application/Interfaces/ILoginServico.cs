using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface ILoginServico
    {
        Task<AutenticacaoResponse> BuscarLoginAsync(LoginRequest usuario);
        
    }
}
