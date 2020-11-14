using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Pagination
{
    public static class LinqExtensions
    {
        public static PagedResult<T> GetPaged<T>(this IQueryable<T> query, int page, int pageSize) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = query.Skip(skip).Take(pageSize).ToList();

            return result;
        }

        public async static Task<PagedResult<T>> GetPagedAsync<T>(this IQueryable<T> query,
                                                int page, int pageSize, CancellationToken cancellationToken) where T : class
        {
            var result = new PagedResult<T>();
            result.CurrentPage = page;
            result.PageSize = pageSize;
            result.RowCount = query.Count();

            var pageCount = (double)result.RowCount / pageSize;
            result.PageCount = (int)Math.Ceiling(pageCount);

            var skip = (page - 1) * pageSize;
            result.Items = await query.Skip(skip).Take(pageSize).ToListAsync(cancellationToken);

            return result;
        }
    }
}
