using System;
using System.Collections.Generic;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Interace specifying the API of a Suite Node
    ///     Interface : <see cref="INode" />
    ///     Interface : <see cref="IEquatable{T}" />
    /// </summary>
    public interface ISuiteNode : INode, IEquatable<ISuiteNode>
    {
        /// <summary>
        ///     Test Suites included in this Test Suite Node
        /// </summary>
        IList<string> Suites { get; }

        /// <summary>
        ///     Tests included directly in this TestSuite
        /// </summary>
        IList<string> Stubs { get; }

        /// <summary>
        ///     Amount of Tests encapsulated this Test Suite
        /// </summary>
        int StubCount { get; }
    }
}