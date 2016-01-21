using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Split
{
    public interface ISplitPackage<out T> : IReadOnlyCollection<T> where T : INode
    {
    }
}