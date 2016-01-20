using System.Collections.Generic;
using System.Linq;
using TestExtractor.Structure;

namespace Split
{
    public static class Split
    {
        public static ISplitResult<T> SplitByPackageSize<T>(IList<T> source, int packageSize) where T : INode
        {
            return source.ChunkBy(packageSize).ToSplitResult();
        }

        /// <summary>
        /// See 
        /// http://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size
        /// http://stackoverflow.com/questions/419019/split-list-into-sublists-with-linq
        /// </summary>
        private static IList<List<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize) where T : INode
        {
            return source
                .Select((x, i) => new { Index = i, Value = x })
                .GroupBy(x => x.Index / chunkSize)
                .Select(x => x.Select(v => v.Value).ToList())
                .ToList();
        }

        private static ISplitResult<T> ToSplitResult<T>(this IEnumerable<IEnumerable<T>> source) where T : INode
        {
            var package = new SplitResult<T>();
            foreach (var chunk in source)
            {
                package.Add(new SplitPackage<T>(chunk));
            }
            return package;
        }
    }
}