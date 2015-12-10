using System;
using NUnit.Core;
using TestExtractor.Structure;

namespace TestExtractor.Extractors.NUnit.Structure
{
    /// <summary>
    ///     Concrete implementation of the Stub Node Interface
    ///     Class :     <see cref="Node" />
    ///     Interface : <see cref="IStubNode" />
    /// </summary>
    [Serializable]
    public class StubNode : Node, IStubNode
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StubNode" /> class.
        /// </summary>
        /// <param name="node">NUnit Test Node</param>
        public StubNode (TestNode node)
            : base(node)
        {
        }

        #region Implementations of <see cref="ITestMethodNode" />

        /// <summary>
        ///     Implements <see cref="IStubNode.StubName" />
        /// </summary>
        public string StubName
        {
            get
            {
                return Tnode.MethodName;
            }
        }

        #endregion

        #region IEquatable

        /// <summary>
        ///     Implements <see cref="IEquatable{T}.Equals(T)" />
        /// </summary>
        public bool Equals (IStubNode other)
        {
            return base.Equals(other);
        }

        #endregion
    }
}