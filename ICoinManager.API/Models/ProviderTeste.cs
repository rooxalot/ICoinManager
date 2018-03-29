using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin.Security.OAuth;
using System.Threading.Tasks;
using System.Security.Claims;

namespace ICoinManager.API.Models
{
    public class ProviderTeste : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuarioEnviado = context.UserName;
            var senhaEnviada = context.Password;

            var usuario =  BaseUsuarios.Usuarios()
                .FirstOrDefault(x => x.Nome.Trim() == usuarioEnviado && x.Senha == senhaEnviada);

            if (usuario == null)
            {
                context.SetError("invalid_grant", "Usuário não encontrado ou senha incorreta.");
                return;
            }

            var identidadeUsuario = new ClaimsIdentity(context.Options.AuthenticationType);
            context.Validated(identidadeUsuario);
        }


        internal static class BaseUsuarios
        {
            public static IEnumerable<Usuario> Usuarios()
            {
                return new List<Usuario>
                {
                    new Usuario { Nome = "Fulano", Senha = "1234" },
                    new Usuario { Nome = "Beltrano", Senha = "5678" },
                    new Usuario { Nome = "Sicrano", Senha = "0912" }
                };
            }
        }
    }
}