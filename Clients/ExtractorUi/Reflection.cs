using System;
using System.Linq.Expressions;

namespace TestExtractor.ExtractorUi
{
    /// <summary>
    ///     Static class providing some Reflection Methods
    /// </summary>
    internal static class Reflection
    {
        /// <summary>
        ///     Returns the name of a property
        /// </summary>
        /// <typeparam name="TSource"> Source of Property </typeparam>
        /// <typeparam name="TProperty"> Property </typeparam>
        /// <param name="funcExpression"> Function Expression used to search the Property </param>
        /// <returns> The name of the property </returns>
        public static string PropertyName<TSource, TProperty>(Expression<Func<TSource, TProperty>> funcExpression)
        {
            var property = (MemberExpression) funcExpression.Body;
            return property.Member.Name;
        }
    }
}