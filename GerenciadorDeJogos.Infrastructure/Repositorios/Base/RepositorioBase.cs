using GerenciadorDeJogos.Application.Repositorios;
using GerenciadorDeJogos.Domain.Entidades.Base;
using GerenciadorDeJogos.Domain.Enum;
using GerenciadorDeJogos.Infrastructure.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorDeJogos.Infrastructure.Repositorios.Base
{
    public class RepositorioBase<T> : IRepositorioBase<T> where T : Entidade
    {
        protected readonly JogosContexto _context;

        // Declaração de um dataset genérico
        private DbSet<T> dataset;

        public RepositorioBase(JogosContexto context)
        {
            _context = context;
            dataset = _context.Set<T>();
        }


        public T Inserir(T item)
        {
            try
            {
                dataset.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Excluir(int id)
        {
            var result = dataset.SingleOrDefault(i => i.Id.Equals(id));
            try
            {
                if (result != null)
                {
                    dataset.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Existe(int id)
        {
            return dataset.Any(b => b.Id.Equals(id));
        }

        public IQueryable<T> ListarTodos()
        {
            return dataset.AsNoTracking();
        }

        public T BuscarPorId(int id, params Expression<Func<T, object>>[] incluindoPropriedades)
        {           
            return AdicionarInclude(dataset.AsNoTracking(), incluindoPropriedades).SingleOrDefault(p => p.Id.Equals(id));           
        }

        private IQueryable<T> AdicionarInclude(IQueryable<T> query,params Expression<Func<T, object>>[] incluindoPropriedades)
        {
            if (incluindoPropriedades != null)
            {
                foreach (var item in incluindoPropriedades)
                {
                    query = query.Include(item);
                }
            }

            return query;
        }

        public T Atualizar(T item)
        {
            if (!Existe(item.Id)) return null;

            var result = dataset.FirstOrDefault(b => b.Id == item.Id);
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return result;
        }

        public IQueryable<T> ListarPor(Expression<Func<T, bool>> predicado)
        {
            return _context.Set<T>().AsNoTracking().Where(predicado);
        }

        public void Salvar()
        {
            _context.SaveChanges();
        }

        public IQueryable<T> TodosIncluindo(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>().AsNoTracking();
            return includeProperties
                .Aggregate(query, (q, y) => q.Include(y));
        }

        public ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T,IKey>> chaveSeletora, TipoDeOrdenacao orderBy)
        {
            return Listar(indiceDaPagina, tamanhoDaPagina, chaveSeletora, orderBy);
        }

        public ListaPaginavel<T> Listar(int indiceDaPagina, int tamanhoDaPagina, Expression<Func<T, IKey>> chaveSeletora, Expression<Func<T, bool>> predicado, TipoDeOrdenacao orderBy, params Expression<Func<T, object>>[] incluindoPropriedades)
        {
            IQueryable<T> query = TodosIncluindo(incluindoPropriedades);

            query = (predicado == null) ? query : query.Where(predicado);

            return query.ParaListaPaginavel(indiceDaPagina, tamanhoDaPagina, orderBy, chaveSeletora);
        }
    }
}
