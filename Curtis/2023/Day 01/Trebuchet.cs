namespace csteeves.Advent2023;

public class Trebuchet : DaySolution2023 {

    public override string Dir() {
        return "Day 01";
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

        for (int i = 1; i <= 9; i++) {
            string token = i.ToString();

            CheckFirst(line, ref firstIndex, ref firstValue, i, token);
            CheckLast(line, ref lastIndex, ref lastValue, i, token);

            string writtenToken = ToWritten(i);
            CheckFirst(line, ref firstIndex, ref firstValue, i, writtenToken);
            CheckLast(line, ref lastIndex, ref lastValue, i, writtenToken);
        }

        return Tuple.Create(firstValue, lastValue);
    }

    private static void CheckFirst(
            string line,
            ref int firstIndex,
            ref int firstValue,
            int value,
            string token) {
        int tokenFirstIndex = line.IndexOf(token);
        if (tokenFirstIndex >= 0 && tokenFirstIndex < firstIndex) {
            firstIndex = tokenFirstIndex;
            firstValue = value;
        }
    }

    private static void CheckLast(
            string line,
            ref int lastIndex,
            ref int lastValue,
            int value,
            string token) {
        int tokenLastIndex = line.LastIndexOf(token);
        if (tokenLastIndex >= 0 && tokenLastIndex > lastIndex) {
            lastIndex = tokenLastIndex;
            lastValue = value;
        }
    }

    private string ToWritten(int value) {
        switch (value) {
            case 1:
                return "one";
            case 2:
                return "two";
            case 3:
                return "three";
            case 4:
                return "four";
            case 5:
                return "five";
            case 6:
                return "six";
            case 7:
                return "seven";
            case 8:
                return "eight";
            case 9:
                return "nine";
            default:
                throw new IndexOutOfRangeException();
        }
    }
}