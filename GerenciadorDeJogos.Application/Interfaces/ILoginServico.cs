using GerenciadorDeJogos.Application.Models.Request;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface ILoginServico
    {
        Task<object> BuscarLoginAsync(LoginRequest usuario);
        
    }
}
