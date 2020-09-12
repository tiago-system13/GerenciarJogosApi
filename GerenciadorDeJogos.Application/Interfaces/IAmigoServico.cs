﻿using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Domain.Entidades.Base;
using System;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Interfaces
{
    public interface IAmigoServico
    {
        Task<AmigoResult> InserirAsync(AmigoRequest amigoRequest);
        Task<AmigoResult> BuscarPorIdAsync(Guid id);
        Task<AmigoResult> AtualizarAsync(AmigoRequest amigoRequest);
        Task<bool> ExcluirAsync(Guid id);
        Task<ListaPaginavel<AmigoResult>> PesquisarAsync(PesquisaResquest pesquisa);
    }
}
