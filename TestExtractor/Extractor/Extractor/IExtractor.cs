using System;
using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Extractor.Extractor
{
    /// <summary>
    ///     Interface describing the API of an Extractor Object
    /// </summary>
    public interface IExtractor
    {
        /// <summary>
        ///     <see cref="Enum"/> of the TestFramework of the Extractor
        /// </summary>
        Enum TestFramework { get; }

        /// <summary>
        ///     Extract Test Nodes from a List of Assemblies
        /// </summary>
        /// <typeparam name="T">Type to extract. Must be of Type <see cref="INode" /></typeparam>
        /// <param name="extractionAssemblies">List of Assemblies</param>
        /// <returns>List of Extracted Types</returns>
        IList<T> Extract<T>(IList<string> extractionAssemblies) where T : INode;

        /// <summary>
        ///     Extract Test Nodes from a List of Assemblies and time the operation
        /// </summary>
        /// <typeparam name="T">Type to extract. Must be of Type <see cref="INode" /></typeparam>
        /// <param name="extractionAssemblies">List of Assemblies</param>
        /// <returns>List of Extracted Types and the <see cref="TimeSpan" /> result of the time measurement</returns>
        Tuple<IList<T>, TimeSpan> ExtractTimed<T>(IList<string> extractionAssemblies) where T : INode;
    }
}