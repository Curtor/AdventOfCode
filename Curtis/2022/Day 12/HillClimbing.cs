namespace csteeves.Advent2022;

using Directions = Directions<Move<GridNode<HillPoint>, HillPoint>, GridNode<HillPoint>, HillPoint>;

public class HillClimbing : DaySolution2022 {

    public override string Dir() {
        return "Day 12";
    }

    public override void Part1(List<string> input) {
        Grid<HillPoint> grid = CreateGrid(input);
        grid.SetNeighbors(IsNeighbor);

        GridNode<HillPoint> start = grid.AllNodes().First(n => n.value.isStart);
        Directions directions = start.GetRoute(grid.AllNodes().Where(n => n.value.isEnd));
        int stepCount = directions.Steps.Count();

        Console.WriteLine($"Start Steps: {stepCount}");
    }

    public override void Part2(List<string> input) {
        Grid<HillPoint> grid = CreateGrid(input);
        grid.SetNeighbors(IsReverseNeighbor);

        GridNode<HillPoint> start = grid.AllNodes().First(n => n.value.isEnd);
        Directions directions = start.GetRoute(grid.AllNodes().Where(n => n.value.raw == 'a'));
        int stepCount = directions.Steps.Count();

        Console.WriteLine($"Min Steps: {stepCount}");
    }

    private Grid<HillPoint> CreateGrid(List<string> input) {
        Grid<HillPoint> grid = new Grid<HillPoint>(input[0].Length, input.Count);
        grid.SetValues(coord => GetValue(input, coord));
        return grid;
    }

    private HillPoint GetValue(List<string> input, GridNode<HillPoint> node) {
        return new HillPoint(input[node.coord.y][node.coord.x]);
    }

    private bool IsNeighbor(GridNode<HillPoint> node, GridNode<HillPoint> neighbor) {
        bool result = node.value.height - neighbor.value.height >= -1;
        return result;
    }

    private bool IsReverseNeighbor(GridNode<HillPoint> node, GridNode<HillPoint> neighbor) {
        bool result = node.value.height - neighbor.value.height <= 1;
        return result;
    }
}