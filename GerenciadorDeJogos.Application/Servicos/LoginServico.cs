using GerenciadorDeJogos.Application.Interfaces;
using GerenciadorDeJogos.Application.Models.Request;
using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Application.Seguranca.Configuracao;
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

        public async Task<object> BuscarLoginAsync(LoginRequest usuario)
        {
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
                DateTime expirationDate = createDate.AddMinutes(30);

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
                Issuer = "Teste",
                Audience = "Aud",
                SigningCredentials = _signingConfiguracao.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            var token = handler.WriteToken(securityToken);
            
            return token;
        }

        private object ExceptionObject()
        {
            return new
            {
                autenticated = false,
                message = "Failed to autheticate"
            };
        }

        private object SuccessObject(DateTime createDate, DateTime expirationDate, string token)
        {
            return new
            {
                autenticated = true,
                created = createDate.ToString("yyyy-MM-dd HH:mm:ss"),
                expiration = expirationDate.ToString("yyyy-MM-dd HH:mm:ss"),
                accessToken = token,
                message = "OK"
            };
        }
    }
}
