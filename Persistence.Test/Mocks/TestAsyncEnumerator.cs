using System.Collections.Generic;
using System.Threading.Tasks;

namespace Persistence.Test.Mocks
{
    public class TestAsyncEnumerator<TEntity> : IAsyncEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> _entities;

        public TestAsyncEnumerator(IEnumerator<TEntity> entities)
        {
            _entities = entities;
        }


        public async ValueTask DisposeAsync()
        {
            await Task.Run(() => _entities.Dispose());
        }

        public async ValueTask<bool> MoveNextAsync()
        {
            return await Task.FromResult(_entities.MoveNext());
        }

        public TEntity Current => _entities.Current;
    }
}