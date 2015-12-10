using System;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Interace specifying the API of a Node Name
    ///     Interface : <see cref="IEquatable{T}" />
    /// </summary>
    public interface INodeName : IEquatable<INodeName>
    {
        /// <summary>
        ///     Full Testname
        /// </summary>
        string FullName { get; }

        /// <summary>
        ///     Test Name
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Unique Test Name
        /// </summary>
        string UniqueName { get; }
    }
}