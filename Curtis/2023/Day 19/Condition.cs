namespace csteeves.Advent2023;

public class Condition {

    public enum Operator { GT, LT, TRUE }

    public readonly PartRanking.Category category;
    public readonly Operator op;
    public readonly int threshold;

    public readonly string targetName;

    public Condition(string s) {
        List<string> tokens = LineParser.Tokens(s, ":");
        if (tokens.Count == 1) {
            category = PartRanking.Category.UNSPECIFIED;
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

    private Condition(
            PartRanking.Category category, Operator op, int threshold, string targetName) {
        this.category = category;
        this.op = op;
        this.threshold = threshold;
        this.targetName = targetName;
    }

    public static Condition Inverse(Condition condition) {
        Operator op;
        int threshold = condition.threshold;

        switch (condition.op) {
            case Operator.GT:
                op = Operator.LT;
                threshold++;
                break;
            case Operator.LT:
                op = Operator.GT;
                threshold--;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return new Condition(condition.category, op, threshold, condition.targetName);
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