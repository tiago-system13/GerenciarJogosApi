using GerenciadorDeJogos.Domain.Entidades.Base;
using GerenciadorDeJogos.Domain.Enum;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorDeJogos.Application.Repositorios
{
    public interface IRepositorioBase<T> where T : Entidade
    {
        T Inserir(T item);
        T BuscarPorId(int id, params Expression<Func<T, object>>[] incluindoPropriedades);
        IQueryable<T> ListarTodos();
        T Atualizar(T item);
        void Excluir(int id);
        bool Existe(int id);
        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T,IKey>> chaveSeletora, TipoDeOrdenacao orderBy);

        ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T,IKey>> chaveSeletora,
            Expression<Func<T, bool>> predicado, TipoDeOrdenacao orderBy, params Expression<Func<T, object>>[] incluindoPropriedades);
        IQueryable<T> ListarPor(Expression<Func<T, bool>> predicado);
        void Salvar();
        IQueryable<T> TodosIncluindo(params Expression<Func<T, object>>[] includeProperties);
    }
}
