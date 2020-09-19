using System.Collections.Generic;
using System.Threading.Tasks;
using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/emprestimo")]
    [ApiController]
    public class EmprestimoController : ControllerBase
    {
        private readonly IEmprestimoServico _emprestimoServico;

        public EmprestimoController(IEmprestimoServico emprestimoServico)
        {
            _emprestimoServico = emprestimoServico;
        }

        [HttpPost]
        public async Task<ActionResult<EmprestimoResponse>> Emprestar([FromBody]EmprestimoRequest emprestimo)
        {

            var EmprestimoResponse = await _emprestimoServico.EmprestarAsync(emprestimo).ConfigureAwait(false);
            return Ok(EmprestimoResponse);
        }

        [HttpPut]
        public async Task<ActionResult<EmprestimoResponse>> Devolver([FromBody]DevolucaoRequest devolucao)
        {
            var amigoResult = await _emprestimoServico.DevolverAsync(devolucao).ConfigureAwait(false);
            return Ok(amigoResult);
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<List<EmprestimoResponse>>> Pesquisar([FromQuery] PesquisaEmprestimoRequest pesquisaResquest)
        {
            var amigos = await _emprestimoServico.PesquisarEmprestimosAsync(pesquisaResquest).ConfigureAwait(false);

            return Ok(amigos);
        }

        [HttpGet]
        [Route("{id:int}/amigo/emprestimo")]
        public async Task<ActionResult<EmprestimoResponse>> BuscarEmprestimoNaoDevolvidoPorAmigo(int id)
        {
            var amigo = await _emprestimoServico.BuscarEmprestimoNaoDevolvidoPorAmigoAsync(id).ConfigureAwait(false);
            return Ok(amigo);
        }

        [HttpGet]
        [Route("{jogoId:int}/jogo/{amigoId:int}/amigo/emprestimo")]
        public async Task<ActionResult<EmprestimoResponse>> BuscarEmprestimoNaoDevolvido(int jogoId, int amigoId)
        {
            var amigo = await _emprestimoServico.BuscarEmprestimoNaoDevolvidoPorJogoAsync(jogoId, amigoId).ConfigureAwait(false);
            return Ok(amigo);
        }

    }
}