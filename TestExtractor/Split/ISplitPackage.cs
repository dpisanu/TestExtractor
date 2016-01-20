using System.Collections.Generic;
using TestExtractor.Structure;

namespace Split
{
    public interface ISplitPackage<out T> : IReadOnlyCollection<T> where T : INode
    {
    }
}