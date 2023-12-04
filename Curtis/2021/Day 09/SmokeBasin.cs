namespace csteeves.Advent2021;

using csteeves.AdventLibrary;

public class SmokeBasin : DaySolution2021 {

    private Dictionary<int, long> reproduction = [];

    public override string Dir() {
        return "Day 09";
    }

    public override void Part1(List<string> input) {
        Grid<int> grid = CreateGrid(input);
        List<GridNode<int>> lowPoints = GetLowPoints(grid);

        string lowPointsToString = lowPoints.Select(n => n.value).Join(",");
        Console.WriteLine($"Low points: {lowPointsToString}");

        List<int> riskLevels = lowPoints.Select(n => n.value + 1).ToList();

        int sum = riskLevels.Sum();
        Console.WriteLine($"Sum: {sum}");
    }

    public override void Part2(List<string> input) {
        Grid<int> grid = CreateGrid(input);
        List<GridNode<int>> lowPoints = GetLowPoints(grid);

        List<int> basinSizes = [];
        foreach (GridNode<int> lowPoint in lowPoints) {
            int basinSize = GetBasinSize(lowPoint);
            basinSizes.Add(basinSize);
        }

        string basinSizesString = basinSizes.Join(",");
        Console.WriteLine($"Basin sizes: {basinSizesString}");

        basinSizes.Sort();
        basinSizes.Reverse();

        int product = basinSizes[0] * basinSizes[1] * basinSizes[2];
        Console.WriteLine($"Product: {product}");

    }

    public Grid<int> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<int> grid = new Grid<int>(width, height);
        grid.SetValues(node => int.Parse(input[node.coord.y][node.coord.x].ToString()));
        grid.SetNeighbors((node, neighbor) => node.value < neighbor.value);
        return grid;
    }

    private List<GridNode<int>> GetLowPoints(Grid<int> grid) {
        return grid.AllNodes().Where(node => HasMaxNeighbors(grid, node)).ToList();
    }

    private bool HasMaxNeighbors(Grid<int> grid, GridNode<int> node) {
        int maxNeighbors = 4;

        if (node.coord.x == 0 || node.coord.x == grid.width - 1) {
            maxNeighbors--;
        }

        if (node.coord.y == 0 || node.coord.y == grid.height - 1) {
            maxNeighbors--;
        }

        return node.Neighbors.Count == maxNeighbors;
    }

    private int GetBasinSize(GridNode<int> lowPoint) {
        int basinSize = 0;
        foreach (GridNode<int> node in lowPoint.TraverseBreadthFirst()) {
            if (node.value < 9) {
                basinSize++;
            }
        }
        return basinSize;
    }
}