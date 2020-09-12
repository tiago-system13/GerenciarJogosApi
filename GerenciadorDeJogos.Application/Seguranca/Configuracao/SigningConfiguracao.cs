using System.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace GerenciadorDeJogos.Application.Seguranca.Configuracao
{
    public class SigningConfiguracao
    {
        public SecurityKey Key { get; }
        public SigningCredentials SigningCredentials { get; }

        public SigningConfiguracao()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);
        }
    }
}
