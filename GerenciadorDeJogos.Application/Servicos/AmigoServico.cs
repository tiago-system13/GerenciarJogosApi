using AutoMapper;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Servicos
{
    public class AmigoServico : IAmigoServico
    {
        private readonly IAmigoRepositorio _amigoRepositorio;
        private readonly IMapper _mapper;

        public AmigoServico(IAmigoRepositorio amigoRepositorio, IMapper mapper)
        {
            _amigoRepositorio = amigoRepositorio;
            _mapper = mapper;
        }

        public async Task<AmigoResult> AtualizarAsync(AmigoRequest amigoRequest)
        {
            var ExisteAmigo = _amigoRepositorio.Existe(amigoRequest.Id);
            
            if (!ExisteAmigo)
            {
                throw new ArgumentException("Amigo Não Encontrado!");
            }

            var amigo = _mapper.Map<Amigo>(amigoRequest);

            return await Task.FromResult(_mapper.Map<AmigoResult>(_amigoRepositorio.Atualizar(amigo)));
        }

        public async Task<AmigoResult> BuscarPorIdAsync(Guid id)
        {
            return await Task.FromResult(_mapper.Map<AmigoResult>(_amigoRepositorio.BuscarPorId(id)));
        }

        public async Task<bool> ExcluirAsync(Guid id)
        {
            var ExisteAmigo = _amigoRepositorio.Existe(id);

            if (!ExisteAmigo)
            {
                throw new ArgumentException("Amigo Não Encontrado!");
            }

            _amigoRepositorio.Excluir(id);

            return await Task.FromResult(true);
        }

        public async Task<AmigoResult> InserirAsync(AmigoRequest amigoRequest)
        {
            var amigo = _mapper.Map<Amigo>(amigoRequest);

            return await Task.FromResult(_mapper.Map<AmigoResult>(_amigoRepositorio.Inserir(amigo)));
        }

        public async Task<ListaPaginavel<AmigoResult>> PesquisarAsync(PesquisaResquest pesquisa)
        {
            var resultadoPesquisa =_amigoRepositorio.PesquisarAmigos(_mapper.Map<Pesquisa>(pesquisa));

            return await Task.FromResult(_mapper.Map<ListaPaginavel<AmigoResult>>(resultadoPesquisa));
        }
    }
}
