using System;
using System.Collections.Generic;
using NUnit.Core;
using TestExtractor.Structure;

namespace TestExtractor.Extractors.NUnit.Structure
{
    /// <summary>
    ///     Concrete implementation of the Suite Node Interface
    ///     Class :     <see cref="Node" />
    ///     Interface : <see cref="ISuiteNode" />
    /// </summary>
    [Serializable]
    public sealed class SuiteNode : Node, ISuiteNode
    {
        private readonly List<string> _suites;
        private readonly List<string> _stubs;

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuiteNode" /> class.
        /// </summary>
        public SuiteNode ()
        {
            _suites = new List<string>();
            _stubs = new List<string>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="SuiteNode" /> class.
        /// </summary>
        /// <param name="tNode">NUnit Test Node</param>
        public SuiteNode (TestNode tNode)
            : base(tNode)
        {
            _suites = new List<string>();
            _stubs = new List<string>();
        }

        #region Implementations of <see cref="ITestNode" />

        /// <summary>
        ///     Implements <see cref="ISuiteNode.Suites" />
        /// </summary>
        public IList<string> Suites
        {
            get
            {
                return _suites;
            }
        }

        /// <summary>
        ///     Implements <see cref="ISuiteNode.Stubs" />
        /// </summary>
        public IList<string> Stubs
        {
            get
            {
                return _stubs;
            }
        }

        /// <summary>
        ///     Implements <see cref="ISuiteNode.StubCount" />
        /// </summary>
        public int StubCount
        {
            get
            {
                return Tnode.TestCount;
            }
        }

        #endregion

        #region IEquatable

        /// <summary>
        ///     Implements <see cref="IEquatable{T}.Equals(T)" />
        /// </summary>
        public bool Equals (ISuiteNode other)
        {
            return base.Equals(other);
        }

        #endregion
    }
}