using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Contexto;
using GerenciadorDeJogos.Infrastructure.Repositorios.Base;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : RepositorioBase<Usuario>, IUsuarioRepositorio
    {

        public UsuarioRepositorio(JogosContexto context):base(context)
        {
           
        }

        public Usuario BuscarPorLogin(string login)
        {
            return _context.Usuarios.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}
