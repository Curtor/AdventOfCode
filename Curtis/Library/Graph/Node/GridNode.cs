﻿namespace csteeves;

public class GridNode<T> : GraphNode<GridNode<T>, T> {

    public readonly Vector2Int coord;

    public GridNode(int x, int y, T? value = default(T)) : base(value) {
        this.coord = new Vector2Int(x, y);
        this.heuristicDistance = node => coord.Distance(node.coord);
    }

    public GridNode(GridNode<T> node) : base(node.value) {
        coord = node.coord;
        heuristicDistance = node.heuristicDistance;
    }

    public override string ToString() {
        return $"Node {coord}: {value}";
    }
}
