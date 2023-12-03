namespace csteeves.Advent2021;

public class SyntaxScoring : DaySolution2021 {

    private static readonly Dictionary<char, BracketPair> pairs =
        new Dictionary<char, BracketPair>() {
            {'(', new BracketPair('(', ')')},
            {'[', new BracketPair('[', ']')},
            {'{', new BracketPair('{', '}')},
            {'<', new BracketPair('<', '>')},
        };

    private static readonly Dictionary<char, int> mismatchScoring =
        new Dictionary<char, int>() {
            {')', 3},
            {']', 57},
            {'}', 1197},
            {'>', 25137},
        };

    private static readonly Dictionary<char, int> completionScoring =
        new Dictionary<char, int>() {
            {')', 1},
            {']', 2},
            {'}', 3},
            {'>', 4},
        };

    public override string Dir() {
        return "Day 10";
    }

    public override void Part1(List<string> input) {
        int mismatchScore = 0;

        foreach (string line in input) {
            Stack<BracketPair> stack = new Stack<BracketPair>();

            foreach (char c in line) {
                if (pairs.TryGetValue(c, out BracketPair? pair)) {
                    stack.Push(pair);
                    continue;
                }

                BracketPair top = stack.Pop();
                if (c != top.close) {
                    int score = mismatchScoring[c];
                    mismatchScore += score;
                    Console.WriteLine($"Mismatch found: {c} isntead of {top.close}: {score}");
                    break;
                }
            }
        }

        Console.WriteLine($"Mismatch score: {mismatchScore}");
    }

    public override void Part2(List<string> input) {
        int scoreRoundMultiplier = 5;
        List<long> lineCompletionScores = [];

        foreach (string line in input) {
            Stack<BracketPair> stack = new Stack<BracketPair>();
            bool invalidLine = false;

            foreach (char c in line) {
                if (pairs.TryGetValue(c, out BracketPair? pair)) {
                    stack.Push(pair);
                    continue;
                }

                if (!stack.TryPop(out BracketPair? top) || c != top.close) {
                    invalidLine = true;
                    break;
                }
            }

            if (invalidLine) {
                continue;
            }

            string completionString = "";
            long lineCompletionScore = 0;
            while (stack.TryPop(out BracketPair? top)) {
                completionString += top.close;
                lineCompletionScore =
                    (scoreRoundMultiplier * lineCompletionScore)
                        + completionScoring[top.close];
            }

            Console.WriteLine(
                $"Line score: {lineCompletionScore} for adding {completionString} to {line}");
            lineCompletionScores.Add(lineCompletionScore);
        }

        lineCompletionScores.Sort();
        long median = lineCompletionScores[lineCompletionScores.Count / 2];

        Console.WriteLine($"Completion score median: {median}");
    }
}