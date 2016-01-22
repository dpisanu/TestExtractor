using System;
using System.Collections;
using System.Collections.Generic;

namespace TestExtractor.Split
{
    /// <summary>
    ///     Concrete Implementation of SplitPackage object.
    ///     Implements Interface : <see cref="ISplitPackage{T}" />
    /// </summary>
    [Serializable]
    internal sealed class SplitPackage<T> : ISplitPackage<T>
    {
        private readonly IList<T> _internalList;

        /// <summary>
        ///     Created a new instance of <see cref="SplitPackage{T}" />
        /// </summary>
        internal SplitPackage(IEnumerable<T> subList)
        {
            _internalList = new List<T>(subList);
        }

        /// <summary>
        ///     Implements <see cref="IReadOnlyCollection{T}.GetEnumerator" />
        /// </summary>
        public IEnumerator<T> GetEnumerator()
        {
            return _internalList.GetEnumerator();
        }

        /// <summary>
        ///     Implements <see cref="IEnumerable.GetEnumerator" />
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///     Implements <see cref="IReadOnlyCollection{T}.Count" />
        /// </summary>
        public int Count
        {
            get { return _internalList.Count; }
        }
    }
}