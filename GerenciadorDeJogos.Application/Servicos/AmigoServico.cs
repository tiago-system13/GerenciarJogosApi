using AutoMapper;
using GerenciadorDeJogos.Application.Exceptions;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Application.Validations;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<AmigoResponse> AtualizarAsync(AmigoRequest amigoRequest)
        {
            var ExisteAmigo = _amigoRepositorio.Existe(amigoRequest.Id);
            
            if (!ExisteAmigo)
            {
                throw new NegocioException("Amigo Não Encontrado!");
            }

            var amigo = _mapper.Map<Amigo>(amigoRequest);
            ValidarAmigo(amigo);

            return await Task.FromResult(_mapper.Map<AmigoResponse>(_amigoRepositorio.Atualizar(amigo)));
        }

        public async Task<AmigoResponse> BuscarPorIdAsync(int id)
        {
            return await Task.FromResult(_mapper.Map<AmigoResponse>(_amigoRepositorio.BuscarPorId(id)));
        }

        public async Task<List<AmigoResponse>> BuscarPorNome(string nome)
        {
            var amigos = _amigoRepositorio.ListarTodos();

            if (!string.IsNullOrWhiteSpace(nome))
            {
                amigos = amigos.Where(a=> a.Nome.ToLower().Contains(nome.ToLower()));
            }

            return await Task.FromResult(_mapper.Map<List<AmigoResponse>>(amigos));
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var ExisteAmigo = _amigoRepositorio.Existe(id);

            if (!ExisteAmigo)
            {
                throw new NegocioException("Amigo Não Encontrado!");
            }

            _amigoRepositorio.Excluir(id);

            return await Task.FromResult(true);
        }

        public async Task<AmigoResponse> InserirAsync(AmigoRequest amigoRequest)
        {
            var amigo = _mapper.Map<Amigo>(amigoRequest);
            ValidarAmigo(amigo);

            return await Task.FromResult(_mapper.Map<AmigoResponse>(_amigoRepositorio.Inserir(amigo)));
        }

        public async Task<ListaPaginavel<AmigoResponse>> PesquisarAsync(PesquisaResquest pesquisa)
        {
            IQueryable<Amigo> query;

            query = _amigoRepositorio.ListarTodos().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa.Nome))
            {
                query = query.Where(x => x.Nome.ToLower().Contains(pesquisa.Nome.ToLower()));
            }

            var resultadoPesquisa = query.ParaListaPaginavel(pesquisa.IndiceDePagina, pesquisa.RegistrosPorPagina, pesquisa.Ordenacao, x => x.Nome);
           
            return await Task.FromResult(_mapper.Map<ListaPaginavel<AmigoResponse>>(resultadoPesquisa));
        }

        private void ValidarAmigo(Amigo amigo)
        {
            var amigoValidate = new AmigoValidacao();
            new FluentResultAdapter().VerificaErros(amigoValidate.Validate(amigo));
        }
    }
}
