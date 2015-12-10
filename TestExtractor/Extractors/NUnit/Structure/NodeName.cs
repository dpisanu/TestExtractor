using System;
using NUnit.Core;

namespace TestExtractor.Extractors.NUnit.Structure
{
    /// <summary>
    ///     Concrete implementation of the Node Name Interface
    ///     Inherits : <see cref="TestExtractor.Structure.NodeName" />
    /// </summary>
    [Serializable]
    public sealed class NodeName : TestExtractor.Structure.NodeName
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NodeName" /> class.
        /// </summary>
        public NodeName ()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="NodeName" /> class.
        /// </summary>
        public NodeName (ITest tNode)
        {
            FullName = tNode.TestName.FullName;
            Name = tNode.TestName.Name;
            UniqueName = tNode.TestName.UniqueName;
        }
    }
}