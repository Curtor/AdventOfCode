using System.Text;

namespace csteeves;

public class Grid {
    public enum Direction { LEFT, RIGHT, UP, DOWN }

    public static Direction DirectionFrom(char token) {
        switch (token) {
            case 'l':
            case 'L':
            case '<':
                return Direction.LEFT;
            case 'r':
            case 'R':
            case '>':
                return Direction.RIGHT;
            case 'u':
            case 'U':
            case '^':
                return Direction.UP;
            case 'd':
            case 'D':
            case 'v':
            case 'V':
                return Direction.DOWN;
        }

        throw new ArgumentOutOfRangeException();
    }
}

public class Grid<T> {

    public readonly int width;
    public readonly int height;
    public readonly bool allowDiagonal;

    private readonly GridNode<T>[,] nodes;

    private string key;

    public Grid(int width, int height, bool allowDiag = false) {
        this.width = width;
        this.height = height;
        this.allowDiagonal = allowDiag;

        nodes = new GridNode<T>[width, height];
        InitializeNodes();
    }

    public Grid(Grid<T> grid) {
        width = grid.width;
        height = grid.height;
        allowDiagonal = grid.allowDiagonal;

        nodes = new GridNode<T>[width, height];
        foreach (GridNode<T> node in grid.AllNodes()) {
            nodes[node.coord.x, node.coord.y] = new GridNode<T>(node);
        }
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

    public IEnumerable<GridNode<T>> PerimeterNodes() {
        for (int x = 0; x < width; x++) {
            yield return nodes[x, 0];
            yield return nodes[x, height - 1];
        }
        for (int y = 1; y < height - 1; y++) {
            yield return nodes[0, y];
            yield return nodes[width - 1, y];
        }
    }

    public NodeNeighbor<GridNode<T>, T>? GetNeighbor(GridNode<T> node, Grid.Direction direction) {
        Vector2Int neighborCoord = new Vector2Int(node.coord);
        switch (direction) {
            case Grid.Direction.LEFT:
                neighborCoord.x--;
                break;
            case Grid.Direction.RIGHT:
                neighborCoord.x++;
                break;
            case Grid.Direction.UP:
                neighborCoord.y--;
                break;
            case Grid.Direction.DOWN:
                neighborCoord.y++;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        if (neighborCoord.x < 0 || neighborCoord.x >= width
            || neighborCoord.y < 0 || neighborCoord.y >= height) {
            return null;
        }

        return node.Neighbors.FirstOrDefault(n
            => n.neighbor.coord.Equals(neighborCoord), null);

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

    public void PrettyPrint() {
        PrettyPrint(node => node.value == null ? "." : node.value.ToString());
    }

    public void PrettyPrint(Func<GridNode<T>, string> toString) {
        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                GridNode<T> current = nodes[x, y];
                Console.Write($"{toString(current),-2}");
            }
            Console.WriteLine();
        }
    }

    public string GetKey() {
        if (string.IsNullOrEmpty(key)) {
            StringBuilder sb = new StringBuilder();
            foreach (GridNode<T> node in nodes) {
                sb.Append(node.value?.ToString());
            }
            key = sb.ToString();
        }

        return key;
    }
}
