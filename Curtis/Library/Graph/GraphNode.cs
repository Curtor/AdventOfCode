namespace csteeves.AdventLibrary;

public class GraphNode<N, T> : Node<N, T> where N : GraphNode<N, T> {

    public List<NodeNeighbor<N, T>> Neighbors = [];

    public GraphNode(T? value = default(T), float cost = 1) : base(value, cost) { }

    protected override IEnumerable<NodeNeighbor<N, T>> NextNodes() {
        return Neighbors;
    }
}
