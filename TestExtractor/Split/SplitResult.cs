using System.Collections;
using System.Collections.Generic;
using TestExtractor.Structure;

namespace Split
{
    internal class SplitResult<T> : ISplitResult<T> where T : INode
    {
        private readonly List<ISplitPackage<T>> _internalList;

        internal SplitResult()
        {
            _internalList = new List<ISplitPackage<T>>();
        }

        public void Add(ISplitPackage<T> result)
        {
            _internalList.Add(result);
        }

        public IEnumerator<ISplitResult<T>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get { return _internalList.Count; } }
    }
}