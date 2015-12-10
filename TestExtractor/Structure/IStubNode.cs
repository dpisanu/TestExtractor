using System;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Interace specifying the API of a Stub Node
    ///     Interface : <see cref="INode" />
    ///     Interface : <see cref="IEquatable{T}" />
    /// </summary>
    public interface IStubNode : INode, IEquatable<IStubNode>
    {
        /// <summary>
        ///     Name of the Stub Node
        /// </summary>
        string StubName { get; }
    }
}