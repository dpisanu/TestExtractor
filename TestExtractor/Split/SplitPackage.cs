using System.Collections;
using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Split
{
    internal class SplitPackage<T> : ISplitPackage<T> where T : INode
    {
        private readonly IList<T> _internalList;

        internal SplitPackage(IEnumerable<T> subList)
        {
            _internalList = new List<T>(subList);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count { get { return _internalList.Count; } }
    }
}
