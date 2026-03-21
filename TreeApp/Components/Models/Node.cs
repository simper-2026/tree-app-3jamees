namespace TreeApp.Models
{
    public class Node
    {
        public int Value { get; private set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node? Parent { get; set; }
        public int Height { get; set; }

        public Node(int value, Node? parent = null)
        {
            Value = value;
            Parent = parent;
            Height = 0;
        }
    }
}