using System;
using System.Collections.Generic;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Interace specifying the API of a Node
    ///     Interface : <see cref="IEquatable{T}" />
    /// </summary>
    public interface INode : IEquatable<INode>
    {
        /// <summary>
        ///     Assembly where this Node is located in
        /// </summary>
        string Assembly { get; }

        /// <summary>
        ///     Categories of the Node
        /// </summary>
        IList<string> Categories { get; }

        /// <summary>
        ///     Class Name of the Node
        /// </summary>
        string ClassName { get; }

        /// <summary>
        ///     Description of the Node
        /// </summary>
        string Description { get; }

        /// <summary>
        ///     Ignore Reason of the Node
        /// </summary>
        string IgnoreReason { get; }

        /// <summary>
        ///     If Node is ignored
        /// </summary>
        bool Ignored { get; }

        /// <summary>
        ///     If Node is a Test Suite
        /// </summary>
        bool IsSuite { get; }

        /// <summary>
        ///     Full Name of Test Node Parent
        /// </summary>
        string ParentFullName { get; }

        /// <summary>
        ///     Test Name of the Node
        /// </summary>
        INodeName NodeName { get; }

        /// <summary>
        ///     Type of Node
        /// </summary>
        NodeTypes NodeType { get; }
    }
}