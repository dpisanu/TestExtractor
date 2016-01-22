using System;
using System.Collections;
using System.Collections.Generic;

namespace TestExtractor.Split
{
    /// <summary>
    ///     Concrete Implementation of a SplitResult Object.
    ///     Implements Interface : <see cref="ISplitResult{T}" />
    /// </summary>
    [Serializable]
    internal sealed class SplitResult<T> : ISplitResult<T>
    {
        private readonly List<ISplitPackage<T>> _internalList;

        /// <summary>
        ///     Created a new instance of <see cref="SplitResult{T}" />
        /// </summary>
        internal SplitResult()
        {
            _internalList = new List<ISplitPackage<T>>();
        }

        /// <summary>
        ///     Created a new instance of <see cref="SplitResult{T}" />
        /// </summary>
        /// <param name="packages">Packages to directly include in the <see cref="SplitResult{T}" /></param>
        internal SplitResult(IEnumerable<ISplitPackage<T>> packages)
        {
            _internalList = new List<ISplitPackage<T>>(packages);
        }

        /// <summary>
        ///     Implements <see cref="IReadOnlyCollection{T}.GetEnumerator" />
        /// </summary>
        public IEnumerator<ISplitPackage<T>> GetEnumerator()
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

        /// <summary>
        ///     Add a <see cref="ISplitPackage{T}" /> to the SplitResult
        /// </summary>
        /// <param name="package"><see cref="ISplitPackage{T}" /> to add.</param>
        public void Add(ISplitPackage<T> package)
        {
            if (_internalList.Contains(package))
            {
                return;
            }
            _internalList.Add(package);
        }
    }
}