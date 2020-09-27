using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IEmprestimoServico
    {
        Task<EmprestimoResponse> EmprestarAsync(EmprestimoRequest emprestimoRequest);

        Task<EmprestimoResponse> DevolverAsync(DevolucaoRequest devolucaoRequest);

        Task<EmprestimoResponse> BuscarEmprestimoNaoDevolvidoPorAmigoAsync(int amigoId);

        Task<EmprestimoResponse> BuscarEmprestimoNaoDevolvidoPorJogoAsync(int jogoId, int proprietarioId);

        Task<ListaPaginavel<EmprestimoResponse>> PesquisarEmprestimosAsync(PesquisaEmprestimoRequest pesquisaResquest);
    }
}
