namespace TreeApp.Models
{
    public class BinaryTree
    {
        private Node? root;

        public void Insert(int value)
        {
            root = Insert(root, value);
        }

        private Node Insert(Node? node, int value)
        {
            if (node == null) return new Node(value);

            if (value < node.Value)
                node.Left = Insert(node.Left, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value);

            return node;
        }

        public string InOrder()
        {
            return InOrder(root).Trim();
        }

        private string InOrder(Node? node)
        {
            if (node == null) return "";

            return InOrder(node.Left) + node.Value + " " + InOrder(node.Right);
        }

        public int Height()
        {
            return Height(root);
        }

        private int Height(Node? node)
        {
            if (node == null) return -1;

            return Math.Max(Height(node.Left), Height(node.Right)) + 1;
        }

        public string ToMermaid()
        {
            if (root == null)
                return "graph TD\n empty[\"(empty tree)\"]";

            if (root.Left == null && root.Right == null)
                return "graph TD\n " + root.Value;

            return "graph TD\n" + BuildMermaid(root);
        }

        private string BuildMermaid(Node? node)
        {
            if (node == null) return "";

            string result = "";

            if (node.Left != null)
            {
                result += node.Value + " --> " + node.Left.Value + "\n";
                result += BuildMermaid(node.Left);
            }

            if (node.Right != null)
            {
                result += node.Value + " --> " + node.Right.Value + "\n";
                result += BuildMermaid(node.Right);
            }

            return result;
        }
    }
}