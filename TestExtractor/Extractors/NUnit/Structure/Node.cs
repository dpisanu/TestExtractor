using System;
using NUnit.Core;
using TestExtractor.Structure;
using TestExtractor.Structure.Enums;

namespace TestExtractor.Extractors.NUnit.Structure
{
    /// <summary>
    ///     Concrete implementation of the Node Interface
    ///     Inherits : <see cref="Node{T}" />
    /// </summary>
    [Serializable]
    public abstract class Node : Node<TestNode>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Node" /> class.
        /// </summary>
        protected Node ()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="Node" /> class.
        /// </summary>
        /// <param name="tNode">NUnit Test Node</param>
        protected Node (TestNode tNode)
            : base(tNode)
        {
            NodeName = new NodeName(tNode);
            NodeType = EnumConverter.Convert(Tnode.TestType);

            foreach (var category in Tnode.Categories)
            {
                AddCategory(category);
            }
        }

        #region Node{T}

        /// <summary>
        ///     Overrides <see cref="Node{T}.ClassName" />
        /// </summary>
        public override string ClassName
        {
            get
            {
                return string.IsNullOrEmpty(Tnode.ClassName) ? string.Empty : Tnode.ClassName;
            }
        }

        /// <summary>
        ///     Overrides <see cref="Node{T}.Description" />
        /// </summary>
        public override string Description
        {
            get
            {
                return string.IsNullOrEmpty(Tnode.Description) ? string.Empty : Tnode.Description.Trim('\"');
            }
        }

        /// <summary>
        ///     Overrides <see cref="Node{T}.IgnoreReason" />
        /// </summary>
        public override string IgnoreReason
        {
            get
            {
                return string.IsNullOrEmpty(Tnode.IgnoreReason) ? null : Tnode.IgnoreReason;
            }
        }

        /// <summary>
        ///     Overrides <see cref="Node{T}.IsSuite" />
        /// </summary>
        public override bool IsSuite
        {
            get
            {
                return Tnode.IsSuite;
            }
        }

        /// <summary>
        ///     Overrides <see cref="Node{T}.ParentFullName" />
        /// </summary>
        public override string ParentFullName
        {
            get
            {
                return Tnode.Parent == null ? string.Empty : Tnode.Parent.TestName.FullName;
            }
        }

        #endregion
    }
}