namespace csteeves.Advent2022;

using Directions = Directions<Move<GridNode<HillPoint>, HillPoint>, GridNode<HillPoint>, HillPoint>;

public class HillClimbing : DaySolution2022 {

    private const string dir = "Day 12";

    public override string Dir() {
        return dir;
    }

    public override void Run(List<string> input) {
        Grid<HillPoint> grid = new Grid<HillPoint>(input[0].Length, input.Count);
        grid.SetValues(coord => GetValue(input, coord));
        grid.SetNeighbors(IsNeighbor);

        GridNode<HillPoint> start = grid.AllNodes().First(n => n.value.isStart);
        Directions directions = start.GetRoute(grid.AllNodes().Where(n => n.value.isEnd));
        int stepCount = directions.Steps.Count();

        Console.WriteLine($"Start Steps: {stepCount}");

        grid.SetNeighbors(IsReverseNeighbor);

        start = grid.AllNodes().First(n => n.value.isEnd);
        directions = start.GetRoute(grid.AllNodes().Where(n => n.value.raw == 'a'));
        stepCount = directions.Steps.Count();

        Console.WriteLine($"Min Steps: {stepCount}");

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