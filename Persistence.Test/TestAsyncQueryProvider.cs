using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Models;

namespace Persistence.Test
{
    public class TestAsyncQueryProvider : IAsyncQueryProvider
    {
        private readonly IQueryProvider _provider;

        public TestAsyncQueryProvider(IQueryProvider provider)
        {
            _provider = provider;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return _provider.CreateQuery(expression);
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return _provider.CreateQuery<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            return _provider.Execute(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return _provider.Execute<TResult>(expression);
        }

        public TResult ExecuteAsync<TResult>(Expression expression,
            CancellationToken cancellationToken = new CancellationToken())
        {
            return (TResult) typeof(Task).GetMethod(nameof(Task.FromResult))
                ?.MakeGenericMethod(expression.Type).Invoke(null, new[] {_provider.Execute(expression)});
        }
    }
}