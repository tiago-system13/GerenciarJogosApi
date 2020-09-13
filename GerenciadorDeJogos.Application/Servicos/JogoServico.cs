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
    public class JogoServico : IJogoServico
    {
        private readonly IJogoRepositorio _jogoRepositorio;
        private readonly IMapper _mapper;

        public JogoServico(IJogoRepositorio jogoRepositorio, IMapper mapper)
        {
            _jogoRepositorio = jogoRepositorio;
            _mapper = mapper;
        }

        public async Task<JogoResult> AtualizarAsync(JogoRequest jogoRequest)
        {
            var ExisteJogo = _jogoRepositorio.Existe(jogoRequest.Id);

            if (!ExisteJogo)
            {
                throw new ArgumentException("Jogo Não Encontrado!");
            }

            var jogo = _mapper.Map<Jogo>(jogoRequest);

            return await Task.FromResult(_mapper.Map<JogoResult>(_jogoRepositorio.Atualizar(jogo)));
        }

        public async Task<JogoResult> BuscarPorIdAsync(Guid id)
        {
            return await Task.FromResult(_mapper.Map<JogoResult>(_jogoRepositorio.BuscarPorId(id)));
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            var ExisteJogo = _jogoRepositorio.Existe(id);

            if (!ExisteJogo)
            {
                throw new ArgumentException("Jogo Não Encontrado!");
            }

            _jogoRepositorio.Excluir(id);

            return await Task.FromResult(true);
        }

        public async Task<JogoResult> InserirAsync(JogoRequest jogoRequest)
        {
            var jogo = _mapper.Map<Jogo>(jogoRequest);

            return await Task.FromResult(_mapper.Map<JogoResult>(_jogoRepositorio.Inserir(jogo)));
        }

        public async Task<ListaPaginavel<JogoResult>> PesquisarAsync(PesquisaResquest pesquisa)
        {
            IQueryable<Jogo> query;

            query = _jogoRepositorio.TodosIncluindo(x => x.Proprietario).AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa.Nome))
            {
                query = query.Where(x => x.Nome.ToLower().Contains(pesquisa.Nome.ToLower()));
            }

            var resultadoPesquisa = query.ParaListaPaginavel(pesquisa.IndiceDePagina, pesquisa.RegistrosPorPagina, pesquisa.Ordenacao, x => x.Nome);

            return await Task.FromResult(_mapper.Map<ListaPaginavel<JogoResult>>(resultadoPesquisa));
        }

        public async Task<List<JogoResult>> BuscarPorNome(string nome)
        {
            var jogos = _jogoRepositorio.ListarTodos();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                jogos = jogos.Where(a => a.Nome.ToLower().Contains(nome.ToLower()));
            }

            return await Task.FromResult(_mapper.Map<List<JogoResult>>(jogos));
        }
    }
}
