using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NSubstitute;

namespace Persistence.Test
{
    internal static class TestExtensions
    {
        public static DbSet<TEntity> ToMockDbSet<TEntity>(this IEnumerable<TEntity> data) where TEntity : class
        {
            var mockDbSet = Substitute.For<DbSet<TEntity>, IQueryable<TEntity>>();
            var dataQuery = data.AsQueryable();
            var dbQuery   = (IQueryable<TEntity>) mockDbSet;
            dbQuery.Expression.Returns(dataQuery.Expression);
            dbQuery.Provider.Returns(dataQuery.Provider);
            dbQuery.ElementType.Returns(dbQuery.ElementType);
            dbQuery.GetEnumerator().Returns(new TestEnumerator<TEntity>(dataQuery.GetEnumerator()));

            return mockDbSet;
        }
    }
}