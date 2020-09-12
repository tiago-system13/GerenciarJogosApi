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

        Task<EmprestimoResult> DevolverAsync(EmprestimoRequest emprestimoRequest);

        Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorAmigoAsync(Guid amigoId);

        Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorJogoAsync(Guid jogoId, Guid proprietarioId);

        Task<ListaPaginavel<EmprestimoResult>> PesquisarEmprestimosAsync(PesquisaEmprestimoRequest pesquisaResquest);
    }
}
