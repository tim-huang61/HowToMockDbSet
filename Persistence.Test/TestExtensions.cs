using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Moq;
using NSubstitute;
using Persistence.Test.Mocks;

namespace Persistence.Test
{
    internal static class TestExtensions
    {
        public static DbSet<TEntity> ToMockDbSet<TEntity>(this IEnumerable<TEntity> data) where TEntity : class
        {
            var mockDbSet = Substitute.For<DbSet<TEntity>, IOrderedQueryable<TEntity>, IAsyncEnumerable<TEntity>>();
            var dataQuery = data.AsQueryable();
            var dbQuery   = (IQueryable<TEntity>) mockDbSet;
            dbQuery.Expression.Returns(dataQuery.Expression);
            dbQuery.Provider.Returns(new TestAsyncQueryProvider<TEntity>(dataQuery.Provider));
            dbQuery.ElementType.Returns(dbQuery.ElementType);
            dbQuery.GetEnumerator().Returns(new TestEnumerator<TEntity>(dataQuery.GetEnumerator()));
            var dbAsync = (IAsyncEnumerable<TEntity>) mockDbSet;
            mockDbSet.AsAsyncEnumerable().Returns(new TestAsyncEnumerable<TEntity>(data));
            dbAsync.GetAsyncEnumerator().Returns(new TestAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDbSet;
        }

        public static DbSet<TEntity> ToMockDbSetByMoq<TEntity>(this List<TEntity> data) where TEntity : class
        {
            var mockDbSet = new Mock<DbSet<TEntity>>();
            var dataQuery = data.AsQueryable();
            mockDbSet.As<IQueryable<TEntity>>().Setup(d => d.Expression).Returns(dataQuery.Expression);
            mockDbSet.As<IQueryable<TEntity>>().Setup(d => d.ElementType).Returns(dataQuery.ElementType);
            mockDbSet.As<IQueryable<TEntity>>().Setup(d => d.Provider)
                .Returns(new TestAsyncQueryProvider<TEntity>(dataQuery.Provider));
            mockDbSet.As<IQueryable<TEntity>>().Setup(d => d.GetEnumerator())
                .Returns(new TestEnumerator<TEntity>(data.GetEnumerator()));
            mockDbSet.As<IAsyncEnumerable<TEntity>>().Setup(d => d.GetAsyncEnumerator(CancellationToken.None))
                .Returns(new TestAsyncEnumerator<TEntity>(data.GetEnumerator()));

            return mockDbSet.Object;
        }
    }
}