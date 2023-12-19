using static csteeves.Advent2023.PartRanking;

namespace csteeves.Advent2023;

public class Condition {

    private enum Operator { GT, LT, TRUE }

    private readonly PartRanking.Category category;
    private readonly Operator op;
    private readonly int threshold;

    public readonly string targetName;

    public Condition(string s) {
        List<string> tokens = LineParser.Tokens(s, ":");
        if (tokens.Count == 1) {
            category = Category.UNSPECIFIED;
            op = Operator.TRUE;
            threshold = 0;

            targetName = tokens[0];
            return;
        }

        List<string> coperationTokens;
        if (tokens[0].Contains(">")) {
            coperationTokens = LineParser.Tokens(tokens[0], ">");
            op = Operator.GT;
        } else if (tokens[0].Contains("<")) {
            coperationTokens = LineParser.Tokens(tokens[0], "<");
            op = Operator.LT;
        } else {
            throw new ArgumentException();
        }

        category = PartRanking.CategoryFrom(coperationTokens[0]);
        threshold = int.Parse(coperationTokens[1]);

        targetName = tokens[1];
    }

    public bool Met(PartRanking ranking) {
        if (op == Operator.TRUE) {
            return true;
        }

        int value = ranking.Get(category);
        switch (op) {
            case Operator.GT:
                return value > threshold;
            case Operator.LT:
                return value < threshold;
        }

        throw new ArgumentOutOfRangeException();
    }

    public override string ToString() {
        return $"{category} {op} {threshold}";
    }
}