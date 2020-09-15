using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/jogo")]
    [ApiController]
    public class JogoController : ControllerBase
    {
        private readonly IJogoServico _jogoServico;

        public JogoController(IJogoServico jogoServico)
        {
            _jogoServico = jogoServico;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<JogoResult>>> Pesquisar([FromQuery] PesquisaResquest pesquisaResquest)
        {
            var amigos = await _jogoServico.PesquisarAsync(pesquisaResquest).ConfigureAwait(false);

            return Ok(amigos);
        }

        [HttpGet]
        [Route("{id:int}/jogo")]
        public async Task<ActionResult<JogoResult>> ObterPorId(int id)
        {
            var amigo = await _jogoServico.BuscarPorIdAsync(id).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpGet]
        [Route("{nome}/jogo")]
        public async Task<ActionResult<JogoResult>> ObterPorNome(string nome)
        {
            var amigo = await _jogoServico.BuscarPorNome(nome).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpPost]
        public async Task<ActionResult<JogoResult>> Adicionar([FromBody]JogoRequest jogo)
        {

            var jogoResult = await _jogoServico.InserirAsync(jogo).ConfigureAwait(false);
            return Ok(jogoResult);
        }

        [HttpPut]
        public async Task<ActionResult<JogoResult>> Editar([FromBody]JogoRequest jogo)
        {
            var jogoResult = await _jogoServico.AtualizarAsync(jogo).ConfigureAwait(false);
            return Ok(jogoResult);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Deletar(int id)
        {
            var result = await (_jogoServico.ExcluirAsync(id)).ConfigureAwait(false);
            return Ok(result);
        }
    }
}