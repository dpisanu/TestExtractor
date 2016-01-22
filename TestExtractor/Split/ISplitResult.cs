using System.Collections.Generic;

namespace TestExtractor.Split
{
    /// <summary>
    ///     Interface defining the API of a Split Result Object
    ///     Implements Interface : <see cref="IReadOnlyCollection{T}" />
    /// </summary>
    public interface ISplitResult<out T> : IReadOnlyCollection<ISplitPackage<T>>
    {
    }
}