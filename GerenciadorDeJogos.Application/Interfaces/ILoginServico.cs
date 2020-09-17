using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface ILoginServico
    {
        Task<AutenticacaoResult> BuscarLoginAsync(LoginRequest usuario);
        
    }
}
