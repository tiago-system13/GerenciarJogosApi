using GerenciadorDeJogos.Domain.Entidades;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IUsuarioRepositorio: IRepositorioBase<Usuario>
    {
        Usuario BuscarPorLogin(string login);
    }
}
