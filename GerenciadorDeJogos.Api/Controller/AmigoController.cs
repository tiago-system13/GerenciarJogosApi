using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/[controller]")]
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
        public async Task<ActionResult<List<AmigoResponse>>> PesquisarAmigo([FromQuery] PesquisaResquest pesquisaResquest)
        {
            var amigos = await _amigoServico.PesquisarAsync(pesquisaResquest).ConfigureAwait(false);

            return Ok(amigos);
        }

        [HttpGet]
        [Route("{id:int}/amigo")]
        public async Task<ActionResult<AmigoResponse>> ObterPorId(int id)
        {
            var amigo = await _amigoServico.BuscarPorIdAsync(id).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpGet]
        [Route("{nome}/amigo")]
        public async Task<ActionResult<AmigoResponse>> ObterPorNome(string nome)
        {
            var amigo = await _amigoServico.BuscarPorNome(nome).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpPost]
        public async Task<ActionResult<AmigoResponse>> Adicionar([FromBody]AmigoRequest amigo)
        {

            var AmigoResponse = await _amigoServico.InserirAsync(amigo).ConfigureAwait(false);
            return Ok(AmigoResponse);
        }

        [HttpPut]
        public async Task<ActionResult<AmigoResponse>> Editar([FromBody]AmigoRequest amigo)
        {
            var AmigoResponse = await _amigoServico.AtualizarAsync(amigo).ConfigureAwait(false);
            return Ok(AmigoResponse);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Deletar(int id)
        {
            var result = await (_amigoServico.ExcluirAsync(id)).ConfigureAwait(false);
            return Ok(result);
        }

    }
}