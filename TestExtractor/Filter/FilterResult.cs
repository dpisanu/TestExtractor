using System;
using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Extractor.Filter
{
    /// <summary>
    ///     Concrete Implementation of a FilterResult
    ///     Implements Interface : <see cref="IFilterResult{T}" />
    /// </summary>
    /// <typeparam name="T">
    ///     Type of object contained in the FilterResult.
    ///     Needs to be of Type <see cref="INode" />
    /// </typeparam>
    [Serializable]
    public class FilterResult<T> : IFilterResult<T> where T : INode
    {
        /// <summary>
        ///     Created a new instance of <see cref="FilterResult{T}" />
        /// </summary>
        public FilterResult(IList<T> ofFilters, IList<T> notOfFilters)
        {
            OfFilters = ofFilters;
            NotOfFilters = notOfFilters;
        }

        /// <summary>
        ///     Implements <see cref="IFilterResult{T}.OfFilters" />
        /// </summary>
        public IList<T> OfFilters { get; private set; }

        /// <summary>
        ///     Implements <see cref="IFilterResult{T}.NotOfFilters" />
        /// </summary>
        public IList<T> NotOfFilters { get; private set; }
    }
}