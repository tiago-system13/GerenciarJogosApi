using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Models.Result;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Application.Seguranca.Configuracao;
using GerenciadorDeJogos.Application.Validations;
using GerenciadorDeJogos.Domain.Constantes;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace GerenciadorDeJogos.Application.Servicos
{
    public class LoginServico : ILoginServico
    {
        private IUsuarioRepositorio _repositorio;
        private SigningConfiguracao _signingConfiguracao;
        private TokenConfiguracao _tokenConfiguracao;

        public LoginServico(IUsuarioRepositorio repositorio, SigningConfiguracao signingConfiguracao, TokenConfiguracao tokenConfiguracao)
        {
            _repositorio = repositorio;
            _signingConfiguracao = signingConfiguracao;
            _tokenConfiguracao = tokenConfiguracao;
        }

        public async Task<AutenticacaoResult> BuscarLoginAsync(LoginRequest usuario)
        {
            ValidarLogin(usuario);

            bool credentialsIsValid = false;
            if (usuario != null && !string.IsNullOrWhiteSpace(usuario.Login))
            {
                var baseUser = _repositorio.BuscarPorLogin(usuario.Login);

                credentialsIsValid = (baseUser != null && usuario.Login.Equals(baseUser.Login) && usuario.Senha.Equals(baseUser.Senha));
            }

            if (credentialsIsValid)
            {
                ClaimsIdentity identity = new ClaimsIdentity(
                    new GenericIdentity(usuario.Login, "Login"),
                        new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                            new Claim(JwtRegisteredClaimNames.UniqueName, usuario.Login)
                        }
                    );

                DateTime createDate = DateTime.Now;
                DateTime expirationDate = createDate.AddMinutes(Convert.ToDouble(EnvConstants.TempoExpiracaoToken));

                var handler = new JwtSecurityTokenHandler();
                string token = CreateToken(identity, createDate, expirationDate, handler);

                return await Task.FromResult(SuccessObject(createDate, expirationDate, token));
            }
            else
            {
                return await Task.FromResult(ExceptionObject());
            }
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new Microsoft.IdentityModel.Tokens.SecurityTokenDescriptor
            {
                Issuer = EnvConstants.ISSUER,
                Audience = EnvConstants.Audience,
                SigningCredentials = _signingConfiguracao.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            
            return token;
        }

        private AutenticacaoResult ExceptionObject()
        {
            return new AutenticacaoResult
            {
                Autenticated = false,
                Message = "Failed to autheticate"
            };
        }

        private AutenticacaoResult SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new AutenticacaoResult
            {
                Autenticated = true,
                Created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }

        private void ValidarLogin(LoginRequest login)
        {
            var loginValidate = new LoginValidation();
            new FluentResultAdapter().VerificaErros(loginValidate.Validate(login));
        }
    }
}
