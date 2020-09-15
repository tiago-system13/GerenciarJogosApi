using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IUsuarioServico
    {
        Task<UsuarioResult> InserirAsync(UsuarioRequest usuarioRequest);
        Task<UsuarioResult> BuscarPorIdAsync(int id);
        Task<UsuarioResult> AtualizarAsync(UsuarioRequest usuarioRequest);
        Task<bool> ExcluirAsync(int id);
        Task<ListaPaginavel<UsuarioResult>> PesquisarAsync(PesquisaResquest pesquisa);
    }
}
