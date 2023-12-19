
namespace csteeves.Advent2021;

public class BingoCard {

    private bool boardWon = false;
    private int[,] grid = new int[5, 5];
    private bool[,] marked = new bool[5, 5];

    public BingoCard(string row0, string row1, string row2, string row3, string row4) {
        List<int> row0Tokens = LineParser.Tokens(row0).Select(int.Parse).ToList();
        List<int> row1Tokens = LineParser.Tokens(row1).Select(int.Parse).ToList();
        List<int> row2Tokens = LineParser.Tokens(row2).Select(int.Parse).ToList();
        List<int> row3Tokens = LineParser.Tokens(row3).Select(int.Parse).ToList();
        List<int> row4Tokens = LineParser.Tokens(row4).Select(int.Parse).ToList();

        for (int col = 0; col < 5; col++) {
            grid[0, col] = row0Tokens[col];
            grid[1, col] = row1Tokens[col];
            grid[2, col] = row2Tokens[col];
            grid[3, col] = row3Tokens[col];
            grid[4, col] = row4Tokens[col];
        }

        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                marked[row, col] = false;
            }
        }
    }

    public bool StillPlaying() {
        return !boardWon;
    }

    public bool Call(int number) {
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                if (grid[row, col] == number) {
                    marked[row, col] = true;
                    if (Win(row, col)) {
                        boardWon = true;
                        return true;
                    }
                    return false;
                }
            }
        }

        return false;
    }

    private bool Win(int row, int col) {
        return WinRow(col) || WinCol(row);
    }

    private bool WinRow(int col) {
        for (int row = 0; row < 5; row++) {
            if (!marked[row, col]) {
                return false;
            }
        }
        return true;
    }

    private bool WinCol(int row) {
        for (int col = 0; col < 5; col++) {
            if (!marked[row, col]) {
                return false;
            }
        }
        return true;
    }

    public int GetUnmarkedSum() {
        int sum = 0;
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                if (!marked[row, col]) {
                    sum += grid[row, col];
                }
            }
        }
        return sum;
    }

    public void PrettyPrint() {
        for (int row = 0; row < 5; row++) {
            for (int col = 0; col < 5; col++) {
                int value = grid[row, col];
                if (marked[row, col]) {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write($"{value,-2} ");
                    Console.ResetColor();
                } else {
                    Console.Write($"{value,-2} ");
                }
            }
            Console.WriteLine();
        }
    }
}