﻿using AutoMapper;
using GerenciadorDeJogos.Application.Exceptions;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using Microsoft.EntityFrameworkCore;
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

        public async Task<EmprestimoResponse> BuscarEmprestimoNaoDevolvidoPorAmigoAsync(int amigoId)
        {
           return await Task.FromResult(_mapper.Map<EmprestimoResponse>(_emprestimoRepositorio.BuscarEmprestimoNaoDevolvidoPorAmigo(amigoId)));
        }

        public async Task<EmprestimoResponse> BuscarEmprestimoNaoDevolvidoPorJogoAsync(int jogoId, int proprietarioId)
        {
            return await Task.FromResult(_mapper.Map<EmprestimoResponse>(_emprestimoRepositorio.BuscarEmprestimoNaoDevolvidoPorJogo(jogoId, proprietarioId)));
        }

        public async Task<ListaPaginavel<EmprestimoResponse>> PesquisarEmprestimosAsync(PesquisaEmprestimoRequest pesquisaResquest)
        {
            IQueryable<Emprestimo> query;

            query = _emprestimoRepositorio.ListarTodos().Include(i => i.ItensEmprestados).ThenInclude(j => j.Jogo).Include(a => a.Amigo);

            if (pesquisaResquest.AmigoId != null)
            {
                query = query.Where(x => x.AmigoId == pesquisaResquest.AmigoId);
            }

            if (pesquisaResquest.JogoId != null)
            {
                query = query.Where(x => x.ItensEmprestados.Any(item => item.JogoId == pesquisaResquest.JogoId));
            }

            if (pesquisaResquest.DataEmprestimo != null)
            {
                query = query.Where(x => x.DataEmprestimo == pesquisaResquest.DataEmprestimo);
            }

            var emprestimosResponse = query.ParaListaPaginavel(pesquisaResquest.IndiceDePagina, pesquisaResquest.RegistrosPorPagina, pesquisaResquest.Ordenacao, x => x.DataEmprestimo);
            return await Task.FromResult(_mapper.Map<ListaPaginavel<EmprestimoResponse>>(emprestimosResponse));
        }

        public async Task<EmprestimoResponse> DevolverAsync(DevolucaoRequest devolucaoRequest)
        { 
            var emprestimoDb = _emprestimoRepositorio.BuscarPorId(devolucaoRequest.Id,e=> e.ItensEmprestados);

            if (emprestimoDb == null)
            {
                throw new NegocioException("Emprestimo não encontrado!");
            }

            EfetivarDevolucao(emprestimoDb.ItensEmprestados, devolucaoRequest.ItensDevolvidos);

            return await Task.FromResult(_mapper.Map<EmprestimoResponse>(_emprestimoRepositorio.Devolver(emprestimoDb)));
        }

        public async Task<EmprestimoResponse> EmprestarAsync(EmprestimoRequest emprestimoRequest)
        {
            var emprestimo = _mapper.Map<Emprestimo>(emprestimoRequest);

            
            emprestimo.DataEmprestimo = DateTime.Now;
            emprestimo.DataPrevistaDeVolucao = CalcularDataPrevistaDevolucao(emprestimo);

            foreach (var item in emprestimo.ItensEmprestados)
            {
                item.Emprestimo = emprestimo;
            }

           return await Task.FromResult(_mapper.Map<EmprestimoResponse>(_emprestimoRepositorio.Inserir(emprestimo)));
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
