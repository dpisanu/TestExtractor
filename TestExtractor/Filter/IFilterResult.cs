using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Extractor.Filter
{
    /// <summary>
    ///     Interface defining the APi of a Filter Result
    /// </summary>
    /// <typeparam name="T">
    ///     Type of object contained in the FilterResult.
    ///     Needs to be of Type <see cref="INode" />
    /// </typeparam>
    public interface IFilterResult<T> where T : INode
    {
        /// <summary>
        ///     List of Objects that contain at least one of the filters
        /// </summary>
        IList<T> OfFilters { get; }

        /// <summary>
        ///     List of Objects that contain none of the filters
        /// </summary>
        IList<T> NotOfFilters { get; }
    }
}