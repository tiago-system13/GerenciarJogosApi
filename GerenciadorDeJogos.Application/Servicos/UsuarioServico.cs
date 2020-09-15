using AutoMapper;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Servicos
{
    public class UsuarioServico : IUsuarioServico
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IMapper _mapper;

        public UsuarioServico(IUsuarioRepositorio usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }
        public async Task<UsuarioResult> AtualizarAsync(UsuarioRequest usuarioRequest)
        {
            var ExisteUsuario = _usuarioRepositorio.Existe(usuarioRequest.Id);

            if (!ExisteUsuario)
            {
                throw new ArgumentException("Usuário Não Encontrado!");
            }

            var usuario = _mapper.Map<Usuario>(usuarioRequest);

            return await Task.FromResult(_mapper.Map<UsuarioResult>(_usuarioRepositorio.Atualizar(usuario)));
        }

        public async Task<UsuarioResult> BuscarPorIdAsync(int id)
        {
            return await Task.FromResult(_mapper.Map<UsuarioResult>(_usuarioRepositorio.BuscarPorId(id)));
        }

        public async Task<bool> ExcluirAsync(int id)
        {
            var ExisteUsuario = _usuarioRepositorio.Existe(id);

            if (!ExisteUsuario)
            {
                throw new ArgumentException("Usuário Não Encontrado!");
            }

            _usuarioRepositorio.Excluir(id);

            return await Task.FromResult(true);
        }

        public async Task<UsuarioResult> InserirAsync(UsuarioRequest usuarioRequest)
        {
            var usuario = _mapper.Map<Usuario>(usuarioRequest);

            return await Task.FromResult(_mapper.Map<UsuarioResult>(_usuarioRepositorio.Inserir(usuario)));
        }

        public async Task<ListaPaginavel<UsuarioResult>> PesquisarAsync(PesquisaResquest pesquisa)
        {
            IQueryable<Usuario> query;

            query = _usuarioRepositorio.ListarTodos().AsQueryable();

            if (!string.IsNullOrWhiteSpace(pesquisa.Nome))
            {
                query = query.Where(x => x.Nome.ToLower().Contains(pesquisa.Nome.ToLower()));
            }

            var resultadoPesquisa = query.ParaListaPaginavel(pesquisa.IndiceDePagina, pesquisa.RegistrosPorPagina, pesquisa.Ordenacao, x => x.Nome);

            return await Task.FromResult(_mapper.Map<ListaPaginavel<UsuarioResult>>(resultadoPesquisa));
        }
    }
}
