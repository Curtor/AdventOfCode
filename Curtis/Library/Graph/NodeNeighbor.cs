namespace csteeves;

public class NodeNeighbor<N, T> where N : Node<N, T> {

    public readonly N neighbor;
    public readonly float cost;

    public NodeNeighbor(N neighbor, float cost = 1) {
        if (cost <= 0) {
            throw new ArgumentException("cost");
        }

        this.neighbor = neighbor;
        this.cost = cost;
    }
}
