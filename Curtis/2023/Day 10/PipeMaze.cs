namespace csteeves.Advent2023;

public class PipeMaze : DaySolution2023 {

    public override string Dir() {
        return "Day 10";
    }

    public override void Part1(List<string> input) {
        Grid<PipeNode> grid = CreateGrid(input);
        GridNode<PipeNode> start = GetStartNode(grid);

        List<GridNode<PipeNode>> neighbors = start.Neighbors.Select(n => n.neighbor).ToList();
        if (neighbors.Count != 2) {
            throw new ArgumentOutOfRangeException();
        }

        IEnumerator<GridNode<PipeNode>> firstPath =
            neighbors[0].TraverseDepthFirst([start]).GetEnumerator();
        IEnumerator<GridNode<PipeNode>> secondPath =
            neighbors[1].TraverseDepthFirst([start]).GetEnumerator();

        firstPath.MoveNext();
        secondPath.MoveNext();

        int steps = 1;
        while (firstPath.Current != secondPath.Current) {
            steps++;
            firstPath.MoveNext();
            if (firstPath.Current == secondPath.Current) {
                break;
            }
            secondPath.MoveNext();
        }

        Console.WriteLine($"Steps to furthest point in loop: {steps}");
    }

    public override void Part2(List<string> input) {
        Grid<PipeNode> grid = CreateGrid(input);
        GridNode<PipeNode> start = GetStartNode(grid);

        UpdateStartNode(start);

        foreach (GridNode<PipeNode> pipeNode in start.TraverseDepthFirst()) {
            pipeNode.value.onPath = true;
        }

        int enclosedTiles = 0;
        for (int x = 0; x < grid.width; x++) {

            int pathCrosses = 0;
            bool forcedLeft = false;
            bool forcedRight = false;
            for (int y = 0; y < grid.height; y++) {
                GridNode<PipeNode> pipeNode = grid.GetNode(x, y);

                if (!pipeNode.value.onPath && (y == 0 || y == grid.height - 1)) {
                    pipeNode.value.value = 'O';
                    continue;
                }

                if (pipeNode.value.onPath) {
                    if (pipeNode.value.value == 'F') {
                        forcedLeft = true;
                        forcedRight = false;
                    } else if (pipeNode.value.value == '7') {
                        forcedLeft = false;
                        forcedRight = true;
                    } else if (pipeNode.value.value == '-'
                        || (forcedLeft && pipeNode.value.value == 'J')
                        || (forcedRight && pipeNode.value.value == 'L')) {
                        pathCrosses++;
                    }
                    continue;
                }

                if (pathCrosses % 2 == 0) {
                    pipeNode.value.value = 'O';
                } else {
                    pipeNode.value.value = 'I';
                    enclosedTiles++;
                }
            }
        }

        // grid.PrettyPrint();
        // Console.WriteLine();
        Console.WriteLine($"Enclosed tiles: {enclosedTiles}");
    }

    private static Grid<PipeNode> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<PipeNode> grid = new Grid<PipeNode>(width, height);
        grid.SetValues(node => new PipeNode(input[node.coord.y][node.coord.x]));
        grid.SetNeighbors(IsNeighbor);
        return grid;
    }

    private static bool IsNeighbor(GridNode<PipeNode> node, GridNode<PipeNode> adjacent) {
        if (node.coord.x == adjacent.coord.x) {
            if (node.coord.y < adjacent.coord.y) {
                return ConnectedVertical(node.value.value, adjacent.value.value);
            } else if (node.coord.y > adjacent.coord.y) {
                return ConnectedVertical(adjacent.value.value, node.value.value);
            }
            throw new ArgumentOutOfRangeException();

        } else if (node.coord.y == adjacent.coord.y) {
            if (node.coord.x < adjacent.coord.x) {
                return ConnectedHorizontal(node.value.value, adjacent.value.value);
            } else if (node.coord.x > adjacent.coord.x) {
                return ConnectedHorizontal(adjacent.value.value, node.value.value);
            }
            throw new ArgumentOutOfRangeException();

        }
        throw new ArgumentOutOfRangeException();
    }

    private static bool ConnectedVertical(char above, char below) {
        return (above == 'S' || above == '|' || above == '7' || above == 'F')
            && (below == 'S' || below == '|' || below == 'J' || below == 'L');
    }

    private static bool ConnectedHorizontal(char left, char right) {
        return (left == 'S' || left == '-' || left == 'F' || left == 'L')
            && (right == 'S' || right == '-' || right == '7' || right == 'J');
    }

    private static GridNode<PipeNode> GetStartNode(Grid<PipeNode> grid) {
        for (int x = 0; x < grid.width; x++) {
            for (int y = 0; y < grid.height; y++) {
                if (grid.GetNode(x, y).value.value == 'S') {
                    return grid.GetNode(x, y);
                }
            }
        }

        throw new ArgumentOutOfRangeException();
    }

    private static void UpdateStartNode(GridNode<PipeNode> start) {
        List<GridNode<PipeNode>> neighbors = start.Neighbors.Select(n => n.neighbor).ToList();
        if (neighbors.Count != 2) {
            throw new ArgumentOutOfRangeException();
        }

        GridNode<PipeNode> firstNeighbor = neighbors[0];
        GridNode<PipeNode> secondNeighbor = neighbors[0];

        start.value.onPath = true;
        if (firstNeighbor.coord.x == start.coord.x) {
            if (secondNeighbor.coord.x == start.coord.x) {
                start.value.value = '|';
                return;
            }
        }

        if (firstNeighbor.coord.y == start.coord.y) {
            if (secondNeighbor.coord.y == start.coord.y) {
                start.value.value = '-';
                return;
            }
        }
    }
}