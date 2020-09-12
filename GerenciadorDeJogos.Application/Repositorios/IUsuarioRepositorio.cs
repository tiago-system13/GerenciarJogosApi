using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IUsuarioRepositorio
    {
        Usuario BuscarPorLogin(string login);
    }
}
