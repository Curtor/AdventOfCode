namespace csteeves.Advent2021;

public class HydrothermalVenture : DaySolution2021 {

    public override string Dir() {
        return "Day 05";
    }

    public override void Part1(List<string> input) {
        int answer = GetAnswer(input, false);
        Console.WriteLine($"Overlaps: {answer}");
    }

    public override void Part2(List<string> input) {
        int answer = GetAnswer(input, true);
        Console.WriteLine($"Overlaps: {answer}");
    }

    private int GetAnswer(List<string> input, bool includeDiagonal) {
        Vector2Int maxCoords = GetMaxCoords(input);

        int[,] grid = new int[maxCoords.x + 1, maxCoords.y + 1];
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                grid[y, x] = 0;
            }
        }

        foreach (string inputLine in input) {
            Line line = GetLine(inputLine);
            if (!includeDiagonal && line.isDiagonal()) {
                continue;
            }
            AddLineToGrid(grid, line);
        }

        int overlaps = 0;
        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                if (grid[y, x] > 1) {
                    overlaps++;
                }
            }
        }

        // PrettyPrint(grid);

        return overlaps;
    }

    private Vector2Int GetMaxCoords(List<string> input) {
        Vector2Int result = new Vector2Int(0, 0);

        foreach (string inputLine in input) {
            Line line = GetLine(inputLine);
            if (line.pointA.x > result.x) {
                result.x = line.pointA.x;
            }
            if (line.pointB.x > result.x) {
                result.x = line.pointB.x;
            }
            if (line.pointA.y > result.y) {
                result.y = line.pointA.y;
            }
            if (line.pointB.y > result.y) {
                result.y = line.pointB.y;
            }
        }

        return result;
    }

    private Line GetLine(string inputLine) {
        List<string> tokens = LineParser.Tokens(inputLine, " -> ");
        List<string> coordsA = LineParser.Tokens(tokens[0], ",");
        List<string> coordsB = LineParser.Tokens(tokens[1], ",");

        return new Line(
            int.Parse(coordsA[0]),
            int.Parse(coordsA[1]),
            int.Parse(coordsB[0]),
            int.Parse(coordsB[1]));
    }

    private void AddLineToGrid(int[,] grid, Line line) {
        int xDelta = line.pointB.x - line.pointA.x;
        int yDelta = line.pointB.y - line.pointA.y;
        int maxDelta = Math.Max(Math.Abs(xDelta), Math.Abs(yDelta));

        for (int step = 0; step <= maxDelta; step++) {
            int x = line.pointA.x + (step * xDelta / maxDelta);
            int y = line.pointA.y + (step * yDelta / maxDelta);
            grid[y, x]++;
        }
    }

    private void PrettyPrint(int[,] grid) {
        int height = grid.GetLength(0);
        int width = grid.GetLength(1);

        Console.WriteLine();

        for (int y = 0; y < height; y++) {
            for (int x = 0; x < width; x++) {
                Console.Write(grid[y, x]);
            }
            Console.WriteLine();
        }

        Console.WriteLine();
    }
}