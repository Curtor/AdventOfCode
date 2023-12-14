namespace csteeves.Advent2023;

public class ParabolicReflectorDish : DaySolution2023 {

    private static Dictionary<string, Tuple<int, Grid<char>>> cycleCache = [];

    public override string Dir() {
        return "Day 14";
    }

    public override void Part1(List<string> input) {
        Grid<char> grid = CreateGrid(input);

        Console.WriteLine("Starting Grid:");
        grid.PrettyPrint();
        Console.WriteLine();

        MoveRocksNorth(grid);

        Console.WriteLine("Done Moving:");
        grid.PrettyPrint();
        Console.WriteLine();

        long load = CalculateNorthLoad(grid);
        Console.WriteLine($"Grid load: {load}");
    }

    public override void Part2(List<string> input) {
        Grid<char> grid = CreateGrid(input);

        Console.WriteLine("Starting Grid:");
        grid.PrettyPrint();
        Console.WriteLine();

        int cycles = 1_000_000_000;
        int currentCycle = 0;
        int milestoneSpacing = 10_000;
        int nextMilestone = 0;
        while (currentCycle < cycles) {
            string gridKey = grid.GetKey();

            Tuple<int, Grid<char>> result = AttemptJumpAhead(currentCycle, grid, cycles);

            grid = result.Item2;
            int moveAhead = result.Item1;
            currentCycle += moveAhead;

            if (currentCycle == cycles) {
                break;
            }

            MoveRocksNorth(grid);
            MoveRocksWest(grid);
            MoveRocksSouth(grid);
            MoveRocksEast(grid);

            moveAhead++;
            currentCycle++;
            Grid<char> copy = new Grid<char>(grid);

            if (currentCycle + moveAhead < cycles &&
                (!cycleCache.TryGetValue(gridKey, out Tuple<int, Grid<char>>? cachedGrid)
                    || cachedGrid.Item1 < moveAhead
                    || cachedGrid.Item1 + currentCycle > cycles)) {
                cycleCache[gridKey] = Tuple.Create(moveAhead, copy);
            }

            if (currentCycle > nextMilestone) {
                float percent = currentCycle / (float)cycles * 100;
                Console.WriteLine($"{currentCycle}/{cycles} ({percent}%)");
                nextMilestone += milestoneSpacing;
            }
        }

        Console.WriteLine("Done Moving:");
        grid.PrettyPrint();
        Console.WriteLine();

        long load = CalculateNorthLoad(grid);
        Console.WriteLine($"Grid load: {load}");

        // 93847: Too high
    }

    private Tuple<int, Grid<char>> AttemptJumpAhead(int cycle, Grid<char> grid, int maxCycles) {
        int moveAhead = 0;
        string originalGridKey = grid.GetKey();
        string gridKey = originalGridKey;

        while (cycleCache.TryGetValue(gridKey, out Tuple<int, Grid<char>>? cachedGrid)) {
            if (cycle + moveAhead + cachedGrid.Item1 > maxCycles) {
                break;
            }

            moveAhead += cachedGrid.Item1;
            grid = cachedGrid.Item2;
            gridKey = grid.GetKey();

            if (gridKey.Equals(originalGridKey) && cycle + moveAhead + moveAhead < maxCycles) {
                cycleCache[gridKey] = Tuple.Create(moveAhead, grid);
            }
        }

        return Tuple.Create(moveAhead, grid);
    }

    private static Grid<char> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<char> grid = new Grid<char>(width, height);
        grid.SetValues(node => input[node.coord.y][node.coord.x]);
        grid.SetNeighbors((node1, node2) => true);
        return grid;
    }

    private void MoveRocksNorth(Grid<char> grid) {
        for (int x = 0; x < grid.width; x++) {
            for (int y = 0; y < grid.height; y++) {
                GridNode<char> node = grid.GetNode(x, y);
                if (node.value == 'O') {
                    MoveRockNorth(node, grid);
                }
            }
        }
    }

    private void MoveRockNorth(GridNode<char> node, Grid<char> grid) {
        while (node.coord.y > 0) {
            GridNode<char> northNode = grid.GetNode(node.coord.x, node.coord.y - 1);
            if (!TryMoveNode(ref node, northNode)) {
                return;
            }
        }
    }

    private void MoveRocksWest(Grid<char> grid) {
        for (int x = 0; x < grid.width; x++) {
            for (int y = 0; y < grid.height; y++) {
                GridNode<char> node = grid.GetNode(x, y);
                if (node.value == 'O') {
                    MoveRockWest(node, grid);
                }
            }
        }
    }

    private void MoveRockWest(GridNode<char> node, Grid<char> grid) {
        while (node.coord.x > 0) {
            GridNode<char> westNode = grid.GetNode(node.coord.x - 1, node.coord.y);
            if (!TryMoveNode(ref node, westNode)) {
                return;
            }
        }
    }

    private void MoveRocksSouth(Grid<char> grid) {
        for (int x = 0; x < grid.width; x++) {
            for (int y = grid.height - 1; y >= 0; y--) {
                GridNode<char> node = grid.GetNode(x, y);
                if (node.value == 'O') {
                    MoveRockSouth(node, grid);
                }
            }
        }
    }

    private void MoveRockSouth(GridNode<char> node, Grid<char> grid) {
        while (node.coord.y < grid.height - 1) {
            GridNode<char> southNode = grid.GetNode(node.coord.x, node.coord.y + 1);
            if (!TryMoveNode(ref node, southNode)) {
                return;
            }
        }
    }

    private void MoveRocksEast(Grid<char> grid) {
        for (int x = grid.width - 1; x >= 0; x--) {
            for (int y = 0; y < grid.height; y++) {
                GridNode<char> node = grid.GetNode(x, y);
                if (node.value == 'O') {
                    MoveRockEast(node, grid);
                }
            }
        }
    }

    private void MoveRockEast(GridNode<char> node, Grid<char> grid) {
        while (node.coord.x < grid.width - 1) {
            GridNode<char> eastNode = grid.GetNode(node.coord.x + 1, node.coord.y);
            if (!TryMoveNode(ref node, eastNode)) {
                return;
            }
        }
    }

    private static bool TryMoveNode(ref GridNode<char> node, GridNode<char> destination) {
        if (destination.value != '.') {
            return false;
        }

        destination.value = 'O';
        node.value = '.';
        node = destination;
        return true;
    }

    private long CalculateNorthLoad(Grid<char> grid) {
        long load = 0;
        for (int y = 0; y < grid.height; y++) {
            for (int x = 0; x < grid.width; x++) {
                GridNode<char> node = grid.GetNode(x, y);
                if (node.value == 'O') {
                    load += grid.height - y;
                }
            }
        }
        return load;
    }
}