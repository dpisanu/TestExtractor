using System.Collections.Generic;
using TestExtractor.Structure;

namespace TestExtractor.Split
{
    public interface ISplitResult<T> : IReadOnlyCollection<ISplitPackage<T>> where T : INode
    {
    }
}