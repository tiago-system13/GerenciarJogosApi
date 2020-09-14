using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/amigo")]
    [ApiController]
    public class AmigoController : ControllerBase
    {
        private readonly IAmigoServico _amigoServico;

        public AmigoController(IAmigoServico amigoServico)
        {
            _amigoServico = amigoServico;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<AmigoResult>>> PesquisarAmigo([FromQuery] PesquisaResquest pesquisaResquest)
        {
            var amigos = await _amigoServico.PesquisarAsync(pesquisaResquest).ConfigureAwait(false);

            return Ok(amigos);
        }

        [HttpGet]
        [Route("{id:Guid}/amigo")]
        public async Task<ActionResult<AmigoResult>> ObterPorId(Guid id)
        {
            var amigo = await _amigoServico.BuscarPorIdAsync(id).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpGet]
        [Route("{nome}/amigo")]
        public async Task<ActionResult<JogoResult>> ObterPorNome(string nome)
        {
            var amigo = await _amigoServico.BuscarPorNome(nome).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpPost]
        public async Task<ActionResult<AmigoResult>> Adicionar([FromBody]AmigoRequest amigo)
        {

            var amigoResult = await _amigoServico.InserirAsync(amigo).ConfigureAwait(false);
            return Ok(amigoResult);
        }

        [HttpPut]
        public async Task<ActionResult<AmigoResult>> Editar([FromBody]AmigoRequest amigo)
        {
            var amigoResult = await _amigoServico.AtualizarAsync(amigo).ConfigureAwait(false);
            return Ok(amigoResult);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Deletar(Guid id)
        {
            var result = await (_amigoServico.ExcluirAsync(id)).ConfigureAwait(false);
            return Ok(result);
        }

    }
}