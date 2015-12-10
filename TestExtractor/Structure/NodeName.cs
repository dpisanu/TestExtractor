using System;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Concrete implementation of the Node Name Interface
    ///     Interface : <see cref="INodeName" />
    /// </summary>
    [Serializable]
    public abstract class NodeName : INodeName
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="NodeName" /> class.
        /// </summary>
        protected NodeName()
        {
            FullName = string.Empty;
            Name = string.Empty;
            UniqueName = string.Empty;
        }

        #region Implementations of <see cref="ITestName" />

        /// <summary>
        ///     Implements <see cref="INodeName.FullName" />
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        ///     Implements <see cref="INodeName.Name" />
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     Implements <see cref="INodeName.UniqueName" />
        /// </summary>
        public string UniqueName { get; set; }

        #endregion

        #region IEquatable

        /// <summary>
        ///     Implements <see cref="IEquatable{T}.Equals(T)" />
        /// </summary>
        public bool Equals(INodeName other)
        {
            return FullName.Equals(other.FullName) && Name.Equals(other.Name) && UniqueName.Equals(other.UniqueName);
        }

        /// <summary>
        ///     Return true if the hash codes of the Ids are equal
        /// </summary>
        public override bool Equals (object other)
        {
            if (other == null)
            {
                return false;
            }
            if (!(other is INodeName))
            {
                return false;
            }
            return Equals(other as INodeName);
        }

        /// <summary>
        ///     Returns the hash code of this object
        /// </summary>
        public override int GetHashCode ()
        {
            unchecked
            {
                return FullName.GetHashCode() + Name.GetHashCode() + UniqueName.GetHashCode();
            }
        }

        #endregion
    }
}