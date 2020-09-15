using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IJogoServico
    {
        Task<JogoResult> InserirAsync(JogoRequest amigoRequest);
        Task<JogoResult> BuscarPorIdAsync(int id);
        Task<JogoResult> AtualizarAsync(JogoRequest amigoRequest);
        Task<bool> ExcluirAsync(int id);
        Task<ListaPaginavel<JogoResult>> PesquisarAsync(PesquisaResquest pesquisa);
        Task<List<JogoResult>> BuscarPorNome(string nome);
    }
}
