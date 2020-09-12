using GerenciadorDeJogos.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace GerenciadorDeJogos.Domain.Entidades.Base
{
    public class ListaPaginavel<T> : List<T>
    {
        public ListaPaginavel() { }

        public int IndiceDePagina { get; }
        public int NumeroDeRegistrosPorPagina { get; }
        public int TotalDeRegistros { get; }
        public int TotalDePaginas { get; }
        public bool TemPaginaAnterior { get { return (IndiceDePagina > 1); } }

        public bool TemProximaPagina { get { return (IndiceDePagina < TotalDePaginas); } }

        internal ListaPaginavel(
            int indiceDePagina,
            int numeroDeRegistrosPorPagina,
            int totalDeRegistros,
            IQueryable<T> fonteDeDados)
        {
            AddRange(fonteDeDados);
            IndiceDePagina = indiceDePagina;
            NumeroDeRegistrosPorPagina = numeroDeRegistrosPorPagina;
            TotalDeRegistros = totalDeRegistros;
            TotalDePaginas = (int)Math.Ceiling(totalDeRegistros / (double)numeroDeRegistrosPorPagina);
        }

    }

    public static class IQueriableExtensions
    {
        /// <summary>
        /// Extensão para facilitar buscas pagináveis.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de retorno</typeparam>
        /// <typeparam name="TKey">Tipo do objeto de chave da entidade</typeparam>
        /// <param name="fonteDeDados">Fonte geradora para buscas em uma fonte de dados</param>
        /// <param name="indiceDePagina">O índice da página a ser requisitada</param>
        /// <param name="registrosPorPagina">Número de Registros máximo em uma página</param>
        /// <param name="ordenacao">O tipo da ordenação, ASC - Crescente, DESC - Decrescente</param>
        /// <param name="chavesDeOrdenacao">Função seletora das propriedades que representam a ordem de propriedades para ordenação da fonte de dados</param>
        /// <returns></returns>
        public static ListaPaginavel<T> ParaListaPaginavel<T,TKey>(
            this IQueryable<T> fonteDeDados,
            int indiceDePagina,
            int registrosPorPagina,
            TipoDeOrdenacao ordenacao,
            Expression<Func<T, TKey>> chavesDeOrdenacao)
        {
            var totalDeRegistros = fonteDeDados.Count();


            IOrderedQueryable<T> colecaoOrdenada;
            if (ordenacao == TipoDeOrdenacao.ASC)
            {
                colecaoOrdenada = fonteDeDados.OrderBy(chavesDeOrdenacao);
            }
            else
            {
                colecaoOrdenada = fonteDeDados.OrderByDescending(chavesDeOrdenacao);
            }

            var colecao = colecaoOrdenada.Skip((indiceDePagina - 1) * registrosPorPagina).Take(registrosPorPagina);

            return new ListaPaginavel<T>(indiceDePagina, registrosPorPagina, totalDeRegistros, colecao);
        }

        /// <summary>
        /// Extensão para facilitar consultas pagináveis.
        /// </summary>
        /// <typeparam name="T">Tipo do objeto de Retorno</typeparam>
        /// <typeparam name="TKey">Tipo da chave de ordenacao</typeparam>
        /// <param name="fonteDeDados">A fonte geradora de consultas da fonte de dados</param>
        /// <param name="indiceDePagina">O índice da página</param>
        /// <param name="registrosPorPagina">O número de registros máximo por página</param>
        /// <param name="ordenacao">O tipo de ordenação, ASC - Crescente, DESC - Decrescente</param>
        /// <param name="colunaDeOrdenacao">String com o nome da propriedade do objeto de retorno utilizado na ordenação</param>
        /// <returns>Um lista paginável</returns>
        public static ListaPaginavel<T> ParaListaPaginavel<T>(
            this IQueryable<T> fonteDeDados,
            int indiceDePagina,
            int registrosPorPagina,
            TipoDeOrdenacao ordenacao,
            string colunaDeOrdenacao)
        {
            var totalDeRegistro = fonteDeDados.Count();

            var colecao = fonteDeDados
                .OrderBy(colunaDeOrdenacao, ordenacao == TipoDeOrdenacao.DESC)
                .Skip((indiceDePagina - 1) * registrosPorPagina).Take(registrosPorPagina);

            return new ListaPaginavel<T>(indiceDePagina, registrosPorPagina, totalDeRegistro, colecao);
        }

        /// <summary>
        /// OrderBy dinâmico
        /// </summary>
        /// <typeparam name="TEntity">Objeto de retorno</typeparam>
        /// <param name="source">Fonte dos dados</param>
        /// <param name="orderByProperty">Nome da Propriedade em string</param>
        /// <param name="desc">true - ordem descrescente, false - ordem crescente</param>
        /// <returns>Fonte de dados ordenado pelo nome da propriedade</returns>
        /// Baseado em :http://stackoverflow.com/questions/41244/dynamic-linq-orderby-on-ienumerablet
        private static IQueryable<TEntity> OrderBy<TEntity>(this IEnumerable<TEntity> source,
                                                    string orderByProperty, bool desc)
        {
            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command,
                                                   new[] { type, property.PropertyType },
                                                   source.AsQueryable().Expression,
                                                   Expression.Quote(orderByExpression));
            return source.AsQueryable().Provider.CreateQuery<TEntity>(resultExpression);
        }
    }
}
