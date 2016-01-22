using System.Collections.Generic;

namespace TestExtractor.Split
{
    /// <summary>
    ///     Interface defining the API of a Split Package Object
    ///     Implements Interface : <see cref="IReadOnlyCollection{T}" />
    /// </summary>
    public interface ISplitPackage<out T> : IReadOnlyCollection<T>
    {
    }
}