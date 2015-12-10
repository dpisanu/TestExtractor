using System;

namespace TestExtractor.Structure.Enums
{
    /// <summary>
    ///     Enumerator defining different Node Types
    /// </summary>
    [Serializable]
    public enum NodeTypes
    {
        Assembly,
        Namespace,
        TestFixture,
        TestMethod
    }

    public static class EnumConverter
    {
        public static NodeTypes Convert(string testType)
        {
            NodeTypes type;
            Enum.TryParse(testType, out type);
            return type;
        }
    }
}