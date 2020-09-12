using AutoMapper;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
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

        public async Task<JogoRequest> AtualizarAsync(JogoRequest jogoRequest)
        {
            var ExisteJogo = _jogoRepositorio.Existe(jogoRequest.Id);

            if (!ExisteJogo)
            {
                throw new ArgumentException("Jogo Não Encontrado!");
            }

            var jogo = _mapper.Map<Jogo>(jogoRequest);

            return await Task.FromResult(_mapper.Map<JogoRequest>(_jogoRepositorio.Atualizar(jogo)));
        }

        public async Task<JogoRequest> BuscarPorIdAsync(Guid id)
        {
            return await Task.FromResult(_mapper.Map<JogoRequest>(_jogoRepositorio.BuscarPorId(id)));
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

        public async Task<JogoRequest> InserirAsync(JogoRequest jogoRequest)
        {
            var jogo = _mapper.Map<Jogo>(jogoRequest);

            return await Task.FromResult(_mapper.Map<JogoRequest>(_jogoRepositorio.Inserir(jogo)));
        }

        public async Task<ListaPaginavel<JogoRequest>> PesquisarAsync(PesquisaResquest pesquisa)
        {
            var resultadoPesquisa = _jogoRepositorio.PesquisarJogos(_mapper.Map<Pesquisa>(pesquisa));

            return await Task.FromResult(_mapper.Map<ListaPaginavel<JogoRequest>>(resultadoPesquisa));
        }
    }
}
