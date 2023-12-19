namespace csteeves.Advent2023;

public class DigSite {

    private readonly long width;
    private readonly long height;
    private readonly HashSet<Vector2Int> digPerimeter;

    public DigSite(List<DigInstruction> instructions) {
        int minX = 0;
        int maxX = 0;
        int minY = 0;
        int maxY = 0;

        Vector2Int position = new Vector2Int(0, 0);

        foreach (DigInstruction currentInstruction in instructions) {
            position = currentInstruction.GetNextPosition(position);
            minX = Math.Min(minX, position.x);
            maxX = Math.Max(maxX, position.x);
            minY = Math.Min(minY, position.y);
            maxY = Math.Max(maxY, position.y);
        }

        width = Math.Abs(maxX - minX + 1);
        height = Math.Abs(maxY - minY + 1);
        int xOffset = -1 * minX;
        int yOffset = -1 * minY;

        position = new Vector2Int(xOffset, yOffset);
        digPerimeter = [position];

        foreach (DigInstruction currentInstruction in instructions) {
            Vector2Int nextPosition = currentInstruction.GetNextPosition(position);
            foreach (Vector2Int stepPosition in GetSteps(position, nextPosition)) {
                digPerimeter.Add(new Vector2Int(stepPosition.x, stepPosition.y));
            }
            position = nextPosition;
        }
    }

    private static IEnumerable<Vector2Int> GetSteps(Vector2Int current, Vector2Int next) {
        if (current.x == next.x) {
            int yStep = current.y < next.y ? 1 : -1;
            for (int y = current.y + yStep; y != next.y; y += yStep) {
                yield return new Vector2Int(next.x, y);
            }
            yield return new Vector2Int(next);
            yield break;
        }

        if (current.y == next.y) {
            int xStep = current.x < next.x ? 1 : -1;
            for (int x = current.x + xStep; x != next.x; x += xStep) {
                yield return new Vector2Int(x, next.y);
            }
            yield return new Vector2Int(next);
            yield break;
        }

        throw new ArgumentOutOfRangeException();
    }

    public long GetInteriorSize() {
        return (width * height) - GetExteriorSize();
    }

    public long GetExteriorSize() {
        long count = 0;
        HashSet<Vector2Int> visited = [];
        foreach (Vector2Int coord in GetDigSitePerimeter()) {
            count += MeasureExterior(coord, visited);
        }
        return count;
    }

    private IEnumerable<Vector2Int> GetDigSitePerimeter() {
        for (int x = 0; x < width; x++) {
            yield return new Vector2Int(x, 0);
            yield return new Vector2Int(x, (int)height - 1);
        }
        for (int y = 1; y < height - 1; y++) {
            yield return new Vector2Int(0, y);
            yield return new Vector2Int((int)width - 1, y);
        }
    }

    private long MeasureExterior(Vector2Int start, HashSet<Vector2Int> visited) {
        if (digPerimeter.Contains(start) || visited.Contains(start)) {
            return 0;
        }

        int result = 1;
        visited.Add(start);
        Queue<Vector2Int> neighbors = [];
        foreach (Vector2Int neighbor in GetNeighbors(start)) {
            neighbors.Enqueue(neighbor);
        }

        while (neighbors.Any()) {
            Vector2Int current = neighbors.Dequeue();
            if (digPerimeter.Contains(current) || visited.Contains(current)) {
                continue;
            }

            result++;
            visited.Add(current);
            foreach (Vector2Int neighbor in GetNeighbors(current)) {
                neighbors.Enqueue(neighbor);
            }
        }

        return result;
    }

    private IEnumerable<Vector2Int> GetNeighbors(Vector2Int coord) {
        for (int x = coord.x - 1; x <= coord.x + 1; x++) {
            for (int y = coord.y - 1; y <= coord.y + 1; y++) {
                // Don't return self
                if (x == coord.x && y == coord.y) {
                    continue;
                }

                // Don't return diagonals
                if (x != coord.x && y != coord.y) {
                    continue;
                }

                // Don't return off of the dig site
                if (x < 0 || x >= width || y < 0 || y >= height) {
                    continue;
                }

                yield return new Vector2Int(x, y);
            }
        }
    }
}
