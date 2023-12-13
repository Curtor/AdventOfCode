namespace csteeves.Advent2023;

public class PointOfIncidence : DaySolution2023 {

    public override string Dir() {
        return "Day 13";
    }

    public override void Part1(List<string> input) {
        int reflectionScoreSum = 0;
        foreach (Grid<char> grid in GetGrids(input)) {
            int reflectionScore = GetReflectionScore(grid);
            reflectionScoreSum += reflectionScore;
        }

        Console.WriteLine($"Reflection score sums: {reflectionScoreSum}");
    }

    public override void Part2(List<string> input) {
        int reflectionScoreSum = 0;
        foreach (Grid<char> grid in GetGrids(input)) {
            int reflectionScore = GetReflectionScore(grid, true);
            reflectionScoreSum += reflectionScore;
        }

        Console.WriteLine($"Smudge reflection score sums: {reflectionScoreSum}");
    }

    private IEnumerable<Grid<char>> GetGrids(List<string> input) {
        List<string> gridLines = [];

        foreach (string line in input) {
            if (!string.IsNullOrEmpty(line)) {
                gridLines.Add(line);
                continue;
            }

            yield return CreateGrid(gridLines);
            gridLines = [];
        }

        yield return CreateGrid(gridLines);
    }

    private Grid<char> CreateGrid(List<string> gridLines) {
        int width = gridLines[0].Length;
        int height = gridLines.Count;

        Grid<char> grid = new Grid<char>(width, height);
        grid.SetValues(node => gridLines[node.coord.y][node.coord.x]);
        return grid;
    }

    private int GetReflectionScore(Grid<char> grid, bool fixSmudge = false) {
        int reflectionCol = GetHorizontalReflection(grid, fixSmudge);
        if (reflectionCol >= 0) {
            return reflectionCol + 1;
        }

        int reflectionRow = GetVerticalReflection(grid, fixSmudge) + 1;
        if (reflectionRow >= 0) {
            return 100 * reflectionRow;
        }

        throw new ArgumentException();
    }

    private int GetHorizontalReflection(Grid<char> grid, bool fixSmudge) {
        List<int> colHashes = [];
        for (int x = 0; x < grid.width; x++) {
            colHashes.Add(GetColValue(grid, x));
        }

        for (int i = 0; i < colHashes.Count - 1; i++) {
            if (isReflectionPoint(colHashes, i, fixSmudge)) {
                return i;
            }
        }

        return -1;
    }

    private int GetVerticalReflection(Grid<char> grid, bool fixSmudge) {
        List<int> rowHashes = [];
        for (int y = 0; y < grid.height; y++) {
            rowHashes.Add(GetRowValue(grid, y));
        }

        for (int i = 0; i < rowHashes.Count - 1; i++) {
            if (isReflectionPoint(rowHashes, i, fixSmudge)) {
                return i;
            }
        }

        return -1;
    }

    private int GetColValue(Grid<char> grid, int col) {
        int result = 0;
        for (int y = 0; y < grid.height; y++) {
            if (grid.GetNode(col, y).value == '#') {
                result |= 1 << y;
            }
        }
        return result;
    }

    private int GetRowValue(Grid<char> grid, int row) {
        int result = 0;
        for (int x = 0; x < grid.width; x++) {
            if (grid.GetNode(x, row).value == '#') {
                result |= 1 << x;
            }
        }
        return result;
    }

    private bool isReflectionPoint(List<int> rowHashes, int index, bool fixSmudge) {
        bool smudgeFound = false;
        for (int i = 0; i <= rowHashes.Count / 2; i++) {
            int firstIndex = index - i;
            int secondIndex = index + 1 + i;

            if (firstIndex < 0 || secondIndex >= rowHashes.Count) {
                return fixSmudge ? smudgeFound : true;
            }

            if (rowHashes[firstIndex] != rowHashes[secondIndex]) {
                if (!fixSmudge) {
                    return false;
                }
                if (!ReflectionFixable(rowHashes[firstIndex], rowHashes[secondIndex])) {
                    return false;
                }
                smudgeFound = true;
            }
        }

        return true;
    }

    private bool ReflectionFixable(int v1, int v2) {
        int xor = v1 ^ v2;
        return xor > 0 && (xor & (xor - 1)) == 0;
    }
}