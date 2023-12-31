namespace csteeves.Advent2023;

public class GearRatios : DaySolution2023 {

    public override string Dir() {
        return "Day 03";
    }

    public override void Part1(List<string> input) {
        Grid<char> grid = CreateGrid(input);

        int sum = 0;
        HashSet<GridNode<char>> visitedPartNumbers = [];

        foreach (GridNode<char> node in grid.AllNodes()) {
            if (!IsSymbol(node.value)) {
                continue;
            }

            foreach (int partNumber in GetAdjacentPartNumbers(grid, node, visitedPartNumbers)) {
                Console.WriteLine($"Part number: {partNumber}");
                sum += partNumber;
            }
        }

        Console.WriteLine($"Part numbers sum: {sum}");
    }

    public override void Part2(List<string> input) {
        Grid<char> grid = CreateGrid(input);

        int sum = 0;
        foreach (GridNode<char> node in grid.AllNodes()) {
            if (node.value != '*') {
                continue;
            }

            List<int> adjacentPartNumbers = GetAdjacentPartNumbers(grid, node).ToList();
            if (adjacentPartNumbers.Count == 2) {
                int ratio0 = adjacentPartNumbers[0];
                int rato1 = adjacentPartNumbers[1];
                Console.WriteLine($"Gear ratio: {ratio0}:{rato1}");
                sum += ratio0 * rato1;
            }
        }

        Console.WriteLine($"Gear ratios sum: {sum}");
    }

    public Grid<char> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<char> grid = new Grid<char>(width, height, true);
        grid.SetValues(node => input[node.coord.y][node.coord.x]);
        grid.SetNeighbors(IsNeighbor);
        return grid;
    }

    private bool IsNeighbor(GridNode<char> node, GridNode<char> adjacent) {
        return node.value != '.' && adjacent.value != '.';
    }

    private bool IsSymbol(char c) {
        return !char.IsAsciiDigit(c) && c != '.';
    }

    /** 
     * If visited is not null, then will ignore part numbers where the node of those numbers are
     * contained within previously visited nodes.
     */
    private IEnumerable<int> GetAdjacentPartNumbers(
             Grid<char> grid,
             GridNode<char> node,
             HashSet<GridNode<char>>? visitedPartNumbers = null) {

        // Initialize the visited set and use it anyway if null
        // to ensure we don't return the same part number twice
        // if multiple nodes of the same number are adjacent.
        if (visitedPartNumbers == null) {
            visitedPartNumbers = [];
        }

        foreach (NodeNeighbor<GridNode<char>, char> neighbor in node.Neighbors) {
            if (!char.IsAsciiDigit(neighbor.neighbor.value)
                    || visitedPartNumbers.Contains(neighbor.neighbor)) {
                continue;
            }

            yield return BuildPartNumber(grid, neighbor.neighbor, visitedPartNumbers);
        }
    }

    /**
     * Starting at a given node within the part number, walk left until we get to the start of the
     * part number, and then build the number by walking back to the right.
     */
    private int BuildPartNumber(
            Grid<char> grid,
            GridNode<char> node,
            HashSet<GridNode<char>> visitedPartNumbers) {

        GridNode<char>? leftNeighbor = GetLeftNeighbor(grid, node);
        while (leftNeighbor != null && char.IsAsciiDigit(leftNeighbor.value)) {
            node = leftNeighbor;
            leftNeighbor = GetLeftNeighbor(grid, node);
        }

        GridNode<char>? currentNode = node;
        string partNumberString = "";
        do {
            partNumberString += currentNode.value.ToString();
            visitedPartNumbers.Add(currentNode);
            currentNode = GetRightNeighbor(grid, currentNode);
        } while (currentNode != null && char.IsAsciiDigit(currentNode.value));

        return int.Parse(partNumberString);
    }

    private GridNode<char>? GetLeftNeighbor(Grid<char> grid, GridNode<char> node) {
        if (node.coord.x == 0) {
            return null;
        }
        return grid.GetNode(node.coord.x - 1, node.coord.y);
    }

    private GridNode<char>? GetRightNeighbor(Grid<char> grid, GridNode<char> node) {
        if (node.coord.x == grid.width - 1) {
            return null;
        }
        return grid.GetNode(node.coord.x + 1, node.coord.y);
    }
}