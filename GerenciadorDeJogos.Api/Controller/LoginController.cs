using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Api.Controller
{
    [Route("api/login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILoginServico _loginServico;

        public LoginController(ILoginServico loginServico)
        {
            _loginServico = loginServico;
        }

        [AllowAnonymous]
        [HttpPost]
        public Task<AutenticacaoResult> Post([FromBody]LoginRequest user)
        {
            return _loginServico.BuscarLoginAsync(user);
            
        }
    }
}