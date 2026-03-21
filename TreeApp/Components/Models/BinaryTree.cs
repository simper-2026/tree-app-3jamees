namespace TreeApp.Models
{
    public class BinaryTree
    {
        private Node? root;

        public void Insert(int value)
        {
            root = Insert(root, value, null);
        }

        private Node Insert(Node? node, int value, Node? parent)
        {
            if (node == null) return new Node(value, parent);

            if (value < node.Value)
                node.Left = Insert(node.Left, value, node);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value, node);
            
            UpdateHeight(node);

            return Rebalance(node);
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

            string current = $"{node.Value}[\"{node.Value} h:{node.Height}\"]";

            if (node.Left != null)
            {
                string left = $"{node.Left.Value}[\"{node.Left.Value} h:{node.Left.Height}\"]";
                result += $"{current} --> {left}\n";
                result += BuildMermaid(node.Left);
            }

            if (node.Right != null)
            {
                string right = $"{node.Right.Value}[\"{node.Right.Value} h:{node.Right.Height}\"]";
                result += $"{current} --> {right}\n";
                result += BuildMermaid(node.Right);
            }

            return result;
        }

        private int GetHeight(Node? node)
        {
            return node?.Height ?? -1;
        }

        private void UpdateHeight(Node node)
        {
            node.Height = Math.Max(GetHeight(node.Left), GetHeight(node.Right)) + 1;
        }

        private int GetBalance(Node node)
        {
            return GetHeight(node.Left) - GetHeight(node.Right);
        }

        private Node Rebalance(Node node)
        {
            int balance = GetBalance(node);

            if (balance > 1)
            {
                if (GetBalance(node.Left!) < 0)
                    node.Left = RotateLeft(node.Left!);

                return RotateRight(node);
            }

            if (balance < -1)
            {
                if (GetBalance(node.Right!) > 0)
                    node.Right = RotateRight(node.Right!);

                return RotateLeft(node);
            }

            return node;
        }

        private Node RotateRight(Node z)
        {
            Node y = z.Left!;
            Node? t3 = y.Right;

            y.Right = z;
            z.Left = t3;

            y.Parent = z.Parent;
            z.Parent = y;
            if (t3 != null) t3.Parent = z;

            UpdateHeight(z);
            UpdateHeight(y);

            return y;
        }

        private Node RotateLeft(Node z)
        {
            Node y = z.Right!;
            Node? t2 = y.Left;

            y.Left = z;
            z.Right = t2;

            y.Parent = z.Parent;
            z.Parent = y;
            if (t2 != null) t2.Parent = z;

            UpdateHeight(z);
            UpdateHeight(y);

            return y;
        }
    }
}