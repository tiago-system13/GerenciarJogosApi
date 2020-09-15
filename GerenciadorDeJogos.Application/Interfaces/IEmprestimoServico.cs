using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IEmprestimoServico
    {
        Task<EmprestimoResult> EmprestarAsync(EmprestimoRequest emprestimoRequest);

        Task<EmprestimoResult> DevolverAsync(DevolucaoRequest devolucaoRequest);

        Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorAmigoAsync(int amigoId);

        Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorJogoAsync(int jogoId, int proprietarioId);

        Task<ListaPaginavel<EmprestimoResult>> PesquisarEmprestimosAsync(PesquisaEmprestimoRequest pesquisaResquest);
    }
}
