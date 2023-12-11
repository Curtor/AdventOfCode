


namespace csteeves.Advent2023;

public class CosmicExpansion : DaySolution2023 {

    public override string Dir() {
        return "Day 11";
    }

    public override void Part1(List<string> input) {
        long shortestPathesSum = GetShortestPathSums(input, 2);
        Console.WriteLine($"Shorter path sums: {shortestPathesSum}");
    }

    public override void Part2(List<string> input) {
        long shortestPathesSum = GetShortestPathSums(input, 1000000);
        Console.WriteLine($"Larger Path sums: {shortestPathesSum}");
    }

    private long GetShortestPathSums(List<string> input, long expansionDistance) {
        Grid<bool> grid = CreateGrid(input);

        HashSet<int> expansionRows = GetExpansionRows(grid);
        HashSet<int> expansionCols = GetExpansionCols(grid);

        List<Vector2Int> galaxies =
            grid.AllNodes().Where(n => n.value).Select(n => n.coord).ToList();

        long shortestPathesSum = 0;
        for (int i = 0; i < galaxies.Count - 1; i++) {
            Vector2Int startGalaxy = galaxies[i];
            for (int j = i + 1; j < galaxies.Count; j++) {
                Vector2Int endGalaxy = galaxies[j];
                shortestPathesSum += GalaxyDistance(
                    startGalaxy, endGalaxy, expansionRows, expansionCols, expansionDistance);
            }
        }

        return shortestPathesSum;
    }

    private static Grid<bool> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<bool> grid = new Grid<bool>(width, height);
        grid.SetValues(node => input[node.coord.y][node.coord.x] == '#');
        return grid;
    }

    private static HashSet<int> GetExpansionRows(Grid<bool> grid) {
        HashSet<int> expansionRows = [];
        for (int y = 0; y < grid.height; y++) {
            bool expansionRow = true;
            for (int x = 0; x < grid.width; x++) {
                if (grid.GetNode(x, y).value) {
                    expansionRow = false;
                    break;
                }
            }
            if (expansionRow) {
                expansionRows.Add(y);
            }
        }
        return expansionRows;
    }

    private static HashSet<int> GetExpansionCols(Grid<bool> grid) {
        HashSet<int> expansionCols = [];
        for (int x = 0; x < grid.width; x++) {
            bool expansionCol = true;
            for (int y = 0; y < grid.height; y++) {
                if (grid.GetNode(x, y).value) {
                    expansionCol = false;
                    break;
                }
            }
            if (expansionCol) {
                expansionCols.Add(x);
            }
        }
        return expansionCols;
    }

    private long GalaxyDistance(
            Vector2Int startGalaxy,
            Vector2Int endGalaxy,
            HashSet<int> expansionRows,
            HashSet<int> expansionCols,
            long expansionDistance) {

        int startX = Math.Min(startGalaxy.x, endGalaxy.x);
        int startY = Math.Min(startGalaxy.y, endGalaxy.y);
        int endX = Math.Max(startGalaxy.x, endGalaxy.x);
        int endY = Math.Max(startGalaxy.y, endGalaxy.y);

        int extraRows = 0;
        for (int row = startY + 1; row < endY; row++) {
            if (expansionRows.Contains(row)) {
                extraRows++;
            }
        }

        int extraCols = 0;
        for (int col = startX + 1; col < endX; col++) {
            if (expansionCols.Contains(col)) {
                extraCols++;
            }
        }

        long xDistance = endX - startX + (extraCols * (expansionDistance - 1));
        long yDistance = endY - startY + (extraRows * (expansionDistance - 1));
        return xDistance + yDistance;
    }
}