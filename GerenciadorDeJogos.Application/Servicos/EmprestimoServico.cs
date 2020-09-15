using AutoMapper;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Servicos
{
    public class EmprestimoServico : IEmprestimoServico
    {
        private readonly IEmprestimoRepositorio _emprestimoRepositorio;
        private readonly IMapper _mapper;

        public EmprestimoServico(IEmprestimoRepositorio emprestimoRepositorio, IMapper mapper)
        {
            _emprestimoRepositorio = emprestimoRepositorio;
            _mapper = mapper;
        }

        public async Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorAmigoAsync(int amigoId)
        {
           return await Task.FromResult(_mapper.Map<EmprestimoResult>(_emprestimoRepositorio.BuscarEmprestimoNaoDevolvidoPorAmigo(amigoId)));
        }

        public async Task<EmprestimoResult> BuscarEmprestimoNaoDevolvidoPorJogoAsync(int jogoId, int proprietarioId)
        {
            return await Task.FromResult(_mapper.Map<EmprestimoResult>(_emprestimoRepositorio.BuscarEmprestimoNaoDevolvidoPorJogo(jogoId, proprietarioId)));
        }

        public async Task<ListaPaginavel<EmprestimoResult>> PesquisarEmprestimosAsync(PesquisaEmprestimoRequest pesquisaResquest)
        {
            return await Task.FromResult(_mapper.Map<ListaPaginavel<EmprestimoResult>>(_emprestimoRepositorio.PesquisarEmprestimos(_mapper.Map<PesquisaEmprestimo>(pesquisaResquest))));
        }

        public async Task<EmprestimoResult> DevolverAsync(DevolucaoRequest devolucaoRequest)
        { 
            var emprestimoDb = _emprestimoRepositorio.BuscarPorId(devolucaoRequest.Id);

            if (emprestimoDb == null)
            {
                throw new ArgumentException("Emprestimo não encontrado!");
            }

            EfetivarDevolucao(emprestimoDb.ItensEmprestados, devolucaoRequest.ItensDevolvidos);

            return await Task.FromResult(_mapper.Map<EmprestimoResult>(_emprestimoRepositorio.Atualizar(emprestimoDb)));
        }

        public async Task<EmprestimoResult> EmprestarAsync(EmprestimoRequest emprestimoRequest)
        {
            var emprestimo = _mapper.Map<Emprestimo>(emprestimoRequest);

            
            emprestimo.DataEmprestimo = DateTime.Now;
            emprestimo.DataPrevistaDeVolucao = CalcularDataPrevistaDevolucao(emprestimo);

            foreach (var item in emprestimo.ItensEmprestados)
            {
                item.Emprestimo = emprestimo;
            }

           return await Task.FromResult(_mapper.Map<EmprestimoResult>(_emprestimoRepositorio.Inserir(emprestimo)));
        }

        private DateTime CalcularDataPrevistaDevolucao(Emprestimo emprestimo)
        {
            return emprestimo.DataEmprestimo.AddDays(emprestimo.QuantidadeDeDias);
        }

        private void EfetivarDevolucao(List<ItensEmprestados> itensEmprestados, List<ItensDevolvidosRequest> itensDevolvido)
        {
            foreach (var itemDevolvido in itensDevolvido.Where(j => j.Devolvido == true))
            {
                foreach (var item in itensEmprestados.Where(i => i.EmprestimoId == itemDevolvido.EmprestimoId && i.JogoId == itemDevolvido.JogoId && i.DataDevolucao == null))
                {
                    item.DataDevolucao = DateTime.Now;
                    item.Devolvido = itemDevolvido.Devolvido;
                }
            }
        }
    }
}
