using System.Collections;
using System.Collections.Generic;

namespace Persistence.Test.Mocks
{
    public class TestEnumerator<TEntity> : IEnumerator<TEntity>
    {
        private readonly IEnumerator<TEntity> _entities;

        public TestEnumerator(IEnumerator<TEntity> entities)
        {
            _entities = entities;
        }

        public bool MoveNext()
        {
            return _entities.MoveNext();
        }

        public void Reset()
        {
            _entities.Reset();
        }

        public TEntity Current => _entities.Current;

        object? IEnumerator.Current => Current;

        public void Dispose()
        {
            _entities.Dispose();
        }
    }
}