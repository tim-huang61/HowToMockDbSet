using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;

namespace Persistence.Test.Mocks
{
    public class TestAsyncEnumerable<TEntity> : EnumerableQuery<TEntity>, IAsyncEnumerable<TEntity>, IQueryable<TEntity>
    {
        public TestAsyncEnumerable(IEnumerable<TEntity> entities) : base(entities)
        {
        }

        public TestAsyncEnumerable(Expression expression) : base(expression)
        {
        }

        public IAsyncEnumerator<TEntity> GetAsyncEnumerator(
            CancellationToken cancellationToken = new CancellationToken())
        {
            return new TestAsyncEnumerator<TEntity>(((IQueryable<TEntity>) this).GetEnumerator());
        }

        IQueryProvider IQueryable.Provider => new TestAsyncQueryProvider<TEntity>(this);
    }
}