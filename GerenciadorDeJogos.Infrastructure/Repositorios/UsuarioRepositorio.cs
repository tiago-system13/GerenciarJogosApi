using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades;
using GerenciadorDeJogos.Infrastructure.Contexto;
using System.Linq;

namespace GerenciadorDeJogos.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly JogosContexto _context;

        public UsuarioRepositorio(JogosContexto context)
        {
            _context = context;
        }

        public Usuario BuscarPorLogin(string login)
        {
            return _context.Usuarios.SingleOrDefault(u => u.Login.Equals(login));
        }
    }
}
