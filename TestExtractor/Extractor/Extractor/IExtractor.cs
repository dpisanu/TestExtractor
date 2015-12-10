using System;
using System.Collections.Generic;
using TestExtractor.Extractor.Enums;
using TestExtractor.Structure;
using TestExtractor.Time;

namespace TestExtractor.Extractor.Extractor
{
    public interface IExtractor
    {
        TestFramework TestFramework { get; }

        IList<T> Extract<T> (IList<string> extractionAssemblies) where T : INode;

        Tuple<IList<T>, ITime> ExtractTimed<T>(IList<string> extractionAssemblies) where T : INode;
    }
}