namespace csteeves;

internal class Grid<T> {

    public readonly int width;
    public readonly int height;
    public readonly bool allowDiagonal;

    private readonly GridNode<T>[,] nodes;

    public Grid(int width, int height, bool allowDiag = false) {
        this.width = width;
        this.height = height;
        this.allowDiagonal = allowDiag;

        nodes = new GridNode<T>[width, height];
        InitializeNodes();
    }

    private void InitializeNodes() {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                nodes[x, y] = new GridNode<T>(x, y);
            }
        }
    }

    public GridNode<T> GetNode(int x, int y) {
        return nodes[x, y];
    }

    public IEnumerable<GridNode<T>> AllNodes() {
        foreach (GridNode<T> node in nodes) {
            yield return node;
        }
    }

    public void SetValues(Func<GridNode<T>, T> value) {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                GridNode<T> current = nodes[x, y];
                current.value = value(current);
            }
        }
    }

    public void SetNeighbors(
            Func<GridNode<T>, GridNode<T>, bool> isNeighbor,
            Func<GridNode<T>, GridNode<T>, float>? neighborCost = null) {
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                GridNode<T> current = nodes[x, y];
                current.Neighbors.Clear();
                foreach (GridNode<T> neighbor in GetDefaultNeighbors(x, y)) {
                    if (isNeighbor(current, neighbor)) {
                        float cost = neighborCost == null ? 1 : neighborCost(current, neighbor);
                        current.Neighbors.Add(new NodeNeighbor<GridNode<T>, T>(neighbor, cost));
                    }
                }
            }
        }
    }

    private IEnumerable<GridNode<T>> GetDefaultNeighbors(int targetX, int targetY) {
        for (int x = targetX - 1; x <= targetX + 1; x++) {
            for (int y = targetY - 1; y <= targetY + 1; y++) {
                if (x == targetX && y == targetY) {
                    continue;
                }
                if (x < 0 || y < 0 || x >= width || y >= height) {
                    continue;
                }
                if (!allowDiagonal && x != targetX && y != targetY) {
                    continue;
                }
                yield return nodes[x, y];
            }
        }
    }
}
