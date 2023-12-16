

namespace csteeves.Advent2023;

public class TheFloorWillBeLava : DaySolution2023 {

    public override string Dir() {
        return "Day 16";
    }

    public override void Part1(List<string> input) {
        Grid<LightTile> grid = CreateGrid(input);
        int energizedTiles = GetEnergizationStartingAt(grid, 0, 0, LightTile.Direction.RIGHT);
        Console.WriteLine($"Energized tiles: {energizedTiles}");
    }

    public override void Part2(List<string> input) {
        Grid<LightTile> grid = CreateGrid(input);

        int maxEnergizedTiles = 0;
        for (int x = 0; x < grid.width; x++) {
            int energizedTiles = GetEnergizationStartingAt(grid, x, 0, LightTile.Direction.DOWN);
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles);
            ResetGrid(grid);

            energizedTiles =
                GetEnergizationStartingAt(grid, x, grid.height - 1, LightTile.Direction.UP);
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles);
            ResetGrid(grid);
        }

        for (int y = 0; y < grid.height; y++) {
            int energizedTiles = GetEnergizationStartingAt(grid, 0, y, LightTile.Direction.RIGHT);
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles);
            ResetGrid(grid);

            energizedTiles =
                GetEnergizationStartingAt(grid, grid.width - 1, y, LightTile.Direction.LEFT);
            maxEnergizedTiles = Math.Max(maxEnergizedTiles, energizedTiles);
            ResetGrid(grid);
        }

        Console.WriteLine($"Max energized tiles: {maxEnergizedTiles}");
    }
    private static Grid<LightTile> CreateGrid(List<string> input) {
        int width = input[0].Length;
        int height = input.Count;

        Grid<LightTile> grid = new Grid<LightTile>(width, height);
        grid.SetValues(node => new LightTile(input[node.coord.y][node.coord.x]));
        grid.SetNeighbors((node1, node2) => true);
        return grid;
    }

    private void ResetGrid(Grid<LightTile> grid) {
        foreach (GridNode<LightTile> node in grid.AllNodes()) {
            node.value.Reset();
        }
    }

    private int GetEnergizationStartingAt(
            Grid<LightTile> grid, int x, int y, LightTile.Direction startDirection) {

        GridNode<LightTile> startingNode = grid.GetNode(x, y);
        foreach (LightTile.Direction lightDirection
                in startingNode.value.AddAndFollowLight(startDirection)) {
            FollowLightToNextTile(grid, startingNode, lightDirection);
        }

        int energizedTiles = 0;
        foreach (GridNode<LightTile> node in grid.AllNodes()) {
            if (node.value.ContainsLight()) {
                energizedTiles++;
            }
        }

        return energizedTiles;
    }

    private void FollowLightToNextTile(
            Grid<LightTile> grid, GridNode<LightTile> node, LightTile.Direction lightDirection) {

        GridNode<LightTile>? nextNode = GetNextNode(grid, node, lightDirection);
        if (nextNode == null) {
            return;
        }

        foreach (LightTile.Direction nextLightDirection
                in nextNode.value.AddAndFollowLight(lightDirection)) {
            FollowLightToNextTile(grid, nextNode, nextLightDirection);
        }
    }

    private GridNode<LightTile>? GetNextNode(
            Grid<LightTile> grid, GridNode<LightTile> node, LightTile.Direction direction) {
        switch (direction) {
            case LightTile.Direction.RIGHT:
                return node.coord.x == grid.width - 1
                    ? null : grid.GetNode(node.coord.x + 1, node.coord.y);
            case LightTile.Direction.DOWN:
                return node.coord.y == grid.height - 1
                    ? null : grid.GetNode(node.coord.x, node.coord.y + 1);
            case LightTile.Direction.LEFT:
                return node.coord.x == 0
                    ? null : grid.GetNode(node.coord.x - 1, node.coord.y);
            case LightTile.Direction.UP:
                return node.coord.y == 0
                    ? null : grid.GetNode(node.coord.x, node.coord.y - 1);
        }

        throw new ArgumentOutOfRangeException();
    }
}