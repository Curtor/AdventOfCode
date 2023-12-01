namespace csteeves.Advent2023;

public class Trebuchet : DaySolution2023 {

    private const string dir = "Day 1";

    private List<string> tokens = [
        "0",
        "1",
        "2",
        "3",
        "4",
        "5",
        "6",
        "7",
        "8",
        "9",
        "one",
        "two",
        "three",
        "four",
        "five",
        "six",
        "seven",
        "eight",
        "nine"
    ];

    public override string Dir() {
        return dir;
    }

    public override void Part1(List<string> input) {
        int calibrationSum = 0;

        foreach (string line in input) {
            char first = line.First(c => c >= '0' && c <= '9');
            char last = line.Last(c => c >= '0' && c <= '9');

            calibrationSum += int.Parse(first.ToString() + last.ToString());
        }

        Console.WriteLine("Part 1");
        Console.WriteLine($"Calibration Sum: {calibrationSum}");
    }

    public override void Part2(List<string> input) {
        int calibrationSum = 0;

        foreach (string line in input) {
            Tuple<int, int> tokens = GetTokens(line);
            int value = 10 * tokens.Item1 + tokens.Item2;
            calibrationSum += value;
        }

        Console.WriteLine("Part 2");
        Console.WriteLine($"Calibration Sum: {calibrationSum}");
    }

    private Tuple<int, int> GetTokens(string line) {
        int firstIndex = int.MaxValue;
        int firstValue = -1;

        int lastIndex = -1;
        int lastValue = -1;

        foreach (string token in tokens) {
            int tokenFirstIndex = line.IndexOf(token);
            int tokenLastIndex = line.LastIndexOf(token);

            if (tokenFirstIndex >= 0 && tokenFirstIndex < firstIndex) {
                firstIndex = tokenFirstIndex;
                firstValue = GetValue(token);
            }

            if (tokenLastIndex >= 0 && tokenLastIndex > lastIndex) {
                lastIndex = tokenLastIndex;
                lastValue = GetValue(token);
            }
        }

        return Tuple.Create(firstValue, lastValue);
    }

    private int GetValue(string token) {
        if (token.Length == 1) {
            return int.Parse(token);
        }

        switch (token) {
            case "one":
                return 1;
            case "two":
                return 2;
            case "three":
                return 3;
            case "four":
                return 4;
            case "five":
                return 5;
            case "six":
                return 6;
            case "seven":
                return 7;
            case "eight":
                return 8;
            case "nine":
                return 9;
            default:
                throw new ArgumentException();
        }
    }
}