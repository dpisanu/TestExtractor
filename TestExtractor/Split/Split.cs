using System.Collections.Generic;
using System.Linq;

namespace TestExtractor.Split
{
    /// <summary>
    ///     Static Class offering Extention Methods for <see cref="IEnumerable{T}" />
    /// </summary>
    public static class Split
    {
        /// <summary>
        ///     Split a Collection of T into a Collection of SubCollections of a specific size
        /// </summary>
        /// <typeparam name="T">Type of item contained in Collection</typeparam>
        /// <param name="source">Source Collection</param>
        /// <param name="packageSize">Size of Sub Package</param>
        /// <returns><see cref="ISplitResult{T}" /> Result</returns>
        public static ISplitResult<T> SplitByPackageSize<T>(IEnumerable<T> source, int packageSize)
        {
            return source.ChunkBy(packageSize).ToSplitResult();
        }

        /// <summary>
        ///     Create a Collection of Sub Collections of a specific from a source collection.
        /// </summary>
        /// <typeparam name="T">Type of item contained in Collection</typeparam>
        /// <param name="source">Source Collection</param>
        /// <param name="chunkSize">Chunksize</param>
        /// <returns>A collection of sub collections</returns>
        /// <remarks>
        ///     See
        ///     <see cref="http://stackoverflow.com/questions/11463734/split-a-list-into-smaller-lists-of-n-size" />
        ///     <see cref="http://stackoverflow.com/questions/419019/split-list-into-sublists-with-linq" />
        /// </remarks>
        private static IEnumerable<IEnumerable<T>> ChunkBy<T>(this IEnumerable<T> source, int chunkSize)
        {
            return source
                .Select((x, i) => new {Index = i, Value = x})
                .GroupBy(x => x.Index/chunkSize)
                .Select(x => x.Select(v => v.Value).ToList());
        }

        /// <summary>
        /// Overload Method to make an <see cref="ISplitResult{T}"/> object out of a collection of collections
        /// </summary>
        /// <typeparam name="T">Type of item contained in Collection</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>A <see cref="ISplitResult{T}"/></returns>
        private static ISplitResult<T> ToSplitResult<T>(this IEnumerable<IEnumerable<T>> source)
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