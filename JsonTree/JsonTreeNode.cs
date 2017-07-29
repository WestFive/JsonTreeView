using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JsonTree
{

    public enum NodeType
    {
        Value,
        Array,
        Object
    }

    public class JsonTreeNode : TreeNode
    {
        public NodeType NodeType { get; set; }

        public string TextWhenSelected
        {
            get
            {
                return textWhenSelected;
            }
        }

        public bool IsExpandable
        {
            get
            {
                return NodeType == NodeType.Object || NodeType == NodeType.Array;
            }
        }

        public JsonTreeNode(NodeType nodeType, string text, string textWhenSelected = null)
        {
            NodeType = nodeType;
            Text = text;
            this.textWhenSelected = textWhenSelected ?? text;
        }

        private string textWhenSelected;
    }
}
