namespace csteeves;

public class TreeNode<N, T> : Node<N, T> where N : TreeNode<N, T> {

    public NodeNeighbor<N, T>? Parent = null;

    protected List<NodeNeighbor<N, T>> Children = [];

    public TreeNode() : base() { }
    public TreeNode(T value) : base(value) { }

    public override IEnumerable<NodeNeighbor<N, T>> NextNodes() {
        return Children;
    }
}
