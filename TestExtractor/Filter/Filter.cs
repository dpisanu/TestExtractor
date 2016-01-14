using System;
using System.Collections.Generic;
using System.Linq;
using TestExtractor.Structure;

namespace Filter
{
    /// <summary>
    ///     Concrete Implementation of the Filter Class
    ///     Implements Interface : <see cref="IFilter" />
    /// </summary>
    [Serializable]
    public class Filter : IFilter
    {
        /// <summary>
        ///     Implements <see cref="IFilter.FilterCategories{T}" />
        /// </summary>
        public IFilterResult<T> FilterCategories<T>(IList<T> nodes, IList<string> categories) where T : INode
        {
            var ofFilters = new List<T>();
            var notOfFilters = new List<T>();

            foreach (var node in nodes)
            {
                if (node.Categories.Any(categories.Contains))
                {
                    ofFilters.Add(node);
                }
                else
                {
                    notOfFilters.Add(node);
                }
            }

            return new FilterResult<T>(ofFilters, notOfFilters);
        }

        /// <summary>
        ///     Implements <see cref="IFilter.FilterAssembly{T}" />
        /// </summary>
        public IFilterResult<T> FilterAssembly<T>(IList<T> nodes, IList<string> assemblies) where T : INode
        {
            var ofFilters = new List<T>();
            var notOfFilters = new List<T>();

            foreach (var node in nodes)
            {
                if (assemblies.Contains(node.Assembly))
                {
                    ofFilters.Add(node);
                }
                else
                {
                    notOfFilters.Add(node);
                }
            }

            return new FilterResult<T>(ofFilters, notOfFilters);
        }
    }
}
