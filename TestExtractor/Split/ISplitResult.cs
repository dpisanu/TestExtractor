using System.Collections.Generic;
using TestExtractor.Structure;

namespace Split
{
    public interface ISplitResult<T> : IReadOnlyCollection<ISplitResult<T>> where T : INode
    {
    }
}