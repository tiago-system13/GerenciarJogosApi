using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioServico _usuarioServico;

        public UsuarioController(IUsuarioServico usuarioServico)
        {
            _usuarioServico = usuarioServico;
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<UsuarioResult>>> Pesquisar([FromQuery] PesquisaResquest pesquisaResquest)
        {
            var usuario = await _usuarioServico.PesquisarAsync(pesquisaResquest).ConfigureAwait(false);

            return Ok(usuario);
        }

        [HttpGet]
        [Route("{id:int}/amigo")]
        public async Task<ActionResult<UsuarioResult>> ObterPorId(int id)
        {
            var usuario = await _usuarioServico.BuscarPorIdAsync(id).ConfigureAwait(false);
            return Ok(usuario);
        }

        [HttpPost]
        public async Task<ActionResult<UsuarioResult>> Adicionar([FromBody]UsuarioRequest usuario)
        {

            var amigoResult = await _usuarioServico.InserirAsync(usuario).ConfigureAwait(false);
            return Ok(amigoResult);
        }

        [HttpPut]
        public async Task<ActionResult<UsuarioResult>> Editar([FromBody]UsuarioRequest usuario)
        {
            var usuarioResult = await _usuarioServico.AtualizarAsync(usuario).ConfigureAwait(false);
            return Ok(usuarioResult);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> Deletar(int id)
        {
            var result = await (_usuarioServico.ExcluirAsync(id)).ConfigureAwait(false);
            return Ok(result);
        }
    }
}