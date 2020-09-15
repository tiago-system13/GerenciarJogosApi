using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
        public object Post([FromBody]LoginRequest user)
        {
            if (user == null) return BadRequest();
            return _loginServico.BuscarLoginAsync(user).ConfigureAwait(false); 
        }
    }
}