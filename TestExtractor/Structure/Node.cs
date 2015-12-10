using System;
using System.Collections.Generic;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Structure
{
    /// <summary>
    ///     Concrete implementation of the Node Interface
    ///     Interface : <see cref="INode" />
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public abstract class Node<T> : INode
    {
        private readonly List<string> _categories;
        protected readonly T Tnode;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Node{T}" /> class.
        /// </summary>
        protected Node ()
        {
            _categories = new List<string>();
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Node{T}" /> class.
        /// </summary>
        /// <param name="tNode">Node of T</param>
        protected Node (T tNode)
            : this()
        {
            Tnode = tNode;
        }

        #region Public Methods

        /// <summary>
        ///     Add a new Entry to the Categories List
        /// </summary>
        /// <param name="category">Category Entry to Add</param>
        public void AddCategory (object category)
        {
            var categoryToAdd = category.ToString();
            if (_categories.Contains(categoryToAdd))
            {
                return;
            }
            _categories.Add(categoryToAdd);
        }

        #endregion

        #region Implementations of <see cref="INode" />

        /// <summary>
        ///     Implements <see cref="INode.Assembly" />
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        ///     Implements <see cref="INode.Categories" />
        /// </summary>
        public IList<string> Categories
        {
            get
            {
                return _categories;
            }
        }

        /// <summary>
        ///     Implements <see cref="INode.ClassName" />
        /// </summary>
        public abstract string ClassName { get; }

        /// <summary>
        ///     Implements <see cref="INode.Description" />
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        ///     Implements <see cref="INode.IgnoreReason" />
        /// </summary>
        public abstract string IgnoreReason { get; }

        /// <summary>
        ///     Implements <see cref="INode.Ignored" />
        /// </summary>
        public bool Ignored
        {
            get
            {
                return IgnoreReason != null;
            }
        }

        /// <summary>
        ///     Implements <see cref="INode.IsSuite" />
        /// </summary>
        public abstract bool IsSuite { get; }

        /// <summary>
        ///     Implements <see cref="INode.ParentFullName" />
        /// </summary>
        public abstract string ParentFullName { get; }

        /// <summary>
        ///     Implements <see cref="INode.NodeName" />
        /// </summary>
        public INodeName NodeName { get; protected set; }

        /// <summary>
        ///     Implements <see cref="INode.NodeType" />
        /// </summary>
        public NodeTypes NodeType { get; protected set; }

        #endregion

        #region IEquatable

        /// <summary>
        ///     Implements <see cref="IEquatable{T}.Equals(T)" />
        /// </summary>
        public bool Equals(INode other)
        {
            return NodeName.Equals(other.NodeName) && IsSuite.Equals(other.IsSuite) && ParentFullName.Equals(other.ParentFullName);
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
            if (!(other is INode))
            {
                return false;
            }
            return Equals(other as INode);
        }

        /// <summary>
        ///     Returns the hash code of this object
        /// </summary>
        public override sealed int GetHashCode ()
        {
            unchecked
            {
                if (ParentFullName == null)
                {
                    return NodeName.GetHashCode() + IsSuite.GetHashCode();
                }
                return NodeName.GetHashCode() + ParentFullName.GetHashCode() + IsSuite.GetHashCode();
            }
        }

        #endregion
    }
}