using System;
using TestExtractor.Extractor.Extractor;

namespace TestExtractor.Extractor
{
    public static class ExtractFactory
    {
        public static T Extractor<T>() where T : new()
        {
            if (!typeof(IExtractor).IsAssignableFrom(typeof(T)))
            {
                throw new Exception("T does not implement IExtractor");
            } 
            return new T();
        }
    }
}