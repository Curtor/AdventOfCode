namespace csteeves.Advent2023;

public class ClumsyCrucible : DaySolution2023 {

    public override string Dir() {
        return "Day 17";
    }

    public override void Part1(List<string> input) {
        Grid<int> grid = CreateGrid(input);
        int heatLoss = GetCrucibleRouteHeatLoss(grid, 1, 3);
        Console.WriteLine($"Route heat loss: {heatLoss}");
    }

    public override void Part2(List<string> input) {
        Grid<int> grid = CreateGrid(input);
        int heatLoss = GetCrucibleRouteHeatLoss(grid, 4, 10);
        Console.WriteLine($"Route heat loss: {heatLoss}");
    }

    private static Grid<int> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<int> grid = new Grid<int>(width, height);
        grid.SetValues(node => int.Parse(input[node.coord.y][node.coord.x].ToString()));
        grid.SetNeighbors((node1, node2) => true);
        return grid;
    }

    private int GetCrucibleRouteHeatLoss(Grid<int> grid, int minLegLength, int maxLegLength) {
        CrucibleMove.minLineMoveLength = minLegLength;
        CrucibleMove.maxLineMoveLength = maxLegLength;

        GridNode<int> currentNode = grid.GetNode(0, 0);
        GridNode<int> targetNode = grid.GetNode(grid.width - 1, grid.height - 1);
        CrucibleMove startMove = new CrucibleMove(0, currentNode, null);

        PriorityQueue<CrucibleMove> queue = new();
        HashSet<CrucibleMove> visited = [];

        EnqueueNextMoves(startMove, queue, visited, targetNode);

        while (queue.Any()) {
            CrucibleMove currentMove = queue.Dequeue();
            if (currentMove.current == targetNode && currentMove.LegLength >= minLegLength) {
                //PrettyPrintPath(grid, currentMove);
                return currentMove.accumulatedHeat;
            }
            EnqueueNextMoves(currentMove, queue, visited, targetNode);
        }

        throw new ArgumentOutOfRangeException();
    }

    private void EnqueueNextMoves(
            CrucibleMove currentMove,
            PriorityQueue<CrucibleMove> queue,
            HashSet<CrucibleMove> visited,
            GridNode<int> targetNode) {
        foreach (NodeNeighbor<GridNode<int>, int> nodeNeighbor
                in currentMove.current.NextNodes()) {

            GridNode<int> nextNode = nodeNeighbor.neighbor;
            if (!currentMove.ValidNextMove(nextNode)) {
                continue;
            }

            CrucibleMove nextMove = new CrucibleMove(
                currentMove.accumulatedHeat + nextNode.value, nextNode, currentMove);
            if (visited.Contains(nextMove)) {
                continue;
            }

            int distance = Math.Abs(targetNode.coord.x - nextNode.coord.x)
                + Math.Abs(targetNode.coord.y - nextNode.coord.y);
            int heuristic = nextMove.accumulatedHeat + distance;

            queue.Enqueue(heuristic, nextMove);
            visited.Add(nextMove);
        }
    }

    private void PrettyPrintPath(Grid<int> grid, CrucibleMove? currentMove) {
        Grid<string> pathGrid = new Grid<string>(grid.width, grid.height);
        pathGrid.SetValues(node => grid.GetNode(node.coord.x, node.coord.y).value.ToString());

        while (currentMove != null) {
            string? directionString = GetDirectionString(currentMove);
            if (directionString != null) {
                Vector2Int coord = currentMove.current.coord;
                pathGrid.GetNode(coord.x, coord.y).value = directionString;
            }

            currentMove = currentMove.previous;
        }

        pathGrid.PrettyPrint();
    }

    private string? GetDirectionString(CrucibleMove move) {
        if (move.previous == null) {
            return null;
        }

        GridNode<int> current = move.current;
        GridNode<int> previous = move.previous.current;

        if (current.coord.x == previous.coord.x) {
            if (current.coord.y == previous.coord.y + 1) {
                return "v";
            }
            if (current.coord.y == previous.coord.y - 1) {
                return "^";
            }
        }

        if (current.coord.y == previous.coord.y) {
            if (current.coord.x == previous.coord.x + 1) {
                return ">";
            }
            if (current.coord.x == previous.coord.x - 1) {
                return "<";
            }
        }

        throw new ArgumentOutOfRangeException();
    }
}