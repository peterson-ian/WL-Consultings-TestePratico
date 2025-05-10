using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WL_Consultings_TestePratico.Models.DTOs.Paginacao;

namespace WL_Consultings_TestePratico.Models.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginacaoResponse<T>> ToPaginatedResultAsync<T>(this IQueryable<T> query, int numeroPagina, int tamanhoPagina)
        {
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)tamanhoPagina);

            var items = await query
                .Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PaginacaoResponse<T>
            {
                Items = items,
                TotalItems = totalItems,
                NumeroPagina = numeroPagina,
                TotalPaginas = totalPages,
                TemPaginaSequente = numeroPagina < totalPages,
                TemPaginaAnterior = numeroPagina > 1
            };
        }

        public static async Task<PaginacaoResponse<TDestination>> ToPaginatedResultAsync<TSource, TDestination>(
            this IQueryable<TSource> query,
            int numeroPagina,
            int tamanhoPagina,
            AutoMapper.IConfigurationProvider configuracaoMapper)
        {
            var totalItems = await query.CountAsync();
            var totalPages = (int)Math.Ceiling(totalItems / (double)tamanhoPagina);

            var items = await query
                .ProjectTo<TDestination>(configuracaoMapper)
                .Skip((numeroPagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .ToListAsync();

            return new PaginacaoResponse<TDestination>
            {
                Items = items,
                TotalItems = totalItems,
                NumeroPagina = numeroPagina,
                TotalPaginas = totalPages,
                TemPaginaSequente = numeroPagina < totalPages,
                TemPaginaAnterior = numeroPagina > 1
            };
        }
    }
}
