using System;
using TestExtractor.Extractor.Extractor;

namespace TestExtractor.Extractor
{
    /// <summary>
    ///     Static Class representing an ExtractFactory
    /// </summary>
    public static class ExtractFactory
    {
        /// <summary>
        ///     Create a new Instance of a concrete Extractor that implements the Interface <see cref="IExtractor" />
        /// </summary>
        /// <typeparam name="T">T to create an instance of</typeparam>
        /// <returns>Instance of T</returns>
        public static T Extractor<T>() where T : new()
        {
            if (!typeof (IExtractor).IsAssignableFrom(typeof (T)))
            {
                throw new Exception("T does not implement IExtractor");
            }
            return new T();
        }
    }
}